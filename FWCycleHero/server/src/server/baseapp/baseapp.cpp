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
#include "base.hpp"

#include "archiver.hpp"
#include "backuper.hpp"
#include "common/timestamp.hpp"
#include "common/kbeversion.hpp"
#include "network/common.hpp"
#include "network/tcp_packet.hpp"
#include "server/components.hpp"
#include "math/math.hpp"
#include "../../server/dbmgr/dbmgr_interface.hpp"


namespace KBEngine{

KBE_SINGLETON_INIT(Baseapp);

uint64 Baseapp::_g_lastTimestamp = timestamp();
//-------------------------------------------------------------------------------------
Baseapp::Baseapp(EventDispatcher& dispatcher, 
					NetSession& ninterface, 
					COMPONENT_TYPE componentType,
					COMPONENT_ID componentID):
	loopCheckTimerHandle_(),
	pBackuper_(),
	load_(0.f),
	numProxices_(0),
	pTelnetServer_(NULL),
	pResmgrTimerHandle_()
{
	
}

//-------------------------------------------------------------------------------------
Baseapp::~Baseapp()
{

}




//-------------------------------------------------------------------------------------	
bool Baseapp::canShutdown()
{
	Components::COMPONENTS& cellapp_components = Components::getSingleton().getComponents(CELLAPP_TYPE);
	if(cellapp_components.size() > 0)
	{
		std::string s;
		for(size_t i=0; i<cellapp_components.size(); i++)
		{
			s += fmt::format("{}, ", cellapp_components[i].cid);
		}

		INFO_MSG(fmt::format("Baseapp::canShutdown(): Waiting for cellapp[{}] destruction!\n", 
			s));

		return false;
	}

	
	return true;
}

//-------------------------------------------------------------------------------------	
void Baseapp::onShutdownBegin()
{
	
}

//-------------------------------------------------------------------------------------	
void Baseapp::onShutdown(bool first)
{
	
}

//-------------------------------------------------------------------------------------	
void Baseapp::onShutdownEnd()
{
	
}



//-------------------------------------------------------------------------------------
bool Baseapp::initialize()
{
	return true;
}

//-------------------------------------------------------------------------------------
bool Baseapp::initializeBegin()
{
	return true;
}

//-------------------------------------------------------------------------------------
bool Baseapp::initializeEnd()
{	
	return true;
}

//-------------------------------------------------------------------------------------
void Baseapp::finalise()
{

}

//-------------------------------------------------------------------------------------
int Baseapp::quantumPassedPercent(uint64 curr)
{
	//// 得到上一个tick到现在所流逝的时间
	//uint64 pass_stamps = (curr - _g_lastTimestamp) * uint64(1000) / stampsPerSecond();

	//// 得到每Hertz的毫秒数
	//static int ms_expected = (1000 / g_kbeSrvConfig.gameUpdateHertz());

	//// 得到当前流逝的时间占一个时钟周期的的百分比
	//return int(pass_stamps) * 100 / ms_expected;
	return 0;
}

//-------------------------------------------------------------------------------------
uint64 Baseapp::checkTickPeriod()
{
	uint64 curr = timestamp();
	int percent = quantumPassedPercent(curr);

	if (percent > 200)
	{
		WARNING_MSG(fmt::format("Baseapp::handleGameTick: tick took {0}% ({1:.2f} seconds)!\n",
			percent, (float(percent)/1000.f)));
	}

	uint64 elapsed = curr - _g_lastTimestamp;
	_g_lastTimestamp = curr;
	return elapsed;
}

//-------------------------------------------------------------------------------------
bool Baseapp::run()
{
	return false;
}

//-------------------------------------------------------------------------------------
void Baseapp::handleTimeout(TimerHandle handle, void * arg)
{
	switch (reinterpret_cast<uintptr>(arg))
	{
		case TIMEOUT_CHECK_STATUS:
			this->handleCheckStatusTick();
			return;
		default:
			break;
	}

	
}

//-------------------------------------------------------------------------------------
void Baseapp::handleCheckStatusTick()
{
	//pendingLoginMgr_.process();
}

//-------------------------------------------------------------------------------------
void Baseapp::updateLoad()
{
	uint64 lastTickInStamps = checkTickPeriod();

	// 获得空闲时间比例
	double spareTime = 1.0;
	
	// 如果空闲时间比例小于0 或者大于1则表明计时不准确
	if ((spareTime < 0.f) || (1.f < spareTime))
	{
		if (g_timingMethod == RDTSC_TIMING_METHOD)
		{
			CRITICAL_MSG(fmt::format("Baseapp::handleGameTick: "
				"Invalid timing result {:.3f}.\n"
				"Please change the environment variable KBE_TIMING_METHOD to [rdtsc|gettimeofday|gettime](curr = {1})!",
				spareTime, getTimingMethodName()));
		}
		else
		{
			CRITICAL_MSG(fmt::format("Baseapp::handleGameTick: Invalid timing result {:.3f}.\n",
				spareTime));
		}
	}

	// 负载的值为1.0 - 空闲时间比例, 必须在0-1.f之间
	float load = KBEClamp(1.f - float(spareTime), 0.f, 1.f);

	
}

//-------------------------------------------------------------------------------------
void Baseapp::handleGameTick()
{
	// 一定要在最前面
	updateLoad();

	
	handleBackup();
	handleArchive();
}

//-------------------------------------------------------------------------------------
void Baseapp::handleBackup()
{
	pBackuper_->tick();
}

//-------------------------------------------------------------------------------------
void Baseapp::handleArchive()
{
	pArchiver_->tick();
}




//-------------------------------------------------------------------------------------
void Baseapp::onCellAppDeath(Channel * pChannel)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onRequestRestoreCB(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}


//-------------------------------------------------------------------------------------
void Baseapp::onRestoreSpaceCellFromOtherBaseapp(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onChannelDeregister(Channel * pChannel)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onGetEntityAppFromDbmgr(Channel* pChannel, int32 uid, std::string& username, 
						COMPONENT_TYPE componentType, COMPONENT_ID componentID, COMPONENT_ORDER globalorderID, COMPONENT_ORDER grouporderID,
						uint32 intaddr, uint16 intport, uint32 extaddr, uint16 extport, std::string& extaddrEx)
{
	
}





//-------------------------------------------------------------------------------------
void Baseapp::onCreateBaseFromDBIDCallback(Channel* pChannel, KBEngine::MemoryStream& s)
{
	std::string entityType;
	DBID dbid;
	CALLBACK_ID callbackID;
	bool success = false;
	bool wasActive = false;
	ENTITY_ID entityID;
	COMPONENT_ID wasActiveCID;
	ENTITY_ID wasActiveEntityID;

	s >> entityType;
	s >> dbid;
	s >> callbackID;
	s >> success;
	s >> entityID;
	s >> wasActive;

	if(wasActive)
	{
		s >> wasActiveCID;
		s >> wasActiveEntityID;
	}

	
	
}




//-------------------------------------------------------------------------------------
void Baseapp::onCreateBaseAnywhereFromDBIDCallback(Channel* pChannel, KBEngine::MemoryStream& s)
{
	size_t currpos = s.rpos();

	std::string entityType;
	DBID dbid;
	CALLBACK_ID callbackID;
	bool success = false;
	bool wasActive = false;
	ENTITY_ID entityID;
	COMPONENT_ID wasActiveCID;
	ENTITY_ID wasActiveEntityID;

	s >> entityType;
	s >> dbid;
	s >> callbackID;
	s >> success;
	s >> entityID;
	s >> wasActive;

	if(wasActive)
	{
		s >> wasActiveCID;
		s >> wasActiveEntityID;
	}

	
}

//-------------------------------------------------------------------------------------
void Baseapp::createBaseAnywhereFromDBIDOtherBaseapp(Channel* pChannel, KBEngine::MemoryStream& s)
{
	std::string entityType;
	DBID dbid;
	CALLBACK_ID callbackID;
	bool success = false;
	bool wasActive = false;
	ENTITY_ID entityID;
	COMPONENT_ID sourceBaseappID;

	s >> sourceBaseappID;
	s >> entityType;
	s >> dbid;
	s >> callbackID;
	s >> success;
	s >> entityID;
	s >> wasActive;

	
}

//-------------------------------------------------------------------------------------
void Baseapp::onCreateBaseAnywhereFromDBIDOtherBaseappCallback(Channel* pChannel, COMPONENT_ID createByBaseappID,
	std::string entityType, ENTITY_ID createdEntityID, CALLBACK_ID callbackID, DBID dbid)
{




}





//-------------------------------------------------------------------------------------
void Baseapp::onQueryAccountCBFromDbmgr(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::forwardMessageToClientFromCellapp(Channel* pChannel, 
												KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::forwardMessageToCellappFromCellapp(Channel* pChannel, 
												KBEngine::MemoryStream& s)
{
	
}


//-------------------------------------------------------------------------------------
void Baseapp::onEntityMail(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onRemoteCallCellMethodFromClient(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onUpdateDataFromClient(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onBackupEntityCellData(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onCellWriteToDBCompleted(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onClientActiveTick(Channel* pChannel)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onWriteToDBCallback(Channel* pChannel, ENTITY_ID eid, 
								  DBID entityDBID, CALLBACK_ID callbackID, bool success)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::onHello(Channel* pChannel, 
						const std::string& verInfo, 
						const std::string& scriptVerInfo,
						const std::string& encryptedKey)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::lookApp(Channel* pChannel)
{
	
}

//-------------------------------------------------------------------------------------
void Baseapp::importClientMessages(Channel* pChannel)
{
	
}




//-------------------------------------------------------------------------------------
void Baseapp::deleteBaseByDBIDCB(Channel* pChannel, KBEngine::MemoryStream& s)
{
	
	
}



//-------------------------------------------------------------------------------------

}
