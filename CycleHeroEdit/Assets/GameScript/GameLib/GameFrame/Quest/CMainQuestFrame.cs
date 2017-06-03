using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;



public class CMainQuestFrame : GUIFrame 
{


	UnityEngine.GameObject		m_btnDropQuest;
	private UILabel     		m_LabelExp;
	private UILabel     		m_LabelMoney;
	private UILabel     		m_LabelQuestDesc;
	private UILabel     		m_LabelQuestAccp;
	UnityEngine.GameObject		m_questlist;
	UnityEngine.GameObject		m_acceptlist;
	UnityEngine.GameObject		m_questItem;
	UnityEngine.GameObject		m_btnClose;	


	UnityEngine.GameObject[]	m_reward = new GameObject[3];

	private Dictionary< UnityEngine.GameObject, uint > m_mapObj2QuestID;
    public override bool ReloadUI()
    {
        base.ReloadUI();

		m_mapObj2QuestID 		= new Dictionary<GameObject, uint> ();

		m_btnDropQuest 			= UnityEngine.GameObject.Find ( "dropquest" );

		m_LabelExp 				= transform.Find("Anchor/rewardexp" ).GetComponent<UILabel>();
        m_LabelMoney            = transform.Find("Anchor/rewardmoney").GetComponent<UILabel>();
        m_LabelQuestDesc        = transform.Find("Anchor/questmiaoshu").GetComponent<UILabel>();
        m_LabelQuestAccp        = transform.Find("Anchor/acceptmiaoshu").GetComponent<UILabel>();
		m_questlist 			= UnityEngine.GameObject.Find ( "questlist" );
		m_acceptlist 			= UnityEngine.GameObject.Find ( "questlist" );

		m_reward[0]				= UnityEngine.GameObject.Find ( "reward0" );
		m_reward[1]				= UnityEngine.GameObject.Find ( "reward1" );
		m_reward[2]				= UnityEngine.GameObject.Find ( "reward2" );
		m_btnClose				= UnityEngine.GameObject.Find ( "questclose" );


		if (m_acceptlist != null)
			m_acceptlist.SetActive (false);

		UIEventListener.Get( m_btnDropQuest ).onClick 	= OnDropQuestEvent;
		UIEventListener.Get( m_btnClose ).onClick 		= OnQuestClose;

        return true;
	}

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {
        m_btnDropQuest  = null;
	    m_LabelExp      = null;
	    m_LabelMoney    = null;
	    m_LabelQuestDesc= null;
	    m_LabelQuestAccp= null;
	    m_questlist     = null;
	    m_acceptlist    = null;
	    m_questItem     = null;
	    m_btnClose      = null;	
        base.Destroy();
    }


	void ClearQuest( )
	{

		foreach( var item in m_mapObj2QuestID )
		{
			UnityEngine.GameObject.DestroyImmediate( item.Key );
		}

		m_mapObj2QuestID.Clear();
	}


	void OnQuestClose( UnityEngine.GameObject item )
	{
		
	}


	void OnQuestTabEvent( UnityEngine.GameObject item )
	{


	}


	void OnQuestCheckEvent( UnityEngine.GameObject item )
	{
		uint questid = 0;
		m_mapObj2QuestID.TryGetValue (item, out questid);
		if( questid > 0 )
		{
			ShowQuestInfo(questid);
		}
	}

	
	void OnDropQuestEvent( UnityEngine.GameObject item )
	{

	}


	void ShowMainQuestList( )
	{

		ClearQuest ();
		foreach( var item in CQuestMgr.Inst.m_currentQuests )
		{
			CQuest pQuest = item.Value;
			if( pQuest != null )
				AddQuestOne( pQuest.GetProto() );
		}
	}
	

	void AddQuestOne( tagQuestProto pProto )
	{

		tagQuestText pQuestText = CQuestMgr.Inst.GetData().GetQuestText( pProto.id );
		if( pQuestText == null )
			return;

		if( m_questItem != null )
		{
			GameObject pItem = NGUITools.AddChild( m_questlist, m_questItem );
			if( pItem != null )
			{
				Transform pIcon = pItem.transform.FindChild("Icon");
				if( pIcon != null )
				{
					UnityEngine.GameObject ctrl = pIcon.gameObject;
					ctrl.GetComponent<UILabel>().text = pQuestText.name;
				}

				float fheight = (m_questItem.transform.position.y - 32 * m_mapObj2QuestID.Count);
				Vector3 pos   = new Vector3( m_questItem.transform.position.x, fheight,m_questItem.transform.position.z );
				pItem.transform.position = pos;

				UIEventListener.Get( pItem ).onClick = OnQuestCheckEvent;
				m_mapObj2QuestID.Add( pItem, pProto.id );
			}
		}
	}

	void ShowQuestInfo( uint questid )
	{

		tagQuestText pQuestText = CQuestMgr.Inst.GetData().GetQuestText( questid );
		if( pQuestText == null )
			return;

		tagQuestProto pProto = CQuestMgr.Inst.GetData().GetQuestProto( questid );
		if( pProto == null )
			return;

		m_LabelExp.text 		= pProto.rew_value.ToString ();
		m_LabelMoney.text 		= pProto.rew_value2.ToString ();
		m_LabelQuestDesc.text 	= pQuestText.over_view;
		m_LabelQuestAccp.text 	= pQuestText.AcceptTalk;

		for( int i = 0; i < 3; i++ )
		{
			ShowRewardItem( m_reward[i], pProto.rew_item[i], pProto.rew_item_num[i] ); 
		}
	}


	void ShowRewardItem( GameObject item, uint dwTypeID, int nNum )
	{

		if( ItemCreator.MIsEquipment(dwTypeID) )
		{
			tagEquipProto pEquipProto = null;
            CProtoManager.inst.m_mapEquip.TryGetValue(dwTypeID, out pEquipProto);
			if( pEquipProto != null )
			{
				Transform pIcon = item.transform.FindChild("Icon");
				if( pIcon != null )
				{
					UIAtlas tu = Resources.Load("GameIcon", typeof(UIAtlas)) as UIAtlas;
					UnityEngine.GameObject ctrl = pIcon.gameObject;
					ctrl.GetComponent<UISprite>().atlas = tu;
					ctrl.GetComponent<UISprite>().spriteName = pEquipProto.strIcon;
				}
				
				Transform pNum  = item.transform.FindChild("num");
				if( pNum != null )
				{
					UnityEngine.GameObject ctrl = pNum.gameObject;
					ctrl.GetComponent<UILabel>().text = nNum.ToString();
				}
			}
		}
		else
		{
			tagItemProto pProto = null;
            CProtoManager.inst.m_mapItem.TryGetValue(dwTypeID, out pProto);
			if( pProto != null )
			{
				Transform pIcon = item.transform.FindChild("Icon");
				if( pIcon != null )
				{
					UIAtlas tu = Resources.Load("GameIcon", typeof(UIAtlas)) as UIAtlas;
					UnityEngine.GameObject ctrl = pIcon.gameObject;
					ctrl.GetComponent<UISprite>().atlas = tu;
					ctrl.GetComponent<UISprite>().spriteName = pProto.strIcon;
				}
				
				Transform pNum  = item.transform.FindChild("num");
				if( pNum != null )
				{
					UnityEngine.GameObject ctrl = pNum.gameObject;
					ctrl.GetComponent<UILabel>().text = nNum.ToString();
				}
			}
		}
	}
}
