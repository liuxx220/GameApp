/*
This source file is part of KBEngine
For the latest info, see http://www.kbengine.org/

Copyright (c) 2008-2016 KBEngine.

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


#include "cellapp.h"
#include "space.h"
#include "profile.h"
#include "coordinate_node.h"
#include "cellapp_interface.h"
#include "network/tcp_packet.hpp"
#include "server/components.hpp"
#include "navigation/navigation.hpp"


namespace KBEngine{
	
ServerConfig g_serverConfig;
KBE_SINGLETON_INIT(Cellapp);

Navigation g_navigation;

//-------------------------------------------------------------------------------------
Cellapp::Cellapp(EventDispatcher& dispatcher, 
			 NetSession& ninterface, 
			 COMPONENT_TYPE componentType,
			 COMPONENT_ID componentID) : ServerApp(dispatcher, ninterface, componentType, componentID)
{
	
}

//-------------------------------------------------------------------------------------
Cellapp::~Cellapp()
{
	//EntityMailbox::resetCallHooks();
}

//-------------------------------------------------------------------------------------	
bool Cellapp::canShutdown()
{
	//Entities<Entity>::ENTITYS_MAP& entities =  this->pEntities()->getEntities();
	//Entities<Entity>::ENTITYS_MAP::iterator iter = entities.begin();
	//for(; iter != entities.end(); ++iter)
	//{
	//	//Entity* pEntity = static_cast<Entity*>(iter->second.get());
	//	//if(pEntity->baseMailbox() != NULL && 
	//	//		pEntity->pScriptModule()->isPersistent())
	//	{
	//		lastShutdownFailReason_ = "destroyHasBaseEntitys";
	//		return false;
	//	}
	//}

	return true;
}

//-------------------------------------------------------------------------------------	
void Cellapp::onShutdown(bool first)
{
	//EntityApp<Entity>::onShutdown(first);

	//uint32 count = g_serverConfig.getCellApp().perSecsDestroyEntitySize;
	//Entities<Entity>::ENTITYS_MAP& entities =  this->pEntities()->getEntities();

	//while(count > 0)
	//{
	//	std::vector<Entity*> vecs;

	//	bool done = false;
	//	Entities<Entity>::ENTITYS_MAP::iterator iter = entities.begin();
	//	for(; iter != entities.end(); ++iter)
	//	{
	//		//Entity* pEntity = static_cast<Entity*>(iter->second.get());
	//		//if(pEntity->baseMailbox() != NULL && 
	//		//	pEntity->pScriptModule()->isPersistent())
	//		{
	//			this->destroyEntity(static_cast<Entity*>(iter->second.get())->id(), true);

	//			count--;
	//			done = true;
	//			break;
	//		}
	//	}

	//	// 如果count等于perSecsDestroyEntitySize说明上面已经没有可处理的东西了
	//	// 剩下的应该都是space，可以开始销毁了
	//	Spaces::finalise();

	//	if(!done)
	//		break;
	//}
}


//-------------------------------------------------------------------------------------
bool Cellapp::run()
{
	return true;
}

//-------------------------------------------------------------------------------------
void Cellapp::handleTimeout(TimerHandle handle, void * arg)
{
	switch (reinterpret_cast<uintptr>(arg))
	{
		case TIMEOUT_LOADING_TICK:
			break;
		default:
			break;
	}
}

//-------------------------------------------------------------------------------------
void Cellapp::handleGameTick()
{
	AUTO_SCOPED_PROFILE("gameTick");

	// 一定要在最前面
	//updateLoad();

	Spaces::update();
}

//-------------------------------------------------------------------------------------
bool Cellapp::initializeBegin()
{
	return true;
}

//-------------------------------------------------------------------------------------
bool Cellapp::initializeEnd()
{
	
	/*bool ret = pTelnetServer_->start(g_kbeSrvConfig.getCellApp().telnet_passwd,
		g_kbeSrvConfig.getCellApp().telnet_deflayer,
		g_kbeSrvConfig.getCellApp().telnet_port);

		Components::getSingleton().extraData4(pTelnetServer_->port());*/
	return true;
}

//-------------------------------------------------------------------------------------
void Cellapp::finalise()
{
	/*SAFE_RELEASE(pGhostManager_);
	SAFE_RELEASE(pWitnessedTimeoutHandler_);

	if(pTelnetServer_)
	{
	pTelnetServer_->stop();
	SAFE_RELEASE(pTelnetServer_);
	}

	Spaces::finalise();
	Navigation::getSingleton().finalise();*/
}


//-------------------------------------------------------------------------------------
void Cellapp::onUpdateLoad()
{
	
}

}
