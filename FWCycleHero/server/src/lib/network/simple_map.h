//-----------------------------------------------------------------------------
//!\file simple_map.h
//!\Auth:
//!
//!\date 2004-10-27
//! last 2004-10-27
//!
//!\brief	提供简单的map管理
//!
//!
//-----------------------------------------------------------------------------
#pragma once







namespace KBEngine {

//-----------------------------------------------------------------------------
// 提供简单的map管理
//-----------------------------------------------------------------------------
template<class KeyType, class ValueType> class TMap
{
public:
	typedef std::map<KeyType, ValueType>					map_type;
	typedef typename map_type::iterator						TMapIterator;
	typedef typename map_type::const_iterator				TMapCIterator;

public:
	//! 添加元素
	void Add(KeyType key, ValueType value)
	{ m_map.insert(std::make_pair(key, value)); }
	
	//! 读取元素
	ValueType Peek(KeyType key)
	{
		std::map<KeyType, ValueType>::iterator it = m_map.find(key);
		if( it == m_map.end() )
			return ValueType(GT_INVALID);
		else
			return it->second;
	}

	ValueType Peek( KeyType key ) const
	{
		std::map<KeyType, ValueType>::const_iterator it = m_map.find(key);
		if( it == m_map.end() )
			return ValueType(GT_INVALID);
		else
			return it->second;
	}
	
	//! 改变指定key对应的值
	bool ChangeValue(KeyType key, ValueType new_value)
	{
		std::map<KeyType, ValueType>::iterator it = m_map.find(key);
		if( it == m_map.end() )
			return false;

		it->second = new_value;
		return true;
	}

	//! 累加指定key对应的值，如果key不存在，则直接添加（要求ValueType必须有operator+和operator=重载）
	void ModifyValue(KeyType key, ValueType mod_value)
	{
		std::map<KeyType, ValueType>::iterator it = m_map.find(key);
		if( it == m_map.end() )
		{
			m_map.insert(std::make_pair(key, mod_value));
		}
		else
		{
			it->second = it->second + mod_value;
		}
	}

	//! 检查指定元素是否存在
	bool IsExist(KeyType key) const
	{
		std::map<KeyType, ValueType>::const_iterator it = m_map.find(key);
		if( it == m_map.end() )
			return false;
		else
			return true;
	}

	//! 删除一个指定元素
	bool Erase(KeyType key)
	{ 
		std::map<KeyType, ValueType>::iterator it = m_map.find(key);
		if( it == m_map.end() )
			return false;

        m_map.erase(it);
		return true;
	}

	//! 清空所有元素
	void Clear() { m_map.clear(); }

	//! 得到元素个数
	INT32	Size() const { return (INT32)m_map.size(); }

	bool Empty() { return m_map.empty(); }

	//! 将内部的迭代器初始化到map的开始
	void ResetIterator()
	{ m_it = m_map.begin(); }

	void ResetIterator() const
	{
		m_const_it = m_map.begin();
	}

	// ! 得到map的头
	TMapIterator	Begin()			{ return m_map.begin(); }
	TMapCIterator	Begin() const	{ return m_map.begin(); }
	// ! 得到map的尾
	TMapIterator	End()			{ return m_map.end(); }
	TMapCIterator	End() const		{ return m_map.end(); }


	//! 取出内部的迭代器指向的元素，并后移迭代器
	bool PeekNext(ValueType& value)
	{
		if( m_it == m_map.end() )
			return false;
		value = m_it->second;
		++m_it;
		return true;
	}

	//! 取出指定迭代器指向的元素，并后移该迭代器
	bool PeekNext(TMapIterator& it, ValueType& value)
	{
		if( it == m_map.end() )
			return false;
		value = it->second;
		++it;
		return true;
	}

	bool PeekNext(TMapCIterator& it, ValueType& value) const
	{
		if( it == m_map.end() )
			return false;
		value = it->second;
		++it;
		return true;
	}


	//! 取出内部的迭代器指向的元素，并后移迭代器
	bool PeekNext(KeyType& key, ValueType& value)
	{
		if( m_it == m_map.end() )
			return false;
		key = m_it->first;
		value = m_it->second;
		++m_it;
		return true;
	}

	bool PeekNext(KeyType& key, const ValueType& value) const
	{
		if( m_const_it == m_map.end() )
			return false;
		key = m_const_it->first;
		value = m_const_it->second;
		++m_const_it;
		return true;
	}

	//! 取出指定的迭代器指向的元素，并后移迭代器
	bool PeekNext(TMapIterator& it, KeyType& key, ValueType& value)
	{
		if( it == m_map.end() )
			return false;
		key = it->first;
		value = it->second;
		++it;
		return true;
	}

	//! 从list里面随机抽取一个元素
	bool RandPeek(KeyType& key, ValueType& value)
	{
		INT nSize = m_map.size();
		if( nSize <= 0 )
			return false;

		INT nRand = rand() % nSize;

		std::map<KeyType, ValueType>::iterator it = m_map.begin();
		for(INT n=0; n<nRand; n++)
			++it;

		key = it->first;
		value = it->second;
		return true;
	}


	//! 将所有key导出到一个给定std::list
	void ExportAllKey(std::list<KeyType>& listDest)
	{
		std::map<KeyType, ValueType>::iterator it;
		for(it = m_map.begin(); it != m_map.end(); ++it)
			listDest.push_back(it->first);
	}
	
	//! 将所有value导出到一个给定std::list
	void ExportAllValue(std::list<ValueType>& listDest)
	{
		std::map<KeyType, ValueType>::iterator it;
		for(it = m_map.begin(); it != m_map.end(); ++it)
			listDest.push_back(it->second);
	}


private:
	std::map<KeyType, ValueType>					m_map;
	typename std::map<KeyType, ValueType>::iterator m_it;
	typename std::map<KeyType, ValueType>::const_iterator m_const_it;
};


}
