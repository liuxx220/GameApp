/*
---------------------------------------------------------------------------------------------------------
file name : DBSessionMgr.h
desc      : 连接到DB Server 上所有 Session 管理器
author    : ljp

log		  : 2017-06-09 : 15:55
---------------------------------------------------------------------------------------------------------
*/
#include "DBSessionMgr.h"
#include "loginSession.h"






namespace KBEngine{	


	CDBAppSessionMgr::CDBAppSessionMgr() : m_GenID(0)
	{
		mapSession.Clear();
	}

	CDBAppSessionMgr::~CDBAppSessionMgr()
	{

	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 创建连接到登录服务器的 PlayerSession 对象
	//-------------------------------------------------------------------------------------------
	INetSession* CDBAppSessionMgr::CreateSession(SESSION_TYPE type)
	{
		CLoginSession* pSession = new CLoginSession();
		if (pSession != nullptr)
		{
			pSession->SetType(ST_CLIENT_Login);
			mapSession.Add(m_GenID, pSession);
			pSession->SetID(m_GenID);
			m_GenID++;
		}
		return pSession;
	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 更新session的网络信息
	//-------------------------------------------------------------------------------------------
	void CDBAppSessionMgr::UpdateSession(void)
	{
		if (mapSession.Size() <= 0)
			return;


		mapSession.ResetIterator();
		CLoginSession* pSeesion = NULL;
		while (mapSession.PeekNext(pSeesion))
		{
			pSeesion->HandleMessage();
		}
	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 从列表内删除 session 对象
	//-------------------------------------------------------------------------------------------
	bool CDBAppSessionMgr::RemoveSession(INetSession* pSession)
	{
		return mapSession.Erase(pSession->GetID());
	}

}
