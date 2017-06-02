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
#include "data_download.hpp"
#include "data_downloads.hpp"


namespace KBEngine{	

//-------------------------------------------------------------------------------------
DataDownload::DataDownload( const std::string & descr, int16 id):
descr_(descr),
id_(id),
pDataDownloads_(NULL),
sentStart_(false),
totalBytes_(0),
totalSentBytes_(0),
remainSent_(0),
currSent_(0),
stream_(NULL),
entityID_(0),
error_(false)
{
}

//-------------------------------------------------------------------------------------
DataDownload::~DataDownload()
{
	SAFE_RELEASE_ARRAY(stream_);

	
}

//-------------------------------------------------------------------------------------
//bool DataDownload::send(const MessageHandler& msgHandler, Bundle* pBundle)
//{
//	Proxy* proxy = static_cast<Proxy*>(Baseapp::getSingleton().findEntity(entityID_));
//	
//	if(proxy){
//		proxy->sendToClient(msgHandler, pBundle);
//	}
//	else{
//		Bundle::ObjPool().reclaimObject(pBundle);
//		return false;
//	}
//
//	return true;
//}

//-------------------------------------------------------------------------------------
thread::TPTask::TPTaskState DataDownload::presentMainThread()
{
	
	return thread::TPTask::TPTASK_STATE_CONTINUE_MAINTHREAD; 
}



//-------------------------------------------------------------------------------------
StringDataDownload::~StringDataDownload()
{
}

//-------------------------------------------------------------------------------------
bool StringDataDownload::process()
{
	return false;
}

//-------------------------------------------------------------------------------------
char* StringDataDownload::getOutStream()
{
	remainSent_ = totalBytes_ - totalSentBytes_;
	if(remainSent_ > 65535)
		remainSent_ = 65535;
	
	currSent_ = 0;
	return stream_ + totalSentBytes_;
}

//-------------------------------------------------------------------------------------
int8 StringDataDownload::type()
{
	return static_cast<int8>(DataDownloadFactory::DATA_DOWNLOAD_STREAM_STRING);
}



//-------------------------------------------------------------------------------------
FileDataDownload::~FileDataDownload()
{
}

//-------------------------------------------------------------------------------------
bool FileDataDownload::process()
{
	
	return false;
}

//-------------------------------------------------------------------------------------
int8 FileDataDownload::type()
{
	return static_cast<int8>(DataDownloadFactory::DATA_DOWNLOAD_STREAM_FILE);
}


//-------------------------------------------------------------------------------------
}
