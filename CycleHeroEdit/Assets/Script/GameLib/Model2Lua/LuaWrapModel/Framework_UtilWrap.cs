
/*
 * ---------------------------------------------------------------------------------------------------------
 *      file name : Framework_UtilWrap.cs
 *      desc      : 注册工具类的接口到 ulua
 *      author    : by ljp
 *      
 *      log       : [ 2016-4-17 ]
 * ---------------------------------------------------------------------------------------------------------
*/
using System;
using UnityEngine;
using LuaInterface;

public class Framework_UtilWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Int",            Int),
			new LuaMethod("Float",          Float),
			new LuaMethod("Long",           Long),
			new LuaMethod("Random",         Random),
			new LuaMethod("Uid",            Uid),
			new LuaMethod("GetTime",        GetTime),
			new LuaMethod("Child",          Child),
			new LuaMethod("Peer",           Peer),
			new LuaMethod("Vibrate",        Vibrate),
			new LuaMethod("Encode",         Encode),
			new LuaMethod("Decode",         Decode),
			new LuaMethod("IsNumeric",      IsNumeric),
			new LuaMethod("HashToMD5Hex",   HashToMD5Hex),
			new LuaMethod("md5",            md5),
			new LuaMethod("md5file",        md5file),
			new LuaMethod("ClearChild",     ClearChild),
			new LuaMethod("GetKey",         GetKey),
			new LuaMethod("GetInt",         GetInt),
			new LuaMethod("HasKey",         HasKey),
			new LuaMethod("SetInt",         SetInt),
			new LuaMethod("GetString",      GetString),
			new LuaMethod("SetString",      SetString),
			new LuaMethod("RemoveData",     RemoveData),
			new LuaMethod("ClearMemory",    ClearMemory),
			new LuaMethod("IsNumber",       IsNumber),
			new LuaMethod("GetFileText",    GetFileText),
			new LuaMethod("AppContentPath", AppContentPath),
			new LuaMethod("AddClick",       AddClick),
			new LuaMethod("LuaPath",        LuaPath),
			new LuaMethod("SearchLuaPath",  SearchLuaPath),
			new LuaMethod("AddLuaPath",     AddLuaPath),
			new LuaMethod("RemoveLuaPath",  RemoveLuaPath),
			new LuaMethod("Log",            Log),
			new LuaMethod("LogWarning",     LogWarning),
			new LuaMethod("LogError",       LogError),
			new LuaMethod("LoadAsset",      LoadAsset),
			new LuaMethod("AddComponent",   AddComponent),
			new LuaMethod("LoadPrefab",     LoadPrefab),
			new LuaMethod("CallMethod",     CallMethod),
			new LuaMethod("CheckEnvironment", CheckEnvironment),
			new LuaMethod("New",            _CreateFramework_Util),
			new LuaMethod("GetClassType",   GetClassType),
            new LuaMethod("RegisterEvent",  RegisterClickEvent ),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("DataPath",        get_DataPath, null),
			new LuaField("NetAvailable",    get_NetAvailable, null),
			new LuaField("IsWifi",          get_IsWifi, null),
			new LuaField("isLogin",         get_isLogin, null),
			new LuaField("isMain",          get_isMain, null),
			new LuaField("isFight",         get_isFight, null),
			new LuaField("isApplePlatform", get_isApplePlatform, null),
           
		};

        LuaScriptMgr.RegisterLib(L, "Framework.Util", typeof(LuaFramework.Util), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateFramework_Util(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
            LuaFramework.Util obj = new LuaFramework.Util();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
            LuaDLL.luaL_error(L, "invalid arguments to method: LuaFramework.Util.New");
		}

		return 0;
	}

    static Type classType = typeof(LuaFramework.Util);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DataPath(IntPtr L)
	{
        LuaScriptMgr.Push(L, LuaFramework.Util.DataPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NetAvailable(IntPtr L)
	{
        LuaScriptMgr.Push(L, LuaFramework.Util.NetAvailable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsWifi(IntPtr L)
	{
        LuaScriptMgr.Push(L, LuaFramework.Util.IsWifi);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isLogin(IntPtr L)
	{
        LuaScriptMgr.Push(L, LuaFramework.Util.isLogin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isMain(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaFramework.Util.isMain);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaFramework.Util.isFight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isApplePlatform(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaFramework.Util.isApplePlatform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Int(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object arg0 = LuaScriptMgr.GetVarObject(L, 1);
		int o = LuaFramework.Util.Int(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Float(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object arg0 = LuaScriptMgr.GetVarObject(L, 1);
		float o = LuaFramework.Util.Float(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Long(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object arg0 = LuaScriptMgr.GetVarObject(L, 1);
		long o = LuaFramework.Util.Long(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Random(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(float), typeof(float)))
		{
			float arg0 = (float)LuaDLL.lua_tonumber(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float o = LuaFramework.Util.Random(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(int)))
		{
			int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
			int o = LuaFramework.Util.Random(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LuaFramework.Util.Random");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Uid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.Uid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		long o = LuaFramework.Util.GetTime();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Child(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(string)))
		{
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 1);
			string arg1 = LuaScriptMgr.GetString(L, 2);
			GameObject o = LuaFramework.Util.Child(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
		{
			GameObject arg0 = (GameObject)LuaScriptMgr.GetLuaObject(L, 1);
			string arg1 = LuaScriptMgr.GetString(L, 2);
			GameObject o = LuaFramework.Util.Child(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LuaFramework.Util.Child");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Peer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(string)))
		{
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 1);
			string arg1 = LuaScriptMgr.GetString(L, 2);
			GameObject o = LuaFramework.Util.Peer(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
		{
			GameObject arg0 = (GameObject)LuaScriptMgr.GetLuaObject(L, 1);
			string arg1 = LuaScriptMgr.GetString(L, 2);
			GameObject o = LuaFramework.Util.Peer(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LuaFramework.Util.Peer");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Vibrate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		LuaFramework.Util.Vibrate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Encode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.Encode(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Decode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.Decode(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsNumeric(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = LuaFramework.Util.IsNumeric(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HashToMD5Hex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.HashToMD5Hex(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int md5(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.md5(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int md5file(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.md5file(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		LuaFramework.Util.ClearChild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetKey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.GetKey(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int o = LuaFramework.Util.GetInt(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasKey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = LuaFramework.Util.HasKey(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		LuaFramework.Util.SetInt(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.GetString(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		LuaFramework.Util.SetString(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFramework.Util.RemoveData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearMemory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		LuaFramework.Util.ClearMemory();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsNumber(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = LuaFramework.Util.IsNumber(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFileText(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.GetFileText(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AppContentPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = LuaFramework.Util.AppContentPath();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		object arg1 = LuaScriptMgr.GetVarObject(L, 2);
		LuaFramework.Util.AddClick(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.LuaPath(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SearchLuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = LuaFramework.Util.SearchLuaPath(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddLuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFramework.Util.AddLuaPath(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveLuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFramework.Util.RemoveLuaPath(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Log(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFramework.Util.Log(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogWarning(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFramework.Util.LogWarning(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogError(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFramework.Util.LogError(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AssetBundle arg0 = (AssetBundle)LuaScriptMgr.GetUnityObject(L, 1, typeof(AssetBundle));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		GameObject o = LuaFramework.Util.LoadAsset(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddComponent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameObject arg0     = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		string arg1         = LuaScriptMgr.GetLuaString(L, 2);
		string arg2         = LuaScriptMgr.GetLuaString(L, 3);
		Component o         = LuaFramework.Util.AddComponent(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0         = LuaScriptMgr.GetLuaString(L, 1);
		GameObject o        = LuaFramework.Util.LoadPrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CallMethod(IntPtr L)
	{
		int count           = LuaDLL.lua_gettop(L);
		string arg0         = LuaScriptMgr.GetLuaString(L, 1);
		string arg1         = LuaScriptMgr.GetLuaString(L, 2);
		object[] objs2      = LuaScriptMgr.GetParamsObject(L, 3, count - 2);
		object[] o          = LuaFramework.Util.CallMethod(arg0,arg1,objs2);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckEnvironment(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		bool o              = LuaFramework.Util.CheckEnvironment();
		LuaScriptMgr.Push(L, o);
		return 1;
	}


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int RegisterClickEvent(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        GameObject go       = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
        LuaFunction luafunc = LuaScriptMgr.GetLuaFunction(L, 2);
       
        if (go == null) return 0;
        UIEventListener.Get(go).onClick = delegate(GameObject o)
        {
            luafunc.Call(go);
        };
        return 1;
    }
}

