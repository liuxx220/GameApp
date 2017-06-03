using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;



public class Framework_GameUIManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("CreatePanel",        CreatePanel),
            new LuaMethod("DestoryFrame",       DestoryFrame),
			new LuaMethod("New",                _CreateFramework_GameUIManager),
			new LuaMethod("GetClassType",       GetClassType),
            new LuaMethod("HidePanel",          HidePanel),
            new LuaMethod("ShowPanel",          ShowPanel),        
			new LuaMethod("__eq",               Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{

		};

        LuaScriptMgr.RegisterLib(L, "Framework.GameUIManager", typeof(GameUIManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateFramework_GameUIManager(IntPtr L)
	{
        LuaDLL.luaL_error(L, "Framework.GameUIManager class does not have a constructor function");
		return 0;
	}

    static Type classType = typeof(GameUIManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

    /// <summary>
    /// 创建界面
    /// </summary>
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
        GameUIManager obj  = (GameUIManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.PanelManager");
		string arg0        = LuaScriptMgr.GetLuaString(L, 2);
		LuaFunction arg1   = LuaScriptMgr.GetLuaFunction(L, 3);
		obj.CreatePanel( arg0, arg1);
		return 0;
	}


    /// <summary>
    /// 显示界面
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int DestoryFrame(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        GameUIManager obj   = (GameUIManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.PanelManager");
        string strFrame     = LuaScriptMgr.GetLuaString(L, 2);
        obj.DestoryFrame(strFrame);
        return 0;
    }

    /// <summary>
    /// 显示界面
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ShowPanel( IntPtr L )
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        GameUIManager obj   = (GameUIManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.PanelManager");
        string strFrame     = LuaScriptMgr.GetLuaString(L, 2);
        obj.ShowFrame(strFrame);
        return 0;
    }

    /// <summary>
    /// 隐藏界面
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int HidePanel(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        GameUIManager obj   = (GameUIManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.PanelManager");
        string strFrame     = LuaScriptMgr.GetLuaString(L, 2);
        obj.HideFrame(strFrame );
        return 0;
    }

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0        = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1        = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

