#include "msg_login.h"
#include "common/memorystream.hpp"









namespace KBEngine
{
	
	/////////////////////////////////////////////////////////////////////////////////////////
	// 账号验证消息
	CProofAccountNetCmd::CProofAccountNetCmd() : tagNetCmd(CL_ProofAccount)
	{
		memset(this, 0, sizeof(*this));
	}

	CProofAccountNetCmd::~CProofAccountNetCmd()
	{

	}

	void CProofAccountNetCmd::Serialize( MemoryStream* pNetStream )
	{
		(*pNetStream) << dwID;
		(*pNetStream) << dwSize;

		(*pNetStream) << szUserName;
		(*pNetStream) << szPsd;
		
	}

	void CProofAccountNetCmd::UnSerialize(MemoryStream* pNetStream)
	{
		(*pNetStream) >> dwID;
		(*pNetStream) >> dwSize;

		(*pNetStream) >> szUserName;
		(*pNetStream) >> szPsd;
		
	}

	/// 账号验证返回
	CProofAccountRespond::CProofAccountRespond() : tagNetCmd(LC_ProofAccountRespond)
	{
		
	}

	CProofAccountRespond::~CProofAccountRespond()
	{

	}

	void CProofAccountRespond::Serialize(MemoryStream* pNetStream)
	{
		(*pNetStream) << dwID;
		(*pNetStream) << dwSize;

		(*pNetStream) << errorCode;

	}

	void CProofAccountRespond::UnSerialize(MemoryStream* pNetStream)
	{
		(*pNetStream) >> dwID;
		(*pNetStream) >> dwSize;

		(*pNetStream) >> errorCode;
	}

	/// 登录消息
	CLoginAppNetCmd::CLoginAppNetCmd() : tagNetCmd(CL_LoginApp)
	{
		
	}

	CLoginAppNetCmd::~CLoginAppNetCmd()
	{

	}

	void CLoginAppNetCmd::Serialize(MemoryStream* pNetStream)
	{
		(*pNetStream) << dwID;
		(*pNetStream) << dwSize;

		(*pNetStream) << dwErrorCode;
		(*pNetStream) << dwAccountID;
		(*pNetStream) << dwIP;
		(*pNetStream) << dwPort;
	}

	void CLoginAppNetCmd::UnSerialize(MemoryStream* pNetStream)
	{
		(*pNetStream) >> dwID;
		(*pNetStream) >> dwSize;

		(*pNetStream) >> dwErrorCode;
		(*pNetStream) >> dwAccountID;
		(*pNetStream) >> dwIP;
		(*pNetStream) >> dwPort;
	}

	/// 心跳消息
	CHeartbestNetCmd::CHeartbestNetCmd() : tagNetCmd(CL_Heartbest)
	{
		
	}

	CHeartbestNetCmd::~CHeartbestNetCmd()
	{

	}

	void CHeartbestNetCmd::Serialize(MemoryStream* pNetStream)
	{
		(*pNetStream) << dwID;
		(*pNetStream) << dwSize;
	}

	void CHeartbestNetCmd::UnSerialize(MemoryStream* pNetStream)
	{
		(*pNetStream) >> dwID;
		(*pNetStream) >> dwSize;
	}
}