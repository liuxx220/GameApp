/*
----------------------------------------------------------------------------------------------
			file name : sync_loginapp_handler.h
			desc	  : 场景服务器与登录服务器返回协议处理
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

	class CLoginSession;
	typedef UINT32(CLoginSession::*LOGINNETMSGHANDLER)(MemoryStream* pMsg);
	class CLoginAppNetCmdMgr
	{

	public:

		//-------------------------------------------------------------------------------------
		// 构造函数
		 CLoginAppNetCmdMgr();
		~CLoginAppNetCmdMgr();

		void					Destroy(void);

		bool					Register(UINT32 szCmd, LOGINNETMSGHANDLER fp, char* szDesc);
		void					UnRegisterAll();


		LOGINNETMSGHANDLER		GetMsgHandler(UINT32 dwMsgID, UINT32 nSize);

	protected:

		typedef struct tagPlayerCmd
		{
			UINT32				strCmd;			// 命令名
			std::string			strDesc;		// 描述
			LOGINNETMSGHANDLER	handler;		// 函数指针
			UINT32				nTimes;			// 收到此命令的次数
		} tagPlayerCmd;

		TMap<UINT32, tagPlayerCmd*>		m_mapProc;
	};
}


