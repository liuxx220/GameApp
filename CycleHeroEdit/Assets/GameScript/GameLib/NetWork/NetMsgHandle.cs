/*
 * ------------------------------------------------------------------------------------------
 *          file name : 
 *          desc      : 注册网络消息，等网络游戏返回调用相应的接口
 *                      客户端与服务端交互基于消息通讯， 任何一个行为一条指令都是以一个消息包来描述
 *          auther    : LJP
 * ------------------------------------------------------------------------------------------
*/
using UnityEngine;
using System.Collections;
using System;
using ProtoBuf;
using System.Collections.Generic;




class CNetMsgHandle
{
    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 网络消息的处理
    /// </summary>
    ///-----------------------------------------------------------------------------
    public void HandleNetMsg(StreamMemory stream, int nProtocalID)
    {
        NETPROTOCOL eProtoID = (NETPROTOCOL)nProtocalID;
        switch( eProtoID )
        {
            case NETPROTOCOL.LC_ProofAccount_RESPOND:
                HandleLoginRespond(stream);
                break;

            //case NETPROTOCOL_RESPOND.LC_GETSCENEPROTOCOL_RESPOND:
            //    HandleGetSceneInfoRespond(null);
            //    break;

            //case NETPROTOCOL_RESPOND.LC_ENTERSCENEPROTOCOL_RESPOND:
            //    HandleEnterSceneRespond(null);
            //    break;

            default:
                break;
        }
    }


    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 封装 proto 解析
    /// </summary>
    ///-----------------------------------------------------------------------------
    private bool ProctoToNetMsg<T>( out T pMsg, System.IO.Stream stream )
    {

        try
        {
            pMsg = ProtoBuf.Serializer.Deserialize<T>(stream);
            if (pMsg == null)
            {
                pMsg = default(T);
                return false;
            }
            return true;
        }

        catch( Exception )
        {
            pMsg = default(T);
            return true;
        }
       
    }


    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 登录到Login服务器，Login server 返回
    /// </summary>
    ///-----------------------------------------------------------------------------
    private void HandleLoginRespond( StreamMemory pNetStream)
    {
        CProofAccountRespond pMsg = new CProofAccountRespond();
        pMsg.unserialize( ref pNetStream );
        if (pMsg.PackageSize != pNetStream.Readlength())
        {
            Debugger.LogError(" paser net message is error!!");
            return;
        }
        pMsg = null;
    }

    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 返回要登录到的 Scene server 的信息
    /// </summary>
    ///-----------------------------------------------------------------------------
    private void HandleGetSceneInfoRespond(StreamMemory pNetStream)
    {
        GetSceneProtocolRespond pMsg = new GetSceneProtocolRespond();
        pMsg.unserialize( ref pNetStream );
        if( pMsg.PackageSize != pNetStream.Readlength() )
        {
            Debugger.LogError(" paser net message is error!!");
            return;
        }
        pMsg = null;
    }

    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 进入 Scene server, 请求返回
    /// </summary>
    ///-----------------------------------------------------------------------------
    private void HandleEnterSceneRespond(StreamMemory pNetStream)
    {
        EnterSceneProtocolRespond pMsg = new EnterSceneProtocolRespond();
        pMsg.unserialize(ref pNetStream);
        if (pMsg.PackageSize != pNetStream.Readlength())
        {
            Debugger.LogError(" paser net message is error!!");
            return;
        }
        pMsg = null;
    }
}
