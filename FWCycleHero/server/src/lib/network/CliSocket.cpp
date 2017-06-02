/*
-------------------------------------------------------------------------------------------------
file name : endpoint.hpp
desc	  : 对网络 socket 的封装
author	  : LJP

log		  : [ 2015-04-26 ]
-------------------------------------------------------------------------------------------------
*/
#include "CliSocket.hpp"
#include "network/event_iocp.hpp"
#include "helper/debug_helper.hpp"






namespace KBEngine
{

	#define POOL_PKG_SIZE 1024
	#define MAX_PKG_LEN		(1024*10)
	/*****************************************************************************************
		client socket
	*****************************************************************************************/
	ClientSocket::ClientSocket() : m_socket(-1), m_bConnected(false), m_pSendBuf(0)
	{
		m_stRecvIoData.nOp			= IOCP_RECV;
		m_stSendIoData.nOp			= IOCP_SEND;
		m_stPerHandleData.bListen	= false;
		m_stPerHandleData.ptr		= this;
		m_nDataReceived				= 0;
		m_IsShutDown				= false;
		m_bConnected				= false;
		m_pSendBuf					= new char[MAX_PKG_LEN];
		m_pRecvBuf					= new char[MAX_PKG_LEN];
		m_PoolSend					= new SafeMemPool(POOL_PKG_SIZE);
		m_PoolRecv					= new SafeMemPool(POOL_PKG_SIZE);
	}

	ClientSocket::~ClientSocket()
	{

	}


	/*******************************************************************************************
	* function :
	* desc     : 销毁对象及相关
	*******************************************************************************************/
	void ClientSocket::Destory()
	{
		SAFE_DEL_ARRAY(m_pSendBuf);

		while (m_SendQueue.GetNum() > 0)
		{
			tagUnit* pUnit = m_SendQueue.Get();
			if (pUnit == NULL)
				return;
			SAFE_DEL(pUnit);
		}
		SAFE_DEL(m_PoolSend);

		while (m_RecvQueue.GetNum() > 0)
		{
			tagUnit* pUnit = m_RecvQueue.Get();
			if (pUnit == NULL)
				return;
			SAFE_DEL(pUnit);
		}
		SAFE_DEL(m_PoolRecv);

		closesock();
	}

	/*****************************************************************************************
		
	*****************************************************************************************/
	void ClientSocket::connect(UINT32 nIP, UINT16 nPort)
	{
		sockaddr_in	sin;
		sin.sin_family		= AF_INET;
		sin.sin_port		= nPort;
		sin.sin_addr.s_addr = nIP;

		int ret = ::connect(m_socket, (sockaddr*)&sin, sizeof(sin));
		
		// 设置 socket noblock
		u_long val = 1;
		::ioctlsocket(m_socket, FIONBIO, &val);	

		// 设置 socket nodelay
		int arg = 1;
		setsockopt(m_socket, IPPROTO_TCP, TCP_NODELAY, (char*)&arg, sizeof(int));
	}


	//-------------------------------------------------------------------------------------------
	//  function : 
	//  desc     : 关闭网络
	//-------------------------------------------------------------------------------------------
	void ClientSocket::closesock()
	{
		if (m_socket == -1)
			return;

		m_bConnected = false;
		m_IsShutDown = true;
		::closesocket(m_socket);
	}


	void ClientSocket::sendmsg( void )
	{
		/////////////////////////////////////////////////////////////////////////////////////
		// 合并包发送
		UINT32 dwTotal = 0;
		while ( m_SendQueue.GetNum() > 0 )
		{
			tagUnit* pUnit = m_SendQueue.Get();
			if (pUnit == NULL)
				return;

			if ((pUnit->dwSize + dwTotal) >= POOL_PKG_SIZE)
			{
				m_SendQueue.AddFront(pUnit);
				break;
			}
			
			memcpy(m_pSendBuf + dwTotal, pUnit->Buf, pUnit->dwSize);
			dwTotal += pUnit->dwSize;

			// 归还池子内存
			m_PoolSend->Free(pUnit);
		}

		if (dwTotal = 0)
			return;

		memset(&m_stSendIoData.stOverlapped, 0, sizeof(m_stSendIoData.stOverlapped));

		// 在包头填写大小和压缩大小
		m_stSendIoData.stWsaBuf.buf = m_pSendBuf;
		m_stSendIoData.stWsaBuf.len = dwTotal;

		DWORD dwNumberOfBytesSend	= 0;
		if (0 != WSASend(m_socket, &m_stSendIoData.stWsaBuf, 1, &dwNumberOfBytesSend, 0, &m_stSendIoData.stOverlapped, NULL))
		{
			if (WSAGetLastError() != WSA_IO_PENDING)
			{
				return;
			}
		}
	}

	INT32 ClientSocket::SyncSend(const char* pBuf, UINT32 dwLen)
	{
		WSABUF  sndBuf;
		sndBuf.buf = (char*)pBuf;
		sndBuf.len = dwLen;

		DWORD dwNumberOfBytesSend = 0;
		if (0 != WSASend(m_socket, &sndBuf, 1, &dwNumberOfBytesSend, 0, NULL, NULL))
		{
			INT32 errNO = WSAGetLastError();
			if (errNO == WSA_IO_PENDING)
			{
				closesock();
				return -1;
			}

			KBE_ASSERT(dwLen == dwNumberOfBytesSend);
		}
		return dwNumberOfBytesSend;
	}


