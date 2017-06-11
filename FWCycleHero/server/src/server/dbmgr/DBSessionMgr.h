/*
---------------------------------------------------------------------------------------------------------
		file name : dbappserver.h
		desc      : db 网络服务器,管理所有到DB server 的链接
		author    : ljp

		log		  : 2017-06-09 : 15:55
---------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/memorystream.hpp"
#include "network/SessionMgr.hpp"







namespace KBEngine
{

	class CLoginSession;
	class CDBAppSessionMgr : public INetSessionMgr
	{

	public:
		CDBAppSessionMgr();
		virtual ~CDBAppSessionMgr();

	public:

		//////////////////////////////////////////////////////////////////////////
		// 更新所有Session
		//////////////////////////////////////////////////////////////////////////
		void						UpdateSession(void);

		virtual INetSession*		CreateSession(SESSION_TYPE type);
		virtual bool				RemoveSession(INetSession* pSession);

	protected:

		TMap<UINT32, CLoginSession*>	mapSession;
		UINT32						m_GenID;
	};
	
}

