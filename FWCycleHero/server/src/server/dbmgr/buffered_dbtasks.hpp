/*
------------------------------------------------------------------------------------------------------------
		file name		:
		desc			: DB 任务缓冲区
		author			: ljp

		log				:
------------------------------------------------------------------------------------------------------------
*/

#ifndef KBE_BUFFERED_DBTASKS_HPP
#define KBE_BUFFERED_DBTASKS_HPP

// common include	
// #define NDEBUG
#include "dbtasks.hpp"
#include "common/common.hpp"
#include "common/memorystream.hpp"
#include "thread/threadtask.hpp"
#include "helper/debug_helper.hpp"

namespace KBEngine
{ 

	/*
		数据库线程任务buffer
	*/
	//class Buffered_DBTasks
	//{
	//public:
	//	typedef std::multimap<DBID, EntityDBTask*> DBID_TASKS_MAP;  
	//	typedef std::multimap<ENTITY_ID, EntityDBTask*> ENTITYID_TASKS_MAP;  
	//
	//	Buffered_DBTasks();
	//	virtual ~Buffered_DBTasks();
	//
	//	void				addTask(EntityDBTask* pTask);

	//	EntityDBTask*		tryGetNextTask(EntityDBTask* pTask);

	//	size_t				size(){ return dbid_tasks_.size() + entityid_tasks_.size(); }

	//	/**
	//		提供给watcher使用
	//	*/
	//	uint32 dbid_tasksSize()
	//	{ 
	//		mutex_.lockMutex();
	//		uint32 ret = (uint32)dbid_tasks_.size(); 
	//		mutex_.unlockMutex();
	//		return ret;
	//	}

	//	/**
	//		提供给watcher使用
	//	*/
	//	uint32 entityid_tasksSize()
	//	{ 
	//		mutex_.lockMutex();
	//		uint32 ret = (uint32)entityid_tasks_.size(); 
	//		mutex_.unlockMutex();
	//		return ret;
	//	}

	//	/**
	//		提供给watcher使用
	//	*/
	//	std::string			printBuffered_dbid();
	//	std::string			printBuffered_entityID();
	//	std::string			printBuffered_dbid_();
	//	std::string			printBuffered_entityID_();

	//protected:
	//	bool				hasTask_(DBID dbid);
	//	bool				hasTask_(ENTITY_ID entityID);

	//	DBID_TASKS_MAP		dbid_tasks_;
	//	ENTITYID_TASKS_MAP	entityid_tasks_;

	//	KBEngine::thread::ThreadMutex mutex_;
	//};

}

#endif // KBE_BUFFERED_DBTASKS_HPP
