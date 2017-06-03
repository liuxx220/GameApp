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
	
// common include	
#include "buffered_dbtasks.hpp"
#include "server/serverconfig.hpp"
#include "server/serverconfig.hpp"
#include "common/timer.hpp"
#include "server/glw_resmgr.hpp"
#include "thread/threadpool.hpp"

//#define NDEBUG
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#endif
	
namespace KBEngine
{

	class DBInterface;
	class BillingHandler;
	class SyncAppDatasHandler;

	class AppDBServer : public Singleton<AppDBServer>
	{

	public:
	
		 AppDBServer();
		~AppDBServer();
	
		
		bool						Initialize(COMPONENT_TYPE componentType);
		void						MainLoop(void);

		/* 初始化相关接口 */
		/* 初始化相关接口 */
		bool						initDB();
		bool						InitializeEnd();
		void						Destroy(void);

	protected:
		
		COMPONENT_TYPE				mComponentType;		
		
	};

}

#endif // KBE_DBMGR_HPP
