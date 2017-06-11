/*
---------------------------------------------------------------------------------------------------
			file name : cellapp.h
			desc      : 場景服務器
			author	  : ljp

			log		  : by ljp create 2017-06-11
---------------------------------------------------------------------------------------------------
*/
#pragma once
#include "spaces.h"
#include "cells.h"
#include "LoginSession.h"
#include "DBSession.h"
#include "network/event_dispatcher.hpp"
#include "ClientSessionMgr.h"

	



namespace KBEngine
{

	class CellServerApp : public Singleton<CellServerApp>
	{
	public:
	
		 CellServerApp();
		~CellServerApp();
	
		bool						Initialize(COMPONENT_TYPE componentType);
		void						MainLoop(void);

		/* 初始化相关接口 */
		/* 初始化相关接口 */
		bool						initDB();
		bool						InitializeEnd();
		void						Destroy(void);
	protected:

		// 所有的cell
		COMPONENT_TYPE				mComponentType;

		CCellAppSessionMgr*			m_CellSessionMgr;
		EventDispatcher*			m_pDispatcher;
		CLoginSession				m_Login;				// 连接到登录服务器
		CDBSession					m_DB;					// 连接到数据库

		Cells						cells_;
		uint32						flags_;
	};

}


