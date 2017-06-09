/*
----------------------------------------------------------------------------
	file name : fplayer_mgr.hpp
	desc	  : 登录到到账号服务器的client 的管理
	author    : LJP

	log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#ifndef FPLAYER_MGR_HPP
#define FPLAYER_MGR_HPP
#include "common/singleton.hpp"
#include "network/safe_map.h"
#include "network/simple_map.h"
#include "network/safe_list.h"
#include "network/event_dispatcher.hpp"
#include "proof_Policy.hpp"
#include "LoginSessionMgr.h"
#include "flogin_define.hpp"






namespace KBEngine
{
	
	class Player;
	class CLoginPolicy;
	class CPlayerMgr : public Singleton<CPlayerMgr>
	{

	public:
		 CPlayerMgr();
		~CPlayerMgr();


		//--------------------------------------------------------------------------
		// 初始化，更新和销毁
		//--------------------------------------------------------------------------
		bool				Init( void );
		void				Update( void );
		void				Destroy( void );
		void				InitNetWork(void);

		//--------------------------------------------------------------------------
		// 各种Get
		//--------------------------------------------------------------------------
		uint32				GetCurrVer()		{ return m_dwCurrVer; }
		int32				GetPlayerNum()		{ return m_nPlayerNum; }
		int32				GetPlayerLoging()	{ return m_nPlayerLoging; }
		int32				GetProofResultNum()	{ return m_nProofResultNum; }
		EProofPolicy		GetProofPolicy()	{ return m_ePolicy; }
		CLoginPolicy*		GetProofPolicyPtr()	{ return m_pPolicy; }

		//--------------------------------------------------------------------------
		// 网络包相关
		//--------------------------------------------------------------------------
		void				SendMsg(UINT32 dwCDIndex, void* pMsg, UINT32 dwSize);
		
		//--------------------------------------------------------------------------
		// 踢人
		//--------------------------------------------------------------------------
		void				Kick(UINT32 dwCDIndex)		{ m_pDispatcher->KickPlayer(dwCDIndex); }

		//--------------------------------------------------------------------------
		// ID生成
		//--------------------------------------------------------------------------
		uint32				GenerateClientID()				{ ++m_dwClientIDGen; return m_dwClientIDGen; }

		//--------------------------------------------------------------------------
		// 玩家相关
		//--------------------------------------------------------------------------
		void				AddPlayerToAll(Player* pPlayer);
		void				AddPlayerToLogining(Player* pPlayer);
		void				RemovePlayerFromAll(UINT32 dwClientID)		{ m_mapAllPlayer.Erase(dwClientID); }
		void				RemovePlayerFromLogining(UINT32 dwClientID)	{ m_mapLoginingPlayer.Erase(dwClientID); }
		void				PlayerLogout(Player* pPlayer);

		void				AddAccount(UINT32 dwAccountID, UINT32 dwClientID);
		void				RemoveAccount(UINT32 dwAccountID);
		bool				IsAccountExist(UINT32 dwAccountID)			{ return m_mapAccountIDClientID.IsExist(dwAccountID); }


		//--------------------------------------------------------------------------
		// 验证相关
		//--------------------------------------------------------------------------
		void				Proof(UINT32 dwClientID, char* szAccoutName, char* szPsd, char* szGUID);
		
	
	private:
		//--------------------------------------------------------------------------
		// 初始化配置文件
		//--------------------------------------------------------------------------
		bool				InitConfig();

		//--------------------------------------------------------------------------
		// 更新
		//--------------------------------------------------------------------------
		void				UpdateSession();
		void				UpdateProofResult(){}

		//--------------------------------------------------------------------------
		// 登陆登出回调
		//--------------------------------------------------------------------------
		uint32				LoginCallBack(void*, void*);
		uint32				LogoutCallBack(void*);

	private:
		
		//--------------------------------------------------------------------------
		// 网络相关
		//--------------------------------------------------------------------------
		int32								m_nPort;

		//--------------------------------------------------------------------------
		// ID生成器
		//--------------------------------------------------------------------------
		uint32								m_dwClientIDGen;

		//--------------------------------------------------------------------------
		// 版本号和类型
		//--------------------------------------------------------------------------
		uint32								m_dwCurrVer;

		//--------------------------------------------------------------------------
		// 统计信息
		//--------------------------------------------------------------------------
		volatile int32						m_nPlayerNum;
		volatile int32						m_nPlayerLoging;
		volatile int32						m_nProofResultNum;

		//--------------------------------------------------------------------------
		// 玩家相关
		//--------------------------------------------------------------------------
		TSafeMap<UINT32, Player*>			m_mapAllPlayer;			// 所有的Player,以ClientID 为 key
		TMap<UINT32, Player*>				m_mapLoginingPlayer;	// 正在登录的玩家
		TMap<UINT32, UINT32>				m_mapAccountIDClientID;	// 在线的AccountID和ClientID对应表
		TSafeList<tagProofResultFull*>		m_listProofResult;		// 返回成功的玩家

		TMap<UINT32, tagAccountData*>		m_mapAccountData;		// 所有的AccountID的缓冲数据，包括AccountName，guard，ip 初始化加载，动态更新
		TMap<UINT32, UINT32>				m_mapAccountNameCrc2AccountID;	// accountid -> namecrc	初始化时加载，并且动态添加

		//--------------------------------------------------------------------------
		// 验证方法
		//--------------------------------------------------------------------------
		EProofPolicy						m_ePolicy;				// 验证方式
		CLoginPolicy*						m_pPolicy;				// 验证策略

		CLoginSessionMgr*					m_pNetSessionMgr;
		EventDispatcher*					m_pDispatcher;			// 网络消息派送器
	};

	#define sPlayerMgr CPlayerMgr::getSingleton()
}

#endif
