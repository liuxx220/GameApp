using System;
using LuaInterface;

public class Framework_ByteBufferWrap
{
    public static void Register(IntPtr L)
    {
        LuaMethod[] regs = new LuaMethod[]
        {
            new LuaMethod("Close",          Close),
            new LuaMethod("WriteByte",      WriteByte),
            new LuaMethod("WriteInt",       WriteInt),
            new LuaMethod("WriteShort",     WriteShort),
            new LuaMethod("WriteFloat",     WriteFloat),
            new LuaMethod("WriteString",    WriteString),
            new LuaMethod("WriteBytes",     WriteBytes),
            new LuaMethod("ReadByte",       ReadByte),
            new LuaMethod("ReadInt",        ReadInt),
            new LuaMethod("ReadShort",      ReadShort),
            new LuaMethod("ReadFloat",      ReadFloat),
            new LuaMethod("ReadString",     ReadString),
            new LuaMethod("ToBytes",        ToBytes),
            new LuaMethod("Flush",          Flush),
            new LuaMethod("New",            _CreateFramework_ByteBuffer),
            new LuaMethod("GetClassType",   GetClassType),
        };

        LuaField[] fields = new LuaField[]
        {
        };

        LuaScriptMgr.RegisterLib(L, "Framework.ByteBuffer", typeof(StreamMemory), regs, fields, typeof(object));
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateFramework_ByteBuffer(IntPtr L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 0)
        {
            StreamMemory obj = new StreamMemory();
            LuaScriptMgr.PushObject(L, obj);
            return 1;
        }
        
        else
        {
            LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.ByteBuffer.New");
        }

        return 0;
    }

    static Type classType = typeof(StreamMemory);

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetClassType(IntPtr L)
    {
        LuaScriptMgr.Push(L, classType);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Close(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        obj.Close();
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int WriteByte(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        byte arg0 = (byte)LuaScriptMgr.GetNumber(L, 2);
        obj.WriteByte(arg0);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int WriteInt(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
        obj.WriteInt(arg0);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int WriteShort(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        ushort arg0 = (ushort)LuaScriptMgr.GetNumber(L, 2);
        obj.WriteShort(arg0);
        return 0;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int WriteFloat(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
        obj.WriteFloat(arg0);
        return 0;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int WriteString(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        string arg0 = LuaScriptMgr.GetLuaString(L, 2);
        obj.WriteString(arg0);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int WriteBytes(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 2);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        byte[] objs0 = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
        obj.WriteBytes(objs0);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ReadByte(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        byte o = obj.ReadByte();
        LuaScriptMgr.Push(L, o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ReadInt(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        int o = obj.ReadInt();
        LuaScriptMgr.Push(L, o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ReadShort(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        ushort o = obj.ReadShort();
        LuaScriptMgr.Push(L, o);
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ReadFloat(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        float o = obj.ReadFloat();
        LuaScriptMgr.Push(L, o);
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ReadString(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.StreamMemory");
        string o = obj.ReadString();
        LuaScriptMgr.Push(L, o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ToBytes(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.ByteBuffer");
        byte[] o = obj.ToBytes();
        LuaScriptMgr.PushArray(L, o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Flush(IntPtr L)
    {
        LuaScriptMgr.CheckArgsCount(L, 1);
        StreamMemory obj = (StreamMemory)LuaScriptMgr.GetNetObjectSelf(L, 1, "Framework.ByteBuffer");
        obj.Flush();
        return 0;
    }
}

