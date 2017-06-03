using UnityEngine;
using System.Collections;
using System.Collections.Generic;




//public class LoadLevelFrame : MonoBehaviour 
//{

//    private UISlider            _Slider = null;
	

//    public override bool ReloadUI()
//    {
//        base.ReloadUI();

//        _Slider     = transform.Find("Anchor/Progress Bar").GetComponent<UISlider>();
//        return true;
//    }

   
//    /// ---------------------------------------------------------------------------
//    /// <summary>
//    /// UI的心跳逻辑
//    /// </summary>
//    /// --------------------------------------------------------------------------
//    public override void Update () 
//    {
//        if( LoadLevelMgr.Instance._asyncLoader != null )
//        {
//            float toProgress = LoadLevelMgr.Instance._asyncLoader.progress * 100;
//            SetLoadingPercentage( toProgress );
//        }
       
//    }

//    void SetLoadingPercentage(float fValue)
//    {
//        if (_Slider != null)
//        {
//            _Slider.value = fValue / 100.0f;
//            Common.DEBUG_MSG("Load progress is " + _Slider.value.ToString());
//        }
//    }

//    /// ---------------------------------------------------------------------------
//    /// <summary>
//    /// 释放本UIFrame 所用到的资源
//    /// </summary>
//    /// --------------------------------------------------------------------------
//    public override void Destroy()
//    {
//        _Slider     = null;
//        base.Destroy();
//    }

//    /// ----------------------------------------------------------------------------
//    /// <summary>
//    /// UI资源加载完成
//    /// </summary>
//    /// ----------------------------------------------------------------------------
//    public override void OnAsyncLoaded()
//    {
//        base.OnAsyncLoaded();
//        if (LoadLevelMgr.Instance != null)
//        {
//            LoadLevelMgr.Instance.StartLoadLevel();
//        }
//    }
//}
