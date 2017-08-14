using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class Framework_BundleManagerWrap
{
    public static void Register(IntPtr L)
    {
        LuaMethod[] regs = new LuaMethod[]
        {
            new LuaMethod("CreateObjByBundle",     CreateObjByBundle),
            new LuaMethod("New",                   _CreateFramework_BundleManager),
            new LuaMethod("GetClassType",           GetClassType),
            new LuaMethod("__eq",                  Lua_Eq),
        };

        LuaField[] fields = new LuaField[]
        {

        };

        LuaScriptMgr.RegisterLib(L, "Framework.CBundleManager", typeof(CBundleManager), regs, fields, typeof(View));
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateFramework_BundleManager(IntPtr L)
    {
        LuaDLL.luaL_error(L, "Framework.CBundleManager class does not have a constructor function");
        return 0;
    }

    static Type classType = typeof(CBundleManager);

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetClassType(IntPtr L)
    {
        LuaScriptMgr.Push(L, classType);
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int CreateObjByBundle(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 3);
        CBundleManager obj      = (CBundleManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Framework.BundleManager");
        string arg0             = LuaScriptMgr.GetLuaString(L, 2);
        string arg1             = LuaScriptMgr.GetLuaString(L, 3);
        GameObject o            = obj.CreateObjByBundle(arg0, int.Parse(arg1));
        LuaScriptMgr.Push(L, o);
        return 1;
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

