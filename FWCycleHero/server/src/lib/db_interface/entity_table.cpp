/*
--------------------------------------------------------------------------------------------------
		file name	:
		desc		: 服务器数据库表维护
		author		: ljp

		log			:
--------------------------------------------------------------------------------------------------
*/
#include "db_tasks.h"
#include "db_threadpool.h"
#include "entity_table.h"
#include "db_interface.h"
#include "thread/threadguard.hpp"







namespace KBEngine { 


	KBE_SINGLETON_INIT(EntityTables);
	EntityTables g_EntityTables;

	//-------------------------------------------------------------------------------------
	void EntityTable::addItem(EntityTableItem* pItem)
	{
		tableItems_[pItem->utype()].reset(pItem);
		tableFixedOrderItems_.push_back(pItem);
	}

	//-------------------------------------------------------------------------------------
	EntityTableItem* EntityTable::findItem(int32/*ENTITY_PROPERTY_UID*/ utype)
	{
		TABLEITEM_MAP::iterator iter = tableItems_.find(utype);
		if(iter != tableItems_.end())
		{
			return iter->second.get();
		}

		return NULL;
	}



	//-------------------------------------------------------------------------------------
	EntityTables::EntityTables():
	tables_(),
	kbe_tables_(),
	numSyncTables_(0),
	syncTablesError_(false)
	{

	}

	//-------------------------------------------------------------------------------------
	EntityTables::~EntityTables()
	{
		tables_.clear();
		kbe_tables_.clear();
	}

	//-------------------------------------------------------------------------------------
	bool EntityTables::load(DBInterface* dbi)
	{
	

		return true;
	}

	//-------------------------------------------------------------------------------------
	void EntityTables::onTableSyncSuccessfully(SHARED_PTR<EntityTable> pEntityTable, bool error)
	{
		if(error)
		{
			syncTablesError_ = true;
			return;
		}

		numSyncTables_++;
	}


	//-------------------------------------------------------------------------------------
	bool EntityTables::syncToDB(DBInterface* dbi)
	{
		DBThreadPool* pDBThreadPool = static_cast<DBThreadPool*>(DBUtil::pThreadPool());
		KBE_ASSERT(pDBThreadPool != NULL);
	
		int num = 0;
		try
		{
			// 开始同步所有表
			EntityTables::TABLES_MAP::iterator kiter = kbe_tables_.begin();
			for(; kiter != kbe_tables_.end(); ++kiter)
			{
				num++;
				pDBThreadPool->addTask(new DBTaskSyncTable(kiter->second));
			}

			EntityTables::TABLES_MAP::iterator iter = tables_.begin();
			for(; iter != tables_.end(); ++iter)
			{
				if(!iter->second->hasSync())
				{
					num++;
					pDBThreadPool->addTask(new DBTaskSyncTable(iter->second));
				}
			}

			while(true)
			{
				if(syncTablesError_)
					return false;

				if(numSyncTables_ == num)
					break;

				pDBThreadPool->onMainThreadTick();
				sleep(10);
			};


			std::vector<std::string> dbTableNames;
			dbi->getTableNames(dbTableNames, "");

			// 检查是否有需要删除的表
			std::vector<std::string>::iterator iter0 = dbTableNames.begin();
			for(; iter0 != dbTableNames.end(); ++iter0)
			{
				std::string tname = (*iter0);
				if(std::string::npos == tname.find(ENTITY_TABLE_PERFIX"_"))
					continue;

				KBEngine::strutil::kbe_replace(tname, ENTITY_TABLE_PERFIX"_", "");
				EntityTables::TABLES_MAP::iterator iter = tables_.find(tname);
				if(iter == tables_.end())
				{
					if(!dbi->dropEntityTableFromDB((std::string(ENTITY_TABLE_PERFIX"_") + tname).c_str()))
						return false;
				}
			}
		}
		catch (std::exception& e)
		{
			ERROR_MSG(fmt::format("EntityTables::syncToDB: {}\n", e.what()));
			return false;
		}

		return true;
	}

	//-------------------------------------------------------------------------------------
	void EntityTables::addTable(EntityTable* pTable)
	{
		TABLES_MAP::iterator iter = tables_.begin();

		for(; iter != tables_.end(); ++iter)
		{
			if(iter->first == pTable->tableName())
			{
				KBE_ASSERT(false && "table exist!\n");
				return;
			}
		}

		tables_[pTable->tableName()].reset(pTable);
	}

	//-------------------------------------------------------------------------------------
	EntityTable* EntityTables::findTable(std::string name)
	{
		TABLES_MAP::iterator iter = tables_.find(name);
		if(iter != tables_.end())
		{
			return iter->second.get();
		}

		return NULL;
	};

	//-------------------------------------------------------------------------------------
	void EntityTables::addKBETable(EntityTable* pTable)
	{
		TABLES_MAP::iterator iter = kbe_tables_.begin();

		for(; iter != kbe_tables_.end(); ++iter)
		{
			if(iter->first == pTable->tableName())
			{
				KBE_ASSERT(false && "table exist!\n");
				return;
			}
		}

		kbe_tables_[pTable->tableName()].reset(pTable);
	}

	//-------------------------------------------------------------------------------------
	EntityTable* EntityTables::findKBETable(std::string name)
	{
		TABLES_MAP::iterator iter = kbe_tables_.find(name);
		if(iter != kbe_tables_.end())
		{
			return iter->second.get();
		}

		return NULL;
	};

}
