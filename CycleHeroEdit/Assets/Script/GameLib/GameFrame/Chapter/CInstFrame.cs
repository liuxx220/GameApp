using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;



public class CInstFrame : GUIFrame 
{



	UnityEngine.GameObject			m_btnEnter;
	UnityEngine.GameObject			m_btnClose;

	
	void Awake ()     
	{
		m_btnEnter 		= UnityEngine.GameObject.Find("EnterInst");
		m_btnClose  	= UnityEngine.GameObject.Find("CloseInst");

		UIEventListener.Get( m_btnEnter ).onClick  = onEnterInstClicked; 
		UIEventListener.Get( m_btnClose  ).onClick = onCloseInstClicked; 
	}
	
	// Use this for initialization
	void Start () 
	{
		
	}

	
	void onEnterInstClicked( UnityEngine.GameObject item )
	{
        int iEnterID = CFightTeamMgr.Instance.m_iEnteingMapID;
        tagInstanceProto pProto = null;
        CProtoManager.inst.m_mapInstance.TryGetValue(iEnterID, out pProto);
        if (pProto == null)
        {
            return;
        }	
	}

	void onCloseInstClicked( UnityEngine.GameObject item )
	{
		
		transform.gameObject.SetActive( false );

	}
}
 