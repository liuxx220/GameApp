/*
------------------------------------------------------------------------------------------------
		file name : event_poller.hpp
		desc	  : ÍøÂç socket Ä£ÐÍ
		author    : LJP

		log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/
#include "event_poller.hpp"
#include "event_iocp.hpp"
#include "event_epoll.hpp"
#include "helper/profile.hpp"





namespace KBEngine 
{ 


		//-------------------------------------------------------------------------------------
		EventPoller::EventPoller() : fdReadHandlers_(), fdWriteHandlers_(), spareTime_(0)
		{

		}


		//-------------------------------------------------------------------------------------
		EventPoller::~EventPoller()
		{

		}

		//-------------------------------------------------------------------------------------
		int EventPoller::maxFD() const
		{
			int readMaxFD		= EventPoller::maxFD(fdReadHandlers_);
			int writeMaxFD		= EventPoller::maxFD(fdWriteHandlers_);
			return std::max(readMaxFD, writeMaxFD);
		}

		//-------------------------------------------------------------------------------------
		int EventPoller::maxFD(const FDHandlers & handlerMap)
		{
			int maxFD = -1;
			FDHandlers::const_iterator iFDHandler = handlerMap.begin();
			while (iFDHandler != handlerMap.end())
			{
				if (iFDHandler->first > maxFD)
				{
					maxFD = iFDHandler->first;
				}
				++iFDHandler;
			}
			return maxFD;
		}

		//-------------------------------------------------------------------------------------
		EventPoller * EventPoller::create()
		{
		#if KBE_PLATFORM == PLATFORM_WIN32					
			return new CIocpPoller();
		#else												
			return new EPoller();
		#endif
		}
}
