/*
------------------------------------------------------------------------------------------------------------------
file Name	:
desc		: db 服务器管理模块
author		:
log			:
------------------------------------------------------------------------------------------------------------------
*/
#include "dbmgr.hpp"
#include "dbtasks.hpp"
#include "billingmgr.hpp"
#include "sync_app_datas_handler.hpp"
#include "network/common.hpp"
#include "network/message_handler.hpp"
#include "thread/threadpool.hpp"
#include "server/components.hpp"
#include "db_interface/db_interface.h"



namespace KBEngine{
	
	ServerConfig g_serverConfig;
	KBE_SINGLETON_INIT(AppDBServer);

	//-------------------------------------------------------------------------------------
	AppDBServer::AppDBServer()
	{

	}

	//-------------------------------------------------------------------------------------
	AppDBServer::~AppDBServer()
	{

	}

	//--------------------------------------------------------------------------------------
	bool AppDBServer::Initialize(COMPONENT_TYPE componentType )
	{
		// 注册网络需要处理的消息接口
		mComponentType = componentType;
		return true;
	}

	bool AppDBServer::InitializeEnd()
	{
		return initDB();
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::MainLoop( void )
	{
		while (true)
		{
			
			sleep(10);
		}
	}


	
	//-------------------------------------------------------------------------------------		
	bool AppDBServer::initDB()
	{
		
		if(!DBUtil::initialize())
		{
			ERROR_MSG("Dbmgr::initDB(): can't initialize dbinterface!\n");
			return false;
		}

		DBInterface* pDBInterface = DBUtil::createInterface();
		if(pDBInterface == NULL)
		{
			ERROR_MSG("Dbmgr::initDB(): can't create dbinterface!\n");
			return false;
		}

		bool ret = DBUtil::initInterface(pDBInterface);
		pDBInterface->detach();
		SAFE_RELEASE(pDBInterface);

		if(!ret)
			return false;

		return true;
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::Destroy()
	{
	
		
	}
}
