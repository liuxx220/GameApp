/*
----------------------------------------------------------------------------------------------------
		file name : 
		desc      : 场景服务器程序入口
		author	  : ljp

		log		  : by ljp create 2017-06-11
----------------------------------------------------------------------------------------------------
*/
#include "cellapp.h"




using namespace KBEngine;
int main(int argc, char* argv[])
{
	CellServerApp app;
	if (app.Initialize(CELLAPP_TYPE))
	{
		app.MainLoop();
	}
	return 0; 
}
