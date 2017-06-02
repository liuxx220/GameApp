/*
----------------------------------------------------------------------------
		file name : loginapp.hpp
		desc	  : 登录服务器
		author    : LJP

		log		  : [ 2015-04-25 ]
----------------------------------------------------------------------------
*/
#ifndef KBE_LOGINAPP_HPP
#define KBE_LOGINAPP_HPP	
#include "server/serverconfig.hpp"
#include "network/event_dispatcher.hpp"
#include "common/timer.hpp"

	



namespace KBEngine
{

	class Loginapp : public Singleton<Loginapp>
	{

	public:
	
		 Loginapp();
		~Loginapp();
	

		bool				Initialize( COMPONENT_TYPE componentType );
		void				MainLoop( void );
	
		
		/* 初始化相关接口 */
		bool				InitializeEnd();
		void				Destroy(void);


		void				handleTimeout(TimerHandle handle, void * arg);

		
	protected:

		COMPONENT_TYPE		mComponentType;

	};

}

#endif // KBE_LOGINAPP_HPP
