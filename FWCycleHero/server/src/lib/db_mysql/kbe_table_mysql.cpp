#include "entity_table_mysql.h"
#include "kbe_table_mysql.h"
#include "db_exception.h"
#include "db_interface_mysql.h"
#include "db_interface/db_interface.h"
#include "db_interface/entity_table.h"
#include "server/serverconfig.hpp"





namespace KBEngine 
{ 

	//-------------------------------------------------------------------------------------
	bool KBEEntityLogTableMysql::syncToDB(DBInterface* dbi)
	{
		std::string sqlstr = "DROP TABLE IF EXISTS kbe_entitylog;";
		try
		{
			dbi->query(sqlstr.c_str(), sqlstr.size(), false);
		}
		catch (...)
		{

		}

		bool ret = false;
		sqlstr = "CREATE TABLE IF NOT EXISTS kbe_entitylog "
				 "(entityDBID bigint(20) not null DEFAULT 0,"
				 "entityType int unsigned not null DEFAULT 0,"
				 "entityID int unsigned not null DEFAULT 0,"
				 "ip varchar(64),"
				 "port int unsigned not null DEFAULT 0,"
				 "componentID bigint unsigned not null DEFAULT 0,"
				 "PRIMARY KEY (entityDBID, entityType))"
				 "ENGINE="MYSQL_ENGINE_TYPE;

		ret = dbi->query(sqlstr.c_str(), sqlstr.size(), true);
		KBE_ASSERT(ret);
		return ret;
	}


	//-------------------------------------------------------------------------------------
	KBEEntityLogTableMysql::KBEEntityLogTableMysql() : KBEEntityLogTable()
	{

	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::syncToDB(DBInterface* dbi)
	{
		bool ret = false;
		std::string sqlstr = "CREATE TABLE IF NOT EXISTS accountinfos "
							 "(`accountName` varchar(255) not null, PRIMARY KEY idKey (`accountName`),"
							 "`password` varchar(255),"
							 "`bindata` blob,"
							 "`email` varchar(255),"
							 "`entityDBID` bigint(20) not null DEFAULT 0, UNIQUE KEY `entityDBID` (`entityDBID`),"
							 "`flags` int unsigned not null DEFAULT 0,"
							 "`deadline` bigint(20) not null DEFAULT 0,"
							 "`regtime` bigint(20) not null DEFAULT 0,"
							 "`lasttime` bigint(20) not null DEFAULT 0,"
							 "`numlogin` int unsigned not null DEFAULT 0)"
							 "ENGINE="MYSQL_ENGINE_TYPE;

		ret = dbi->query(sqlstr.c_str(), sqlstr.size(), true);
		KBE_ASSERT(ret);
		return ret;
	}

	//-------------------------------------------------------------------------------------
	KBEAccountTableMysql::KBEAccountTableMysql() : KBEAccountTable()
	{

	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::setFlagsDeadline(DBInterface * dbi, const std::string& name, uint32 flags, uint64 deadline)
	{
		char* tbuf = new char[name.size() * 2 + 1];

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
									tbuf, name.c_str(), name.size());

		std::string sqlstr = fmt::format("update accountinfos set flags={}, deadline={} where accountName like \"{}\"", 
										  flags, deadline, tbuf);

		SAFE_RELEASE_ARRAY(tbuf);

		// 如果查询失败则返回存在， 避免可能产生的错误
		if(dbi->query(sqlstr.c_str(), sqlstr.size(), false))
			return true;

		return false;
	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::queryAccount(DBInterface * dbi, const std::string& name, ACCOUNT_INFOS& info)
	{
		std::string sqlstr = "select entityDBID, password, flags, deadline from accountinfos where accountName like \"";

		char* tbuf = new char[name.size() * 2 + 1];

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, name.c_str(), name.size());

		sqlstr += tbuf;

		SAFE_RELEASE_ARRAY(tbuf);
		sqlstr += "\"";

		// 如果查询失败则返回存在， 避免可能产生的错误
		if(!dbi->query(sqlstr.c_str(), sqlstr.size(), false))
			return true;

		info.dbid = 0;
		MYSQL_RES * pResult = mysql_store_result(static_cast<DBInterfaceMysql*>(dbi)->mysql());
		if(pResult)
		{
			MYSQL_ROW arow;
			while((arow = mysql_fetch_row(pResult)) != NULL)
			{
				KBEngine::StringConv::str2value(info.dbid, arow[0]);
				info.name = name;
				info.password = arow[1];

				KBEngine::StringConv::str2value(info.flags, arow[2]);
				KBEngine::StringConv::str2value(info.deadline, arow[3]);
			}

			mysql_free_result(pResult);
		}

		return info.dbid > 0;
	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::queryAccountAllInfos(DBInterface * dbi, const std::string& name, ACCOUNT_INFOS& info)
	{
		std::string sqlstr = "select entityDBID, password, email, flags, deadline from accountinfos where accountName like \"";

		char* tbuf = new char[name.size() * 2 + 1];

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, name.c_str(), name.size());

		sqlstr += tbuf;

		SAFE_RELEASE_ARRAY(tbuf);
		sqlstr += "\"";

		// 如果查询失败则返回存在， 避免可能产生的错误
		if(!dbi->query(sqlstr.c_str(), sqlstr.size(), false))
			return true;

		info.dbid = 0;
		MYSQL_RES * pResult = mysql_store_result(static_cast<DBInterfaceMysql*>(dbi)->mysql());
		if(pResult)
		{
			MYSQL_ROW arow;
			while((arow = mysql_fetch_row(pResult)) != NULL)
			{
				KBEngine::StringConv::str2value(info.dbid, arow[0]);
				info.name = name;
				info.password = arow[1];
				info.email = arow[2];
				KBEngine::StringConv::str2value(info.flags, arow[3]);
				KBEngine::StringConv::str2value(info.deadline, arow[4]);
			}

			mysql_free_result(pResult);
		}

		return info.dbid > 0;
	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::updateCount(DBInterface * dbi, DBID dbid)
	{
		// 如果查询失败则返回存在， 避免可能产生的错误
		if(!dbi->query(fmt::format("update accountinfos set lasttime={}, numlogin=numlogin+1 where entityDBID={}",
			time(NULL), dbid), false))
			return false;

		return true;
	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::updatePassword(DBInterface * dbi, const std::string& name, const std::string& password)
	{
		char* tbuf = new char[MAX_BUF * 3];
		char* tbuf1 = new char[MAX_BUF * 3];

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, password.c_str(), password.size());

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf1, name.c_str(), name.size());

		// 如果查询失败则返回存在， 避免可能产生的错误
		if(!dbi->query(fmt::format("update accountinfos set password=\"{}\" where accountName like \"{}\"", 
			password, tbuf1), false))
		{
			SAFE_RELEASE_ARRAY(tbuf);
			SAFE_RELEASE_ARRAY(tbuf1);
			return false;
		}
	
		SAFE_RELEASE_ARRAY(tbuf);
		SAFE_RELEASE_ARRAY(tbuf1);
		return true;
	}

	//-------------------------------------------------------------------------------------
	bool KBEAccountTableMysql::logAccount(DBInterface * dbi, ACCOUNT_INFOS& info)
	{
		std::string sqlstr = "insert into accountinfos (accountName, password, bindata, email, entityDBID, flags, deadline, regtime, lasttime) values(";

		char* tbuf = new char[MAX_BUF > info.datas.size() ? MAX_BUF * 3 : info.datas.size() * 3];

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, info.name.c_str(), info.name.size());

		sqlstr += "\"";
		sqlstr += tbuf;
		sqlstr += "\",";

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, info.password.c_str(), info.password.size());

