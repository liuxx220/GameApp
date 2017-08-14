/*
 ---------------------------------------------------------------------------------
        file name : Frame.cs
        desc      : UI Frame 的基类
        author    : LJP
 
        log       : [ 2015-05-02 ]
 ---------------------------------------------------------------------------------
*/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;





public class GUIFrame : AssetObject
{

    public UIPanel        m_hPanel    = null;
    public Transform      transform   = null;
    public Asset          mAsset      = null;
    public bool           mIsLoaded   = false;
    private string        modelName   = "";
    public bool           IsNeedUpdate = false;

    private UISprite[]    UISpriteList;
    private UITexture[]   UITextureList;


    private LuaScriptMgr  m_LuaMgr = null;
    public GUIFrame()
    {
        mIsLoaded        = false;
        mAsset           = new CEntiyUI();
        mAsset.LoaderObj = this;

        if (m_LuaMgr == null)
        {
            m_LuaMgr     = ClientApp.Instance.GetManager<LuaScriptMgr>(ManagerName.Lua);
        }
    }

    public GUIFrame( string model )
    {

        modelName        = model;
        mIsLoaded        = false;
        mAsset           = new CEntiyUI();
        mAsset.LoaderObj = this;

        if (m_LuaMgr == null)
        {
            m_LuaMgr     = ClientApp.Instance.GetManager<LuaScriptMgr>(ManagerName.Lua);
        }
    }


    ///--------------------------------------------------------------------
    /// <summary>
    /// 导入本窗口的子控件到代码中
    /// </summary>
    ///--------------------------------------------------------------------
    public virtual bool ReloadUI( )
    {

        m_hPanel        = transform.GetComponent<UIPanel>();
        UISpriteList    = transform.GetComponentsInChildren<UISprite>( );
        UITextureList   = transform.GetComponentsInChildren<UITexture>();

        // 挂CLuaBehaviour 组件，做注册事件使用
        transform.gameObject.AddComponent<CLuaBehaviour>();

        GameUIManager PanelMgr = LuaFramework.LuaHelper.GetPanelManager();
        PanelMgr.AttackUIRoot(this);
        CallMethod("ReloadUI", transform.gameObject );
        return true;
    }

  
    ///--------------------------------------------------------------------
    /// <summary>
    /// 控制窗口的显示
    /// </summary>
    ///--------------------------------------------------------------------
    public virtual void OnShow( )
    {
        if( m_hPanel != null )
        {
            m_hPanel.gameObject.SetActive(true);
        }
    }

    ///--------------------------------------------------------------------
    /// <summary>
    /// 控制窗口的隐藏
    /// </summary>
    ///--------------------------------------------------------------------
    public virtual void OnHide( )
    {
        if (m_hPanel != null)
        {
            m_hPanel.gameObject.SetActive(false);
        }
    }

    ///--------------------------------------------------------------------
    /// <summary>
    /// 判断窗口是否显示
    /// </summary>
    ///--------------------------------------------------------------------
    public bool IsFrameShow( )
    {
        if (m_hPanel != null)
        {
            return m_hPanel.gameObject.activeSelf;
        }
        return false;
    }

    public bool IsLoaded()
    {
        return mIsLoaded;
    }

    ///--------------------------------------------------------------------
    /// <summary>
    /// 控制窗口点任意位置隐藏
    /// </summary>
    ///--------------------------------------------------------------------
    public virtual void OnEscape( )
    {

    }

    ///--------------------------------------------------------------------
    /// <summary>
    /// 界面的Update 逻辑
    /// </summary>
    ///--------------------------------------------------------------------
    public virtual void Update()
    {
        if( IsNeedUpdate )
            CallMethod("Update");
    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// UI资源加载万程
    /// </summary>
    /// ----------------------------------------------------------------------------
    public override void OnAsyncLoaded()
    {
        if( gameObject != null )
        {
            transform = gameObject.transform;
            
            ReloadUI();
            mIsLoaded = true;
        }
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public virtual void Destroy()
    {
        CallMethod("OnDestroy");

        mIsLoaded   = false;
        m_hPanel    = null;
        //for (int node = 0; node < UISpriteList.Length; node++ )
        //{
        //    UnityEngine.GameObject.DestroyImmediate(UISpriteList[node].gameObject);
        //    UISpriteList[node] = null;
        //}

        //for (int node = 0; node < UITextureList.Length; node++)
        //{
        //    UnityEngine.GameObject.DestroyImmediate(UITextureList[node].gameObject);
        //    UITextureList[node] = null;
        //}

        //if (transform != null && transform.gameObject != null)
        //{
        //    UnityEngine.GameObject.Destroy(transform.gameObject);
        //}

        if( mAsset != null )
        {
            mAsset.Destroy();
        }
        mAsset = null;
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }


    /// --------------------------------------------------------------------------
    /// <summary>
    /// 添加单击事件
    /// </summary>
    /// --------------------------------------------------------------------------
    public void AddClick(GameObject go, LuaFunction luafunc)
    {
        if (go == null) return;
        UIEventListener.Get(go).onClick = delegate(GameObject o)
        {
            luafunc.Call(go);
        };
    }


    /// --------------------------------------------------------------------------
    /// <summary>
    /// 执行 Lua 接口
    /// </summary>
    /// --------------------------------------------------------------------------
    protected object[] CallMethod( string func, params object[] args )
    {
        return LuaFramework.Util.CallMethod(modelName, func, args);
    }
}
