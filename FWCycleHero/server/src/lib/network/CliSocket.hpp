/*
-------------------------------------------------------------------------------------------
		file name : ClientSocket.hpp
		desc	  : 对网络 socket 的封装
		author    : LJP

		log		  : [2015-04-26]
-------------------------------------------------------------------------------------------
*/
#ifndef __NET_CLIENTSOCKET_H__
#define __NET_CLIENTSOCKET_H__
#include "common/common.hpp"
#include "network/ISocket.hpp"
#include "network/x_queue.h"
#include "network/safe_mem_pool.h"





namespace KBEngine 
{ 
	class ClientSocket : public ISocket
	{

	public:

		ClientSocket();
		virtual ~ClientSocket();

		void					Destory();
	public:
		// 基类的虚函数
		virtual void			setsocket(int fd)			{ m_socket = fd; }
		virtual void			connect(UINT32 ip, UINT16 nPort);
		virtual void			closesock();
		void					sendmsg( void);
		void					PostRecv();
		void					recvmsg(UINT32 dwLen);

		bool					IsShutDown()				{ return m_IsShutDown; }
		KBESOCKET				GetSocket()					{ return m_socket; }
		void					SetConnected(bool bConnect) { m_bConnected = bConnect; }
		bool					IsConnected()				{ return m_bConnected; }

		int						GetBufferSize(int optname) const;
		bool					SetBufferSize(int optname, int size);
		tagUnit*				GetRecvMessages(UINT32& dwSize, INT32& nRecvNum);

		void					Send(const char* pMsg, UINT32 dwLen);
		INT32					SyncSend(const char* pBuf, UINT32 dwLen);
		bool					AssociateWithIOCP();

	protected:

		KBESOCKET				m_socket;
		bool					m_IsShutDown;
		bool					m_bConnected;

		char*					m_pSendBuf;
		char*					m_pRecvBuf;
		INT32					m_nDataReceived;
		SPerIoData				m_stRecvIoData;
		SPerIoData				m_stSendIoData;
		SPerHandleData			m_stPerHandleData;
		
	public:
		// tagUnit
		SafeMemPool*			m_PoolSend;			// 发送内存池
		SafeMemPool*			m_PoolRecv;			// 接受内存池

		XQueue<tagUnit*> 		m_RecvQueue;		// 收包队列,待上层收取
		XQueue<tagUnit*> 		m_SendQueue;
	};
}

#endif // __NET_CLIENTSOCKET_H__
