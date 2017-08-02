using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tanks.Utilities;
using Tanks.Data;





namespace Tanks
{
    public class HUDPlayerManager : CustomSingleton<HUDPlayerManager>
    {
     
        /// <summary>
        /// 缓存一下UI相机
        /// </summary>
        protected Camera        m_uiCamera = null;


        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 游戏中血条和冒血对象管理器
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void Init( )
        {
            m_uiCamera             = UICamera.currentCamera;
        }


        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 创建血条对象
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public HUDPlayer CreateHUDPlayerPrefab(Transform entity)
        {

            UnityEngine.Object obj = AssetManager.Get().GetResources("HUDPlayer");
            GameObject hudgameobj  = GameObject.Instantiate(obj) as GameObject;
            if (obj == null)
                Debug.LogError("obj = null");

            
            hudgameobj.transform.parent     = GetUICamera().transform;
            hudgameobj.transform.localScale = Vector3.one;
            HUDPlayer hud = hudgameobj.GetComponent<HUDPlayer>();
            if (hud != null)
                hud.Init(entity);
            return hud;
        }


        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 释放战斗资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void Destory()
        {
            m_uiCamera = null;
        }


        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 得到UI相机
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public Camera GetUICamera()
        {
            if (UICamera.currentCamera == null && m_uiCamera == null )
            {
                GameObject cameraobj = GameObject.Find("Camera");
                m_uiCamera           = cameraobj.GetComponent<Camera>();
                return m_uiCamera;
            }
            return UICamera.currentCamera;
        }
    }
}