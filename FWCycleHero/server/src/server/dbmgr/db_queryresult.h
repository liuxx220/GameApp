/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_queryresult.h
		desc      : 查询结果封装
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"
#include "helper/debug_helper.hpp"
#include "db_field.h"
#include "mysql/mysql.h"







namespace KBEngine {

	//----------------------------------------------------------------------------------------------------
	/**	QueryResult类 - 提供数据库结果集的相关功能，对结果集所保存的数据和属性进行管理
		1. 提供按行依序提取结果的功能，每行新的列值会更新到各个Field中
		2. 提供按索引和字符串检索列的功能
		3. 提供查询属性的功能，最大行数，最大列数等等
		4. 提供游标的功能，可以定位到对应行和对应列（后续完成）
	
		注意事项
		1. 该结果集只适用于使用mysql_store_result()返回的结果集，对于mysql_use_reslut()的结果集现在还不提供支持
		2. 对于mysql_use_result使用的结果集，一般不推荐使用，后续会提供该功能的简单类
	*/
	//----------------------------------------------------------------------------------------------------
	class Field;
	struct Connection;
	class QueryResult
	{
	public:
		QueryResult(MYSQL_RES* result, INT nRows, INT nCols, Connection* con);
		~QueryResult();

		inline INT				GetRowCount() const		{ return m_nRows; }
		inline INT				GetFieldCount() const		{ return m_nCols; }
		inline const Field*		Fetch() const		{ return m_CurrentRow; }

		bool					NextRow();
		inline Field&			operator [] (INT nIndex) const { KBE_ASSERT(nIndex >= 0 && nIndex < m_nCols && m_CurrentRow != NULL); return m_CurrentRow[nIndex]; }
		inline Field&			operator [] (char* szFieldName) const;

	private:
		Field::DataTypes ConvertType(enum_field_types mysqlType) const;
	
	private:
		MYSQL_RES*		m_Result;			// mysql结果集
		INT				m_nRows;			// 结果集行数
		INT				m_nCols;			// 结果集列数
		Field*			m_CurrentRow;		// 当前行列集
		Connection*		m_con;

	};

}