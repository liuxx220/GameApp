/*
---------------------------------------------------------------------------------------------------------------------------
file name : db_streamqueue.h
desc	  :
author    : ljp

log		  : by ljp create 2017-06-13
---------------------------------------------------------------------------------------------------------------------------
*/
#include "db_stream.h"
#include "db_streamqueue.h"





namespace KBEngine
{
	//--------------------------------------------------------------------------------------------------------
	// ��������
	//--------------------------------------------------------------------------------------------------------
	SafeStreamQueue::~SafeStreamQueue()
	{
		// ASSERT( 0 == m_lNum );
		if( m_hEvent )
			::CloseHandle(m_hEvent);
	}

} // namespace Beton