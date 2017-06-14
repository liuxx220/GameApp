/*
---------------------------------------------------------------------------------------------------------------------------
			file name : db_interface_mysql.h
			desc	  : 连接和操作mysql数据的封装
			author    : ljp

			log		  : by ljp create 2017-06-13
---------------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"
#include "common/singleton.hpp"
#include "common/memorystream.hpp"
#include "helper/debug_helper.hpp"
#include "mysql/mysql.h"






//------------------------------------------------------------------------------------------------------------------------
/**	Database类 - 提供数据库操作类，包括连接的建立及维护，各种查询及更新操作机制等等
1. Database对象与数据库一一对应，如果上层应用程序需要多个库，要建立多个InterfaceMysql对象
1. 连接池机制，方便多线程的并发操作
2. 提供对数据库连接参数以及当前数据库状态信息的提取
3. 提供三种查询及操作方式——阻塞式操作，回调式操作及无返回操作
4. 提供简单的事务处理机制，日后添加完整事务处理支持
*/
//------------------------------------------------------------------------------------------------------------------------
namespace KBEngine 
{ 

	struct MysqlConnection
	{
		MYSQL*		m_Mysql;
		MysqlConnection();
	};


	/*
		数据库接口
	*/
	class InterfaceMysql
	{

	public:
		 InterfaceMysql();
		~InterfaceMysql();

	private:
		InterfaceMysql(const InterfaceMysql&);
		InterfaceMysql& operator = (const InterfaceMysql&);

	protected:

		/**
		与某个数据库关联
		*/
		bool				attach(const char* databaseName = NULL);
		bool				detach();
		bool				reattach();

		bool				query(const char* strCommand, uint32 size, bool showexecinfo = true);
		bool				execute(const char* strCommand, uint32 size, MemoryStream * resdata);

		/**
		如果数据库不存在则创建一个数据库
		*/
		virtual bool		createDatabaseIfNotExist();

		/**
		数据表操作
		*/
		bool				createTable();
		bool				dropTableFromDB(const char* tablename);

		/**
		锁住接口操作
		*/
		bool				lock();
		bool				unlock();

		/**
		处理异常
		*/
		bool				processException(std::exception & e);

	protected:
		MYSQL*				m_pMysql;
		bool				m_IslostConnection;
	};


}
