using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tanks.Utilities;





namespace Tanks
{
    public class AssetManager : CustomSingleton<AssetManager>
    {
        const string SOUND_PATH     = "sounds/";
        const string SPRITE_PATH    = "Sprites/";
        const string PERFAB_PATH    = "Prefabs/";
        const string SCRIPT_PATH    = "lua/";
        const string UI_PATH        = "Prefabs/UI/";

        Dictionary<string, Object> resources = new Dictionary<string, Object>();

        public void Init()
        {
            /////////////////////////////////////////
            LoadBattleResources();
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 加载战斗用资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 加载战斗用资源
        /// </summary>
        public void LoadBattleResources()
        {
            /////////////////////////////////////////
            /// 建筑
            

            /////////////////////////////////////////
            /// 武器
            AddSprite("Weapon001");
            AddSprite("Weapon002");
            AddSprite("Weapon003");
            AddSprite("Weapon004");

            ////////////////////////////////////////
            /// 声音
            AddSound("explosion01");

            ////////////////////////////////////////
            /// UI
            AddUI("HUDPlayer");
            AddUI("HUDText");
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 释放战斗资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void UnLoadBattleResources()
        {
            /////////////////////////////////////////
            /// 建筑

            /////////////////////////////////////////
            /// 武器
            RemoveResources("Weapon001");
            RemoveResources("Weapon002");
            RemoveResources("Weapon003");
            RemoveResources("Weapon004");

            ////////////////////////////////////////
            /// 声音
            RemoveResources("explosion01");
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 预加载战斗资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void FakeLoadBattleResources()
        {
            /////////////////////////////////////////
            /// 建筑



            /////////////////////////////////////////
            /// 特效


            ////////////////////////////////////////
            /// 声音


            ////////////////////////////////////////
            /// ui
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        void RemoveResources(string key)
        {
            if (resources.ContainsKey(key))
            {
                resources.Remove(key);
            }
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public Object GetResources(string key)
        {
            Object res = null;

            resources.TryGetValue(key, out res);

            return res;
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 添加脚步资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void AddScript(string key)
        {
            if (resources.ContainsKey(key))
                return;

            resources.Add(key, LoadResource(SCRIPT_PATH + key));
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 添加图片资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void AddSprite(string key)
        {
            if (resources.ContainsKey(key))
                return;

            resources.Add(key, LoadResource(PERFAB_PATH + key));
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 添加声音资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void AddSound(string key)
        {
            if (resources.ContainsKey(key))
                return;

            resources.Add(key, LoadResource(SOUND_PATH + key));
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 添加ui资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void AddUI(string key)
        {
            if (resources.ContainsKey(key))
                return;

            resources.Add(key, LoadResource(UI_PATH + key));
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 读取资源
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public Object LoadResource(string path)
        {
            object asset = Resources.Load(path);
            return (Object)asset;
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 加载streaming资源
        /// 仅仅是获取了正确的资源路径，仍然使用外部读取手段
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public string LoadStramingAsset(string relativePath)
        {
            object asset = null;
            if (asset == null)
            {
                return FormatDataProviderPath(relativePath);
            }

            return (string)asset;
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public string FormatDataProviderPath(string datapath)
        {
            return System.IO.Path.Combine( GetStreamAssetsRootDir(), datapath);
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public string GetStreamAssetsRootDir()
        {
            return Application.streamingAssetsPath;
        }
    }
}