/*
--------------------------------------------------------------------------------------------------------------
		file name : 
		desc	  : 连接到场景服务器上的客户端管理对象
		author	  : ljp

		log		  : by ljp create 2017-06-11
--------------------------------------------------------------------------------------------------------------
*/

#include "cellapp.h"
#include "LoginSession.h"
#include "common/memorystream.hpp"







namespace KBEngine{	

	CLoginSession::CLoginSession()
	{

	}

	CLoginSession::~CLoginSession()
	{

	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 注册网络协议
	//--------------------------------------------------------------------------------------------------------
	void  CLoginSession::InitProtol()
	{
		//m_LoginSessionNetMgr.Register(CL_ProofAccount, &CLoginSession::HandleProofAccount, "CL_ProofAccount");

	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 注册网络协议
	//--------------------------------------------------------------------------------------------------------
	void CLoginSession::Destory()
	{
		m_LoginSessionNetMgr.UnRegisterAll();
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc		: 网路协议的处理接口
	//--------------------------------------------------------------------------------------------------------
	UINT32 CLoginSession::HandleMessage()
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
		LOGINNETMSGHANDLER handle = m_LoginSessionNetMgr.GetMsgHandler(nMsgID, dwSize);
		if (NULL != handle)
			(this->*handle)(&m_RecvStream);

		ReturnMsg(pMsg);
		return 0;
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc     : 同步发送网络消息
	//--------------------------------------------------------------------------------------------------------
	void CLoginSession::SyncSendMessage(tagNetCmd* pMsg)
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
	void CLoginSession::SendMessage(tagNetCmd* pMsg)
	{
		if (mClientSocket != NULL)
		{
			m_SendStream.clear(true);
			pMsg->Serialize(&m_SendStream);
			mClientSocket->Send((const char*)m_SendStream.data(), m_SendStream.wpos());
		}
	}

	//--------------------------------------------------------------------------------------------------------
	// function :
	// desc     : 网络分配的内存归还到内存池子
	//--------------------------------------------------------------------------------------------------------
	void CLoginSession::ReturnMsg(tagUnit* pMsg)
	{

	}
}
