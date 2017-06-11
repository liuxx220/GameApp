/*
--------------------------------------------------------------------------------
		file name : serverconfig.hpp
		desc	  : 服务器配置信息
		author	  : LJP

		log		  : [2015-04-25]
--------------------------------------------------------------------------------
*/
#include "serverconfig.hpp"
#include "network/common.hpp"
#include "server/glw_resmgr.hpp"
#include "common/kbeversion.hpp"

#ifndef CODE_INLINE
#include "serverconfig.ipp"
#endif

namespace KBEngine{
KBE_SINGLETON_INIT(ServerConfig);

//-------------------------------------------------------------------------------------
ServerConfig::ServerConfig():
gameUpdateHertz_(10),
tick_max_sync_logs_(32),
shutdown_time_(1.f),
shutdown_waitTickTime_(1.f),
callback_timeout_(180.f),
thread_timeout_(300.f),
thread_init_create_(1),
thread_pre_create_(2),
thread_max_create_(8)
{

}

//-------------------------------------------------------------------------------------
ServerConfig::~ServerConfig()
{
}

//-------------------------------------------------------------------------------------
bool ServerConfig::loadConfig(std::string fileName)
{
	TiXmlNode* node = NULL, *rootNode = NULL;
	XmlPlus* xml = new XmlPlus(Resmgr::getSingleton().matchRes(fileName).c_str());

	if(!xml->isGood())
	{
		ERROR_MSG(fmt::format("ServerConfig::loadConfig: load {} is failed!\n",fileName.c_str()));
		SAFE_RELEASE(xml);
		return false;
	}
	

	rootNode = xml->getRootNode("packetAlwaysContainLength");
	if(rootNode != NULL){
		g_packetAlwaysContainLength = xml->getValInt(rootNode) != 0;
	}

	rootNode = xml->getRootNode("shutdown_time");
	if(rootNode != NULL)
	{
		shutdown_time_ = float(xml->getValFloat(rootNode));
	}
	
	rootNode = xml->getRootNode("shutdown_waittick");
	if(rootNode != NULL)
	{
		shutdown_waitTickTime_ = float(xml->getValFloat(rootNode));
	}

	rootNode = xml->getRootNode("callback_timeout");
	if(rootNode != NULL)
	{
		callback_timeout_ = float(xml->getValFloat(rootNode));
		if(callback_timeout_ < 5.f)
			callback_timeout_ = 5.f;
	}
	
	rootNode = xml->getRootNode("thread_pool");
	if(rootNode != NULL)
	{
		TiXmlNode* childnode = xml->enterNode(rootNode, "timeout");
		if(childnode)
		{
			thread_timeout_ = float(KBE_MAX(1.0, xml->getValFloat(childnode)));
		}

		childnode = xml->enterNode(rootNode, "init_create");
		if(childnode)
		{
			thread_init_create_ = KBE_MAX(1, xml->getValInt(childnode));
		}

		childnode = xml->enterNode(rootNode, "pre_create");
		if(childnode)
		{
			thread_pre_create_ = KBE_MAX(1, xml->getValInt(childnode));
		}

		childnode = xml->enterNode(rootNode, "max_create");
		if(childnode)
		{
			thread_max_create_ = KBE_MAX(1, xml->getValInt(childnode));
		}
	}

	rootNode = xml->getRootNode("gameUpdateHertz");
	if(rootNode != NULL){
		gameUpdateHertz_ = xml->getValInt(rootNode);
	}

	rootNode = xml->getRootNode("bitsPerSecondToClient");
	if(rootNode != NULL){
		bitsPerSecondToClient_ = xml->getValInt(rootNode);
	}

	
	rootNode = xml->getRootNode("cellapp");
	if(rootNode != NULL)
	{
		TiXmlNode* aoiNode = xml->enterNode(rootNode, "defaultAoIRadius");
		if(aoiNode != NULL)
		{
			node = NULL;
			node = xml->enterNode(aoiNode, "radius");
			if(node != NULL)
				_cellAppInfo.defaultAoIRadius = float(xml->getValFloat(node));
					
			node = xml->enterNode(aoiNode, "hysteresisArea");
			if(node != NULL)
				_cellAppInfo.defaultAoIHysteresisArea = float(xml->getValFloat(node));
		}
			
		node = xml->enterNode(rootNode, "ids");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "criticallyLowSize");
			if(childnode)
			{
				_cellAppInfo.criticallyLowSize = xml->getValInt(childnode);
				if(_cellAppInfo.criticallyLowSize < 100)
					_cellAppInfo.criticallyLowSize = 100;
			}
		}
		
		
		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_cellAppInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "aliasEntityID");
		if(node != NULL){
			_cellAppInfo.aliasEntityID = (xml->getValStr(node) == "true");
		}

		node = xml->enterNode(rootNode, "entitydefAliasID");
		if(node != NULL){
			_cellAppInfo.entitydefAliasID = (xml->getValStr(node) == "true");
		}

		node = xml->enterNode(rootNode, "ghostDistance");
		if(node != NULL){
			_cellAppInfo.ghostDistance = (float)xml->getValFloat(node);
		}

		node = xml->enterNode(rootNode, "ghostingMaxPerCheck");
		if(node != NULL){
			_cellAppInfo.ghostingMaxPerCheck = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "ghostUpdateHertz");
		if(node != NULL){
			_cellAppInfo.ghostUpdateHertz = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "telnet_service");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "port");
			if(childnode)
			{
				_cellAppInfo.telnet_port = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "password");
			if(childnode)
			{
				_cellAppInfo.telnet_passwd = xml->getValStr(childnode);
			}

			childnode = xml->enterNode(node, "default_layer");
			if(childnode)
			{
				_cellAppInfo.telnet_deflayer = xml->getValStr(childnode);
			}
		}

		node = xml->enterNode(rootNode, "shutdown");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "perSecsDestroyEntitySize");
			if(childnode)
			{
				_cellAppInfo.perSecsDestroyEntitySize = uint32(xml->getValInt(childnode));
			}
		}

		node = xml->enterNode(rootNode, "witness");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "timeout");
			if(childnode)
			{
				_cellAppInfo.witness_timeout = uint16(xml->getValInt(childnode));
			}
		}
	}
	
	rootNode = xml->getRootNode("baseapp");
	if(rootNode != NULL)
	{
		node = xml->enterNode(rootNode, "externalAddress");	
		if(node != NULL)
			strncpy((char*)&_baseAppInfo.externalAddress, xml->getValStr(node).c_str(), MAX_NAME);

		node = xml->enterNode(rootNode, "externalPorts_max");
		if(node != NULL)	
			_baseAppInfo.externalPorts = xml->getValInt(node);

		node = xml->enterNode(rootNode, "archivePeriod");
		if(node != NULL)
			_baseAppInfo.archivePeriod = float(xml->getValFloat(node));
				
		node = xml->enterNode(rootNode, "backupPeriod");
		if(node != NULL)
			_baseAppInfo.backupPeriod = float(xml->getValFloat(node));
		
		node = xml->enterNode(rootNode, "backUpUndefinedProperties");
		if(node != NULL)
			_baseAppInfo.backUpUndefinedProperties = xml->getValInt(node) > 0;
			
		node = xml->enterNode(rootNode, "loadSmoothingBias");
		if(node != NULL)
			_baseAppInfo.loadSmoothingBias = float(xml->getValFloat(node));
		
		node = xml->enterNode(rootNode, "ids");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "criticallyLowSize");
			if(childnode)
			{
				_baseAppInfo.criticallyLowSize = xml->getValInt(childnode);
				if(_baseAppInfo.criticallyLowSize < 100)
					_baseAppInfo.criticallyLowSize = 100;
			}
		}
		
		node = xml->enterNode(rootNode, "downloadStreaming");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "bitsPerSecondTotal");
			if(childnode)
				_baseAppInfo.downloadBitsPerSecondTotal = xml->getValInt(childnode);

			childnode = xml->enterNode(node, "bitsPerSecondPerClient");
			if(childnode)
				_baseAppInfo.downloadBitsPerSecondPerClient = xml->getValInt(childnode);
		}
	
		
		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_baseAppInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "entityRestoreSize");
		if(node != NULL){
			_baseAppInfo.entityRestoreSize = xml->getValInt(node);
		}
		
		if(_baseAppInfo.entityRestoreSize <= 0)
			_baseAppInfo.entityRestoreSize = 32;

		node = xml->enterNode(rootNode, "telnet_service");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "port");
			if(childnode)
			{
				_baseAppInfo.telnet_port = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "password");
			if(childnode)
			{
				_baseAppInfo.telnet_passwd = xml->getValStr(childnode);
			}

			childnode = xml->enterNode(node, "default_layer");
			if(childnode)
			{
				_baseAppInfo.telnet_deflayer = xml->getValStr(childnode);
			}
		}

		node = xml->enterNode(rootNode, "shutdown");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "perSecsDestroyEntitySize");
			if(childnode)
			{
				_baseAppInfo.perSecsDestroyEntitySize = uint32(xml->getValInt(childnode));
			}
		}

		node = xml->enterNode(rootNode, "respool");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "buffer_size");
			if(childnode)
				_baseAppInfo.respool_buffersize = xml->getValInt(childnode);

			childnode = xml->enterNode(node, "timeout");
			if(childnode)
				_baseAppInfo.respool_timeout = uint64(xml->getValInt(childnode));

			childnode = xml->enterNode(node, "checktick");
			if(childnode)
				Resmgr::respool_checktick = xml->getValInt(childnode);

			Resmgr::respool_timeout = _baseAppInfo.respool_timeout;
			Resmgr::respool_buffersize = _baseAppInfo.respool_buffersize;
		}
	}

	rootNode = xml->getRootNode("dbmgr");
	if(rootNode != NULL)
	{
		
		node = xml->enterNode(rootNode, "type");	
		if(node != NULL)
			strncpy((char*)&_dbmgrInfo.db_type, xml->getValStr(node).c_str(), MAX_NAME);

		node = xml->enterNode(rootNode, "host");	
		if(node != NULL)
			strncpy((char*)&_dbmgrInfo.db_ip, xml->getValStr(node).c_str(), MAX_IP);

		node = xml->enterNode(rootNode, "port");	
		if(node != NULL)
			_dbmgrInfo.db_port = xml->getValInt(node);	

		node = xml->enterNode(rootNode, "auth");	
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "password");
			if(childnode)
			{
				strncpy((char*)&_dbmgrInfo.db_password, xml->getValStr(childnode).c_str(), MAX_BUF * 10);
			}

			childnode = xml->enterNode(node, "username");
			if(childnode)
			{
				strncpy((char*)&_dbmgrInfo.db_username, xml->getValStr(childnode).c_str(), MAX_NAME);
			}

			childnode = xml->enterNode(node, "encrypt");
			if(childnode)
			{
				_dbmgrInfo.db_passwordEncrypt = xml->getValStr(childnode) == "true";
			}
		}
		
		node = xml->enterNode(rootNode, "databaseName");	
		if(node != NULL)
			strncpy((char*)&_dbmgrInfo.db_name, xml->getValStr(node).c_str(), MAX_NAME);

		node = xml->enterNode(rootNode, "numConnections");	
		if(node != NULL)
			_dbmgrInfo.db_numConnections = xml->getValInt(node);
		
		node = xml->enterNode(rootNode, "unicodeString");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "characterSet");
			if(childnode)
			{
				_dbmgrInfo.db_unicodeString_characterSet = xml->getValStr(childnode);
			}

			childnode = xml->enterNode(node, "collation");
			if(childnode)
			{
				_dbmgrInfo.db_unicodeString_collation = xml->getValStr(childnode);
			}
		}

		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_dbmgrInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}
		
		node = xml->enterNode(rootNode, "debug");
		if(node != NULL){
			_dbmgrInfo.debugDBMgr = (xml->getValStr(node) == "true");
		}

		node = xml->enterNode(rootNode, "allowEmptyDigest");
		if(node != NULL){
			_dbmgrInfo.allowEmptyDigest = (xml->getValStr(node) == "true");
		}

		node = xml->enterNode(rootNode, "account_system");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "accountDefaultFlags");
			if(childnode)
			{
				_dbmgrInfo.accountDefaultFlags = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "accountDefaultDeadline");	
			if(childnode != NULL)
			{
				_dbmgrInfo.accountDefaultDeadline = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "account_registration");	
			if(childnode != NULL)
			{
				TiXmlNode* childchildnode = xml->enterNode(childnode, "enable");
				if(childchildnode)
				{
					_dbmgrInfo.account_registration_enable = (xml->getValStr(childchildnode) == "true");
				}

				childchildnode = xml->enterNode(childnode, "loginAutoCreate");
				if(childchildnode != NULL){
					_dbmgrInfo.notFoundAccountAutoCreate = (xml->getValStr(childchildnode) == "true");
				}
			} 
		}
	}

	if(_dbmgrInfo.db_unicodeString_characterSet.size() == 0)
		_dbmgrInfo.db_unicodeString_characterSet = "utf8";

	if(_dbmgrInfo.db_unicodeString_collation.size() == 0)
		_dbmgrInfo.db_unicodeString_collation = "utf8_bin";

	rootNode = xml->getRootNode("loginapp");
	if(rootNode != NULL)
	{
		node = xml->enterNode(rootNode, "externalAddress");	
		if(node != NULL)
			strncpy((char*)&_loginAppInfo.externalAddress, xml->getValStr(node).c_str(), MAX_NAME);

		node = xml->enterNode(rootNode, "externalPorts_min");
		if(node != NULL)	
			_loginAppInfo.externalPorts = xml->getValInt(node);

		node = xml->enterNode(rootNode, "externalPorts_max");
		if(node != NULL)	
			_loginAppInfo.internalPorts = xml->getValInt(node);

		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_loginAppInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "encrypt_login");
		if(node != NULL){
			_loginAppInfo.encrypt_login = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "account_type");
		if(node != NULL){
			_loginAppInfo.account_type = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "http_cbhost");
		if(node)
			_loginAppInfo.http_cbhost = xml->getValStr(node);

		node = xml->enterNode(rootNode, "http_cbport");
		if(node)
			_loginAppInfo.http_cbport = xml->getValInt(node);
	}
	
	rootNode = xml->getRootNode("cellappmgr");
	if(rootNode != NULL)
	{
		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_cellAppMgrInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}
	}
	
	rootNode = xml->getRootNode("baseappmgr");
	if(rootNode != NULL)
	{
		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_baseAppMgrInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}
	}
	
	rootNode = xml->getRootNode("kbmachine");
	if(rootNode != NULL)
	{
		
		node = xml->enterNode(rootNode, "externalPorts_min");
		if(node != NULL)	
			_kbMachineInfo.externalPorts = xml->getValInt(node);

		node = xml->enterNode(rootNode, "externalPorts_max");
		if(node != NULL)	
			_kbMachineInfo.internalPorts = xml->getValInt(node);

		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_kbMachineInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}
	}
	
	rootNode = xml->getRootNode("bots");
	if(rootNode != NULL)
	{
		node = xml->enterNode(rootNode, "host");	
		if(node != NULL)
			strncpy((char*)&_botsInfo.login_ip, xml->getValStr(node).c_str(), MAX_IP);

		node = xml->enterNode(rootNode, "port");	
		if(node != NULL)
			_botsInfo.login_port = xml->getValInt(node);

		node = xml->enterNode(rootNode, "defaultAddBots");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "totalcount");
			if(childnode)
			{
				_botsInfo.defaultAddBots_totalCount = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "tickcount");
			if(childnode)
			{
				_botsInfo.defaultAddBots_tickCount = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "ticktime");
			if(childnode)
			{
				_botsInfo.defaultAddBots_tickTime = (float)xml->getValFloat(childnode);
			}
		}

		node = xml->enterNode(rootNode, "account_infos");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "account_name_prefix");
			if(childnode)
			{
				_botsInfo.bots_account_name_prefix = xml->getValStr(childnode);
			}

			childnode = xml->enterNode(node, "account_name_suffix_inc");
			if(childnode)
			{
				_botsInfo.bots_account_name_suffix_inc = xml->getValInt(childnode);
			}
		}

		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_botsInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "telnet_service");
		if(node != NULL)
		{
			TiXmlNode* childnode = xml->enterNode(node, "port");
			if(childnode)
			{
				_botsInfo.telnet_port = xml->getValInt(childnode);
			}

			childnode = xml->enterNode(node, "password");
			if(childnode)
			{
				_botsInfo.telnet_passwd = xml->getValStr(childnode);
			}

			childnode = xml->enterNode(node, "default_layer");
			if(childnode)
			{
				_botsInfo.telnet_deflayer = xml->getValStr(childnode);
			}
		}
	}

	rootNode = xml->getRootNode("messagelog");
	if(rootNode != NULL)
	{
		node = xml->enterNode(rootNode, "SOMAXCONN");
		if(node != NULL){
			_messagelogInfo.tcp_SOMAXCONN = xml->getValInt(node);
		}

		node = xml->enterNode(rootNode, "tick_max_sync_logs");
		if(node != NULL){
			tick_max_sync_logs_ = (uint32)xml->getValInt(node);
		}
	}

	SAFE_RELEASE(xml);
	return true;
}

