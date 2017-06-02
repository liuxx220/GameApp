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
#include "base.hpp"
#include "profile.hpp"
#include "network/channel.hpp"	


#ifndef CODE_INLINE
#include "base.ipp"
#endif
#include "../../server/dbmgr/dbmgr_interface.hpp"

namespace KBEngine{



	
//-------------------------------------------------------------------------------------
Base::Base(ENTITY_ID id, bool isInitialised):
hasDB_(false),
DBID_(0),
isGetingCellData_(false),
isArchiveing_(false),
shouldAutoArchive_(1),
shouldAutoBackup_(1),
creatingCell_(false),
createdSpace_(false),
inRestore_(false)
{

}

//-------------------------------------------------------------------------------------
Base::~Base()
{
	
}	


//-------------------------------------------------------------------------------------
void Base::onDestroy(bool callScript)																					
{
	if(callScript)
	{
		SCOPED_PROFILE(SCRIPTCALL_PROFILE);
		
	}

	
	eraseEntityLog();
}

//-------------------------------------------------------------------------------------
void Base::eraseEntityLog()
{
	
}




//-------------------------------------------------------------------------------------
void Base::sendToCellapp(Bundle* pBundle)
{
	KBE_ASSERT(cellMailbox_ != NULL);

	

	//sendToCellapp(cellMailbox_->getChannel(), pBundle);
}

//-------------------------------------------------------------------------------------
void Base::sendToCellapp(Channel* pChannel, Bundle* pBundle)
{
	KBE_ASSERT(pChannel != NULL && pBundle != NULL);
	
}

//-------------------------------------------------------------------------------------
void Base::destroyCellData(void)
{
	
}








//-------------------------------------------------------------------------------------
void Base::onCellAppDeath()
{
	isArchiveing_ = false;
	isGetingCellData_ = false;
}



//-------------------------------------------------------------------------------------
void Base::forwardEntityMessageToCellappFromClient(Channel* pChannel, MemoryStream& s)
{
	
}



//-------------------------------------------------------------------------------------
}
