/*
-------------------------------------------------------------------------------------------------
		file name : endpoint.hpp
		desc	  : 对网络 socket 的封装
		author	  : LJP

		log		  : [ 2015-04-26 ]
-------------------------------------------------------------------------------------------------
*/
#include "INetSocket.hpp"
#include "network/event_iocp.hpp"
#include "network/Connection.hpp"
#include "helper/debug_helper.hpp"




namespace KBEngine 
{ 
	
	#define MAX_PKG_LEN		(1024*1024*10)
	INT32 RECV_LOOP_BUF_SIZE = (10 * 1024 * 1024);	//10M
	/*****************************************************************************************
								client socket 
	*****************************************************************************************/
	ClientSocket::ClientSocket() : m_socket(-1), m_bConnected(false), m_pSendBuf(0), m_pRecvBuf(0)
								 , m_pPacketParser(0)
	{
		m_stRecvIoData.nOp		  = IOCP_RECV;
		m_stSendIoData.nOp		  = IOCP_SEND;
		m_stCloseIoData.nOp		  = IOCP_CLOSE;
		m_stPerHandleData.bListen = false;
		m_stPerHandleData.ptr	  = this;
	}

	ClientSocket::~ClientSocket()
	{
		
	}


	UINT32 ClientSocket::send(const char* pBuf, UINT32 dwBytes)
	{
		bool bSend = false;
		if (!IsConnected())
		{
			return 0;
		}

		if (dwBytes > m_dwSendBufSize)
		{
			syncSend(pBuf, dwBytes);
			return 1;
		}

		if (m_nDataSend == 0)
		{
			bSend = true;
		}

		memcpy(m_pSendBuf + m_nDataSend, pBuf, dwBytes);
		m_nDataSend += dwBytes;
		asyncSend(m_pSendBuf, m_nDataSend);
	}

	//在缓冲区不够的情况下，使用同步发送
	UINT32 ClientSocket::syncSend(const char* pBuf, UINT32 dwLen)
	{
		WSABUF sndBuf;
		sndBuf.buf = (char*)pBuf;
		sndBuf.len = dwLen;

		DWORD dwNumberOfBytesSent = 0;
		if (0 != WSASend(m_socket, &sndBuf, 1, &dwNumberOfBytesSent, 0, NULL, NULL))
		{
			INT32 errNO = WSAGetLastError();
			if (errNO != WSA_IO_PENDING)
			{
				close();
				return -1;
			}
		}

		KBE_ASSERT(dwLen == dwNumberOfBytesSent);
		return dwNumberOfBytesSent;
	}


	// 异步发送数据
	bool ClientSocket::asyncSend(const char* pBuf, UINT32 dwLen)
	{
		m_stSendIoData.stWsaBuf.buf = (char*)pBuf;
		m_stSendIoData.stWsaBuf.len = dwLen;

		DWORD dwNumberOfBytesSent = 0;
		ZeroMemory(&m_stSendIoData.stOverlapped, sizeof(m_stSendIoData.stOverlapped));
		if (0 != WSASend(m_socket, &m_stSendIoData.stWsaBuf, 1, &dwNumberOfBytesSent, 0, &m_stSendIoData.stOverlapped, NULL))
		{
			if (WSAGetLastError() != WSA_IO_PENDING)
			{
				return false;
			}
		}
		IncPostSend();
		return true;
	}

	UINT32	ClientSocket::recv(UINT32 dwBytes)
	{
		m_nDataReceived += dwBytes;
		char*p = m_pRecvBuf;
		while (m_nDataReceived > 0)
		{
			int nUsed = m_nDataReceived;
			if (m_pPacketParser != NULL)
			{
				nUsed = m_pPacketParser->ParsePacket(p, m_nDataReceived);
			}
			if (0 == nUsed)
			{
				//接收缓冲区已满，但还没有装下一个完整包，只好断开连接
				if (m_nDataReceived >= (INT32)m_dwRecvBufSize || m_nDataReceived >= MAX_PKG_LEN || m_nDataReceived >= RECV_LOOP_BUF_SIZE)
				{
					close();
					return;
				}
				break;
			}
			else if (nUsed > 0)
			{
				if (nUsed > m_nDataReceived)
				{
					close();
					return;
				}

				p += nUsed;
				m_nDataReceived -= nUsed;
			}
			else
			{
				close();
				return;
			}
		}

		if (m_nDataReceived > 0 && (p != m_pRecvBuf))
		{
			memmove(m_pRecvBuf, p, m_nDataReceived);
		}

		if (!IsConnected())
		{
			close();
			return;
		}
	}

