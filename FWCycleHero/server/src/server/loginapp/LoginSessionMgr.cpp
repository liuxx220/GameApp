/*
----------------------------------------------------------------------------
		file name : LoginSessionMgr.hpp
		desc	  : 网络命令管理
		author    : LJP

		log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#include "LoginSessionMgr.hpp"
#include "msg_cl.h"
#include "msg_login.h"





namespace KBEngine
{

	CPlayerSession::CPlayerSession()
	{
		InitProtol();
	}

	CPlayerSession::~CPlayerSession()
	{
		
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 注册网络协议
	//--------------------------------------------------------------------------------------------------------
	void  CPlayerSession::InitProtol()
	{
		m_playerNetMgr.Register(CL_ProofAccount,	&CPlayerSession::HandleProofAccount,	"CL_ProofAccount");
		m_playerNetMgr.Register(CL_LoginApp,		&CPlayerSession::HandleLoginAppMessage, "CL_LoginApp");
		m_playerNetMgr.Register(CL_Heartbest,		&CPlayerSession::HandleHeartBeatMessage,"CL_Heartbest");
	}


	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 注册网络协议
	//--------------------------------------------------------------------------------------------------------
	void CPlayerSession::Destory()
	{
		m_playerNetMgr.UnRegisterAll();
	}


	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 网路协议的处理接口
	//--------------------------------------------------------------------------------------------------------
	UINT32 CPlayerSession::HandleMessage()
	{
		
		UINT32 dwSize = 0;
		INT32  nUnRecved = 0;
		tagUnit* pMsg = mClientSocket->GetRecvMessages(dwSize, nUnRecved);
		if (!P_VALID(pMsg))
			return 0;


		uint32 nMsgID = -1;
		m_RecvStream.clear(true);
		m_RecvStream.append((const uint8*)pMsg->Buf, pMsg->dwSize);
		m_RecvStream >> nMsgID;
		m_RecvStream.flushread();
		NETMSGHANDLER handle = m_playerNetMgr.GetMsgHandler(nMsgID, dwSize);
		if (NULL != handle)
			(this->*handle)(&m_RecvStream);
		
		ReturnMsg(pMsg);
		return 0;
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc     : 同步发送网络消息
	//--------------------------------------------------------------------------------------------------------
	void CPlayerSession::SyncSendMessage(tagNetCmd* pMsg)
	{
		if (mClientSocket != NULL)
		{
			m_SendStream.clear(true);
			pMsg->Serialize(&m_SendStream);
			mClientSocket->SyncSend((const char*)m_SendStream.data(), m_SendStream.wpos());
		}
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc     : 异步发送消息
	//--------------------------------------------------------------------------------------------------------
	void CPlayerSession::SendMessage(tagNetCmd* pMsg)
	{
		if (mClientSocket != NULL)
		{
			m_SendStream.clear(true);
			pMsg->Serialize(&m_SendStream);
			mClientSocket->Send((const char*)m_SendStream.data(), m_SendStream.wpos() );
		}
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc     : 网络分配的内存归还到内存池子
	//--------------------------------------------------------------------------------------------------------
	void CPlayerSession::ReturnMsg(tagUnit* pMsg)
	{

	}

	///-----------------------------------------------------------------------------------------
	// 网络消息处理接口
	UINT32 CPlayerSession::HandleProofAccount(MemoryStream* pMsg)
	{
		CProofAccountNetCmd netcmd;
		netcmd.UnSerialize(pMsg);
	

		CProofAccountRespond respond;
		respond.errorCode = E_ProofResult_Account_Ok;
		SyncSendMessage(&respond);
		return 0;
	}

	UINT32 CPlayerSession::HandleLoginAppMessage(MemoryStream* pMsg)
	{
		CLoginAppNetCmd netcmd;
		netcmd.UnSerialize(pMsg);

		return 0;
	}

	UINT32 CPlayerSession::HandleHeartBeatMessage(MemoryStream* pMsg)
	{
		CHeartbestNetCmd netcmd;
		netcmd.UnSerialize(pMsg);
		return 0;
	}



	CLoginSessionMgr::CLoginSessionMgr() : m_GenID(0)
	{
		mapSession.Clear();
	}

	CLoginSessionMgr::~CLoginSessionMgr()
	{
		mapSession.ResetIterator();
		CPlayerSession* pSeesion = NULL;
		while (mapSession.PeekNext(pSeesion))
		{
			SAFE_DEL(pSeesion);
		}
	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 创建连接到登录服务器的 PlayerSession 对象
	//-------------------------------------------------------------------------------------------
	INetSession* CLoginSessionMgr::CreateSession(SESSION_TYPE type)
	{
		CPlayerSession* pSession = new CPlayerSession();
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
	void CLoginSessionMgr::UpdateSession( void )
	{
		if (mapSession.Size() <= 0)
			return;


		mapSession.ResetIterator();
		CPlayerSession* pSeesion = NULL;
		while (mapSession.PeekNext(pSeesion))
		{
			pSeesion->HandleMessage();
		}
	}

	//--------------------------------------------------------------------------------------------
	// function :
	// desc		: 从列表内删除 session 对象
	//-------------------------------------------------------------------------------------------
	bool CLoginSessionMgr::RemoveSession(INetSession* pSession)
	{
		return mapSession.Erase(pSession->GetID());
	}

}