/*
 * ----------------------------------------------------------------------------------------------
 *          file name : TerrainPatch.cs 
 *          desc      : 地形块
 *          author    : 李江坡
 *          log       : by ljp 创建 [ 2015-12-27 
 *                    : 实现世界地图的无缝连接，整个地形都基于 patch 管理
 * ----------------------------------------------------------------------------------------------         
*/
using UnityEngine;
using System;
using System.Xml;
using System.Collections.Generic;



class CTerrainPatch : AssetObject
{

    public static int TERRAINPATCHSIZE = 128;

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 地形 patch 的四叉树
    /// </summary>
    /// -----------------------------------------------------------------------------------------
    public CPatchQuadTree _quadtree = null;

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 是否加载完 patch 相关的数据
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    private bool          _loaded   = false;
    private Asset         _Asset    = null;
    private Terrain       _terrain  = null;

   
    bool IsLoaded
    {
        get { return _loaded; }
        set { _loaded = value; }
    }

    /// <summary>
    /// 加载地形patch相关的资源
    /// </summary>
    public void LoadFromFile( string patch )
    {
        //Rect Bound = new Rect( (x-1) * CQuadTreeConfig.TerrainPatchSizeX, 
        //                       (y-1) * CQuadTreeConfig.TerrainPatchSizeY, x * CQuadTreeConfig.TerrainPatchSizeX, y * CQuadTreeConfig.TerrainPatchSizeY );

        //if( _quadtree == null )
        //    _quadtree = new CPatchQuadTree( Bound );

        _Asset           = new CEntityTerrain();
        _Asset.source    = patch;
        _Asset.LoaderObj = this;
        _Asset.LoadAsset();
    }

    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 加载地形patch四叉树
    /// </summary>
    /// ------------------------------------------------------------------------------------
    private void LoadQuadTrees( XmlElement gameobject )
    {
        foreach( XmlElement child in gameobject.ChildNodes )
        {
            string _id            = child.GetAttribute("id");
            //string _layer         = child.GetAttribute("layer");
            string _asset         = child.GetAttribute("asset" );

            Vector3 pos           = Vector3.zero;
            Vector3 rot           = Vector3.zero;
            Vector3 sca           = Vector3.zero;

            XmlNode gameobjectpos = child.SelectSingleNode("transform").SelectSingleNode("position");
            XmlNode gameobjectrot = child.SelectSingleNode("transform").SelectSingleNode("rotation");
            XmlNode gameobjectsca = child.SelectSingleNode("transform").SelectSingleNode("scale");

            pos.x                 = float.Parse(gameobjectpos.SelectSingleNode("x").InnerText);
            pos.y                 = float.Parse(gameobjectpos.SelectSingleNode("y").InnerText);
            pos.z                 = float.Parse(gameobjectpos.SelectSingleNode("z").InnerText);

            rot.x                 = float.Parse(gameobjectrot.SelectSingleNode("x").InnerText);
            rot.y                 = float.Parse(gameobjectrot.SelectSingleNode("y").InnerText);
            rot.z                 = float.Parse(gameobjectrot.SelectSingleNode("z").InnerText);

            sca.x                 = float.Parse(gameobjectsca.SelectSingleNode("x").InnerText);
            sca.y                 = float.Parse(gameobjectsca.SelectSingleNode("y").InnerText);
            sca.z                 = float.Parse(gameobjectsca.SelectSingleNode("z").InnerText);

            NodeData nodedata     = new NodeData();
            nodedata._id          = int.Parse(_id);
            nodedata._asset       = _asset;
            nodedata._pos         = pos;
            nodedata._rot         = rot;
            nodedata._sca         = sca;
            _quadtree.AddTreeNode(nodedata);
        }

        IsLoaded = true;
    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// 地形资源加载完成
    /// </summary>
    /// ----------------------------------------------------------------------------
    public override void OnAsyncLoaded()
    {
        if (gameObject != null)
        {
            _terrain = gameObject.GetComponent<Terrain>();
            if( _terrain != null )
            {
                _loaded = true;
                RefreshPatchDetail();
            }
        }
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public void Destroy()
    {

    }


    private void RefreshPatchDetail( )
    {

        TerrainData  terrainData = _terrain.terrainData;
        SplatPrototype[] tsplatPrototypes = terrainData.splatPrototypes;
        for( int i = 0; i < tsplatPrototypes.Length; i++ )
        {

        }

        DetailPrototype[] tDetailPrototypes = terrainData.detailPrototypes;
        for( int i = 0; i < tDetailPrototypes.Length; i++ )
        {

        }

        TreePrototype[] tTreePrototype = terrainData.treePrototypes;
        for( int i = 0; i < tTreePrototype.Length; i++ )
        {

        }

        terrainData.RefreshPrototypes();
        _terrain.Flush();
    }
}

