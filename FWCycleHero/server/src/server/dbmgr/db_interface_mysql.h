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
#include "db_stream.h"
#include "db_queryresult.h"
#include "db_streampool.h"
#include "db_streamqueue.h"
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

		//---------------------------------------------------------------------------
		// 初始化及结束
		//---------------------------------------------------------------------------
		bool				Init(const char* strHost, const char* strUser, const char* strPassword, const char* strDatabase, int32 nPort, int32 nConNum = 2);
		void				ShutDown();

		//---------------------------------------------------------------------------
		// 连接相关
		//---------------------------------------------------------------------------
		INLINE MysqlConnection* GetFreeConnection();
		INLINE void			ReturnConnection(MysqlConnection* con);

		//---------------------------------------------------------------------------
		// 查看连接丢失及重连
		//---------------------------------------------------------------------------
		bool				Reconnect();
		INLINE bool			IsConnLost()		{ return m_bConLost; }

		//---------------------------------------------------------------------------
		// Stream相关
		//---------------------------------------------------------------------------
		INLINE MyStream*	GetStream() { return m_StreamPool.AllocStream(); }
		INLINE void			ReturnStream(MyStream* pStream) { m_StreamPool.FreeStream(pStream); }

		//----------------------------------------------------------------------------
		// 查询相关
		//----------------------------------------------------------------------------
		INLINE QueryResult* Query(const char* szStr);
		INLINE QueryResult* Query(const MyStream* pStream);
		INLINE QueryResult* WaitQuery(const char* szStr, MysqlConnection* con);
		INLINE QueryResult* WaitQuery(const MyStream* pStream, MysqlConnection* con);

		//-----------------------------------------------------------------------------
		// 操作相关
		//-----------------------------------------------------------------------------
		INLINE bool			Execute(const char* szStr);
		INLINE bool			Execute(const MyStream* pStream);
		INLINE bool			WaitExecute(const char* szStr, MysqlConnection* con);
		INLINE bool			WaitExecute(const MyStream* pStream, MysqlConnection* con);

		//-----------------------------------------------------------------------------
		// 检测操作相关
		//-----------------------------------------------------------------------------
		INLINE int32		CheckExecute(const char* szStr);
		INLINE int32		CheckExecute(const MyStream* pStream);
		INLINE int32		CheckWaitExecute(const char* szStr, MysqlConnection* con);
		INLINE int32		CheckWaitExecute(const MyStream* pStream, MysqlConnection* con);

		//------------------------------------------------------------------------------
		// 记录集相关
		//------------------------------------------------------------------------------
		INLINE void			FreeQueryResult(QueryResult* pRet) { SAFE_DEL(pRet); }

		//------------------------------------------------------------------------------
		// 异步操作相关
		//------------------------------------------------------------------------------
		INLINE void			AddQuery(MyStream* pStream) { m_AsynStreamQueue.Add(pStream); }

		//------------------------------------------------------------------------------
		// 事物相关
		//------------------------------------------------------------------------------
		INLINE bool			BeginTransaction(MysqlConnection* con);
		INLINE bool			EndTransaction(MysqlConnection* con);
		INLINE bool			RollBack(MysqlConnection* con);

		INLINE QueryResult* NextResult(MysqlConnection* con);
		INLINE QueryResult* StoreQueryResult(MysqlConnection* con);
	private:
		InterfaceMysql(const InterfaceMysql&);
		InterfaceMysql& operator = (const InterfaceMysql&);

	private:
		bool				Start();

		INLINE bool			Reconnect(MysqlConnection* con);
		INLINE bool			SendQuery(MysqlConnection* con, const char* szSql, INT nLen, bool bSelf = FALSE);
	protected:

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
		
		MysqlConnection*	m_SqlConn;
		bool				m_bConLost;
		StreamPool			m_StreamPool;
		SafeStreamQueue		m_AsynStreamQueue;

		std::string			m_Hostname;				// mysql服务器主机
		std::string			m_Username;				// 用户名
		std::string			m_Password;				// 密码
		std::string			m_DatabaseName;			// 数据库名
		int32				m_nPort;				// mysql服务器端口号
		int32				m_nConNum;
	};


}
