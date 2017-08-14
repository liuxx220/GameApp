using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;





public class CQuestTrackFrame : GUIFrame 
{

	UnityEngine.GameObject			m_btnTrack;
	UnityEngine.GameObject			m_pNPCIcon;

	void Awake( )     
	{
		m_btnTrack 		= UnityEngine.GameObject.Find ( "trackquest" );
		m_pNPCIcon		= UnityEngine.GameObject.Find ( "npc_icon" );


		UIEventListener.Get( m_btnTrack ).onClick = OnAutoTrackQuest;
	}


	public IEnumerator UpdateNPCIcon( )
	{

		uint dwNPCID 	= CQuestMgr.Inst.GetQuestNPCID();
		if( dwNPCID <= 0 )
			yield break;

		tagEntityProto pEntity = null;
        CProtoManager.inst.m_mapEntity.TryGetValue(dwNPCID, out pEntity);
		if (pEntity == null)
			yield break;

		if( m_pNPCIcon != null )
		{
			UIAtlas tu = Resources.Load("NPC_Icon", typeof(UIAtlas)) as UIAtlas;
			m_pNPCIcon.GetComponent<UISprite>().atlas = tu;
			m_pNPCIcon.GetComponent<UISprite>().spriteName = pEntity.szIcon;
		}
	}


	void OnAutoTrackQuest( UnityEngine.GameObject item )
	{

		uint dwNPCID 	= CQuestMgr.Inst.GetQuestNPCID();
		if( dwNPCID <= 0 )
			return;

        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero == null)
			return;

		tagNpcMapPos vTargetPos;
        CProtoManager.inst.m_mapNPCPos.TryGetValue(dwNPCID, out vTargetPos);

	}
}
