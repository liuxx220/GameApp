//-----------------------------------------------------------------------------
//!\file net_define.h
//!\Auth:
//!
//!\date 2004-07-14
//! last 2004-07-14
//!
//!\brief 网络客户端通讯底层基本定义
//-----------------------------------------------------------------------------
#pragma once
#include "common/common.hpp"







namespace KBEngine 
{
	CONST INT32 GT_NET_BLOCK_TIME = 1000*150;	// 150 ms
	#pragma pack(push, 1)

	//-----------------------------------------------------------------------------
	// 网络消息基本结构
	//-----------------------------------------------------------------------------
	class MemoryStream;
	class tagNetCmd
	{
	public:
		  tagNetCmd(int32 ID){ dwID = ID; dwSize = dwSize = sizeof(*this); }
		 ~tagNetCmd(){}


		 virtual void	Serialize(MemoryStream* pNetStream )		{}
		 virtual void	UnSerialize(MemoryStream* pNetStream )		{}

	public:
		int32			dwID;
		int32			dwSize;
	};

	struct tagNetCmdE
	{
		int32			dwSize;
		int32			dwID;
	};

	//-----------------------------------------------------------------------------
	// 方便网络消息的定义
	//-----------------------------------------------------------------------------
	#ifndef CMD_START

	#define CMDT( name ) tag##name

	
	// 因为有了这个构造函数所以就不再是POD类型……
	#define CMD_START(name)											\
	struct  CMDT(name) : public tagNetCmdE{							\
			CMDT(name)(){ memset(this, 0, sizeof(*this));			\
						  dwID = name; dwSize = sizeof(*this);}
	#define CMD_END		};


	#endif

	#pragma pack(pop)


	#define X_LONG_NAME				256
	#define X_SHORT_NAME			32
} 
