//°ÔName				:   SafeStreamQueue.cpp
//°ÔCompiler			:	Microsoft Visual C++ 9.0
//°ÔVersion				:	1.00
//°ÔCreate Date			:	05/31/2009
//°ÔLastModified		:	05/31/2009
//°ÔCopyright (C)		:	
//°ÔWriten  by			:   
//°ÔMode  by			:   
//°ÔBrief				:	
//////////////////////////////////////////////////////////////////////////
#include "StdAfx.h"
#include "Config.h"
#include "MysqlStream.h"
#include "SafeStreamQueue.h"

namespace Beton
{
//--------------------------------------------------------------------------------------------------------
// Œˆππ∫Ø ˝
//--------------------------------------------------------------------------------------------------------
SafeStreamQueue::~SafeStreamQueue()
{
	// ASSERT( 0 == m_lNum );
	if( m_hEvent )
		::CloseHandle(m_hEvent);
}

} // namespace Beton