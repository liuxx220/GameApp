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
1. Database对象与数据库一一对应，如果上层应用程序需要多个库，要建立多个Database对象
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
		virtual ~InterfaceMysql();

	private:
		InterfaceMysql(const InterfaceMysql&);
		InterfaceMysql& operator = (const InterfaceMysql&);



	protected:

	};


}
