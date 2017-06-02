/*
-------------------------------------------------------------------------------------------
		file name : address.hpp
		desc	  : 网络终端地址的描述
		author    : LJP

		log		  : [2015-04-26]
-------------------------------------------------------------------------------------------
*/

#ifndef __NET_ISOCKET_H__
#define __NET_ISOCKET_H__
#include "common/common.hpp"





namespace KBEngine 
{ 
	// 网络事件枚举的定义 
	enum EIocpOperation
	{
		IOCP_NULL = 0,
		IOCP_ACCEPT,
		IOCP_RECV,
		IOCP_SEND,
		IOCP_CLOSE
	};


	/**
	* @brief 每次IO的数据,和每次IO操作关联在一起的数据
	*/
	struct SPerIoData
	{
		OVERLAPPED			stOverlapped;
		SOCKET				hSock;
		EIocpOperation		nOp;
		WSABUF				stWsaBuf;
		char				Buf[128];
	};



	//-----------------------------------------------------------------------------
	//!	unit定义
	//-----------------------------------------------------------------------------
	#define MAX_PAGE_SIZE 256
	struct tagUnit
	{
		UINT32				dwSize;					// 有效内容大小,并非实际内存空间大小
		tagUnit*			pNext;					// 指向下一个unit,用于safe_unit_queue
		char				Buf[MAX_PAGE_SIZE];
	};


	//-----------------------------------------------------------------------------
	//!	针对于单个客户端的定义
	//-----------------------------------------------------------------------------
	class ClientSocket;
	struct tagXClient
	{
		BOOL volatile			bShutDown;		// 是否已经被shutdown
		LONG volatile			lSendCast;		// 已经发出的send,尚未收到通知的
		LONG volatile			lRecvCast;		// 已经发出的recv,尚未收到通知的

		UINT32					dwClientID;		// 客户端ID,上层确认第一个包以后才有效
		UINT32					dwConnectTime;	// 用来确定踢出长时间未验证客户端
		UINT32					dwSendSize;		// 等待发送大小

		ClientSocket*			pClient;
	};

	/**
	* @brief 每句柄数据,和句柄关联在一起的数据
	*/
	struct SPerHandleData
	{
		bool		bListen;
		void*		ptr;
	};

	
	/**
	* @brief Break packet from TCP data stream. The interface need to be implemented by user
	*/
	class IPacketParser
	{
	public:
		/**
		* @brief Return the length in bytes of the packet in TCP data stream
		* @param pBuf : the data buffer need to parse
		* @param dwLen : the data buffer length
		*/
		virtual INT32			ParsePacket(const char* pBuf, UINT32 dwLen) = 0;
	};

	/**
	* @brief CONNECTION_OPT_SOCKOPT is a data structure, its members correspond the parameters of setsockopt
	*/
	struct SConnectionOptSockopt
	{
		INT32       nLevel;
		INT32       nOptName;
		const char* pOptVal;
		INT32       nOptLen;
	};


	/*
	 * @brief the interface for TCP socket 
	*/
	class ISocket
	{

	public:
		/**
		* @ 如果是 client socket 需要实现该接口，连接到服务器
		*        
		* @param ip, nPort
		*/
		virtual void			connect(UINT32 ip, UINT16 nPort) = 0;

		/**
		* @ 断开与远端的连接
		*      
		* @param 
		*/
		virtual void			closesock() = 0;

		/**
		* @ 关联网络 socket
		*
		* @param
		*/
		virtual void			setsocket(int sock) = 0;

	};
}


#endif // __NET_ISOCKET_H__
