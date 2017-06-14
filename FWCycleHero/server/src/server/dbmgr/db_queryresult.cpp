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
#include "db_queryresult.h"
#include "db_field.h"
#include "db_interface_mysql.h"






namespace KBEngine {

	//---------------------------------------------------------------------------------------------------
	// 构造函数
	//---------------------------------------------------------------------------------------------------
	QueryResult::QueryResult(MYSQL_RES* result, INT nRows, INT nCols, Connection* con)
							: m_Result(result), m_nRows(nRows), m_nCols(nCols), m_con(con)
	{
		KBE_ASSERT(m_Result != NULL && m_nRows >= 0 && m_nCols > 0);

		m_CurrentRow = new Field[m_nCols];
		KBE_ASSERT(m_CurrentRow != NULL);

		MYSQL_FIELD* field = mysql_fetch_fields(m_Result);
		for(INT n = 0; n < m_nCols; n++)
		{
			m_CurrentRow[n].SetALL(field[n].name, ConvertType(field[n].type), field[n].max_length);
		}
	}

	//-----------------------------------------------------------------------------------------------------
	// 析构函数
	//-----------------------------------------------------------------------------------------------------
	QueryResult::~QueryResult()
	{
		delete[] m_CurrentRow;
		if( m_Result )
			mysql_free_result(m_Result);
		//do {
		//	MYSQL_RES* pRes = mysql_store_result(m_con->m_Mysql);
		//	mysql_free_result( pRes );
		//}while( mysql_next_result(m_con->m_Mysql) );
	}

	//-----------------------------------------------------------------------------------------------------
	// 得到下一行
	// 注意：当生成QueryResult时，上层需要立刻调用一次NextRow，以便定位到结果集的第一行
	//-----------------------------------------------------------------------------------------------------
	bool QueryResult::NextRow()
	{
		MYSQL_ROW row = mysql_fetch_row(m_Result);
		if( NULL == row )
			return false;

		// 同时设置列值和列的长度
		ulong* uLen = mysql_fetch_lengths(m_Result);
		for(INT n = 0; n < m_nCols; n++)
		{
			m_CurrentRow[n].SetValue(row[n]);
			m_CurrentRow[n].SetLen(uLen[n]);
		}

		return true;
	}

	//-----------------------------------------------------------------------------------------------------
	// 转换mysql的内置类型为Field预定义的枚举类型
	//-----------------------------------------------------------------------------------------------------
	Field::DataTypes QueryResult::ConvertType(enum_field_types mysqlType) const
	{
		switch (mysqlType)
		{
		case FIELD_TYPE_TIMESTAMP:
		case FIELD_TYPE_DATE:
		case FIELD_TYPE_TIME:
		case FIELD_TYPE_DATETIME:
		case FIELD_TYPE_YEAR:
		case FIELD_TYPE_STRING:
		case FIELD_TYPE_VAR_STRING:
		case FIELD_TYPE_SET:
		case FIELD_TYPE_NULL:
			return Field::EDBT_STRING;

		case FIELD_TYPE_TINY:
		case FIELD_TYPE_SHORT:
		case FIELD_TYPE_LONG:
		case FIELD_TYPE_INT24:
		case FIELD_TYPE_LONGLONG:
		case FIELD_TYPE_ENUM:
			return Field::EDBT_INTEGER;

		case FIELD_TYPE_DECIMAL:
		case FIELD_TYPE_FLOAT:
		case FIELD_TYPE_DOUBLE:
			return Field::EDBT_FLOAT;
		
		case FIELD_TYPE_BLOB:
			return Field::EDBT_BLOB;

		default:
			return Field::EDBT_UNKNOWN;
		}
	}

}