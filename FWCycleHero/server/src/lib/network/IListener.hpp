/*
-------------------------------------------------------------------------------------------
		file name : IListener.hpp
		desc	  : 服务器网络监听对象
		author    : LJP

		log		  : [2015-04-26]
-------------------------------------------------------------------------------------------
*/

#ifndef __NET_LISTENER_H__
#define __NET_LISTENER_H__
#include "common/common.hpp"
#include "network/ISocket.hpp"
#include "network/ISessionFactory.hpp"









namespace KBEngine 
{ 
	/**
	* @brief The interface provides functionality for TCP listening
	*/
	class IListener
	{
	public:
		/**
		* @brief Set an user implemented ISDPacketParser object, which process
		*        packet parse logic of the connection accepted by the ISDListener
		* @param poPacketParser ISDPacketParser instance pointer
		*/
		virtual void			SetPacketParser(IPacketParser* poPacketParser) = 0;

		/**
		* @brief Set the user implemented ISDSessionFactory object to ISDListener
		*
		* When a TCP connection is accepted by ISDListener,
		* ISDSessionFactory object will be asked to create a ISDSession
		* @param poSessionFactory ISDSessionFactory instance factory
		*/
		virtual void			SetSessionFactory(ISessionFactory* pSessionFactory) = 0;

		/**
		* @brief Set the send and receive buffer size of the connection accepted by the ISDListener object
		* @param dwRecvBufSize : receiving buffer size in bytes
		* @param dwSendBufSize : sending buffer size in bytes
		*/
		virtual void			SetBufferSize(UINT32 dwRecvBufSize, UINT32 dwSendBufSize) = 0;

		
		/**
		* @brief Listen at specified IP and port
		* @param pszIP : IP string
		* @param wPort : port number
		* @param bReUseAddr : the flag for re-using same address
		* @return true means success, false means failure
		*/
		virtual bool			Start(const char* pszIP, UINT16 wPort, bool bReUseAddr = true) = 0;

		/**
		* @brief Stop listening
		* @return true means success, false means failure
		*/
		virtual bool			Stop(void) = 0;

		/**
		* @brief Release the ISDListener object
		*/
		virtual void			Release(void) = 0;
	};
}

#endif // __NET_LISTENER_H__
