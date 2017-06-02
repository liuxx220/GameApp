/*
----------------------------------------------------------------------------------------------------
		file name : main.cpp
		desc	  : 登录服务器程序入口文件
		author    : LJP

		log		  : [2015-04-26]
----------------------------------------------------------------------------------------------------
*/
#include "loginapp.hpp"




using namespace KBEngine;																				
int main(int argc, char* argv[])																						
{		

	
	Loginapp app;
	if (app.Initialize(LOGINAPP_TYPE))
	{
		app.MainLoop();
	}

	app.Destroy();
	
	return 0;
}


