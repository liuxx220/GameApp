//----------------------------------------------
//            NJG MiniMap (NGUI)
// Copyright ?2014 Ninjutsu Games LTD.
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;






namespace MapSpace
{
    public class GenerateMapInfo : MonoBehaviour
    {
        public enum SettingsScreen
        {
            General,
            Icons,
            Zones,
            _LastDoNotUse
        }

        public enum Resolution
        {
            Normal,
            Double
        }

        public enum MiniTextureFormat
        {
            ARGB32  = TextureFormat.ARGB32,
            RGB24   = TextureFormat.RGB24
        }

        static GenerateMapInfo          mInstance;
        static public GenerateMapInfo   instance { get { if (mInstance == null) mInstance = GameObject.FindObjectOfType(typeof(GenerateMapInfo)) as GenerateMapInfo; return mInstance; } }


        /// <summary>
        /// Draw bounds on the scene view.
        /// </summary>
        [SerializeField]
        public bool         showBounds = true;

        /// <summary>
        /// If true the instance of NJGMap will be persistence.
        /// </summary>
        public bool         dontDestroy;

        /// <summary>
        /// Map resolution.
        /// </summary>
        [SerializeField]
        public Resolution   mapResolution = Resolution.Normal;

        /// <summary>
        /// Which layers is the map going to render.
        /// </summary>
        [SerializeField]
        public LayerMask    renderLayers = -1;

        [SerializeField]
        public LayerMask    boundLayers = 1;
        public int          layer = -1;

        /// <summary>
        /// Which layers are going to be used for bounds calculation.
        /// </summary>
        public SettingsScreen   screen = SettingsScreen.General;

        /// <summary>
        /// World bounds.
        /// </summary>
        [SerializeField]
        public Bounds           bounds;
        private Bounds          mBounds;

        Vector2 mSize           = new Vector2(1024, 1024);
        [SerializeField]
        public Vector2 mapSize
        {
            set { mSize = value; }
            get
            {
                if (Application.isPlaying)
                {
                    mSize.x = Screen.width;
                    mSize.y = Screen.height;
                }
                return mSize;
            }
        }
        /// <summary>
        /// Texture of the map.
        /// </summary>
        public Texture2D            mapTexture;
            
        /// <summary>
        /// User defined texture of the map.
        /// </summary>
        public Texture2D            userMapTexture;

       
        public bool                 useTextureGenerated;

        [SerializeField]
        public FilterMode           mapFilterMode       = FilterMode.Bilinear;
        [SerializeField]
        public TextureWrapMode      mapWrapMode         = TextureWrapMode.Clamp;
        [SerializeField]
        public MiniTextureFormat    textureFormat       = MiniTextureFormat.ARGB32;
        [SerializeField]
        public CameraClearFlags     cameraClearFlags    = CameraClearFlags.Skybox;
        public Color                cameraBackgroundColor = Color.red;

        public bool                 generateMapTexture;
        public string               mapLevel = "Map Name";
        void Awake()
        {

        }


        void Start()
        {
            UpdateBounds();
        }


        public void GenerateMap()
        {
            if ((Application.isPlaying && generateMapTexture) || (!Application.isPlaying && !generateMapTexture))
            {
                GenerateMapRender.instance.Render();
            }
        }

        public static bool IsInRenderLayers(GameObject obj, LayerMask mask)
        {
            return (mask.value & (1 << obj.layer)) > 0;
        }

        public void UpdateBounds()
        {
            bool flag = false;
            Terrain[] mTerrains;
            mTerrains = FindObjectsOfType(typeof(Terrain)) as Terrain[];
            bool multiTerrain = mTerrains != null;
            if (multiTerrain) multiTerrain = mTerrains.Length > 1;
            if (multiTerrain)
            {
                for (int i = 0; i < mTerrains.Length; i++)
                {
                    Terrain t = mTerrains[i];
                    MeshRenderer mMeshRenderer = t.GetComponent<MeshRenderer>();

                    if( !flag )
                    {
                        mBounds = new Bounds();
                        flag = true;
                    }

                    if( mMeshRenderer != null )
                    {
                        mBounds.Encapsulate(mMeshRenderer.bounds);
                    }
                    else
                    {
                        TerrainCollider mTerrainCollider = t.GetComponent<TerrainCollider>();
                        if (mTerrainCollider != null)
                        {
                            mBounds.Encapsulate(mTerrainCollider.bounds);
                        }
                        else
                        {
                            Debug.LogError("Could not get measure bounds of terrain.", this);
                            return;
                        }
                    }
                }
            }
            else if( Terrain.activeTerrain != null )
            {
                Terrain t = Terrain.activeTerrain;
                MeshRenderer mMeshRenderer = t.GetComponent<MeshRenderer>();

                if (!flag)
                {
                    mBounds = new Bounds();
                    flag = true;
                }

                if (mMeshRenderer != null)
                {
                    mBounds.Encapsulate(mMeshRenderer.bounds);
                }
                else
                {
                    TerrainCollider mTerrainCollider = t.GetComponent<TerrainCollider>();
                    if (mTerrainCollider != null)
                    {
                        mBounds.Encapsulate(mTerrainCollider.bounds);
                    }
                    else
                    {
                        Debug.LogError("Could not get measure bounds of terrain.", this);
                        return;
                    }
                }
            }

            GameObject[] mGameObjects = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            if (mGameObjects != null)
            {
                for (int i = 0; i <  mGameObjects.Length; i++)
				{
					GameObject go = mGameObjects[i];
					if (go.layer == gameObject.layer)
						continue;

                    if (!IsInRenderLayers(go, boundLayers))
                        continue;

					if (!flag)
					{
						mBounds = new Bounds(go.transform.position, new Vector3(1f, 1f, 1f));
						flag = true;
					}
					Renderer renderer = go.GetComponent<Renderer>();
					if (renderer != null)
					{
						mBounds.Encapsulate(renderer.bounds);
					}
					else
					{
						Collider collider = go.GetComponent<Collider>();
						if (collider != null)
						{
							mBounds.Encapsulate(collider.bounds);
						}
					}
				}
            }

            if (!flag)
            {
                Debug.Log("Could not find terrain nor any other bounds in scene", this);
                mBounds = new Bounds(gameObject.transform.position, new Vector3(1f, 1f, 1f));
            }

            mBounds.Expand(new Vector3(0, 0, 0));

            if (mapResolution == Resolution.Double)
            {
              
            }

            // Set bounds
            bounds = mBounds;
        }
    }
}
