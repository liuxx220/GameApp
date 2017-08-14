using System;
using LuaInterface;

public class SimpleFramework_AppConstWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateSimpleFramework_AppConst),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("DebugMode",       get_DebugMode, null),
			new LuaField("ExampleMode",     get_ExampleMode, null),
			new LuaField("UpdateMode",      get_UpdateMode, null),
			new LuaField("AutoWrapMode",    get_AutoWrapMode, null),
			new LuaField("UsePbc",          get_UsePbc, null),
			new LuaField("UseLpeg",         get_UseLpeg, null),
			new LuaField("UsePbLua",        get_UsePbLua, null),
			new LuaField("UseCJson",        get_UseCJson, null),
			new LuaField("UseSproto",       get_UseSproto, null),
			new LuaField("LuaEncode",       get_LuaEncode, null),
			new LuaField("TimerInterval",   get_TimerInterval, null),
			new LuaField("GameFrameRate",   get_GameFrameRate, null),
			new LuaField("AppName",         get_AppName, null),
			new LuaField("AppPrefix",       get_AppPrefix, null),
			new LuaField("WebUrl",          get_WebUrl, null),
			new LuaField("UserId",          get_UserId, set_UserId),
			new LuaField("SocketPort",      get_SocketPort, set_SocketPort),
			new LuaField("SocketAddress",   get_SocketAddress, set_SocketAddress),
			new LuaField("LuaBasePath",     get_LuaBasePath, null),
			new LuaField("LuaWrapPath",     get_LuaWrapPath, null),
		};

        LuaScriptMgr.RegisterLib(L, "ClientAppArgs", typeof(ClientAppArgs), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSimpleFramework_AppConst(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
            ClientAppArgs obj = new ClientAppArgs();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.AppConst.New");
		}

		return 0;
	}

    static Type classType = typeof(ClientAppArgs);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DebugMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.DebugMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ExampleMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.ExampleMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UpdateMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UpdateMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AutoWrapMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.AutoWrapMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UsePbc(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UsePbc);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UseLpeg(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UseLpeg);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UsePbLua(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UsePbLua);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UseCJson(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UseCJson);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UseSproto(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UseSproto);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaEncode(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.LuaEncode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TimerInterval(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.TimerInterval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GameFrameRate(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.GameFrameRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppName(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.AppName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppPrefix(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.AppPrefix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WebUrl(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.WebUrl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UserId(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.UserId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SocketPort(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.SocketPort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SocketAddress(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.SocketAddress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaBasePath(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.LuaBasePath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaWrapPath(IntPtr L)
	{
		LuaScriptMgr.Push(L, ClientAppArgs.LuaWrapPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UserId(IntPtr L)
	{
		ClientAppArgs.UserId = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SocketPort(IntPtr L)
	{
		ClientAppArgs.SocketPort = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SocketAddress(IntPtr L)
	{
		ClientAppArgs.SocketAddress = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

