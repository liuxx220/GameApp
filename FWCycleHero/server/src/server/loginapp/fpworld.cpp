/*
----------------------------------------------------------------------------
		file name : fpworld.hpp
		desc	  : 场景服务器实例
		author    : LJP

		log		  : [ 2015-08-24 ]
----------------------------------------------------------------------------
*/
#include "fpworld.hpp"
#include "fpworld_mgr.hpp"





namespace KBEngine
{
	//------------------------------------------------------------------------------
	// CONSTANTS
	//------------------------------------------------------------------------------
	const INT32	ALLOW_PLAYER_LOGIN = 2;		// 多少秒放一个人进服务器
	const INT32	UPDATE_QUEUE_TIME = 2000;	// 更新排队列表时间间隔
	const FLOAT	LOGIN_LIMIT = 0.9f;	// 当达到fpworld人数上限的90%,就开始排队
	fpWorld::fpWorld() : m_Data(), m_dwTime(0)
	{

	}

	fpWorld::~fpWorld()
	{
		Destroy();
	}


	////-------------------------------------------------------------------------------
	//// 初始化函数
	////-------------------------------------------------------------------------------
	//bool fpWorld::Init(INT32 nIndex)
	//{
	//	// 设置fpworld Data中的数据
	//	m_Data.bValid			= false;
	//	m_Data.eStatus			= EWS_InitNotDone;
	//	m_Data.nCurOnlineNum	= 0;
	//	m_Data.nMaxOnlineNum	= 0;
	//	m_Data.dwIP				= 0;
	//	m_Data.nPort			= 0;
	//	//temp add by songg
	//	m_Data.dwWorldID		= 1;
	//	return true;
	//}


	//-------------------------------------------------------------------------------
	// 更新函数
	//-------------------------------------------------------------------------------
	void fpWorld::Update()
	{
	//	if ( IsValid() )
	//	{
	//		// 更新所有网络消息
	//		UpdateSession();

	//		// 更新所有加入到游戏世界的玩家
	//		UpdateInsertPlayer();

	//		// 更新所有排队的玩家
	//		UpdateQueuedPlayer();

	//		// 更新所有等待列表中的玩家
	//		UpdateWaitingPlayer();

	//		// 更新踢掉列表中的玩家
	//		UpdateKickedPlayer();
	//	}
	//	else
	//	{
	//		// 更新踢掉列表中的玩家
	//		UpdateKickedPlayer();
	//	}
	}

	////-------------------------------------------------------------------------------
	//// 更新连接
	////-------------------------------------------------------------------------------
	//void fpWorld::UpdateSession()
	//{
	//	uchar* pRecv	= NULL;
	//	UINT32 dwSize   = 0;
	//	INT32  nUnRecv  = 0;
	//	UINT32 dwTime   = 0;
	//	pRecv = sFpWorldMgr.RecvMsg(m_Data.dwNameCrc, dwSize, nUnRecv);
	//	while (P_VALID(pRecv) )
	//	{
	//		// 处理消息
	//		sFpWorldMgr.HandleCmd((tagNetCmd*)pRecv, dwSize, (DWORD)this);

	//		// 归还消息
	//		sFpWorldMgr.ReturnMsg(m_Data.dwNameCrc, pRecv);

	//		// 收取下一条消息
	//		pRecv = sFpWorldMgr.RecvMsg(m_Data.dwNameCrc, dwSize, nUnRecv);
	//	}
	//}

	////-------------------------------------------------------------------------------
	//// 更新插入的玩家列表
	////-------------------------------------------------------------------------------
	//void fpWorld::UpdateInsertPlayer()
	//{
	//	if (!m_Data.bValid) return;
	//	if ( EWS_Well == m_Data.eStatus ) return ;

	//	INT32 nBeginWaitNum  = m_Data.nMaxOnlineNum * LOGIN_LIMIT;
	//	INT32 nTemp			 = m_Data.nCurOnlineNum + m_mapWaitting.Size();

	//	Player* pPlayer		 = m_listInsertPlayer.PopFront();
	//	while (P_VALID(pPlayer) )
	//	{
	//		// 查看排队列表中是否为空
	//		if (m_listQueue.Empty())
	//		{
	//			// 如果当前在线人数超过了排队的基线，则开始排队
	//			if (nTemp >= nBeginWaitNum)
	//			{
	//				AddIntoQueue(pPlayer);
	//			}
	//			// 不需要排队
	//			else
	//			{
	//				AddIntoWaitingMap(pPlayer);
	//				++nTemp;		// 累加一个数字
	//			}
	//		}
	//		// 排队列表不为空，直接添加到排队列表
	//		else
	//		{
	//			AddIntoQueue(pPlayer);
	//		}

	//		// 再获取一个玩家
	//		pPlayer = m_listInsertPlayer.PopFront();
	//	}
	//}

