using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






enum EOptionType
{
	EOT_null			= -1,
	EOT_AcceptQuest		= 0,
	EOT_CompleteQuest	= 1,
	EOT_ScenarioTalk
}

public class CNPCTalkFrame : GUIFrame
{

	UnityEngine.GameObject			PlayerTalk;
	UnityEngine.GameObject			NpcTalk;
	UnityEngine.GameObject			QuestTalk;
	UnityEngine.GameObject			m_LabelExp;
	UnityEngine.GameObject			m_LabelMoney;
	UnityEngine.GameObject			m_AcceptQuest;
	UnityEngine.GameObject			m_CompleteQuest;
	UnityEngine.GameObject[]		m_reward = new GameObject[3];

	private uint	 				m_questid;
	private int						m_step = 0;

	private EOptionType	m_eOpType 	= EOptionType.EOT_null;

    public override bool ReloadUI()
    {
        base.ReloadUI();

        PlayerTalk              = transform.FindChild("Anchor/Panel/player_talk").gameObject;
        NpcTalk                 = transform.FindChild("Anchor/Panel/npc_talk").gameObject;
        QuestTalk               = transform.FindChild("Anchor/Panel/quest_talk").gameObject;

        m_AcceptQuest           = transform.FindChild("Anchor/Panel/quest_talk/acceptedquest").gameObject;
        m_CompleteQuest         = transform.FindChild("Anchor/Panel/quest_talk/completequest").gameObject;
        m_LabelExp              = transform.FindChild("Anchor/Panel/quest_talk/exp").gameObject;
        m_LabelMoney            = transform.FindChild("Anchor/Panel/quest_talk/money").gameObject;
        m_reward[0]             = transform.FindChild("Anchor/Panel/quest_talk/qreward0").gameObject;
        m_reward[1]             = transform.FindChild("Anchor/Panel/quest_talk/qreward1").gameObject;
        m_reward[2]             = transform.FindChild("Anchor/Panel/quest_talk/qreward2").gameObject;


		UIEventListener.Get( PlayerTalk ).onClick 	= onClickTalkEvent; 
		UIEventListener.Get( NpcTalk  ).onClick 	= onClickTalkEvent; 

		UIEventListener.Get( m_AcceptQuest  ).onClick 	= onAcceptQuestEvent;
		UIEventListener.Get( m_CompleteQuest  ).onClick = onCompleteQuestEvent;

		if (m_AcceptQuest != null)
			m_AcceptQuest.SetActive (false);
		if (m_CompleteQuest != null)
			m_CompleteQuest.SetActive (false);

        return true;
	}

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {

        PlayerTalk       = null;
        NpcTalk          = null;
        QuestTalk        = null;
        m_LabelExp       = null;
        m_LabelMoney     = null;
        m_AcceptQuest    = null;
        m_CompleteQuest  = null;
        m_reward[0]      = null;
        m_reward[1]      = null;
        m_reward[2]      = null;
        base.Destroy();
    }

    /// ----------------------------------------------------------------------------
    /// <summary>
    /// UI资源加载万程
    /// </summary>
    /// ----------------------------------------------------------------------------
    public override void OnAsyncLoaded()
    {
        if (gameObject != null)
        {
            transform = gameObject.transform;

            ReloadUI();
            mIsLoaded = true;

            OnHide();
        }
    }

	private void ClearTalkFrame( )
	{
		PlayerTalk.SetActive (false);
		NpcTalk.SetActive (false);
		QuestTalk.SetActive (false);
	}


	private void SetHeroCaption( string strContent )
	{

        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if( pHero == null )
			return ;

		ClearTalkFrame ();
		PlayerTalk.SetActive (true);
		Transform pInfo = PlayerTalk.transform.FindChild("info");
		if( pInfo != null )
		{
			pInfo.GetComponent<UILabel>().text  = strContent;
		}

		Transform icon = PlayerTalk.transform.FindChild("icon");
		if( icon != null )
		{
			UnityEngine.GameObject ctrl = icon.gameObject;
			ctrl.GetComponent<UISprite>().spriteName = "player_01";
		}

		Transform name = PlayerTalk.transform.FindChild("name");
		if( icon != null )
		{

			UnityEngine.GameObject ctrl = name.gameObject;
			ctrl.GetComponent<UILabel>().text = "hado123";
		}
	}

