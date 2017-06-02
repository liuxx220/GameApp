/*
-------------------------------------------------------------------------------
		file name : message_handler.hpp
		desc      : 网络消息处理和参数的封装
		author    : LJP

		log		  : [ 2015-04-28 ]
-------------------------------------------------------------------------------
*/
#include "message_handler.hpp"
#include "xmlplus/xmlplus.hpp"
	

namespace KBEngine 
{ 

	//-------------------------------------------------------------------------------------
	MessageHandlers::MessageHandlers() : mapMsgCode( nullptr )
	{
		
	}


	void MessageHandlers::CreateMsgHandlePool(int nMax, int nStartID )
	{
		if (nMax <= 0)
			return;

		if (mapMsgCode != 0)
		{
			SAFE_RELEASE_ARRAY( mapMsgCode );
		}

		mapMsgCode		= new CHandlerNode[nMax];
		memset(mapMsgCode, 0, sizeof(CHandlerNode)*nMax);
	}
	//-------------------------------------------------------------------------------------
	MessageHandlers::~MessageHandlers()
	{
		if (mapMsgCode != 0)
		{
			SAFE_RELEASE_ARRAY(mapMsgCode);
		}
	}

	//-------------------------------------------------------------------------------------
	CHandlerNode::CHandlerNode() : send_count(0),
								   recv_count(0)
	{

	}

	//-------------------------------------------------------------------------------------
	CHandlerNode::~CHandlerNode()
	{
			
	}

	
	//-------------------------------------------------------------------------------------
	void MessageHandlers::AddMessageHandler(int32 nMsgID, NETMSG_HANDLE _handler )
	{
		if (nMsgID < 0 || nMsgID >= _max)
			return;

		CHandlerNode* pNode = mapMsgCode + nMsgID;
		pNode->handler	= _handler;
		
		pNode->msgID		= nMsgID;
		pNode->handler		= _handler;
	}


	CHandlerNode* MessageHandlers::Find(int32 nID)
	{
		if (nID < 0 || nID >= _max)
			return nullptr;

		CHandlerNode* pNode = mapMsgCode + nID;
		return pNode;
	}

	void MessageHandlers::HandleMessage(int nID, const MemoryStream* pNet)
	{
		if (nID < 0 || nID >= _max)
			return;

		CHandlerNode* pNode = mapMsgCode + nID;
		if( pNode != nullptr )
			pNode->handler( nID, pNet );
		else
		{
			INFO_MSG(fmt::format("net msg id ={} is not exist \n", nID));
		}
	}
}