	////-------------------------------------------------------------------------------
	//// 更新排队列表
	////-------------------------------------------------------------------------------
	//void fpWorld::UpdateQueuedPlayer( )
	//{
	//	if (!m_Data.bValid) return;
	//	if (EWS_Well == m_Data.eStatus) return;
	//	if (m_listQueue.Empty()) return;

	//	// 轮询每个玩家
	//	//Player* pPlayer = NULL;
	//	//m_listQueue.ResetIterator();
	//	//INT32 nIndexInQueue = 0;
	//	//while (m_listQueue.PeekNext(pPlayer) )
	//	//{
	//	//	if (pPlayer->IsConnLost())		// 玩家掉线了
	//	//	{
	//	//		m_listQueue.Erase(pPlayer);
	//	//		sPlayerMgr.PlayerLogout(pPlayer);
	//	//		continue;
	//	//	}
	//	//}
	//}


	////-------------------------------------------------------------------------------
	//// 更新等待的玩家
	////-------------------------------------------------------------------------------
	//void fpWorld::UpdateWaitingPlayer()
	//{
	//	if (m_mapWaitting.Empty()) return;

	//	Player* pPlayer = NULL;
	//	m_mapWaitting.ResetIterator();

	//	while (m_mapWaitting.PeekNext(pPlayer))
	//	{
	//		if (pPlayer->IsConnLost())				// 掉线了
	//		{
	//			m_mapWaitting.Erase(pPlayer->GetAccountID());
	//			sPlayerMgr.PlayerLogout(pPlayer);
	//		}
	//		//else if (pPlayer->IsSelectWorldOk())		// 已经验证成功了
	//		//{
	//		//	if (CalcTimeDiff(GetCurrentDWORDTime(), pPlayer->GetKickTime()) > 5)	// 验证成功的玩家如果5秒钟不断开连接，就踢掉
	//		//	{
	//		//		m_mapWaitting.Erase(pPlayer->GetAccountID());
	//		//		KickPlayer(pPlayer);
	//		//	}
	//		//}
	//	}
	//}

	////-------------------------------------------------------------------------------
	//// 更新踢掉的玩家列表
	////-------------------------------------------------------------------------------
	//void fpWorld::UpdateKickedPlayer()
	//{
	//	if (m_listKickPlayer.Empty()) return;

	//	Player* pPlayer = NULL;
	//	m_listKickPlayer.ResetIterator();

	//	while (m_listKickPlayer.PeekNext(pPlayer))
	//	{
	//		if (pPlayer->IsConnLost())		// 被踢掉了
	//		{
	//			m_listKickPlayer.Erase(pPlayer);
	//			sPlayerMgr.PlayerLogout(pPlayer);
	//		}
	//	}
	//}

	//-------------------------------------------------------------------------------
	// 释放资源
	//-------------------------------------------------------------------------------
	void fpWorld::Destroy()
	{
		// 去掉logtime
	}

	////-------------------------------------------------------------------------------
	//// 加入到排队列表中
	////-------------------------------------------------------------------------------
	//void fpWorld::AddIntoQueue(Player* pPlayer)
	//{
	//	//if (!P_VALID(pPlayer)) return;

	//	//m_listQueue.PushBack(pPlayer);

	//	//// 给客户端发送消息
	//	//tagNLS_ProofResult send;
	//	//send.bGuard			= pPlayer->IsGuard();
	//	//send.dwErrorCode	= E_ProofResult_Queue;
	//	//pPlayer->SendMessage(&send, send.dwSize);
	//}

	////-------------------------------------------------------------------------------
	//// 加入到等待列表中
	////-------------------------------------------------------------------------------
	//void fpWorld::AddIntoWaitingMap(Player* pPlayer)
	//{
	//	if (!P_VALID(pPlayer)) return;

	//	m_mapWaitting.Add(pPlayer->GetAccountID(), pPlayer);

	//	// 给world发送消息
	//	//tagNLW_PlayerWillLogin send;
	//	//send.dwVerifyCode	= pPlayer->GetVerifyCode();
	//	//send.dwAccountID	= pPlayer->GetAccountID();
	//	//send.byPrivilege	= pPlayer->GetPrivilege();
	//	//send.bGuard			= pPlayer->GetPlayerData().bGuard;
	//	//send.dwAccOLSec		= pPlayer->GetPlayerData().nAccOLSec;
	//	////add for prevent thief
	//	//send.dwPreLoginIP	= pPlayer->GetPreLoginIP();
	//	//send.dwPreLoginTime = pPlayer->GetPreLoginTime();

	//	//strncpy_s(send.szAccount, pPlayer->GetAccoutName(), X_SHORT_NAME);
	//	//sFpWorldMgr.SendMsg(m_Data.dwNameCrc, &send, send.dwSize);
	//}

