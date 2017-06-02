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
#include "network/interface_defs.hpp"
#include "network/tcp_packet.hpp"
#include "network/message_handler.hpp"
#include "thread/threadpool.hpp"
#include "server/components.hpp"
#include "db_interface/db_interface.h"



namespace KBEngine{
	
	ServerConfig g_serverConfig;
	KBE_SINGLETON_INIT(AppDBServer);

	//-------------------------------------------------------------------------------------
	AppDBServer::AppDBServer(EventDispatcher& dispatcher,
					NetSession& ninterface, 
					COMPONENT_TYPE componentType,
					COMPONENT_ID componentID):
					ServerApp(dispatcher, ninterface, componentType, componentID),
					loopCheckTimerHandle_(),
					mainProcessTimer_(),
					bufferedDBTasks_(),
					numWrittenEntity_(0),
					numRemovedEntity_(0),
					numQueryEntity_(0),
					numExecuteRawDatabaseCommand_(0),
					numCreatedAccount_(0),
					pBillingAccountHandler_(NULL),
					pBillingChargeHandler_(NULL),
					pSyncAppDatasHandler_(NULL)
	{

	}

	//-------------------------------------------------------------------------------------
	AppDBServer::~AppDBServer()
	{
		loopCheckTimerHandle_.cancel();
		mainProcessTimer_.cancel();
		KBEngine::sleep(300);

		SAFE_RELEASE(pBillingAccountHandler_);
		SAFE_RELEASE(pBillingChargeHandler_);
	}

	//--------------------------------------------------------------------------------------
	bool AppDBServer::inInitialize()
	{

		// 注册网络需要处理的消息接口
		NETWORK_MESSAGE_HANDLER(1, AppDBServer::onReqAllocEntityID);
		return true;
	}

	bool AppDBServer::initializeEnd()
	{
		// 添加一个timer， 每秒检查一些状态
		loopCheckTimerHandle_ = this->mainDispatcher().addTimer(1000000, this,
																reinterpret_cast<void *>(TIMEOUT_CHECK_STATUS));

		// 向事件派送对象增加计时器，用来触发 DBMgr 的心跳逻辑
		// 及调用 handleTimeout 函数
		mainProcessTimer_ = this->mainDispatcher().addTimer(1000000 / 50, this,
															reinterpret_cast<void *>(TIMEOUT_TICK));
		return initDB();
	}

	//-------------------------------------------------------------------------------------
	bool AppDBServer::run( )
	{
		return ServerApp::run();
	}


