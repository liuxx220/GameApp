/*
--------------------------------------------------------------------------------------
		file name : handle_listener.hpp
		desc      : 网络监听 socket 处理接口，监听接受网络消息处理器
		author    : LJP

		log		  : [ 2015-04-28 ]
--------------------------------------------------------------------------------------
*/
#include "network/CListener.hpp"
#include "network/CliSocket.hpp"
#include "network/Session.hpp"
#include "network/ISessionFactory.hpp"
#include "network/event_dispatcher.hpp"
#if KBE_PLATFORM == PLATFORM_WIN32
#include <process.h>
#include "network/event_iocp.hpp"
#else
#include "network/event_epoll.hpp"
#endif






namespace KBEngine 
{ 
	//-------------------------------------------------------------------------------------
	CListenerReceiver::CListenerReceiver()
	{
		m_poPacketParser	= NULL;
		m_poSessionFactory	= NULL;
		m_dwRecvBufSize		= DEFAULT_RECVBUF_SIZE;
		m_dwSendBufSize		= DEFAULT_SENDBUF_SIZE;
		m_bStart			= false;
	}

	//-------------------------------------------------------------------------------------
	CListenerReceiver::~CListenerReceiver()
	{
		if (m_ListenerSocket != INVALID_VALUE)
		{
			#ifdef unix
			::close(m_ListenerSocket);
			#else
			::closesocket(m_ListenerSocket);
			#endif
		}
	}


	bool CListenerReceiver::Start(const char* pszIP, UINT16 nPort, bool bReUseAddr)
	{
		m_ListenerSocket = socket(AF_INET, SOCK_STREAM, 0);
		if (m_ListenerSocket == INVALID_VALUE)
		{
			PRINT_MSG("CListenerReceiver: start Listener is error!\n");
			return false;
		}

		// reuse造成多次绑定成功，可能不能找到正确可用的端口
		// 如果两台服务器开在同一台机器会使用同一个端口，造成混乱
		if (bReUseAddr)
		{
			INT32 nReuse = 1;
			setsockopt(m_ListenerSocket, SOL_SOCKET, SO_REUSEADDR, (char*)&nReuse, sizeof(nReuse));
		}

		
		// disables the Nagle algorithm for send coalescing
		int arg = 1;
		setsockopt(m_ListenerSocket, IPPROTO_TCP, TCP_NODELAY, (char*)&arg, sizeof(int));


		// 绑定我们的Local 地址及端口
		sockaddr_in  addr;
		memset(&addr, 0, sizeof(sockaddr_in));
		addr.sin_family		= AF_INET;
		addr.sin_port		= htons(nPort);
		addr.sin_addr.s_addr= htonl(INADDR_ANY);
		

		int nerr = bind(m_ListenerSocket, (sockaddr*)&addr, sizeof(addr));
		if (SOCKET_ERROR == nerr)
		{
			ShutDownSocket(m_ListenerSocket);
			return false;
		}

		//监听该套接字
		if (listen(m_ListenerSocket, 128) < 0)
		{
			PRINT_MSG("listen at failed, errno");
			return false;
		}

		// 绑定到IO完成端口上
		#if KBE_PLATFORM == PLATFORM_WIN32
			if (false == CIocpPoller::Instance()->AssociateWithIocp(m_ListenerSocket, &m_stPerHandleData))
			{
				return false;
			}
		#else
			
		#endif

		return true;
	}

	bool CListenerReceiver::Stop( void )
	{
		if (m_ListenerSocket == INVALID_VALUE)
		{
			return true;
		}

		PRINT_MSG("CListenerReceiver: top Listener\n");
		m_bStart = false;
		#if KBE_PLATFORM == PLATFORM_WIN32
			CancelIo((HANDLE)m_ListenerSocket);
			::closesocket(m_ListenerSocket);
			_InitAccepteEx();
		#else
		::close(m_ListenerSocket);
		#endif

		m_ListenerSocket = INVALID_VALUE;
		return true;
	}

#if KBE_PLATFORM == PLATFORM_WIN32
	bool CListenerReceiver::_InitAccepteEx()
	{
		DWORD dwbytes = 0;
		GUID m_GuidAcceptEx = WSAID_ACCEPTEX;
		int iRc = ::WSAIoctl(
			m_ListenerSocket,
			SIO_GET_EXTENSION_FUNCTION_POINTER,
			&m_GuidAcceptEx,
			sizeof(m_GuidAcceptEx),
			&m_lpfnAcceptEx,
			sizeof(LPFN_ACCEPTEX),
			&dwbytes,
			NULL,
			NULL);

		GUID m_GuidGetAcceptExSockaddrs = WSAID_GETACCEPTEXSOCKADDRS;

		dwbytes = 0;

		iRc = ::WSAIoctl(
			m_ListenerSocket,
			SIO_GET_EXTENSION_FUNCTION_POINTER,
			&m_GuidGetAcceptExSockaddrs,
			sizeof(m_GuidGetAcceptExSockaddrs),
			&m_lpfnGetAcceptExSockAddr,
			sizeof(LPFN_GETACCEPTEXSOCKADDRS),
			&dwbytes,
			NULL,
			NULL);

		return true;
	}

