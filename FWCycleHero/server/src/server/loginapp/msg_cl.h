//★Name				:	msg_base
//★Compiler			:	Microsoft Visual C++ 9.0
//★Version				:	1.00
//★Create Date			:	03/19/2013
//★LastModified		:	03/19/2013
//★Copyright (C)		:	
//★Writen  by			:   
//★Mode  by			:   
//★Brief				:	 游戏消息基类
//////////////////////////////////////////////////////////////////////////

#pragma once



namespace KBEngine
{

	enum  ENUM_MESSAGE_TYPE_CL_BASE
	{
		MTCLB_START				= 1000,

		CL_ProofAccount			= 1000,
		LC_ProofAccountRespond,
		CL_LoginApp,
		CL_Heartbest			= 1006,


		MTCLB_END,
	};
}


