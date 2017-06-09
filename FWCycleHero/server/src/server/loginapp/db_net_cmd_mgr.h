/*
----------------------------------------------------------------------------------------------------------------
		file name : db_net_cmd_mgr.h
		desc	  : DB 客户端处理 DBServer 返回来的消息
		author    : 

		log		  : by ljp create 2017-06-09
----------------------------------------------------------------------------------------------------------------
*/
#ifndef __FPLAYER_NET_CMD_MGR_H__
#define __FPLAYER_NET_CMD_MGR_H__
#include "common/common.hpp"
#include "common/memorystream.hpp"
#include "network/net_define.h"
#include "network/simple_map.h"







namespace KBEngine
{
	class CDBSession;
	typedef UINT32(CDBSession::*DBNETMSGHANDLER)(MemoryStream* pMsg);
	class CDBNetCmdMgr
	{

	public:

		//-------------------------------------------------------------------------------------
		// 构造函数
		 CDBNetCmdMgr();
		~CDBNetCmdMgr();

		
		void					Destroy(void);

		bool					Register(UINT32 szCmd, DBNETMSGHANDLER fp, char* szDesc);
		void					UnRegisterAll();


		DBNETMSGHANDLER			GetMsgHandler(UINT32 dwMsgID, UINT32 nSize);
		
	protected:

		typedef struct tagPlayerCmd
		{
			UINT32				strCmd;			// 命令名
			std::string			strDesc;		// 描述
			DBNETMSGHANDLER		handler;		// 函数指针
			UINT32				nTimes;			// 收到此命令的次数
		} tagPlayerCmd;

		TMap<UINT32, tagPlayerCmd*>		m_mapProc;
	};
}

#endif
