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

#ifndef KBE_CELLAPP_H
#define KBE_CELLAPP_H
	
// common include	
#include "spaces.h"
#include "cells.h"
#include "server/serverapp.hpp"

	
namespace KBEngine
{


class Cellapp : public ServerApp,
				public Singleton<Cellapp>
{
public:
	enum TimeOutType
	{
		TIMEOUT_LOADING_TICK = TIMEOUT_SERVERAPP_MAX + 1
	};
	
	Cellapp(EventDispatcher& dispatcher, NetSession& ninterface,
				COMPONENT_TYPE componentType,COMPONENT_ID componentID);

	~Cellapp();
	
	bool run();
	
	
	/**  
		相关处理接口 
	*/
	virtual void		handleTimeout(TimerHandle handle, void * arg);
	virtual void		handleGameTick();

	/**  
		初始化相关接口 
	*/
	bool				initializeBegin();
	bool				initializeEnd();
	void				finalise();

	virtual bool		canShutdown();
	virtual void		onShutdown(bool first);

	virtual void		onUpdateLoad();

	
protected:
	
	
	// 所有的cell
	Cells								cells_;
	// APP的标志
	uint32								flags_;
};

}

#endif // KBE_CELLAPP_H
