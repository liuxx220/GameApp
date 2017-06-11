/*
--------------------------------------------------------------------------------------------------------------
		file name : 
		desc	  : 连接到场景服务器上的客户端管理对象
		author	  : ljp

		log		  : by ljp create 2017-06-11
--------------------------------------------------------------------------------------------------------------
*/

#include "cellapp.h"
#include "ClientSession.h"
#include "common/memorystream.hpp"







namespace KBEngine{	

	CCellAppSessionMgr::CCellAppSessionMgr() : m_GenID(0)
	{
		mapSession.Clear();
	}

	CCellAppSessionMgr::~CCellAppSessionMgr()
	{

	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 创建连接到登录服务器的 PlayerSession 对象
	//-------------------------------------------------------------------------------------------
	INetSession* CCellAppSessionMgr::CreateSession(SESSION_TYPE type)
	{
		CCleintSession* pSession = new CCleintSession();
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
	void CCellAppSessionMgr::UpdateSession(void)
	{
		if (mapSession.Size() <= 0)
			return;


		mapSession.ResetIterator();
		CCleintSession* pSeesion = NULL;
		while (mapSession.PeekNext(pSeesion))
		{
			pSeesion->HandleMessage();
		}
	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 从列表内删除 session 对象
	//-------------------------------------------------------------------------------------------
	bool CCellAppSessionMgr::RemoveSession(INetSession* pSession)
	{
		return mapSession.Erase(pSession->GetID());
	}

}
