/*
------------------------------------------------------------------------------------
		file name : glw_resmgr.hpp
		desc	  : 资源管理器对象
		author    : LJP

		log		  : [2015-04-25]
------------------------------------------------------------------------------------
*/
#ifndef KBE_RESMGR_HPP
#define KBE_RESMGR_HPP

#include "glw_resobject.hpp"
#include "common/common.hpp"
#include "common/singleton.hpp"
#include "common/timer.hpp"
#include "xmlplus/xmlplus.hpp"	
#include "common/smartpointer.hpp"
	
namespace KBEngine
{

	#define RESOURCE_NORMAL	0x00000000
	#define RESOURCE_RESIDENT 0x00000001
	#define RESOURCE_READ 0x00000002
	#define RESOURCE_WRITE 0x00000004
	#define RESOURCE_APPEND 0x00000008

	class Resmgr : public Singleton<Resmgr>, public TimerHandler
	{

	public:
		// 引擎环境变量
		struct KBEEnv
		{
			std::string root;
			std::string res_path;
			std::string bin_path;
		};

		static uint64 respool_timeout;
		static uint32 respool_buffersize;
		static uint32 respool_checktick;
	public:
		Resmgr();
		~Resmgr();
	
		bool initialize();

		void autoSetPaths();
		void updatePaths();

		const Resmgr::KBEEnv& getEnv() { return kb_env_; }

		/*
			从资源路径中(环境变量中指定的)匹配到完整的资源地址
		*/
		std::string matchRes(const std::string& res);
		std::string matchRes(const char* res);
	
		bool hasRes(const std::string& res);
	
		FILE* openRes(std::string res, const char* mode = "r");

		/*
			列出目录下所有的资源文件
		*/
		bool listPathRes(std::wstring path, const std::wstring& extendName, std::vector<std::wstring>& results);

		/*
			从资源路径中(环境变量中指定的)匹配到目录
		*/
		std::string matchPath(const std::string& path);
		std::string matchPath(const char* path);

		const std::vector<std::string>& respaths() { 
			return respaths_; 
		}

		void print(void);

		bool isInit(){ 
			return isInit_; 
		}

		/**
			获得引擎系统级资源目录
			kbe\\res\\*
		*/
		std::string getPySysResPath();

		/**
			获得用户级资源目录
			demo\\res\\*
		*/
		std::string getPyUserResPath();

		/**
			获得用户级脚本目录
			demo\\scripts\\*
		*/
		std::string getPyUserScriptsPath();

		ResourceObjectPtr openResource(const char* res, const char* model, 
										uint32 flags = RESOURCE_NORMAL);

		void update();
	private:

		virtual void handleTimeout(TimerHandle handle, void * arg);

		KBEEnv kb_env_;
		std::vector<std::string> respaths_;
		bool isInit_;

		UNORDERED_MAP< std::string, ResourceObjectPtr > respool_;

		KBEngine::thread::ThreadMutex mutex_;
	};

}

#endif // KBE_RESMGR_HPP
