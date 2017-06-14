/*
This source file is part of KBEngine
For the latest info, see http://www.kbengine.org/
/*
---------------------------------------------------------------------------------------------------------------------------
			file name : db_interface_mysql.h
			desc	  : 连接和操作mysql数据的封装
			author    : ljp

			log		  : by ljp create 2017-06-13
---------------------------------------------------------------------------------------------------------------------------
*/

#include "db_interface_mysql.h"
#include "db_exception.h"
#include "thread/threadguard.hpp"
#include "server/serverconfig.hpp"






namespace KBEngine { 



	//-------------------------------------------------------------------------------------
	MysqlConnection::MysqlConnection()
	{
		m_Mysql = 0;
	}

	//----------------------------------------------------------------------------------------------------------------------------
	// constructor
	//----------------------------------------------------------------------------------------------------------------------------
	InterfaceMysql::InterfaceMysql() : m_SqlConn(0), m_bConLost(true)
	{

	}

	//----------------------------------------------------------------------------------------------------------------------------
	// destructor
	//----------------------------------------------------------------------------------------------------------------------------
	InterfaceMysql::~InterfaceMysql()
	{
		ShutDown();
	}

	//----------------------------------------------------------------------------------------------------------------------------
	// 初始化函数
	//----------------------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::Init(const char* strHost, const char* strUser, const char* strPassword, const char* strDatabase, int32 nPort, int32 nConNum/*=2*/)
	{
		if (nConNum <= 1) return FALSE;

		return Start();
	}

	//---------------------------------------------------------------------------------------------------------------
	// 重连
	//---------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::Reconnect()
	{
		ShutDown();		// 先全体关闭

		return Start();	// 开启
	}

	//---------------------------------------------------------------------------------------------------------------
	// 关闭
	//---------------------------------------------------------------------------------------------------------------
	void InterfaceMysql::ShutDown()
	{
		// 关闭所有连接
		if (P_VALID(m_SqlConn))
		{
			for (INT n = 0; n < m_nConNum; n++)
			{
				if (m_SqlConn[n].m_Mysql)
					mysql_close(m_SqlConn[n].m_Mysql);
			}
			SAFE_DEL_ARRAY(m_SqlConn);
		}
	}


	//---------------------------------------------------------------------------------------------------------------
	// 启动
	//---------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::Start()
	{
		// 建立连接
		ENGINE_COMPONENT_INFO& dbcfg = g_kbeSrvConfig.getDBMgr();
		m_SqlConn = new MysqlConnection[m_nConNum];
		for (INT n = 0; n < m_nConNum; n++)
		{
			MYSQL* temp = mysql_init(NULL);
			if (mysql_options(temp, MYSQL_SET_CHARSET_NAME, "utf8"))
			{
				// 加入log
			}

			my_bool my_true = true;
			if (mysql_options(temp, MYSQL_OPT_RECONNECT, &my_true))
			{
				// 加入log
			}

			if (NULL == mysql_real_connect(temp, m_Hostname.c_str(), m_Username.c_str(), m_Password.c_str(), m_DatabaseName.c_str(), m_nPort, NULL, 0))
			{
				// 写入日志
				return false;
			}

			m_SqlConn[n].m_Mysql = temp;
		}

		// 设置连接成功
		m_bConLost = false;
		return true;
	}

	//-----------------------------------------------------------------------------------------------------------------
	// 得到一个空闲的连接，对于外部手动获得的连接，在操作完后，必须调用ReturnConnection释放连接
	//-----------------------------------------------------------------------------------------------------------------
	MysqlConnection* InterfaceMysql::GetFreeConnection()
	{
		DWORD n = 0;
		while (TRUE)
		{
			MysqlConnection* con = &m_SqlConn[((n++) % m_nConNum)];
			if (con->m_mutex.TryAcquire())
				return con;
		}

		// 不可能到这里
		return NULL;
	}

	//------------------------------------------------------------------------------------------------------------------
	// 使用完后释放一个连接
	//------------------------------------------------------------------------------------------------------------------
	void InterfaceMysql::ReturnConnection(MysqlConnection* con)
	{
		if (NULL != con)
			con->m_mutex.Release();
	}

	//-------------------------------------------------------------------------------------------------------------------
	// 不指定连接的阻塞时查询操作
	//-------------------------------------------------------------------------------------------------------------------
	QueryResult* InterfaceMysql::Query(const char* szStr)
	{
		if (NULL == szStr) return NULL;

		// 任意获得一条连接
		MysqlConnection* con = GetFreeConnection();

		QueryResult* pRet = NULL;
		if (SendQuery(con, szStr, strlen(szStr)))
		{
			pRet = StoreQueryResult(con);
		}

		ReturnConnection(con);
		return pRet;
	}

