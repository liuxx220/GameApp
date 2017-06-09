/*
----------------------------------------------------------------------------
		file name : db_net_cmd_mgr.cpp
		desc	  : DB 客户端处理 DBServer 返回来的消息
		author    : LJP

		log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#include "helper/debug_helper.hpp"
#include "db_net_cmd_mgr.h"







namespace KBEngine
{
	#define GT_MAX_PACKAGE_LEN		512	
	//-------------------------------------------------------------------------------------
	CDBNetCmdMgr::CDBNetCmdMgr()
	{
		m_mapProc.Clear();
	}

	CDBNetCmdMgr::~CDBNetCmdMgr()
	{
		Destroy();
	}

	//-----------------------------------------------------------------------------
	// destroy
	//-----------------------------------------------------------------------------
	void CDBNetCmdMgr::Destroy()
	{
		
	}

	//-----------------------------------------------------------------------------
	// 注册消息
	//-----------------------------------------------------------------------------
	bool CDBNetCmdMgr::Register(UINT32 dwID, DBNETMSGHANDLER fp, char* szDesc)
	{
		tagPlayerCmd* pCmd = m_mapProc.Peek(dwID);
		if (P_VALID(pCmd))
		{
			if (pCmd->strCmd == dwID)
			{
				KBE_ASSERT(0);
				return false;
			}	
		}
		else
		{
			pCmd = new tagPlayerCmd;
			pCmd->nTimes	= 0;
			pCmd->handler	= fp;
			pCmd->strCmd	= dwID;
			pCmd->strDesc	= szDesc;
			m_mapProc.Add(dwID, pCmd);
		}
		return true;
	}

	//------------------------------------------------------------------------------
	// 取消注册
	//------------------------------------------------------------------------------
	void CDBNetCmdMgr::UnRegisterAll()
	{
		m_mapProc.ResetIterator();
		tagPlayerCmd* pCmd = NULL;
		while (m_mapProc.PeekNext(pCmd))
		{
			SAFE_DEL(pCmd);
		}
		m_mapProc.Clear();
	}

	//------------------------------------------------------------------------------
	// 得到某个消息ID对应的处理函数
	//------------------------------------------------------------------------------
	DBNETMSGHANDLER CDBNetCmdMgr::GetMsgHandler(UINT32 dwMsgID, UINT32 nSize)
	{
		tagPlayerCmd* pCmd = m_mapProc.Peek(dwMsgID);
		if (!P_VALID(pCmd))
		{
			//ERROR_MSG("Unknow player command recved[<cmdid>%d <size>%d]\r\n", pMsg->dwID, nMsgSize);
			return NULL;
		}

		/*
		if (pMsg->dwSize != nSize || nSize > GT_MAX_PACKAGE_LEN)
		{
			//ERROR_MSG("Invalid net command size[<cmd>%u <size>%d]\r\n", pMsg->dwID, pMsg->dwSize);
			return NULL;
		}
		*/
		++pCmd->nTimes;
		return pCmd->handler;
	}
}