	private void SetNpcCaption( uint questid, int step, string strContent )
	{
		tagEntityProto pEntity = null;
        CProtoManager.inst.m_mapEntity.TryGetValue(CQuestMgr.Inst.GetTalkNPCTypeID(), out pEntity);
		if (pEntity == null)
			return;
		
		ClearTalkFrame ();
		NpcTalk.SetActive (true);
		Transform pInfo = NpcTalk.transform.FindChild("info");
		if( pInfo != null )
		{
			pInfo.GetComponent<UILabel>().text  = strContent;
		}
		
		Transform icon = NpcTalk.transform.FindChild("icon");
		if( icon != null )
		{
			UnityEngine.GameObject ctrl = icon.gameObject;
			ctrl.GetComponent<UISprite>().spriteName = pEntity.szIcon;
		}
		
		Transform name = NpcTalk.transform.FindChild("name");
		if( icon != null )
		{
			UnityEngine.GameObject ctrl = name.gameObject;
			ctrl.GetComponent<UILabel>().text = pEntity.szName;
		}
	}


	private void SetQuestCaption( uint questid )
	{

		tagEntityProto pEntity = null;
        CProtoManager.inst.m_mapEntity.TryGetValue(CQuestMgr.Inst.GetTalkNPCTypeID(), out pEntity);
		if (pEntity == null)
			return;

		ClearTalkFrame ();
		QuestTalk.SetActive (true);
		if (m_eOpType == EOptionType.EOT_AcceptQuest) 
		{
			m_AcceptQuest.SetActive( true );
			m_CompleteQuest.SetActive( false );
		}


		if( m_eOpType == EOptionType.EOT_CompleteQuest )
		{
			m_AcceptQuest.SetActive( false );
			m_CompleteQuest.SetActive( true );
		}

		tagQuestText pQuestText = CQuestMgr.Inst.GetData().GetQuestText( questid );
		if( pQuestText == null )
			return;
		
		tagQuestProto pProto = CQuestMgr.Inst.GetData().GetQuestProto( questid );
		if( pProto == null )
			return;

		Transform pInfo = QuestTalk.transform.FindChild("info");
		if( pInfo != null )
		{
			if (m_eOpType == EOptionType.EOT_AcceptQuest)
				pInfo.GetComponent<UILabel>().text  = pQuestText.AcceptTalk;
			if( m_eOpType == EOptionType.EOT_CompleteQuest )
				pInfo.GetComponent<UILabel>().text  = pQuestText.CompleteTalk;
		}

		Transform icon = QuestTalk.transform.FindChild("icon");
		if( icon != null )
		{
			UnityEngine.GameObject ctrl = icon.gameObject;
			ctrl.GetComponent<UISprite>().spriteName = pEntity.szIcon;
		}

		m_LabelExp.GetComponent<UILabel> ().text 		= pProto.rew_value.ToString ();
		m_LabelMoney.GetComponent<UILabel> ().text 		= pProto.rew_value2.ToString ();
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


	void onClickTalkEvent( UnityEngine.GameObject item )
	{
		
		CQuestScript pQS = CQuestMgr.Inst.GetQuestScript ();
		if (pQS == null)
			return;

		ClearTalkFrame();

		if( m_eOpType == EOptionType.EOT_ScenarioTalk )
		{
			pQS.OnScenarioTalk( m_questid, m_step );
		}
	}

	void onAcceptQuestEvent( UnityEngine.GameObject item )
	{
		if( m_questid > 0 )
		{
			if( CQuestMgr.Inst.IsCanAcceptQuest( m_questid ))
				CQuestMgr.Inst.AcceptQuest( m_questid );

			transform.gameObject.SetActive( false );
		}
	}

	void onCompleteQuestEvent( UnityEngine.GameObject item )
	{
		if( m_questid > 0 )
		{
			if( CQuestMgr.Inst.IsCanCompleteQuest( m_questid ))
				CQuestMgr.Inst.CompleteQuest( m_questid );

			transform.gameObject.SetActive( false );
		}
	}

    public void SetQuestTalkOption(uint questid, int step, bool bNpcSay, string strCap)
	{
		m_questid 		= questid;
		m_step		 	= step;
		m_eOpType 		= EOptionType.EOT_ScenarioTalk;
		if( bNpcSay )
		{
			SetNpcCaption( m_questid, m_step, strCap );
		}
		else
		{
			SetHeroCaption( strCap );
		}
	}

    public void SetAcceptTalkOption( uint questid)
	{
		m_questid 		= questid;
		m_eOpType 		= EOptionType.EOT_AcceptQuest;
		SetQuestCaption (m_questid);
	}

    public void SetCompleteTalkOption( uint questid )
	{
		m_questid 		= questid;
		m_eOpType 		= EOptionType.EOT_CompleteQuest;
		SetQuestCaption ( m_questid );
	}
}
