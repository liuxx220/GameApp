using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;







public class CLoginFrame : GUIFrame 
{
	

	UIInput		m_usename;
	UIInput		m_password;

    private UnityEngine.GameObject btnEnterGame = null;
    private UnityEngine.GameObject btnQuitGame = null;
    public override bool ReloadUI()     
	{
        base.ReloadUI();

        m_usename        = transform.Find("Login/zhanghao").GetComponent<UIInput>();
        m_password       = transform.Find("Login/mima").GetComponent<UIInput>();
        btnEnterGame     = transform.Find("Login/StartGame").gameObject;
        btnQuitGame      = transform.Find("Login/QuetGame").gameObject;
        m_usename.value  = "13520488926";
        m_password.value = "123456";

        UIEventListener.Get(btnEnterGame).onClick += ClickStartGame;
        UIEventListener.Get(btnQuitGame).onClick  += ClickQuestGame;
        return true;
	}
	

	void ClickStartGame( UnityEngine.GameObject obj )
	{

		if( m_usename.value == "" || m_usename.value.Length > 30 )
		{
			return;
		}

		if( m_password.value == "" )
		{
			return;
		}

        GameUIManager.Instance().DestoryFrame(GUIDefine.UIF_LOGINFRAME);
        GameUIManager.Instance().CreateFrame(GUIDefine.UIF_SELECTHEROFRAME, true);
	}


    void ClickQuestGame( UnityEngine.GameObject obj )
	{
        GameUIManager.Instance().DestoryFrame(GUIDefine.UIF_LOGINFRAME);
        GameUIManager.Instance().CreateFrame(GUIDefine.UIF_SELECTHEROFRAME, true);
	}
	
    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {
       
        m_usename       = null;
	    m_password      = null;
        btnEnterGame    = null;
        btnQuitGame     = null;
        base.Destroy();
    }
}
 