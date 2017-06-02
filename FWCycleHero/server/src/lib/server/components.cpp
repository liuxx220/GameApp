/*
------------------------------------------------------------------------
		file name : components.hpp
		desc	  : 服务器 exe 的管理和 exe 信息的描述
		author    : LJP

		log		  : [2015-04-25]
------------------------------------------------------------------------
*/
#include "components.hpp"
#include "helper/debug_helper.hpp"
#include "helper/sys_info.hpp"
#include "server/serverconfig.hpp"





namespace KBEngine
{

	//int32 Components::ANY_UID = -1;
	//KBE_SINGLETON_INIT(Components);
	//Components _g_components;

	////-------------------------------------------------------------------------------------
	//Components::Components():
	//Task(),
	//_baseapps(),
	//_cellapps(),
	//_dbmgrs(),
	//_loginapps(),
	//_cellappmgrs(),
	//_baseappmgrs(),
	//_machines(),
	//_messagelogs(),
	//_billings(),
	//_bots(),
	//_consoles(),
	//_pNetSession(NULL),
	//_globalOrderLog(),
	//_baseappGrouplOrderLog(),
	//_cellappGrouplOrderLog(),
	//_loginappGrouplOrderLog(),
	//_pHandler(NULL),
	//componentType_(UNKNOWN_COMPONENT_TYPE),
	//componentID_(0),
	//state_(0),
	//findIdx_(0)
	//{

	//}

	////-------------------------------------------------------------------------------------
	//Components::~Components()
	//{
	//}

	////-------------------------------------------------------------------------------------
	//void Components::initialize(NetSession * pNetSession, COMPONENT_TYPE componentType, COMPONENT_ID componentID)
	//{ 
	//	KBE_ASSERT(pNetSession != NULL); 
	//	_pNetSession	= pNetSession; 
	//	componentType_		= componentType;
	//	componentID_		= componentID;

	//	for(uint8 i=0; i<8; i++)
	//		findComponentTypes_[i] = UNKNOWN_COMPONENT_TYPE;

	//	switch(componentType_)
	//	{
	//	case CELLAPP_TYPE:
	//		findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		findComponentTypes_[1] = DBMGR_TYPE;
	//		findComponentTypes_[2] = CELLAPPMGR_TYPE;
	//		findComponentTypes_[3] = BASEAPPMGR_TYPE;
	//		break;
	//	case BASEAPP_TYPE:
	//		findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		findComponentTypes_[1] = DBMGR_TYPE;
	//		findComponentTypes_[2] = BASEAPPMGR_TYPE;
	//		findComponentTypes_[3] = CELLAPPMGR_TYPE;
	//		break;
	//	case BASEAPPMGR_TYPE:
	//		findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		findComponentTypes_[1] = DBMGR_TYPE;
	//		findComponentTypes_[2] = CELLAPPMGR_TYPE;
	//		break;
	//	case CELLAPPMGR_TYPE:
	//		findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		findComponentTypes_[1] = DBMGR_TYPE;
	//		findComponentTypes_[2] = BASEAPPMGR_TYPE;
	//		break;
	//	case LOGINAPP_TYPE:
	//		findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		findComponentTypes_[1] = DBMGR_TYPE;
	//		findComponentTypes_[2] = BASEAPPMGR_TYPE;
	//		break;
	//	case DBMGR_TYPE:
	//		findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		break;
	//	default:
	//		if(componentType_ != MESSAGELOG_TYPE && 
	//			componentType_ != MACHINE_TYPE && 
	//			componentType_ != BILLING_TYPE)
	//			findComponentTypes_[0] = MESSAGELOG_TYPE;
	//		break;
	//	};
	//}

	////-------------------------------------------------------------------------------------
	//void Components::finalise()
	//{
	//	clear(0, false);
	//}

	////-------------------------------------------------------------------------------------
	//bool Components::checkComponents(int32 uid, COMPONENT_ID componentID, uint32 pid)
	//{
	//	if(componentID <= 0)
	//		return true;

	//	int idx = 0;
	//	while(true)
	//	{
	//		COMPONENT_TYPE ct = ALL_COMPONENT_TYPES[idx++];
	//		if(ct == UNKNOWN_COMPONENT_TYPE)
	//			break;

	//		ComponentInfos* cinfos = findComponent(ct, uid, componentID);
	//		if(cinfos != NULL)
	//		{
	//			if(cinfos->componentType != MACHINE_TYPE && cinfos->pid != 0 /* 等于0通常是预设， 这种情况我们先不作比较 */ && pid != cinfos->pid)
	//			{
	//				ERROR_MSG(fmt::format("Components::checkComponents: uid:{}, componentType={}, componentID:{} exist.\n",
	//					uid, COMPONENT_NAME_EX(ct), componentID));

