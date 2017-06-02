/*
----------------------------------------------------------------------------
		file name : fpworld_mgr.hpp
		desc	  : 管理所有的游戏服务器
		author    : LJP

		log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#include "common/common.hpp"
#include "fpworld_mgr.hpp"






namespace KBEngine
{
	KBE_SINGLETON_INIT(FpWorldMgr);
	//------------------------------------------------------------------------------
	// 构造函数
	//------------------------------------------------------------------------------
	FpWorldMgr::FpWorldMgr()
	{
		//g_pWorldServerMgr = this;
	}

	//------------------------------------------------------------------------------
	// 析构函数
	//------------------------------------------------------------------------------
	FpWorldMgr::~FpWorldMgr()
	{
		Destroy();
	}

	//-------------------------------------------------------------------------------
	// 初始化函数
	//-------------------------------------------------------------------------------
	bool FpWorldMgr::Init()
	{
		return true;
	}

	//-------------------------------------------------------------------------------
	// 销毁函数
	//-------------------------------------------------------------------------------
	void FpWorldMgr::Destroy()
	{
		// 清空fpworld集合
		fpWorld* pFWorld = NULL;
		m_mapFpWorld.ResetIterator();
		while (m_mapFpWorld.PeekNext(pFWorld))
		{
			SAFE_DEL(pFWorld);
		}
		m_mapFpWorld.Clear();
		UnRegisterFpWorldMsg();
	}

	//-------------------------------------------------------------------------------
	// 更新函数
	//-------------------------------------------------------------------------------
	void FpWorldMgr::Update()
	{
		// 更新每一个world
		fpWorld* pfWorld = NULL;
		m_mapFpWorld.ResetIterator();
		while (m_mapFpWorld.PeekNext(pfWorld))
		{
			pfWorld->Update();
			UpdateWorldState(pfWorld);
		}
	}

	//-------------------------------------------------------------------------------
	// 更新世界状态
	//-------------------------------------------------------------------------------
	void FpWorldMgr::UpdateWorldState(fpWorld* pWorld)
	{
		
	}

	//-------------------------------------------------------------------------------
	// 注册网络消息
	//-------------------------------------------------------------------------------
	void FpWorldMgr::RegisterFpWorldMsg()
	{
		/*m_pMsgCmdMgr->Register(NWL_Certification, m_Trunk.sfp2(&FpWorldMgr::HandleCertification), _T("NWL_Certification"));
		m_pMsgCmdMgr->Register(NWL_WorldStatus, m_Trunk.sfp2(&FpWorldMgr::HandleZoneServerStatus), _T("NWL_WorldStatus"));
		m_pMsgCmdMgr->Register(NWL_PlayerWillLogin, m_Trunk.sfp2(&FpWorldMgr::HandlePlayerWillLogin), _T("NWL_PlayerWillLogin"));
		m_pMsgCmdMgr->Register(NWL_PlayerLogin, m_Trunk.sfp2(&FpWorldMgr::HandlePlayerLogin), _T("NWL_PlayerLogin"));
		m_pMsgCmdMgr->Register(NWL_PlayerLogout, m_Trunk.sfp2(&FpWorldMgr::HandlePlayerLogout), _T("NWL_PlayerLogout"));
		m_pMsgCmdMgr->Register(NWL_KickLog, m_Trunk.sfp2(&FpWorldMgr::HandleKickLog), _T("NWL_KickLog"));
		m_pMsgCmdMgr->Register(NWL_WorldColsed, m_Trunk.sfp2(&FpWorldMgr::HandleWorldClosed), _T("NWL_WorldColsed"));*/
	}

	//------------------------------------------------------------------------
	// 反注册网络消息
	//------------------------------------------------------------------------
	void FpWorldMgr::UnRegisterFpWorldMsg()
	{
		//m_pMsgCmdMgr->Destroy();
	}

	//------------------------------------------------------------------------
	// 登陆回调
	//------------------------------------------------------------------------
	UINT32 FpWorldMgr::LoginCallBack(uchar* pByte, UINT32 dwSize)
	{
		return 0;
	}

	//----------------------------------------------------------------------------
	// 登出回调
	//----------------------------------------------------------------------------
	UINT32 FpWorldMgr::LogoutCallBack(UINT32 dwParam)
	{
		return 0;
	}

	//------------------------------------------------------------------------------
	// 添加到游戏世界
	//------------------------------------------------------------------------------
	void FpWorldMgr::AddToWorld(Player* pPlayer, UINT32 dwWorldNameCrc)
	{
		if (!P_VALID(pPlayer)) return;

		fpWorld* pWorld = GetFpWorld(dwWorldNameCrc);
		if (!P_VALID(pWorld)) return;

		pWorld->AddPlayer(pPlayer);

		// 写入数据库
		//sBeton.PlayerLogin(pPlayer->GetAccountID(), dwWorldNameCrc);
	}
}