using System;




public class CProofAccountRequest : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public CProofAccountRequest()
    {
        MessageID = (int)NETPROTOCOL.CL_ProofAccount_REQUEST;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize( ref stream);
        stream.WriteString(mAccountName);
        stream.WriteString(mPassWorld);

        byte[] a = stream.ToBytes();
        int i = 0;
        i++;
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
        mAccountName = stream.ReadString();
        mPassWorld   = stream.ReadString();
    }

   /// <summary>
   /// 账号跟密码
   /// </summary>
    public string mAccountName;
    public string mPassWorld;

}

public class CProofAccountRespond : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public CProofAccountRespond()
    {
        MessageID = (int)NETPROTOCOL.LC_ProofAccount_RESPOND;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
        stream.WriteInt(errorCode);
        
        byte[] a = stream.ToBytes();
        int i = 0;
        i++;
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
        errorCode = stream.ReadInt();
    }

    /// <summary>
    /// 错误码
    /// </summary>
    public Int32    errorCode;
}

public class CLoginProtocolRespond : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public CLoginProtocolRespond()
    {
        MessageID = (int)NETPROTOCOL_RESPOND.LC_LOGINPROTOCOL_RESPOND;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}


public class GetSceneProtocolRequest : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public GetSceneProtocolRequest()
    {
        MessageID = (int)NETPROTOCOL.CL_GETSCENEPROTOCOL_REQUEST;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}


public class GetSceneProtocolRespond : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public GetSceneProtocolRespond()
    {
        MessageID = (int)NETPROTOCOL_RESPOND.LC_GETSCENEPROTOCOL_RESPOND;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}


public class EnterSceneProtocolRequest : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public EnterSceneProtocolRequest()
    {
        MessageID = (int)NETPROTOCOL.CL_ENTERSCENEPROTOCOL_REQUEST;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}


public class EnterSceneProtocolRespond : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public EnterSceneProtocolRespond()
    {
        MessageID = (int)NETPROTOCOL_RESPOND.LC_ENTERSCENEPROTOCOL_RESPOND;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}


// 心跳消息包
public class HeartBeatProtocolRequest : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public HeartBeatProtocolRequest()
    {
        MessageID = (int)NETPROTOCOL.CL_HEARTBEATPROTOCOL_REQUEST;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
        base.PackageSize = stream.Writelength();
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}

public class HeartBeatProtocolRespond : PackageHeader
{
    /// <summary>
    /// 
    /// </summary>
    public HeartBeatProtocolRespond()
    {
        MessageID = (int)NETPROTOCOL_RESPOND.LC_HEARTBEATPROTOCOL_RESPOND;
    }


    public override void serialize(ref StreamMemory stream)
    {
        base.serialize(ref stream);
        base.PackageSize = stream.Writelength();
    }

    public override void unserialize(ref StreamMemory stream)
    {
        base.unserialize(ref stream);
    }
}