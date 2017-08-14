/*
 * ----------------------------------------------------------------------------------------------
 *          file name : PatchQuadTree.cs 
 *          desc      : 四插树对象
 *          author    : 李江坡
 *          log       : by ljp 创建 [ 2015-12-27 
 *                    : 实现世界地图的无缝连接，整个地形都基于 patch 管理
 * ----------------------------------------------------------------------------------------------         
*/
using UnityEngine;
using System;
using System.Collections.Generic;



public class CPatchQuadTree
{

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 四叉树的根节点
    /// </summary>
    /// ---------------------------------------------------------------------------
    private CQuadTreeNode _root;


    public CPatchQuadTree(Rect bound)
    {
        _root       = new CQuadTreeNode(bound);
        C2DBoxUilty.BuildRecursively(_root);
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 读取四叉树数据
    /// </summary>
    /// ---------------------------------------------------------------------------
    public void ImportFromFile( string szFile )
    {

    }

    /// -------------------------------------------------------------------------------------
    /// <summary>
    /// 向四叉树动态增加一个节点
    /// </summary>
    /// -------------------------------------------------------------------------------------
    public void AddTreeNode( NodeData nodedata )
    {
        _root.Receive(nodedata);
    }

    private void DrawDebug( )
    {

    }
}

