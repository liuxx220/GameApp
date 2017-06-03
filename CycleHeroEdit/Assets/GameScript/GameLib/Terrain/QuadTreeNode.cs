/*
 * ------------------------------------------------------------------------------
 * 
 *          desc    : 基于地形Patch的四叉树节点对象 
 *  
 * 
 * ------------------------------------------------------------------------------
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class NodeData
{
    public int         _id;
    public string      _asset;
    public Vector3     _pos;
    public Vector3     _rot;
    public Vector3     _sca;
}


public class CQuadTreeNode
{

    /// <summary>
    /// 四叉树节点的，4个子节点
    /// </summary>
    public CQuadTreeNode[]      _chilren;

    // 如果是叶子节点的话，_NodeData 的内容就不会为空了
    private List< NodeData >    _NodeData = new List<NodeData>();

    public const int chilren    = 4;

    /// <summary>
    /// 节点在 x. z面上的范围
    /// </summary>
    protected Rect              _bound;

    public CQuadTreeNode( Rect bound )
    {
        _bound = bound;
    }

    public void SetChildrenNode( CQuadTreeNode[] childrens )
    {
        _chilren = childrens;
    }

    public Rect Bound
    {
        get { return _bound; }
    }


    public void Receive( NodeData  nodedata )
    {
        if ( !C2DBoxUilty.Intersects(_bound, nodedata) )
        {
            return;
        }

        if( _chilren == null )
        {
            _NodeData.Add(nodedata);
            return;
        }

        for( int i = 0; i < chilren; i++ )
        {
            if (_chilren[i] != null)
                _chilren[i].Receive(nodedata);
        }
    }
}

public delegate CQuadTreeNode   CreateNode( Rect bnd );
public delegate void            CForeachLeaf( CQuadTreeNode leaf );


public static class C2DBoxUilty
{

    public static bool Intersects( Rect nodeBound, NodeData nodedata )
    {
        return nodeBound.Contains( nodedata._pos );
    }


    public static void BuildRecursively( CQuadTreeNode node )
    {
        float subWidth      = node.Bound.width * 0.5f;
        float subHeight     = node.Bound.height * 0.5f;
        bool isPartible     = subWidth >= CQuadTreeConfig.CellSizeThreshole && subHeight >= CQuadTreeConfig.CellSizeThreshole;
        
        CreateNode _nodeCreator = ( bnd ) => { return new CQuadTreeNode(bnd); };
        CreateNode creator      = _nodeCreator;
        node.SetChildrenNode(new CQuadTreeNode[CQuadTreeNode.chilren] {
                                creator(new Rect(node.Bound.xMin,             node.Bound.yMin,                subWidth, subHeight)),
                                creator(new Rect(node.Bound.xMin + subWidth,  node.Bound.yMin,                subWidth, subHeight)),
                                creator(new Rect(node.Bound.xMin,             node.Bound.yMin + subHeight,    subWidth, subHeight)),
                                creator(new Rect(node.Bound.xMin + subWidth,  node.Bound.yMin + subHeight,    subWidth, subHeight)),});

        if (isPartible)
        {
            foreach (var sub in node._chilren )
            {
                BuildRecursively(sub);
            }
        }
    }
}