		sqlstr += "md5(\"";
		sqlstr += tbuf;
		sqlstr += "\"),";

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, info.datas.data(), info.datas.size());

		sqlstr += "\"";
		sqlstr += tbuf;
		sqlstr += "\",";

		mysql_real_escape_string(static_cast<DBInterfaceMysql*>(dbi)->mysql(), 
			tbuf, info.email.c_str(), info.email.size());

		sqlstr += "\"";
		sqlstr += tbuf;
		sqlstr += "\",";

		kbe_snprintf(tbuf, MAX_BUF, "%"PRDBID, info.dbid);
		sqlstr += tbuf;
		sqlstr += ",";

		kbe_snprintf(tbuf, MAX_BUF, "%u", info.flags);
		sqlstr += tbuf;
		sqlstr += ",";
	
		kbe_snprintf(tbuf, MAX_BUF, "%"PRIu64, info.deadline);
		sqlstr += tbuf;
		sqlstr += ",";
	
		kbe_snprintf(tbuf, MAX_BUF, "%"PRTime, time(NULL));
		sqlstr += tbuf;
		sqlstr += ",";

		kbe_snprintf(tbuf, MAX_BUF, "%"PRTime, time(NULL));
		sqlstr += tbuf;
		sqlstr += ")";

		SAFE_RELEASE_ARRAY(tbuf);

		// 如果查询失败则返回存在， 避免可能产生的错误
		if(!dbi->query(sqlstr.c_str(), sqlstr.size(), false))
		{
			ERROR_MSG(fmt::format("KBEAccountTableMysql::logAccount({}): sql({}) is failed({})!\n", 
					info.name, sqlstr, dbi->getstrerror()));

			return false;
		}

		return true;
	}

//-------------------------------------------------------------------------------------
}
