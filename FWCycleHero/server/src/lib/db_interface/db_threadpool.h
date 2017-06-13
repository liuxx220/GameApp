/*
----------------------------------------------------------------------------------------------------------------------
			file name : 
			desc      : 访问数据库的线程池
			author    : ljp

			log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "db_tasks.h"
#include "common/common.hpp"
#include "common/memorystream.hpp"
#include "thread/threadtask.hpp"
#include "helper/debug_helper.hpp"
#include "thread/threadpool.hpp"







namespace KBEngine
{ 

	/*
		数据库线程任务buffer
	*/
	class TPThread;
	class DBThreadPool : public thread::ThreadPool
	{
	public:
		 DBThreadPool();
		~DBThreadPool();

		virtual thread::TPThread*	createThread(int threadWaitSecond = 0);
		virtual std::string			name() const{ return "DBThreadPool"; }
	protected:

	};

}

