/*
This source file is part of KBEngine
For the latest info, see http://www.kbengine.org/

Copyright (c) 2008-2012 KBEngine.

KBEngine is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

KBEngine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.
 
You should have received a copy of the GNU Lesser General Public License
along with KBEngine.  If not, see <http://www.gnu.org/licenses/>.
*/


#ifndef KBE_PROXY_HPP
#define KBE_PROXY_HPP
	
// common include
#include "base.hpp"
#include "data_downloads.hpp"
#include "common/common.hpp"
#include "helper/debug_helper.hpp"
#include "network/message_handler.hpp"

//#define NDEBUG
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#endif
	
namespace KBEngine{



class Channel;
class ProxyForwarder;

#define LOG_ON_REJECT  0
#define LOG_ON_ACCEPT  1
#define LOG_ON_WAIT_FOR_DESTROY 2

class Proxy : public Base
{
	/** 子类化 将一些py操作填充进派生类 */
	
public:
	Proxy(ENTITY_ID id);
	~Proxy();
	
	INLINE void addr(const Address& address);
	INLINE const Address& addr()const;

	typedef std::vector<Bundle*> Bundles;
	Bundles* pBundles();

	
	bool sendToClient(Bundle* pBundle);
	bool sendToClient(bool expectData = true);

	/** 
		脚本请求获取连接的rtt值
	*/
	double getRoundTripTime()const;
	
	/** 
		This is the number of seconds since a packet from the client was last received. 
	*/
	double getTimeSinceHeardFromClient()const;
	
	/** 
		脚本请求获取是否有client绑定到proxy上
	*/
	bool hasClient()const;
	
	/** 
		脚本请求获取连接的rtt值
	*/
	INLINE bool entitiesEnabled()const;
	
	/**
		这个entity被激活了, 在客户端初始化好对应的entity后， 这个方法被调用
	*/
	void onEntitiesEnabled(void);
	
	/**
		一个数据下载任务完成
	*/
	void onStreamComplete(int16 id, bool success);

	/**
		登陆尝试， 当正常的登陆失败之后， 调用这个接口再进行尝试 
	*/
	int32 onLogOnAttempt(const char* addr, uint32 port, const char* password);
	
	/**
		初始化客户端proxy的属性
	*/
	void initClientBasePropertys();
	void initClientCellPropertys();

	/** 
		当察觉这个entity对应的客户端socket断开时被调用 
	*/
	void onClientDeath(void);
	
	/** 网络接口
		当客户端所关联的这个entity的cell被创建时，被调用 
	*/
	void onClientGetCell(Channel* pChannel, COMPONENT_ID componentID);

	/**
		获取前端类别
	*/
	INLINE COMPONENT_CLIENT_TYPE getClientType()const;
	INLINE void setClientType(COMPONENT_CLIENT_TYPE ctype);
	
	/**
		每个proxy创建之后都会由系统产生一个uuid， 提供前端重登陆时用作身份识别
	*/
	INLINE uint64 rndUUID()const;
	INLINE void rndUUID(uint64 uid);

	/** 
		将其自身所关联的客户端转给另一个proxy去关联 
	*/
	void giveClientTo(Proxy* proxy);
	void onGiveClientTo(Channel* lpChannel);
	void onGiveClientToFailure();
	

	
	/**
		绑定了witness
	*/
	void onGetWitness();

protected:
	uint64 rndUUID_;
	Address addr_;
	DataDownloads dataDownloads_;

	bool entitiesEnabled_;

	// 限制客户端每秒所能使用的带宽
	int32 bandwidthPerSecond_;

	// 通信加密key 默认blowfish
	std::string encryptionKey;

	ProxyForwarder* pProxyForwarder_;

	COMPONENT_CLIENT_TYPE clientComponentType_;
};

}


#ifdef CODE_INLINE
#include "proxy.ipp"
#endif

#endif // KBE_PROXY_HPP
