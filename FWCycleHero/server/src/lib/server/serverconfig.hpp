/*
--------------------------------------------------------------------------------
		file name : serverconfig.hpp
		desc	  : 服务器配置信息
		author	  : LJP

		log		  : [2015-04-25]
--------------------------------------------------------------------------------
*/
#ifndef KBE_SERVER_CONFIG_HPP
#define KBE_SERVER_CONFIG_HPP

#define __LIB_DLLAPI__	
// common include
#include "common/common.hpp"
#if KBE_PLATFORM == PLATFORM_WIN32
#pragma warning (disable : 4996)
#endif
//#define NDEBUG
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <iostream>	
#include <stdarg.h> 
#include "common/singleton.hpp"
#include "thread/threadmutex.hpp"
#include "thread/threadguard.hpp"
#include "xmlplus/xmlplus.hpp"	
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#include <errno.h>
#endif
	
namespace KBEngine
{

	class Address;
	// 引擎组件信息结构体
	typedef struct EngineComponentInfo
	{
		EngineComponentInfo()
		{
			tcp_SOMAXCONN = 5;
			notFoundAccountAutoCreate = false;
			account_registration_enable = false;
			account_type = 3;
			debugDBMgr = false;

			externalAddress[0] = '\0';
		}

		~EngineComponentInfo()
		{
		}

		uint32 port;											// 组件的运行后监听的端口
		char ip[MAX_BUF];										// 组件的运行期ip地址

		float defaultAoIRadius;									// 配置在cellapp节点中的player的aoi半径大小
		float defaultAoIHysteresisArea;							// 配置在cellapp节点中的player的aoi的滞后范围
		uint16 witness_timeout;									// 观察者默认超时时间(秒)
		COMPONENT_ID componentID;

		float ghostDistance;									// ghost区域距离
		uint16 ghostingMaxPerCheck;								// 每秒检查ghost次数
		uint16 ghostUpdateHertz;								// ghost更新hz
	
		bool aliasEntityID;										// 优化EntityID，aoi范围内小于255个EntityID, 传输到client时使用1字节伪ID 
		bool entitydefAliasID;									// 优化entity属性和方法广播时占用的带宽，entity客户端属性或者客户端不超过255个时， 方法uid和属性uid传输到client时使用1字节别名ID


		char externalAddress[MAX_NAME];							// 外部IP地址
		int32 externalPorts;									// 对外socket端口
		int32 internalPorts;									// 对内socket端口

		char db_type[MAX_BUF];									// 数据库的类别
		uint32 db_port;											// 数据库的端口
		char db_ip[MAX_BUF];									// 数据库的ip地址
		char db_username[MAX_NAME];								// 数据库的用户名
		char db_password[MAX_BUF * 10];							// 数据库的密码
		char db_name[MAX_NAME];									// 数据库名
		uint16 db_numConnections;								// 数据库最大连接
		std::string db_unicodeString_characterSet;				// 设置数据库字符集
		std::string db_unicodeString_collation;
		bool notFoundAccountAutoCreate;							// 登录合法时游戏数据库找不到游戏账号则自动创建
		bool db_passwordEncrypt;								// db密码是否是加密的
		bool allowEmptyDigest;									// 是否检查defs-MD5
		bool account_registration_enable;						// 是否开放注册

		float archivePeriod;									// entity存储数据库周期
		float backupPeriod;										// entity备份周期
		bool backUpUndefinedProperties;							// entity是否备份未定义属性
		uint16 entityRestoreSize;								// entity restore每tick数量 

		float loadSmoothingBias;								// baseapp负载滤平衡调整值， 
		uint32 login_port;										// 服务器登录端口 目前bots在用
		char login_ip[MAX_BUF];									// 服务器登录ip地址

		ENTITY_ID criticallyLowSize;							// id剩余这么多个时向dbmgr申请新的id资源

		uint32 downloadBitsPerSecondTotal;						// 所有客户端每秒下载带宽总上限
		uint32 downloadBitsPerSecondPerClient;					// 每个客户端每秒的下载带宽

		uint32 defaultAddBots_totalCount;						// 默认启动进程后自动添加这么多个bots 添加总数量
		float defaultAddBots_tickTime;							// 默认启动进程后自动添加这么多个bots 每次添加所用时间(s)
		uint32 defaultAddBots_tickCount;						// 默认启动进程后自动添加这么多个bots 每次添加数量

		std::string bots_account_name_prefix;					// 机器人账号名称的前缀
		uint32 bots_account_name_suffix_inc;					// 机器人账号名称的后缀递增, 0使用随机数递增， 否则按照baseNum填写的数递增

		uint32 tcp_SOMAXCONN;									// listen监听队列最大值

		int8 encrypt_login;										// 加密登录信息

		uint32 telnet_port;
		std::string telnet_passwd;
		std::string telnet_deflayer;

		uint32 perSecsDestroyEntitySize;						// 每秒销毁base|entity数量

		uint64 respool_timeout;
		uint32 respool_buffersize;

		uint8 account_type;										// 1: 普通账号, 2: email账号(需要激活), 3: 智能账号(自动识别email， 普通号码等) 
		uint32 accountDefaultFlags;								// 新账号默认标记(ACCOUNT_FLAGS可叠加， 填写时按十进制格式) 
		uint64 accountDefaultDeadline;							// 新账号默认过期时间(秒, 引擎会加上当前时间)
	
		std::string http_cbhost;
		uint16 http_cbport;										// 用户http回调接口，处理认证、密码重置等

		bool debugDBMgr;										// debug模式下可输出读写操作信息
	}ENGINE_COMPONENT_INFO;

