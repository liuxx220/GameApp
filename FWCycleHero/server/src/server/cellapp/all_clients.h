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


#ifndef KBE_ALL_CLIENTS_H
#define KBE_ALL_CLIENTS_H
	
// common include	
#include "common/common.h"
#include "network/address.h"

//#define NDEBUG
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <iostream>	
#include <map>	
#include <vector>	
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#include <time.h> 
#else
// linux include
#include <errno.h>
#endif
	
namespace KBEngine{


class Channel;
class Bundle;
class ScriptDefModule;

class AllClients
{
	
public:
	AllClients(bool otherClients);
	
	~AllClients();
	
	
	void c_str(char* s, size_t size);
	
	

protected:
	
	bool									otherClients_;			// 是否只是其他客户端， 不包括自己
};

}

#endif // KBE_ALL_CLIENTS_H
