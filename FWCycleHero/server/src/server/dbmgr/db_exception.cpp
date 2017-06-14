/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_exception.h
		desc      : 访问数据异常处理
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#include "db_exception.h"
#include "db_interface_mysql.h"
#include <mysql/mysqld_error.h>
#include <mysql/errmsg.h>






namespace KBEngine { 

	//-------------------------------------------------------------------------------------
	DBException::DBException(DBInterface* dbi)
	{

	}

	//-------------------------------------------------------------------------------------
	DBException::~DBException() throw()
	{
	}

	//-------------------------------------------------------------------------------------
	bool DBException::shouldRetry() const
	{
		return (errNum_== ER_LOCK_DEADLOCK) ||
				(errNum_ == ER_LOCK_WAIT_TIMEOUT);
	}

	//-------------------------------------------------------------------------------------
	bool DBException::isLostConnection() const
	{
		return (errNum_ == CR_SERVER_GONE_ERROR) ||
				(errNum_ == CR_SERVER_LOST);
	}

	//-------------------------------------------------------------------------------------
	}
