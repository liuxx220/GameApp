using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;



public class ChapterFrame : GUIFrame 
{



	UnityEngine.GameObject			m_btnChapter1;
	UnityEngine.GameObject			m_btnChapter2;
	UnityEngine.GameObject			m_btnClose;

	public Transform 				m_fuben;	// 关卡

	void Awake ()     
	{
		m_btnChapter1  	= UnityEngine.GameObject.Find("Chapter01");
		m_btnChapter2  	= UnityEngine.GameObject.Find("Chapter02");
		m_btnClose  	= UnityEngine.GameObject.Find("Close");

		UIEventListener.Get( m_btnChapter1 ).onPress  = onChapterClicked; 
		UIEventListener.Get( m_btnChapter1  ).onPress = onChapterClicked; 
		UIEventListener.Get( m_btnClose  ).onPress = onCloseClicked; 
	}
	
	// Use this for initialization
	void Start () 
	{
		
	}

	
	void onChapterClicked( UnityEngine.GameObject item, bool state )
	{
        //if (state) 
        //{
        //    // 关闭父窗口
        //    transform.gameObject.SetActive( false );

        //    if( CFightTeamMgr.inst != null )
        //        CFightTeamMgr.inst.m_iEnteingMapID = 1;

        //    Transform Chapter 		= Instantiate (m_fuben) as Transform;
        //    Chapter.parent 			= m_fuben.parent;
        //    Chapter.localScale 		= m_fuben.localScale;
        //    Chapter.localPosition 	= m_fuben.localPosition;
        //    Chapter.gameObject.SetActive (true);
        //}
		
	}

	void onCloseClicked( UnityEngine.GameObject item, bool state )
	{

        CFightTeamMgr.Instance.m_iEnteingMapID = 0;
		transform.gameObject.SetActive( false );

	}
}
 