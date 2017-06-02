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


#ifndef KBE_BASEAPP_HPP
#define KBE_BASEAPP_HPP
	
// common include	
#include "server/serverapp.hpp"
#include "proxy.hpp"
#include "profile.hpp"
#include "network/endpoint.hpp"

//#define NDEBUG
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#endif
	
namespace KBEngine{


class Channel;
class Proxy;
class Backuper;
class Archiver;
class TelnetServer;


class Baseapp : public ServerApp,
				public Singleton<Baseapp>
{
public:
	enum TimeOutType
	{
		TIMEOUT_CHECK_STATUS =1,
		TIMEOUT_MAX
	};
	
	Baseapp(EventDispatcher& dispatcher, NetSession& ninterface, COMPONENT_TYPE componentType,COMPONENT_ID componentID);
	~Baseapp();
	
	
	bool					run();
	
	/** 
		相关处理接口 
	*/
	virtual void			handleTimeout(TimerHandle handle, void * arg);
	virtual void			handleGameTick();
	void					handleCheckStatusTick();
	void					handleBackup();
	void					handleArchive();

	/** 
		初始化相关接口 
	*/
	bool					initialize();
	bool					initializeBegin();
	bool					initializeEnd();
	void					finalise();
	
	virtual bool			canShutdown();
	virtual void			onShutdownBegin();
	virtual void			onShutdown(bool first);
	virtual void			onShutdownEnd();

	
	float					getLoad()const { return load_; }
	void					updateLoad();
	static uint64			checkTickPeriod();
	static int				quantumPassedPercent(uint64 curr = timestamp());
	virtual void			onChannelDeregister(Channel * pChannel);

	/**
		一个cellapp死亡
	*/
	void onCellAppDeath(Channel * pChannel);

	/** 网络接口
		dbmgr告知已经启动的其他baseapp或者cellapp的地址
		当前app需要主动的去与他们建立连接
	*/
	virtual void onGetEntityAppFromDbmgr(Channel* pChannel, 
							int32 uid, 
							std::string& username, 
							COMPONENT_TYPE componentType, COMPONENT_ID componentID, COMPONENT_ORDER globalorderID, COMPONENT_ORDER grouporderID,
							uint32 intaddr, uint16 intport, uint32 extaddr, uint16 extport, std::string& extaddrEx);
	
	/** 网络接口
		某个client向本app告知处于活动状态。
	*/
	void onClientActiveTick(Channel* pChannel);

	

	/**
		恢复一个space 
	*/
	void restoreSpaceInCell(Base* base);

	
	/** 收到baseappmgr决定将某个baseapp要求createBaseAnywhere的请求在本baseapp上执行 
		@param entityType	: entity的类别， entities.xml中的定义的。
		@param strInitData	: 这个entity被创建后应该给他初始化的一些数据， 需要使用pickle.loads解包.
		@param componentID	: 请求创建entity的baseapp的组件ID
	*/
	void onCreateBaseAnywhere(Channel* pChannel, MemoryStream& s);


	/** 网络接口
		createBaseFromDBID的回调。
	*/
	void onCreateBaseFromDBIDCallback(Channel* pChannel, KBEngine::MemoryStream& s);

	
	/** 网络接口
		createBaseFromDBID的回调。
	*/
	// 从数据库来的回调
	void onCreateBaseAnywhereFromDBIDCallback(Channel* pChannel, KBEngine::MemoryStream& s);
	// 请求在这个进程上创建这个entity
	void createBaseAnywhereFromDBIDOtherBaseapp(Channel* pChannel, KBEngine::MemoryStream& s);
	// 创建完毕后的回调
	void onCreateBaseAnywhereFromDBIDOtherBaseappCallback(Channel* pChannel, COMPONENT_ID createByBaseappID, 
							std::string entityType, ENTITY_ID createdEntityID, CALLBACK_ID callbackID, DBID dbid);
	

	/** 
		baseapp 的createBaseAnywhere的回调 
	*/
	void onCreateBaseAnywhereCallback(Channel* pChannel, KBEngine::MemoryStream& s);
	void _onCreateBaseAnywhereCallback(Channel* pChannel, CALLBACK_ID callbackID, 
		std::string& entityType, ENTITY_ID eid, COMPONENT_ID componentID);

	/** 网络接口
		createCellEntity失败的回调。
	*/
	void onCreateCellFailure(Channel* pChannel, ENTITY_ID entityID);

	/** 网络接口
		createCellEntity的cell实体创建成功回调。
	*/
	void onEntityGetCell(Channel* pChannel, ENTITY_ID id, COMPONENT_ID componentID, SPACE_ID spaceID);

	/** 
		通知客户端创建一个proxy对应的实体 
	*/
	bool createClientProxies(Proxy* base, bool reload = false);

	void onExecuteRawDatabaseCommandCB(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		dbmgr发送初始信息
		startID: 初始分配ENTITY_ID 段起始位置
		endID: 初始分配ENTITY_ID 段结束位置
		startGlobalOrder: 全局启动顺序 包括各种不同组件
		startGroupOrder: 组内启动顺序， 比如在所有baseapp中第几个启动。
	*/
	void onDbmgrInitCompleted(Channel* pChannel, 
		GAME_TIME gametime, ENTITY_ID startID, ENTITY_ID endID, COMPONENT_ORDER startGlobalOrder, COMPONENT_ORDER startGroupOrder, const std::string& digest);

	/** 网络接口
		dbmgr广播global数据的改变
	*/
	void onBroadcastBaseAppDataChanged(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		注册将要登录的账号, 注册后则允许登录到此网关
	*/
	void registerPendingLogin(Channel* pChannel, std::string& loginName, std::string& accountName, 
		std::string& password, ENTITY_ID entityID, DBID entityDBID, uint32 flags, uint64 deadline, COMPONENT_TYPE componentType);

	/** 网络接口
		新用户请求登录到网关上
	*/
	void loginGateway(Channel* pChannel, std::string& accountName, std::string& password);

	
	/** 网络接口
		重新登录 快速与网关建立交互关系(前提是之前已经登录了， 
		之后断开在服务器判定该前端的Entity未超时销毁的前提下可以快速与服务器建立连接并达到操控该entity的目的)
	*/
	void reLoginGateway(Channel* pChannel, std::string& accountName, 
		std::string& password, uint64 key, ENTITY_ID entityID);

	/** 网络接口
		从dbmgr获取到账号Entity信息
	*/
	void onQueryAccountCBFromDbmgr(Channel* pChannel, KBEngine::MemoryStream& s);
	
	/**
		客户端自身进入世界了
	*/
	void onClientEntityEnterWorld(Proxy* base, COMPONENT_ID componentID);

	/** 网络接口
		entity收到一封mail, 由某个app上的mailbox发起(只限与服务器内部使用， 客户端的mailbox调用方法走
		onRemoteCellMethodCallFromClient)
	*/
	void onEntityMail(Channel* pChannel, KBEngine::MemoryStream& s);
	
	/** 网络接口
		client访问entity的cell方法
	*/
	void onRemoteCallCellMethodFromClient(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		client更新数据
	*/
	void onUpdateDataFromClient(Channel* pChannel, KBEngine::MemoryStream& s);


	/** 网络接口
		cellapp备份entity的cell数据
	*/
	void onBackupEntityCellData(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		cellapp writeToDB完成
	*/
	void onCellWriteToDBCompleted(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		cellapp转发entity消息给client
	*/
	void forwardMessageToClientFromCellapp(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		cellapp转发entity消息给某个baseEntity的cellEntity
	*/
	void forwardMessageToCellappFromCellapp(Channel* pChannel, KBEngine::MemoryStream& s);
	

	/** 网络接口
		写entity到db回调
	*/
	void onWriteToDBCallback(Channel* pChannel, ENTITY_ID eid, DBID entityDBID, CALLBACK_ID callbackID, bool success);

	/**
		增加proxices计数
	*/
	void incProxicesCount(){ ++numProxices_; }

	/**
		减少proxices计数
	*/
	void decProxicesCount(){ --numProxices_; }

	/**
		获得proxices计数
	*/
	int32 numProxices()const{ return numProxices_; }

	
	void onChargeCB(Channel* pChannel, KBEngine::MemoryStream& s);

	
	virtual void onHello(Channel* pChannel, 
		const std::string& verInfo, 
		const std::string& scriptVerInfo, 
		const std::string& encryptedKey);

	
	/** 网络接口
		请求在其他APP灾难恢复返回结果
	*/
	void onRequestRestoreCB(Channel* pChannel, KBEngine::MemoryStream& s);

	
	/** 网络接口
		某个baseapp上的space恢复了cell， 判断当前baseapp是否有相关entity需要恢复cell
	*/
	void onRestoreSpaceCellFromOtherBaseapp(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		某个app请求查看该app
	*/
	virtual void lookApp(Channel* pChannel);

	/** 网络接口
		客户端协议导出
	*/
	void importClientMessages(Channel* pChannel);

	/** 网络接口
		客户端entitydef导出
	*/
	void importClientEntityDef(Channel* pChannel);

	
	/** 网络接口
		通过dbid从数据库中删除一个实体的回调
	*/
	void deleteBaseByDBIDCB(Channel* pChannel, KBEngine::MemoryStream& s);

	
	/** 网络接口
		如果实体在线回调返回basemailbox，如果实体不在线则回调返回true，其他任何原因都返回false.
	*/
	void lookUpBaseByDBIDCB(Channel* pChannel, KBEngine::MemoryStream& s);

	/** 网络接口
		请求绑定email
	*/
	void reqAccountBindEmail(Channel* pChannel, ENTITY_ID entityID, std::string& password, std::string& email);


	/** 网络接口
		请求绑定email
	*/
	void reqAccountNewPassword(Channel* pChannel, ENTITY_ID entityID, std::string& oldpassworld, std::string& newpassword);

	
protected:
	TimerHandle												loopCheckTimerHandle_;

	
	// 备份存档相关
	SHARED_PTR< Backuper >								pBackuper_;	
	SHARED_PTR< Archiver >								pArchiver_;	

	float													load_;

	static uint64											_g_lastTimestamp;

	int32													numProxices_;

	TelnetServer*											pTelnetServer_;

	
	TimerHandle												pResmgrTimerHandle_;
};

}

#endif // KBE_BASEAPP_HPP
