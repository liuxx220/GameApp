/*
------------------------------------------------------------------------------------------------
file name : event_iocp.hpp
desc	  : 网络 windows 下 完成端口网络模型
author    : LJP

log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/
#include "event_iocp.hpp"
#include <process.h>
#include "network/CListener.hpp"
#include "network/CliSocket.hpp"







#if KBE_PLATFORM == PLATFORM_WIN32
namespace KBEngine 
{ 

	CIocpPoller* CIocpPoller::g_IocpInstance = NULL;
	//-------------------------------------------------------------------------------------
	CIocpPoller::CIocpPoller() : m_hCompletionPort(NULL), m_pClient(NULL)
	{
		m_lpfnAcceptEx				= 0;
		m_lpfnGetAcceptExSockAddr	= 0;
		m_nNumberOfWorkers			= 0;
		g_IocpInstance				= this;
	
		m_sockListen				= INVALID_SOCKET;
		m_stPerHandleData.bListen	= true;
		m_stPerHandleData.ptr		= this;
		
		m_listFreeClient.Clear();
		m_listDestroyClient.Clear();
	}

	CIocpPoller::~CIocpPoller()
	{
		Destroy();
	}


	CIocpPoller* CIocpPoller::Instance()
	{
		return g_IocpInstance;
	}
	
	/*
	--------------------------------------------------------------------------------------------------
	* @brief 初始化IOCP控制类
	* @根据配置的参数初始化client 相关的数据
	--------------------------------------------------------------------------------------------------
	*/
	bool CIocpPoller::InitNetEngine(UINT32 nPort)
	{

		m_hCompletionPort = CreateIoCompletionPort(INVALID_HANDLE_VALUE, NULL, 0, 0);
		DWORD nErr = GetLastError();
		if (m_hCompletionPort == NULL)
		{
			ERROR_MSG(fmt::format(" create CreateIoCompletion error !"));
			return false;
		}


		m_pClient = new ClientSocket[4096];
		if (m_pClient == NULL )
			return false;

		for (int i = 0; i < 4096; i++)
		{
			ClientSocket* pFree = new ClientSocket();
			m_listFreeClient.PushBack(pFree);
		}

		return CreateIoCompletion(nPort);
	}

	/*
	--------------------------------------------------------------------------------------------------
	* @brief 初始化IOCP控制类
	* @return 是否初始化成功，true为成功，false为失败
	--------------------------------------------------------------------------------------------------
	*/
	bool CIocpPoller::CreateIoCompletion( UINT16 nPort )
	{
		
		m_sockListen = socket(AF_INET, SOCK_STREAM, 0);
		if (m_sockListen == -1)
		{
			ERROR_MSG("Create listen socket error!");
			return false;
		}
		// reuse造成多次绑定成功，可能不能找到正确可用的端口
		// 如果两台服务器开在同一台机器会使用同一个端口，造成混乱
		//if (0)
		{
			INT32 nReuse = 1;
			setsockopt(m_sockListen, SOL_SOCKET, SO_REUSEADDR, (char*)&nReuse, sizeof(nReuse));
		}

		// disables the Nagle algorithm for send coalescing
		//int arg = 1;
		//setsockopt(m_sockListen, IPPROTO_TCP, TCP_NODELAY, (char*)&arg, sizeof(int));


		sockaddr_in address = { 0 };
		ZeroMemory(&address, sizeof(sockaddr_in));
		address.sin_family		= AF_INET;
		address.sin_addr.s_addr = htonl(INADDR_ANY);
		address.sin_port		= htons(nPort);

		int nerr = bind(m_sockListen, (sockaddr*)&address, sizeof(address));
		if (SOCKET_ERROR == nerr)
		{
			return false;
		}

		if (listen(m_sockListen, 5) < 0)
		{
			ERROR_MSG("listen socket error!");
			return false;
		}

		// 绑定完成端口
		CreateIoCompletionPort((HANDLE)m_sockListen, m_hCompletionPort, (ULONG_PTR)&m_stPerHandleData, 0);

		// 获得AcceptEx\GetAcceptExSockAddrs的函数指针，直接调用更加快捷
		GUID m_GuidAcceptEx = WSAID_ACCEPTEX;
		DWORD dwBytes = 0;
		::WSAIoctl(m_sockListen, SIO_GET_EXTENSION_FUNCTION_POINTER, 
								&m_GuidAcceptEx,
								sizeof(m_GuidAcceptEx), &m_lpfnAcceptEx, 
								sizeof(LPFN_ACCEPTEX), &dwBytes, NULL, NULL);

		GUID m_GuidGetAcceptExSockaddrs = WSAID_GETACCEPTEXSOCKADDRS;
		::WSAIoctl(m_sockListen, SIO_GET_EXTENSION_FUNCTION_POINTER, 
								&m_GuidGetAcceptExSockaddrs,
								sizeof(m_GuidGetAcceptExSockaddrs), 
								&m_lpfnGetAcceptExSockAddr, 
								sizeof(LPFN_GETACCEPTEXSOCKADDRS), &dwBytes, NULL, NULL);

		// 初始化重叠IO数据
		m_pPerIoDataArray = new SPerIoData[5];
		if (m_pPerIoDataArray == nullptr)
			return false;

		for (INT32 i = 0; i < 5; i++)
		{
			SOCKET hNewSocket = WSASocket(AF_INET, SOCK_STREAM, 0, NULL, 0, WSA_FLAG_OVERLAPPED);
			if (hNewSocket == INVALID_SOCKET)
			{
				return false;
			}
			memset(&m_pPerIoDataArray[i].stOverlapped, 0, sizeof(m_pPerIoDataArray[i].stOverlapped));
			m_pPerIoDataArray[i].hSock = hNewSocket;
			m_pPerIoDataArray[i].nOp   = IOCP_ACCEPT;

			int bRet = m_lpfnAcceptEx(m_sockListen, hNewSocket, m_pPerIoDataArray[i].Buf, 0,
									  sizeof(SOCKADDR_IN)+16, 
									  sizeof(SOCKADDR_IN)+16, &dwBytes, &m_pPerIoDataArray[i].stOverlapped);
			if (S_OK != bRet)
			{
				if (WSA_IO_PENDING != WSAGetLastError())
				{
					closesocket(hNewSocket);
					return false;
				}
			}
		}

		// 创建监听线程
		SYSTEM_INFO	 sysInfo;
		GetSystemInfo(&sysInfo);
		m_nNumberOfWorkers = sysInfo.dwNumberOfProcessors * 2;
		m_WorkerArray = new HANDLE[m_nNumberOfWorkers];
		for (INT32 i = 0; i < m_nNumberOfWorkers; i++)
		{
			m_WorkerArray[i] = INVALID_HANDLE_VALUE;
		}

		// 接受监听线程和接受消息线程公用
		UINT32 dwThreadID = 0;
		for (INT32 i = 0; i < m_nNumberOfWorkers; i++)
		{
			m_WorkerArray[i] = (HANDLE)_beginthreadex(NULL, 0, &CIocpPoller::ListenAcceptThread, (void*)this, NULL, &dwThreadID );
			if (m_WorkerArray[i] == NULL)
			{
				ERROR_MSG("CIocpCtrl::Init, Create Worker thread failed, Close Handler");
				this->Destroy();
				return false;
			}
		}
		
		// 发送线程
		HANDLE hSend = (HANDLE)_beginthreadex(NULL, 0, &CIocpPoller::ThreadSendProc, (void*)this, NULL, &dwThreadID);
		if (hSend == NULL)
		{
			ERROR_MSG("CIocpCtrl::Init, Create send thread failed, Close Handler");
			this->Destroy();
			return false;
		}
		return true;
	}


	/*
	--------------------------------------------------------------------------------------------------
	* @brief 初始化IOCP控制类
	* @return 是否初始化成功，true为成功，false为失败
	--------------------------------------------------------------------------------------------------
	*/
	void CIocpPoller::Destroy( )
	{
		if (m_nNumberOfWorkers == 0)
		{
			return;
		}

		m_listFreeClient.Clear();
		m_listDestroyClient.Clear();

		INT32 i = 0;
		for (i = 0; i < m_nNumberOfWorkers; i++)
		{
			PostQueuedCompletionStatus(m_hCompletionPort, 0, NULL, NULL);
		}
		WaitForMultipleObjects(m_nNumberOfWorkers, m_WorkerArray, true, 3000);
		for (i = 0; i < m_nNumberOfWorkers; i++)
		{
			CloseHandle(m_WorkerArray[i]);
		}

		SAFE_DEL_ARRAY(m_WorkerArray);


		PostQueuedCompletionStatus( m_hCompletionPort, 0, NULL, NULL );
		SAFE_CLOSE_HANDLE(m_hCompletionPort);
		
		closesocket(m_sockListen);
		SAFE_DEL_ARRAY(m_pPerIoDataArray);
		CloseHandle(m_hCompletionPort);
		m_hCompletionPort = 0;
	}

	//------------------------------------------------------------------------------------
	// 网络接受连接线程
	//------------------------------------------------------------------------------------
	UINT32 WINAPI CIocpPoller::ListenAcceptThread(void* arg)
	{
		CIocpPoller* pListen = static_cast<CIocpPoller*>(arg);
		if (pListen != NULL)
		{
			pListen->ThreadSetupAcceptEx();
		}
		return 0;
	}

	//------------------------------------------------------------------------------------
	// 网络发送线程
	//------------------------------------------------------------------------------------
	UINT32 WINAPI CIocpPoller::ThreadSendProc(void* arg)
	{
		CIocpPoller* pListen = static_cast<CIocpPoller*>(arg);
		if (pListen != NULL)
		{
			pListen->ThreadSendProceEX();
		}
		return 0;
	}


	//------------------------------------------------------------------------------------
	// 网络接受连接线程
	//------------------------------------------------------------------------------------
	void CIocpPoller::ThreadSetupAcceptEx( )
	{
		SPerHandleData*		pstPerHandleData = NULL;
		ClientSocket*		pSocket;
		BOOL				bRet;
		SPerIoData*			pOverlapped = NULL;
		DWORD				dwByteTrabsferred;

		while (true )
		{
			pstPerHandleData	= NULL;
			pOverlapped			= NULL;
			dwByteTrabsferred	= 0;
			bRet = GetQueuedCompletionStatus(m_hCompletionPort, &dwByteTrabsferred,
												(PULONG_PTR)&pstPerHandleData, (LPOVERLAPPED*)&pOverlapped, INFINITE);
			// 检查是否是线程退出
			if (NULL == pstPerHandleData)
				return;

			// 当有客户端请求创建连接的时候
			if (pstPerHandleData->bListen)
			{
				GT_VALID(pstPerHandleData);
				OnAccept(pOverlapped);
				continue;
			}

			pSocket = (ClientSocket*)pstPerHandleData->ptr;
			if (!bRet )
			{
				ERROR_MSG(fmt::format("[connected msg is error, close socket]\n"));
				pSocket->closesock();
				continue;
			}

			switch (pOverlapped->nOp)
			{
			case IOCP_RECV:
				{
					if (dwByteTrabsferred > 0)
						pSocket->recvmsg(dwByteTrabsferred);
					else
						pSocket->closesock();
				}
				break;

			case IOCP_SEND:
				{
					if (dwByteTrabsferred > 0)
						pSocket->sendmsg();
					else
						pSocket->closesock();
				}
				break;

			case IOCP_CLOSE:
				{
					pSocket->closesock();
				}
				break;
			}
		}
	}

	//------------------------------------------------------------------------------------
	// 网络发送线程
	//------------------------------------------------------------------------------------
	void CIocpPoller::ThreadSendProceEX()
	{
		ClientSocket* pClient = NULL;
		while (true)
		{
			for (INT32 i = 0; i < 4096; i++)
			{
				pClient = &m_pClient[i];
				if (!pClient->IsConnected() || pClient->IsShutDown() )
					continue;

				if (pClient->m_SendQueue.GetNum() <= 0)
					continue;

				pClient->sendmsg();
			}

			Sleep(10);
		}

		_endthreadex(0);
	}


	//------------------------------------------------------------------------------------
	// 接受连接处理接口
	//------------------------------------------------------------------------------------
	void CIocpPoller::OnAccept( SPerIoData* pIoInfo )
	{

		SOCKET sock		= pIoInfo->hSock;
		SOCKET hNewSock = WSASocket(AF_INET, SOCK_STREAM, 0, NULL, 0, WSA_FLAG_OVERLAPPED);
		if (INVALID_SOCKET == hNewSock)
		{
			return;
		}

		memset(&pIoInfo->stOverlapped, 0, sizeof(pIoInfo->stOverlapped));
		pIoInfo->hSock	= hNewSock;
		pIoInfo->nOp	= IOCP_ACCEPT;

		DWORD dwBytes;
		int bRet = m_lpfnAcceptEx(  m_sockListen, hNewSock,pIoInfo->Buf,0,
									sizeof(SOCKADDR_IN)+16,
									sizeof(SOCKADDR_IN)+16,
									&dwBytes,
									&pIoInfo->stOverlapped);

		if (S_OK != bRet)
		{
			if (WSA_IO_PENDING != ::WSAGetLastError())
			{
				closesocket(hNewSock);
				return;
			}
		}

		ClientSocket* pClientSocket = m_listFreeClient.PopFront();
		if (pClientSocket == NULL)
			return;

		if (0 != ::setsockopt(sock, SOL_SOCKET, SO_UPDATE_ACCEPT_CONTEXT, (char *)&m_sockListen, sizeof(SOCKET)))
		{
			int err = ::WSAGetLastError();
			ERROR_MSG("setsockopt for new socket on UpdateConetext failed,");
		}

		const CHAR szOpt = 1;
		if (0 != ::setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, (char *)&szOpt, sizeof(char)))
		{
			int err = ::WSAGetLastError();
			ERROR_MSG("setsockopt for new socket on UpdateConetext failed, errno");
		}

		
		// 创建session
		pClientSocket->setsocket(sock);
		if (m_poSessionFactory != NULL)
		{
			INetSession* pSession = m_poSessionFactory->CreateSession(ST_None);
			pSession->SetSocketObj(pClientSocket);

			UINT32 localaddr;
			UINT16 localport;
			pClientSocket->SetConnected(true);
			NetUnity::getremoteaddress(sock, &localport, &localaddr);
			pSession->SetRemotePort(localport);
			pSession->SeRemoteIP(localaddr);
		}

		// 綁定完成端口
		if (!pClientSocket->AssociateWithIOCP())
		{
			pClientSocket->closesock();
		}

		pClientSocket->PostRecv();
	}


	/**
	* @brief 将某个套接字接口绑定在此IOCP上
	* @param hSock : 绑定的套接字
	* @param pstData : 和该套接字相关的数据
	* @return 是否绑定成功
	*/
	bool  CIocpPoller::AssociateWithIocp(SOCKET hSock, SPerHandleData* pstData)
	{
		if (NULL == m_hCompletionPort)
			return false;

		if (NULL == CreateIoCompletionPort((HANDLE)hSock, m_hCompletionPort, (ULONG_PTR)pstData, 0))
		{
			ERROR_MSG(fmt::format("[CreateIoCompletionPort msg is error]\n"));
			return false;
		}
		return true;
	}
}

#endif