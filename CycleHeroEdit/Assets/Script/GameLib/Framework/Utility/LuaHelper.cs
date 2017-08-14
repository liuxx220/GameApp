/*
 * ----------------------------------------------------------------------------
 *          file name : LuaHelper.cs
 *          desc      : Lua 的帮助接口
 *          author    : LJP
 *          
 *          log       : [ 2015-05-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using LuaInterface;
using System.Collections.Generic;




namespace LuaFramework
{
    public static class LuaHelper
    {

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据类的名字，返回类的类型
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static System.Type GetType( string classname )
        {
            Assembly assb = Assembly.GetExecutingAssembly();
            System.Type t = null;

            t = assb.GetType(classname);
            if( t == null )
                t = assb.GetType(classname);

            return t;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 面板管理器
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static GameUIManager GetPanelManager()
        {
            return ClientApp.Instance.GetManager<GameUIManager>(ManagerName.Panel);
        }



        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 资源管理器
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static CBundleManager GetResManager()
        {
            return ClientApp.Instance.GetManager<CBundleManager>(ManagerName.Resource);
        }


        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 网络管理器
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static NetworkManager GetNetworkManager()
        {
            return ClientApp.Instance.GetManager<NetworkManager>(ManagerName.Network);
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 地图加载管理器
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static CLoadLevelMgr GetLevelManager()
        {
            return ClientApp.Instance.GetManager<CLoadLevelMgr>(ManagerName.LoadLevelMgr);
        }


        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 事件管理器对象
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static CEventCenterMgr GetEventManager()
        {
            return ClientApp.Instance.GetManager<CEventCenterMgr>(ManagerName.EventCenterMgr);
        }


        public static Action Action( LuaFunction func )
        {
            Action action = () => {
               func.Call();
            };

            return action;
        }

        public static UIEventListener.VoidDelegate VoidDelegate(LuaFunction func)
        {
            UIEventListener.VoidDelegate action = (go) =>
            {
                func.Call(go);
            };
            return action;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// pbc/pblua函数回调
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public static void OnCallLuaFunc(LuaStringBuffer data, LuaFunction func)
        {
            byte[] buffer = data.buffer;
            if (func != null)
            {
                LuaScriptMgr mgr = ClientApp.Instance.GetManager<LuaScriptMgr>(ManagerName.Lua);
                int oldTop = func.BeginPCall();
                LuaDLL.lua_pushlstring(mgr.lua.L, buffer, buffer.Length);
                if (func.PCall(oldTop, 1)) func.EndPCall(oldTop);
            }
            Debug.LogWarning("OnCallLuaFunc buffer:>>" + buffer + " lenght:>>" + buffer.Length);
        }

        /// <summary>
        /// cjson函数回调
        /// </summary>
        public static void OnJsonCallFunc(string data, LuaFunction func)
        {
            Debug.LogWarning("OnJsonCallback data:>>" + data + " lenght:>>" + data.Length);
            if (func != null) func.Call(data);
        }
    }
}