	//				KBE_ASSERT(false && "Components::checkComponents: componentID exist.\n");
	//			}
	//			return false;
	//		}
	//	}

	//	return true;
	//}

	////-------------------------------------------------------------------------------------		
	//void Components::addComponent(int32 uid, const char* username, 
	//			COMPONENT_TYPE componentType, COMPONENT_ID componentID, COMPONENT_ORDER globalorderid, COMPONENT_ORDER grouporderid,
	//			uint32 intaddr, uint16 intport, 
	//			uint32 extaddr, uint16 extport, std::string& extaddrEx, uint32 pid,
	//			float cpu, float mem, uint32 usedmem, uint64 extradata, uint64 extradata1, uint64 extradata2, uint64 extradata3,
	//			Channel* pChannel)
	//{

	//	COMPONENTS& components = getComponents(componentType);
	//	if(!checkComponents(uid, componentID, pid))
	//		return;

	//	ComponentInfos* cinfos = findComponent(componentType, uid, componentID);
	//	if(cinfos != NULL)
	//	{
	//		WARNING_MSG(fmt::format("Components::addComponent[{}]: uid:{}, username:{}, "
	//			"componentType:{}, componentID:{} is exist!\n",
	//			COMPONENT_NAME_EX(componentType), uid, username, (int32)componentType, componentID));
	//		return;
	//	}
	//
	//	ComponentInfos componentInfos;

	//	componentInfos.pIntAddr.reset(new Address(intaddr, intport));
	//	componentInfos.pExtAddr.reset(new Address(extaddr, extport));

	//	if(extaddrEx.size() > 0)
	//		strncpy(componentInfos.externalAddressEx, extaddrEx.c_str(), MAX_NAME);
	//
	//	componentInfos.uid		= uid;
	//	componentInfos.cid		= componentID;
	//	componentInfos.pChannel = pChannel;
	//	componentInfos.componentType	= componentType;
	//	componentInfos.groupOrderid		= 1;
	//	componentInfos.globalOrderid	= 1;

	//	componentInfos.mem		= mem;
	//	componentInfos.cpu		= cpu;
	//	componentInfos.usedmem	= usedmem;
	//	componentInfos.extradata  = extradata;
	//	componentInfos.extradata1 = extradata1;
	//	componentInfos.extradata2 = extradata2;
	//	componentInfos.extradata3 = extradata3;
	//	componentInfos.pid		  = pid;

	//	
	//	strncpy(componentInfos.username, username, MAX_NAME);

	//	_globalOrderLog[uid]++;

	//	switch(componentType)
	//	{
	//	case BASEAPP_TYPE:
	//		_baseappGrouplOrderLog[uid]++;
	//		componentInfos.groupOrderid = _baseappGrouplOrderLog[uid];
	//		break;
	//	case CELLAPP_TYPE:
	//		_cellappGrouplOrderLog[uid]++;
	//		componentInfos.groupOrderid = _cellappGrouplOrderLog[uid];
	//		break;
	//	case LOGINAPP_TYPE:
	//		_loginappGrouplOrderLog[uid]++;
	//		componentInfos.groupOrderid = _loginappGrouplOrderLog[uid];
	//		break;
	//	default:
	//		break;
	//	};
	//
	//	if(grouporderid > 0)
	//		componentInfos.groupOrderid = grouporderid;

	//	if(globalorderid > 0)
	//		componentInfos.globalOrderid = globalorderid;
	//	else
	//		componentInfos.globalOrderid = _globalOrderLog[uid];

	//	if(cinfos == NULL)
	//		components.push_back(componentInfos);
	//	else
	//		*cinfos = componentInfos;

	//	INFO_MSG(fmt::format("Components::addComponent[{}], uid={}, "
	//		     "componentID={}, globalorderid={}, grouporderid={}, totalcount={}\n",
	//			 COMPONENT_NAME_EX(componentType), 
	//			 uid,
	//			 componentID, 
	//			 ((int32)componentInfos.globalOrderid),
	//			 ((int32)componentInfos.groupOrderid),
	//			 components.size()));
	//}

