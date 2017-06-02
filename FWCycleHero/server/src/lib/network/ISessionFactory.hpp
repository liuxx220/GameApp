/*
-------------------------------------------------------------------------------------------
		file name : address.hpp
		desc	  : 连接回话工程 
		author    : LJP

		log		  : [2015-04-26]
-------------------------------------------------------------------------------------------
*/

#ifndef __NET_SESSIONFACTORY_H__
#define __NET_SESSIONFACTORY_H__
#include "common/common.hpp"
#include "network/common.hpp"
#include "network/Session.hpp"







namespace KBEngine 
{ 
	/**
	* @brief Create user implemented ISDSession object. This interface need to be implemented by user too
	*/
	class ISessionFactory
	{
	public:
		/**
		* @brief Create an user implemented ISDSession object
		* @param poConnection : ISDConnection instance pointer
		* @return ISDSession instance pointer
		*/
		virtual INetSession*		CreateSession(SESSION_TYPE type) = 0;
	};
}

#endif // __NET_SESSIONFACTORY_H__
