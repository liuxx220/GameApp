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
	bool  ServerConfig::loadDBConfig(std::string fileName)
	{
		TiXmlNode* node = NULL, *rootNode = NULL;
		XmlPlus* xml = new XmlPlus(Resmgr::getSingleton().matchRes(fileName).c_str());

		if (!xml->isGood())
		{
			ERROR_MSG(fmt::format("ServerConfig::loadConfig: load {} is failed!\n", fileName.c_str()));
			SAFE_RELEASE(xml);
			return false;
		}

		rootNode = xml->getRootNode("dbmgr");
		if (rootNode != NULL)
		{
			
			node = xml->enterNode(rootNode, "host");
			if(node != NULL)
				strncpy((char*)&_dbconfig.db_ip, xml->getValStr(node).c_str(), MAX_IP);

			node = xml->enterNode(rootNode, "port");
			if(node != NULL)
				_dbconfig.db_port = xml->getValInt(node);

			node = xml->enterNode(rootNode, "auth");
			if(node != NULL)
			{
			TiXmlNode* childnode = xml->enterNode(node, "password");
			if(childnode)
			{
				strncpy((char*)&_dbconfig.db_password, xml->getValStr(childnode).c_str(), MAX_BUF * 10);
			}

			childnode = xml->enterNode(node, "username");
			if(childnode)
			{
				strncpy((char*)&_dbconfig.db_username, xml->getValStr(childnode).c_str(), MAX_NAME);
			}

			}

			node = xml->enterNode(rootNode, "databaseName");
			if(node != NULL)
				strncpy((char*)&_dbconfig.db_name, xml->getValStr(node).c_str(), MAX_NAME);

			node = xml->enterNode(rootNode, "numConnections");
			if(node != NULL)
				_dbconfig.db_maxconn = xml->getValInt(node);
		}
	}
}