	////-------------------------------------------------------------------------------------	
	//// function :
	//// desc		: 删除一个组件 
	////-------------------------------------------------------------------------------------
	//void Components::delComponent(int32 uid, COMPONENT_TYPE componentType, 
	//							  COMPONENT_ID componentID, bool ignoreComponentID, bool shouldShowLog)
	//{
	//	COMPONENTS& components = getComponents(componentType);
	//	COMPONENTS::iterator iter = components.begin();
	//	for(; iter != components.end();)
	//	{
	//		if((uid < 0 || (*iter).uid == uid) && (ignoreComponentID == true || (*iter).cid == componentID))
	//		{
	//			INFO_MSG(fmt::format("Components::delComponent[{}] componentID={}, component:totalcount={}.\n", 
	//				COMPONENT_NAME_EX(componentType), componentID, components.size()));

	//			ComponentInfos* componentInfos = &(*iter);
	//			if(_pHandler)
	//				_pHandler->onRemoveComponent(componentInfos);

	//			iter = components.erase(iter);
	//			if(!ignoreComponentID)
	//				return;
	//		}
	//		else
	//			iter++;
	//	}

	//	if(shouldShowLog)
	//	{
	//		ERROR_MSG(fmt::format("Components::delComponent::not found [{}] component:totalcount:{}\n", 
	//				  COMPONENT_NAME_EX(componentType), components.size()));
	//	}
	//}

	////-------------------------------------------------------------------------------------		
	//void Components::removeComponentFromChannel(Channel * pChannel, bool isShutingdown)
	//{
	//	int ifind = 0;
	//	while(ALL_COMPONENT_TYPES[ifind] != UNKNOWN_COMPONENT_TYPE)
	//	{
	//		COMPONENT_TYPE componentType = ALL_COMPONENT_TYPES[ifind++];
	//		COMPONENTS& components		 = getComponents(componentType);
	//		COMPONENTS::iterator iter	 = components.begin();
	//		for(; iter != components.end();)
	//		{
	//			if((*iter).pChannel == pChannel)
	//			{
	//				if(!isShutingdown)
	//				{
	//					ERROR_MSG(fmt::format("Components::removeComponentFromChannel: {} : {}, Abnormal exit.\n",
	//						COMPONENT_NAME_EX(componentType), (*iter).cid));

	//#if KBE_PLATFORM == PLATFORM_WIN32
	//					printf("[ERROR]: %s.\n", (fmt::format("Components::removeComponentFromChannel: {} : {}, Abnormal exit!\n",
	//						COMPONENT_NAME_EX(componentType), (*iter).cid)).c_str());
	//#endif
	//				}
	//				else
	//				{
	//					INFO_MSG(fmt::format("Components::removeComponentFromChannel: {} : {}, Normal exit!\n",
	//						COMPONENT_NAME_EX(componentType), (*iter).cid));
	//				}

	//				ComponentInfos* componentInfos = &(*iter);
	//				if(_pHandler)
	//					_pHandler->onRemoveComponent(componentInfos);

	//				iter = components.erase(iter);
	//				return;
	//			}
	//			else
	//				iter++;
	//		}
	//	}
	//}

	////-------------------------------------------------------------------------------------		
	//int Components::connectComponent(COMPONENT_TYPE componentType, int32 uid, COMPONENT_ID componentID)
	//{
	//	Components::ComponentInfos* pComponentInfos = findComponent(componentType, uid, componentID);
	//	KBE_ASSERT(pComponentInfos != NULL);

	//	EndPoint * pEndpoint = new EndPoint;
	//	pEndpoint->socket(SOCK_STREAM);
	//	if (!pEndpoint->good())
	//	{
	//		ERROR_MSG("Components::connectComponent: couldn't create a socket\n");
	//		delete pEndpoint;
	//		return -1;
	//	}

	//	pEndpoint->addr(*pComponentInfos->pIntAddr);
	//	int ret = pEndpoint->connect(pComponentInfos->pIntAddr->port, pComponentInfos->pIntAddr->ip);
	//	if(ret == 0)
	//	{
	//		pComponentInfos->pChannel = new Channel( pEndpoint, Channel::INTERNAL);
	//		if(!_pNetSession->registerChannel(pComponentInfos->pChannel))
	//		{
	//			ERROR_MSG(fmt::format("Components::connectComponent: registerChannel({}) is failed!\n",
	//					  pComponentInfos->pChannel->c_str()));

	//			pComponentInfos->pChannel->destroy();
	//		

	//			// 此时不可强制释放内存，destroy中已经对其减引用
	//			// SAFE_RELEASE(pComponentInfos->pChannel);
	//			pComponentInfos->pChannel = NULL;
	//			return -1;
	//		}
	//	}
	//	else
	//	{
	//		ERROR_MSG(fmt::format("Components::connectComponent: connect({}) is failed! {}.\n",
	//				  pComponentInfos->pIntAddr->c_str(), kbe_strerror()));

