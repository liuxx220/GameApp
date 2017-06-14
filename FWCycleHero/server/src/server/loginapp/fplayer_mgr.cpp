/*
----------------------------------------------------------------------------
	file name : fplayer_mgr.cpp
	desc	  : 登录到到账号服务器的client 的管理
	author    : LJP

	log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#include "fplayer_mgr.hpp"
#include "player.hpp"
#include "server/serverconfig.hpp"







namespace KBEngine
{

	KBE_SINGLETON_INIT(CPlayerMgr);
	//-------------------------------------------------------------------------------
	// 构造函数
	//-------------------------------------------------------------------------------
	CPlayerMgr::CPlayerMgr() : m_nPlayerNum(0), m_nPlayerLoging(0), m_nProofResultNum(0),
							   m_dwClientIDGen(0), m_dwCurrVer(0), m_nPort(0), 
							   m_ePolicy(EPP_Null)
	{

	}

	//-------------------------------------------------------------------------------
	// 析构函数
	//-------------------------------------------------------------------------------
	CPlayerMgr::~CPlayerMgr()
	{
		Destroy();
	}


	//-------------------------------------------------------------------------------
	// 销毁函数
	//-------------------------------------------------------------------------------
	void CPlayerMgr::AddPlayerToAll(Player* pPlayer)				
	{ 
		m_mapAllPlayer.Add(pPlayer->GetClientID(), pPlayer); 
	}

	//-------------------------------------------------------------------------------
	// 销毁函数
	//-------------------------------------------------------------------------------
	void CPlayerMgr::AddPlayerToLogining(Player* pPlayer)		
	{
		m_mapLoginingPlayer.Add(pPlayer->GetClientID(), pPlayer);
	}

	//-------------------------------------------------------------------------------
	// 销毁函数
	//-------------------------------------------------------------------------------
	void CPlayerMgr::RemoveAccount(UINT32 dwAccountID)
	{
		if (!P_VALID(dwAccountID))
			return;
		m_mapAccountIDClientID.Erase(dwAccountID);
	}

	
	//-------------------------------------------------------------------------------
	// 销毁函数
	//-------------------------------------------------------------------------------
	void CPlayerMgr::Destroy()
	{
		/////////////////////////////////////////////////////
		m_mapAllPlayer.Clear();
		m_mapAccountData.Clear();
		m_listProofResult.Clear();
		m_mapLoginingPlayer.Clear();
		m_mapAccountIDClientID.Clear();
		m_mapAccountNameCrc2AccountID.Clear();
		/////////////////////////////////////////////////////

		Player::UnRegisterPlayerMsg();
		// 网络
		// 清空正在登录的玩家
		Player* pPlayer = NULL;
		m_mapLoginingPlayer.ResetIterator();
		while (m_mapLoginingPlayer.PeekNext(pPlayer))
		{
			SAFE_DEL(pPlayer);
		}
		// 验证策略销毁
		//m_pPolicy->Destroy();
	}


	//-------------------------------------------------------------------------------
	// 更新函数
	//-------------------------------------------------------------------------------
	void CPlayerMgr::Update()
	{
		// 更新验证结果
		UpdateProofResult();
	}


	//---------------------------------------------------------------------------------
	// 玩家登出
	//---------------------------------------------------------------------------------
	void CPlayerMgr::PlayerLogout(Player* pPlayer)
	{
		bool bLogoutFromDB = true;		// 是否设置数据库中该玩家为离线
		if (!pPlayer->IsProofEnd() || GT_INVALID == pPlayer->GetAccountID())	// 还没有验证成功，数据库根本就没有操作过
		{
			bLogoutFromDB = false;
		}
		else if (pPlayer->IsSelectWorldOk())	// world已经给了回应，说明马上就会登入到world上
		{
			bLogoutFromDB = false;
		}

		// 如果需要设置数据库
		/*if (bLogoutFromDB)
		{
			sBeton.PlayerLogout(pPlayer->GetAccountID());
		}*/

		// 如果帐号合法，则移除帐号
		if (GT_INVALID != pPlayer->GetAccountID())
		{
			RemoveAccount(pPlayer->GetAccountID());
		}

		// 从列表中删除该玩家
		RemovePlayerFromAll(pPlayer->GetClientID());

		// 删除玩家
		SAFE_DEL(pPlayer);
	}

	//------------------------------------------------------------------------------------
	// 发送验证
	//------------------------------------------------------------------------------------
	void CPlayerMgr::Proof(UINT32 dwClientID, char* szAccoutName, char* szPsd, char* szGUID)
	{
		if (!P_VALID(szAccoutName) || !P_VALID(szPsd) || !P_VALID(szGUID))
			return;

		// 发送给相应的验证策略进行验证
		// m_pPolicy->Proof(dwClientID, szAccoutName, szPsd, szGUID);
	}
}
