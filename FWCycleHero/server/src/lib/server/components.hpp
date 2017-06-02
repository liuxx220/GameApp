/*
------------------------------------------------------------------------
		file name : components.hpp
		desc	  : 服务器 exe 的管理和 exe 信息的描述
		author    : LJP

		log		  : [2015-04-25]
------------------------------------------------------------------------
*/

#ifndef KBE_ENGINE_COMPONENT_MGR_HPP
#define KBE_ENGINE_COMPONENT_MGR_HPP
	

#include "common/timer.hpp"
#include "common/tasks.hpp"
#include "common/common.hpp"
#include "common/singleton.hpp"
#include "thread/threadmutex.hpp"
#include "thread/threadguard.hpp"
#include "server/common.hpp"

// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#include <errno.h>
#endif
	
namespace KBEngine
{



	// ComponentInfos.flags标志
	#define COMPONENT_FLAG_NORMAL			0x00000000
	#define COMPONENT_FLAG_SHUTTINGDOWN		0x00000001
	class Address;
	class Components : public Task, public Singleton<Components>
	{
	public:
		static int32 ANY_UID; 


		/// 一个EXE程序的信息描述
		struct ComponentInfos
		{
			ComponentInfos()
			{
				uid				= 0;
				flags			= COMPONENT_FLAG_NORMAL;
				cid				= 0;
				groupOrderid	= 0;
				globalOrderid	= 0;
				shutdownState	= 0;
				mem = cpu		= 0.f;
				usedmem			= 0;
				extradata		= extradata1 = extradata2 = 0;
				pid				= 0;
				externalAddressEx[0] = '\0';
				logTime			= timestamp();
			}

			SHARED_PTR<Address > pIntAddr, pExtAddr;	// 内部和外部地址
			char externalAddressEx[MAX_NAME + 1];					// 强制暴露给外部的公网地址, 详见配置中的externalAddressEx

			int32			uid;
			COMPONENT_ID	cid;
			COMPONENT_ORDER groupOrderid, globalOrderid;
			char			username[MAX_NAME + 1];
			COMPONENT_TYPE  componentType;
			uint32			flags;
			int8			shutdownState;
			float			cpu;
			float			mem;
			uint32			usedmem;
			uint64			extradata, extradata1, extradata2, extradata3;
			uint32			pid;
			uint64			logTime;
		};

		/// 组件列表
		typedef std::vector<ComponentInfos> COMPONENTS;

		/** 组件添加删除handler */
		class ComponentsNotificationHandler
		{
		public:
			virtual ~ComponentsNotificationHandler() {};
			virtual void		onAddComponent(const Components::ComponentInfos*) = 0;
			virtual void		onRemoveComponent(const Components::ComponentInfos*) = 0;
			virtual void		onIdentityillegal(COMPONENT_TYPE componentType, COMPONENT_ID componentID, uint32 pid, const char* pAddr) = 0;
		};

	public:
		Components();
		~Components();

		void			initialize(COMPONENT_TYPE componentType, COMPONENT_ID componentID);
		void			finalise();


		void			addComponent(  int32 uid, const char* username, 
										COMPONENT_TYPE componentType, COMPONENT_ID componentID, COMPONENT_ORDER globalorderid, COMPONENT_ORDER grouporderid,
										uint32 intaddr, uint16 intport, 
										uint32 extaddr, uint16 extport, std::string& extaddrEx, uint32 pid,
										float cpu, float mem, uint32 usedmem, uint64 extradata, uint64 extradata1, uint64 extradata2, uint64 extradata3);

		void			delComponent(  int32 uid, COMPONENT_TYPE componentType, 
										COMPONENT_ID componentID, bool ignoreComponentID = false, bool shouldShowLog = true);

		
		void			clear(int32 uid = -1, bool shouldShowLog = true);

		Components::COMPONENTS&		getComponents(COMPONENT_TYPE componentType);

		Components::ComponentInfos* findComponent(COMPONENT_TYPE componentType, int32 uid, COMPONENT_ID componentID);
		Components::ComponentInfos* findComponent(COMPONENT_TYPE componentType, COMPONENT_ID componentID);
		Components::ComponentInfos* findComponent(COMPONENT_ID componentID);
		
		/** 
			通过进程id寻找本地组件
		*/
		Components::ComponentInfos* findLocalComponent(uint32 pid);

		int				connectComponent(COMPONENT_TYPE componentType, int32 uid, COMPONENT_ID componentID);

		typedef std::map<int32/*uid*/, COMPONENT_ORDER/*lastorder*/> ORDER_LOG;
		ORDER_LOG&		getGlobalOrderLog(){ return _globalOrderLog; }
		ORDER_LOG&		getBaseappGroupOrderLog(){ return _baseappGrouplOrderLog; }
		ORDER_LOG&		getCellappGroupOrderLog(){ return _cellappGrouplOrderLog; }
		ORDER_LOG&		getLoginappGroupOrderLog(){ return _loginappGrouplOrderLog; }
	
		/** 
			检查所有的组件， 防止有重复的uuid， 此时应该报错.
		*/
		bool			checkComponents(int32 uid, COMPONENT_ID componentID, uint32 pid);

		void			pHandler(ComponentsNotificationHandler* ph){ _pHandler = ph; };

		/** 
			检查某个组件端口是否有效.
		*/
		bool			checkComponentPortUsable(const Components::ComponentInfos* info);

		/** 
			是否是本地组件.
		*/
		bool			isLocalComponent(const Components::ComponentInfos* info);

		/** 
			是否本地组件是否在运行中.
		*/
		const Components::ComponentInfos* lookupLocalComponentRunning(uint32 pid);

		Components::ComponentInfos* getBaseappmgr();
		Components::ComponentInfos* getCellappmgr();
		Components::ComponentInfos* getDbmgr();
		Components::ComponentInfos* getMessagelog();
		Components::ComponentInfos* getBillings();

		/** 
			获取游戏服务端必要组件的注册数量。
		*/
		size_t getGameSrvComponentsSize();

		void componentID(COMPONENT_ID id){ componentID_ = id; }
		COMPONENT_ID componentID()const { return componentID_; }
		void componentType(COMPONENT_TYPE t){ componentType_ = t; }
		COMPONENT_TYPE componentType()const { return componentType_; }
	
		EventDispatcher & dispatcher();

	private:
		virtual bool	process();
		bool			findInterfaces();

	private:
		COMPONENTS								_baseapps;
		COMPONENTS								_cellapps;
		COMPONENTS								_dbmgrs;
		COMPONENTS								_loginapps;
		COMPONENTS								_cellappmgrs;
		COMPONENTS								_baseappmgrs;
		COMPONENTS								_machines;
		COMPONENTS								_messagelogs;
		COMPONENTS								_billings;
		COMPONENTS								_bots;
		COMPONENTS								_consoles;

		// 组件的全局启动次序log和组(相同的组件为一组， 如：所有baseapp为一个组)启动次序log
		// 注意:中途有死掉的app组件这里log并不去做减操作, 从使用意图来看也没有必要做这个匹配。
		ORDER_LOG								_globalOrderLog;
		ORDER_LOG								_baseappGrouplOrderLog;
		ORDER_LOG								_cellappGrouplOrderLog;
		ORDER_LOG								_loginappGrouplOrderLog;

		ComponentsNotificationHandler*			_pHandler;

		// 以下组网用
		COMPONENT_TYPE							componentType_;
		// 本组件的ID
		COMPONENT_ID							componentID_;									
		uint8									state_;
		int16									findIdx_;
		int8									findComponentTypes_[8];
	};

}

#endif // KBE_ENGINE_COMPONENT_MGR_HPP