	//		delete pEndpoint;
	//		return -1;
	//	}

	//	return ret;
	//}

	////-------------------------------------------------------------------------------------		
	//void Components::clear(int32 uid, bool shouldShowLog)
	//{
	//	delComponent(uid, DBMGR_TYPE, uid, true, shouldShowLog);
	//	delComponent(uid, BASEAPPMGR_TYPE, uid, true, shouldShowLog);
	//	delComponent(uid, CELLAPPMGR_TYPE, uid, true, shouldShowLog);
	//	delComponent(uid, CELLAPP_TYPE, uid, true, shouldShowLog);
	//	delComponent(uid, BASEAPP_TYPE, uid, true, shouldShowLog);
	//	delComponent(uid, LOGINAPP_TYPE, uid, true, shouldShowLog);
	//	//delComponent(uid, MESSAGELOG_TYPE, uid, true, shouldShowLog);
	//}

	////-------------------------------------------------------------------------------------		
	//Components::COMPONENTS& Components::getComponents(COMPONENT_TYPE componentType)
	//{
	//	switch(componentType)
	//	{
	//	case DBMGR_TYPE:
	//		return _dbmgrs;
	//	case LOGINAPP_TYPE:
	//		return _loginapps;
	//	case BASEAPPMGR_TYPE:
	//		return _baseappmgrs;
	//	case CELLAPPMGR_TYPE:
	//		return _cellappmgrs;
	//	case CELLAPP_TYPE:
	//		return _cellapps;
	//	case BASEAPP_TYPE:
	//		return _baseapps;
	//	case MACHINE_TYPE:
	//		return _machines;
	//	case MESSAGELOG_TYPE:
	//		return _messagelogs;			
	//	case BILLING_TYPE:
	//		return _billings;	
	//	case BOTS_TYPE:
	//		return _bots;	
	//	default:
	//		break;
	//	};

