/*
----------------------------------------------------------------------------------------------------------------------
			file name : 
			desc      : 访问数据库的线程池
			author    : ljp

			log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------
*/
#include "db_threadpool.h"
#include "thread/threadtask.hpp"
#include "db_interface/db_interface.h"
#include "thread/threadpool.hpp"
#include "thread/threadguard.hpp"






namespace KBEngine
{

	class DBThread : public thread::TPThread
	{

	public:
		DBThread( thread::ThreadPool* threadPool, int threadWaitSecond = 0):
				  thread::TPThread(threadPool, threadWaitSecond),m_pInterface(NULL)
		{

		}

		~DBThread()
		{

		}

		virtual void onStart()
		{
			DBUtil::initThread();
			m_pInterface = DBUtil::createInterface(false);
			if (m_pInterface == NULL)
			{
				ERROR_MSG("DBThread:: can't create dbinterface!\n");
			}
		}

		virtual void onEnd()
		{
			if (m_pInterface)
			{
				m_pInterface->detach();
				SAFE_RELEASE(m_pInterface);
				DBUtil::finiThread();
			}
		}

		
		virtual thread::TPTask* tryGetTask(void)
		{
			return thread::TPThread::tryGetTask();
		}


		virtual void processTask(thread::TPTask* pTask)
		{ 
			bool retry;
			do
			{
				retry = false;
				try
				{
					thread::TPThread::processTask(pTask); 
				}
				catch (std::exception & e)
				{
					if (!m_pInterface->processException(e))
						break;
				}
			}
			while (retry);
		}

	private:

		//////////////////////////////////////////////////////////////////////////
		// 访问 mysql 的接口
		DBInterface*		m_pInterface;		
	};


	//-------------------------------------------------------------------------------------
	DBThreadPool::DBThreadPool():
	thread::ThreadPool()
	{

	}

	//-------------------------------------------------------------------------------------
	DBThreadPool::~DBThreadPool()
	{

	}

	//-------------------------------------------------------------------------------------
	thread::TPThread* DBThreadPool::createThread(int threadWaitSecond)
	{
		DBThread* tptd = new DBThread(this, threadWaitSecond);
		tptd->createThread();
		return tptd;
	}	

	//-------------------------------------------------------------------------------------
}
