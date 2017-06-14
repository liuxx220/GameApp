/*
----------------------------------------------------------------------------
	file name : DBSession.h
	desc	  : 链接到DB的client session
	author    : LJP

	log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#pragma once
#include "common/singleton.hpp"
#include "common/memorystream.hpp"
#include "network/SessionMgr.hpp"
#include "db_net_cmd_mgr.h"
#include "fpworld.hpp"






namespace KBEngine
{
	
	//////////////////////////////////////////////////////////////////////////
	class MemoryStream;
	class CDBSession : public INetSession
	{

	public:

		CDBSession();
		virtual ~CDBSession();

		void						InitProtol();
		void						Destory();
		void						UpdateSession(void);

		////////////////////////////////////////////////////////////////////////////////////////////
		// 发送消息
		void						SendMessage(tagNetCmd* pMsg);
		void						SyncSendMessage(tagNetCmd* pMsg);
		

		UINT32						HandleMessage();
		void						ReturnMsg(tagUnit* pMsg);


		///-----------------------------------------------------------------------------------------
		// 网络消息处理接口
		UINT32						HandleProofAccount(MemoryStream* pMsg);
		UINT32						HandleLoginAppMessage(MemoryStream* pMsg);
		UINT32						HandleHeartBeatMessage(MemoryStream* pMsg);

	public:

		MemoryStream				m_SendStream;
		MemoryStream				m_RecvStream;
		CDBNetCmdMgr				m_playerNetMgr;		// 消息管理器
	};
}


