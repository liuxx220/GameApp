//��Name				:   QueryResult.h
//��Compiler			:	Microsoft Visual C++ 9.0
//��Version				:	1.00
//��Create Date			:	05/31/2009
//��LastModified		:	05/31/2009
//��Copyright (C)		:	
//��Writen  by			:   
//��Mode  by			:   
//��Brief				:	
//////////////////////////////////////////////////////////////////////////

#pragma once

#include "StdAfx.h"
#include "Config.h"
#include "Field.h"

namespace Beton {

//----------------------------------------------------------------------------------------------------
/**	QueryResult�� - �ṩ���ݿ���������ع��ܣ��Խ��������������ݺ����Խ��й���
	1. �ṩ����������ȡ����Ĺ��ܣ�ÿ���µ���ֵ����µ�����Field��
	2. �ṩ���������ַ��������еĹ���
	3. �ṩ��ѯ���ԵĹ��ܣ������������������ȵ�
	4. �ṩ�α�Ĺ��ܣ����Զ�λ����Ӧ�кͶ�Ӧ�У�������ɣ�
	
	ע������
	1. �ý����ֻ������ʹ��mysql_store_result()���صĽ����������mysql_use_reslut()�Ľ�������ڻ����ṩ֧��
	2. ����mysql_use_resultʹ�õĽ������һ�㲻�Ƽ�ʹ�ã��������ṩ�ù��ܵļ���
*/
//----------------------------------------------------------------------------------------------------
class Field;
struct Connection;

class BETON_API QueryResult
{
public:
	QueryResult(MYSQL_RES* result, INT nRows, INT nCols, Connection* con);
	~QueryResult();

	BETON_INLINE INT GetRowCount() const		{ return m_nRows; }
	BETON_INLINE INT GetFieldCount() const		{ return m_nCols; }
	BETON_INLINE const Field* Fetch() const		{ return m_CurrentRow; } 

	BOOL NextRow();
	BETON_INLINE Field& operator [] (INT nIndex) const { ASSERT(nIndex >= 0 && nIndex < m_nCols && m_CurrentRow != NULL); return m_CurrentRow[nIndex]; }
	BETON_INLINE Field& operator [] (char* szFieldName) const;

private:
	Field::DataTypes ConvertType(enum_field_types mysqlType) const;
	
private:
	MYSQL_RES*		m_Result;			// mysql�����
	INT				m_nRows;			// ���������
	INT				m_nCols;			// ���������
	Field*			m_CurrentRow;		// ��ǰ���м�
	Connection*		m_con;

};

} // namespace Beton