	//-------------------------------------------------------------------------------------
	int ClientSocket::GetBufferSize(int optname) const
	{
		KBE_ASSERT(optname == SO_SNDBUF || optname == SO_RCVBUF);
		int recvbuf = -1;
		socklen_t rbargsize = sizeof(int);
		int rberr = getsockopt(m_socket, SOL_SOCKET, optname,(char*)&recvbuf, &rbargsize);
		if (rberr == 0 && rbargsize == sizeof(int))
			return recvbuf;

		return -1;
	}

	bool ClientSocket::SetBufferSize(int optname, int size)
	{
		setsockopt(m_socket, SOL_SOCKET, optname, (const char*)&size, sizeof(size));
		return this->GetBufferSize(optname) >= size;
	}

	int ClientSocket::Setnodelay(bool nodelay)
	{
		int arg = int(nodelay);
		return setsockopt(m_socket, IPPROTO_TCP, TCP_NODELAY, (char*)&arg, sizeof(int));
	}

	int ClientSocket::Setnonblocking(bool nonblocking)
	{
		#ifdef unix
			int val = nonblocking ? O_NONBLOCK : 0;
			return ::fcntl(socket_, F_SETFL, val);
		#elif defined(PLAYSTATION3)
			int val = nonblocking ? 1 : 0;
			return setsockopt(m_socket, SOL_SOCKET, SO_NBIO, &val, sizeof(int));
		#else
			u_long val = nonblocking ? 1 : 0;
			return ::ioctlsocket(m_socket, FIONBIO, &val);
		#endif
	}

	int ClientSocket::Setbroadcast(bool broadcast)
	{
		#ifdef unix
		int val;
		if (broadcast)
		{
			val = 2;
			::setsockopt(m_socket, SOL_IP, IP_MULTICAST_TTL, &val, sizeof(int));
		}
		#else
		bool val;
		#endif
		val = broadcast ? 1 : 0;
		return ::setsockopt(m_socket, SOL_SOCKET, SO_BROADCAST, (char*)&val, sizeof(val));
	}

	int ClientSocket::Setreuseaddr(bool reuseaddr)
	{
		#ifdef unix
		int val;
		#else
		bool val;
		#endif
		val = reuseaddr ? 1 : 0;
		return ::setsockopt(m_socket, SOL_SOCKET, SO_REUSEADDR,(char*)&val, sizeof(val));
	}

	int ClientSocket::Setkeepalive(bool keepalive)
	{
		#ifdef unix
		int val;
		#else
		bool val;
		#endif
		val = keepalive ? 1 : 0;
		return ::setsockopt(m_socket, SOL_SOCKET, SO_KEEPALIVE, (char*)&val, sizeof(val));
	}


	/*******
	* @brief 本函数不能保证线程安全
	*/
	bool ClientSocket::AssociateWithIOCP()
	{
		return CIocpPoller::Instance()->AssociateWithIocp(m_socket, &m_stPerHandleData);
	}
		
	/*****************************************************************************************
			CServerSocket Begin
	*****************************************************************************************/
	CServerSocket::CServerSocket() : m_sockListen(-1)
	{

	}

	CServerSocket::CServerSocket(int fd) : m_sockListen(fd)
	{

	}

	CServerSocket::CServerSocket(UINT32 networkAddr, UINT16 networkPort) : m_sockListen(-1)
	{

	}


	CServerSocket::~CServerSocket()
	{
		if (IsValied())
		{
			close();
		}
	}

