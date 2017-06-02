#pragma once
#include "msg_cl.h"
#include "network/net_define.h"







namespace KBEngine
{
		#define MAX_MD5_ARRAY	36
		#pragma pack(push, 1)

		//--------------------------------------------------------------------------------------------
		// Login 错误代码
		//--------------------------------------------------------------------------------------------
		enum
		{
			E_ProofResult_Account_Ok = 0,
			E_ProofResult_Account_No_Match = 1,				//此帐号非法(帐户不存在或者密码不对,密保错误)
			E_ProofResult_Account_In_Use = 2,				//此帐号当前已经在使用
			E_ProofResult_Account_Freezed = 3,				//此帐号被停用
			E_ProofResult_Wrong_Build_Number = 4,			//版本号不对
			E_ProofResult_Disabled = 5,						//此帐号已经被封停
			E_ProofResult_Queue = 6,						//开始排队	
			E_ProofResult_Mibao_Error = 7,					//密保错误
			E_ProofResult_Wrong_Type = 8,					//非法验证策略
			E_ProofResult_Proofing = 9,						//正在验证或已经验证完成

			E_SelectWorld_Server_Full = 11,					//该服务器当前拥挤
			E_SelectWorld_Server_Suspended = 12,			//该服务器当前阻塞
			E_SelectWorld_Server_Maintenance = 13,			//该服务器当前维护
			E_SelectWorld_Failed = 14,						//出现异常，选择失败
			E_SelectWorld_Short_Time = 15,					//间隔时间太短,请稍候重试
			E_SelectWorld_No_Select = 16,					//你已经选择成功,不可再选
			E_SelectWorld_GameWorldName_Wrong = 17,			//非法的游戏世界名


			E_ProofResult_Forbid_MemberCenter = 18,			//您的会员中心账号已被封停，请联系客服
			E_ProofResult_Forbid_GMTool = 19,				//您的游戏账号已被封停，请联系客服	
			E_ProofResult_Forbid_CellPhone = 20,			//您的账号已被锁定，请联系客服
			E_ProofResult_Forbid_MiBao = 21,				//您的账号已经挂失密保卡，请完成解绑后登录
			E_SelecWordSystemError = 22,					// 服务器异常
		};

		//---------------------------------------------------------------------------------------------
		// 排队时间10分钟
		//---------------------------------------------------------------------------------------------
		#define QUEUE_TIME	10*60

		//-----------------------------------------------------------------------------
		// 验证消息
		//-----------------------------------------------------------------------------
		class MemoryStream;
		class CProofAccountNetCmd : public tagNetCmd
		{
		public:
			 CProofAccountNetCmd( );
			~CProofAccountNetCmd();

			virtual void	Serialize( MemoryStream* pNetStream );
			virtual void	UnSerialize(MemoryStream* pNetStream );
		public:
			char			szUserName[X_SHORT_NAME];		// 用户名
			char			szPsd[MAX_MD5_ARRAY];			// 密码
		
		};

		CMD_START(LC_ProofAccountRespond)
			uint32	errorCode;		// 错误码
		CMD_END


		class CProofAccountRespond : public tagNetCmd
		{
		public:
			 CProofAccountRespond();
			~CProofAccountRespond();

			virtual void	Serialize(MemoryStream* pNetStream);
			virtual void	UnSerialize(MemoryStream* pNetStream);
		public:
			uint32			errorCode;
		};

		class CLoginAppNetCmd : public tagNetCmd
		{
		public:
			CLoginAppNetCmd( );
			~CLoginAppNetCmd();

			virtual void	Serialize(MemoryStream* pNetStream);
			virtual void	UnSerialize(MemoryStream* pNetStream);
		public:
			uint32			dwErrorCode;					// 错误码
			uint32			dwAccountID;					// 帐号ID
			uint32			dwIP;							// ZoneServer IP 地址
			uint32			dwPort;							// ZoneServer 端口号
		};


		class CHeartbestNetCmd : public tagNetCmd
		{
		public:
			 CHeartbestNetCmd();
			~CHeartbestNetCmd();

			virtual void	Serialize(MemoryStream* pNetStream);
			virtual void	UnSerialize(MemoryStream* pNetStream);
		
		};

		
		
	#pragma pack(pop)
}

