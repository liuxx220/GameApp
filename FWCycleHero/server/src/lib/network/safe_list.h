//-----------------------------------------------------------------------------
//!\file safe_list.h
//!\Auth:
//!
//!\date 2004-07-07
//! last 2004-07-12
//!
//!\brief	将std::list包装成线程安全
//!
//-----------------------------------------------------------------------------
#pragma once








namespace KBEngine {

//-----------------------------------------------------------------------------
// 将std::list包装成线程安全
//-----------------------------------------------------------------------------
template<typename Type> class TSafeList
{
public:
	// 添加元素到队列尾
	void PushBack(Type val)
	{
		EnterCriticalSection(&m_Lock);
		m_list.push_back(val);
		++m_nItemNum;
		LeaveCriticalSection(&m_Lock);
	}

	// 添加元素到队列首部
	void PushFront(Type val)
	{
		EnterCriticalSection(&m_Lock);
		m_list.push_front(val);
		++m_nItemNum;
		LeaveCriticalSection(&m_Lock);
	}

	// 从队列头取元素
	Type PopFront()
	{
		Type val;
		if( m_nItemNum <= 0 )
			return Type(GT_INVALID);

		EnterCriticalSection(&m_Lock);
		if( m_nItemNum <= 0 )
		{
			LeaveCriticalSection(&m_Lock);
			return Type(GT_INVALID);
		}

		val = m_list.front();
		m_list.pop_front();
		m_nItemNum--;
		
		LeaveCriticalSection(&m_Lock);
		return val;
	}

	// 删除指定元素
	bool Erase(Type& val)
	{
		std::list<Type>::iterator it;
		EnterCriticalSection(&m_Lock);
		for(it=m_list.begin(); it!=m_list.end(); ++it)
		{
			if( val == *it )
			{
				m_list.erase(it);
				--m_nItemNum;
				LeaveCriticalSection(&m_Lock);
				return true;
			}
		}
		LeaveCriticalSection(&m_Lock);
		return false;
	}


	// 检查指定元素是否存在
	bool IsExist(Type& val)
	{
		bool bFound = false;
		EnterCriticalSection(&m_Lock);
		std::list<Type>::iterator it;
		for(it=m_list.begin(); it!=m_list.end(); ++it)
		{
			if( val == *it )
			{
				bFound = true;
				break;
			}
		}
		LeaveCriticalSection(&m_Lock);
		return bFound;
	}



	// 清空所有元素
	void Clear()
	{
		EnterCriticalSection(&m_Lock);
		m_list.clear();
		m_nItemNum=0;
		LeaveCriticalSection(&m_Lock);
	}

	// 得到元素数目,std::list.size()并不能保证线程安全,
	// 所以我们自己保存一份个数数据
	INT32	Size() { return m_nItemNum;	}

	// 安全的将数据导入一个普通std::list,返回数据个数
	INT32 Export(std::list<Type>& listDest)
	{
		INT32 n=0;
		EnterCriticalSection(&m_Lock);
		std::list<Type>::iterator it;
		for(it=m_list.begin(); it!=m_list.end(); ++it, ++n)
			listDest.push_back(*it);
            
		LeaveCriticalSection(&m_Lock);
		return n;
	}


	TSafeList():m_nItemNum(0) { InitializeCriticalSection(&m_Lock); }
	~TSafeList(){ DeleteCriticalSection(&m_Lock); }

private:
	std::list<Type>		m_list;
	INT32				m_nItemNum;
	CRITICAL_SECTION	m_Lock;
};



}