//-------------------------------------------------------------------------------------	
uint32 ServerConfig::tcp_SOMAXCONN(COMPONENT_TYPE componentType)
{
	ENGINE_COMPONENT_INFO& cinfo = getComponent(componentType);
	return cinfo.tcp_SOMAXCONN;
}

//-------------------------------------------------------------------------------------	
void ServerConfig::_updateEmailInfos()
{
	// 如果小于64则表示目前还是明文密码
	
}

//-------------------------------------------------------------------------------------	
void ServerConfig::updateExternalAddress(char* buf)
{
	if(strlen(buf) > 0)
	{
		unsigned int inaddr = 0; 
		if((inaddr = inet_addr(buf)) == INADDR_NONE)  
		{
			struct hostent *host;
			host = gethostbyname(buf);
			if(host)
			{
				strncpy(buf, inet_ntoa(*(struct in_addr*)host->h_addr_list[0]), MAX_BUF);
			}	
		}
	}
}

//-------------------------------------------------------------------------------------	
void ServerConfig::updateInfos(bool isPrint, COMPONENT_TYPE componentType, COMPONENT_ID componentID, 
							   const Address& internalAddr, const Address& externalAddr)
{
	std::string infostr = "";

	//updateExternalAddress(getBaseApp().externalAddress);
	//updateExternalAddress(getLoginApp().externalAddress);

	if(componentType == CELLAPP_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getCellApp();
		info.componentID = componentID;

		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			INFO_MSG(fmt::format("\tgameUpdateHertz : {}\n", gameUpdateHertz()));
			INFO_MSG(fmt::format("\tdefaultAoIRadius : {}\n", info.defaultAoIRadius));
			INFO_MSG(fmt::format("\tdefaultAoIHysteresisArea : {}\n", info.defaultAoIHysteresisArea));
			//INFO_MSG(fmt::format("\texternalAddr : {}\n", externalAddr.c_str()));
			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			infostr += (fmt::format("\tgameUpdateHertz : {}\n", gameUpdateHertz()));
			infostr += (fmt::format("\tdefaultAoIRadius : {}\n", info.defaultAoIRadius));
			infostr += (fmt::format("\tdefaultAoIHysteresisArea : {}\n", info.defaultAoIHysteresisArea));
			//infostr += (fmt::format("\texternalAddr : {}\n", externalAddr.c_str()));
			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}
	}
	else if (componentType == BASEAPP_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getBaseApp();
		info.componentID = componentID;

		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			INFO_MSG(fmt::format("\tgameUpdateHertz : {}\n", gameUpdateHertz()));
			
			if(strlen(info.externalAddress) > 0)
			{
				INFO_MSG(fmt::format("\texternalCustomAddr : {}\n", info.externalAddress));
			}

			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			infostr += (fmt::format("\tgameUpdateHertz : {}\n", gameUpdateHertz()));
			
			if(strlen(info.externalAddress) > 0)
			{
				infostr +=  (fmt::format("\texternalCustomAddr : {}\n", info.externalAddress));
			}

			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}

		_updateEmailInfos();
	}
	else if (componentType == BASEAPPMGR_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getBaseAppMgr();
		info.componentID = componentID;

		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			//INFO_MSG("\texternalAddr : %s\n", externalAddr.c_str()));
			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}
	}
	else if (componentType == CELLAPPMGR_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getCellAppMgr();
		info.componentID = componentID;

		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			//INFO_MSG("\texternalAddr : %s\n", externalAddr.c_str());
			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}
	}
	else if (componentType == DBMGR_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getDBMgr();
		info.componentID = componentID;

		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			//INFO_MSG("\texternalAddr : %s\n", externalAddr.c_str()));
			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}
	}
	else if (componentType == LOGINAPP_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getLoginApp();
		info.componentID = componentID;

		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			if(strlen(info.externalAddress) > 0)
			{
				INFO_MSG(fmt::format("\texternalCustomAddr : {}\n", info.externalAddress));
			}

			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			if(strlen(info.externalAddress) > 0)
			{
				infostr +=  (fmt::format("\texternalCustomAddr : {}\n", info.externalAddress));
			}

			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}

		_updateEmailInfos();
	}
	else if (componentType == MACHINE_TYPE)
	{
		ENGINE_COMPONENT_INFO info = getKBMachine();
		info.componentID = componentID;
		if(isPrint)
		{
			INFO_MSG("server-configs:\n");
			//INFO_MSG("\texternalAddr : %s\n", externalAddr.c_str()));
			INFO_MSG(fmt::format("\tcomponentID : {}\n", info.componentID));

			infostr += "server-configs:\n";
			infostr += (fmt::format("\tcomponentID : {}\n", info.componentID));
		}
	}

#if KBE_PLATFORM == PLATFORM_WIN32
	if(infostr.size() > 0)
	{
		infostr += "\n";
		printf("%s", infostr.c_str());
	}
#endif
}

//-------------------------------------------------------------------------------------		
}
