//-----------------------------------------------------------------------------
//!\file x_list.h
//!\Auth:
//!
//!\date 2004-07-07
//! last 2009-06-11
//!
//!\brief	将std::list包装成线程安全
//!
//-----------------------------------------------------------------------------
#pragma once






namespace KBEngine 
{

	//-----------------------------------------------------------------------------
	// 将std::list包装成线程安全
	//-----------------------------------------------------------------------------
	template<typename Type> class XList
	{
	public:
		inline void PushBack(Type val)	// 添加元素到队列尾
		{
			Lock();
			m_list.push_back(val);
			++m_nItemNum;
			Unlock();
		}

		inline Type PopFront()	// 从队列头取元素
		{
			if( m_nItemNum <= 0 )
				return Type(GT_INVALID);

			Lock();
			if( m_nItemNum <= 0 )
			{
				Unlock();
				return Type(GT_INVALID);
			}
			Type val = m_list.front();
			m_list.pop_front();
			m_nItemNum--;
			Unlock();
			return val;
		}
	
		inline bool Erase(Type& val)	// 删除指定元素
		{
			std::list<Type>::iterator it;
			Lock();
			for(it=m_list.begin(); it!=m_list.end(); ++it)
			{
				if( val == *it )
				{
					m_list.erase(it);
					--m_nItemNum;
					Unlock();
					return true;
				}
			}
			Unlock();
			return false;
		}

		inline bool IsExist(Type& val)	// 检查指定元素是否存在
		{
			Lock();
			std::list<Type>::iterator it;
			for(it=m_list.begin(); it!=m_list.end(); ++it)
			{
				if( val == *it )
				{
					Unlock();
					return true
				}
			}
			Unlock();
			return false;
		}

		inline void Clear()	// 清空所有元素
		{
			Lock();
			m_list.clear();
			m_nItemNum=0;
			Unlock();
		}

		// 得到元素数目,std::list.size()并不能保证线程安全,所以我们自己保存一份个数数据
		inline INT32	Size() { return m_nItemNum; }

		//! 取出内部的迭代器指向的元素，并后移迭代器,注意要lock后才能使用
		inline bool _Peek(Type& value)
		{
			if( m_it == m_list.end() )
				return FALSE;
			value = *m_it;
			return true;
		}

		//! 将内部的迭代器初始化到map的开始,注意要lock后才能使用
		inline void _ResetIterator(){ m_it = m_list.begin(); }
		inline void _AddIterator(){ ++m_it; }
		inline void _EraseCurrent(){ m_list.erase(m_it); }

		inline XList() :m_nItemNum(0), m_lLock(0) {}
		inline ~XList(){ }//ASSERT( 0 == m_nItemNum); }
		inline void	Lock() { while (InterlockedCompareExchange((LPLONG)&m_lLock, 1, 0) != 0) Sleep(0); }
		inline void	Unlock() { InterlockedExchange((LPLONG)(&m_lLock), 0); }

	private:
		std::list<Type>						m_list;
		typename std::list<Type>::iterator	m_it;
	
		INT32	volatile	m_nItemNum;
		LONG	volatile	m_lLock;
	};

}