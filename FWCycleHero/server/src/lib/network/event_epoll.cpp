/*
------------------------------------------------------------------------------------------------
		file name : event_poller.hpp
		desc	  : ÍøÂç socket Ä£ÐÍ
		author    : LJP

		log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/


#include "event_poller.hpp"
#include "helper/profile.hpp"



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
				{ return this->doRegister(fd, true, true); }

			virtual bool doRegisterForWrite(int fd)
				{ return this->doRegister(fd, false, true); }

			virtual bool doDeregisterForRead(int fd)
				{ return this->doRegister(fd, true, false); }

			virtual bool doDeregisterForWrite(int fd)
				{ return this->doRegister(fd, false, false); }

			virtual int processPendingEvents(double maxWait);

			bool doRegister(int fd, bool isRead, bool isRegister);

		private:
			// epoll file descriptor
			int epfd_;
		};

		//-------------------------------------------------------------------------------------
		EPoller::EPoller(int expectedSize) :
			epfd_(epoll_create(expectedSize))
		{
			if (epfd_ == -1)
			{
				ERROR_MSG(fmt::format("EPoller::EPoller: epoll_create failed: {}\n",
						kbe_strerror()));
			}
		};

		//-------------------------------------------------------------------------------------
		EPoller::~EPoller()
		{
			if (epfd_ != -1)
			{
				close(epfd_);
			}
		}

		//-------------------------------------------------------------------------------------
		bool EPoller::doRegister(int fd, bool isRead, bool isRegister)
		{
			struct epoll_event ev;
			memset(&ev, 0, sizeof(ev)); // stop valgrind warning
			int op;

			ev.data.fd = fd;

			// Handle the case where the file is already registered for the opposite
			// action.
			if (this->isRegistered(fd, !isRead))
			{
				op = EPOLL_CTL_MOD;

				ev.events = isRegister ? EPOLLIN|EPOLLOUT :
							isRead ? EPOLLOUT : EPOLLIN;
			}
			else
			{
				// TODO: Could be good to use EPOLLET (leave like select for now).
				ev.events = isRead ? EPOLLIN : EPOLLOUT;
				op = isRegister ? EPOLL_CTL_ADD : EPOLL_CTL_DEL;
			}

			if (epoll_ctl(epfd_, op, fd, &ev) < 0)
			{
				const char* MESSAGE = "EPoller::doRegister: Failed to {} {} file "
						"descriptor {} ({})\n";
				if (errno == EBADF)
				{
					WARNING_MSG(fmt::format(MESSAGE,
							(isRegister ? "add" : "remove"),
							(isRead ? "read" : "write"),
							fd,
							kbe_strerror()));
				}
				else
				{
					ERROR_MSG(fmt::format(MESSAGE,
							(isRegister ? "add" : "remove"),
							(isRead ? "read" : "write"),
							fd,
							kbe_strerror()));
				}

				return false;
			}

			return true;
		}

		//-------------------------------------------------------------------------------------
		int EPoller::processPendingEvents(double maxWait)
		{
			const int MAX_EVENTS = 10;
			struct epoll_event events[ MAX_EVENTS ];
			int maxWaitInMilliseconds = int(ceil(maxWait * 1000));

		#if ENABLE_WATCHERS
			g_idleProfile.start();
		#else
			uint64 startTime = timestamp();
		#endif

			KBEConcurrency::startMainThreadIdling();
			int nfds = epoll_wait(epfd_, events, MAX_EVENTS, maxWaitInMilliseconds);
			KBEConcurrency::endMainThreadIdling();


		#if ENABLE_WATCHERS
			g_idleProfile.stop();
			spareTime_ += g_idleProfile.lastTime_;
		#else
			spareTime_ += timestamp() - startTime;
		#endif

			for (int i = 0; i < nfds; ++i)
			{
				if (events[i].events & (EPOLLERR|EPOLLHUP))
				{
					this->triggerError(events[i].data.fd);
				}
				else
				{
					if (events[i].events & EPOLLIN)
					{
						this->triggerRead(events[i].data.fd);
					}

					if (events[i].events & EPOLLOUT)
					{
						this->triggerWrite(events[i].data.fd);
					}
				}
			}

			return nfds;
		}

		#endif // HAS_EPOLL
}
