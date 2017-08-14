using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using MessageID = System.UInt16;
using MessageLength = System.UInt16;




public class ClientApp : Facade
{

	public static ClientApp         intstance = null;
    private ClientAppArgs           _args = null;


    public static ClientApp Instance
    {
        get
        {
            return intstance;
        }
    }

    // 客户端的类别
	public enum CLIENT_TYPE
	{
		// Mobile(Phone, Pad)
		CLIENT_TYPE_MOBILE				= 1,

		// Windows/Linux/Mac Application program
		// Contains the Python-scripts, entitydefs parsing and check entitydefs-MD5, Native
		CLIENT_TYPE_PC					= 2,

		// bots(Contains the Python-scripts, entitydefs parsing and check entitydefs-MD5, Native)
		CLIENT_TYPE_BOTS				= 3,

        // Mobile ( Android )
        CLIENT_TYPE_ANDROID             = 4,
	};


    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GAMESTATUS
    {
        GMS_STATRGAME                   = 0,
        GMS_LOGIN                       = 1,
        GMS_PLAYGAME                    = 2,
    }
		
    public string username              = "kbengine";
    public string password              = "123456";
        
	
	// 服务端与客户端的版本号以及协议MD5
    public string currserver            = "";
    public string serverVersion         = "0.4.0";
	public string clientVersion         = "0.4.0";
		
		
		
	// 描述服务端返回的错误信息
	public struct ServerErr
	{
		public string name;
		public string descr;
		public UInt16 id;
	}
    GAMESTATUS GamePlay = GAMESTATUS.GMS_STATRGAME;

	
    public ClientApp(ClientAppArgs args)
    {
        if (intstance != null)
			throw new Exception("Only one instance of KBEngineApp!");

        intstance   = this;
        _args       = args;
        InitFramework();
    }

    /// -----------------------------------------------------------------------------------
    /// <summary>
    /// 启动框架
    /// </summary>
    /// -----------------------------------------------------------------------------------
    override protected void InitFramework()
    {
        base.InitFramework();
        

        if (!LuaFramework.Util.CheckEnvironment()) return;

        //-----------------初始化管理器-----------------------
        AddManager(ManagerName.Lua, new LuaScriptMgr());
        AddManager<CProtoManager>   (ManagerName.Proto);
        AddManager<GameUIManager>   (ManagerName.Panel);
        AddManager<CLoadLevelMgr>   (ManagerName.LoadLevelMgr);
        AddManager<CEventCenterMgr> (ManagerName.EventCenterMgr);
        AddManager<CLoadLevelMgr>   (ManagerName.LoadLevelMgr);
        AddManager<CBundleManager>  (ManagerName.Resource);
        AddManager<NetworkManager>  (ManagerName.Network);
        Debug.Log("Framework StartUp-------->>>>>");

        //-----------------初始化管理器-----------------------
        NetworkManager.Get().InitNetwork(_args);
    }


    public ClientAppArgs getInitArgs()
	{
		return _args;
	}
		
    public virtual void destroy()
    {
        ClientApp.intstance = null;
    }

		
	public void Update()
	{

        if( GamePlay == GAMESTATUS.GMS_STATRGAME )
        {
            GamePlay = GAMESTATUS.GMS_LOGIN;
            GameUIManager.Instance().CreateFrame( GUIDefine.UIF_LOGINFRAME, true );
        }

        if( GamePlay == GAMESTATUS.GMS_LOGIN )
        {

        }

        GameUIManager.Instance().Update();
	}

    /// ---------------------------------------------------------------------------------
    /// 游戏登录状态
    /// ---------------------------------------------------------------------------------
    public void UpdateGameLogin()
    {

    }

    /// ---------------------------------------------------------------------------------
    /// play game 状态
    /// ---------------------------------------------------------------------------------
    public void UpdateGamePlaying()
    {

    }
}




