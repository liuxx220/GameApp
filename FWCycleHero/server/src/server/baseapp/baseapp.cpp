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
#include "server/components.hpp"
#include "math/math.hpp"



namespace KBEngine{

	KBE_SINGLETON_INIT(Baseapp);

	uint64 Baseapp::_g_lastTimestamp = timestamp();
	//-------------------------------------------------------------------------------------
	Baseapp::Baseapp( )
	{
	
	}

	//-------------------------------------------------------------------------------------
	Baseapp::~Baseapp()
	{

	}

	//--------------------------------------------------------------------------------------
	bool Baseapp::Initialize(COMPONENT_TYPE componentType)
	{
		// 注册网络需要处理的消息接口
		mComponentType = componentType;
		return true;
	}

	bool Baseapp::InitializeEnd()
	{
		return true;
	}

	//-------------------------------------------------------------------------------------
	void Baseapp::MainLoop(void)
	{
		while (true)
		{

			sleep(10);
		}
	}

	//-------------------------------------------------------------------------------------
	void Baseapp::Destroy()
	{


	}
}