	//-------------------------------------------------------------------------------------
	bool AppDBServer::canShutdown()
	{
		if( bufferedDBTasks_.size() > 0)
		{
			INFO_MSG( fmt::format("Wait for the task to complete, tasks={}, threads={}, threadpoolDestroyed={}!\n", 
						bufferedDBTasks_.size(), DBUtil::pThreadPool()->currentThreadCount(), DBUtil::pThreadPool()->isDestroyed()));

			return false;
		}

		Components::COMPONENTS& cellapp_components = Components::getSingleton().getComponents(CELLAPP_TYPE);
		if(cellapp_components.size() > 0)
		{
			std::string s;
			for(size_t i=0; i<cellapp_components.size(); i++)
			{
				s += fmt::format("{}, ", cellapp_components[i].cid);
			}

			INFO_MSG(fmt::format("Dbmgr::canShutdown(): Waiting for cellapp[{}] destruction!\n", 
				s));

			return false;
		}

		Components::COMPONENTS& baseapp_components = Components::getSingleton().getComponents(BASEAPP_TYPE);
		if(baseapp_components.size() > 0)
		{
			std::string s;
			for(size_t i=0; i<baseapp_components.size(); i++)
			{
				s += fmt::format("{}, ", baseapp_components[i].cid);
			}

			INFO_MSG(fmt::format("Dbmgr::canShutdown(): Waiting for baseapp[{}] destruction!\n", s));

			return false;
		}
		return true;
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::handleTimeout(TimerHandle handle, void * arg)
	{
		switch (reinterpret_cast<uintptr>(arg))
		{
			case TIMEOUT_TICK:
				this->handleMainTick();
				break;
			case TIMEOUT_CHECK_STATUS:
				this->handleCheckStatusTick();
				break;
			default:
				break;
		}

		ServerApp::handleTimeout(handle, arg);
	}

	//-------------------------------------------------------------------------------------
	// DB Server 主逻辑处理
	//-------------------------------------------------------------------------------------
	void AppDBServer::handleMainTick()
	{
		time_t t = ::time(NULL);
		threadPool_.onMainThreadTick();
		DBUtil::pThreadPool()->onMainThreadTick();
		GetSession().processChannels();
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::handleCheckStatusTick()
	{

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
	void AppDBServer::finalise()
	{
	
		ServerApp::finalise();
	}

	//-------------------------------------------------------------------------------------
	int AppDBServer::onReqAllocEntityID(int msgid, const MemoryStream* pNetCmd)
	{
	
		return 0;
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onRegisterNewApp(Channel* pChannel, int32 uid, std::string& username,
							COMPONENT_TYPE componentType, COMPONENT_ID componentID, COMPONENT_ORDER globalorderID, COMPONENT_ORDER grouporderID,
							uint32 intaddr, uint16 intport, uint32 extaddr, uint16 extport, std::string& extaddrEx)
	{
		//if(pChannel->isExternal())
		//	return;

		//ServerApp::onRegisterNewApp(pChannel, uid, username, componentType, componentID, globalorderID, grouporderID,
		//					intaddr, intport, extaddr, extport, extaddrEx);

		//KBEngine::COMPONENT_TYPE tcomponentType = (KBEngine::COMPONENT_TYPE)componentType;
		//
		//COMPONENT_ORDER startGroupOrder = 1;
		//COMPONENT_ORDER startGlobalOrder = Components::getSingleton().getGlobalOrderLog()[getUserUID()];

		//if(grouporderID > 0)
		//	startGroupOrder = grouporderID;

		//if(globalorderID > 0)
		//	startGlobalOrder = globalorderID;

		//if(pSyncAppDatasHandler_ == NULL)
		//	pSyncAppDatasHandler_ = new SyncAppDatasHandler(this->NetSession());

		//// 下一步:
		//// 如果是连接到dbmgr则需要等待接收app初始信息
		//// 例如：初始会分配entityID段以及这个app启动的顺序信息（是否第一个baseapp启动）
		//if(tcomponentType == BASEAPP_TYPE || 
		//	tcomponentType == CELLAPP_TYPE || 
		//	tcomponentType == LOGINAPP_TYPE)
		//{
		//	switch(tcomponentType)
		//	{
		//	case BASEAPP_TYPE:
		//		{
		//			if(grouporderID <= 0)
		//				startGroupOrder = Components::getSingleton().getBaseappGroupOrderLog()[getUserUID()];
		//		}
		//		break;
		//	case CELLAPP_TYPE:
		//		{
		//			if(grouporderID <= 0)
		//				startGroupOrder = Components::getSingleton().getCellappGroupOrderLog()[getUserUID()];
		//		}
		//		break;
		//	case LOGINAPP_TYPE:
		//		if(grouporderID <= 0)
		//			startGroupOrder = Components::getSingleton().getLoginappGroupOrderLog()[getUserUID()];

		//		break;
		//	default:
		//		break;
		//	}
		//}

		//pSyncAppDatasHandler_->pushApp(componentID, startGroupOrder, startGlobalOrder);

		//// 如果是baseapp或者cellapp则将自己注册到所有其他baseapp和cellapp
		//if(tcomponentType == BASEAPP_TYPE || 
		//	tcomponentType == CELLAPP_TYPE)
		//{
		//	KBEngine::COMPONENT_TYPE broadcastCpTypes[2] = {BASEAPP_TYPE, CELLAPP_TYPE};
		//	for(int idx = 0; idx < 2; idx++)
		//	{
		//		Components::COMPONENTS& cts = Components::getSingleton().getComponents(broadcastCpTypes[idx]);
		//		Components::COMPONENTS::iterator fiter = cts.begin();
		//		for(; fiter != cts.end(); fiter++)
		//		{
		//			if((*fiter).cid == componentID)
		//				continue;

		//			Bundle* pBundle = Bundle::ObjPool().createObject();
		//			ENTITTAPP_COMMON_NETWORK_MESSAGE(broadcastCpTypes[idx], (*pBundle), onGetEntityAppFromDbmgr);
		//			
		//			if(tcomponentType == BASEAPP_TYPE)
		//			{
		//				BaseappInterface::onGetEntityAppFromDbmgrArgs11::staticAddToBundle((*pBundle), 
		//					uid, username, componentType, componentID, startGlobalOrder, startGroupOrder,
		//						intaddr, intport, extaddr, extport, g_kbeSrvConfig.getConfig().externalAddress);
		//			}
		//			else
		//			{
		//				CellappInterface::onGetEntityAppFromDbmgrArgs11::staticAddToBundle((*pBundle), 
		//					uid, username, componentType, componentID, startGlobalOrder, startGroupOrder,
		//						intaddr, intport, extaddr, extport, g_kbeSrvConfig.getConfig().externalAddress);
		//			}
		//			
		//			KBE_ASSERT((*fiter).pChannel != NULL);
		//			(*pBundle).send(NetSession_, (*fiter).pChannel);
		//			Bundle::ObjPool().reclaimObject(pBundle);
		//		}
		//	}
		//}
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onGlobalDataClientLogon(Channel* pChannel, COMPONENT_TYPE componentType)
	{
		/*if(BASEAPP_TYPE == componentType)
		{
			pBaseAppData_->onGlobalDataClientLogon(pChannel, componentType);
			pGlobalData_->onGlobalDataClientLogon(pChannel, componentType);
		}
		else if(CELLAPP_TYPE == componentType)
		{
			pGlobalData_->onGlobalDataClientLogon(pChannel, componentType);
			pCellAppData_->onGlobalDataClientLogon(pChannel, componentType);
		}
		else
		{
			ERROR_MSG(fmt::format("Dbmgr::onGlobalDataClientLogon: nonsupport {}!\n",
				COMPONENT_NAME_EX(componentType)));
		}*/
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onBroadcastGlobalDataChanged(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		uint8 dataType;
		std::string key, value;
		bool isDelete;
		COMPONENT_TYPE componentType;
	
		s >> dataType;
		s >> isDelete;

		s.readBlob(key);

		if(!isDelete)
		{
			s.readBlob(value);
		}

		s >> componentType;

		/*switch(dataType)
		{
		case GlobalDataServer::GLOBAL_DATA:
			if(isDelete)
				pGlobalData_->del(pChannel, componentType, key);
			else
				pGlobalData_->write(pChannel, componentType, key, value);
			break;
		case GlobalDataServer::BASEAPP_DATA:
			if(isDelete)
				pBaseAppData_->del(pChannel, componentType, key);
			else
				pBaseAppData_->write(pChannel, componentType, key, value);
			break;
		case GlobalDataServer::CELLAPP_DATA:
			if(isDelete)
				pCellAppData_->del(pChannel, componentType, key);
			else
				pCellAppData_->write(pChannel, componentType, key, value);
			break;
		default:
			KBE_ASSERT(false && "dataType is error!\n");
			break;
		};*/
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onAccountLogin(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		std::string loginName, password, datas;
		s >> loginName >> password;
		s.readBlob(datas);

		if(loginName.size() == 0)
		{
			ERROR_MSG("Dbmgr::onAccountLogin: loginName is empty.\n");
			return;
		}

		pBillingAccountHandler_->loginAccount(pChannel, loginName, password, datas);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onLoginAccountCBBFromBilling(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		pBillingAccountHandler_->onLoginAccountCB(s);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::queryAccount(Channel* pChannel,
							 std::string& accountName, 
							 std::string& password,
							 COMPONENT_ID componentID,
							 ENTITY_ID entityID,
							 DBID entityDBID, 
							 uint32 ip, 
							 uint16 port)
	{
		/*if(accountName.size() == 0)
		{
			ERROR_MSG("Dbmgr::queryAccount: accountName is empty.\n");
			return;
		}

		bufferedDBTasks_.addTask(new DBTaskQueryAccount(pChannel->addr(), 
			accountName, password, componentID, entityID, entityDBID, ip, port));
	*/
		numQueryEntity_++;
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onAccountOnline(Channel* pChannel,
								std::string& accountName, 
								COMPONENT_ID componentID, 
								ENTITY_ID entityID)
	{
		// bufferedDBTasks_.addTask(new DBTaskAccountOnline(pChannel->addr(), 
		//	accountName, componentID, entityID));
	}


	//-------------------------------------------------------------------------------------
	void AppDBServer::writeEntity(Channel* pChannel,
							KBEngine::MemoryStream& s)
	{
		/*ENTITY_ID eid;
		DBID entityDBID;
		COMPONENT_ID componentID;

		s >> componentID >> eid >> entityDBID;

		bufferedDBTasks_.addTask(new DBTaskWriteEntity(pChannel->addr(), componentID, eid, entityDBID, s));
		s.done();
	*/
		numWrittenEntity_++;
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::removeEntity(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		/*ENTITY_ID eid;
		DBID entityDBID;
		COMPONENT_ID componentID;

		s >> componentID >> eid >> entityDBID;
		KBE_ASSERT(entityDBID > 0);

		bufferedDBTasks_.addTask(new DBTaskRemoveEntity(pChannel->addr(), 
			componentID, eid, entityDBID, s));

		s.done();*/

		numRemovedEntity_++;
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::deleteBaseByDBID(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		/*COMPONENT_ID componentID;
		ENTITY_SCRIPT_UID sid;
		CALLBACK_ID callbackID = 0;
		DBID entityDBID;

		s >> componentID >> entityDBID >> callbackID >> sid;
		KBE_ASSERT(entityDBID > 0);

		DBUtil::pThreadPool()->addTask(new DBTaskDeleteBaseByDBID(pChannel->addr(), 
			componentID, entityDBID, callbackID, sid));*/
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::lookUpBaseByDBID(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		/*COMPONENT_ID componentID;
		ENTITY_SCRIPT_UID sid;
		CALLBACK_ID callbackID = 0;
		DBID entityDBID;

		s >> componentID >> entityDBID >> callbackID >> sid;
		KBE_ASSERT(entityDBID > 0);

		DBUtil::pThreadPool()->addTask(new DBTaskLookUpBaseByDBID(pChannel->addr(), 
			componentID, entityDBID, callbackID, sid));*/
	}


	//-------------------------------------------------------------------------------------
	void AppDBServer::charge(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		pBillingChargeHandler_->charge(pChannel, s);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::onChargeCB(Channel* pChannel, KBEngine::MemoryStream& s)
	{
		pBillingChargeHandler_->onChargeCB(s);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::eraseClientReq(Channel* pChannel, std::string& logkey)
	{
		pBillingAccountHandler_->eraseClientReq(pChannel, logkey);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::accountActivate(Channel* pChannel, std::string& scode)
	{
		INFO_MSG(fmt::format("Dbmgr::accountActivate: code={}.\n", scode));
		pBillingAccountHandler_->accountActivate(pChannel, scode);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::accountReqResetPassword(Channel* pChannel, std::string& accountName)
	{
		INFO_MSG(fmt::format("Dbmgr::accountReqResetPassword: accountName={}.\n", accountName));
		pBillingAccountHandler_->accountReqResetPassword(pChannel, accountName);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::accountResetPassword(Channel* pChannel, std::string& accountName, std::string& newpassword, std::string& code)
	{
		INFO_MSG(fmt::format("Dbmgr::accountResetPassword: accountName={}.\n", accountName));
		pBillingAccountHandler_->accountResetPassword(pChannel, accountName, newpassword, code);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::accountReqBindMail(Channel* pChannel, ENTITY_ID entityID, std::string& accountName,
								   std::string& password, std::string& email)
	{
		INFO_MSG(fmt::format("Dbmgr::accountReqBindMail: accountName={}, email={}.\n", accountName, email));
		pBillingAccountHandler_->accountReqBindMail(pChannel, entityID, accountName, password, email);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::accountBindMail(Channel* pChannel, std::string& username, std::string& scode)
	{
		INFO_MSG(fmt::format("Dbmgr::accountBindMail: username={}, scode={}.\n", username, scode));
		pBillingAccountHandler_->accountBindMail(pChannel, username, scode);
	}

	//-------------------------------------------------------------------------------------
	void AppDBServer::accountNewPassword(Channel* pChannel, ENTITY_ID entityID, std::string& accountName,
								   std::string& password, std::string& newpassword)
	{
		INFO_MSG(fmt::format("Dbmgr::accountNewPassword: accountName={}.\n", accountName));
		pBillingAccountHandler_->accountNewPassword(pChannel, entityID, accountName, password, newpassword);
	}

	//-------------------------------------------------------------------------------------

}
