/*
 * ----------------------------------------------------------------------
 *          消息包头
 * 
 * 
 * ----------------------------------------------------------------------
*/
using System;
using System.IO;




public class PackageHeader 
{

    /// <summary>
    /// 接受消息包的ID
    /// </summary>
    public int MessageID
    {
        get;
        set;
    }

    /// <summary>
    /// 消息包的大小
    /// </summary>
    public int PackageSize
    {
        get;
        set;
    }
    public virtual void serialize( ref StreamMemory stream )
    {
        stream.WriteInt( MessageID );
        stream.WriteInt(PackageSize);
    }

    public virtual void unserialize(ref StreamMemory stream)
    {
        MessageID       = stream.ReadInt();
        PackageSize     = stream.ReadInt();
    }
}

