using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;




public class CTerrainExcision : EditorWindow {

    private GameObject[]    selection;
    private GameObject[]    terrainGameObjects;
    private Terrain[]       terrains;
    private TerrainData[]   data;

    static public Terrain   baseTerrain;
    private TerrainData     baseData;
    private int             resolutionPerPatch = 8;


    /// UI Layout
    private GUIContent      label1       = null;
    private GUIContent      label2       = null;
    private GUIContent      label3       = null;
    private GUIContent      label4       = null;
    private GUIContent      label5       = null;
    private GUIContent      label6       = null;
    private GUIContent      label7       = null;
    private GUIContent      label8       = null;
    private GUIContent      label9       = null;
    private GUIContent      label10      = null;
    private GUIContent      label11      = null;


    public static CBackUpTerrain _BackTerrain = null;
    // 在 Unity3D 工具栏中增加菜单
    [MenuItem("TerrainEditor/地形切割管理")]
    static void ShowCTerrainExcisionWindow()
    {
        _BackTerrain            = new CBackUpTerrain();
        CTerrainExcision window = EditorWindow.GetWindow(typeof(CTerrainExcision)) as CTerrainExcision;
        window.position         =  new Rect( Screen.width/2, 300, 600, 300 );
    }

    /// --------------------------------------------------------------------------------------
    /// <summary>
    /// 编辑器界面初始化操作
    /// </summary>
    /// --------------------------------------------------------------------------------------
    void OnEnable()
    {
        minSize     = new Vector2( 660, 370 );
        if (Application.isPlaying)
            return;

        
        // 在运行模式下，不允许地形分割操作
        if( !PlayerPrefs.HasKey("Terrain path"))
        {
            PlayerPrefs.SetString("Terrain path", "Assets/TerrainPatch/TerrainData");
            CTerrainConfig.TerrainPatchPath = "Assets/TerrainPatch/TerrainData";
        }
        else
        {
            CTerrainConfig.TerrainPatchPath = PlayerPrefs.GetString("Terrain path");
        }

        if (!Directory.Exists(CTerrainConfig.TerrainPatchPath))
            Directory.CreateDirectory(CTerrainConfig.TerrainPatchPath);


        /// 得到选择的对象
        selection = Selection.gameObjects;
        if( selection.Length == 1 )
        {
            if (selection[0].GetComponent<Terrain>() != null)
                baseTerrain = selection[0].GetComponent<Terrain>() as Terrain;
            else
                Debug.Log("选择错误：选择的 GameObject 不是一个地图对象！");
        }
            
        else if( selection.Length > 1 )
        {
            Debug.Log("选择错误：选择的 GameObject 太多了！只能选择一个地图对象");
        }

        label1          = new GUIContent("选择地形", "");
        label2          = new GUIContent("地块大小", "");
        label3          = new GUIContent("分割维度", "");
        label4          = new GUIContent("保存路径", "");
        label5          = new GUIContent("重设路径", "");
        label6          = new GUIContent("默认保存路径", "");
        label7          = new GUIContent("覆盖地形数据", "");
        label8          = new GUIContent("平滑地行边缘", "");
        label9          = new GUIContent("是否拷贝植被", "");
        label10         = new GUIContent("拷贝场景细节", "");
        label11         = new GUIContent("生成四茶树", "对每个Patch生成一个四叉树");
    }