	//-------------------------------------------------------------------------------------
	int CServerSocket::GetBufferSize(int optname) const
	{
		KBE_ASSERT(optname == SO_SNDBUF || optname == SO_RCVBUF);
		int recvbuf = -1;
		socklen_t rbargsize = sizeof(int);
		int rberr = getsockopt(m_sockListen, SOL_SOCKET, optname, (char*)&recvbuf, &rbargsize);
		if (rberr == 0 && rbargsize == sizeof(int))
			return recvbuf;

		return -1;
	}

	bool CServerSocket::SetBufferSize(int optname, int size)
	{
		setsockopt(m_sockListen, SOL_SOCKET, optname, (const char*)&size, sizeof(size));
		return this->GetBufferSize(optname) >= size;
	}

	int CServerSocket::Setnodelay(bool nodelay)
	{
		int arg = int(nodelay);
		return setsockopt(m_sockListen, IPPROTO_TCP, TCP_NODELAY, (char*)&arg, sizeof(int));
	}

	int CServerSocket::Setnonblocking(bool nonblocking)
	{
		#ifdef unix
			int val = nonblocking ? O_NONBLOCK : 0;
			return ::fcntl(socket_, F_SETFL, val);
		#elif defined(PLAYSTATION3)
			int val = nonblocking ? 1 : 0;
			return setsockopt(m_sockListen, SOL_SOCKET, SO_NBIO, &val, sizeof(int));
		#else
			u_long val = nonblocking ? 1 : 0;
			return ::ioctlsocket(m_sockListen, FIONBIO, &val);
		#endif
	}

	int CServerSocket::Setbroadcast(bool broadcast)
	{
		#ifdef unix
		int val;
		if (broadcast)
		{
			val = 2;
			::setsockopt(m_sockListen, SOL_IP, IP_MULTICAST_TTL, &val, sizeof(int));
		}
		#else
		bool val;
		#endif
		val = broadcast ? 1 : 0;
		return ::setsockopt(m_sockListen, SOL_SOCKET, SO_BROADCAST, (char*)&val, sizeof(val));
	}

	int CServerSocket::Setreuseaddr(bool reuseaddr)
	{
		#ifdef unix
		int val;
		#else
		bool val;
		#endif
		val = reuseaddr ? 1 : 0;
		return ::setsockopt(m_sockListen, SOL_SOCKET, SO_REUSEADDR, (char*)&val, sizeof(val));
	}

	int CServerSocket::Setkeepalive(bool keepalive)
	{
		#ifdef unix
		int val;
		#else
		bool val;
		#endif
		val = keepalive ? 1 : 0;
		return ::setsockopt(m_sockListen, SOL_SOCKET, SO_KEEPALIVE, (char*)&val, sizeof(val));
	}

	/*
	------------------------------------------------------------------------------------------------------
	* @brief 开始监听
	* @param pszIP : 本地IP地址
	* @param wPort : 本地端口号
	* @param bReUseAddr : 是否端口复用
	* @return 监听成功返回true,监听失败返回false
	------------------------------------------------------------------------------------------------------
	*/
	bool CServerSocket::StartListener(const char* pszIP, UINT16 wPort, bool bReUseAddr)
	{
		m_sockListen = socket(AF_INET, SOCK_STREAM, 0);
		if (m_sockListen == -1)
		{
			ERROR_MSG( fmt::format( " create listener socket error !" ));
			return false;
		}

		//是否允许端口复用
		if (bReUseAddr)
		{
			INT32 nReuse = 1;
			setsockopt(m_sockListen, SOL_SOCKET, SO_REUSEADDR, (char*)&nReuse, sizeof(nReuse));
		}

		//绑定IP及端口
		sockaddr_in stAddr = { 0 };
		stAddr.sin_family = AF_INET;
		if (0 == strcmp(pszIP, "0"))
		{
			stAddr.sin_addr.s_addr = htonl(INADDR_ANY);
		}
		else
		{
			stAddr.sin_addr.s_addr = inet_addr(pszIP);
		}
		stAddr.sin_port = htons(wPort);

		if (bind(m_sockListen, (sockaddr*)&stAddr, sizeof(stAddr)) < 0)
		{
			ERROR_MSG(fmt::format(" create listener socket error !"));
			return false;
		}

		//监听该套接字
		if (listen(m_sockListen, 128) < 0)
		{
			ERROR_MSG(fmt::format(" create listener socket error !"));
			return false;
		}

		return true;
	}

