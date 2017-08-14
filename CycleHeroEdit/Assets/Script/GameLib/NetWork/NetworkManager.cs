using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections; 
using System.Collections.Generic;
using System.Threading;





public class NetworkManager : View
{

    public string               mAccount;
    public string               mPassworld;
    /// <summary>
    /// 连接服务器的类型枚举
    /// </summary>
    public enum ServerType
    {
        LoginServer     = 0,
        SceneServer,
        GateServer
    }
    
    public ClientAppArgs        _args               = new ClientAppArgs();
    private TcpClient           mClient = null;
    private TcpClient           mConnecting = null;


    // 异步事件
    IAsyncResult                mRecvResult;
    IAsyncResult                mConnectResult;

    // 发武器消息返回处理对象
    private CNetMsgHandle       mNetMsgHandle       = new CNetMsgHandle();

    // 发送数据stream
    public StreamMemory         mSendBuffer         = new StreamMemory();
   
    // 接受数据 stream
    public List<int>            mReceiveMsgIDs      = new List<int>();
    public List<StreamMemory>   mReceiveStreams     = new List<StreamMemory>();
    public List<StreamMemory>   mReceiveStreamsPool = new List<StreamMemory>();


    public const int TCP_PACKET_MAX = 1460;
    public Int32  m_RecvPos                         = 0;
    public byte[] m_RecvBuffer                      = new byte[2 * 1024 * 1024];

    private bool Isconnected = false;


    public static NetworkManager Instance = null;
    public static NetworkManager Get()
    {
        if (Instance == null)
        {
            Instance = ClientApp.Instance.GetManager<NetworkManager>(ManagerName.Network);
        }
        return Instance;
    }
    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 初始化网络层配置和对象
    /// </summary>
    ///-----------------------------------------------------------------------------
    public void InitNetwork( ClientAppArgs args )
	{
        _args = args;
        for( int i = 0; i < 50; ++i )
        {
            mReceiveStreamsPool.Add(new StreamMemory());
        }
	}

    public void Close()
    {
        if (mClient != null)
        {
            OnClosed(mClient, null);
        }
    }

    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 试着链接服务器
    /// </summary>
    ///-----------------------------------------------------------------------------
    public void TryConnect( )
    {
        if( mClient != null )
        {
            throw new Exception("fuck, The socket is connecting, cannot connect again!");
        }

        if( mConnecting != null )
        {
            throw new Exception("fuck, The socket is connecting, cannot connect again!");
        }

        IPAddress ipAddress = IPAddress.Parse(_args.ip);
        try
        {
            mConnecting     = new TcpClient();
            mConnectResult = mConnecting.BeginConnect(_args.ip, _args.port, null, null );
            
        }

        catch( Exception ex )
        {
            mClient     = mConnecting;
            mConnecting = null;
            OnConnectError(mClient, null);
        }
    }

    /// <summary>
    /// 连接Login app 回调函数
    /// </summary>
    private void OnProofAccountLoginApp( )
    {
        try
        {
            // 链接到服务器返回处理
            CProofAccountRequest Request = new CProofAccountRequest();
            Request.mAccountName = mAccount;
            Request.mPassWorld   = mPassworld;
            SendMessage(Request);
        }
        catch (Exception e)
        {

        }
    }


