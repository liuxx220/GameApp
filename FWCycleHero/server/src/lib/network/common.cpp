/*
This source file is part of KBEngine
For the latest info, see http://www.kbengine.org/

Copyright (c) 2008-2012 KBEngine.

KBEngine is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

KBEngine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.
 
You should have received a copy of the GNU Lesser General Public License
along with KBEngine.  If not, see <http://www.gnu.org/licenses/>.
*/


#include "common.hpp"


namespace KBEngine 
{ 

		float g_channelInternalTimeout = 60.f;
		float g_channelExternalTimeout = 60.f;

		int8 g_channelExternalEncryptType = 0;

		uint32 g_SOMAXCONN = 5;

		// network stats
		uint64						g_numPacketsSent = 0;
		uint64						g_numPacketsReceived = 0;
		uint64						g_numBytesSent = 0;
		uint64						g_numBytesReceived = 0;

		
		// 通道发送超时重试
		uint32						g_intReSendInterval = 10;
		uint32						g_intReSendRetries = 0;
		uint32						g_extReSendInterval = 10;
		uint32						g_extReSendRetries = 3;

		namespace NetUnity
		{

			int getlocaladdress(KBESOCKET sock, UINT16 * networkPort, UINT32 * networkAddr)
			{
				sockaddr_in		sin;
				socklen_t		sinLen = sizeof(sin);
				int ret = ::getsockname(sock, (struct sockaddr*)&sin, &sinLen);
				if (ret == 0)
				{
					if (networkPort != NULL) *networkPort = sin.sin_port;
					if (networkAddr != NULL) *networkAddr = sin.sin_addr.s_addr;
				}
				return ret;
			}

			int getremoteaddress(KBESOCKET sock, UINT16 * networkPort, UINT32 * networkAddr)
			{
				sockaddr_in		sin;
				socklen_t		sinLen = sizeof(sin);
				int ret = ::getpeername(sock, (struct sockaddr*)&sin, &sinLen);
				if (ret == 0)
				{
					if (networkPort != NULL) *networkPort = sin.sin_port;
					if (networkAddr != NULL) *networkAddr = sin.sin_addr.s_addr;
				}
				return ret;
			}
		}
}
