/*
--------------------------------------------------------------------------------------------------
		file name	:
		desc		: 服务器数据库表维护
		author		: ljp

		log			:
--------------------------------------------------------------------------------------------------
*/

#ifndef KBE_KBE_TABLES_H
#define KBE_KBE_TABLES_H

#include "entity_table.h"
#include "common/common.hpp"
#include "common/memorystream.hpp"
#include "helper/debug_helper.hpp"






namespace KBEngine { 


/*
	kbe系统表
*/
class KBEEntityLogTable : public EntityTable
{
public:
	struct EntityLog
	{
		DBID dbid;
		ENTITY_ID entityID;
		char ip[MAX_IP];
		uint16 port;
		COMPONENT_ID componentID;
	};

	KBEEntityLogTable(): EntityTable()
	{
		tableName("kbe_entitylog");
	}
	
	virtual ~KBEEntityLogTable()
	{
	}
	
	
protected:
	
};

class KBEAccountTable : public EntityTable
{
public:
	KBEAccountTable(): EntityTable(),
					   accountDefMemoryStream_()
	{
		tableName("accountinfos");
	}
	
	virtual ~KBEAccountTable()
	{
	}

	virtual bool			queryAccount(DBInterface * dbi, const std::string& name, ACCOUNT_INFOS& info) = 0;
	virtual bool			logAccount(DBInterface * dbi, ACCOUNT_INFOS& info) = 0;
	virtual bool			setFlagsDeadline(DBInterface * dbi, const std::string& name, uint32 flags, uint64 deadline) = 0;
	virtual bool			updateCount(DBInterface * dbi, DBID dbid) = 0;
	virtual bool			queryAccountAllInfos(DBInterface * dbi, const std::string& name, ACCOUNT_INFOS& info) = 0;
	virtual bool			updatePassword(DBInterface * dbi, const std::string& name, const std::string& password) = 0;

	MemoryStream& accountDefMemoryStream()
	{ 
		return accountDefMemoryStream_; 
	}

	void accountDefMemoryStream(MemoryStream& s)
	{
		accountDefMemoryStream_.clear(false);
		accountDefMemoryStream_.append(s.data() + s.rpos(), s.length()); 
	}
protected:
	MemoryStream			accountDefMemoryStream_;
};

class KBEEmailVerificationTable : public EntityTable
{
public:
	enum V_TYPE
	{
		V_TYPE_CREATEACCOUNT = 1,
		V_TYPE_RESETPASSWORD = 2,
		V_TYPE_BIND_MAIL = 3
	};

	KBEEmailVerificationTable(): EntityTable()
	{
		tableName("kbe_email_verification");
	}
	
	virtual ~KBEEmailVerificationTable()
	{
	}

	virtual bool queryAccount(DBInterface * dbi, int8 type, const std::string& name, ACCOUNT_INFOS& info) = 0;
	virtual bool logAccount(DBInterface * dbi, int8 type, const std::string& name, const std::string& datas, const std::string& code) = 0;
	virtual bool delAccount(DBInterface * dbi, int8 type, const std::string& name) = 0;
	virtual bool activateAccount(DBInterface * dbi, const std::string& code, ACCOUNT_INFOS& info) = 0;
	virtual bool bindEMail(DBInterface * dbi, const std::string& name, const std::string& code) = 0;
	virtual bool resetpassword(DBInterface * dbi, const std::string& name, const std::string& password, const std::string& code) = 0;
protected:
};

}

#endif // KBE_KBE_TABLES_H
