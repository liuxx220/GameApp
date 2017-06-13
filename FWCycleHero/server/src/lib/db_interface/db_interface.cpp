/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_interface.cpp
		desc      : 应用层到DB操作的封装
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#include "db_interface.h"
#include "db_threadpool.h"
#include "entity_table.h"
#include "db_mysql/db_interface_mysql.h"
#include "db_mysql/kbe_table_mysql.h"
#include "server/serverconfig.hpp"
#include "thread/threadpool.hpp"






namespace KBEngine 
{ 

	KBE_SINGLETON_INIT(DBUtil);
	DBUtil g_DBUtil;
	thread::ThreadPool* DBUtil::pThreadPool_ = new DBThreadPool();

	//-------------------------------------------------------------------------------------
	DBUtil::DBUtil()
	{

	}

	//-------------------------------------------------------------------------------------
	DBUtil::~DBUtil()
	{

	}

	//-------------------------------------------------------------------------------------
	bool DBUtil::initThread()
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		if(strcmp(dbcfg.db_type, "mysql") == 0)
		{
			if (!mysql_thread_safe()) 
			{
				KBE_ASSERT(false);
			}
			else
			{
				mysql_thread_init();
			}
		}
	
		return true;
	}

	//-------------------------------------------------------------------------------------
	bool DBUtil::finiThread()
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		if(strcmp(dbcfg.db_type, "mysql") == 0)
		{
			mysql_thread_end();
		}

		return true;
	}

	//-------------------------------------------------------------------------------------
	bool DBUtil::initialize()
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		if(dbcfg.db_passwordEncrypt)
		{
			// 如果小于64则表明当前是明文密码配置
			if(strlen(dbcfg.db_password) < 64)
			{
				//WARNING_MSG(fmt::format("DBUtil::createInterface: db password is not encrypted!\nplease use password(rsa):\n{}\n",
				//	KBEKey::getSingleton().encrypt(dbcfg.db_password)));
			}
			else
			{
				/*std::string out = KBEKey::getSingleton().decrypt(dbcfg.db_password);

				if(out.size() == 0)
					return false;

				kbe_snprintf(dbcfg.db_password, MAX_BUF, "%s", out.c_str());*/
			}
		}

		return true;
	}

	//-------------------------------------------------------------------------------------
	void DBUtil::finalise()
	{
		pThreadPool()->finalise();
		SAFE_RELEASE(pThreadPool_);
	}

	//-------------------------------------------------------------------------------------
	DBInterface* DBUtil::createInterface(bool showinfo)
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		DBInterface* dbinterface = NULL;

		if(strcmp(dbcfg.db_type, "mysql") == 0)
		{
			dbinterface = new DBInterfaceMysql(dbcfg.db_unicodeString_characterSet, dbcfg.db_unicodeString_collation);
		}

		if(dbinterface == NULL)
		{
			ERROR_MSG("DBUtil::createInterface: can't create dbinterface!\n");
			return NULL;
		}
	
		kbe_snprintf(dbinterface->db_type_, MAX_BUF, "%s", dbcfg.db_type);
		dbinterface->db_port_ = dbcfg.db_port;	
		kbe_snprintf(dbinterface->db_ip_, MAX_IP, "%s", dbcfg.db_ip);
		kbe_snprintf(dbinterface->db_username_, MAX_BUF, "%s", dbcfg.db_username);
		dbinterface->db_numConnections_ = dbcfg.db_numConnections;
		kbe_snprintf(dbinterface->db_password_, MAX_BUF, "%s", dbcfg.db_password);

		if(!dbinterface->attach(DBUtil::dbname()))
		{
			ERROR_MSG( fmt::format("DBUtil::createInterface: can't attach to database!\n\tdbinterface={0:p}\n\targs={1}",
					   (void*)&dbinterface, dbinterface->c_str()));

			delete dbinterface;
			return NULL;
		}
		else
		{
			if(showinfo)
			{
				INFO_MSG( fmt::format("DBUtil::createInterface[{0:p}]: {1}", (void*)&dbinterface, 
						  dbinterface->c_str()));
			}
		}

		return dbinterface;
	}

	//-------------------------------------------------------------------------------------
	const char* DBUtil::dbname()
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		return dbcfg.db_name;
	}

	//-------------------------------------------------------------------------------------
	const char* DBUtil::dbtype()
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		return dbcfg.db_type;
	}

	//-------------------------------------------------------------------------------------
	const char* DBUtil::accountScriptName()
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		return NULL;
	}

	//-------------------------------------------------------------------------------------
	bool DBUtil::initInterface(DBInterface* dbi)
	{
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		if(strcmp(dbcfg.db_type, "mysql") == 0)
		{
			EntityTables::getSingleton().addKBETable(new KBEAccountTableMysql());
			EntityTables::getSingleton().addKBETable(new KBEEntityLogTableMysql());
		}
	
		if(!pThreadPool_->isInitialize())
		{
			if(!pThreadPool_->createThreadPool(dbcfg.db_numConnections, 
				dbcfg.db_numConnections, dbcfg.db_numConnections))
				return false;
		}

		bool ret = EntityTables::getSingleton().load(dbi);
		if(ret)
		{
			ret = dbi->checkEnvironment();
		}
	
		return ret && EntityTables::getSingleton().syncToDB(dbi);
	}

//-------------------------------------------------------------------------------------
}
