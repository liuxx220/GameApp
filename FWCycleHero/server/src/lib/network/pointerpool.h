/*
------------------------------------------------------------------------------------------------
	file name : pointerpool.hpp
	desc	  : 指针池子
	author    : LJP

	log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"







#define PP_MAX 10000
#define PP_INVALID -1
template<typename T>
class PointerPool
{
public:
	typedef typename std::vector<T*> CPointerPool;
	typedef typename std::vector<T*>::iterator handleitr;
	PointerPool()
	{
		memset(mPointers,0,sizeof(T*)*PP_MAX);
		mMaxPos = 1;
	}
	virtual ~PointerPool()
	{

	}
public:
	bool IsFull()
	{ 
		return mPointerMap.size()==PP_MAX;
	}
	UINT32 GetSize()
	{
		return mPointerMap.size();
	}
	UINT32 AddPointer(T* pointer)
	{
		if (mMaxPos<PP_MAX)
		{//1,增加到最大值//
			mPointers[mMaxPos] = pointer;
			mPointerMap.push_back(pointer);
			return mMaxPos++;
		}
		else if (mInvalidPos.size()>0)
		{//2,获取无效列表//
			UINT32 invalidPos = mInvalidPos.front();
			if (invalidPos!=PP_INVALID)
			{
				mPointers[invalidPos] = pointer;
				mPointerMap.push_back(pointer);
				mInvalidPos.pop_front();
				return invalidPos;
			}
		}
		return PP_INVALID;
	}
	T* GetPointer(UINT32 pos)
	{
		if (pos<PP_MAX)
		{
			return mPointers[pos];
		}
		return NULL;
	}
	void RemovePointer(UINT32 pos)
	{
		if (pos<PP_MAX)
		{
			T* tempPointer = mPointers[pos];
			if (tempPointer!=NULL)
			{
				for (auto it=mPointerMap.begin();it!=mPointerMap.end();++it)
				{
					if (tempPointer==(*it))
					{
						mPointerMap.erase(it);
						break;
					}
				}
				mPointers[pos] = NULL;
				mInvalidPos.push_back(pos);
			}
		}
	}
	CPointerPool& GetPointerMap() { return mPointerMap; }
private:
	T*					mPointers[PP_MAX];
	UINT32				mMaxPos;
	CPointerPool		mPointerMap;
	std::list<UINT32>	mInvalidPos;
};
