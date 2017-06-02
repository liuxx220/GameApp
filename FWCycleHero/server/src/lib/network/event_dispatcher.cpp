/*
---------------------------------------------------------------------------------------
		file name : event_dispatcher.hpp
		desc      : 网络事件分派对象，并管理一些时间处理器
		author    : LJP

		log		  : [ 2015-04-28 ]
---------------------------------------------------------------------------------------
*/


#include "event_dispatcher.hpp"
#include "network/event_poller.hpp"







namespace KBEngine 
{ 
		EventDispatcher::EventDispatcher() :
			breakProcessing_(EVENT_DISPATCHER_STATUS_RUNNING),
			maxWait_(0.1),
			numTimerCalls_(0),
			accSpareTime_(0),
			oldSpareTime_(0),
			totSpareTime_(0),
			lastStatisticsGathered_(0),
			pFrequentTasks_(new Tasks)
		{
			pPoller_ = EventPoller::create();
		}

		//-------------------------------------------------------------------------------------
		EventDispatcher::~EventDispatcher()
		{
			SAFE_RELEASE(pFrequentTasks_);
			SAFE_RELEASE(pPoller_);
		}

		//-------------------------------------------------------------------------------------
		EventPoller* EventDispatcher::createPoller()
		{
			pPoller_ = EventPoller::create();
			return pPoller_;
		}

		//-------------------------------------------------------------------------------------
		TimerHandle EventDispatcher::addTimerCommon(int64 microseconds,TimerHandler * handler,
													void * arg, bool recurrent)
		{
			KBE_ASSERT(handler);

			if (microseconds <= 0)
				return TimerHandle();

			uint64 interval = int64(
				(((double)microseconds)/1000000.0) * stampsPerSecondD());

			TimerHandle handle;/* = pTimers_->add(timestamp() + interval,
					recurrent ? interval : 0,
					handler, arg);*/

			return handle;
		}

		//-------------------------------------------------------------------------------------
		uint64 EventDispatcher::getSpareTime() const
		{
			return pPoller_->spareTime();
		}

		//-------------------------------------------------------------------------------------
		void EventDispatcher::clearSpareTime()
		{
			accSpareTime_ += pPoller_->spareTime();
			pPoller_->clearSpareTime();
		}

		//-------------------------------------------------------------------------------------
		double EventDispatcher::proportionalSpareTime() const
		{
			double ret = (double)(int64)(totSpareTime_ - oldSpareTime_);
			return ret / stampsPerSecondD();
			return 0;
		}

		//-------------------------------------------------------------------------------------
		void EventDispatcher::addFrequentTask(Task * pTask)
		{
			pFrequentTasks_->add(pTask);
		}

		//-------------------------------------------------------------------------------------
		bool EventDispatcher::cancelFrequentTask(Task * pTask)
		{
			return pFrequentTasks_->cancel(pTask);
		}

		//-------------------------------------------------------------------------------------
		void EventDispatcher::processFrequentTasks()
		{
			pFrequentTasks_->process();
		}

		//-------------------------------------------------------------------------------------
		void EventDispatcher::processTimers()
		{
			//numTimerCalls_ += pTimers_->process(timestamp());
		}

		//-------------------------------------------------------------------------------------
		void EventDispatcher::processStats()
		{
			if (timestamp() - lastStatisticsGathered_ >= stampsPerSecond())
			{
				oldSpareTime_ = totSpareTime_;
				totSpareTime_ = accSpareTime_ + pPoller_->spareTime();

				lastStatisticsGathered_ = timestamp();
			}
		}

		
		//-------------------------------------------------------------------------------------
		void EventDispatcher::processUntilBreak()
		{
			if (breakProcessing_ != EVENT_DISPATCHER_STATUS_BREAK_PROCESSING)
				breakProcessing_ = EVENT_DISPATCHER_STATUS_RUNNING;

			while (breakProcessing_ != EVENT_DISPATCHER_STATUS_BREAK_PROCESSING)
			{
				this->processOnce(true);
			}
		}

		//-------------------------------------------------------------------------------------
		// function :
		// desc		: 主逻辑的驱动,相当于驱动主逻辑的 Tick
		//-------------------------------------------------------------------------------------
		int EventDispatcher::processOnce(bool shouldIdle)
		{
			
			if (breakProcessing_ != EVENT_DISPATCHER_STATUS_BREAK_PROCESSING)
				breakProcessing_ = EVENT_DISPATCHER_STATUS_RUNNING;

			processFrequentTasks();

			if (breakProcessing_ != EVENT_DISPATCHER_STATUS_BREAK_PROCESSING){
				processTimers();
			}

			this->processStats();
			return 0;
		}

		
		UINT8* EventDispatcher::GetRecv(UINT32 dwHandle, UINT32& dwSize, INT32& nRecvNum)
		{
			return pPoller_->GetRecv(dwHandle, dwSize, nRecvNum);
		}
		
}
