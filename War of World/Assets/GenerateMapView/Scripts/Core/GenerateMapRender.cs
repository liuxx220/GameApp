//----------------------------------------------
//            NJG MiniMap (NGUI)
// Copyright � 2014 Ninjutsu Games LTD.
//----------------------------------------------

using System.Collections;
using UnityEngine;
using MapSpace;





[ExecuteInEditMode]
public class GenerateMapRender : MonoBehaviour
{


    static GenerateMapRender        mInstance;
    static public GenerateMapRender instance
	{
		get
		{
            if (mInstance == null)
			{
                mInstance = GameObject.FindObjectOfType(typeof(GenerateMapRender)) as GenerateMapRender;
                if (mInstance == null)
				{
					GameObject go       = new GameObject("_miniMapRenderer");
                    go.transform.parent = GenerateMapInfo.instance.transform;
					go.layer            = LayerMask.NameToLayer("TransparentFX");
                    mInstance           = go.AddComponent<GenerateMapRender>();					
				}
			}
            return mInstance;
		}
	}

	/// <summary>
	/// Cached transform for speed.
	/// </summary>

    public Transform cachedTransform { get { return transform; } }


	Vector2             mSize;
	bool                canRender = true;
	bool                mGenerated = false;
	bool                mWarning = false;
	bool                mReaded = false;
	bool                mApplied = false;
	float               lastRender = 0;
    GenerateMapInfo     map;

	void Awake()
	{
        map = GenerateMapInfo.instance;
		if (map == null)
		{
			Debug.LogWarning("Can't render map photo. NJGMiniMap instance not found.");
			NGUITools.Destroy(gameObject);
			return;
		}		

		if (gameObject.GetComponent<Camera>() == null) 
			gameObject.AddComponent<Camera>();

		GetComponent<Camera>().useOcclusionCulling = false;

		Render();
	}

	void Start()
	{
        if (map.boundLayers.value == 0)
        {
            Debug.LogWarning("Can't render map photo. You have not choosen any layer for bounds calculation. Go to the NJGMiniMap inspector.", map);
            NGUITools.DestroyImmediate(gameObject);
            return;
        }
		if (map.renderLayers.value == 0)
		{
			Debug.LogWarning("Can't render map photo. You have not choosen any layer for rendering. Go to the NJGMiniMap inspector.", map);
            NGUITools.DestroyImmediate(gameObject);
			return;
		}

		ConfigCamera();
		if(!Application.isPlaying) Render();
	}

	void ConfigCamera()
	{
		map.UpdateBounds();
		Bounds bounds = map.bounds;
		GetComponent<Camera>().depth            = -100;
		GetComponent<Camera>().backgroundColor  = map.cameraBackgroundColor;
		GetComponent<Camera>().cullingMask      = map.renderLayers;
		GetComponent<Camera>().clearFlags       = (CameraClearFlags)map.cameraClearFlags;
		GetComponent<Camera>().orthographic     = true;

		float z = 0;
		GetComponent<Camera>().farClipPlane     = bounds.size.y * 1.1f;
		z = bounds.extents.z;
		GetComponent<Camera>().aspect = bounds.size.x / bounds.size.z;
		
		GetComponent<Camera>().farClipPlane     = GetComponent<Camera>().farClipPlane * 5f;
		GetComponent<Camera>().orthographicSize = z;


        cachedTransform.eulerAngles = new Vector3(90f, 0, 0);
        cachedTransform.localScale = Vector3.one;
        cachedTransform.position = new Vector3(bounds.max.x - bounds.extents.x, bounds.size.y * 2f, bounds.center.z);
        GetComponent<Camera>().enabled = true;
	}

