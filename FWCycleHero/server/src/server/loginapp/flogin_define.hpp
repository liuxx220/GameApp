/*
----------------------------------------------------------------------------
	file name : fplayer_mgr.hpp
	desc	  : 登录到到账号服务器的client 的管理
	author    : LJP

	log		  : [ 2016-02-28 ]
----------------------------------------------------------------------------
*/
#pragma once
#include "common/singleton.hpp"







namespace KBEngine
{
	//-----------------------------------------------------------------------------
	// 游戏世界状态
	//-----------------------------------------------------------------------------
	enum EWorldStatus
	{
		EWS_Well = 0,		// 良好
		EWS_InitNotDone = 1,		// 未初始化完毕
		EWS_SystemError = 2,		// 系统出现故障
		EWS_ProofError = 3,		// 验证服务器故障
	};

	//------------------------------------------------------------------------------
	// 认证类型
	//------------------------------------------------------------------------------
	enum EProofPolicy
	{
		EPP_Null = -1,
		EPP_Test = 0,		// 测试专用
		EPP_Own = 1,		// 自己公司验证策略
		EPP_XunLei = 2,		// xunlei

		EPP_End = 3
	};

	//------------------------------------------------------------------------------
	// 玩家登录时的状态
	//------------------------------------------------------------------------------
	enum  EPlayerLoginStatus
	{
		EPLS_Null = -1,		// 无效
		EPLS_OffLine = 0,		// 离线
		EPLS_Online = 1,		// 在线
		EPLS_Loging = 2,		// 登录中
		EPLS_Unknown = 3,		// 未知
	};

	//-----------------------------------------------------------------------------
	// 验证结果结构
	//-----------------------------------------------------------------------------
	struct tagProofResult
	{
		UINT32					dwClientID;			// 客户端ID
		UINT32					dwAccountID;		// 帐号ID
		bool					bGuard;				// 防沉迷
		UINT32					nGuardAccuTime;		// 防沉迷累积时间
		EPlayerLoginStatus		eLoginStatus;		// 登陆状态
		UINT32					dwWorldNameCrc;		// 当前登陆的游戏世界
		UINT32					dwFrobidMask;		// 封停掩码 每1位对应一种封停类型
		uchar					byPrivilege;		// 权限
		bool					bNeedMibao;			// 是否使用密保
		UINT32					dwPreLoginIP;		// 上次登录ip
		uchar					byReserved[2];		// 保留，对齐

		tagProofResult()
		{
			dwClientID = GT_INVALID;
			dwAccountID = GT_INVALID;
			bGuard = FALSE;
			nGuardAccuTime = 0;
			eLoginStatus = EPLS_OffLine;
			byPrivilege = 0;
			bNeedMibao = false;
			dwFrobidMask = 0;
			dwPreLoginIP = 0;
		}
	};


	struct tagProofResultFull : public tagProofResult
	{
		INT32 nRet;		// 返回结果
	};


	//----------------------------------------------------------------
	// 登录玩家基本信息 sizeof = 396
	//----------------------------------------------------------------
	struct tagAccountData
	{
		char*				szAccountName[32];
		uint32				dwIp;
		bool				bGuard;
	};

	//------------------------------------------------------------------------------
	// fpworld当前信息
	//------------------------------------------------------------------------------
	struct tagWorldInfo
	{

		UINT32			dwNameCrc;					// 名字CRC值
		UINT32			dwWorldID;					// worldid
		UINT32			dwIP;						// IP
		UINT32			nPort;						// 端口
		UINT32			nMaxOnlineNum;				// 最大在线人数
		UINT32			nCurOnlineNum;				// 当前在线人数
		bool			bAutoSeal;					// 是否自动
		bool			bValid;						// 当前是否连接
		char			szName[32];					// World名称
		EWorldStatus	eStatus;					// 当前状态


		tagWorldInfo()
		{
			memset(this, 0, sizeof(*this));
		}
	};
}