    /// --------------------------------------------------------------------------------------
    /// <summary>
    /// 编辑器界面初始化操作
    /// </summary>
    /// --------------------------------------------------------------------------------------
    void OnGUI()
    {

        /// 本操作只能编辑器模式下运行
        if (!Application.isPlaying)
        {
            GUILayout.Label( "参数设置", EditorStyles.boldLabel );

            EditorGUILayout.Space();
            baseTerrain         = (Terrain)EditorGUILayout.ObjectField(label1, baseTerrain, typeof(Terrain), false);
            EditorGUILayout.Space();

            resolutionPerPatch  = EditorGUILayout.IntField(label2, resolutionPerPatch);
            EditorGUILayout.Space();

            _BackTerrain.enumValue = (ExcisionSize)EditorGUILayout.EnumPopup(label3, _BackTerrain.enumValue );
            EditorGUILayout.Space();

            CTerrainConfig.TerrainPatchPath = EditorGUILayout.TextField(label4, CTerrainConfig.TerrainPatchPath );
            EditorGUILayout.Space();

            if( GUILayout.Button( label5 ) )
            {
                GUIUtility.keyboardControl = 0;
                CTerrainConfig.TerrainPatchPath = PlayerPrefs.GetString("Terrain path");
                SaveTerrianPath();
            }
            EditorGUILayout.Space();

            if( GUILayout.Button( label6 ) )
            {

            }
            EditorGUILayout.Space();

            CTerrainConfig.isOverwrite = EditorGUILayout.Toggle(label7, CTerrainConfig.isOverwrite);
            EditorGUILayout.Space();

            CTerrainConfig.isBlend = EditorGUILayout.Toggle(label8, CTerrainConfig.isBlend);
            EditorGUILayout.Space();

            CTerrainConfig.isCopyAllTrees = EditorGUILayout.Toggle(label9, CTerrainConfig.isCopyAllTrees);
            EditorGUILayout.Space();

            CTerrainConfig.isCopyAllDetails = EditorGUILayout.Toggle(label10, CTerrainConfig.isCopyAllDetails);
            EditorGUILayout.Space();

            CTerrainConfig.isGenQuadtree = EditorGUILayout.Toggle(label11, CTerrainConfig.isGenQuadtree);
            EditorGUILayout.Space();

            // 分割地形
            if( GUILayout.Button( "开始分割地形" ) )
            {
                if (baseTerrain != null)
                {
                    _BackTerrain.BackTerrain(baseTerrain);
                    baseData = baseTerrain.terrainData;

                    if (CheckForErrors())
                    {
                        GenTerrainPatchs();
                    }

                    if (CTerrainConfig.isBlend)
                    {
                        BlendPatchsEdges();
                    }

                    SetPatchsNeighbors();
                    this.Close();
                }
                else
                {
                    this.ShowNotification(new GUIContent("Base Terrain must be selected."));
                }
            }
        }
    } 

    void SaveTerrianPath()
    {
        PlayerPrefs.SetString("Terrain path", CTerrainConfig.TerrainPatchPath );
        label5 = new GUIContent("重设路径", "");
    }

    private float _progress = 0.0f;
    private float progressScale = 1;

