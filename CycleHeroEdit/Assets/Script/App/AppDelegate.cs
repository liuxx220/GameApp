using UnityEngine;
using System;
using System.Collections;





/*
	可以理解为客户端启动的入口模块，为客户端数据初始化
	在这个入口中安装了需要监听的事件(installEvents)，同时初始化KBEngine(initKBEngine)
*/
public class AppDelegate : MonoBehaviour 
{
    public ClientApp gameapp = null;
	
	// 在unity3d界面中可见选项
	public string           ip   = "127.0.0.1";
	public int              port = 20013;
    public ClientApp.CLIENT_TYPE clientType = ClientApp.CLIENT_TYPE.CLIENT_TYPE_PC;

	public int threadUpdateHZ    = 10;
	
	void Awake( )
    {
        DontDestroyOnLoad(transform.gameObject);
    }
 
	// Use this for initialization
	void Start () 
	{
		
        InitClientApp();

        CProtoManager dataplat = new CProtoManager();
        dataplat.LoadXMLS();


        ///-----------------------------------------------------------------
        ///数据初始化
        CScriptLuaMgr scripmgr = new CScriptLuaMgr();
        scripmgr.InitLuaMgr();

        //------------------------------------------------------------------
        // UI 系统的初始化操作
        GameUIManager PanelMgr = LuaFramework.LuaHelper.GetPanelManager();
        PanelMgr.InitFrames();


        CQuestMgr questMgr = new CQuestMgr();
        questMgr.InitQuest();

        CItemMgr itemgr = new CItemMgr();
        itemgr.InitIteMgr();

        CFightTeamMgr.Instance.Initlize();
        ///-----------------------------------------------------------------
	}
	
	public virtual void InitClientApp()
	{

        ClientAppArgs args      = new ClientAppArgs();
		
		args.ip                 = ip;
		args.port               = port;
		args.clientType         = clientType;
		args.threadUpdateHZ     = threadUpdateHZ;
		
        gameapp = new ClientApp(args);
	}
	
	void OnDestroy()
	{
		ClientApp.intstance.destroy();
	}
}
