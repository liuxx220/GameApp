using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class Framework_EventCenter
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("RegisterListenEvent",          RegisterListenEvent),
            new LuaMethod("UnRegisterListenEvent",        UnRegisterListenEvent),
            new LuaMethod("BroadcastEventONE",            BroadcastEvent),
			new LuaMethod("New",                          _CreateFramework_EventCenter),
			new LuaMethod("GetClassType",                 GetClassType),   
			new LuaMethod("__eq",                         Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{

		};

        LuaScriptMgr.RegisterLib(L, "Framework.EventCenter", typeof(CEventCenterMgr), regs, fields, typeof(View));
	}


	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateFramework_EventCenter(IntPtr L)
	{
        LuaDLL.luaL_error(L, "Framework.LevelManager class does not have a constructor function");
		return 0;
	}


    static Type classType = typeof(CEventCenterMgr);
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

    /// <summary>
    /// 
    /// </summary>
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int RegisterListenEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
        CEventCenterMgr obj     = (CEventCenterMgr)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.EventCenter");
        EGameEvent eventid      = (EGameEvent)(int.Parse(LuaScriptMgr.GetLuaString(L, 2)));
        LuaFunction func        = LuaScriptMgr.GetLuaFunction(L, 3);
        obj.AddListener(eventid, func );

		return 0;
	}


    /// <summary>
    /// 
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int UnRegisterListenEvent(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        CEventCenterMgr obj     = (CEventCenterMgr)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.EventCenter");
        EGameEvent eventid      = (EGameEvent)(int.Parse(LuaScriptMgr.GetLuaString(L, 2)));
        obj.RemoveListener( eventid );
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int BroadcastEvent(IntPtr L)
    {
        int count = LuaDLL.lua_gettop(L);
        if (count > 2 )
        {
            CEventCenterMgr obj     = (CEventCenterMgr)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.EventCenter");
            EGameEvent eventid      = (EGameEvent)(int.Parse(LuaScriptMgr.GetLuaString(L, 2)));
            CEvent PARAM            = new CEvent(eventid);

            obj.FireEvent(eventid, PARAM );
        }
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

