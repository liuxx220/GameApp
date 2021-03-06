using UnityEngine;
using System;
using System.Collections;




public class CLoadLevelMgr : View
{

   
    private string          LoadLevelName   = "";
    private string          LoadedShowFrame = null;
    private float           _loadprocess    = 0.0f;

  

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 加载场景
    /// </summary>
    /// --------------------------------------------------------------------------
    IEnumerator BeginLoadLevel()
    {
      
        for (int i = 0; i < 120; i++)
        {
            _loadprocess += 1 / 120f;
            yield return new WaitForEndOfFrame();
        }

        Application.LoadLevel(LoadLevelName);

    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 启动协程开始加载场景
    /// </summary>
    /// --------------------------------------------------------------------------
    public void StartLoadLevel( string strLevel, string showframe )
    {
        LoadedShowFrame         = showframe;
        LoadLevelName           = strLevel;
        GameUIManager PanelMgr = LuaFramework.LuaHelper.GetPanelManager();
        PanelMgr.ShowFrame("CLoadLevelFrame"); ;
        StartCoroutine( BeginLoadLevel() );
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 启动协程开始加载场景
    /// </summary>
    /// --------------------------------------------------------------------------
    public int GetLoadProcees( )
    {
        return (int)_loadprocess;
    }
}
