/*
 * ------------------------------------------------------------------------------
 *          file    : CQuadTreeConfig.cs
 *          
 *          desc    : 基于Patch的GameObject 的场景四叉树节点管理
 *  
 * 
 * ------------------------------------------------------------------------------
*/
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;





public class CQuadTreeConfig
{

    public static float     CellSizeThreshole = 50.0f;


    // 要求地形的大小必须是 128 的倍数
    public static int       TerrainPatchSizeX = 128;

    public static int       TerrainPatchSizeY = 128;

    
   
}


