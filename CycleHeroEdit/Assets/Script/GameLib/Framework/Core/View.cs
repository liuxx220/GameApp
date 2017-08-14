using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;






public class View : MonoBehaviour, IView 
{
    private ClientApp       _clientApp = null;
    private LuaScriptMgr    m_LuaMgr   = null;
    public virtual void OnMessage(IMessage message){
    }

    protected ClientApp facade
    {
        get
        {
            if (_clientApp == null)
            {
                _clientApp = ClientApp.Instance;
            }
            return _clientApp;
        }
    }


    protected LuaScriptMgr LuaManager
    {
        get
        {
            if (m_LuaMgr == null)
            {
                m_LuaMgr = facade.GetManager<LuaScriptMgr>(ManagerName.Lua);
            }
            return m_LuaMgr;
        }
        set { m_LuaMgr = value; }
    }



    /// ---------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册消息
    /// </summary>
    /// ---------------------------------------------------------------------------------------------------
    protected void RegisterMessage(IView view, List<string> messages)
    {
        if (messages == null || messages.Count == 0) return;
        Controller.Instance.RegisterViewCommand(view, messages.ToArray());
    }


    /// ---------------------------------------------------------------------------------------------------
    /// <summary>
    /// 移除消息
    /// </summary>
    /// ---------------------------------------------------------------------------------------------------
    protected void RemoveMessage(IView view, List<string> messages)
    {
        if (messages == null || messages.Count == 0) return;
        Controller.Instance.RemoveViewCommand(view, messages.ToArray());
    }
}