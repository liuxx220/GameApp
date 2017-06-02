//-----------------------------------------------------------------------------
// File: safe_mem_pool
// Desc: game tool mem pool 2.0
// Auth:
// Date: 2009-1-8	
// Last: 2009-3-18
//-----------------------------------------------------------------------------
#pragma once
#include "common/common.hpp"





namespace KBEngine {
//-----------------------------------------------------------------------------
// 内存池(注意：内存池的大小表示:内存池当前空闲内存的大小)
// 初始化时：
// 1 外部设定内存池的最大允许大小
// 2 内存池不做任何内存预分配
//
// 外部申请内存时,依次判断：
// 1 如果内存池有对应尺寸的空闲块,使用空闲块返回
// 2 没有空闲块时,直接申请内存并在内存前加入tagMemItem,然后返回
//
// 外部释放内存时,依次判断:
// 1 如果释放内存块大小>内存池最大大小,直接释放到系统内存
// 2 如果当前内存池大小+释放内存块大小<内存池最大大小,直接放入内存池
// 3 进行垃圾收集：垃圾收集后重新检查上面的第2步，如果不通过，则进行第4步
// 4 否则直接释放入系统内存
//-----------------------------------------------------------------------------
class SafeMemPool
{
public:
	 void*					Alloc(UINT32 dwBytes);
	 void					Free(void* pMem);
	 void*					TryAlloc(UINT32 dwBytes);	// 尝试进入锁定区域
	 bool					TryFree(void* pMem);		// 尝试进入锁定区域
	 void					SetMaxSize(UINT32 dwSize) { m_dwMaxSize = dwSize; }
	 UINT32					GetSize() { return m_dwCurrentSize; }
	 UINT32					GetMalloc() { return m_dwMalloc; }
	 UINT32					GetGC() { return m_dwGCTimes; }

	 SafeMemPool(UINT32 dwMaxPoolSize = 16 * 1024 * 1024);
	~SafeMemPool();

private:
	struct tagNode	// 内存块头描述
	{
		tagNode*	pNext;
		tagNode*	pPrev;
		INT			nIndex;
		UINT32		dwSize;
		UINT32		dwUseTime;
		UINT32		pMem[1];	// 实际内存空间
	};

	struct
	{
		tagNode*	pFirst;
		tagNode*	pLast;
	} m_Pool[16];

	UINT32			m_dwMaxSize;		// 外部设定的最大允许空闲内存
	UINT32			m_dwMalloc;			// 统计用，实际Malloc次数
	UINT32			m_dwGCTimes;		// 统计用，实际垃圾收集次数

	UINT32 volatile m_dwCurrentSize;	// 内存池中空闲内存总数
	LONG volatile 	m_lLock;

	void Lock()		{ while (InterlockedCompareExchange((LPLONG)&m_lLock, 1, 0) != 0) Sleep(0); }
	void Unlock()	{ InterlockedExchange((LPLONG)(&m_lLock), 0); }
	bool TryLock()	{ return InterlockedCompareExchange((LPLONG)(&m_lLock), 1, 0) == 0;	}

	// 垃圾收集
	void			GarbageCollect(UINT32 dwExpectSize, UINT32 dwUseTime);
	// 返回最匹配的大小
	INT32				SizeToIndex(UINT32 dwSize, UINT32& dwRealSize);
};
} 
