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
#include "server/components.hpp"
#include "navigation/navigation.hpp"


namespace KBEngine{
	

	KBE_SINGLETON_INIT(CellServerApp);
	//Navigation g_navigation;

	//-------------------------------------------------------------------------------------
	CellServerApp::CellServerApp()
	{
	
	}

	//-------------------------------------------------------------------------------------
	CellServerApp::~CellServerApp()
	{
		//EntityMailbox::resetCallHooks();
	}


	//--------------------------------------------------------------------------------------
	bool CellServerApp::Initialize(COMPONENT_TYPE componentType)
	{
		// 注册网络需要处理的消息接口
		mComponentType = componentType;
		return true;
	}

	bool CellServerApp::InitializeEnd()
	{
		return true;
	}

	//-------------------------------------------------------------------------------------
	void CellServerApp::MainLoop(void)
	{
		while (true)
		{

			sleep(10);
		}
	}

	//-------------------------------------------------------------------------------------
	void CellServerApp::Destroy()
	{


	}
}
