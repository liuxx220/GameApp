/*
-----------------------------------------------------------------------------------------------
		file name : common.h
		desc      : 网络层一些数据定义
		author	  : LJP

		log		  : [2015-04-26]
-----------------------------------------------------------------------------------------------
*/

#ifndef KBE_NETWORK_COMMON_HPP
#define KBE_NETWORK_COMMON_HPP
#include "common/common.hpp"
#include "helper/debug_option.hpp"
// windows include	
#if KBE_PLATFORM == PLATFORM_WIN32
#else
// linux include
#endif




namespace KBEngine 
{ 
		const uint32 BROADCAST = 0xFFFFFFFF;
		const uint32 LOCALHOST = 0x0100007F;

		typedef int32	ChannelID;
		const ChannelID CHANNEL_ID_NULL = 0;

		// 通道超时时间
		extern float g_channelInternalTimeout;
		extern float g_channelExternalTimeout;

		// 通道发送超时重试
		extern uint32 g_intReSendInterval;
		extern uint32 g_intReSendRetries;
		extern uint32 g_extReSendInterval;
		extern uint32 g_extReSendRetries;

		// 外部通道加密类别
		extern int8 g_channelExternalEncryptType;

		// listen监听队列最大值
		extern uint32 g_SOMAXCONN;

		


		// 加密额外存储的信息占用字节(长度+填充)
		#define ENCRYPTTION_WASTAGE_SIZE			(1 + 7)

		#define PACKET_MAX_SIZE						1500
		#ifndef PACKET_MAX_SIZE_TCP
		#define PACKET_MAX_SIZE_TCP					1460
		#endif
		#define PACKET_MAX_SIZE_UDP					1472
		#define PACKET_LENGTH_SIZE					sizeof(uint16)

		#define NETWORK_MESSAGE_ID_SIZE				sizeof(int32)
		#define NETWORK_MESSAGE_LENGTH_SIZE			sizeof(int32)
		#define NETWORK_MESSAGE_LENGTH1_SIZE		sizeof(int32)
		#define NETWORK_MESSAGE_MAX_SIZE			65535
		#define NETWORK_MESSAGE_MAX_SIZE1			4294967295

		// 游戏内容可用包大小
		#define GAME_PACKET_MAX_SIZE_TCP			PACKET_MAX_SIZE_TCP - NETWORK_MESSAGE_ID_SIZE - \
													NETWORK_MESSAGE_LENGTH_SIZE - ENCRYPTTION_WASTAGE_SIZE

		/** kbe machine端口 */
		#define KBE_PORT_START						20000
		#define KBE_MACHINE_BRAODCAST_SEND_PORT		KBE_PORT_START + 86			// machine接收广播的端口
		#define KBE_PORT_BROADCAST_DISCOVERY		KBE_PORT_START + 87
		#define KBE_MACHINE_TCP_PORT				KBE_PORT_START + 88

		#define KBE_BILLING_TCP_PORT				30099

		/*
			网络消息类型， 定长或者变长。
			如果需要自定义长度则在NETWORK_INTERFACE_DECLARE_BEGIN中声明时填入长度即可。
		*/
		#ifndef NETWORK_FIXED_MESSAGE
		#define NETWORK_FIXED_MESSAGE 0
		#endif

		#ifndef NETWORK_VARIABLE_MESSAGE
		#define NETWORK_VARIABLE_MESSAGE -1
		#endif

		#define DEFAULT_SENDBUF_SIZE	65536
		#define DEFAULT_RECVBUF_SIZE	65536
		#define INVALID_SOCKET			(SOCKET)(~0)
		enum ProtocolType
		{
			PROTOCOL_TCP = 0,
			PROTOCOL_UDP = 1,
		};

		enum Reason
		{
			REASON_SUCCESS = 0,				 ///< No reason.
			REASON_TIMER_EXPIRED = -1,		 ///< Timer expired.
			REASON_NO_SUCH_PORT = -2,		 ///< Destination port is not open.
			REASON_GENERAL_NETWORK = -3,	 ///< The network is stuffed.
			REASON_CORRUPTED_PACKET = -4,	 ///< Got a bad packet.
			REASON_NONEXISTENT_ENTRY = -5,	 ///< Wanted to call a null function.
			REASON_WINDOW_OVERFLOW = -6,	 ///< Channel send window overflowed.
			REASON_INACTIVITY = -7,			 ///< Channel inactivity timeout.
			REASON_RESOURCE_UNAVAILABLE = -8,///< Corresponds to EAGAIN
			REASON_CLIENT_DISCONNECTED = -9, ///< Client disconnected voluntarily.
			REASON_TRANSMIT_QUEUE_FULL = -10,///< Corresponds to ENOBUFS
			REASON_CHANNEL_LOST = -11,		 ///< Corresponds to channel lost
			REASON_SHUTTING_DOWN = -12,		 ///< Corresponds to shutting down app.
			REASON_HTML5_ERROR = -13,		 ///< html5 error.
			REASON_CHANNEL_CONDEMN = -14,	 ///< condemn error.
		};

		inline
		const char * reasonToString(Reason reason)
		{
			const char * reasons[] =
			{
				"REASON_SUCCESS",
				"REASON_TIMER_EXPIRED",
				"REASON_NO_SUCH_PORT",
				"REASON_GENERAL_NETWORK",
				"REASON_CORRUPTED_PACKET",
				"REASON_NONEXISTENT_ENTRY",
				"REASON_WINDOW_OVERFLOW",
				"REASON_INACTIVITY",
				"REASON_RESOURCE_UNAVAILABLE",
				"REASON_CLIENT_DISCONNECTED",
				"REASON_TRANSMIT_QUEUE_FULL",
				"REASON_CHANNEL_LOST",
				"REASON_SHUTTING_DOWN",
				"REASON_HTML5_ERROR",
				"REASON_CHANNEL_CONDEMN"
			};

			unsigned int index = -reason;

			if (index < sizeof(reasons)/sizeof(reasons[0]))
			{
				return reasons[index];
			}

			return "REASON_UNKNOWN";
		}

		// network stats
		extern uint64						g_numPacketsSent;
		extern uint64						g_numPacketsReceived;
		extern uint64						g_numBytesSent;
		extern uint64						g_numBytesReceived;


		namespace NetUnity
		{
			// 工具类的接口暂时在这里定义吧
			int					getlocaladdress(KBESOCKET sock, UINT16 * networkPort, UINT32 * networkAddr);
			int					getremoteaddress(KBESOCKET sock, UINT16 * networkPort, UINT32 * networkAddr);
		}
}

#endif // KBE_NETWORK_COMMON_HPP
