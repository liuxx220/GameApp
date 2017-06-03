/*
 * ----------------------------------------------------------------------------------------------
 *          file name : WorldMapMgr.cs 
 *          desc      : 世界地图管理器
 *          author    : 李江坡
 *          log       : by ljp 创建 [ 2015-12-27 
 *                    : 实现世界地图的无缝连接，整个地形都基于 patch 管理
 *                    : 本管理器只管patch和动态物体，但除特效除外
 * ----------------------------------------------------------------------------------------------         
*/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;





enum GRIDENUM
{
    LeftTop = 0,
    Top = 1,
    RightTop = 2,
    Left = 3,
    Mid = 4,
    Right = 5,
    LeftDown = 6,
    Down = 7,
    RightDown = 8,
}


/// ----------------------------------------------------------------------------------------------
/// <summary>
/// 管理九宫格的地形Patch,以及相关的资源
/// </summary>
/// ----------------------------------------------------------------------------------------------
public class CWorldMapMgr
{

   

    const int GRIDSIZE = 9;

    private static CWorldMapMgr _instance;

    /// <summary>
    /// 九宫格来管理地形的patch
    /// </summary>
    private CTerrainPatch[]     _patchs          = new CTerrainPatch[GRIDSIZE];
    private Dictionary<int, CTerrainPatch> _Grid = new Dictionary<int,CTerrainPatch>();
    //private bool[,,]            _loadedTerrain   = null;

    public struct ChunkPos
    {
        public int x;
        public int z;
    };

    private ChunkPos            _lastchunk;

    /// <summary>
    /// 世界地图分割了多少块 N*N
    /// </summary>
    public int                  chunkSplit    = 8;  

    public static CWorldMapMgr Instance
    {
        get
        {
            if( _instance == null )
                _instance = new CWorldMapMgr();
            return _instance;
        }
    }

    public CWorldMapMgr( )
    {
        _lastchunk.x = -1;
        _lastchunk.z = -1;

        InitialWorld();
    }


    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 根据角色当前的位置，更新地形的九宫格
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    public void UpdateViewPatchs( float PosX, float PosZ )
    {

        ChunkPos chunk = CalcChunkPosition(PosX, PosZ);
        if (chunk.x == _lastchunk.x && chunk.z == _lastchunk.z)
            return;


        for (int i = -1; i <= 1; i++)
        {
            for (int ii = -1; ii <= 1; ii++)
            {
                ChunkPos temp = _lastchunk;
                temp.x       += i;
                temp.z       += ii;
                int deltX     = Math.Abs( chunk.x - temp.x );
                int deltZ     = Math.Abs( chunk.z - temp.z );


                // 从九宫格内删除 patch
                int nOldID    = CalcChunkID(temp);
                bool IsInView = IsInViewByGridID(nOldID);
                if ((deltX >= 2 || deltZ >= 2) && IsInView)
                {
                    delLoadPatch(temp);
                }

                // 组织新的九宫格内的数据
                ChunkPos temp2 = chunk;
                temp2.x     += i;
                temp2.z     += ii;
                int nNewID  = CalcChunkID(temp2);
                IsInView    = IsInViewByGridID(nNewID);
                if (!IsInView)
                {
                    addLoadPatch(temp2);
                }
            }
        }

        _lastchunk     = chunk;
    }


    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 根据角色当前的位置，更新地形的九宫格
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private void refresh9GridPatch( ChunkPos chunk )
    {
        ChunkPos LeftTop, RightTop, RightDown, LeftDown;
        ChunkPos Top, Left, Right, Down;
        // 左上
        LeftTop.x       = chunk.x - 1;
        LeftTop.z       = chunk.z + 1;

        // 右上
        RightTop.x      = chunk.x + 1;
        RightTop.z      = chunk.z + 1;

        // 左下
        LeftDown.x      = chunk.x - 1;
        LeftDown.z      = chunk.z - 1;

        // 右下
        RightDown.x     = chunk.x + 1;
        RightDown.z     = chunk.z - 1;

        // 左
        Left.x          = chunk.x - 1;
        Left.z          = chunk.z;

        // 右
        Right.x         = chunk.x + 1;
        Right.z         = chunk.z;

        // 上
        Top.x           = chunk.x;
        Top.z           = chunk.z + 1;

        // 下
        Down.x          = chunk.x;
        Down.z          = chunk.z - 1;
    }

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 判断某个格子是否在九宫格内
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private bool IsInViewByGridID( int nGridID )
    {
        CTerrainPatch patch = null;
        if( _Grid.TryGetValue( nGridID, out patch ) )
        {
            return true;
        }
        return false;
    }


    private void delLoadPatch( ChunkPos chunk)
    {
        CTerrainPatch patch = null;
        int nGridID         = CalcChunkID(chunk);
        if (_Grid.TryGetValue(nGridID, out patch))
        {
            _Grid.Remove(nGridID);
            patch.Destroy();
        }
    }

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 加载一个patch
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private void addLoadPatch( ChunkPos chunk )
    {
        CTerrainPatch patch = new CTerrainPatch();
        if( patch != null )
        {
            patch.LoadFromFile(GetChunkName(chunk));
        }
    }

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 根据坐标转成地块的索引
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private ChunkPos CalcChunkPosition( float x, float z )
    {
        ChunkPos pos;

        pos.x = (int)(x / CTerrainPatch.TERRAINPATCHSIZE);
        pos.z = (int)(z / CTerrainPatch.TERRAINPATCHSIZE);

        return pos;
    }

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 根据块计算数组的索引
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private int CalcChunkID( ChunkPos chunk )
    {
        return chunk.z * chunkSplit + chunk.x;
    }


    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 根据坐标转成地块的名字
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    public string GetChunkName( ChunkPos chunk )
    {
        return "patch_" + (chunk.z + 1) + "_" + (chunk.x + 1);
    }

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 初始化游戏世界的一些基本配置参数
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private void InitialWorld( )
    {
        for (int i = 0; i < GRIDSIZE; i++)
        {
            _patchs[i] = null;
        }
    }
}

