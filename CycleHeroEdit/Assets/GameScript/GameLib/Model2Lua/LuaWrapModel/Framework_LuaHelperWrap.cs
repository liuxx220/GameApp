using System;
using LuaInterface;
using LuaFramework;




public class Framework_LuaHelperWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetType",                GetType),
			new LuaMethod("GetPanelManager",        GetPanelManager),
			new LuaMethod("GetResManager",          GetResManager),
            new LuaMethod("GetLevelManager",        GetLevelManager),
			new LuaMethod("GetNetworkManager",      GetNetworkManager),
			//new LuaMethod("GetMusicManager",      GetMusicManager),
            new LuaMethod("GetEventManager",        GetEventManager),
			new LuaMethod("Action",                 Action),
			new LuaMethod("VoidDelegate",           VoidDelegate),
			new LuaMethod("OnCallLuaFunc",          OnCallLuaFunc),
			new LuaMethod("OnJsonCallFunc",         OnJsonCallFunc),
			new LuaMethod("New",                    CreateFramework_LuaHelper),
			new LuaMethod("GetClassType",           GetClassType),
		};

        LuaScriptMgr.RegisterLib(L, "Framework.LuaHelper", regs);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateFramework_LuaHelper(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Framework.LuaHelper class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(LuaHelper);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Type o      = LuaHelper.GetType(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetPanelManager(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 0);
        GameUIManager o = LuaHelper.GetPanelManager();
        LuaScriptMgr.Push(L, o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetResManager(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 0);
        CBundleManager o = LuaHelper.GetResManager();
        LuaScriptMgr.Push(L, o);
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetLevelManager(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 0);
        CLoadLevelMgr o = LuaHelper.GetLevelManager();
        LuaScriptMgr.Push(L, o);
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetNetworkManager(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 0);
        NetworkManager o = LuaHelper.GetNetworkManager();
        LuaScriptMgr.Push(L, o);
        return 1;
    }
    // 
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetEventManager(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 0);
        CEventCenterMgr o = LuaHelper.GetEventManager();
        LuaScriptMgr.Push(L, o);
        return 1;
    }

    //[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    //static int GetMusicManager(IntPtr L)
    //{
    //    LuaScriptMgr.CheckArgsCount(L, 0);
    //    SimpleFramework.Manager.MusicManager o = LuaHelper.GetMusicManager();
    //    LuaScriptMgr.Push(L, o);
    //    return 1;
    //}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Action(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Action o         = LuaHelper.Action(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0               = LuaScriptMgr.GetLuaFunction(L, 1);
		UIEventListener.VoidDelegate o = LuaHelper.VoidDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCallLuaFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaStringBuffer arg0 = LuaScriptMgr.GetStringBuffer(L, 1);
		LuaFunction arg1     = LuaScriptMgr.GetLuaFunction(L, 2);
		LuaHelper.OnCallLuaFunc(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnJsonCallFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0      = LuaScriptMgr.GetLuaString(L, 1);
		LuaFunction arg1 = LuaScriptMgr.GetLuaFunction(L, 2);
		LuaHelper.OnJsonCallFunc(arg0,arg1);
		return 0;
	}
}