	QueryResult* InterfaceMysql::Query(const MyStream* pStream)
	{
		if (NULL == pStream || NULL == pStream->GetBuf() || 0 == pStream->GetBufLen())
			return NULL;

		// 任意获得一条连接
		MysqlConnection* con = GetFreeConnection();

		QueryResult* pRet = NULL;
		if (SendQuery(con, pStream->GetBuf(), pStream->GetBufLen()))
		{
			pRet = StoreQueryResult(con);
		}

		ReturnConnection(con);
		return pRet;
	}

	//------------------------------------------------------------------------------------------------------------------------
	// 指定连接的阻塞式查询操作
	//------------------------------------------------------------------------------------------------------------------------
	QueryResult* InterfaceMysql::WaitQuery(const char* szStr, MysqlConnection* con)
	{
		if (NULL == szStr || NULL == con) return NULL;

		QueryResult* pRet = NULL;
		if (SendQuery(con, szStr, strlen(szStr)))
		{
			pRet = StoreQueryResult(con);
		}

		return pRet;
	}

	QueryResult* InterfaceMysql::WaitQuery(const MyStream* pStream, MysqlConnection* con)
	{
		if (NULL == pStream || NULL == pStream->GetBuf() || 0 == pStream->GetBufLen() || NULL == con)
			return NULL;

		QueryResult* pRet = NULL;
		if (SendQuery(con, pStream->GetBuf(), pStream->GetBufLen()))
		{
			pRet = StoreQueryResult(con);
		}

		return pRet;
	}

	//-----------------------------------------------------------------------------------------------------------
	// 不指定连接的阻塞式数据库执行操作
	//-----------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::Execute(const char* szStr)
	{
		if (NULL == szStr) return FALSE;

		// 任意获得一条连接
		MysqlConnection* con = GetFreeConnection();
		BOOL bRet = SendQuery(con, szStr, strlen(szStr));
		ReturnConnection(con);
		return bRet;
	}

	bool InterfaceMysql::Execute(const MyStream* pStream)
	{
		if (NULL == pStream || NULL == pStream->GetBuf() || 0 == pStream->GetBufLen())
			return FALSE;

		// 任意获得一条连接
		MysqlConnection* con = GetFreeConnection();
		BOOL bRet = SendQuery(con, pStream->GetBuf(), pStream->GetBufLen());
		ReturnConnection(con);
		return bRet;
	}

	//-------------------------------------------------------------------------------------------------------------
	// 指定连接的阻塞式数据库执行操作
	//--------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::WaitExecute(const char* szStr, MysqlConnection* con)
	{
		if (NULL == szStr || NULL == con) 
			return false;

		return SendQuery(con, szStr, strlen(szStr));
	}

	bool InterfaceMysql::WaitExecute(const MyStream* pStream, MysqlConnection* con)
	{
		if (NULL == pStream || NULL == pStream->GetBuf() || 0 == pStream->GetBufLen() || NULL == con)
			return FALSE;

		return SendQuery(con, pStream->GetBuf(), pStream->GetBufLen());
	}

	//---------------------------------------------------------------------------------------------------------------
	// 不指定连接的阻塞式数据库检测执行操作，如果成功，返回影响的记录个数，否则返回-1
	//---------------------------------------------------------------------------------------------------------------
	int32 InterfaceMysql::CheckExecute(const char* szStr)
	{
		if (NULL == szStr) return GT_INVALID;

		// 任意获得一条连接
		MysqlConnection* con = GetFreeConnection();
		bool bRet = SendQuery(con, szStr, strlen(szStr));
		ReturnConnection(con);

		return (bRet ? (INT)mysql_affected_rows(con->m_Mysql) : GT_INVALID);
	}

	int32 InterfaceMysql::CheckExecute(const MyStream* pStream)
	{
		if (NULL == pStream || NULL == pStream->GetBuf() || 0 == pStream->GetBufLen())
			return GT_INVALID;

		// 任意获得一条连接
		MysqlConnection* con = GetFreeConnection();
		bool bRet = SendQuery(con, pStream->GetBuf(), pStream->GetBufLen());
		ReturnConnection(con);

		return (bRet ? (int32)mysql_affected_rows(con->m_Mysql) : GT_INVALID);
	}

