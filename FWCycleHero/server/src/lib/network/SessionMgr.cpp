/*
--------------------------------------------------------------------------------------------
file name : INetSessionMgr.hpp
desc	  : 网络Seession管理器对象
author    : LJP

log		  : [2015-04-26]
--------------------------------------------------------------------------------------------
*/
#include "SessionMgr.hpp"
#include "network/CListener.hpp"
#include "network/Session.hpp"
#include "network/message_handler.hpp"




namespace KBEngine 
{ 

	INetSessionMgr* INetSessionMgr::mInstance = NULL;
	INetSessionMgr::INetSessionMgr()
	{
		if (mInstance != NULL) { char* p = NULL; *p = 0; }
		mInstance = this;
		memset(&mListener[0], 0, sizeof(IListener*)* MAX_COUNT_LISTENER);
	}

	INetSessionMgr::~INetSessionMgr()
	{
		for (int i = 0; i < MAX_COUNT_LISTENER; ++i)  {
			if (mListener[i])
				mListener[i]->Stop();
		}
	}

	bool INetSessionMgr::RemoveSession(INetSession* pSession)
	{
		return true;
	}

	void INetSessionMgr::Send(SESSION_TYPE stype, int sessionId, char* pBuffer)
	{
		/*int Len = *(int*)pBuffer;
		if (sessionId>0)
		{
			INetSession* pSession = mapSession.GetPointer(sessionId);
			if (pSession)
			{
				pSession->Send(pBuffer, Len);
			}
		}
		else
		{
			auto &map = mapSession.GetPointerMap();
			for (auto it = map.begin(); it != map.end(); ++it)
			{
				INetSession* pSession = (*it);
				if (pSession)
				{
					if (pSession->GetType() == stype)
					{
						pSession->Send(pBuffer, Len);
						if (sessionId == 0) break;
					}
				}
			}
		}*/
	}

	
	void INetSessionMgr::SendMsgToSession(SESSION_TYPE stype, int sessionId, const std::string& sMsg, int n32MsgID)
	{
		SendMsgToSession(stype, sessionId, sMsg.c_str(), sMsg.size(), n32MsgID);
	}

	void INetSessionMgr::SendMsgToSession(SESSION_TYPE stype, int sessionId, const char* pMsgBuffer, int n32MsgLen, int n32MsgID)
	{
		/*if (!mIsUnSafeSend)
		{
			int n32Length = n32MsgLen + 2 * sizeof(int);
			char* pBuffer = new char[n32Length];
			memcpy(pBuffer + 0 * sizeof(int), (char*)&n32Length, sizeof(int));
			memcpy(pBuffer + 1 * sizeof(int), (char*)&n32MsgID, sizeof(int));
			memcpy(pBuffer + 2 * sizeof(int), pMsgBuffer, n32MsgLen);
			Send(stype, sessionId, pBuffer);
			delete[]pBuffer;
		}
		else
		{
			int n32Length = n32MsgLen + 4 * sizeof(int);
			char* pBuffer = new char[n32Length];
			memcpy(pBuffer + 0 * sizeof(int), (char*)&stype, sizeof(int));
			memcpy(pBuffer + 1 * sizeof(int), (char*)&sessionId, sizeof(int));
			n32Length = n32MsgLen + 8;
			memcpy(pBuffer + 2 * sizeof(int), (char*)&n32Length, sizeof(int));
			memcpy(pBuffer + 3 * sizeof(int), (char*)&n32MsgID, sizeof(int));
			memcpy(pBuffer + 4 * sizeof(int), pMsgBuffer, n32MsgLen);
			EnterCriticalSection(&mNetworkCs);
			m_SafeQueue.push_back(pBuffer);
			LeaveCriticalSection(&mNetworkCs);
		}*/
	}

	void INetSessionMgr::TranMsgToSession(SESSION_TYPE stype, int sessionId, const char* pMsgBuffer, int n32MsgLen, int n32MsgID, int n32TransId, int n32GcNet)
	{
		if (n32TransId == 0) n32TransId = n32MsgID;//无法伪装

		//if (!mIsUnSafeSend)
		//{
		//	int n32Length = n32MsgLen + 4 * sizeof(int);
		//	char* pBuffer = new char[n32Length];
		//	memcpy(pBuffer + 0 * sizeof(int), (char*)&n32Length, sizeof(int));
		//	memcpy(pBuffer + 1 * sizeof(int), (char*)&n32TransId, sizeof(int));//伪装消息ID
		//	memcpy(pBuffer + 2 * sizeof(int), (char*)&n32MsgID, sizeof(int));//真实消息ID
		//	memcpy(pBuffer + 3 * sizeof(int), (char*)&n32GcNet, sizeof(int));//插入
		//	memcpy(pBuffer + 4 * sizeof(int), pMsgBuffer, n32MsgLen);
		//	Send(stype, sessionId, pBuffer);
		//	delete[]pBuffer;
		//}
		//else
		//{
		//	int n32Length = n32MsgLen + 6 * sizeof(int);
		//	char* pBuffer = new char[n32Length];
		//	memcpy(pBuffer + 0 * sizeof(int), (char*)&stype, sizeof(int));
		//	memcpy(pBuffer + 1 * sizeof(int), (char*)&sessionId, sizeof(int));
		//	n32Length = n32MsgLen + 16;
		//	memcpy(pBuffer + 2 * sizeof(int), (char*)&n32Length, sizeof(int));
		//	memcpy(pBuffer + 3 * sizeof(int), (char*)&n32TransId, sizeof(int));//伪装消息ID
		//	memcpy(pBuffer + 4 * sizeof(int), (char*)&n32MsgID, sizeof(int));//真实消息ID
		//	memcpy(pBuffer + 5 * sizeof(int), (char*)&n32GcNet, sizeof(int));//插入
		//	memcpy(pBuffer + 6 * sizeof(int), pMsgBuffer, n32MsgLen);
		//	EnterCriticalSection(&mNetworkCs);
		//	m_SafeQueue.push_back(pBuffer);
		//	LeaveCriticalSection(&mNetworkCs);
		//}
	}

	void INetSessionMgr::DisconnectOne(int sessionId)
	{
		/*INetSession* pSession = mapSession.GetPointer(sessionId);
		if (pSession) pSession->DelayClose(111);*/
	}
}
