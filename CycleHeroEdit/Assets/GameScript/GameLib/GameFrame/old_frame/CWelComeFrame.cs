using UnityEngine;
using System.Collections;
using System;




public class CWelComeFrame : GUIFrame
{

	UnityEngine.GameObject			m_StartProcess;
	void Awake ()     
	{
		m_StartProcess		= UnityEngine.GameObject.Find("startprocess");
		UIEventListener.Get( m_StartProcess ).onClick = OnStartProcess; 
	}


	void Start () 
	{

	}


	void OnStartProcess( UnityEngine.GameObject item )
	{
		transform.gameObject.SetActive (false);
		if( CQuestMgr.Inst != null )
		{
			CQuestMgr.Inst.AcceptQuest( 1 );
		}
	}
}

