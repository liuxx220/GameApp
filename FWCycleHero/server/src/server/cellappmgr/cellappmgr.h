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


#ifndef KBE_CELLAPPMGR_H
#define KBE_CELLAPPMGR_H
	
#include "cellapp.h"
#include "server/serverconfig.hpp"
#include "common/timer.hpp"






namespace KBEngine{


	class Cellappmgr : public Singleton<Cellappmgr>
	{
	public:
	
		 Cellappmgr();
		~Cellappmgr();
	
		bool						Initialize(COMPONENT_TYPE componentType);
		void						MainLoop(void);

		/* 初始化相关接口 */
		/* 初始化相关接口 */
		bool						initDB();
		bool						InitializeEnd();
		void						Destroy(void);
	protected:

		COMPONENT_TYPE				mComponentType;
		std::map< COMPONENT_ID, Cellapp >	cellapps_;
	};

}

#endif // KBE_CELLAPPMGR_H
