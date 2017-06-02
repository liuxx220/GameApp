/*
---------------------------------------------------------------------------------------
		file name : event_dispatcher.hpp
		desc      : 网络事件分派对象，并管理一些时间处理器
		author    : LJP

		log		  : [ 2015-04-28 ]
---------------------------------------------------------------------------------------
*/

#ifndef KBE_EVENT_DISPATCHER_HPP
#define KBE_EVENT_DISPATCHER_HPP

#include <map>
#include "common/tasks.hpp"
#include "common/timer.hpp"
#include "network/interfaces.hpp"
#include "network/common.hpp"




namespace KBEngine 
{ 
		class DispatcherCoupling;
		class EventPoller;
		class EventDispatcher
		{
		public:
			enum EVENT_DISPATCHER_STATUS
			{
				EVENT_DISPATCHER_STATUS_RUNNING = 0,
				EVENT_DISPATCHER_STATUS_WAITING_BREAK_PROCESSING = 1,
				EVENT_DISPATCHER_STATUS_BREAK_PROCESSING = 2
			};

			EventDispatcher();
			virtual ~EventDispatcher();
	

			/////////////////////////////////////////////////////////////////////////////////
			// 网络相关接口
			UINT8*					GetRecv(UINT32 dwHandle, UINT32& dwSize, INT32& nRecvNum);
			void					KickPlayer(UINT32 dwCDIndex){}
			/////////////////////////////////////////////////////////////////////////////////
			
			void					processUntilBreak();
			int						processOnce(bool shouldIdle = false);
			bool					isBreakProcessing()const			{ return breakProcessing_ == EVENT_DISPATCHER_STATUS_BREAK_PROCESSING; }
			bool					isWaitBreakProcessing()const		{ return breakProcessing_ == EVENT_DISPATCHER_STATUS_WAITING_BREAK_PROCESSING; }

			void					breakProcessing(bool breakState = true);
			INLINE void				setWaitBreakProcessing();
			bool					processingBroken()const;
	
			void					addFrequentTask(Task * pTask);
			bool					cancelFrequentTask(Task * pTask);
	
			INLINE double			maxWait() const;
			INLINE void				maxWait(double seconds);

			
			INLINE TimerHandle		addTimer(int64 microseconds,
												TimerHandler * handler, void* arg = NULL);
			INLINE TimerHandle		addOnceOffTimer(int64 microseconds,
														TimerHandler * handler, void * arg = NULL);

			uint64					timerDeliveryTime(TimerHandle handle) const;
			uint64					timerIntervalTime(TimerHandle handle) const;
			uint64&					timerIntervalTime(TimerHandle handle);
	
			uint64					getSpareTime() const;
			void					clearSpareTime();
			double					proportionalSpareTime() const;

		
			INLINE EventPoller*		createPoller();
			EventPoller*			pPoller(){ return pPoller_; }

		private:
			TimerHandle				addTimerCommon(int64 microseconds,
													TimerHandler * handler,
													void * arg,
													bool recurrent);

			void					processFrequentTasks();
			void					processTimers();
			void					processStats();
	
			double					calculateWait() const;
	
			void					attachTo(EventDispatcher & parentDispatcher);
			void					detachFrom(EventDispatcher & parentDispatcher);
	
		protected:

			int8					breakProcessing_;
			double					maxWait_;
			uint32					numTimerCalls_;
	
			// Statistics
			TimeStamp				accSpareTime_;
			TimeStamp				oldSpareTime_;
			TimeStamp				totSpareTime_;
			TimeStamp				lastStatisticsGathered_;
	
			Tasks*					pFrequentTasks_;
			EventPoller*			pPoller_;
		};

		/*INLINE TimerHandle EventDispatcher::addTimer(int64 microseconds,TimerHandler * handler, void * arg)
		{
			return this->addTimerCommon(microseconds, handler, arg, true);
		}

		INLINE TimerHandle EventDispatcher::addOnceOffTimer(int64 microseconds,
			TimerHandler * handler, void * arg)
		{
			return this->addTimerCommon(microseconds, handler, arg, false);
		}

		INLINE void EventDispatcher::breakProcessing(bool breakState)
		{
			if (breakState)
				breakProcessing_ = EVENT_DISPATCHER_STATUS_BREAK_PROCESSING;
			else
				breakProcessing_ = EVENT_DISPATCHER_STATUS_RUNNING;
		}

		INLINE void EventDispatcher::setWaitBreakProcessing()
		{
			breakProcessing_ = EVENT_DISPATCHER_STATUS_WAITING_BREAK_PROCESSING;
		}

		INLINE bool EventDispatcher::processingBroken() const
		{
			return breakProcessing_ == EVENT_DISPATCHER_STATUS_BREAK_PROCESSING;
		}

		INLINE double EventDispatcher::maxWait() const
		{
			return maxWait_;
		}

		INLINE void EventDispatcher::maxWait(double seconds)
		{
			maxWait_ = seconds;
		}*/
}

#endif // KBE_EVENT_DISPATCHER_HPP