	//------------------------------------------------------------------------------------------
	// function :
	// desc     : 放到發送隊列中
	//--------------------------------------------------------------------------------------------
	void ClientSocket::Send(const char* pMsg, UINT32 dwLen)
	{
		if ( IsConnected() )
		{
			tagUnit* pMsgUnit = (tagUnit*)(m_PoolSend->TryAlloc(dwLen));
			pMsgUnit->dwSize  = dwLen;
			pMsgUnit->pNext	  = NULL;
			memcpy( pMsgUnit->Buf, pMsg, dwLen );
			m_SendQueue.Add( pMsgUnit );
		}
	}


	//--------------------------------------------------------------------------------------------
	// function : 
	// desc     : 从网络上接受固定数据长度的
	//--------------------------------------------------------------------------------------------
	void ClientSocket::PostRecv()
	{
		memset(&m_stRecvIoData.stOverlapped, 0, sizeof(m_stRecvIoData.stOverlapped));
		m_stRecvIoData.stWsaBuf.buf = m_pRecvBuf;
		m_stRecvIoData.stWsaBuf.len = MAX_PKG_LEN;

		if (m_stRecvIoData.stWsaBuf.len > MAX_PKG_LEN)
		{
			m_stRecvIoData.stWsaBuf.len = MAX_PKG_LEN;
		}

		DWORD  dwBytes;
		DWORD  dwFlag = 0;		// 這裏一定要設置成0 否則會包10045 錯誤
		if (WSARecv(m_socket, &m_stRecvIoData.stWsaBuf, 1, &dwBytes, &dwFlag, &m_stRecvIoData.stOverlapped, NULL) != 0)
		{
			int err = WSAGetLastError();
			if (err != WSA_IO_PENDING)
			{
				return;
			}
		}
	}

	void ClientSocket::recvmsg(UINT32 dwBytes)
	{
		m_nDataReceived += dwBytes;
		
		char* p			 = m_pRecvBuf;
		while (m_nDataReceived > 0)
		{
			INT32 nUsedLen = m_nDataReceived;
			if (0 == nUsedLen)
			{
				// 如果一次接受缓冲区已满， 但是没有装下一个完成包， 只好断开连接了
				if (m_nDataReceived >= MAX_PKG_LEN)
				{
					this->closesock();
					return;
				}
			}
			else if (nUsedLen > 0)
			{
				// 如果单个包超过规定大小，认为异常
				if (nUsedLen > MAX_PAGE_SIZE)
				{
					this->closesock();
					return;
				}

				/// 处理接受数据
				tagUnit* pUnit = (tagUnit*)m_PoolRecv->Alloc(sizeof(tagUnit));
				pUnit->dwSize  = nUsedLen;
				memset(pUnit->Buf, 0, MAX_PAGE_SIZE);
				
				
				{
					memcpy(pUnit->Buf, p, nUsedLen);
				}
				m_RecvQueue.Add(pUnit);

				p += nUsedLen;
				m_nDataReceived -= nUsedLen;
			}
		}

		PostRecv();
	}

	//-------------------------------------------------------------------------------------------
	// function :
	// desc     : 得到发送缓存的大小
	//-------------------------------------------------------------------------------------------
	int ClientSocket::GetBufferSize(int optname) const
	{
		KBE_ASSERT(optname == SO_SNDBUF || optname == SO_RCVBUF);
		int recvbuf = -1;
		socklen_t rbargsize = sizeof(int);
		int rberr = getsockopt(m_socket, SOL_SOCKET, optname, (char*)&recvbuf, &rbargsize);
		if (rberr == 0 && rbargsize == sizeof(int))
			return recvbuf;

		return -1;
	}

	bool ClientSocket::SetBufferSize(int optname, int size)
	{
		setsockopt(m_socket, SOL_SOCKET, optname, (const char*)&size, sizeof(size));
		return this->GetBufferSize(optname) >= size;
	}


	//-------------------------------------------------------------------------------------------
	//  function : 
	//  desc     : 绑定完成端口模式
	//-------------------------------------------------------------------------------------------
	bool ClientSocket::AssociateWithIOCP()
	{
		return CIocpPoller::Instance()->AssociateWithIocp(m_socket, &m_stPerHandleData);
	}


	//-------------------------------------------------------------------------------------------
	//  function : 
	//  desc     : 得到该客户端已经接受的一个网络消息
	//-------------------------------------------------------------------------------------------
	tagUnit* ClientSocket::GetRecvMessages(UINT32& dwSize, INT32& nRecvNum)
	{

		nRecvNum		= m_RecvQueue.GetNum();
		tagUnit* pUnit	= m_RecvQueue.Get();
		if (pUnit == NULL)
			return NULL;

		dwSize = pUnit->dwSize;
		return pUnit;
	}
}
