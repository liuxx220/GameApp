/*
----------------------------------------------------------------------------
		file name : DBSession.hpp
		desc	  : 链接到DB的client session
		author    : LJP

		log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#include "db_session.h"
#include "msg_cl.h"
#include "msg_login.h"





namespace KBEngine
{

	CDBSession::CDBSession()
	{
		InitProtol();
	}

	CDBSession::~CDBSession()
	{
		
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 注册网络协议
	//--------------------------------------------------------------------------------------------------------
	void  CDBSession::InitProtol()
	{
		
		m_playerNetMgr.Register(CL_ProofAccount,	&CDBSession::HandleProofAccount,		"CL_ProofAccount");
		m_playerNetMgr.Register(CL_LoginApp,		&CDBSession::HandleLoginAppMessage,		"CL_LoginApp");
		m_playerNetMgr.Register(CL_Heartbest,		&CDBSession::HandleHeartBeatMessage,	"CL_Heartbest");
		
	}


	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 注册网络协议
	//--------------------------------------------------------------------------------------------------------
	void CDBSession::Destory()
	{
		m_playerNetMgr.UnRegisterAll();
	}


	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 心跳函数
	//--------------------------------------------------------------------------------------------------------
	void CDBSession::UpdateSession()
	{
		HandleMessage();
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 网路协议的处理接口
	//--------------------------------------------------------------------------------------------------------
	UINT32 CDBSession::HandleMessage()
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
		DBNETMSGHANDLER handle = m_playerNetMgr.GetMsgHandler(nMsgID, dwSize);
		if (NULL != handle)
			(this->*handle)(&m_RecvStream);
		
		ReturnMsg(pMsg);
		return 0;
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc     : 同步发送网络消息
	//--------------------------------------------------------------------------------------------------------
	void CDBSession::SyncSendMessage(tagNetCmd* pMsg)
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
	void CDBSession::SendMessage(tagNetCmd* pMsg)
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
	void CDBSession::ReturnMsg(tagUnit* pMsg)
	{

	}

	///-----------------------------------------------------------------------------------------
	// 网络消息处理接口
	UINT32 CDBSession::HandleProofAccount(MemoryStream* pMsg)
	{
		CProofAccountNetCmd netcmd;
		netcmd.UnSerialize(pMsg);
	

		CProofAccountRespond respond;
		respond.errorCode = E_ProofResult_Account_Ok;
		SyncSendMessage(&respond);
		return 0;
	}

	UINT32 CDBSession::HandleLoginAppMessage(MemoryStream* pMsg)
	{
		CLoginAppNetCmd netcmd;
		netcmd.UnSerialize(pMsg);

		return 0;
	}

	UINT32 CDBSession::HandleHeartBeatMessage(MemoryStream* pMsg)
	{
		CHeartbestNetCmd netcmd;
		netcmd.UnSerialize(pMsg);
		return 0;
	}
}