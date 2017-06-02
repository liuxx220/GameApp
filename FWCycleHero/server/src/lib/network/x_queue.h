//-----------------------------------------------------------------------------
// File: x_queue.h
// Desc: 提供线程安全的先进先出unit队列管理
// Auth:
// Date: 2003-11-30
// Last: 2009-06-11
//-----------------------------------------------------------------------------
#pragma once
#include "common/common.hpp"








namespace KBEngine 
{
//-----------------------------------------------------------------------------
// XQueue: 提供线程安全的先进先出指针队列管理
//-----------------------------------------------------------------------------
template<typename ITEM_POINTER> class XQueue
{
public:
	inline bool Add(ITEM_POINTER pItem)	// 添加到队列尾
	{
		Lock();
		if( 0 == m_lItemNum )
			m_pFirst = pItem;
		else
			m_pLast->pNext = pItem;
		m_pLast = pItem;
		++m_lItemNum;
		Unlock();
		return TRUE;
	}

	inline ITEM_POINTER Get()	// 从队列头取item，外边需负责释放
	{
		if( m_lItemNum <= 0 )	// 先测试再进critical section
			return NULL;
		Lock();
		if( m_lItemNum <= 0 )
		{
			Unlock();
			return NULL;
		}
		ITEM_POINTER pItem = m_pFirst;	// 取出ITEM_POINTER
		m_pFirst = m_pFirst->pNext;
		--m_lItemNum;
		Unlock();
		return pItem;
	}

	inline bool AddFront(ITEM_POINTER pItem)	// 添加到队列首
	{
		Lock();
		if( 0 == m_lItemNum )
			m_pLast = pItem;
		else
			pItem->pNext = m_pFirst;
		m_pFirst = pItem;
		++m_lItemNum;
		Unlock();
		return TRUE;
	}
	
	inline LONG GetNum() { return m_lItemNum; }	// 得到队列中的items数目
	inline XQueue() :m_lItemNum(0), m_pFirst(0), m_pLast(0), m_lLock(0) {}
	inline ~XQueue() { }//ASSERT( 0 == m_lItemNum ); }

private:
	LONG volatile		m_lLock;
	LONG volatile		m_lItemNum;
	ITEM_POINTER		m_pFirst;
	ITEM_POINTER		m_pLast;

	inline void			Lock()		{ while (InterlockedCompareExchange((LPLONG)&m_lLock, 1, 0) != 0) Sleep(0); }
	inline void			Unlock()	{ InterlockedExchange((LPLONG)(&m_lLock), 0); }
};
}
