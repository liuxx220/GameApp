/*
----------------------------------------------------------------------------
		file name : fpworld.hpp
		desc	  : 场景服务器实例
		author    : LJP

		log		  : [ 2015-08-24 ]
----------------------------------------------------------------------------
*/
#pragma once
#include "flogin_define.hpp"
#include "common/common.hpp"
#include "network/simple_list.h"
#include "network/simple_map.h"





namespace KBEngine
{
	//-----------------------------------------------------------------------------
	// 游戏世界
	//-----------------------------------------------------------------------------
	class Player;
	class fpWorld 
	{

	public:
		//--------------------------------------------------------------------------
		// CONSTRUCT
		//--------------------------------------------------------------------------
		 fpWorld();
		~fpWorld();

		//--------------------------------------------------------------------------
		// 初始化，更新及销毁
		//--------------------------------------------------------------------------
		bool				Init(INT32 nIndex);
		void				Update();
		void				Destroy();


		//--------------------------------------------------------------------------
		// 开启和关闭
		//--------------------------------------------------------------------------
		bool				WorldLogin(UINT32 dwIP, INT32 nPort, UINT32* pOLAccountIDs, INT32 nOLAccountIDNum);
		bool				WorldLogout();

		//--------------------------------------------------------------------------
		// 各种Get
		//--------------------------------------------------------------------------
		UINT32				GetID()				{ return m_Data.dwNameCrc; }
		UINT32				GetWorldID()		{ return m_Data.dwWorldID; }
		EWorldStatus		GetStatus()			{ return m_Data.eStatus; }
		INT32				GetMaxOnlineNum()	{ return m_Data.nMaxOnlineNum; }
		INT32				GetCurrOnlineNum()	{ return m_Data.nCurOnlineNum; }
		char*				GetName()			{ return m_Data.szName; }
		INT32				GetListQueue()		{ return m_listQueue.Size(); }
		UINT32				GetDBUpdateTime()	{ return m_dwDBUpdateTime; }
		UINT32				GetDBInsertTime()	{ return m_dwDBInsertTime; }

		//--------------------------------------------------------------------------
		// 各种Set
		//--------------------------------------------------------------------------
		void				SetDBUpdateTime(UINT32 dwTime)	{ m_dwDBUpdateTime = dwTime; }
		void				SetDBInsertTime(UINT32 dwTime)	{ m_dwDBInsertTime = dwTime; }

		bool				IsValid()			{ return m_Data.bValid; }
		bool				IsAutoSeal()		{ return m_Data.bAutoSeal; }

		//--------------------------------------------------------------------------
		// 更新状态
		//--------------------------------------------------------------------------
		void				UpdateStatus(EWorldStatus eStatus, INT32 nCurOnline, INT32 nMaxOnline);


		//--------------------------------------------------------------------------
		// 辅助函数
		//--------------------------------------------------------------------------
		void				KickPlayer(Player* pPlayer);
		void				AddPlayer(Player* pPlayer)	{}

		//----------------------------------------------------------------------------
		// 玩家请求登入结果
		//----------------------------------------------------------------------------
		void				PlayerWillLoginRet(UINT32 dwAccountID, UINT32 dwErrorCode);

		
	private:
		//----------------------------------------------------------------------------
		// 更新所有连接，排队列表和等待列表
		//----------------------------------------------------------------------------
		void				UpdateSession();
		void				UpdateInsertPlayer();
		void				UpdateQueuedPlayer();
		void				UpdateWaitingPlayer();
		void				UpdateKickedPlayer();

		//----------------------------------------------------------------------------
		// 加入各个列表
		//----------------------------------------------------------------------------
		void				AddIntoQueue(Player* pPlayer);
		void				AddIntoWaitingMap(Player* pPlayer);
	private:

		UINT32					m_id; 
		EWorldStatus			m_eStatus;
		tagWorldInfo			m_Data;
		UINT32					m_dwTime;					// 用于定时更新排队列表

		UINT32					m_dwDBUpdateTime;
		UINT32					m_dwDBInsertTime;
		//----------------------------------------------------------------------------------
		// 玩家列表
		//----------------------------------------------------------------------------------
		TList<Player*>			m_listInsertPlayer;			// 插入列表
		TList<Player*>			m_listQueue;				// 排队列表
		TMap<DWORD, Player*>	m_mapWaitting;				// 等待进入游戏世界的玩家列表
		TList<Player*>			m_listKickPlayer;			// 由于种种原因被踢掉玩家
	};
}