	////-------------------------------------------------------------------------------
	//// fpworld登陆
	////-------------------------------------------------------------------------------
	//bool fpWorld::WorldLogin(UINT32 dwIP, INT32 nPort, UINT32* pOLAccountIDs, INT32 nOLAccountIDNum)
	//{
	//	if (IsValid()) return false;	// 检查是否已经连接上

	//	// 设置登陆信息
	//	m_Data.bValid			= TRUE;
	//	m_Data.dwIP				= dwIP;
	//	m_Data.nPort			= nPort;
	//	m_Data.eStatus			= EWS_InitNotDone;
	//	m_Data.nCurOnlineNum	= 0;
	//	m_Data.nMaxOnlineNum	= 0;

	//	//// tdb:worldnamecrc为本world的状态为EPLS_Online的玩家状态为EPLS_Unknown
	//	//sBeton.FixOneWorldPlayerLoginStatus(m_Data.dwNameCrc, EPLS_Online, EPLS_Unknown);

	//	//// tbd:更新玩家状态，指定玩家更新为为EPLS_Online
	//	//sBeton.ResetPlayersLoginStatus(pOLAccountIDs, nOLAccountIDNum, EPLS_Online);

	//	//// tdb:worldnamecrc为本world的状态为EPLS_Unknown的玩家状态为EPLS_Offline
	//	//sBeton.FixOneWorldPlayerLoginStatus(m_Data.dwNameCrc, EPLS_Unknown, EPLS_OffLine);

	//	//sFatigueMgr.ResetWorldAccounts(m_Data.dwNameCrc, pOLAccountIDs, nOLAccountIDNum);

	//	return true;
	//}

	////-------------------------------------------------------------------------------
	//// fpworld登出
	////-------------------------------------------------------------------------------
	//bool fpWorld::WorldLogout()
	//{
	//	if (!IsValid()) return false;	// 检查是否一开始就没连接上

	//	// 设置登出信息
	//	m_Data.bValid		= FALSE;
	//	m_Data.dwIP			= 0;
	//	m_Data.nPort		= 0;
	//	m_Data.eStatus		= EWS_InitNotDone;
	//	m_Data.nCurOnlineNum = 0;
	//	m_Data.nMaxOnlineNum = 0;

	//	// 踢掉所有该游戏世界内的玩家
	//	Player* pPlayer = NULL;

	//	//// 首先踢掉insertlist中的玩家
	//	//pPlayer = m_listInsertPlayer.PopFront();
	//	//while (P_VALID(pPlayer))
	//	//{
	//	//	KickPlayer(pPlayer);

	//	//	pPlayer = m_listInsertPlayer.PopFront();
	//	//}

	//	//// 再踢掉排队列表中的玩家
	//	//pPlayer = m_listQueue.PopFront();
	//	//while (P_VALID(pPlayer))
	//	//{
	//	//	KickPlayer(pPlayer);

	//	//	pPlayer = m_listQueue.PopFront();
	//	//}

	//	//// 再踢掉等待列表中的玩家
	//	//m_mapWaitting.ResetIterator();
	//	//while (m_mapWaitting.PeekNext(pPlayer))
	//	//{
	//	//	KickPlayer(pPlayer);
	//	//}
	//	//m_mapWaitting.Clear();

	//	//sFatigueMgr.ResetWorldAccounts(m_Data.dwNameCrc, NULL, 0);

	//	return true;
	//}

	////-----------------------------------------------------------------------
	//// 玩家登录结果
	////-----------------------------------------------------------------------
	//void fpWorld::PlayerWillLoginRet(UINT32 dwAccountID, UINT32 dwErrorCode)
	//{
	//	// 在等待列表中查找玩家
	//	//Player* pPlayer = m_mapWaitting.Peek(dwAccountID);
	//	//if (P_VALID(pPlayer))
	//	//{
	//	//	// 如果是验证成功，则设置选择游戏世界完毕
	//	//	if (E_Success == dwErrorCode)
	//	//	{
	//	//		pPlayer->SetSelectWorldOK();
	//	//	}

	//	//	// 向玩家发送返回结果
	//	//	tagNLS_ProofResult send;
	//	//	send.bGuard = pPlayer->IsGuard();
	//	//	send.dwIndex = 0;
	//	//	send.dwIP = m_Data.dwIP;
	//	//	send.dwPort = m_Data.nPort;
	//	//	send.dwAccountID = dwAccountID;
	//	//	send.dwVerifyCode = pPlayer->GetVerifyCode();

	//	//	if (E_Success == dwErrorCode)
	//	//		send.dwErrorCode = E_Success;
	//	//	else
	//	//		send.dwErrorCode = E_ProofResult_Account_No_Match;

	//	//	pPlayer->SendMessage(&send, send.dwSize);

	//	//	// 设置玩家要被踢掉的时间
	//	//	pPlayer->SetKickTime(GetCurrentDWORDTime());
	//	//}
	//}
}