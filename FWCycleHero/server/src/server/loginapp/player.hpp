/*
----------------------------------------------------------------------------
		file name : player.hpp
		desc	  : 登录服务器管理的登录的账号
		author    : LJP

		log		  : [ 2016-02-27 ]
----------------------------------------------------------------------------
*/
#ifndef _PLAYER_HPP_
#define _PLAYER_HPP_
#include <string>
#include "common/platform.hpp"
#include "fplayer_mgr.hpp"






namespace KBEngine
{

	//------------------------------------------------------------------------------
	// 玩家封停类型  会员中心封停0x01、GM工具封停0x02、手机锁0x04、密保卡0x08
	//------------------------------------------------------------------------------
	enum  EPlayerFrobidMask
	{
		EPLM_MemberCenter	= 0x01,		// 会员中心
		EPLM_GMTool			= 0x02,		// GM工具
		EPLM_CellPhone		= 0x04,		// 手机锁
		EPLM_MiBao			= 0x08,		// 密保卡封停
		EPLM_WaiGua			= 0x10		// 反外挂自动封停
	};

	//----------------------------------------------------------------
	// 登录玩家基本信息 sizeof = 396
	//----------------------------------------------------------------
	struct tagPlayerInfo
	{
		uint32				dwClientID;							// 客户端ID
		uint32				dwCDIndex;							// 网络ID
		uint32				dwAccountID;						// 帐号ID
		uchar				szAccountName[32];					// 帐号名称
		uint32				dwMibaoCrc;							// 根据
		uint32				dwFrobidMask;						// 封停掩码 每1位对应一种封停类型
		uint32				dwIP;								// 客户端IP
		uint32				dwWorldNameCrc;						// 游戏世界名称CRC值
		bool				bGuard;								// 防沉迷用户
		int32				nAccOLSec;							// 累计登录时间

		uchar				byPrivilege;						// 权限
	
		tagPlayerInfo()		{ memset(this, 0, sizeof(*this)); }
	};

	
	//-----------------------------------------------------------------------------
	// 玩家类
	//-----------------------------------------------------------------------------
	class Player
	{
	public:
		//-------------------------------------------------------------------------
		// 构造函数
		//-------------------------------------------------------------------------
		Player(uint32 dwClientID, uint32 dwCDIndex, uint32 dwIP, uint32 dwWorldID);
		~Player();

		//-------------------------------------------------------------------------
		// 各种Get
		//-------------------------------------------------------------------------
		uint32				GetAccountID()				{ return m_Data.dwAccountID;	}
		uint32				GetClientID()				{ return m_Data.dwClientID;		}
		uint32				GetCDIndex()				{ return m_Data.dwCDIndex;		}
		uint32				GetLoginWorldNameCrc()		{ return m_Data.dwWorldNameCrc; }
		
		//uint32				GetPreLoginTime()			{ return m_dwPreLoginTime;	}
		uint32				GetPreLoginIP()				{ return m_dwPreLoginIP;		}

		int32				GetVerifyCode()				{ return m_nVerifyCode;			}
		uchar				GetPrivilege()				{ return m_Data.byPrivilege;	}
		uchar*				GetAccoutName()				{ return m_Data.szAccountName;	}
		tagPlayerInfo&		GetPlayerData()				{ return m_Data;				}

		BOOL				GetForbidMask()				{ return m_Data.dwFrobidMask;	}
		BOOL				IsGuard()					{ return m_Data.bGuard;			}
		
		//-------------------------------------------------------------------------
		// 玩家信息相关
		//-------------------------------------------------------------------------
		void				ProofReturn(tagProofResult* pResult);

		//-------------------------------------------------------------------------
		// 连接相关
		//-------------------------------------------------------------------------
		void				SetConnLost()				{ InterlockedExchange((LPLONG)(&m_bConnLost), TRUE); }
		bool				IsConnLost()				{ return m_bConnLost; }

		//-------------------------------------------------------------------------
		// 网络消息相关
		//-------------------------------------------------------------------------
		static void			RegisterPlayerMsg();
		static void			UnRegisterPlayerMsg();

		INT32				HandleMessage();
		
		//-------------------------------------------------------------------------
		// 验证相关
		//-------------------------------------------------------------------------
		void				SetPreLoginIP(DWORD dwIP)		{ m_dwPreLoginIP = dwIP; }


		bool				IsNeedMibao()					{ return m_bNeedMibao; }
		bool				IsProofing()					{ return m_bProofing; }
		bool				IsProofEnd()					{ return m_bProofEnd; }
		void				SetProofing()					{ m_bProofing = TRUE; m_bProofEnd = FALSE; }
		void				SetProofEnd()					{ m_bProofEnd = TRUE; m_bProofing = FALSE; }

		bool				IsSelectWorldOk()				{ return m_bSelectWorldOk; }
		void				SetSelectWorldOK()				{ m_bSelectWorldOk = TRUE; }

	private:
		//-----------------------------------------------------------------------
		// 消息处理函数
		//-----------------------------------------------------------------------
		UINT32				HandleProof(tagNetCmd* pCmd);
		
		//-----------------------------------------------------------------------
		// 消息相关
		//-----------------------------------------------------------------------
		void	            SendMsg(uchar* pMsg, uint32 dwSize);

		//-----------------------------------------------------------------------
		// 辅助函数
		//-----------------------------------------------------------------------
		bool				CheckName(std::string& str);
	private:
		
		tagPlayerInfo		m_Data;

		int32				m_nVerifyCode;			// 验证码
		bool				m_bNeedMibao;			// 是否需要密保验证
		bool				m_bProofing;			// 是否正在进行验证
		bool				m_bProofEnd;			// 是否已经验证完毕
		bool				m_bSelectWorldOk;		// 是否已经选择游戏世界成功

		volatile bool		m_bConnLost;			// 连接是否已经断开
		uint32				m_dwPreLoginIP;			//上次登录ip

	};
	
}
#endif




