/*
------------------------------------------------------------------------------------------------
file name : event_poller.hpp
desc	  : ÍøÂç socket select epoll iocp ÂÖÑ¯Ä£ÐÍ
author    : LJP

log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/

#ifndef EVENT_POLLER_EPOLL_HPP
#define EVENT_POLLER_EPOLL_HPP

#include "common/common.hpp"
#include "common/timestamp.hpp"
#include "network/interfaces.hpp"
#include "thread/concurrency.hpp"
#include "network/common.hpp"
#include <map>





namespace KBEngine 
{ 
	//-------------------------------------------------------------------------------------
#ifdef HAS_EPOLL
	#include <sys/epoll.h>


	/**
	*	This class is an EventPoller that uses epoll.
	*/
	class EPoller : public EventPoller
	{
	public:
		EPoller(int expectedSize = 10);
		virtual ~EPoller();

		int getFileDescriptor() const { return epfd_; }

	protected:
		virtual bool doRegisterForRead(int fd)
		{
			return this->doRegister(fd, true, true);
		}

		virtual bool doRegisterForWrite(int fd)
		{
			return this->doRegister(fd, false, true);
		}

		virtual bool doDeregisterForRead(int fd)
		{
			return this->doRegister(fd, true, false);
		}

		virtual bool doDeregisterForWrite(int fd)
		{
			return this->doRegister(fd, false, false);
		}

		virtual int processPendingEvents(double maxWait);

		bool doRegister(int fd, bool isRead, bool isRegister);

	private:
		// epoll file descriptor
		int epfd_;
	};

	#endif // HAS_EPOLL
}
#endif // EVENT_POLLER_EPOLL_HPP
