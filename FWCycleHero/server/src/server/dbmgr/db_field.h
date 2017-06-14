/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_exception.h
		desc      : 数据可字段的封装
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"






namespace KBEngine {

	//----------------------------------------------------------------------------------------------------
	/**	Filed类 - 提供数据库结构集中当前某一行某一列的当前属性
		1. 所有列的当前值均由char*存储，并提供按特定类型提取数据的功能，对BOLB字段做单独处理
		2. 提供外部查询的列属性，列最大程度，当前行当前列的长度，是否为NULL，字段类型等等
		3. 对Unicode字符串提供支持，只允许数据表中存储utf8类型的字符串，所以m_szValue用char*表示
	*/
	//----------------------------------------------------------------------------------------------------
	class Field
	{
	public:
		typedef enum						// 类型枚举
		{
			EDBT_UNKNOWN	= 0,			// 未知
			EDBT_INTEGER	= 1,			// 整型
			EDBT_STRING		= 2,			// 字符串型
			EDBT_FLOAT		= 3,			// 浮点型
			EDBT_BLOB		= 4				// 二进制类型
		} DataTypes;

	public:
		Field();
		Field(char* szValue, char* szName, ulong uLen, ulong uMaxLen, DataTypes eType);
		Field(Field& field);
	
		~Field() {}

		inline void			SetValue(char* szValue)			{ m_szValue = szValue; }
		inline void			SetName(char* szName)			{ m_szName = szName; }
		inline void			SetType(DataTypes eType)		{ m_eType = eType; }
		inline void			SetLen(ulong uLen)				{ m_uLen = uLen; }
		inline void			SetMaxLen(ulong uMaxLen)		{ m_uMaxLen = uMaxLen; }
		inline void			SetALL(char* szName, DataTypes eType, ulong uMaxLen)
		{
			SetName(szName); SetType(eType); SetMaxLen(uMaxLen);
		}

		inline bool			IsNull() const					{ return m_szValue == NULL; }

		inline const char*	GetName()	const				{ return m_szName; }
		inline DataTypes	GetType() const					{ return m_eType; }
		inline ulong GetLen() const							{ return m_uLen; }
		inline ulong GetMaxLen() const						{ return m_uMaxLen; }

		inline const char*	GetString() const 				{ return m_szValue; }
		inline bool			GetBool()	const				{ return m_szValue ? atol(m_szValue) : 0; }
		inline int32		GetInt() const					{ return m_szValue ? static_cast<int32>(atoi(m_szValue)) : 0; }
		inline uint32		GetDword() const				{ return m_szValue ? static_cast<uint32>(_atoi64(m_szValue)) : 0; }
		inline int64		GetLong() const					{ return m_szValue ? static_cast<long64>(_atoi64(m_szValue)) : 0; }
		inline BYTE			GetByte() const					{ return m_szValue ? static_cast<BYTE>(_atoi64(m_szValue)) : 0; }
		inline int16		GetShort() const				{ return m_szValue ? static_cast<int16>(_atoi64(m_szValue)) : 0; }
		inline float		GetFloat() const				{ return m_szValue ? static_cast<float>(atof(m_szValue)) : 0.0f; }
		inline double		GetDouble() const				{ return m_szValue ? static_cast<double>(atof(m_szValue)) : 0.0; }

		int32				GetTCHAR(TCHAR* szValue, int32 nLen) const;
		bool				GetBlob(void* buffer, ulong nLen) const;
		std::string			GetUnicodeString() const;

	private:
		char*				m_szValue;				// 该列的值
		char*				m_szName;				// 该列的名称
		uint32				m_dwNameCrc;			// 该列的名称CRC
		DataTypes			m_eType;				// 字段类型
		ulong				m_uLen;					// 当前某行该列的长度，主要为blog字段使用
		ulong				m_uMaxLen;				// 该列的最大程度
	};


	//------------------------------------------------------------------------------------------------------
	// 构造函数
	//------------------------------------------------------------------------------------------------------
	inline Field::Field() 
	: m_szValue(NULL), m_szName(NULL), m_eType(EDBT_UNKNOWN), m_uLen(0), m_uMaxLen(0)
	{

	}

	inline Field::Field(Field& field) 
	:m_szValue(field.m_szValue), m_szName(field.m_szName), m_eType(field.m_eType), m_uLen(field.m_uLen), m_uMaxLen(field.m_uMaxLen)
	{

	}

	inline Field::Field(char* szValue, char* szName, ulong uLen, ulong uMaxLen, DataTypes eType)
	: m_szValue(szValue), m_szName(szName), m_eType(eType), m_uLen(uLen), m_uMaxLen(uMaxLen)
	{

	}

} // namespace Beton