	//	return _consoles;
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::findComponent(COMPONENT_TYPE componentType, int32 uid,
	//													  COMPONENT_ID componentID)
	//{
	//	COMPONENTS& components = getComponents(componentType);
	//	COMPONENTS::iterator iter = components.begin();
	//	for(; iter != components.end(); iter++)
	//	{
	//		if((*iter).uid == uid && (componentID == 0 || (*iter).cid == componentID))
	//			return &(*iter);
	//	}

	//	return NULL;
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::findComponent(COMPONENT_TYPE componentType, COMPONENT_ID componentID)
	//{
	//	COMPONENTS& components = getComponents(componentType);
	//	COMPONENTS::iterator iter = components.begin();
	//	for(; iter != components.end(); iter++)
	//	{
	//		if(componentID == 0 || (*iter).cid == componentID)
	//			return &(*iter);
	//	}

	//	return NULL;
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::findComponent(COMPONENT_ID componentID)
	//{
	//	int idx = 0;
	//	int32 uid = getUserUID();

	//	while(true)
	//	{
	//		COMPONENT_TYPE ct = ALL_COMPONENT_TYPES[idx++];
	//		if(ct == UNKNOWN_COMPONENT_TYPE)
	//			break;

	//		ComponentInfos* cinfos = findComponent(ct, uid, componentID);
	//		if(cinfos != NULL)
	//		{
	//			return cinfos;
	//		}
	//	}

	//	return NULL;
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::findComponent(Channel * pChannel)
	//{
	//	int ifind = 0;
	//	while(ALL_COMPONENT_TYPES[ifind] != UNKNOWN_COMPONENT_TYPE)
	//	{
	//		COMPONENT_TYPE componentType = ALL_COMPONENT_TYPES[ifind++];
	//		COMPONENTS& components = getComponents(componentType);
	//		COMPONENTS::iterator iter = components.begin();

	//		for(; iter != components.end(); iter++)
	//		{
	//			if((*iter).pChannel == pChannel)
	//			{
	//				return &(*iter);
	//			}
	//		}
	//	}

	//	return NULL;
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::findLocalComponent(uint32 pid)
	//{
	//	int ifind = 0;
	//	while(ALL_COMPONENT_TYPES[ifind] != UNKNOWN_COMPONENT_TYPE)
	//	{
	//		COMPONENT_TYPE componentType = ALL_COMPONENT_TYPES[ifind++];
	//		COMPONENTS& components = getComponents(componentType);
	//		COMPONENTS::iterator iter = components.begin();

	//		for(; iter != components.end(); iter++)
	//		{
	//			if(isLocalComponent(&(*iter)) && (*iter).pid == pid)
	//			{
	//				return &(*iter);
	//			}
	//		}
	//	}

	//	return NULL;
	//}

	////-------------------------------------------------------------------------------------		
	//bool Components::isLocalComponent(const Components::ComponentInfos* info)
	//{
	//	return _pNetSession->intaddr().ip == info->pIntAddr->ip ||
	//			_pNetSession->extaddr().ip == info->pIntAddr->ip;
	//}

	////-------------------------------------------------------------------------------------		
	//const Components::ComponentInfos* Components::lookupLocalComponentRunning(uint32 pid)
	//{
	//	if(pid > 0)
	//	{
	//		SystemInfo::PROCESS_INFOS sysinfos = SystemInfo::getSingleton().getProcessInfo(pid);
	//		if(sysinfos.error)
	//		{
	//			return NULL;
	//		}
	//		else
	//		{
	//			Components::ComponentInfos* winfo = findLocalComponent(pid);

	//			if(winfo)
	//			{
	//				winfo->cpu = sysinfos.cpu;
	//				winfo->usedmem = (uint32)sysinfos.memused;

	//				winfo->mem = float((winfo->usedmem * 1.0 / SystemInfo::getSingleton().totalmem()) * 100.0);
	//			}

	//			return winfo;
	//		}
	//	}

	//	return NULL;
	//}

	////-------------------------------------------------------------------------------------		
	//bool Components::checkComponentPortUsable(const Components::ComponentInfos* info)
	//{
	//	
	//	return true;
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::getBaseappmgr()
	//{
	//	return findComponent(BASEAPPMGR_TYPE, getUserUID(), 0);
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::getCellappmgr()
	//{
	//	return findComponent(CELLAPPMGR_TYPE, getUserUID(), 0);
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::getDbmgr()
	//{
	//	return findComponent(DBMGR_TYPE, getUserUID(), 0);
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::getMessagelog()
	//{
	//	return findComponent(MESSAGELOG_TYPE, getUserUID(), 0);
	//}

	////-------------------------------------------------------------------------------------		
	//Components::ComponentInfos* Components::getBillings()
	//{
	//	return findComponent(BILLING_TYPE, getUserUID(), 0);
	//}

	////-------------------------------------------------------------------------------------		
	//Channel* Components::getBaseappmgrChannel()
	//{
	//	Components::ComponentInfos* cinfo = getBaseappmgr();
	//	if(cinfo == NULL)
	//		 return NULL;

	//	return cinfo->pChannel;
	//}

	////-------------------------------------------------------------------------------------		
	//Channel* Components::getCellappmgrChannel()
	//{
	//	Components::ComponentInfos* cinfo = getCellappmgr();
	//	if(cinfo == NULL)
	//		 return NULL;

	//	return cinfo->pChannel;
	//}

	////-------------------------------------------------------------------------------------		
	//Channel* Components::getDbmgrChannel()
	//{
	//	Components::ComponentInfos* cinfo = getDbmgr();
	//	if(cinfo == NULL)
	//		 return NULL;

	//	return cinfo->pChannel;
	//}

	////-------------------------------------------------------------------------------------		
	//Channel* Components::getMessagelogChannel()
	//{
	//	Components::ComponentInfos* cinfo = getMessagelog();
	//	if(cinfo == NULL)
	//		 return NULL;

	//	return cinfo->pChannel;
	//}

	////-------------------------------------------------------------------------------------	
	//size_t Components::getGameSrvComponentsSize()
	//{
	//	COMPONENTS	_baseapps;
	//	COMPONENTS	_cellapps;
	//	COMPONENTS	_dbmgrs;
	//	COMPONENTS	_loginapps;
	//	COMPONENTS	_cellappmgrs;
	//	COMPONENTS	_baseappmgrs;

	//	return _baseapps.size() + _cellapps.size() + _dbmgrs.size() + 
	//		_loginapps.size() + _cellappmgrs.size() + _baseappmgrs.size();
	//}

	////-------------------------------------------------------------------------------------
	//EventDispatcher & Components::dispatcher()
	//{
	//	return pNetSession()->dispatcher();
	//}

	////-------------------------------------------------------------------------------------
	//void Components::onChannelDeregister(Channel * pChannel, bool isShutingdown)
	//{
	//	removeComponentFromChannel(pChannel, isShutingdown);
	//}

	////-------------------------------------------------------------------------------------
	//bool Components::findInterfaces()
	//{
	//
	//	return true;
	//}
	////-------------------------------------------------------------------------------------
	//bool Components::process()
	//{
	//	if(componentType_ == MACHINE_TYPE)
	//		return false;


	//	return false;
	//}

	////-------------------------------------------------------------------------------------		
	//
}
