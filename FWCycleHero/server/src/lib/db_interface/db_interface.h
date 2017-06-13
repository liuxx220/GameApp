/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_interface.h
		desc      : 应用层到DB操作的封装
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"
#include "common/singleton.hpp"
#include "helper/debug_helper.hpp"
#include "db_interface/entity_table.h"







namespace KBEngine 
{ 

	namespace thread
	{
		class ThreadPool;
	}
	class DBUtil;

	/*
		数据库接口
	*/
	class DBInterface
	{
	public:
		enum DB_OP_STATE
		{
			DB_OP_READ,
			DB_OP_WRITE,
		};

		friend class DBUtil;
		DBInterface(): db_port_(3306), db_numConnections_(1),lastquery_()
		{

		};

		virtual ~DBInterface()
		{

		};

		/**
			检查环境
		*/
		virtual bool				checkEnvironment() = 0;
	
		/**
			检查错误， 对错误的内容进行纠正
			如果纠正不成功返回失败
		*/
		virtual bool				checkErrors() = 0;

		/**
			与某个数据库关联
		*/
		virtual bool				attach(const char* databaseName) = 0;
		virtual bool				detach() = 0;

		/**
			获取数据库所有的表名
		*/
		virtual bool				getTableNames( std::vector<std::string>& tableNames, const char * pattern) = 0;

		/**
			获取数据库某个表所有的字段名称
		*/
		virtual bool				getTableItemNames(const char* tablename, std::vector<std::string>& itemNames) = 0;

		/**
			查询表
		*/
		virtual bool				query(const char* strCommand, uint32 size, bool showexecinfo = true) = 0;
		virtual bool				query(const std::string& cmd, bool showexecinfo = true)
		{
			return					query(cmd.c_str(), cmd.size(), showexecinfo);
		}

		/**
			返回这个接口的描述
		*/
		virtual const char*			c_str() = 0;

		/** 
			获取错误
		*/
		virtual const char*			getstrerror() = 0;

		/** 
			获取错误编号
		*/
		virtual int					getlasterror() = 0;

		/**
			创建一个entity存储表
		*/
		virtual EntityTable*		createEntityTable() = 0;

		/** 
			从数据库删除entity表
		*/
		virtual bool				dropEntityTableFromDB(const char* tablename) = 0;

		/** 
			从数据库删除entity表字段
		*/
		virtual bool				dropEntityTableItemFromDB(const char* tablename, const char* tableItemName) = 0;

		/**
			锁住接口操作
		*/
		virtual bool				lock() = 0;
		virtual bool				unlock() = 0;

		/**
			处理异常
		*/
		virtual bool				processException(std::exception & e) = 0;

		/**
			获取最后一次查询的sql语句
		*/
		virtual const std::string& lastquery() const{ return lastquery_; }
	protected:

		char						db_type_[MAX_BUF];									// 数据库的类别
		uint32						db_port_;											// 数据库的端口
		char						db_ip_[MAX_IP];										// 数据库的ip地址
		char						db_username_[MAX_BUF];								// 数据库的用户名
		char						db_password_[MAX_BUF];								// 数据库的密码
		char						db_name_[MAX_BUF];									// 数据库名
		uint16						db_numConnections_;									// 数据库最大连接
		std::string					lastquery_;											// 最后一次查询描述
	};

	/*
		数据库操作单元
	*/
	class DBUtil : public Singleton<DBUtil>
	{
	public:
		DBUtil();
		~DBUtil();
	
		static bool					initialize();
		static void					finalise();

		static bool					initThread();
		static bool					finiThread();

		static DBInterface*			createInterface(bool showinfo = true);
		static const char*			dbname();
		static const char*			dbtype();
		static const char*			accountScriptName();
		static bool					initInterface(DBInterface* dbi);

		static thread::ThreadPool*  pThreadPool(){ return pThreadPool_; }
	private:
		static thread::ThreadPool*  pThreadPool_;
	};

}
