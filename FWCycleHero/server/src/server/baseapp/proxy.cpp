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

#include "baseapp.hpp"
#include "proxy.hpp"
#include "proxy_forwarder.hpp"
#include "profile.hpp"
#include "data_download.hpp"
#include "network/channel.hpp"
#include "../../server/dbmgr/dbmgr_interface.hpp"

#ifndef CODE_INLINE
#include "proxy.ipp"
#endif

namespace KBEngine{


	
//-------------------------------------------------------------------------------------
Proxy::Proxy(ENTITY_ID id ):
Base(id, true),
rndUUID_(KBEngine::genUUID64()),
addr_(Address::NONE),
dataDownloads_(),
entitiesEnabled_(false),
bandwidthPerSecond_(0),
encryptionKey(),
pProxyForwarder_(NULL),
clientComponentType_(UNKNOWN_CLIENT_COMPONENT_TYPE)
{
	Baseapp::getSingleton().incProxicesCount();

	pProxyForwarder_ = new ProxyForwarder(this);
}

//-------------------------------------------------------------------------------------
Proxy::~Proxy()
{
	Baseapp::getSingleton().decProxicesCount();

	

	SAFE_RELEASE(pProxyForwarder_);
}

//-------------------------------------------------------------------------------------
void Proxy::initClientBasePropertys()
{
	
}


//-------------------------------------------------------------------------------------
void Proxy::onEntitiesEnabled(void)
{
	SCOPED_PROFILE(SCRIPTCALL_PROFILE);
	entitiesEnabled_ = true;
	
}

//-------------------------------------------------------------------------------------
int32 Proxy::onLogOnAttempt(const char* addr, uint32 port, const char* password)
{
	SCOPED_PROFILE(SCRIPTCALL_PROFILE);

	
	
	int32 ret = LOG_ON_REJECT;
	
	return ret;
}

//-------------------------------------------------------------------------------------
void Proxy::onClientDeath(void)
{
	
}

//-------------------------------------------------------------------------------------
void Proxy::onClientGetCell(Channel* pChannel, COMPONENT_ID componentID)
{
	
}



//-------------------------------------------------------------------------------------
void Proxy::onGiveClientToFailure()
{
	SCOPED_PROFILE(SCRIPTCALL_PROFILE);

	
}

//-------------------------------------------------------------------------------------
void Proxy::giveClientTo(Proxy* proxy)
{
	
}

//-------------------------------------------------------------------------------------
void Proxy::onGiveClientTo(Channel* lpChannel)
{
	//clientMailbox(new EntityMailbox(this->scriptModule_, 
	//	&lpChannel->addr(), 0, id_, MAILBOX_TYPE_CLIENT));

	//addr(lpChannel->addr());
	//Baseapp::getSingleton().createClientProxies(this);

	//// 如果有cell, 需要通知其获得witness， 因为这个客户端刚刚绑定到这个proxy
	//// 此时这个entity即使有cell正常情况必须是没有witness的。
	//onGetWitness();
}

//-------------------------------------------------------------------------------------
void Proxy::onGetWitness()
{
	
}






//-------------------------------------------------------------------------------------
//bool Proxy::sendToClient(const MessageHandler& msgHandler, Bundle* pBundle)
//{
//	return sendToClient(pBundle);
//}

//-------------------------------------------------------------------------------------
bool Proxy::sendToClient(Bundle* pBundle)
{
	
	
	return false;
}

//-------------------------------------------------------------------------------------
bool Proxy::sendToClient(bool expectData)
{
	
	//Channel* pChannel = clientMailbox()->getChannel();
	//if(!pChannel)
	//	return false;

	//if(expectData)
	//{
	//	if(pChannel->bundles().size() == 0)
	//	{
	//		WARNING_MSG("Proxy::sendToClient: no data!\n");
	//		return false;
	//	}
	//}

	//{
	//	// 如果数据大量阻塞发不出去将会报警
	//	AUTO_SCOPED_PROFILE("sendToClient");
	//	pChannel->send();
	//}

	return true;
}

//-------------------------------------------------------------------------------------
void Proxy::onStreamComplete(int16 id, bool success)
{
	SCOPED_PROFILE(SCRIPTCALL_PROFILE);

	//SCRIPT_OBJECT_CALL_ARGS2(this, const_cast<char*>("onStreamComplete"), 
	//	const_cast<char*>("hO"), id, success ? Py_True : Py_False);
}

//-------------------------------------------------------------------------------------
}
