using System;
using UnityEngine;
using MessageLengthEx = System.UInt32;
	








/*
	初始化应用程序的的参数类
*/
public class ClientAppArgs 
{
    // 登录ip和端口
	public string ip            = "127.0.0.1";
	public int port             = 20013;
		
	public string 	_username   = "kbengine";
	public string 	_password   = "123456";
		
	// 客户端类型
	public ClientApp.CLIENT_TYPE clientType = ClientApp.CLIENT_TYPE.CLIENT_TYPE_MOBILE;
		
    // 只在多线程模式启用， 线程主循环处理频率
	public int threadUpdateHZ   = 10;


    //////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    public const bool DebugMode         = true;                        //调试模式-用于内部测试

    /// <summary>
    /// 如果想删掉框架自带的例子，那这个例子模式必须要
    /// 关闭，否则会出现一些错误。
    /// </summary>
    public const bool ExampleMode       = true;                        //例子模式 

    /// <summary>
    /// 如果开启更新模式，前提必须启动框架自带服务器端。
    /// 否则就需要自己将StreamingAssets里面的所有内容
    /// 复制到自己的Webserver上面，并修改下面的WebUrl。
    /// </summary>
    public const bool UpdateMode        = false;                        //更新模式-默认关闭 
    public const bool AutoWrapMode      = true;                         //自动添加Wrap模式

    public const bool UsePbc            = true;                         //PBC
    public const bool UseLpeg           = true;                         //lpeg
    public const bool UsePbLua          = true;                         //Protobuff-lua-gen
    public const bool UseCJson          = true;                         //CJson
    public const bool UseSproto         = true;                         //Sproto
    public const bool LuaEncode         = false;                        //使用LUA编码

    public const int TimerInterval      = 1;
    public const int GameFrameRate      = 30;                           //游戏帧频

    public const string AppName         = "SimpleFramework";            //应用程序名称
    public const string AppPrefix       = AppName + "_";                //应用程序前缀
    public const string WebUrl          = "http://localhost:6688/";     //测试更新地址

    public static string UserId         = string.Empty;                 //用户ID
    public static int SocketPort        = 0;                            //Socket服务器端口
    public static string SocketAddress  = string.Empty;                 //Socket服务器地址

    public static string LuaBasePath
    {
        get { return Application.dataPath + "/uLua/Source/"; }
    }

    public static string LuaWrapPath
    {
        get { return LuaBasePath + "LuaWrap/"; }
    }
}
