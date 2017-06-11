/*
------------------------------------------------------------------------------------------------------------------
file Name	:
desc		: db 服务器管理模块
author		:
log			:
------------------------------------------------------------------------------------------------------------------
*/
#ifndef KBE_DBMGR_HPP
#define KBE_DBMGR_HPP
#include "server/serverconfig.hpp"
#include "server/glw_resmgr.hpp"
#include "network/event_dispatcher.hpp"
#include "DBSessionMgr.h"





namespace KBEngine
{

	
	class AppDBServer : public Singleton<AppDBServer>
	{

	public:
	
		 AppDBServer();
		~AppDBServer();
	
		
		bool						Initialize(COMPONENT_TYPE componentType);
		void						InitNetWork( void );
		void						MainLoop(void);

		/* 初始化相关接口 */
		/* 初始化相关接口 */
		bool						initDB();
		bool						InitializeEnd();
		void						Destroy(void);

	protected:
		
		COMPONENT_TYPE				mComponentType;		
		CDBAppSessionMgr*			m_pDBAppSessionMgr;
		EventDispatcher*			m_pDispatcher;
	};

}

#endif // KBE_DBMGR_HPP
