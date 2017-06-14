/*
----------------------------------------------------------------------------------------------------------------------------
		file name : db_exception.h
		desc      : 数据可字段的封装
		author    : ljp

		log		  : by ljp create 2017-06-13
----------------------------------------------------------------------------------------------------------------------------
*/
#include "db_field.h"
#include <string>






namespace KBEngine {

	//----------------------------------------------------------------------------------------------------
	// 得到TCHAR字符串
	// 函数会根据传进的nLen的值来进行相应动作，如果nLen为0，且转换成功，则返回值为需要外部提供的存储空间
	// 的字符数；如果nLen不为0，则函数进行字符转换，如果转换成功，则返回转换成功的字符数，否则返回0
	//----------------------------------------------------------------------------------------------------
	int32 Field::GetTCHAR(TCHAR* szValue, int32 nLen) const
	{
		if( NULL == szValue || NULL == m_szValue || EDBT_BLOB == m_eType )
			return 0;

	#if defined _UNICODE

		return MultiByteToWideChar(CP_UTF8, 0, m_szValue, -1, szValue, nLen);

	#else

		if( nLen < m_uLen + 1 ) return 0;
		memcpy(szValue, m_szValue, m_uLen+1);
		return m_uLen+1;

	#endif
	}

	//------------------------------------------------------------------------------------------------------
	// 得到sstring字符串
	// 如果当前设置UNICODE，函数内部进行转换，将转后后的字符串写入sstring中，否则直接拷贝
	//------------------------------------------------------------------------------------------------------
	std::string Field::GetUnicodeString() const
	{
		if( NULL == m_szValue || EDBT_BLOB == m_eType || EDBT_UNKNOWN == m_eType )
			return std::string("");

	#if defined _UNICODE

		INT nDesiredLen = MultiByteToWideChar(CP_UTF8, 0, m_szValue, -1, NULL, 0);
		if (nDesiredLen == 0) return std::string("");

		TCHAR* szResultValue = new TCHAR[nDesiredLen];
		if( 0 == MultiByteToWideChar(CP_UTF8, 0, m_szValue, -1, szResultValue, nDesiredLen) )
		{
			delete []szResultValue;
			return  std::string("");
		}

		//std::string str(szResultValue);
		std::string str("");
		delete[] szResultValue;
		return str;

	#else

		return sstring(m_szValue);

	#endif

	}

	//--------------------------------------------------------------------------------------------------------
	// 得到blog字段的值，返回拷贝的字节数，如果长度不够，则返回0
	//--------------------------------------------------------------------------------------------------------
	bool Field::GetBlob(void* buffer, ulong uLen) const
	{
		if( uLen < m_uLen ) return false;

		memcpy(buffer, m_szValue, m_uLen);
		return true;
	}

} 