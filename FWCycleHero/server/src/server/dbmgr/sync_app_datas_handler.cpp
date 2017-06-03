/*
This source file is part of KBEngine
For the latest info, see http://www.kbengine.org/

Copyright (c) 2008-2012 KBEngine.

KBEngine is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

KBEngine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.
 
You should have received a copy of the GNU Lesser General Public License
along with KBEngine.  If not, see <http://www.gnu.org/licenses/>.
*/

#include "dbmgr.hpp"
#include "sync_app_datas_handler.hpp"
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
