/*
-------------------------------------------------------------------------------------
		file name	: interfaces.hpp
		desc		: 一些消息通知处理器的接口
		author		: ljp

		log			: [ 2015-04-28 ]
-------------------------------------------------------------------------------------
*/

#ifndef KBE_NETWORK_INTERFACES_HPP
#define KBE_NETWORK_INTERFACES_HPP





namespace KBEngine 
{ 
		class MessageHandler;

		/** 此类接口用于接收普通的Network输入消息通知处理器
		*/
		class InputNotificationHandler
		{
		public:
			virtual ~InputNotificationHandler() {};
			virtual int handleInputNotification(int fd) { return 0; };
		};

		/** 此类接口用于接收普通的Network输出消息通知处理器
		*/
		class OutputNotificationHandler
		{
		public:
			virtual ~OutputNotificationHandler() {};
			virtual int handleOutputNotification(int fd) = 0;
		};

		/** 此类接口用于监听NetworkStats事件
		*/
		class NetworkStatsHandler
		{
		public:
			virtual void onSendMessage(const MessageHandler& msgHandler, int size) = 0;
			virtual void onRecvMessage(const MessageHandler& msgHandler, int size) = 0;
		};
}

#endif // KBE_NETWORK_INTERFACES_HPP
