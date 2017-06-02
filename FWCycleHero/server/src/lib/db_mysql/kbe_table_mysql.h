#ifndef KBE_KBE_TABLE_MYSQL_H
#define KBE_KBE_TABLE_MYSQL_H
#include "common.h"
#include "common/common.hpp"
#include "common/singleton.hpp"
#include "helper/debug_helper.hpp"
#include "db_interface/entity_table.h"
#include "db_interface/kbe_tables.h"







namespace KBEngine 
{ 

	/*
		kbe系统表
	*/
	class KBEEntityLogTableMysql : public KBEEntityLogTable
	{
	public:
		KBEEntityLogTableMysql();
		virtual ~KBEEntityLogTableMysql(){}
	
		/**
			同步entity表到数据库中
		*/
		virtual bool				syncToDB(DBInterface* dbi);
		virtual bool				syncIndexToDB(DBInterface* dbi){ return true; }

	protected:
	
	};

	class KBEAccountTableMysql : public KBEAccountTable
	{
	public:
		KBEAccountTableMysql();
		virtual ~KBEAccountTableMysql(){}
	
		/**
			同步entity表到数据库中
		*/
		virtual bool				syncToDB(DBInterface* dbi);
		virtual bool				syncIndexToDB(DBInterface* dbi){ return true; }

		bool						queryAccount(DBInterface * dbi, const std::string& name, ACCOUNT_INFOS& info);
		bool						queryAccountAllInfos(DBInterface * dbi, const std::string& name, ACCOUNT_INFOS& info);
		bool						logAccount(DBInterface * dbi, ACCOUNT_INFOS& info);
		bool						setFlagsDeadline(DBInterface * dbi, const std::string& name, uint32 flags, uint64 deadline);
		virtual bool				updateCount(DBInterface * dbi, DBID dbid);
		virtual bool				updatePassword(DBInterface * dbi, const std::string& name, const std::string& password);
	protected:
	};

	

}

#endif // KBE_KBE_TABLE_MYSQL_H