	IEnumerator OnPostRender()
	{
		// Can't re-generate the texture map is makeNoLongReadable flag is turned on.
		if (mGenerated && Application.isPlaying && !mWarning)
		{
			mWarning = true;
			Debug.LogWarning("Can't Re-generate the map texture since 'optimize' is activated");
			canRender = false;
		}
		else
		{
			if (canRender)
			{
				if (map.mapTexture == null)
				{
					mSize                       = map.mapSize;
                    if (map.mapResolution == GenerateMapInfo.Resolution.Double) mSize = map.mapSize * 2;
					map.mapTexture              = new Texture2D((int)mSize.x, (int)mSize.y, (TextureFormat)map.textureFormat,false);
                    map.mapTexture.name         = "_miniMapRenderer";
					map.mapTexture.filterMode   = map.mapFilterMode;
					map.mapTexture.wrapMode     = map.mapWrapMode;
				}

				if (!mReaded || !Application.isPlaying)
				{					
					if (map.generateMapTexture && canRender)
					{
                        if (map.mapResolution == GenerateMapInfo.Resolution.Double)
						{

							Bounds bounds = map.bounds;
							//bounds.Expand(new Vector3(-bounds.extents.x, 0f, -bounds.extents.z));
							for (int i = 0; i < 4; i++)
							{
								switch (i)
								{
									case 0:
								
										map.mapTexture.ReadPixels(new Rect(0f, 0f, map.mapSize.x, map.mapSize.y), 0, 0, false);
										cachedTransform.position = new Vector3(bounds.center.x - bounds.extents.x, (bounds.center.y + bounds.extents.y) + 1f, bounds.center.z - bounds.extents.z);
										break;

									case 1:
										cachedTransform.position = new Vector3(bounds.center.x + bounds.extents.x, (bounds.center.y + bounds.extents.y) + 1f, bounds.center.z - bounds.extents.z);
                                        map.mapTexture.ReadPixels(new Rect(0f, 0f, map.mapSize.x, map.mapSize.y), (int)map.mapSize.x, 0, false);
										break;

									case 2:
										cachedTransform.position = new Vector3(bounds.center.x + bounds.extents.x, (bounds.center.y + bounds.extents.y) + 1f, bounds.center.z + bounds.extents.z);
                                        map.mapTexture.ReadPixels(new Rect(0f, 0f, map.mapSize.x, map.mapSize.y), (int)map.mapSize.x, (int)map.mapSize.y, false);
										break;

									case 3:
										cachedTransform.position = new Vector3(bounds.center.x - bounds.extents.x, (bounds.center.y + bounds.extents.y) + 1f, bounds.center.z + bounds.extents.z);
										mReaded = true;
                                        map.mapTexture.ReadPixels(new Rect(0f, 0f, map.mapSize.x, (map.mapSize.y)), 0, (int)map.mapSize.y, false);
										break;
								}
							}
						}
						else
						{
							//yield return new WaitForEndOfFrame();
							mReaded = true;
                            map.mapTexture.ReadPixels(new Rect(0f, 0f, map.mapSize.x, map.mapSize.y), 0, 0, false);
						}
					}
					else
					{
						if (map.userMapTexture != null)
                            map.userMapTexture.ReadPixels(new Rect(0f, 0f, map.mapSize.x, map.mapSize.y), 0, 0, false);
					}
				}
				
				yield return new WaitForEndOfFrame();
				if (GetComponent<Camera>().enabled && Application.isPlaying) GetComponent<Camera>().enabled = false;				
			}
		}
		lastRender = Time.time + 1f;
	}

	/// <summary>
	/// Redraw the map's texture.
	/// </summary>
	
	public void Render()
	{
		if (Time.time >= lastRender)
		{			
			if (Application.isPlaying) lastRender = Time.time + 1f;
			mReaded     = false;
			mApplied    = false;			
			mGenerated  = false;
			mWarning    = false;

			canRender = true;

			if (map.mapSize.x == 0 || map.mapSize.y == 0)
			{
				map.mapSize = new Vector2(Screen.width, Screen.height);
			}

			if (map.generateMapTexture)
			{
				if (map.userMapTexture != null)
				{
                    NGUITools.Destroy(map.userMapTexture);
					map.userMapTexture = null;
				}

				if (map.mapTexture == null)
				{
					mSize = map.mapSize;
                    if (map.mapResolution == GenerateMapInfo.Resolution.Double) 
                        mSize = map.mapSize * 2;
					map.mapTexture = new Texture2D((int)mSize.x, (int)mSize.y, (TextureFormat)map.textureFormat, false);
                    map.mapTexture.name = "_miniMapRenderer";
					map.mapTexture.filterMode = map.mapFilterMode;
					map.mapTexture.wrapMode = map.mapWrapMode;
				}
			}
			else if (!Application.isPlaying)
			{
				if (map.mapTexture != null)
				{
                    NGUITools.DestroyImmediate(map.mapTexture);
					map.mapTexture = null;
				}

				map.userMapTexture              = new Texture2D((int)map.mapSize.x, (int)map.mapSize.y, (TextureFormat)map.textureFormat, false);
				map.userMapTexture.name         = "_miniMapRenderer";
				map.userMapTexture.filterMode   = map.mapFilterMode;
				map.userMapTexture.wrapMode     = map.mapWrapMode;
			}

			
			ConfigCamera();
			GetComponent<Camera>().enabled = true;
		}
	}
}