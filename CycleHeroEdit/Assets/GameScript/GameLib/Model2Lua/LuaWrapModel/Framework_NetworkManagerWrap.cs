using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class Framework_NetworkManagerWrap
{
    public static void Register(IntPtr L)
    {
        LuaMethod[] regs = new LuaMethod[]
        {
            new LuaMethod("Unload",             Unload),
            new LuaMethod("SendConnect",        SendConnect),
            new LuaMethod("SendMessage",        SendMessage),
            new LuaMethod("New",                _Create_NetworkManager),
            new LuaMethod("GetClassType",       GetClassType),
            new LuaMethod("__eq",               Lua_Eq),
        };

        LuaField[] fields = new LuaField[]
        {
        };

        LuaScriptMgr.RegisterLib(L, "Framework.NetworkManager", typeof(NetworkManager), regs, fields, typeof(View));
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _Create_NetworkManager(IntPtr L)
    {
        LuaDLL.luaL_error(L, "SimpleFramework.Manager.NetworkManager class does not have a constructor function");
        return 0;
    }

    static Type classType = typeof(NetworkManager);

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetClassType(IntPtr L)
    {
        LuaScriptMgr.Push(L, classType);
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Unload(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        NetworkManager obj = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.NetworkManager");
        obj.Close();
        return 0;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SendConnect(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 3);
        NetworkManager obj  = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.NetworkManager");
        obj.mAccount        = LuaScriptMgr.GetLuaString(L, 2);
        obj.mPassworld      = LuaScriptMgr.GetLuaString(L, 3);
        obj.TryConnect();
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SendMessage(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        NetworkManager obj = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.NetworkManager");
        StreamMemory arg0  = (StreamMemory)LuaScriptMgr.GetNetObject(L, 2, typeof(StreamMemory));
        obj.SendMessage(arg0, arg0.Writelength());
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Eq(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
        Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
        bool o = arg0 == arg1;
        LuaScriptMgr.Push(L, o);
        return 1;
    }
}

