/*
--------------------------------------------------------------------------------------------------------------
		file name : 
		desc	  : Cell Server 连接到 CellMgr 上对象
		author	  : ljp

		log		  : by ljp create 2017-06-11
--------------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "common/common.hpp"
#include "network/Session.hpp"
#include "network/net_define.h"
#include "common/memorystream.hpp"
#include "sync_cellapp_handler.h"





namespace KBEngine{

	class CCellSession : public INetSession
	{

	public:
		CCellSession();
		virtual ~CCellSession();

		void				InitProtol();
		void				Destory();

		////////////////////////////////////////////////////////////////////////////////////////////
		// 发送消息
		void				SendMessage(tagNetCmd* pMsg);
		void				SyncSendMessage(tagNetCmd* pMsg);


		UINT32				HandleMessage();
		void				ReturnMsg(tagUnit* pMsg);

	protected:

		MemoryStream		m_SendStream;
		MemoryStream		m_RecvStream;
		CCellAppNetCmdMgr	m_CellSessionNetMgr;
	};
}


