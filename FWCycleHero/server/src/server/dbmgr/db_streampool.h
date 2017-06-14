/*
---------------------------------------------------------------------------------------------------------------------------
			file name : db_streampool.h
			desc	  : 
			author    : ljp

			log		  : by ljp create 2017-06-13
---------------------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"
#include "db_stream.h"
#include "db_streamqueue.h"
#include "thread/threadmutex.hpp"







namespace KBEngine {

	class MyStream;
	class SafeStreamQueue;
	//----------------------------------------------------------------------------------------------------
	/**	StreamPool类 - 为上层提供MysqlStream池，供上层提取使用
	1. 预先分配一定大小的资源，如不足可动态扩充，如分配过多，可在不需要时释放多余内存
	2. 线程安全
	*/
	//-----------------------------------------------------------------------------------------------------
	class StreamPool
	{
	public:
		StreamPool(int32 nDefaultSize=1000);
		virtual ~StreamPool();

	private:
		StreamPool(const StreamPool&);
		StreamPool& operator = (const StreamPool&);

	public:
		inline MyStream*	AllocStream();
		inline void			FreeStream(MyStream* pStream);

		inline long			GetAvailableStreamNum() { return m_FreeQueue.Size(); }
		inline int32		GetAllStreamNum() { return m_nTotalNum; }

	private:
		inline VOID			RealAlloc(INT nNum);
		inline VOID			RealFree(INT nNum);
		inline VOID			RealFreeAll();

	private:
		thread::ThreadMutex	m_Mutex;
		SafeStreamQueue		m_FreeQueue;
		volatile int32		m_nDefaultSize;		// 池扩充时的默认大小
		volatile int32		m_nTotalNum;		// 该池一共分配了多少个stream
	};

} 
