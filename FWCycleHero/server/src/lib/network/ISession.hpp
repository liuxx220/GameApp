/*
-------------------------------------------------------------------------------------------
		file name : address.hpp
		desc	  : 网络会话对象的描述，接口类 
		author    : LJP

		log		  : [2015-04-26]
-------------------------------------------------------------------------------------------
*/

#ifndef __NET_ISESSION_H__
#define __NET_ISESSION_H__
#include "common/common.hpp"






namespace KBEngine 
{ 
	/**
	* @brief When network event happens, the related function of ISession
	*        will be callback. User need to implement ISDSession
	*/
	class ISession
	{

	public:
		
		UINT32				GetType()					{ return mType; }
		void				SetType(UINT32 type)		{ mType = type; }

		UINT32				GetID()						{ return mID; }
		void				SetID(UINT32 id)			{ mID = id; }

	protected:

		UINT32				mID;
		UINT32				mType;

	};
}

#endif // __NET_ISESSION_H__
