/*
--------------------------------------------------------------------------------------
		file name : handle_listener.hpp
		完成端口监听器,在本机为服务器端时,此类是使用IOCP真实接受建立连接的类
		此类与CConnectCtrl对应
		author    : LJP

		log		  : [ 2015-04-28 ]
--------------------------------------------------------------------------------------
*/

#ifndef KBE_NETWORKLISTENER_RECEIVER_HPP
#define KBE_NETWORKLISTENER_RECEIVER_HPP
#include "common/common.hpp"
#include "network/IListener.hpp"








namespace KBEngine 
{ 
		/**
		* @brief 完成端口监听器,在本机为服务器端时,此类是使用IOCP真实接受建立连接的类
		* 此类与CConnectCtrl对应
		*/
		class CListenerReceiver : public IListener
		{
		public:
			CListenerReceiver();
			virtual ~CListenerReceiver();

			void						CreateAcceptThread(void);
			
			// 处理网络连接线程
			#if KBE_PLATFORM == PLATFORM_WIN32
			static unsigned __stdcall	ListenAcceptThread(void* arg);
			#else
			static void*				ListenAcceptThread( void* arg );
			#endif
	
			/**
			* @brief 设置完成端口监听器ID
			* @param dwID : 设置的ID
			* @return void
			*/
			inline void					SetID(UINT32 dwID)			{ m_dwID = dwID; }

			/**
			* @brief 设置数据包解析器
			* @param poPacketParser : 设置的数据包解析器
			* @return void
			*/
			inline void					SetPacketParser(IPacketParser* poPacketParser)			{ m_poPacketParser = poPacketParser; }

			/**
			* @brief 设置会话工厂类
			* @param poSessionFactory : 设置的会话工厂类
			* @return void
			*/
			inline void					SetSessionFactory(ISessionFactory* poSessionFactory)	{ m_poSessionFactory = poSessionFactory; }


			/**
			* @brief 设置缓存大小
			* @param dwRecvBufSize : 设置接收缓存大小
			* @param dwSendBufSize : 设置发送缓存大小
			* @return void
			*/
			inline void					SetBufferSize(UINT32 dwRecvBufSize, UINT32 dwSendBufSize) { m_dwRecvBufSize = dwRecvBufSize; m_dwSendBufSize = dwSendBufSize; }


			/**
			* @brief 开始监听
			* @param pszIP : 本地IP地址
			* @param wPort : 本地端口号
			* @param bReUseAddr : 是否端口复用
			* @return 监听成功返回true,监听失败返回false
			*/
			virtual bool				Start(const char* pszIP, UINT16 wPort, bool bReUseAddr);

			/**
			* @brief 停止监听
			* @return void
			*/
			virtual bool				Stop(void);

			/**
			* @brief 释放此监听器
			* @return void
			*/
			virtual void				Release(void) { delete this; }

			/**
			* @brief 当客户端发送一个建立连接请求时，执行此函数
			* @param bSucc : 此连接请求是否成功，true为成功
			* @param pstPerIoData : 此建立连接请求的每IO数据
			* @return void
			*/
			void						OnAccept(BOOL bSucc, SPerIoData* pClient, tagXClient* pTagClient );

		private:
			#if KBE_PLATFORM == PLATFORM_WIN32
			bool						_InitAccepteEx();
			/**
			* @brief 传输一个建立连接的每IO数据
			* @param pstPerIoData : 此建立连接请求的每IO数据
			* @return 是否建立连接成功，true为成功
			*/
			bool						PostAcceptEx(SPerIoData* pstPerIoData);
			#else

			#endif

			inline	void				ShutDownSocket(KBESOCKET sock);
		protected:

			UINT32						m_dwID;
			KBESOCKET					m_ListenerSocket;
			IPacketParser*				m_poPacketParser;
			ISessionFactory*			m_poSessionFactory;
			UINT32						m_dwRecvBufSize;
			UINT32						m_dwSendBufSize;
			bool						m_bStart;
			SPerHandleData              m_stPerHandleData;
			#if KBE_PLATFORM == PLATFORM_WIN32
			HANDLE						m_Handle;
			LPFN_ACCEPTEX				m_lpfnAcceptEx;
			LPFN_GETACCEPTEXSOCKADDRS	m_lpfnGetAcceptExSockAddr;
			#endif
		};

		inline	void CListenerReceiver::ShutDownSocket(KBESOCKET sock)
		{						
			#if KBE_PLATFORM == PLATFORM_WIN32					
			::closesocket(sock);
			#else												
			::close(sock);
			#endif
		}
		
}


#endif // KBE_NETWORKLISTENER_RECEIVER_HPP
