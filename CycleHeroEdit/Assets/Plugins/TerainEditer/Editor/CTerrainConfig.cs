/*
 * ------------------------------------------------------------------------------
 * 
 *          desc    : 生成地形的Patchs 的配置管理类
 *  
 * 
 * ------------------------------------------------------------------------------
*/
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;




public enum ExcisionSize
{
    _2X2 = 2,
    _4X4 = 4,
    _8X8 = 8,
    _16X16 = 16,
    _32X32 = 32,
    _64X64 = 64,
}

class CTerrainConfig
{

    public static string    TerrainPatchPath;
    public static int       terrainsWide;
    public static int       terrainsLong;


    public static bool      isBlend = true;
    public static bool      isOverwrite = false;
    public static bool      isCopyAllTrees;
    public static bool      isCopyAllDetails;
    public static bool      isGenQuadtree = true;


}


public class CBackUpTerrain
{
    public  ExcisionSize    enumValue = ExcisionSize._2X2;
    public float            oldWidth;
    public float            oldHeight;
    public float            oldLenght;
    public float            newWidth;
    public float            newLength;

    public float            xPos;
	public float            yPos;
	public float            zPos;

    public SplatPrototype[] splatProtos;
    public DetailPrototype[]detailProtos;
    public TreePrototype[]  treeProtos;
    public TreeInstance[]   treeInst;

    public float            grassStrength;
    public float            grassAmount;
    public float            grassSpeed;
    public Color            grassTint;


    public float            treeDistance;
    public float            treeBillboardDistance;
    public float            treeCrossFadeLength;
    public int              treeMaximumFullLODCount;
    public float            detailObjectDistance;
    public float            detailObjectDensity;
    public float            heightmapPixelError;
    public int              heightmapMaximumLOD;
    public float            basemapDistance;
    public int              lightmapIndex;
    public bool             castShadows;
    public Material         materialTemplate;

    public int              newDetailResolution;
    public int              newEvenHeightMapResolution;
    public int              newHeightMapResolution;
    public int              newAlphaMapResolution;
    public int              newBaseMapResolution;

    public TerrainRenderFlags editorRenderFlags;


    public void BackTerrain( Terrain baseTerrain )
    {
        int nSize                   = (int)enumValue;
        CTerrainConfig.terrainsLong = nSize;
        CTerrainConfig.terrainsWide = nSize;


        TerrainData baseData        = baseTerrain.terrainData;
        oldWidth                    = baseData.size.x;
        oldHeight                   = baseData.size.y;
        oldLenght                   = baseData.size.z;

        newWidth                    = oldWidth  / CTerrainConfig.terrainsWide;
        newLength                   = oldLenght / CTerrainConfig.terrainsLong;

        xPos                        = baseTerrain.GetPosition().x;
        yPos                        = baseTerrain.GetPosition().y;
        zPos                        = baseTerrain.GetPosition().z;

        newHeightMapResolution      = ((baseData.heightmapResolution - 1) / nSize ) + 1;
        newEvenHeightMapResolution  = newHeightMapResolution - 1;
       
        newDetailResolution         = baseData.detailResolution / nSize;
        newAlphaMapResolution       = baseData.alphamapResolution / nSize;
        newBaseMapResolution        = baseData.baseMapResolution / nSize;

        treeDistance                = baseTerrain.treeDistance;
        treeBillboardDistance       = baseTerrain.treeBillboardDistance;
        treeCrossFadeLength         = baseTerrain.treeCrossFadeLength;
        treeMaximumFullLODCount     = baseTerrain.treeMaximumFullLODCount;
        detailObjectDistance        = baseTerrain.detailObjectDistance;
        detailObjectDensity         = baseTerrain.detailObjectDensity;
        heightmapPixelError         = baseTerrain.heightmapPixelError;
        heightmapMaximumLOD         = baseTerrain.heightmapMaximumLOD;
        basemapDistance             = baseTerrain.basemapDistance;
        lightmapIndex               = baseTerrain.lightmapIndex;
        castShadows                 = baseTerrain.castShadows;
        editorRenderFlags           = baseTerrain.editorRenderFlags;

        splatProtos                 = baseData.splatPrototypes;
        detailProtos                = baseData.detailPrototypes;
        treeProtos                  = baseData.treePrototypes;
        treeInst                    = baseData.treeInstances;

        grassStrength               = baseData.wavingGrassStrength;
        grassAmount                 = baseData.wavingGrassAmount;
        grassSpeed                  = baseData.wavingGrassSpeed;
        grassTint                   = baseData.wavingGrassTint;
    }
}