	class ServerConfig : public Singleton<ServerConfig>
	{
	public:
		ServerConfig();
		~ServerConfig();
	
		bool loadConfig(std::string fileName);
	
		INLINE ENGINE_COMPONENT_INFO& getCellApp(void);
		INLINE ENGINE_COMPONENT_INFO& getBaseApp(void);
		INLINE ENGINE_COMPONENT_INFO& getDBMgr(void);
		INLINE ENGINE_COMPONENT_INFO& getLoginApp(void);
		INLINE ENGINE_COMPONENT_INFO& getCellAppMgr(void);
		INLINE ENGINE_COMPONENT_INFO& getBaseAppMgr(void);
		INLINE ENGINE_COMPONENT_INFO& getKBMachine(void);
		INLINE ENGINE_COMPONENT_INFO& getBots(void);
		INLINE ENGINE_COMPONENT_INFO& getMessagelog(void);
		INLINE ENGINE_COMPONENT_INFO& getBilling(void);

		INLINE ENGINE_COMPONENT_INFO& getComponent(COMPONENT_TYPE componentType);
 	
		INLINE ENGINE_COMPONENT_INFO& getConfig();

 		void updateInfos(bool isPrint, COMPONENT_TYPE componentType, COMPONENT_ID componentID, 
 					const Address& internalAddr, const Address& externalAddr);
 	
		void updateExternalAddress(char* buf);

		INLINE int16 gameUpdateHertz(void)const;
	
		INLINE const char* billingSystemAccountType()const;
		INLINE const char* billingSystemChargeType()const;

		INLINE const char* billingSystemThirdpartyAccountServiceAddr()const;
		INLINE uint16 billingSystemThirdpartyAccountServicePort()const;

		INLINE const char* billingSystemThirdpartyChargeServiceAddr()const;
		INLINE uint16 billingSystemThirdpartyChargeServicePort()const;

		INLINE uint16 billingSystemThirdpartyServiceCBPort()const;

		uint32 tcp_SOMAXCONN(COMPONENT_TYPE componentType);

		float shutdowntime(){ return shutdown_time_; }
		float shutdownWaitTickTime(){ return shutdown_waitTickTime_; }

		uint32 tickMaxSyncLogs()const { return tick_max_sync_logs_; }
	private:
		void _updateEmailInfos();

	private:
		ENGINE_COMPONENT_INFO _cellAppInfo;
		ENGINE_COMPONENT_INFO _baseAppInfo;
		ENGINE_COMPONENT_INFO _dbmgrInfo;
		ENGINE_COMPONENT_INFO _loginAppInfo;
		ENGINE_COMPONENT_INFO _cellAppMgrInfo;
		ENGINE_COMPONENT_INFO _baseAppMgrInfo;
		ENGINE_COMPONENT_INFO _kbMachineInfo;
		ENGINE_COMPONENT_INFO _botsInfo;
		ENGINE_COMPONENT_INFO _messagelogInfo;
		ENGINE_COMPONENT_INFO _billingInfo;
	public:
		int16 gameUpdateHertz_;
		uint32 tick_max_sync_logs_;

		// 每个客户端每秒占用的最大带宽
		uint32 bitsPerSecondToClient_;		
		float shutdown_time_;
		float shutdown_waitTickTime_;

		float callback_timeout_;										// callback默认超时时间(秒)
		float thread_timeout_;											// 默认超时时间(秒)

		uint32 thread_init_create_, thread_pre_create_, thread_max_create_;

	};

	#define g_kbeSrvConfig ServerConfig::getSingleton()
}


#ifdef CODE_INLINE
#include "serverconfig.ipp"
#endif
#endif // KBE_SERVER_CONFIG_HPP
