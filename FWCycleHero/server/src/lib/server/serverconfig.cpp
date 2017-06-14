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
	ServerConfig::ServerConfig()
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
	

	rootNode = xml->getRootNode("cellapp");
	if(rootNode != NULL)
	{
		
		node = xml->enterNode(node, "internalInterface");
		if (node)
		{
			strncpy((char*)&_cellAppInfo.ip, xml->getValStr(node).c_str(), MAX_NAME);
		}
		
		node = xml->enterNode(rootNode, "appport");
		if(node != NULL)
		{

			_cellAppInfo.port = uint16(xml->getValInt(node));
		}
	}
	
	rootNode = xml->getRootNode("baseapp");
	if(rootNode != NULL)
	{

		node = xml->enterNode(node, "internalInterface");
		if (node)
		{
			strncpy((char*)&_baseAppInfo.ip, xml->getValStr(node).c_str(), MAX_NAME);
		}

		node = xml->enterNode(rootNode, "appport");
		if (node != NULL)
		{

			_baseAppInfo.port = uint16(xml->getValInt(node));
		}
	}

	rootNode = xml->getRootNode("dbmgr");
	if(rootNode != NULL)
	{
		
		node = xml->enterNode(node, "internalInterface");
		if (node)
		{
			strncpy((char*)&_dbmgrInfo.ip, xml->getValStr(node).c_str(), MAX_NAME);
		}

		node = xml->enterNode(rootNode, "appport");
		if (node != NULL)
		{

			_dbmgrInfo.port = uint16(xml->getValInt(node));
		}

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
		}
		
		node = xml->enterNode(rootNode, "databaseName");	
		if(node != NULL)
			strncpy((char*)&_dbmgrInfo.db_name, xml->getValStr(node).c_str(), MAX_NAME);

		node = xml->enterNode(rootNode, "numConnections");	
		if(node != NULL)
			_dbmgrInfo.db_maxconn = xml->getValInt(node);
	}

	rootNode = xml->getRootNode("loginapp");
	if(rootNode != NULL)
	{
		node = xml->enterNode(node, "internalInterface");
		if (node)
		{
			strncpy((char*)&_loginAppInfo.ip, xml->getValStr(node).c_str(), MAX_NAME);
		}

		node = xml->enterNode(rootNode, "appport");
		if (node != NULL)
		{

			_loginAppInfo.port = uint16(xml->getValInt(node));
		}
	}
	
	rootNode = xml->getRootNode("cellappmgr");
	if(rootNode != NULL)
	{
		node = xml->enterNode(node, "internalInterface");
		if (node)
		{
			strncpy((char*)&_cellAppMgrInfo.ip, xml->getValStr(node).c_str(), MAX_NAME);
		}

		node = xml->enterNode(rootNode, "appport");
		if (node != NULL)
		{

			_cellAppMgrInfo.port = uint16(xml->getValInt(node));
		}
	}
	
	rootNode = xml->getRootNode("baseappmgr");
	if(rootNode != NULL)
	{
		node = xml->enterNode(node, "internalInterface");
		if (node)
		{
			strncpy((char*)&_baseAppMgrInfo.ip, xml->getValStr(node).c_str(), MAX_NAME);
		}

		node = xml->enterNode(rootNode, "appport");
		if (node != NULL)
		{

			_baseAppMgrInfo.port = uint16(xml->getValInt(node));
		}
	}
	
	SAFE_RELEASE(xml);
	return true;
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
	
}

//-------------------------------------------------------------------------------------		
}
