/*
------------------------------------------------------------------------------------------------
file name : event_iocp.hpp
desc	  : 网络 windows 下 完成端口网络模型
author    : LJP

log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/

#ifndef __EVENT_NET_IOCP_H__
#define __EVENT_NET_IOCP_H__
#include "common/common.hpp"
#include "network/event_poller.hpp"
#include "common/timestamp.hpp"
#include "network/interfaces.hpp"
#include "thread/concurrency.hpp"
#include "network/common.hpp"
#include "network/ISocket.hpp"
#include "network/x_list.h"
#include <map>
#include <list>




#if KBE_PLATFORM == PLATFORM_WIN32
namespace KBEngine 
{ 

#ifndef SAFE_CLOSE_HANDLE
#define SAFE_CLOSE_HANDLE(h)	do { if(h) { CloseHandle(h);	(h) = NULL; } } while(0)
#endif


#define	X_SLEEP_TIME 500


	//-------------------------------------------------------------------------------------
	class ISessionFactory;
	class IPacketParser;
	class IListener;
	class CIocpPoller : public EventPoller
	{

	public:
		CIocpPoller();
		virtual ~CIocpPoller();

		static CIocpPoller*			Instance();

	protected:

		/** brif 接受客户端连接线程
		*/
		static UINT32 WINAPI		ListenAcceptThread(void* arg);
		static UINT32 WINAPI		ThreadSendProc(void* arg);
		
	public:

		/***
		* @brief 初始化 NetWork
		*/
		virtual bool				InitNetEngine(UINT32 nPort);

		/**
		* @brief 初始化IOCP控制类
		* @return 是否初始化成功，true为成功，false为失败
		*/
		bool						CreateIoCompletion( UINT16 nPort );

		/**
		* @brief 清理IOCP控制类
		*
		*/
		void						Destroy();

		/**
		* @brief 清理IOCP控制类
		*
		*/
		void						OnAccept(SPerIoData* pIoInfo);


		/**
		* @brief 当IOCP执行时，调用此函数
		*
		*/
		void						ThreadSetupAcceptEx( );
		void						ThreadSendProceEX();
		
		/**
		* @brief 将某个套接字接口绑定在此IOCP上
		* @param hSock : 绑定的套接字
		* @param pstData : 和该套接字相关的数据
		* @return 是否绑定成功
		*/
		bool						AssociateWithIocp(KBESOCKET hSock, SPerHandleData* pstData);

	protected:
		static CIocpPoller*			g_IocpInstance;
		//IO完成端口的句柄
		HANDLE						m_hCompletionPort;
		
		KBESOCKET					m_sockListen;
		INT32						m_nNumberOfWorkers;
		HANDLE*						m_WorkerArray;

		//-----------------------------------------------------------------------------
		ClientSocket*				m_pClient;
		XList<ClientSocket*>		m_listFreeClient;
		XList<ClientSocket*>		m_listDestroyClient;

		SPerHandleData              m_stPerHandleData;
		SPerIoData*					m_pPerIoDataArray;
		LPFN_ACCEPTEX				m_lpfnAcceptEx;
		LPFN_GETACCEPTEXSOCKADDRS	m_lpfnGetAcceptExSockAddr;
	};
}
#endif

#endif // __EVENT_NET_IOCP_H__
