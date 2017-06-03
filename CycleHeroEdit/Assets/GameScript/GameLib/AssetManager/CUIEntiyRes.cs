using UnityEngine;
using System;
using System.Collections;
using GameObject = UnityEngine.GameObject;
using Object     = UnityEngine.Object;





public class CEntiyUI : Asset
{
    public CEntiyUI()
    {
        type = TYPE.UI;
    }

    /// -------------------------------------------------------------------------
    /// <summary>
    /// 开始异步加载资源
    /// </summary>
    /// -------------------------------------------------------------------------
    public override void LoadEntiy()
    {
        if (mainAsset != null)
        {
            LoaderObj.gameObject = UnityEngine.GameObject.Instantiate(mainAsset) as GameObject;
            base.LoadEntiy();
        }
    }

    
}


public class CEntiyScene : Asset
{


    public CEntiyScene()
    {
        type = TYPE.NPC;
    }

    /// -------------------------------------------------------------------------
    /// <summary>
    /// 开始异步加载资源
    /// </summary>
    /// -------------------------------------------------------------------------
    public override void LoadEntiy()
    {
        if (mainAsset != null)
        {
            LoaderObj.gameObject = UnityEngine.GameObject.Instantiate(mainAsset) as GameObject;
            base.LoadEntiy();
        }
    }
}


public class CEntityTerrain : Asset
{
    public CEntityTerrain()
    {
        type = TYPE.TERRAIN;
    }

    /// -------------------------------------------------------------------------
    /// <summary>
    /// 开始异步加载资源
    /// </summary>
    /// -------------------------------------------------------------------------
    public override void LoadEntiy()
    {
        if (mainAsset != null)
        {
            LoaderObj.gameObject = UnityEngine.GameObject.Instantiate(mainAsset) as GameObject;
            base.LoadEntiy();
        }
    }
}