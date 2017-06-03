using UnityEngine;
using System;
using System.IO;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using GameObject = UnityEngine.GameObject;





public class GameUIManager : View
{

    static GameUIManager        instance = null;
    private GameObject          UIRoot   = null;
    private Dictionary<string, GUIFrame> m_mapFrame = new Dictionary<string, GUIFrame>();
    private List<GUIFrame> m_mapDestory  = new List<GUIFrame>();
    private AssetBundlePool uiAssetPool  = new AssetBundlePool();


    public static GameUIManager Instance()
    {
        if (instance == null)
            instance = new GameUIManager();
        return instance;
    }


    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 创建UI界面
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void InitFrames( )
    {
        //---------------------Lua面板---------------------------
        // 初始化 Lua 和注册各个空间的 wrap
        LuaManager.Start();

        UIRoot = GameObject.Find("UI Root");
        if (LuaManager != null)
        {
            LuaManager.DoFile("GUI/UIPanelManager");
            object[] panels = CallMethod("LuaScriptPanel");
            //---------------------Lua面板---------------------------
            foreach (object o in panels)
            {
                string name = o.ToString().Trim();
                if (string.IsNullOrEmpty(name))
                    continue;

                LuaManager.DoFile("GUI/" + name);
                //Debug.LogWarning("LoadLua---->>>>" + name + ".lua");
            }
            //------------------------------------------------------------
            CallMethod("InitUIManagerOK");
        }
    }

    public void CheckExtractRes()
    {
        bool isExists  = Directory.Exists( Util.DataPath ) &&
                         Directory.Exists( Util.DataPath + "/lua") &&
                         File.Exists(Util.DataPath + "files.txt");

        if( isExists || ClientAppArgs.DebugMode )
        {
            return;
        }
    }


