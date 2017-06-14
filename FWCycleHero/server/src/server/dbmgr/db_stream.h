/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_stream.h
		desc      : 数据库操作流封装
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"







namespace KBEngine {

	struct MysqlConnection;
	class CBlobStreamAdapter;
	//----------------------------------------------------------------------------------------------------
	/**	MysqlStream类 - 非线程安全的MysqlStream类，用于存储转换和转义后的sql语句
	*/
	//----------------------------------------------------------------------------------------------------
	class MyStream
	{
	public:
		MyStream(int32 nDefaultSize=1024);
		virtual ~MyStream();

	private:
		MyStream(const MyStream&);
		MyStream& operator = (MyStream&);

	public:
		//--------------------------------------------------------------------------------------------------
		// 字符串及二进制字段单独处理
		//--------------------------------------------------------------------------------------------------
		MyStream& FillString(const char* p, MysqlConnection* con = NULL);
		MyStream& FillString(const WCHAR* p, MysqlConnection* con = NULL);
		MyStream& FillBlob(const void* p, int32 nSize, MysqlConnection* con);
		MyStream& FillBlob(void* p, int32 nSize);	// 调用该方法的二进制数据不需要转义

		//--------------------------------------------------------------------------------------------------
		// 流操作符重载
		//--------------------------------------------------------------------------------------------------
		inline MyStream& operator << (const int32 p);
		inline MyStream& operator << (const uint32 p);
		inline MyStream& operator << (const int64 p);
		inline MyStream& operator << (const double p);
		inline MyStream& operator << (const float p)		{ return operator << (static_cast<DOUBLE>(p)); }
		inline MyStream& operator << (const int16 p)		{ return operator << (static_cast<int16>(p)); }
		inline MyStream& operator << (const BYTE p)			{ return operator << (static_cast<BYTE>(p)); }
		inline MyStream& operator << (const char* p);
		inline MyStream& operator << (const WCHAR* p);
		inline MyStream& operator << (const CBlobStreamAdapter& blob);



	public:
		//--------------------------------------------------------------------------------------------------
		// 基本SQL语句操作
		//--------------------------------------------------------------------------------------------------
		inline MyStream& SetCopyItem(const char* szTab1, const char* szSeclect, const char* szTab2)
		{

			Clear();
			return (*this).FillString("insert into ").FillString(szTab1).FillString(" select ").FillString(szSeclect).FillString(" from ").FillString(szTab2);
		}

		inline MyStream& SetSelect(const char* szTab, const char* szSeclect)
		{

			Clear();
			return (*this).FillString("select ").FillString(szSeclect).FillString(" from ").FillString(szTab);
		}

		inline MyStream& SetUpdate(const char* szTab)
		{
			Clear();
			return (*this).FillString("update ").FillString(szTab).FillString(" set ");
		}

		inline MyStream& SetInsert(const char* szTab)
		{
			Clear();
			return (*this).FillString("insert into ").FillString(szTab).FillString(" set ");
		}

		inline MyStream& SetInsert(const char* szTab, const char* szPostfix)
		{
			Clear();
			return (*this).FillString("insert into ").FillString(szTab).FillString(szPostfix).FillString(" set ");
		}

		inline MyStream& SetReplace(const char* szTab)
		{
			Clear();
			return (*this).FillString("replace into ").FillString(szTab).FillString(" set ");
		}

		inline MyStream& SetDelete(const char* szTab)
		{
			Clear();
			return (*this).FillString("delete from ").FillString(szTab);
		}

		inline MyStream& SetWhere()
		{
			return (*this).FillString(" where ");
		}

		inline MyStream& SetLimit(INT nNum, INT nBegin = 0)
		{
			(*this).FillString(" limit ");
			if( nBegin > 0 )
			{
				(*this) << nBegin;
				(*this).FillString(",");
			}
			return (*this) << nNum;
		}

	public:
		inline void			Grow(int32 nSize);
		inline bool			Seek(int32 nPos);
		inline void			End()					{ m_pBuf[m_nPos] = '\0'; }
		inline const char*	GetBuf() const			{ return m_pBuf; }
		inline int32		GetBufLen() const		{ return m_nPos; }
		inline bool			IsEmpty() const			{ return m_nPos == 0; }
		inline void			Clear()					{ m_nPos = 0; End(); }

	private:
		char*				m_pBuf;				// 语句缓冲
		int32				m_nBufLen;			// 缓冲的大小
		int32				m_nPos;				// 当前输入所在的位置
		int32				m_nDefaultSize;		// 扩充的大小
	};

	class CBlobStreamAdapter
	{
		const void*	m_pData;
		size_t		m_nSize;
		MysqlConnection* m_pCon;
	public:
		CBlobStreamAdapter(const void* pBlobData, size_t nLength, MysqlConnection* pCon)
			: m_pData(pBlobData)
			, m_nSize(nLength)
			, m_pCon(pCon)
		{
		}

		const void* BlobData() const {
			return m_pData;
		}

		size_t Length() const {
			return m_nSize;
		}

		MysqlConnection* Con() const {
			return m_pCon;
		}

	};

}


