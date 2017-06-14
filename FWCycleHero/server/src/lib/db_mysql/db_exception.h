/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_exception.h
		desc      : 访问数据异常处理
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#pragma once

#include <string>

namespace KBEngine { 

	class DBInterface;
	class DBException : public std::exception
	{
	public:
		DBException(DBInterface* dbi);
		~DBException() throw();

		virtual const char * what() const throw() { return errStr_.c_str(); }

		bool shouldRetry() const;
		bool isLostConnection() const;

	private:
		std::string errStr_;
		unsigned int errNum_;
	};

}