    IEnumerator OnExtractResource( )
    {
        string dataPath = Util.DataPath;
        string resPath  = Util.AppContentPath();   // 游戏包资源目录
        if( Directory.Exists( dataPath ))
            Directory.Delete(dataPath, true);

        Directory.CreateDirectory(dataPath);
        string infile   = resPath + "files.txt";
        string outfile  = resPath + "files.txt";

        if( Application.platform == RuntimePlatform.Android )
        {
            WWW www = new WWW(infile);
            yield return www;
            if (www.isDone)
            {
                File.WriteAllBytes(outfile, www.bytes);
            }
            yield return 0;
        }
        else
        {
            File.Copy(infile, outfile, true);
        }
        yield return new WaitForEndOfFrame();

        // 解析 files.txt 文件
        string[] files = File.ReadAllLines(outfile);
        foreach( var file in files )
        {
            string[] fs = file.Split('|');
            infile      = resPath + fs[0];
            outfile     = dataPath + fs[0];

            string dir  = Path.GetDirectoryName(outfile);
            if( !Directory.Exists(dir) )
                Directory.CreateDirectory(dir);

            if( Application.platform == RuntimePlatform.Android )
            {
                WWW www = new WWW(infile);
                yield return www;
                if( www.isDone )
                    File.WriteAllBytes(outfile, www.bytes);
                yield return 0;
            }
            else
            {
                if( File.Exists( outfile ))
                {
                    File.Delete(outfile);
                }

                File.Copy(infile, outfile, true);
            }

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(OnUpdateResource());
    }


    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 创建UI界面
    /// </summary>
    ///--------------------------------------------------------------------------------------
    IEnumerator OnUpdateResource()
    {
        List<string> downFiles = new List<string>();
        downFiles.Clear();

        string dataPath = Util.DataPath;
        string url      = ClientAppArgs.WebUrl;
        string random   = DateTime.Now.ToString( "yyyymmddhhmmss" );
        string listUrl  = url + "files.txt?v=" + random;

        WWW www = new WWW(listUrl);
        yield return www;

        if( www.error != null )
        {
            // 更新失败！！！！！！！！！
            yield break;
        }

        if( !Directory.Exists( dataPath ) )
        {
            Directory.CreateDirectory(dataPath);
        }
        File.WriteAllBytes(dataPath + "files.txt", www.bytes);

        string filesText = www.text;
        string[] files   = filesText.Split('\n');
        for (int i = 0; i < files.Length;  i++ )
        {
            if( string.IsNullOrEmpty( files[i]))
                continue;
            string[] key = files[i].Split('|');
            string f     = key[0];
            string local = (dataPath + f).Trim();
            string path  = Path.GetDirectoryName( local );
            if( !Directory.Exists( path ))
                Directory.CreateDirectory(path);

        }
        yield return new WaitForEndOfFrame();
    }

    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 创建UI界面
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void CreatePanel(string name, LuaFunction func = null)
    {
        if (string.IsNullOrEmpty(name))
            return;

        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(name, out pFrame);
        if (pFrame == null)
        {
            pFrame = new GUIFrame( name );
            pFrame.mAsset.source = name;
            m_mapFrame.Add(name, pFrame);
        }

        {
            pFrame.mAsset.source = name;
            pFrame.mAsset.LoadAsset();
        }
    }
 
    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 创建UI界面
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void CreateFrame( string strFrameName, bool bLoad )
    {

        if (string.IsNullOrEmpty(strFrameName))
            return;

        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(strFrameName, out pFrame);
        if( pFrame == null )
        {
            pFrame = new GUIFrame();
            pFrame.mAsset.source = strFrameName;    
            m_mapFrame.Add(strFrameName, pFrame);
        }
        
        if( bLoad )
        {
            pFrame.mAsset.source = strFrameName;
            pFrame.mAsset.LoadAsset();
        }
    }

    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 创建并显示UI界面
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void DestoryFrame( string strFrameName )
    {
         if (string.IsNullOrEmpty(strFrameName))
            return;

        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(strFrameName, out pFrame);
        if (pFrame != null)
        {
            uiAssetPool.removeLoad(pFrame.mAsset.source);
            m_mapFrame.Remove(strFrameName);
            m_mapDestory.Add( pFrame );
        }
    }


    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 显示某个窗口
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void ShowFrame( string strFrameName )
    {
        if (string.IsNullOrEmpty(strFrameName))
            return;

        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(strFrameName, out pFrame);
        if (pFrame != null)
        {
            if (pFrame.IsLoaded())
            {
                pFrame.OnShow();
            }
        }
        else
        {
            CreatePanel(strFrameName);
        }
    }

    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 隐藏某个窗口
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void HideFrame( string strFrameName )
    {
        if (string.IsNullOrEmpty(strFrameName))
            return;

        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(strFrameName, out pFrame);
        if (pFrame != null)
        {
            pFrame.OnHide();
        }
    }


    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 判断窗口是否已经存在
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public bool IsExistFrame( string nFrameID )
    {
        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(nFrameID, out pFrame);
        if (pFrame != null)
            return true;

        return false;
    }


    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 判断窗口是否已经存在
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public GUIFrame GetFrame(string nFrameID)
    {
        GUIFrame pFrame = null;
        m_mapFrame.TryGetValue(nFrameID, out pFrame);
        if (pFrame != null)
            return pFrame;

        return null;
    }

    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 将新生成的UI挂接到UI Root 上
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void AttackUIRoot(GUIFrame GameFrame)
    {
        
        if (UIRoot != null && GameFrame.transform != null)
        {
            Transform form = GameFrame.transform;
            form.parent    = UIRoot.transform;
            GameFrame.transform.localScale    = Vector3.one;
            GameFrame.transform.localPosition = Vector3.zero;
        }
    }

    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 将新生成的UI从UI Root 上删除
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void DetachUIRoot(GUIFrame GameFrame, bool bDestroy)
    {
       
        if (UIRoot != null && GameFrame.transform != null)
        {
            GameFrame.transform.parent = null;
        }
    }


    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 界面的Update逻辑
    /// </summary>
    ///--------------------------------------------------------------------------------------
    public void Update( )
    {

        if( UIRoot == null )
        {
            UIRoot = GameObject.Find("UI Root");
        }

        foreach( var item in m_mapFrame )
        {
            GUIFrame pFrame = item.Value as GUIFrame;
            if (pFrame != null && pFrame.IsLoaded() && pFrame.IsNeedUpdate )
                pFrame.Update();
        }

        for (int i = 0; i < m_mapDestory.Count; i++  )
        {
            GUIFrame pFrame = m_mapDestory[i];
            DetachUIRoot( pFrame, true );
            pFrame.Destroy();
        }
        m_mapDestory.Clear();
    }

    ///--------------------------------------------------------------------------------------
    /// <summary>
    /// 执行Lua方法
    /// </summary>
    ///--------------------------------------------------------------------------------------
    protected object[] CallMethod(string func, params object[] args)
    {
        return Util.CallMethod("GameUIManager", func, args);
    }
}