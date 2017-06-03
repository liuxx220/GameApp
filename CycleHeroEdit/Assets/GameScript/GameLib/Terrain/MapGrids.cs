/*
 * ----------------------------------------------------------------------------------------------
 *          file name : TerrainPatch.cs 
 *          desc      : 地形块管理对象
 *          author    : 李江坡
 *          log       : by ljp 创建 [ 2015-12-27 
 *                    : 
 * ----------------------------------------------------------------------------------------------         
*/
using UnityEngine;
using System;
using System.Collections.Generic;



class CMapGrids
{

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------------------
    private CGrid       m_GridMap    = null;

    private Color       m_debugColor = Color.white;
    private bool        m_debugShow  = false;
    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------------------
    // Use this for initialization
    public virtual void Awake(Vector3 origin, int numRows, int numCols, float cellSize, bool show)
    {
        m_GridMap = new CGrid();
        m_GridMap.Awake(origin, numRows, numCols, cellSize, show); 
    }


    void OnDrawGizmos()
    {
        Gizmos.color = m_debugColor;

        if (m_debugShow)
        {
        //    m_GridMap.DebugDraw(transform.position, m_numberOfRows, m_numberOfColumns, m_cellSize, Gizmos.color);
        }

        //Gizmos.DrawCube(transform.position, new Vector3(0.25f, 0.25f, 0.25f));
    }
}

