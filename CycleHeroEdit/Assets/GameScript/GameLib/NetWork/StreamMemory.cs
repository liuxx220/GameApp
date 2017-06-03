/*
 * ------------------------------------------------------------------------------
 *          file    : StreamMemory.cs
 *          desc    : 二进制数据流模块
 *                    能够将一些基本类型序列化(writeXXX)成二进制流同时也提供了反序列化(readXXX)等操作
 *          author  : ljp 
 *          
 *          Log     : 2015-11-07
 * ------------------------------------------------------------------------------
*/
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Text;

	



public class StreamMemory
{

    int rpos = 0;
    int wpos = 0;
    MemoryStream stream = null;


    public StreamMemory() {
        rpos = 0;
        wpos = 0;
        stream = new MemoryStream(512);
    }

    public StreamMemory(byte[] data)
    {
        stream = new MemoryStream(data.Length);
        stream.Write(data, 0, data.Length);
        wpos = data.Length;
    }

    public int Writelength()
    {
        return wpos;
    }

    public int Readlength()
    {
        return rpos;
    }

    public void Close()
    {
        stream.Close();
        stream  = null;
        rpos    = 0;
        wpos    = 0;
    }

    public void WriteByte(byte v)
    {
        byte[] arrBytes = BitConverter.GetBytes(v);
        stream.Write(arrBytes, 0, arrBytes.Length);
        wpos += arrBytes.Length;
    }

    public void WriteInt(int v)
    {
        byte[] arrBytes = BitConverter.GetBytes(v);
        stream.Write(arrBytes, 0, arrBytes.Length);
        wpos += arrBytes.Length;
    }

    public void WriteShort(ushort v)
    {
        byte[] arrBytes = BitConverter.GetBytes(v);
        stream.Write(arrBytes, 0, arrBytes.Length);
        wpos += arrBytes.Length;
    }

    public void WriteLong(long v)
    {
        byte[] arrBytes = BitConverter.GetBytes(v);
        stream.Write(arrBytes, 0, arrBytes.Length);
        wpos += arrBytes.Length;
    }

    public void WriteFloat(float v)
    {
        byte[] arrBytes = BitConverter.GetBytes(v);
        stream.Write(arrBytes, 0, arrBytes.Length);
        wpos += arrBytes.Length;
    }

    public void WriteString(string v)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(v);
        WriteBytes(bytes);
        stream.WriteByte(0);
        wpos++;
    }

    public void WriteBytes(byte[] arrBytes)
    {
        stream.Write(arrBytes, 0, arrBytes.Length);
        wpos += arrBytes.Length;
    }

    public void WriteBytes(byte[] arrBytes, Int32 len )
    {
        stream.Write(arrBytes, 0, len );
        wpos += len;
    }

    public void WriteBuffer(LuaStringBuffer strBuffer)
    {
        WriteBytes(strBuffer.buffer);
    }

    public byte ReadByte()
    {
        stream.Seek(rpos, SeekOrigin.Begin);
        byte value = Convert.ToByte( stream.ReadByte() );
        rpos++;
        return value;
    }

    public int ReadInt()
    {
        int value = BitConverter.ToInt32(stream.GetBuffer(), rpos);
        rpos += sizeof(int);
        return value;
    }

    public UInt16 ReadShort()
    {
        UInt16 value = BitConverter.ToUInt16(stream.GetBuffer(), rpos);
        rpos += sizeof(UInt16);
        return value;
    }

    public float ReadFloat()
    {
        float value = BitConverter.ToSingle(stream.GetBuffer(), rpos);
        rpos += sizeof(float);
        return value;
    }

    public double ReadDouble()
    {
        double value = BitConverter.ToDouble(stream.GetBuffer(), rpos);
        rpos += sizeof(double);
        return value;
    }

    public string ReadString()
    {
        ushort len    = ReadShort();
        byte[] buffer = new byte[len];
        ReadBytes(buffer, len);
        return Encoding.UTF8.GetString(buffer);
    }

    public bool ReadBytes(byte[] arrdata, ushort len )
    {
        stream.Seek(rpos, SeekOrigin.Begin);
        stream.Read(arrdata, 0, rpos);
        rpos += len;
        return true;
    }

    //public LuaStringBuffer ReadBuffer()
    //{
    //    byte[] bytes = ReadBytes();
    //    return new LuaStringBuffer(bytes);
    //}

    public byte[] ToBytes()
    {
        return stream.GetBuffer();
    }

    public void Flush()
    {
        stream.Flush();
        rpos = 0;
        wpos = 0;
    }
}
    

