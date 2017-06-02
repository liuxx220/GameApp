/*
------------------------------------------------------------------------------------------------------------------
file Name	:
desc		: db 服务器管理模块
author		:
log			:
------------------------------------------------------------------------------------------------------------------
*/

#ifndef KBE_DBMGR_HPP
#define KBE_DBMGR_HPP
	
// common include	
#include "buffered_dbtasks.hpp"
#include "server/glw_main.hpp"
#include "server/serverapp.hpp"
#include "server/serverconfig.hpp"
#include "common/timer.hpp"
#include "network/endpoint.hpp"
#include "server/glw_resmgr.hpp"
#include "thread/threadpool.hpp"

//#define NDEBUG
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#endif
	
namespace KBEngine
{

	class DBInterface;
	class BillingHandler;
	class SyncAppDatasHandler;

	class AppDBServer : public ServerApp, public Singleton<AppDBServer>
	{

	public:
		enum TimeOutType
		{
			TIMEOUT_TICK = TIMEOUT_SERVERAPP_MAX + 1,
			TIMEOUT_CHECK_STATUS
		};
	
		AppDBServer(EventDispatcher& dispatcher,
					 NetSession& ninterface, 
					 COMPONENT_TYPE componentType,
					 COMPONENT_ID componentID);

		~AppDBServer();
	
		
		void					handleTimeout(TimerHandle handle, void * arg);
		void					handleMainTick();
		void					handleCheckStatusTick();

		/* 初始化相关接口 */
		virtual bool			inInitialize();
		virtual bool			initializeEnd();
		virtual void			finalise();
		virtual bool			run();
		virtual bool			canShutdown();


		bool					initDB();

		/** 网络接口
			请求分配一个ENTITY_ID段
		*/
		static int			onReqAllocEntityID(int msgid, const MemoryStream* pNetCmd);

		/* 网络接口
			注册一个新激活的baseapp或者cellapp或者dbmgr
			通常是一个新的app被启动了， 它需要向某些组件注册自己。
		*/
		virtual void		onRegisterNewApp(
												int32 uid, 
												std::string& username, 
												COMPONENT_TYPE componentType, COMPONENT_ID componentID, COMPONENT_ORDER globalorderID, COMPONENT_ORDER grouporderID,
												uint32 intaddr, uint16 intport, uint32 extaddr, uint16 extport, std::string& extaddrEx);


		/** 网络接口
			dbmgr广播global数据的改变
		*/
		void				onGlobalDataClientLogon(Channel* pChannel, COMPONENT_TYPE componentType);
		void				onBroadcastGlobalDataChanged(Channel* pChannel, KBEngine::MemoryStream& s);
	
	
		/** 网络接口
			请求擦除客户端请求任务
		*/
		void				eraseClientReq(Channel* pChannel, std::string& logkey);

		/** 网络接口
			一个新用户登录， 需要检查合法性
		*/
		void				onAccountLogin(Channel* pChannel, KBEngine::MemoryStream& s);
		void				onLoginAccountCBBFromBilling(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			baseapp请求查询account信息
		*/
		void				queryAccount(Channel* pChannel, std::string& accountName, std::string& password, 
											COMPONENT_ID componentID, ENTITY_ID entityID, DBID entityDBID, uint32 ip, uint16 port);

		/** 网络接口
			账号从baseapp上线了
		*/
		void				onAccountOnline(Channel* pChannel, std::string& accountName, 
											COMPONENT_ID componentID, ENTITY_ID entityID);


		/** 网络接口
			执行数据库查询
		*/
		void				executeRawDatabaseCommand(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			某个entity存档
		*/
		void				writeEntity(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			删除某个entity的存档数据
		*/
		void				removeEntity(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			通过dbid从数据库中删除一个实体的回调
		*/
		void				deleteBaseByDBID(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			通过dbid查询一个实体是否从数据库检出
		*/
		void				lookUpBaseByDBID(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			请求从db获取entity的所有数据
		*/
		void				queryEntity(Channel* pChannel, COMPONENT_ID componentID, int8	queryMode, DBID dbid, 
										std::string& entityType, CALLBACK_ID callbackID, ENTITY_ID entityID);

		/** 网络接口
			同步entity流模板
		*/
		void				syncEntityStreamTemplate(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			请求充值
		*/
		void				charge(Channel* pChannel, KBEngine::MemoryStream& s);

		/** 网络接口
			充值回调
		*/
		void				onChargeCB(Channel* pChannel, KBEngine::MemoryStream& s);


		/** 网络接口
			激活回调
		*/
		void				accountActivate(Channel* pChannel, std::string& scode);

		/** 网络接口
			账号重置密码
		*/
		void				accountReqResetPassword(Channel* pChannel, std::string& accountName);
		void				accountResetPassword(Channel* pChannel, std::string& accountName, std::string& newpassword, std::string& code);

		/** 网络接口
			账号绑定邮箱
		*/
		void				accountReqBindMail(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& email);
		void				accountBindMail(Channel* pChannel, std::string& username, std::string& scode);

		/** 网络接口
			账号修改密码
		*/
		void				accountNewPassword(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& newpassword);
	
		SyncAppDatasHandler* GetSyncAppDatasHandler()const					{ return pSyncAppDatasHandler_; }
		void				 SetSyncAppDatasHandler(SyncAppDatasHandler* p)	{ pSyncAppDatasHandler_ = p; }
	protected:
		TimerHandle							loopCheckTimerHandle_;
		TimerHandle							mainProcessTimer_;

														
		// 任务缓冲区域
		Buffered_DBTasks					bufferedDBTasks_;			

		// Statistics
		uint32								numWrittenEntity_;
		uint32								numRemovedEntity_;
		uint32								numQueryEntity_;
		uint32								numExecuteRawDatabaseCommand_;
		uint32								numCreatedAccount_;

		BillingHandler*						pBillingAccountHandler_;
		BillingHandler*						pBillingChargeHandler_;

		SyncAppDatasHandler*				pSyncAppDatasHandler_;
	};

}

#endif // KBE_DBMGR_HPP
