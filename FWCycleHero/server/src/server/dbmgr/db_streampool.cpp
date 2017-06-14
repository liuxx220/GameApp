/*
---------------------------------------------------------------------------------------------------------------------------
			file name : db_streampool.h
			desc	  : 
			author    : ljp

			log		  : by ljp create 2017-06-13
---------------------------------------------------------------------------------------------------------------------------
*/
#include "db_stream.h"
#include "db_streampool.h"
#include "db_streamqueue.h"





namespace KBEngine
{
	//-----------------------------------------------------------------------------------------------------------
	// 构造函数析构函数
	//-----------------------------------------------------------------------------------------------------------
	StreamPool::StreamPool(int32 nDefaultSize)
		: m_nDefaultSize(nDefaultSize), m_nTotalNum(0)
	{
		//ASSERT( m_nDefaultSize > 0 );

		RealAlloc(m_nDefaultSize);
	}

	StreamPool::~StreamPool()
	{
		RealFreeAll();
	}

	//------------------------------------------------------------------------------------------------------------
	// 真实分配内存
	//------------------------------------------------------------------------------------------------------------
	void StreamPool::RealAlloc(int32 nNum)
	{
		if( nNum <= 0 ) return;

		m_Mutex.lockMutex();
		for (int32 n = 0; n < nNum; n++)
		{
			MyStream* pStream = new MyStream();
			if( NULL == pStream ) break;

			m_FreeQueue.Add(pStream);
			InterlockedExchangeAdd((LPLONG)&m_nTotalNum, 1);
		}
		m_Mutex.unlockMutex();
	}

	//-------------------------------------------------------------------------------------------------------------
	// 真实释放内存
	//-------------------------------------------------------------------------------------------------------------
	void StreamPool::RealFree(int32 nNum)
	{
		if( nNum <= 0 ) return;

		m_Mutex.lockMutex();
		for (int32 n = 0; n < nNum; n++)
		{
			MyStream* pStream = m_FreeQueue.Get();
			if( pStream )
			{
				SAFE_DEL(pStream);
				InterlockedExchangeAdd((LPLONG)&m_nTotalNum, -1);
			}
		}
		m_Mutex.unlockMutex();
	}

	//-------------------------------------------------------------------------------------------------------------
	// 释放所有申请的内存
	//-------------------------------------------------------------------------------------------------------------
	void StreamPool::RealFreeAll()
	{
		while( m_nTotalNum != 0 )
		{
			MyStream* pStream = m_FreeQueue.Get();
			if( NULL == pStream ) continue;

			SAFE_DEL(pStream);
			InterlockedExchangeAdd((LPLONG)&m_nTotalNum, -1);
		}
	}

	//-------------------------------------------------------------------------------------------------------------
	// 从池中取出，若取不出，则扩充池的内容
	//--------------------------------------------------------------------------------------------------------------
	MyStream* StreamPool::AllocStream()
	{
		MyStream* pStream = m_FreeQueue.Get();

		while( NULL == pStream )
		{
			RealAlloc(m_nDefaultSize);
			pStream = m_FreeQueue.Get();
		}

		return pStream;
	}

	//---------------------------------------------------------------------------------------------------------------
	// 归还池内容
	//----------------------------------------------------------------------------------------------------------------
	void StreamPool::FreeStream(MyStream* pStream)
	{
		if( NULL == pStream ) return;

		m_FreeQueue.Add(pStream);

		// 申请了过多内存，则释放一部分
		int32 nSize = m_FreeQueue.Size();
		if( nSize >= 8 * m_nDefaultSize )
		{
			RealFree(nSize/4);
		}
	}

} // namespace Beton
