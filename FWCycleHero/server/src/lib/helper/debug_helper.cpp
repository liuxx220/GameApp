
#include "debug_helper.hpp"
#include "profile.hpp"
#include "common/common.hpp"
#include "common/timer.hpp"
#include "thread/threadguard.hpp"
#include "network/event_dispatcher.hpp"



#if KBE_PLATFORM == PLATFORM_UNIX
#include <unistd.h>
#include <syslog.h>
#endif


#if KBE_PLATFORM == PLATFORM_WIN32
	#pragma comment (lib, "Mswsock.lib")
	#pragma comment( lib, "odbc32.lib" )
#endif



namespace KBEngine
{
	
	KBE_SINGLETON_INIT(DebugHelper);

	DebugHelper dbghelper;


#define DBG_PT_SIZE 1024 * 4

bool g_shouldWriteToSyslog = false;

#ifdef KBE_USE_ASSERTS
void myassert(const char * exp, const char * func, const char * file, unsigned int line)
{
	std::string s = (fmt::format("assertion failed: {}, file {}, line {}, at: {}\n", exp, file, line, func));
	printf("%s", (std::string("[ASSERT]: ") + s).c_str());
	dbghelper.print_msg(s);
    abort();
}
#endif

//-------------------------------------------------------------------------------------
void utf8printf(FILE *out, const char *str, ...)
{
    va_list ap;
    va_start(ap, str);
    vutf8printf(stdout, str, &ap);
    va_end(ap);
}

//-------------------------------------------------------------------------------------
void vutf8printf(FILE *out, const char *str, va_list* ap)
{
    vfprintf(out, str, *ap);
}



//-------------------------------------------------------------------------------------
DebugHelper::DebugHelper():	_logfile(NULL),_currFile(),_currFuncName(),_currLine(0)
							,noSyncLog_(false)
							,canLogFile_(true)
{
	
}

//-------------------------------------------------------------------------------------
DebugHelper::~DebugHelper()
{
	finalise();
}	

//-------------------------------------------------------------------------------------
void DebugHelper::shouldWriteToSyslog(bool v)
{
	g_shouldWriteToSyslog = v;
}


//-------------------------------------------------------------------------------------
void DebugHelper::changeLogger(std::string name)
{

}


//-------------------------------------------------------------------------------------
void DebugHelper::initialize(COMPONENT_TYPE componentType)
{

}

//-------------------------------------------------------------------------------------
void DebugHelper::finalise()
{
	DebugHelper::getSingleton().clearBufferedLog(true);
}

//-------------------------------------------------------------------------------------
void DebugHelper::clearBufferedLog(bool destroy)
{
	
}

//-------------------------------------------------------------------------------------
void DebugHelper::sync()
{
	
}


//-------------------------------------------------------------------------------------
void DebugHelper::printmsg(INT32 eType, const char* format, ...)
{
	char szBuf[1024] = { 0 };
	int pos = 0;
	va_list arg;
	va_start(arg, format);
	pos = vsnprintf(szBuf, 1024, format, arg);
	va_end(arg);

	//vsnprintf(szBuf, sizeof(szBuf), format, arg);
	printf(szBuf);
}


//-------------------------------------------------------------------------------------
void DebugHelper::print_msg(const std::string& s)
{

	printmsg(KBELOG_PRINT, s.c_str());
}

//-------------------------------------------------------------------------------------
void DebugHelper::error_msg(const std::string& s)
{
	printmsg(KBELOG_ERROR, s.c_str());
}

//-------------------------------------------------------------------------------------
void DebugHelper::info_msg(const std::string& s)
{
	printmsg(KBELOG_INFO, s.c_str());
}


//-------------------------------------------------------------------------------------
void DebugHelper::debug_msg(const std::string& s)
{

	printmsg(KBELOG_DEBUG, s.c_str());
}

//-------------------------------------------------------------------------------------
void DebugHelper::warning_msg(const std::string& s)
{

	printmsg(KBELOG_WARNING, s.c_str());
}

//-------------------------------------------------------------------------------------
void DebugHelper::critical_msg(const std::string& s)
{
	
	char buf[DBG_PT_SIZE];
	kbe_snprintf(buf, DBG_PT_SIZE, "%s(%d) -> %s\n\t%s\n", _currFile.c_str(), _currLine, _currFuncName.c_str(), s.c_str());
	printmsg(KBELOG_CRITICAL, buf);
}
}


