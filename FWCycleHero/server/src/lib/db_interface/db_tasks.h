/*
--------------------------------------------------------------------------------------------------------------
		file name : 
		desc      : 操作数据封装成任务对象
		author	  : ljp

		log		  : by ljp create 2017-06-13
--------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"
#include "common/timer.hpp"
#include "thread/threadtask.hpp"






namespace KBEngine
{ 
	/*-----------------------------------------------------------------------------------------------
	enum TPTaskState
	{
		/// 一个任务已经完成
		TPTASK_STATE_COMPLETED				= 0,

		/// 继续在主线程执行
		TPTASK_STATE_CONTINUE_MAINTHREAD	= 1,

		// 继续在子线程执行
		TPTASK_STATE_CONTINUE_CHILDTHREAD	= 2,
	};
	------------------------------------------------------------------------------------------------*/
	class MemoryStream;
	class DBInterface;
	class EntityTable;

	/*
		数据库线程任务基础类
	*/
	class DBTaskBase : public thread::TPTask
	{
	public:

		DBTaskBase():
		initTime_(timestamp())
		{
		}

		virtual ~DBTaskBase(){}
		virtual bool			process();
		virtual bool			db_thread_process() = 0;
		virtual DBTaskBase*		tryGetNextTask(){ return NULL; }
		
		virtual void			pdbi(DBInterface* ptr){ pdbi_ = ptr; }
		virtual thread::TPTask::TPTaskState presentMainThread();

		uint64					initTime() const{ return initTime_; }
	protected:

		DBInterface*			pdbi_;
		uint64					initTime_;
	};

	/**
		执行一条sql语句
	*/
	class DBTaskSyncTable : public DBTaskBase
	{
	public:
		DBTaskSyncTable(SHARED_PTR<EntityTable> pEntityTable);
		virtual ~DBTaskSyncTable();
		virtual bool			db_thread_process();
		virtual thread::TPTask::TPTaskState presentMainThread();
	protected:
		SHARED_PTR<EntityTable> pEntityTable_;
		bool success_;
	};
}