    /// <summary>
    /// 连接Game app 回调函数
    /// </summary>
    private static void OnConnectGameApp(string ip, int port, bool success, object user)
    {
        //try
        //{
        //   // 链接到服务器返回处理
        //}
        //catch (Exception e)
        //{

        //}
    }
    ///-----------------------------------------------------------------------------
    /// <summary>
    /// 销毁网络对象
    /// </summary>
    ///-----------------------------------------------------------------------------
	public void Destroy( )
	{
       
	}


    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 发送心跳包
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void OnClosed( object sender, EventArgs e )
    {
        try
        {
            mClient.Client.Shutdown(SocketShutdown.Both);
            mClient.GetStream().Close();
            mClient.Close();
            mClient = null;
        }

        catch( Exception ex )
        {
            Debugger.Log(ex.ToString());
        }

        mRecvResult = null;
        mClient     = null;
        m_RecvPos   = 0;
        mReceiveStreams.Clear();
        mReceiveMsgIDs.Clear();
    }

    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 发送心跳包
    /// </summary>
    /// ------------------------------------------------------------------------------
    HeartBeatProtocolRequest HeartBeatPackage = new HeartBeatProtocolRequest();
    StreamMemory HeartBeatStream = new StreamMemory();
    private void SendHeartbeatPackage()
    {
        
        try
        {
            HeartBeatStream.Flush();
            HeartBeatPackage.serialize(ref HeartBeatStream);
            SendMessage(HeartBeatStream, HeartBeatStream.Writelength() );
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("SendHeartbeatPackage net msg exception is" + e.ToString());
        }
        
    }


    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 连接成功后回调函数
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void OnConnected( object sender, EventArgs args )
    {
        Isconnected = true;
        OnProofAccountLoginApp();
    }

    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 连接发生错误回调
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void OnConnectError( object sender, EventArgs args )
    {
        Debugger.Log("OnConnectError begin");
        try
        {
            mClient.Close();
            mClient = null;
            Isconnected = false;
        }
        catch( Exception exp )
        {
            Debugger.Log(exp.ToString());
        }

        mRecvResult = null;
        mClient     = null;
        m_RecvPos   = 0;
             
    }


    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 收到网络消息回调
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void OnDataReceived( object sender, EventArgs args )
    {
        int nCurPos = 0;
        while( m_RecvPos - nCurPos > 8 )
        {
            int nMsg = BitConverter.ToInt32(m_RecvBuffer, nCurPos);
            int nLen = BitConverter.ToInt32(m_RecvBuffer, nCurPos + 4);

            if( nLen > m_RecvBuffer.Length )
            {
                Debugger.LogError("can't pause message" + "type=" + nMsg + "len=" + nLen);
                break;
            }

            if( nLen > m_RecvPos - nCurPos )
            {
                break;
            }

            StreamMemory stream = null;
            if (mReceiveStreamsPool.Count > 0)
            {
                stream = mReceiveStreamsPool[0];
                stream.Flush();
                mReceiveStreamsPool.RemoveAt(0);
            }
            else
            {
                stream = new StreamMemory();
            }

            stream.WriteBytes(m_RecvBuffer, nLen);
            nCurPos        += nLen;
            mReceiveMsgIDs.Add(nMsg);
            mReceiveStreams.Add(stream);
        }
        
        if( nCurPos > 0 )
        {
            m_RecvPos = m_RecvPos - nCurPos;
            if (m_RecvPos > 0)
            {
                Buffer.BlockCopy(m_RecvBuffer, nCurPos, m_RecvBuffer, 0, m_RecvPos);
            }
        }
    }


    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 发送消息
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void SendMessage(PackageHeader pMsg )
    {
        if( mClient != null )
        {
            mSendBuffer.Flush();

            // 序列消息到 stream 中
            pMsg.serialize(ref mSendBuffer);
     
            // 序列到网络中
            mClient.GetStream().Write(mSendBuffer.ToBytes(), 0, mSendBuffer.Writelength() );
            mClient.GetStream().Flush();
        }
    }


    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 发送消息
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void SendMessage( StreamMemory pMsg, Int32 nSize )
    {
        if (mClient != null)
        {
            mClient.GetStream().Write(pMsg.ToBytes(), 0, pMsg.Writelength());
        }
    }
    /// ------------------------------------------------------------------------------
    /// <summary>
    /// 网络接受消息心跳函数
    /// </summary>
    /// ------------------------------------------------------------------------------
    public void Update( )
    {
        if (mClient != null)
        {
            while (mReceiveMsgIDs.Count > 0 && mReceiveStreams.Count > 0)
            {
                int nType = mReceiveMsgIDs[0];
                StreamMemory iostream = mReceiveStreams[0];

                mReceiveMsgIDs.RemoveAt(0);
                mReceiveStreams.RemoveAt(0);

                mNetMsgHandle.HandleNetMsg(iostream, nType);
                iostream = null;
            }

            if (mRecvResult != null)
            {
                if (mRecvResult.IsCompleted)
                {
                    try
                    {
                        Int32 nRecvBytesRead = mClient.GetStream().EndRead(mRecvResult);
                        m_RecvPos += nRecvBytesRead;
                    }

                    catch (Exception ex)
                    {
                        Debugger.LogError(ex.ToString());
                        return;
                    }

                    OnDataReceived(null, null);

                    try
                    {
                        mRecvResult = mClient.GetStream().BeginRead(m_RecvBuffer, m_RecvPos, m_RecvBuffer.Length - m_RecvPos, null, null);
                    }
                    catch (Exception exc)
                    {
                        Debugger.LogError(exc.ToString());
                        return;
                    }
                }
            }
            if (mClient != null && mClient.Connected == false)
            {
                Debugger.LogError("client is close by system, so close it now.");
                return;
            }
        }

        else if( mConnecting != null )
        {
            if( mConnectResult.IsCompleted )
            {
                mClient         = mConnecting;
                mConnecting     = null;
                mConnectResult  = null;
                if( mClient.Connected )
                {
                    // 设置socket 的属性
                    try
                    {

                        //mClient.NoDelay = true;
                        mClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 2000);
                        mClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000);
                        mClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                        mRecvResult = mClient.GetStream().BeginRead(m_RecvBuffer, 0, m_RecvBuffer.Length, null, null);
                        OnConnected(mClient, null);
                    }
                    catch (Exception exc)
                    {
                        Debugger.LogError(exc.ToString());
                        return;
                    }
                }
                else
                {
                    OnConnectError(mClient, null);
                }
            }
        }

        // 心跳更新
        //if (Isconnected )
        //    UpdateHeartBeatLogic();
    }


    private float HeartBeatelapse = 0f;
    private void UpdateHeartBeatLogic( )
    {
        HeartBeatelapse += Time.deltaTime;
        if( HeartBeatelapse >= 5 )
        {
            HeartBeatelapse = 0;
            SendHeartbeatPackage();
        }
    }
}