    /// ----------------------------------------------------------------------------
    /// <summary>
    /// 检查参数配置数据是否有效
    /// </summary>
    /// ----------------------------------------------------------------------------
    bool CheckForErrors()
    {
        if( resolutionPerPatch < 8 )
        {
            this.ShowNotification( new GUIContent("Resolution Per Patch must be 8 or greater") );
			GUIUtility.keyboardControl = 0;
			return false;
        }
        else if (!Mathf.IsPowerOfTwo(resolutionPerPatch))
        {
            this.ShowNotification( new GUIContent("Resolution Per Patch must be a power of 2"));
            GUIUtility.keyboardControl = 0;
            return false;
        }

        else if (_BackTerrain.newHeightMapResolution < 33)
        {
            this.ShowNotification(new GUIContent("Error with Heightmap Resolution - See Console for More Information"));
            GUIUtility.keyboardControl = 0;
            return false;
        }
        else if (_BackTerrain.newAlphaMapResolution < 16)
        {
            this.ShowNotification(new GUIContent("Error with AlphaMap Resolution - See Console for More Information"));
            GUIUtility.keyboardControl = 0;
            return false;
        }
        else if (_BackTerrain.newBaseMapResolution < 16)
        {
            this.ShowNotification(new GUIContent("Error with BaseMap Resolution - See Console for More Information"));
            GUIUtility.keyboardControl = 0;
            return false;
        }
       

        return true;
    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// 把选择的地形分割成多个Patch
    /// </summary>
    /// ----------------------------------------------------------------------------
    public void GenTerrainPatchs()
    {
        _progress = 0.0f;
        EditorUtility.DisplayProgressBar("Progress", "Gen Terrain Patchs", _progress);

        if (!Mathf.IsPowerOfTwo( _BackTerrain.newDetailResolution))
        {
            Debug.Log("Detail Resolution of new terrains is not a power of 2. Accurate results are not guaranteed.");
        }

        int nSize           = CTerrainConfig.terrainsLong * CTerrainConfig.terrainsWide;
        terrainGameObjects  = new GameObject[nSize];
        terrains            = new Terrain[nSize];
        data                = new TerrainData[nSize];
        progressScale       = .9f / nSize;

        for (int y = 0; y < CTerrainConfig.terrainsLong; y++)
        {
            for (int x = 0; x < CTerrainConfig.terrainsWide; x++)
            {
                AssetDatabase.CreateAsset(new TerrainData(), CTerrainConfig.TerrainPatchPath + "/" + "patch_" + (y + 1) + "_" + (x + 1) + ".asset");
                _progress += progressScale;
                EditorUtility.DisplayProgressBar("Progress", "Gen Terrain Patchs", _progress);
            }
        }


        /// 开始拷贝地形数据
        int arraypos = 0;
        progressScale = .2f / nSize;
        for (int y = 0; y < CTerrainConfig.terrainsLong; y++)
        {
            for (int x = 0; x < CTerrainConfig.terrainsWide; x++)
            {
                TerrainData terData                 = AssetDatabase.LoadAssetAtPath(CTerrainConfig.TerrainPatchPath + "/" + "patch_" + (y + 1) + "_" + (x + 1) + ".asset", typeof(TerrainData)) as TerrainData;
                terrainGameObjects[arraypos]        = Terrain.CreateTerrainGameObject(terData);
                terrainGameObjects[arraypos].name   = "patch_" + (y + 1) + "_" + (x + 1);
                terrains[arraypos]                  = terrainGameObjects[arraypos].GetComponent<Terrain>();
                data[arraypos]                      = terrains[arraypos].terrainData;
                data[arraypos].heightmapResolution  = _BackTerrain.newEvenHeightMapResolution;
                data[arraypos].alphamapResolution   = _BackTerrain.newAlphaMapResolution;
                data[arraypos].baseMapResolution    = _BackTerrain.newBaseMapResolution;
                data[arraypos].SetDetailResolution(_BackTerrain.newDetailResolution, resolutionPerPatch);
                data[arraypos].size                 = new Vector3(_BackTerrain.newWidth, _BackTerrain.oldHeight, _BackTerrain.newLength);

                SplatPrototype[] tempSplats         = new SplatPrototype[_BackTerrain.splatProtos.Length];
                for( int i = 0; i < _BackTerrain.splatProtos.Length; i++ )
                {
                    tempSplats[i]                   = new SplatPrototype();
                    tempSplats[i].texture           = _BackTerrain.splatProtos[i].texture;
                    tempSplats[i].tileSize          = _BackTerrain.splatProtos[i].tileSize;

                    float offsetX                   = (_BackTerrain.newWidth * x ) % _BackTerrain.splatProtos[i].tileSize.x + _BackTerrain.splatProtos[i].tileSize.x;
                    float offsetY                   = (_BackTerrain.newLength * y ) % _BackTerrain.splatProtos[i].tileSize.y + _BackTerrain.splatProtos[i].tileSize.y;
                    tempSplats[i].tileOffset        = new Vector2(offsetX, offsetY);
                }
                data[arraypos].splatPrototypes      = tempSplats;

                int[] layers = baseData.GetSupportedLayers(x * data[arraypos].detailWidth-1, y * data[arraypos].detailHeight-1, data[arraypos].detailWidth, data[arraypos].detailHeight);
				int layerLength = layers.Length;
                if (CTerrainConfig.isCopyAllDetails)
                    data[arraypos].detailPrototypes = _BackTerrain.detailProtos;
                else
                {
                    DetailPrototype[] tempDetailProtos = new DetailPrototype[layerLength];
                    for( int n = 0; n < layerLength; n++ )
                        tempDetailProtos[n] = _BackTerrain.detailProtos[layers[n]];
                    data[arraypos].detailPrototypes = tempDetailProtos;
                }

                for (int i = 0; i < layerLength; i++)
                    data[arraypos].SetDetailLayer(0, 0, i, baseData.GetDetailLayer(x * data[arraypos].detailWidth, y * data[arraypos].detailHeight, data[arraypos].detailWidth, data[arraypos].detailHeight, layers[i]));

                System.Array.Clear(layers, 0, layers.Length);

                if( CTerrainConfig.isCopyAllTrees )
                {
                    data[arraypos].treePrototypes = _BackTerrain.treeProtos;
                }

                data[arraypos].wavingGrassStrength  = _BackTerrain.grassStrength;
                data[arraypos].wavingGrassAmount    = _BackTerrain.grassAmount;
                data[arraypos].wavingGrassSpeed     = _BackTerrain.grassSpeed;
                data[arraypos].wavingGrassTint      = _BackTerrain.grassTint;

                float[,] height  = baseData.GetHeights( x * (data[arraypos].heightmapWidth - 1), y * (data[arraypos].heightmapHeight - 1), data[arraypos].heightmapWidth, data[arraypos].heightmapHeight );
                data[arraypos].SetHeights(0, 0, height);
                float[,,] map = new float[_BackTerrain.newAlphaMapResolution, _BackTerrain.newAlphaMapResolution, _BackTerrain.splatProtos.Length];
                map = baseData.GetAlphamaps(x * data[arraypos].alphamapWidth, y * data[arraypos].alphamapHeight, data[arraypos].alphamapWidth, data[arraypos].alphamapHeight);
                data[arraypos].SetAlphamaps(0, 0, map);

                terrainGameObjects[arraypos].GetComponent<TerrainCollider>().terrainData = data[arraypos];
                terrainGameObjects[arraypos].transform.position = new Vector3(x * _BackTerrain.newWidth + _BackTerrain.xPos, _BackTerrain.yPos, y * _BackTerrain.newLength + _BackTerrain.zPos);

                arraypos++;
                _progress += progressScale;
                EditorUtility.DisplayProgressBar("Progress", "Generating Terrain Patch", _progress);

            }
        }

        for( int y = 0; y < terrains.Length; y++ )
        {
            terrains[y].treeDistance            = _BackTerrain.treeDistance;
            terrains[y].treeBillboardDistance   = _BackTerrain.treeBillboardDistance;
            terrains[y].treeCrossFadeLength     = _BackTerrain.treeCrossFadeLength;
            terrains[y].treeMaximumFullLODCount = _BackTerrain.treeMaximumFullLODCount;
            terrains[y].detailObjectDistance    = _BackTerrain.detailObjectDistance;
            terrains[y].detailObjectDensity     = _BackTerrain.detailObjectDensity;
            terrains[y].heightmapPixelError     = _BackTerrain.heightmapPixelError;
            terrains[y].heightmapMaximumLOD     = _BackTerrain.heightmapMaximumLOD;
            terrains[y].basemapDistance         = _BackTerrain.basemapDistance;
            terrains[y].lightmapIndex           = _BackTerrain.lightmapIndex;
            terrains[y].castShadows             = _BackTerrain.castShadows;
            terrains[y].editorRenderFlags       = _BackTerrain.editorRenderFlags;
        }

        if( !CTerrainConfig.isCopyAllTrees )
        {
            int[,] treeTypes     = new int[CTerrainConfig.terrainsLong, _BackTerrain.treeProtos.Length];

            for( int i = 0; i < _BackTerrain.treeInst.Length; i++ )
            {
                
                Vector3 a        = new Vector3(_BackTerrain.oldWidth, 1, _BackTerrain.oldLenght);
                Vector3 b        = new Vector3(_BackTerrain.treeInst[i].position.x, _BackTerrain.treeInst[i].position.y, _BackTerrain.treeInst[i].position.z);
                Vector3 origPos2 = Vector3.Scale( a, b );

                int column2      = Mathf.FloorToInt(origPos2.x / _BackTerrain.newWidth);
                int row2         = Mathf.FloorToInt(origPos2.z / _BackTerrain.newLength );
                treeTypes[(row2 * CTerrainConfig.terrainsWide) + column2,_BackTerrain.treeInst[i].prototypeIndex] = 1;
            }

            for( int i = 0; i < CTerrainConfig.terrainsWide * CTerrainConfig.terrainsLong; i++ )
            {
                int numOfPrototypes = 0;
                for( int y = 0; y < _BackTerrain.treeProtos.Length; y++ )
                    if (treeTypes[i, y] == i)
                        numOfPrototypes++;
                

                TreePrototype[] tempPrototypes = new TreePrototype[numOfPrototypes];
                int tempIndex = 0;
                for( int y = 0; y < _BackTerrain.treeProtos.Length; y++ )
                    if( treeTypes[i, y] == 1 )
                    {
                        tempPrototypes[tempIndex] = _BackTerrain.treeProtos[y];
                        treeTypes[i,y]            = tempIndex;
                        tempIndex++;
                    }
                
                data[i].treePrototypes = tempPrototypes;
            }
        }

        for (int i = 0; i < _BackTerrain.treeInst.Length; i++)
        {
            Vector3 a               = new Vector3(_BackTerrain.oldWidth, 1, _BackTerrain.oldHeight);
            Vector3 b               = new Vector3(_BackTerrain.treeInst[i].position.x, _BackTerrain.treeInst[i].position.y, _BackTerrain.treeInst[i].position.z);
            Vector3 origPos         = Vector3.Scale(a, b);

            int column              = Mathf.FloorToInt(origPos.x / _BackTerrain.newWidth);
            int row                 = Mathf.FloorToInt(origPos.z / _BackTerrain.newLength);

            Vector3 tempVect        = new Vector3((origPos.x - (_BackTerrain.newWidth * column)) / _BackTerrain.newWidth, origPos.y, (origPos.z - (_BackTerrain.newLength * row)) / _BackTerrain.newLength);
            TreeInstance tempInst   = new TreeInstance();

            tempInst.position       = tempVect;
            tempInst.widthScale     = _BackTerrain.treeInst[i].widthScale;
            tempInst.heightScale    = _BackTerrain.treeInst[i].heightScale;
            tempInst.color          = _BackTerrain.treeInst[i].color;
            tempInst.lightmapColor  = _BackTerrain.treeInst[i].lightmapColor;

            if (CTerrainConfig.isCopyAllTrees)
            {
                tempInst.prototypeIndex = _BackTerrain.treeInst[i].prototypeIndex;
            }
            
            terrains[(row * CTerrainConfig.terrainsWide) + column].AddTreeInstance(tempInst);
        }

        for (int i = 0; i < CTerrainConfig.terrainsWide * CTerrainConfig.terrainsLong; i++)
            data[i].RefreshPrototypes();
    }


    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 平滑地块的边缘
    /// </summary>
    /// --------------------------------------------------------------------------
    public void BlendPatchsEdges()
    {
        int alphaWidth      = data[0].alphamapWidth;
        int alphaHeight     = data[0].alphamapHeight;
        int numOfSplats     = data[0].splatPrototypes.Length;

        float avg;
        if( CTerrainConfig.terrainsWide > 1 && CTerrainConfig.terrainsLong == 1 )
        {
            for( int x = 0; x < CTerrainConfig.terrainsWide - 1; x++ )
            {
                float[,,] mapLeft   = data[x].GetAlphamaps( 0, 0, alphaWidth, alphaHeight );
                float[,,] mapRight  = data[x + 1].GetAlphamaps(0, 0, alphaWidth, alphaHeight);

                for( int i = 0; i < alphaHeight; i++ )
                    for( int y = 0; y < numOfSplats; y++ )
                    {
                        avg                           = (mapLeft[i, alphaWidth - 1, y] + mapRight[i, 0, y]) / 2f;
                        mapLeft[i, alphaWidth - 1, y] = avg;
                        mapRight[i, 0, y]             = avg;
                    }

                data[x].SetAlphamaps(0, 0, mapLeft);
                data[x + 1].SetAlphamaps(0, 0, mapRight);
            }
        }

        else if ( CTerrainConfig.terrainsLong > 1 && CTerrainConfig.terrainsWide == 1 )
        {
            for( int x = 0; x < CTerrainConfig.terrainsLong; x++ )
            {
                float[,,] mapBottom = data[x].GetAlphamaps(0, 0, alphaWidth, alphaHeight);
                float[, ,] mapTop = data[x + 1].GetAlphamaps(0, 0, alphaWidth, alphaHeight);

                for( int i = 0; i < alphaWidth; i++ )
                    for( int y = 0; y < numOfSplats; y++ )
                    {
                        avg = (mapBottom[alphaHeight - 1, i, y] + mapTop[0, i, y]) / 2f;

                        mapBottom[alphaHeight - 1, i, y] = avg;
                        mapTop[i, i, y]                  = avg;
                    }

                data[x].SetAlphamaps(0, 0, mapBottom);
                data[x + 1].SetAlphamaps(0, 0, mapTop);
            }
        }

        else if( CTerrainConfig.terrainsWide > 1 && CTerrainConfig.terrainsLong > 1 )
        {
            int arraypos = -2;
            for( int z = 0; z < CTerrainConfig.terrainsLong - 1; z++ )
            {
                arraypos++;
                for( int x = 0; x < CTerrainConfig.terrainsWide - 1; x++ )
                {
                    arraypos++;
                    float[, ,] mapBLeft     = data[arraypos].GetAlphamaps(0, 0, alphaWidth, alphaHeight);
                    float[, ,] mapBRight    = data[arraypos + 1].GetAlphamaps(0, 0, alphaWidth, alphaHeight);
                    float[, ,] mapTLeft     = data[arraypos + CTerrainConfig.terrainsWide].GetAlphamaps(0, 0, alphaWidth, alphaHeight);
                    float[, ,] mapTRight    = data[arraypos + CTerrainConfig.terrainsWide + 1].GetAlphamaps(0, 0, alphaWidth, alphaHeight);

                    //Set the edge between the BRight and TRight
                    for (int i = 1; i < alphaWidth - 1; i++)
                        for (int y = 0; y < numOfSplats; y++)
                        {
                            avg = (mapBRight[alphaHeight - 1, i, y] + mapTRight[0, i, y]) / 2f;
                            mapBRight[alphaHeight - 1, i, y] = avg;
                            mapTRight[0, i, y] = avg;
                        }

                    //Set the edge between the top left and top right terrains
                    for (int i = 1; i < alphaHeight - 1; i++)
                        for (int y = 0; y < numOfSplats; y++)
                        {
                            avg = (mapTLeft[i, alphaWidth - 1, y] + mapTRight[i, 0, y]) / 2f;
                            mapTLeft[i, alphaWidth - 1, y] = avg;
                            mapTRight[i, 0, y] = avg;
                        }

                    //Set the corner between the four terrains
                    for (int y = 0; y < numOfSplats; y++)
                    {
                        avg = (mapBLeft[alphaHeight - 1, alphaWidth - 1, y] + mapBRight[alphaHeight - 1, 0, y] + mapTLeft[0, alphaWidth - 1, y] + mapTRight[0, 0, y]) / 4f;
                        mapBLeft[alphaHeight - 1, alphaWidth - 1, y] = avg;
                        mapBRight[alphaHeight - 1, 0, y] = avg;
                        mapTLeft[0, alphaWidth - 1, y] = avg;
                        mapTRight[0, 0, y] = avg;
                    }

                    //If the terrain is on the bottom row
                    if (z == 0)
                    {
                        //Set the edge between the bottom left and bottom right terrains
                        for (int i = 1; i < alphaHeight - 1; i++)
                            for (int y = 0; y < numOfSplats; y++)
                            {
                                avg = (mapBLeft[i, alphaWidth - 1, y] + mapBRight[i, 0, y]) / 2f;
                                mapBLeft[i, alphaWidth - 1, y] = avg;
                                mapBRight[i, 0, y] = avg;
                            }

                        //Set the bottom most spot point between BLeft and BRight
                        for (int y = 0; y < numOfSplats; y++)
                        {
                            avg = (mapBLeft[0, alphaWidth - 1, y] + mapBRight[0, 0, y]) / 2f;
                            mapBLeft[0, alphaWidth - 1, y] = avg;
                            mapBRight[0, 0, y] = avg;
                        }
                    }


                    //If the terrain is also in the first column
                    if (x == 0)
                    {
                        //Set the edge between the BLeft and TLeft
                        for ( int i = 1; i < alphaWidth - 1; i++)
                            for (int y = 0; y < numOfSplats; y++)
                            {
                                avg = (mapBLeft[alphaHeight - 1, i, y] + mapTLeft[0, i, y]) / 2f;
                                mapBLeft[alphaHeight - 1, i, y] = avg;
                                mapTLeft[0, i, y] = avg;
                            }


                        //Set the left most point between BLeft and TLeft
                        for (int y = 0; y < numOfSplats; y++)
                        {
                            avg = (mapBLeft[alphaHeight - 1, 0, y] + mapTLeft[0, 0, y]) / 2f;
                            mapBLeft[alphaHeight - 1, 0, y] = avg;
                            mapTLeft[0, 0, y] = avg;
                        }


                    }

                    //if this is the second to last terrain in the row
                    if (x == CTerrainConfig.terrainsWide - 2)
                        for (int y = 0; y < numOfSplats; y++)
                        {
                            //Set the right most point between the Bright map and Tright map
                            avg = (mapBRight[alphaHeight - 1, alphaWidth - 1, y] + mapTRight[0, alphaWidth - 1, y]) / 2f;
                            mapBRight[alphaHeight - 1, alphaWidth - 1, y] = avg;
                            mapTRight[0, alphaWidth - 1, y] = avg;
                        }
                    //if this is the second to last terrain in the column
                    if (z == CTerrainConfig.terrainsLong - 2)
                        for (int y = 0; y < numOfSplats; y++)
                        {
                            //Set the right most point between the TLeft map and TRight map
                            avg = (mapTLeft[alphaHeight - 1, alphaWidth - 1, y] + mapTRight[alphaHeight - 1, 0, y]) / 2f;
                            mapTLeft[alphaHeight - 1, alphaWidth - 1, y] = avg;
                            mapTRight[alphaHeight - 1, 0, y] = avg;
                        }

                    data[arraypos].SetAlphamaps(0, 0, mapBLeft);
                    data[arraypos + 1].SetAlphamaps(0, 0, mapBRight);
                    data[arraypos + CTerrainConfig.terrainsWide].SetAlphamaps(0, 0, mapTLeft);
                    data[arraypos + CTerrainConfig.terrainsWide + 1].SetAlphamaps(0, 0, mapTRight);
                }
            }
        }
    }


    /// --------------------------------------------------------------------------
    /// <summary>
    /// 设置各个地块的兄弟节点
    /// </summary>
    /// -------------------------------------------------------------------------
    public void SetPatchsNeighbors()
    {
        int arraypos     = 0;
        int terrainsWide = CTerrainConfig.terrainsWide;
        int terrainsLong = CTerrainConfig.terrainsLong;
        for( int y = 0; y < CTerrainConfig.terrainsLong; y++ )
        {
            for( int x = 0; x < CTerrainConfig.terrainsWide; x++ )
            {
                if (y == 0)
                {
                    if (x == 0)
                        terrains[arraypos].SetNeighbors(null, terrains[arraypos + terrainsWide], terrains[arraypos + 1], null);
                    else if (x == terrainsWide - 1)
                        terrains[arraypos].SetNeighbors(terrains[arraypos - 1], terrains[arraypos + terrainsWide], null, null);
                    else
                        terrains[arraypos].SetNeighbors(terrains[arraypos - 1], terrains[arraypos + terrainsWide], terrains[arraypos + 1], null);
                }
                else if (y == terrainsLong - 1)
                {
                    if (x == 0)
                        terrains[arraypos].SetNeighbors(null, null, terrains[arraypos + 1], terrains[arraypos - terrainsWide]);
                    else if (x == terrainsWide - 1)
                        terrains[arraypos].SetNeighbors(terrains[arraypos - 1], null, null, terrains[arraypos - terrainsWide]);
                    else
                        terrains[arraypos].SetNeighbors(terrains[arraypos - 1], null, terrains[arraypos + 1], terrains[arraypos - terrainsWide]);
                }
                else
                {
                    if (x == 0)
                        terrains[arraypos].SetNeighbors(null, terrains[arraypos + terrainsWide], terrains[arraypos + 1], terrains[arraypos - terrainsWide]);
                    else if (x == terrainsWide - 1)
                        terrains[arraypos].SetNeighbors(terrains[arraypos - 1], terrains[arraypos + terrainsWide], null, terrains[arraypos - terrainsWide]);
                    else
                        terrains[arraypos].SetNeighbors(terrains[arraypos - 1], terrains[arraypos + terrainsWide], terrains[arraypos + 1], terrains[arraypos - terrainsWide]);
                }

                //Increment arrayPos
                arraypos++;
            }
        }
    }

    /// --------------------------------------------------------------------------
    /// <summary>
    /// 每个地块生成对应的四叉树
    /// </summary>
    /// --------------------------------------------------------------------------
    public void GenPatchQuadTree()
    {
        _progress = 0.0f;
        EditorUtility.DisplayProgressBar("Progress", "Gen Patchs QuadTrees", _progress);

        int nSize           = CTerrainConfig.terrainsLong * CTerrainConfig.terrainsWide;
        progressScale       = .9f / nSize;
        //CQaudTreeDataEditor[] Patchs = new CQaudTreeDataEditor[nSize];
        int arraypos = 0;
     
        for (int y = 0; y < CTerrainConfig.terrainsLong; y++)
        {
            for (int x = 0; x < CTerrainConfig.terrainsWide; x++)
            {
               
                //Patchs[arraypos].GenQuadTreeXML( y, x );
                arraypos++;
            }
        }
    }
}



