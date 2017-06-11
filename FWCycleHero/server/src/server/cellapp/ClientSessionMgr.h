/*
--------------------------------------------------------------------------------------------------------------
		file name : 
		desc	  : 连接到场景服务器上的客户端管理对象
		author	  : ljp

		log		  : by ljp create 2017-06-11
--------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "network/SessionMgr.hpp"






namespace KBEngine{

	class CCleintSession;
	class CCellAppSessionMgr : public INetSessionMgr
	{

	public:
		CCellAppSessionMgr();
		virtual ~CCellAppSessionMgr();

	public:

		//////////////////////////////////////////////////////////////////////////
		// 更新所有Session
		//////////////////////////////////////////////////////////////////////////
		void						UpdateSession(void);

		virtual INetSession*		CreateSession(SESSION_TYPE type);
		virtual bool				RemoveSession(INetSession* pSession);

	protected:

		TMap<UINT32, CCleintSession*>	mapSession;
		UINT32						m_GenID;
	};
}


