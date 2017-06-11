/*
----------------------------------------------------------------------------------------------
			file name : sync_app_handler.h
			desc	  : 客户端发到Cell Server 的协议处理接口
			author	  : ljp

			log		  : by ljp create 2017-06-10
----------------------------------------------------------------------------------------------
*/
#pragma once
#include "network/net_define.h"
#include "network/simple_map.h"
#include "helper/debug_helper.hpp"
#include "common/common.hpp"







namespace KBEngine
{

	class CCellSession;
	typedef UINT32(CCellSession::*CELLNETMSGHANDLER)(MemoryStream* pMsg);
	class CCellAppNetCmdMgr
	{

	public:

		//-------------------------------------------------------------------------------------
		// 构造函数
		CCellAppNetCmdMgr();
		~CCellAppNetCmdMgr();

		void					Destroy(void);

		bool					Register(UINT32 szCmd, CELLNETMSGHANDLER fp, char* szDesc);
		void					UnRegisterAll();


		CELLNETMSGHANDLER		GetMsgHandler(UINT32 dwMsgID, UINT32 nSize);

	protected:

		typedef struct tagPlayerCmd
		{
			UINT32				strCmd;			// 命令名
			std::string			strDesc;		// 描述
			CELLNETMSGHANDLER	handler;		// 函数指针
			UINT32				nTimes;			// 收到此命令的次数
		} tagPlayerCmd;

		TMap<UINT32, tagPlayerCmd*>		m_mapProc;
	};
}


