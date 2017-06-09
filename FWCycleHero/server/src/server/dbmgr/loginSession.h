/*
---------------------------------------------------------------------------------------------------------
file name : loginSession.hpp
desc      : 登录服务器到 DBapp 的链接
author    : ljp

log		  : 2017-06-09 : 15:55
---------------------------------------------------------------------------------------------------------
*/
#pragma once
#include "helper/debug_helper.hpp"
#include "common/common.hpp"



#if KBE_PLATFORM == PLATFORM_WIN32
// window include
#else

// linux include
#endif




namespace KBEngine
{

	/*class SyncAppDatasHandler : public Task
	{
	public:
	struct ComponentInitInfo
	{
	COMPONENT_ID cid;
	COMPONENT_ORDER startGroupOrder;
	COMPONENT_ORDER startGlobalOrder;
	};

	SyncAppDatasHandler(NetSession& SessionDB );
	~SyncAppDatasHandler();

	bool							process();

	void							pushApp(COMPONENT_ID cid, COMPONENT_ORDER startGroupOrder, COMPONENT_ORDER startGlobalOrder);
	private:
	NetSession&						m_SessionDB;
	uint64							lastRegAppTime_;
	std::vector<ComponentInitInfo>	apps_;

	};*/
}

