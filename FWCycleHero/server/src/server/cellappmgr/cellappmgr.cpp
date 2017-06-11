/*
---------------------------------------------------------------------------------------------
		file name :
		desc	  : 管理场景服务器对象
		author    : ljp

		log       : by ljp create 2017-06-11
---------------------------------------------------------------------------------------------
*/


#include "cellappmgr.h"
#include "network/common.hpp"
#include "server/glw_resmgr.hpp"
#include "network/message_handler.hpp"
#include "network/event_poller.hpp"
#include "server/components.hpp"



namespace KBEngine{
	
	ServerConfig g_serverConfig;
	KBE_SINGLETON_INIT(Cellappmgr);

	//-------------------------------------------------------------------------------------
	Cellappmgr::Cellappmgr() : m_pCellSessionMgr(0), m_pDispatcher(0)
	{

	}

	//-------------------------------------------------------------------------------------
	Cellappmgr::~Cellappmgr()
	{
		cellapps_.clear();
	}

	//--------------------------------------------------------------------------------------
	bool Cellappmgr::Initialize(COMPONENT_TYPE componentType)
	{
		// 注册网络需要处理的消息接口
		mComponentType = componentType;
		DebugHelper::initialize(componentType);

		// 资源初始化
		new Resmgr();
		Resmgr::getSingleton().initialize();

		INFO_MSG("-----------------------------------------------------------------------------------------\n");
		g_kbeSrvConfig.loadConfig("config/kbengine_defs.xml");
		g_kbeSrvConfig.loadConfig("config/kbengine.xml");
		INFO_MSG("Load config files \n");

		m_pDispatcher = new EventDispatcher();
		InitializeEnd();
		return true;
	}

	bool Cellappmgr::InitializeEnd()
	{
		if (m_pCellSessionMgr == NULL)
		{
			m_pCellSessionMgr = new CCellAppSessionMgr();
		}

		// 创建对内对位监听socket
		ENGINE_COMPONENT_INFO& info = g_kbeSrvConfig.getLoginApp();
		if (m_pDispatcher->pPoller() != NULL)
		{
			m_pDispatcher->pPoller()->InitNetEngine(info.db_port);
			m_pDispatcher->pPoller()->SetSessionFactory(m_pCellSessionMgr);
		}
		return true;
	}

	//-------------------------------------------------------------------------------------
	void Cellappmgr::MainLoop(void)
	{
		while (true)
		{
			m_pCellSessionMgr->UpdateSession();
			sleep(10);
		}
	}

	//-------------------------------------------------------------------------------------
	void Cellappmgr::Destroy()
	{


	}
}
