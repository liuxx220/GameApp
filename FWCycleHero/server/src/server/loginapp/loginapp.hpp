/*
----------------------------------------------------------------------------
		file name : loginapp.hpp
		desc	  : 登录服务器
		author    : LJP

		log		  : [ 2015-04-25 ]
----------------------------------------------------------------------------
*/
#pragma once	
#include "server/serverconfig.hpp"
#include "network/event_dispatcher.hpp"
#include "network/event_poller.hpp"
#include "db_session.h"
#include "LoginSessionMgr.h"

	



namespace KBEngine
{

	class Loginapp : public Singleton<Loginapp>
	{

	public:
	
		 Loginapp();
		~Loginapp();
	

		bool				Initialize( COMPONENT_TYPE componentType );
		void				MainLoop( void );
	
		
		/* 初始化相关接口 */
		bool				InitializeEnd();
		void				Destroy(void);


		void				handleTimeout(TimerHandle handle, void * arg);

		
	protected:

		COMPONENT_TYPE				mComponentType;

		CDBSession*					m_pDB;					// 链接DB的session
		CLoginSessionMgr*			m_pNetSessionMgr;
		EventDispatcher*			m_pDispatcher;			// 网络消息派送器
	};
		
}


