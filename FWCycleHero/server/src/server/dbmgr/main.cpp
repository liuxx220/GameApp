/*
------------------------------------------------------------------------------------------------------------------
file Name	:
desc		: DBMgr ³ÌÐòÈë¿Ú
author		:
log			:
------------------------------------------------------------------------------------------------------------------
*/

#include "server/glw_main.hpp"
#include "dbmgr.hpp"



using namespace KBEngine;
int main(int argc, char* argv[])
{
	loadConfig();
	ENGINE_COMPONENT_INFO& info = g_kbeSrvConfig.getDBMgr();
	return kbeMainT<AppDBServer>(argc, argv, DBMGR_TYPE, -1, -1);
}
