/*
------------------------------------------------------------------------------------------------------------------
		file Name	: 
		desc		: 付费处理模块
		author		:
		log			:
------------------------------------------------------------------------------------------------------------------
*/

#ifndef KBE_BILLING_HANDLER_HPP
#define KBE_BILLING_HANDLER_HPP

// common include	
// #define NDEBUG
#include "dbtasks.hpp"
#include "common/common.hpp"
#include "common/memorystream.hpp"
#include "thread/threadtask.hpp"
#include "helper/debug_helper.hpp"
#include "thread/threadpool.hpp"

namespace KBEngine
{ 


	class EndPoint;
	class DBThreadPool;

	/*
		处理计费、第三方运营账号、注册登录系统等挂接
	*/
	/*class BillingHandler
	{
	public:
		BillingHandler(thread::ThreadPool& threadPool, DBThreadPool& dbThreadPool);
		virtual ~BillingHandler();
	
		virtual bool			initialize() = 0;

		virtual void			eraseClientReq(Channel* pChannel, std::string& logkey) = 0;

		virtual bool			createAccount(Channel* pChannel, std::string& registerName, 
												std::string& password, std::string& datas, ACCOUNT_TYPE uatype) = 0;


		virtual bool			loginAccount(Channel* pChannel, std::string& loginName, 
												std::string& password, std::string& datas) = 0;

		virtual void			onCreateAccountCB(KBEngine::MemoryStream& s) = 0;

		virtual void			onLoginAccountCB(KBEngine::MemoryStream& s) = 0;

		virtual void			charge(Channel* pChannel, KBEngine::MemoryStream& s) = 0;
		virtual void			onChargeCB(KBEngine::MemoryStream& s) = 0;

		virtual void			accountActivate(Channel* pChannel, std::string& scode) = 0;
		virtual void			accountReqResetPassword(Channel* pChannel, std::string& accountName) = 0;
		virtual void			accountResetPassword(Channel* pChannel, std::string& accountName, std::string& newpassword, std::string& scode) = 0;
		virtual void			accountReqBindMail(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& email) = 0;
		virtual void			accountBindMail(Channel* pChannel, std::string& username, std::string& scode) = 0;
		virtual void			accountNewPassword(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& newpassword) = 0;
	protected:
		DBThreadPool&			dbThreadPool_;
		thread::ThreadPool&		threadPool_;
	};

	class BillingHandler_Normal : public BillingHandler
	{
	public:
		BillingHandler_Normal(thread::ThreadPool& threadPool, DBThreadPool& dbThreadPool);
		virtual ~BillingHandler_Normal();
	
		virtual bool			initialize(){ return true; }

		virtual void			eraseClientReq(Channel* pChannel, std::string& logkey);

		virtual bool			createAccount(Channel* pChannel, std::string& registerName, 
												std::string& password, std::string& datas, ACCOUNT_TYPE uatype);

		virtual bool			loginAccount(Channel* pChannel, std::string& loginName, 
												std::string& password, std::string& datas);

		virtual void			onCreateAccountCB(KBEngine::MemoryStream& s);

		virtual void			onLoginAccountCB(KBEngine::MemoryStream& s);

		virtual void			charge(Channel* pChannel, KBEngine::MemoryStream& s);

		virtual void			onChargeCB(KBEngine::MemoryStream& s);

		virtual void			accountActivate(Channel* pChannel, std::string& scode);
		virtual void			accountReqResetPassword(Channel* pChannel, std::string& accountName);
		virtual void			accountResetPassword(Channel* pChannel, std::string& accountName, std::string& newpassword, std::string& scode);
		virtual void			accountReqBindMail(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& email);
		virtual void			accountBindMail(Channel* pChannel, std::string& username, std::string& scode);
		virtual void			accountNewPassword(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& newpassword);
		protected:
	};

	class BillingHandler_ThirdParty : public BillingHandler_Normal, public thread::TPTask
	{
	public:
		BillingHandler_ThirdParty(thread::ThreadPool& threadPool, DBThreadPool& dbThreadPool);
		virtual ~BillingHandler_ThirdParty();
	
		virtual bool			initialize();

		virtual void			eraseClientReq(Channel* pChannel, std::string& logkey);

		virtual bool			createAccount(Channel* pChannel, std::string& registerName, 
												std::string& password, std::string& datas, ACCOUNT_TYPE uatype);

		virtual bool			loginAccount(Channel* pChannel, std::string& loginName, 
												std::string& password, std::string& datas);

		virtual void			onCreateAccountCB(KBEngine::MemoryStream& s);

		virtual void			onLoginAccountCB(KBEngine::MemoryStream& s);

		virtual void			charge(Channel* pChannel, KBEngine::MemoryStream& s);

		virtual void			onChargeCB(KBEngine::MemoryStream& s);

		virtual void			accountActivate(Channel* pChannel, std::string& scode);
		virtual void			accountReqResetPassword(Channel* pChannel, std::string& accountName);
		virtual void			accountResetPassword(Channel* pChannel, std::string& accountName, std::string& newpassword, std::string& scode);
		virtual void			accountReqBindMail(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& email);
		virtual void			accountBindMail(Channel* pChannel, std::string& username, std::string& scode);
		virtual void			accountNewPassword(Channel* pChannel, ENTITY_ID entityID, std::string& accountName, std::string& password, std::string& newpassword);

		bool					reconnect();

		virtual bool			process();
	protected:
		Channel*				pBillingChannel_;
	};

	class BillingHandlerFactory
	{
	public:
		static BillingHandler* create(std::string type, thread::ThreadPool& threadPool, 
										DBThreadPool& dbThreadPool);
	};
*/
}

#endif // KBE_BILLING_HANDLER_HPP
