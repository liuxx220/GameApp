/*
------------------------------------------------------------------------------------------------
		file name : glw_netsession.hpp
		desc	  : 管理所有链接诶到服务器的client,并维护与之的通道
		author    : LJP

		log		  : [ 2015-04-28 ]
------------------------------------------------------------------------------------------------
*/

#ifndef _NETWORK_NETSESSION_H__
#define _NETWORK_NETSESSION_H__
#include "common/common.hpp"
#include <string>
#include <vector>
#include <map>
#include "network/CliSocket.hpp"
#include "network/ISession.hpp"
#include "google/protobuf/descriptor.h"
#include "google/protobuf/message.h"






namespace KBEngine 
{ 

	enum SESSION_TYPE
	{
		ST_None,
		///////////////////////////
		ST_CLIENT_Login,
		ST_SERVER_Login,
		ST_SERVER_SceneSever,
		ST_SERVER_DB,
		ST_SERVER_GS,
		ST_SERVER_Log,
	};

	/** @Brief 会话的连接状态
	*/
	enum NETSESSIONSTATUS
	{
		NET_SESSION_NONE	= 0,
		NET_SESSION_OK,
		NET_SESSION_ASSOCIATE,
		NET_SESSION_CLOSING,
	};

	class INetSession : public ISession
	{

	public:
		INetSession();
		virtual ~INetSession();
		
		void				delayclose(UINT32 error_code, UINT32 delayCount = 10);
		void				SetSocketObj(ClientSocket* psock)	 { mClientSocket = psock; }
		
		UINT32				GetRemoteIP()				{ return mRemoteIP; }
		UINT16				GetRemotePort(void)			{ return mRemotePort; }

		void				SeRemoteIP(UINT32 ip)		{ mRemoteIP = ip; }
		void				SetRemotePort(UINT16 nPort)	{ mRemotePort = nPort; }

		void				DelayClose(UINT32 error_code, UINT32 delayCount = 10);
		void				SetInited(bool isInited, bool isTrigger = false);
		
		void				Close();
		void				Disconnect(void);
		bool				Send(const char* szBuf, UINT32 dwLen);
		bool				Recv(const char* szBuf, UINT dwLen);
		virtual bool		IsConnected()	{ return mIsConnected; }

	
	protected:
		UINT32				mRemoteIP;
		UINT16				mRemotePort;

		bool				mIsConnected;
		bool				mIsUseBuffer;
		
		ClientSocket*		mClientSocket;
		NETSESSIONSTATUS	mConnectStats;

	};
}


#endif
// _NETWORK_NETSESSION_H__