	//---------------------------------------------------------------------------------------------------------------
	// 指定连接的阻塞式数据库检测执行操作，如果成功，返回影响的记录个数，否则返回-1
	//---------------------------------------------------------------------------------------------------------------
	int32 InterfaceMysql::CheckWaitExecute(const char* szStr, MysqlConnection* con)
	{
		if (NULL == szStr || NULL == con) return GT_INVALID;

		bool bRet = SendQuery(con, szStr, strlen(szStr));

		return (bRet ? (INT)mysql_affected_rows(con->m_Mysql) : GT_INVALID);
	}

	int32 InterfaceMysql::CheckWaitExecute(const MyStream *pStream, MysqlConnection* con)
	{
		if (NULL == pStream || NULL == pStream->GetBuf() || 0 == pStream->GetBufLen() || NULL == con)
			return GT_INVALID;

		bool bRet = SendQuery(con, pStream->GetBuf(), pStream->GetBufLen());

		return (bRet ? (INT)mysql_affected_rows(con->m_Mysql) : GT_INVALID);
	}


	//---------------------------------------------------------------------------------------------------------------
	// 事务相关操作
	//---------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::BeginTransaction(MysqlConnection* con)
	{
		return WaitExecute("START TRANSACTION", con);
	}

	bool InterfaceMysql::EndTransaction(MysqlConnection* con)
	{
		return WaitExecute("COMMIT", con);
	}

	bool InterfaceMysql::RollBack(MysqlConnection* con)
	{
		return WaitExecute("ROLLBACK", con);
	}

	//------------------------------------------------------------------------------------------------------------------
	// 正式发送查询语句
	//------------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::SendQuery(MysqlConnection* con, const char* szSql, INT nLen, bool bSelf/* =FALSE */)
	{
		static char szBuf[1024 * 4] = { 0, };
		INT nResult = mysql_real_query(con->m_Mysql, szSql, nLen);

		sprintf_s(szBuf, 1024 * 4, "%d -- %s\n", nResult, szSql);
		// OutputDebugStringA( szBuf );
		if (nResult != 0)
		{
			if (bSelf == FALSE && processException(mysql_errno(con->m_Mysql), szSql))
			{
				// 如果因为连接丢失
				return SendQuery(con, szSql, nLen, true);
			}
			else
			{
				// 可能是连接丢失了，这里需要调用上层的回调函数来通知上层
				const char* pErrMsg = mysql_error(con->m_Mysql);
				pErrMsg = pErrMsg;
			}
		}

		return (nResult == 0 ? true : false);
	}

	QueryResult* InterfaceMysql::NextResult(MysqlConnection* con)
	{
		if (!mysql_next_result(con->m_Mysql))
		{
			QueryResult* pResult = StoreQueryResult(con);
			return pResult;
		}
		return NULL;
	}


	//-------------------------------------------------------------------------------------------------------------------
	// 得到查询结果，只针对数据库的读操作，对数据库的写操作，该函数始终返回NULL
	//-------------------------------------------------------------------------------------------------------------------
	QueryResult* InterfaceMysql::StoreQueryResult(MysqlConnection* con)
	{
		if (NULL == con) return NULL;

		QueryResult* pResult = NULL;

		MYSQL_RES* pRes = mysql_store_result(con->m_Mysql);
		int32 nRows		= (int32)mysql_affected_rows(con->m_Mysql);
		int32 nFields	= (int32)mysql_field_count(con->m_Mysql);

		if (0 == nFields || NULL == pRes)
		{
			if (pRes != NULL)
				mysql_free_result(pRes);

			return NULL;
		}

		pResult = new QueryResult(pRes, nRows, nFields, con);

		pResult->NextRow();

		return pResult;
	}


	//----------------------------------------------------------------------------------------------------------------------
	// 重连机制
	//----------------------------------------------------------------------------------------------------------------------
	bool InterfaceMysql::Reconnect(MysqlConnection* con)
	{
		if (NULL == con) return FALSE;

		MYSQL* temp = mysql_init(NULL);
		my_bool my_true = true;
		mysql_options(temp, MYSQL_SET_CHARSET_NAME, "utf8");
		mysql_options(temp, MYSQL_OPT_RECONNECT, &my_true);

		if (mysql_real_connect(temp, m_Hostname.c_str(), m_Username.c_str(), m_Password.c_str(), m_DatabaseName.c_str(), m_nPort, NULL, 0))
		{
			mysql_close(temp);

			// 设置连接断开
			m_bConLost = true;
			return false;
		}

		if (con->m_Mysql != NULL)
			mysql_close(con->m_Mysql);

		con->m_Mysql = temp;
		return true;
	}

}
