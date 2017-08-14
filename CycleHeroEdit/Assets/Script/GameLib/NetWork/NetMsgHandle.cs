/*
 * ------------------------------------------------------------------------------------------
 *          file name : 
 *          desc      : ע��������Ϣ����������Ϸ���ص�����Ӧ�Ľӿ�
 *                      �ͻ��������˽���������ϢͨѶ�� �κ�һ����Ϊһ��ָ�����һ����Ϣ��������
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
    /// ������Ϣ�Ĵ���
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
    /// ��װ proto ����
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
    /// ��¼��Login��������Login server ����
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
    /// ����Ҫ��¼���� Scene server ����Ϣ
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
    /// ���� Scene server, ���󷵻�
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
