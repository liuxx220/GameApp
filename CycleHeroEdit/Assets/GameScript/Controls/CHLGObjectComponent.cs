/************************************************************************************
 *          
 *      file name :  CHLGObjectComponent.cs
 *      author    :  ljp
 *      desc      :  物体高光和边缘轮廓组件，挂在具体的GameObject上
 *      log       :  creat by ljp [2016-4-10 ]
 * 
************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





public class CHLGObjectComponent : MonoBehaviour
{

    #region Editable Fields

    public static int       highlightingLayer   = 7;
    private static float    constantOnSpeed     = 4.5f;
    private static float    constantOffSpeed    = 4f;
    private static float    transpaentCutoff    = 0.5f;
    #endregion


    private const float doublePI                = 2f * Mathf.PI;
    private int[]       layersCache;
    private bool        materialsIsDirty = false;
    private bool        currentState = false;
    private Color       currentColor;
    private bool        transitionActive = false;
    private float       transitionValue = 0f;
    private bool        zWrite = false;
    private bool        once = false;
    private Color       onceColor;
    private Color       constantColor = Color.yellow;


    private List<CRenderCache> highlightableRenderers;

    //////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 渲染节点缓存池子
    /// </summary>
    private class CRenderCache
    {
        public Renderer    rendererCached;
        public GameObject  go;
        private Material[] sourceMaterials;
        private Material[] replacementMaterials;
        private List<int>  transparentMaterialIndex;

        public CRenderCache( Renderer rend, Material[] mats, Material shareOpaqueMat, bool writeDepth )
        {
            rendererCached = rend;
            go             = rend.gameObject;
            sourceMaterials= mats;
            replacementMaterials = new Material[mats.Length];
            transparentMaterialIndex = new List<int>();

            for( int i = 0; i < mats.Length; i++ )
            {
                Material source = mats[i];
                if( source == null )
                    continue;

                string tag = source.GetTag("RenderType", true);
                if( tag == "Transparent" || tag == "TransparentCutout")
                {
                    Material replacement = new Material(writeDepth ? transparentZShader : transparentShader);
                    if( source.HasProperty( "_MainTex" ))
                    {
                        replacement.SetTexture("_MainTex", source.mainTexture);
                        replacement.SetTextureOffset("_MainTex", source.mainTextureOffset);
                        replacement.SetTextureScale("_MainTex", source.mainTextureScale);
                    }

                    replacement.SetFloat("_Cutoff", source.HasProperty("_Cutoff") ? source.GetFloat("_Cutoff") : transpaentCutoff);
                    replacementMaterials[i] = replacement;
                    transparentMaterialIndex.Add(i);
                }
                else
                {
                    replacementMaterials[i] = shareOpaqueMat;
                }
            }
        }


        public void SetState( bool state )
        {
            rendererCached.sharedMaterials = state ? replacementMaterials : sourceMaterials;
        }

        public void SetColorFoTransparent( Color clr )
        {
            for( int i = 0; i < transparentMaterialIndex.Count; i++ )
            {
                replacementMaterials[transparentMaterialIndex[i]].SetColor("_Outline", clr);
            }
        }
    }

    private Material highlightingMaterial
    {
        get
        {
            return zWrite ? opaqueZMaterial : opaqueMaterial;
        }
    }


    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 不透明材质
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private Material _opaqueMaterial;
    private Material opaqueMaterial
    {
        get
        {
            if( _opaqueMaterial == null )
            {
                _opaqueMaterial = new Material(opaqueShader);
                _opaqueMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return _opaqueMaterial;
        }
    }


    /// -------------------------------------------------------------------------------------------
    /// <summary>
    /// 不透明材质 + 深度写入
    /// </summary>
    /// -------------------------------------------------------------------------------------------
    private Material _opaqueZMaterial;
    private Material opaqueZMaterial
    {
        get
        {
            if( _opaqueZMaterial == null )
            {
                _opaqueZMaterial = new Material(opaqueZShader);
                _opaqueZMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return _opaqueZMaterial;
        }
    }


    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 不透明处理的 shader
    /// </summary>
    /// -----------------------------------------------------------------------------------------
    private static Shader _opaqueShader;
    private static Shader opaqueShader
    {
        get
        {
            if( _opaqueShader == null )
            {
                _opaqueShader = Shader.Find("Hidden/Highlighted/StencilOpaque");
            }
            return _opaqueShader;
        }
    }

    ///-------------------------------------------------------------------------------------------
    /// <summary>
    /// 透明处理的 shader
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private static Shader _transparentShader;
    private static Shader transparentShader
    {
        get
        {
            if( _transparentShader == null )
            {
                _transparentShader = Shader.Find("");
            }
            return _transparentShader;
        }
    }


    /// --------------------------------------------------------------------------------------------
    /// <summary>
    /// 不透明 + 深度写入 shader 
    /// </summary>
    /// --------------------------------------------------------------------------------------------
    private static Shader _opaqueZShader;
    private static Shader opaqueZShader
    {
        get
        {
            if( _opaqueZShader == null )
            {
                _opaqueZShader = Shader.Find("Hidden/Highlighted/StencilTransparent");
            }
            return _opaqueZShader;
        }
    }


    /// -------------------------------------------------------------------------------------------
    /// <summary>
    /// 透明+深度写入的shader
    /// </summary>
    /// ------------------------------------------------------------------------------------------
    private static Shader _transparentZShader;
    private static Shader transparentZShader
    {
        get
        {
            if( _transparentZShader == null )
            {
                _transparentZShader = Shader.Find("");
            }
            return _transparentZShader;
        }
    }

    /// -------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ------------------------------------------------------------------------------------
    private void OnEnable()
    {
        StartCoroutine(EndOfFrame());
        CHLGEffectComponent.highlightingEvent += UpdateEventHandler;
    }


    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ------------------------------------------------------------------------------------
    private void OnDisable()
    {
        StopAllCoroutines();
        CHLGEffectComponent.highlightingEvent -= UpdateEventHandler;

        if( highlightableRenderers != null )
        {
            highlightableRenderers.Clear();
        }

        layersCache         = null;
        materialsIsDirty    = true;
        currentState        = false;
        currentColor        = Color.clear;
        transitionActive    = false;
        transitionValue     = 0f;
        once                = false;
        zWrite              = false;

        if( _opaqueMaterial )
            DestroyImmediate(_opaqueMaterial);
        if( _opaqueZMaterial )
            DestroyImmediate(_opaqueZMaterial);
    }


    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 事件更新
    /// </summary>
    /// -----------------------------------------------------------------------------------
    private void UpdateEventHandler( bool trigger, bool writeDepth )
    {
        if( trigger )
        {
            if( zWrite != writeDepth )
            {
                materialsIsDirty = true;
            }

            if( materialsIsDirty )
            {
                InitMaterials(writeDepth);
            }

            currentState = (once || transitionActive);
            if( currentState )
            {
                UpdateColors();
                PerformTransition();

                if( highlightableRenderers != null )
                {
                    layersCache = new int[highlightableRenderers.Count];
                    for( int i = 0; i < highlightableRenderers.Count; i++ )
                    {
                        GameObject go  = highlightableRenderers[i].go;
                        layersCache[i] = go.layer;
                        go.layer       = highlightingLayer;
                        highlightableRenderers[i].SetState(true);
                    }
                }
            }
        }
        else
        {
            if( currentState && highlightableRenderers != null )
            {
                for( int i = 0; i < highlightableRenderers.Count; i++ )
                {
                    highlightableRenderers[i].go.layer = layersCache[i];
                    highlightableRenderers[i].SetState(false);
                }
            }
        }
    }


    /// -----------------------------------------------------------------------------------
    /// <summary>
    /// 等待帧结束
    /// </summary>
    /// -----------------------------------------------------------------------------------
    IEnumerator EndOfFrame()
    {
        while( enabled )
        {
            yield return new WaitForEndOfFrame();
            once = false;
        }
    }


    /// ----------------------------------------------------------------------------
    /// <summary>
    /// 出事化使用的材质
    /// </summary>
    /// ------------------------------------------------------------------------
    private void InitMaterials( bool writeDepth )
    {
        currentState = false;
        zWrite       = writeDepth;

        highlightableRenderers = new List<CRenderCache>();

        MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer>();
        if( mr.Length > 0 )
        {
            CacheRenderers(mr);
        }

        SkinnedMeshRenderer[] smr = GetComponentsInChildren<SkinnedMeshRenderer>();
        if( smr.Length > 0 )
        {
            CacheRenderers(smr);
        }

        currentState        = false;
        materialsIsDirty    = false;
        currentColor        = Color.clear;
    }


    private void CacheRenderers( Renderer[] renders )
    {
        for( int i = 0; i < renders.Length; i++ )
        {
            Material[] mt = renders[i].sharedMaterials;
            if( mt != null )
            {
                highlightableRenderers.Add(new CRenderCache(renders[i], mt, highlightingMaterial, zWrite));
            }
        }
    }


    /// --------------------------------------------------------------------------------
    /// <summary>
    /// 更新轮廓的颜色 
    /// </summary>
    /// ---------------------------------------------------------------------------------
    private void UpdateColors()
    {

        if( !currentState )
        {
            return;
        }

        if( once )
        {
            SetColor(onceColor);
            return;
        }

        if( transitionActive )
        {
            Color c = new Color( constantColor.r, constantColor.g, constantColor.g, constantColor.a * transitionValue );
            SetColor(c);
        }
        else
        {
            SetColor(constantColor);
        }
    }


    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 设置材质内变量
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    private void SetColor( Color c )
    {
        if( currentColor == c )
        {
            return;
        }

        if( zWrite )
        {
            opaqueZMaterial.SetColor("_Outline", c);
        }
        else
        {
            opaqueMaterial.SetColor("_Outline", c);
        }

        for (int i = 0; i < highlightableRenderers.Count;  i++ )
        {
            highlightableRenderers[i].SetColorFoTransparent(c);
        }

        currentColor = c;
    }


    /// -------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 更新透明度
    /// </summary>
    /// -------------------------------------------------------------------------------------------------------------
    private void PerformTransition()
    {

        if( !transitionActive )
        {
            return;
        }
    }


    /// ------------------------------------------------------------------------------------------------
    /// <summary>
    /// 打开和关闭绘制轮廓效果
    /// </summary>
    /// -----------------------------------------------------------------------------------------------
    public void On()
    {
        once = true;
    }

    public void Off()
    {
        once = false;
        transitionActive = false;
        transitionValue  = 0f;
    }

    public void On( Color C )
    {
        onceColor = C;
        On();
    }
}