	bool CListenerReceiver::PostAcceptEx(SPerIoData* pstPerIoData)
	{/*
		KBESOCKET newsock = socket(AF_INET, SOCK_STREAM, 0);
		if (newsock == INVALID_VALUE)
		{
			return false;
		}

		memset(&pstPerIoData->stOverlapped, 0, sizeof(pstPerIoData->stOverlapped));
		pstPerIoData->hSock		= newsock;
		pstPerIoData->nOp		= IOCP_ACCEPT;

		DWORD dwBytes;
		int bRet = m_lpfnAcceptEx(
			m_ListenerSocket,
			newsock,
			pstPerIoData->Buf,
			0,
			sizeof(SOCKADDR_IN)+16,
			sizeof(SOCKADDR_IN)+16,
			&dwBytes,
			&pstPerIoData->stOverlapped);

		if (S_OK != bRet)
		{
			if (WSA_IO_PENDING != ::WSAGetLastError())
			{
				closesocket(newsock);
				return false;
			}
		}*/
		return true;
	}

#endif
	void CListenerReceiver::CreateAcceptThread(void)
	{
		#if KBE_PLATFORM == PLATFORM_WIN32
		m_Handle = (THREAD_ID)_beginthreadex(NULL, 0, &CListenerReceiver::ListenAcceptThread, (void*)this, NULL, 0);
		#else	
		if (pthread_create(&m_Handle, NULL, &CListenerReceiver::ListenAcceptThread,(void*)this) != 0)
		{
			ERROR_MSG("createThread is error!");
		}
		#endif
	}


	//------------------------------------------------------------------------------------
	// 网络接受连接线程
	//------------------------------------------------------------------------------------
	#if KBE_PLATFORM == PLATFORM_WIN32
	unsigned __stdcall CListenerReceiver::ListenAcceptThread(void* arg)
	{
		bool isRun = true;
		CListenerReceiver* pListen = static_cast<CListenerReceiver*>(arg);
		while (isRun)
		{
			
		}

		return 0;
	}
	#else	
	void* CListenerReceiver::ListenAcceptThread( void* arg )
	{
		bool isRun = true;
		CListenerReceiver* pListen = static_cast<CListenerReceiver*>(arg);
		while (isRun)
		{
			EndPoint* pNewEndPoint = pListen->m_Session.extEndpoint().accept();
			if (pNewEndPoint == NULL)
			{
				continue;
			}

			Channel* pchannel = new Channel(pNewEndPoint, Channel::EXTERNAL);
			if (pchannel == NULL)
				continue;

			if (!pListen->m_Session.registerChannel(pchannel))
			{
				ERROR_MSG(fmt::format("ListenerReceiver::ListenAcceptThread:registerChannel({}) is failed!\n",
					pchannel->c_str()));
			}
		}

		return 0;
	}
	#endif
	

	void CListenerReceiver::OnAccept(BOOL bSucc, SPerIoData* pIoData, tagXClient* pTagClient )
	{
		KBESOCKET newsock = pIoData->hSock;
		if (m_bStart == false || !bSucc )
		{
			ShutDownSocket(newsock );
		}

		PostAcceptEx(pIoData);
		ClientSocket* pClientSocket = new ClientSocket();
		if (pClientSocket == NULL)
			return;

		if (0 != ::setsockopt(newsock, SOL_SOCKET, SO_UPDATE_ACCEPT_CONTEXT, (char *)&m_ListenerSocket, sizeof(SOCKET)))
		{
			ERROR_MSG("setsockopt for new socket on UpdateConetext failed,");
		}

		const CHAR szOpt = 1;
		if (0 != ::setsockopt(newsock, IPPROTO_TCP, TCP_NODELAY, (char *)&szOpt, sizeof(char)))
		{
			ERROR_MSG("setsockopt for new socket on UpdateConetext failed, errno");
		}
		
		pClientSocket->setsocket(newsock);
		pTagClient->bShutDown	= false;
		pTagClient->dwClientID	= GT_INVALID;
		pTagClient->lSendCast	= 0;
		pTagClient->lRecvCast	= 1;
		pTagClient->dwSendSize	= 0;
		pTagClient->pClient		= pClientSocket;
		pTagClient->dwConnectTime = GT_INVALID;

		DWORD dwBytes;
		int bRet = m_lpfnAcceptEx( m_ListenerSocket,newsock,pIoData->Buf,0,
								   sizeof(SOCKADDR_IN)+16,sizeof(SOCKADDR_IN)+16,&dwBytes,&pIoData->stOverlapped);

		if (S_OK != bRet)
		{
			if (WSA_IO_PENDING != ::WSAGetLastError())
			{
				closesocket(newsock);
				return;
			}
		}
		// 创建session
		if (m_poSessionFactory != NULL)
		{
			INetSession* pSession = m_poSessionFactory->CreateSession( ST_None );
			pSession->SetSocketObj(pClientSocket);

			UINT32 localaddr;
			UINT16 localport;
			NetUnity::getremoteaddress(newsock, &localport, &localaddr);
			pSession->SetRemotePort(localport);
			pSession->SeRemoteIP(localaddr);
		}
		else
		{
			ShutDownSocket(newsock);
		}

		if (false == pClientSocket->AssociateWithIOCP())
		{
			pClientSocket->closesock();
		}
	}
}
