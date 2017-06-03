using System;
using UnityEngine;
using LuaInterface;

public class iTweenWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New",                _CreateTweenWrap),
			new LuaMethod("GetClassType",       GetClassType),
            new LuaMethod("MoveTo",             _MoveTo),
            new LuaMethod("RotateTo",           _RotateTo),
		};

		LuaField[] fields = new LuaField[]
		{
			
		};

        LuaScriptMgr.RegisterLib(L, "Framework.iTween", typeof(iTween), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateTweenWrap(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 0)
		{
            iTween obj = new iTween();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
            LuaDLL.luaL_error(L, "invalid arguments to method: iTween.New");
		}

		return 0;
	}

    static Type classType = typeof(iTween);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _MoveTo(IntPtr L)
	{
        Vector3 pos     = Vector3.zero;
        GameObject obj  = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
        pos             = LuaScriptMgr.GetVector3(L, 2);
        float arg0      = (float)LuaDLL.lua_tonumber(L,3);
        
        iTween.MoveTo( obj, pos, arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _RotateTo(IntPtr L)
	{
       // 
        Vector3 Rot     = Vector3.zero;
        GameObject obj  = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
        Rot             = LuaScriptMgr.GetVector3(L, 2);
        float      arg0 = (float)LuaDLL.lua_tonumber(L, 3);

        iTween.RotateTo(obj, Rot, arg0);
		return 0;
	}
}

