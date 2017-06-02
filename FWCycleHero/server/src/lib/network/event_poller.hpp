/*
------------------------------------------------------------------------------------------------
file name : event_poller.hpp
desc	  : 网络 socket select epoll iocp 轮询模型
author    : LJP

log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/

#ifndef EVENT_POLLER_HPP
#define EVENT_POLLER_HPP

#include "common/common.hpp"
#include "common/timestamp.hpp"
#include "network/interfaces.hpp"
#include "thread/concurrency.hpp"
#include "network/common.hpp"
#include "network/ISessionFactory.hpp"
#include <map>





namespace KBEngine 
{ 
		class InputNotificationHandler;
		typedef std::map<int, InputNotificationHandler *> FDHandlers;
		class EventPoller
		{
		public:
			EventPoller();
			virtual ~EventPoller();

			void					clearSpareTime()		{ spareTime_ = 0;	}
			uint64					spareTime() const		{ return spareTime_;}

			static EventPoller*		create();

			/**
			* @brief 设置会话工厂类
			* @param poSessionFactory : 设置的会话工厂类
			* @return void
			*/
			virtual void			SetSessionFactory(ISessionFactory* poSessionFactory)	{ m_poSessionFactory = poSessionFactory; }

			/***
			* @brief 初始化 NetWork
			*/
			virtual bool			InitNetEngine(UINT32 nPort){ return true; }

			/***
			* @brief 得到某个tagXClient 上，已经接受到的网络数据
			*/
			virtual UINT8*			GetRecv(UINT32 dwHandle, UINT32& dwSize, INT32& nRecvNum) { return NULL;  }


			virtual void			Send(UINT32 dwHandle, void* pMsg, UINT32 dwSize){}

		protected:
			
			
			int						maxFD() const;

		private:
			static int				maxFD(const FDHandlers & handlerMap);
			FDHandlers				fdReadHandlers_;
			FDHandlers				fdWriteHandlers_;

		protected:
			uint64					spareTime_;
			ISessionFactory*		m_poSessionFactory;
		};
}
#endif // EVENT_POLLER_HPP