	ClientSocket* CServerSocket::accept(UINT16 * networkPort = NULL, UINT32 * networkAddr = NULL, bool autosetflags)
	{
		sockaddr_in		sin;
		socklen_t		sinLen = sizeof(sin);
		int ret = ::accept(m_sockListen, (sockaddr*)&sin, &sinLen);
		#if defined(unix) || defined(PLAYSTATION3)
			if (ret < 0) return NULL;
		#else
			if (ret == INVALID_SOCKET) return NULL;
		#endif

		ClientSocket* pClient = new ClientSocket();
		pClient->SetSocket(ret);
		pClient->Setnonblocking(true);
		pClient->Setnodelay(true);
		return pClient;
	}

	/*
	------------------------------------------------------------------------------------------------------
	* @brief 停止监听
	* @return void
	------------------------------------------------------------------------------------------------------
	*/
	void CServerSocket::StopListener()
	{
		if (m_sockListen == -1)
			return;

		#ifdef unix
		int ret = ::close(m_socket);
		#elif defined(PLAYSTATION3)
		int ret = ::socketclose(m_sockListen);
		#else
		int ret = ::closesocket(m_sockListen);
		#endif
	}

	/*
	------------------------------------------------------------------------------------------------------
	* @brief 当客户端发送一个建立连接请求时，执行此函数
	* @param bSucc : 此连接请求是否成功，true为成功
	* @param pstPerIoData : 此建立连接请求的每IO数据
	* @return void
	------------------------------------------------------------------------------------------------------
	*/
	void CServerSocket::OnAccept(bool bSucc, SPerIoData* pstPerIoData)
	{
		KBESOCKET socket = pstPerIoData->hSock;
		if (!m_bStart)
		{
			closesocket(socket);
			return;
		}

		PostAcceptEx(pstPerIoData);
		if (bSucc)
		{
			// 创建Session
			ClientSocket* pClient	= new ClientSocket();
			Connection* pConnection = new Connection();
			if (pConnection == NULL)
				return;

			if (0 != ::setsockopt(socket, SOL_SOCKET, SO_UPDATE_ACCEPT_CONTEXT, (char *)&m_sockListen, sizeof(SOCKET)))
			{
				ERROR_MSG("setsockopt for new socket on UpdateConetext failed, errno");
			}

			const char szOpt = 1;
			if (0 != ::setsockopt(socket, IPPROTO_TCP, TCP_NODELAY, (char *)&szOpt, sizeof(char)))
			{
				ERROR_MSG("setsockopt for new socket on UpdateConetext failed, errno");
			}

			ISession* pSession = m_pSessionFactory->CreateSession( NULL );
			if (pSession == NULL)
			{
				closesocket(socket);
				return;
			}

			// 设置连接相关的session
			pClient->SetSocket(socket);
			pClient->SetConnected(true);
			pConnection->SetSession(pSession);

#if KBE_PLATFORM == PLATFORM_WIN32
			// 绑定IOCP
			if (!pClient->AssociateWithIOCP())
			{
				pClient->close();
			}
			else
			{
				if ( !pClient->Po)
			}
#endif
		}
		else
		{
			closesocket(socket);
		}
	}


	/*
	------------------------------------------------------------------------------------------------------
	* @brief 传输一个建立连接的每IO数据
	* @param pstPerIoData : 此建立连接请求的每IO数据
	* @return 是否建立连接成功，true为成功
	------------------------------------------------------------------------------------------------------
	*/
	bool CServerSocket::PostAcceptEx(SPerIoData* pstPerIoData)
	{
		KBESOCKET newsocket = socket(AF_INET, SOCK_STREAM, 0);
		if (newsocket)
		{
			ERROR_MSG(fmt::format(" Post Acceptex failed !"));
			return false;
		}

		memset(&pstPerIoData->stOverlapped, 0, sizeof(pstPerIoData->stOverlapped));
		pstPerIoData->hSock = newsocket;
		pstPerIoData->nOp	= IOCP_ACCEPT;
		return true;
	}
}
