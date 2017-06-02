/*
----------------------------------------------------------------------------
	//★Name				:   player_net_cmd_mgr.h
	//★Compiler			:	Microsoft Visual C++ 9.0
	//★Version				:	1.00
	//★Create Date			:	05/31/2009
	//★LastModified		:	05/31/2009
	//★Copyright (C)		:	
	//★Writen  by			:   
	//★Mode  by			:   
	//★Brief				:	客户端命令管理器，主要解决NetCmdMgr无法针对多对象消息处理的缺陷
//////////////////////////////////////////////////////////////////////////
----------------------------------------------------------------------------
*/
#ifndef __FPLAYER_NET_CMD_MGR_H__
#define __FPLAYER_NET_CMD_MGR_H__
#include "common/common.hpp"
#include "common/memorystream.hpp"
#include "network/net_define.h"
#include "network/simple_map.h"







namespace KBEngine
{
	class CPlayerSession;
	typedef UINT32(CPlayerSession::*NETMSGHANDLER)(MemoryStream* pMsg);
	class PlayerNetCmdMgr
	{

	public:

		//-------------------------------------------------------------------------------------
		// 构造函数
		 PlayerNetCmdMgr();
		~PlayerNetCmdMgr();

		void					Destroy(void);

		bool					Register(UINT32 szCmd, NETMSGHANDLER fp, char* szDesc);
		void					UnRegisterAll();


		NETMSGHANDLER			GetMsgHandler(UINT32 dwMsgID, UINT32 nSize);
		
	protected:

		typedef struct tagPlayerCmd
		{
			UINT32				strCmd;			// 命令名
			std::string			strDesc;		// 描述
			NETMSGHANDLER		handler;		// 函数指针
			UINT32				nTimes;			// 收到此命令的次数
		} tagPlayerCmd;

		TMap<UINT32, tagPlayerCmd*>		m_mapProc;
	};
}

#endif
