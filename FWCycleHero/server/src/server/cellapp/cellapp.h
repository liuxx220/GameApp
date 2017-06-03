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


	
namespace KBEngine
{

	class CellServerApp : public Singleton<CellServerApp>
	{
	public:
	
		 CellServerApp();
		~CellServerApp();
	
		bool						Initialize(COMPONENT_TYPE componentType);
		void						MainLoop(void);

		/* 初始化相关接口 */
		/* 初始化相关接口 */
		bool						initDB();
		bool						InitializeEnd();
		void						Destroy(void);
	protected:

		// 所有的cell
		COMPONENT_TYPE				mComponentType;
		Cells						cells_;
		uint32						flags_;
	};

}

#endif // KBE_CELLAPP_H
