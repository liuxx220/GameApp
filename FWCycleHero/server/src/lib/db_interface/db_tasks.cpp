/*
--------------------------------------------------------------------------------------------------------------
		file name : 
		desc      : 操作数据封装成任务对象
		author	  : ljp

		log		  : by ljp create 2017-06-13
--------------------------------------------------------------------------------------------------------------
*/
#include "db_tasks.h"
#include "db_interface.h"
#include "entity_table.h"
#include "thread/threadpool.hpp"
#include "common/memorystream.hpp"






namespace KBEngine
{

	//-------------------------------------------------------------------------------------
	bool DBTaskBase::process()
	{
		uint64 startTime = timestamp();

		bool ret = db_thread_process();

		uint64 duration = startTime - initTime_;
		if (duration > stampsPerSecond())
		{
			WARNING_MSG(fmt::format("DBTask::process(): delay {0:.2f} seconds, try adjusting the kbengine_defs.xml(numConnections) and MySQL(my.cnf->max_connections or innodb_flush_log_at_trx_commit)!\nsql:({1})\n",
									(double(duration) / stampsPerSecondD()), pdbi_->lastquery()));
		}

		duration = timestamp() - startTime;
		if (duration > stampsPerSecond() * 0.2f)
		{
			WARNING_MSG( fmt::format("DBTask::process(): took {:.2f} seconds\nsql:({})\n",
						 (double(duration) / stampsPerSecondD()), pdbi_->lastquery()));
		}

		return ret;
	}

	//-------------------------------------------------------------------------------------
	thread::TPTask::TPTaskState DBTaskBase::presentMainThread()
	{
		return thread::TPTask::TPTASK_STATE_COMPLETED; 
	}

	//-------------------------------------------------------------------------------------
	DBTaskSyncTable::DBTaskSyncTable(SHARED_PTR<EntityTable> pEntityTable) : pEntityTable_(pEntityTable),
																			 success_(false)
	{

	}

	//-------------------------------------------------------------------------------------
	DBTaskSyncTable::~DBTaskSyncTable()
	{

	}

	//-------------------------------------------------------------------------------------
	bool DBTaskSyncTable::db_thread_process()
	{
		success_ = !pEntityTable_->syncToDB(pdbi_);
		return false;
	}

	//-------------------------------------------------------------------------------------
	thread::TPTask::TPTaskState DBTaskSyncTable::presentMainThread()
	{
		EntityTables::getSingleton().onTableSyncSuccessfully(pEntityTable_, success_);
		return thread::TPTask::TPTASK_STATE_COMPLETED; 
	}

//-------------------------------------------------------------------------------------
}
