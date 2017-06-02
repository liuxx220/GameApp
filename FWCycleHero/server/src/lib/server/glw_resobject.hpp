/*
----------------------------------------------------------------------------------------
		file name : glw_resobject.hpp
		desc	  : 服务器端资源对象的描述
		author    : LJP

		log       : [ 2015-04-25]
----------------------------------------------------------------------------------------
*/
#ifndef KBE_RESOURCE_OBJECT_HPP
#define KBE_RESOURCE_OBJECT_HPP

#include "helper/debug_helper.hpp"
#include "common/common.hpp"
#include "common/smartpointer.hpp"	

namespace KBEngine
{

	class ResourceObject : public RefCountable
	{
	public:
		ResourceObject(const char* res, uint32 flags);
		virtual ~ResourceObject();

		const std::string& resName(){ return resName_; }
		uint32			flags()const{ return flags_; }

		bool			valid()const;
		void			update();

	protected:
		std::string		resName_;
		uint32			flags_;
		bool			invalid_;
		uint64			timeout_;
	};

	class FileObject : public ResourceObject
	{
	public:
		FileObject(const char* res, uint32 flags, const char* model);
		virtual ~FileObject();
	
		FILE*			fd(){ return fd_; }

		bool			seek(uint32 idx, int flags = SEEK_SET);
		uint32			read(char* buf, uint32 limit);
		uint32			tell();
	protected:
		FILE*			fd_;
	};

	typedef SmartPointer<ResourceObject> ResourceObjectPtr;
}

#endif // KBE_RESOURCE_OBJECT_HPP
