using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;
using Object = UnityEngine.Object;

public class UITextureWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
		
			new LuaMethod("New",                _CreateUIButton),
			new LuaMethod("GetClassType",       GetClassType),
			new LuaMethod("__eq",               Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("mainTexture",         get_mainTexture, set_mainTexture),
		};

        LuaScriptMgr.RegisterLib(L, "UITexture", typeof(UITextureWrap), regs, fields, typeof(UIBasicSprite));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIButton(IntPtr L)
	{
        LuaDLL.luaL_error(L, "UITexture class does not have a constructor function");
		return 0;
	}

    static Type classType = typeof(UITexture);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_mainTexture(IntPtr L)
	{
        object obj      = LuaScriptMgr.GetLuaObject(L, 1);
        UITexture uiTex = (UITexture)obj;
        if( uiTex == null )
        {
            LuaTypes types = LuaDLL.lua_type(L, 1);
            if (types == LuaTypes.LUA_TTABLE)
            {
                LuaDLL.luaL_error(L, "unknown member name mainTexture");
            }
            else
            {
                LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
            }
        }
        LuaScriptMgr.Push(L, uiTex.mainTexture);
		return 1;
	}

	
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_mainTexture(IntPtr L)
	{
        object obj      = LuaScriptMgr.GetLuaObject(L, 1);
        UITexture uiTex = (UITexture)obj;
        if( obj == null )
        {
            LuaTypes types = LuaDLL.lua_type(L, 1);
            if (types == LuaTypes.LUA_TTABLE)
            {
                LuaDLL.luaL_error(L, "unknown member name mainTexture");
            }
            else
            {
                LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
            }
        }

        uiTex.mainTexture = (Texture)LuaScriptMgr.GetUnityObject(L, 3, typeof(Texture));
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

