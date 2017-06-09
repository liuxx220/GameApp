/*
---------------------------------------------------------------------------------------------------------
file name : loginSession.hpp
desc      : 登录服务器到 DBapp 的链接
author    : ljp

log		  : 2017-06-09 : 15:55
---------------------------------------------------------------------------------------------------------
*/
#include "dbmgr.hpp"
#include "math/math.hpp"
#include "server/components.hpp"



namespace KBEngine{	

////-------------------------------------------------------------------------------------
//SyncAppDatasHandler::SyncAppDatasHandler(NetSession& SessionDB ):
//					 Task()
//					 ,m_SessionDB(SessionDB)
//					 ,lastRegAppTime_(0)
//					 ,apps_()
//{
//	m_SessionDB.dispatcher().addFrequentTask(this);
//}
//
////-------------------------------------------------------------------------------------
//SyncAppDatasHandler::~SyncAppDatasHandler()
//{
//	//m_SessionDB.mainDispatcher().cancelFrequentTask(this);
//	DEBUG_MSG("SyncAppDatasHandler::~SyncAppDatasHandler()\n");
//
//	AppDBServer::getSingleton().SetSyncAppDatasHandler(NULL);
//}
//
////-------------------------------------------------------------------------------------
//void SyncAppDatasHandler::pushApp(COMPONENT_ID cid, COMPONENT_ORDER startGroupOrder, COMPONENT_ORDER startGlobalOrder)
//{
//	lastRegAppTime_ = timestamp();
//	std::vector<ComponentInitInfo>::iterator iter = apps_.begin();
//	for(; iter != apps_.end(); iter++)
//	{
//		if((*iter).cid == cid)
//		{
//			ERROR_MSG(fmt::format("SyncAppDatasHandler::pushApp: cid({}) is exist!\n", cid));
//			return;
//		}
//	}
//
//	ComponentInitInfo cinfo;
//	cinfo.cid				= cid;
//	cinfo.startGroupOrder	= startGroupOrder;
//	cinfo.startGlobalOrder	= startGlobalOrder;
//	apps_.push_back(cinfo);
//}
//
////-------------------------------------------------------------------------------------
//bool SyncAppDatasHandler::process()
//{
//	if(lastRegAppTime_ == 0)
//		return true;
//
//	bool hasApp = false;
//	std::vector<ComponentInitInfo>::iterator iter = apps_.begin();
//	for(; iter != apps_.end(); iter++)
//	{
//		ComponentInitInfo cInitInfo = (*iter);
//		Components::ComponentInfos* cinfos = Components::getSingleton().findComponent(cInitInfo.cid);
//
//		if(cinfos == NULL)
//			continue;
//
//		COMPONENT_TYPE tcomponentType = cinfos->componentType;
//		if(tcomponentType == BASEAPP_TYPE || tcomponentType == CELLAPP_TYPE)
//		{
//			hasApp = true;
//			break;
//		}
//	}
//	
//	if(!hasApp)
//	{
//		lastRegAppTime_ = timestamp();
//		return true;
//	}
//
//	if(timestamp() - lastRegAppTime_ < uint64( 3 * stampsPerSecond() ) )
//		return true;
//
//	apps_.clear();
//
//	delete this;
//	return false;
//}
//
////-------------------------------------------------------------------------------------

}
