/*
---------------------------------------------------------------------------------------------
		file name :
		desc	  : ³ÌÐòÈë¿Ú
		author    : ljp

		log       : by ljp create 2017-06-11
---------------------------------------------------------------------------------------------
*/
#include "cellappmgr.h"





using namespace KBEngine;
int main(int argc, char* argv[])
{
	Cellappmgr app;
	if (app.Initialize(CELLAPPMGR_TYPE))
	{
		app.MainLoop();
	}
	
	return 0;
}
