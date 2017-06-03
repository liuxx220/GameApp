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


#ifndef KBE_BASEAPP_HPP
#define KBE_BASEAPP_HPP
	
// common include	
#include "proxy.hpp"
#include "profile.hpp"


//#define NDEBUG
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#endif
	
namespace KBEngine{


class Channel;
class Proxy;
class Backuper;
class Archiver;
class TelnetServer;


class Baseapp : public Singleton<Baseapp>
{
public:
	
	 Baseapp();
	~Baseapp();
	
	bool						Initialize(COMPONENT_TYPE componentType);
	void						MainLoop(void);

	/* 初始化相关接口 */
	/* 初始化相关接口 */
	bool						InitializeEnd();
	void						Destroy(void);
protected:
	COMPONENT_TYPE				mComponentType;
	// 备份存档相关
	SHARED_PTR< Backuper >		pBackuper_;	
	SHARED_PTR< Archiver >		pArchiver_;	

	float													load_;

	static uint64											_g_lastTimestamp;

	int32													numProxices_;

	TelnetServer*											pTelnetServer_;
};

}

#endif // KBE_BASEAPP_HPP
