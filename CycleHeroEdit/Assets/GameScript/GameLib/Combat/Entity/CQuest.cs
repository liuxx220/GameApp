using UnityEngine;
using System.Collections;
using System;
using System.Xml;






class CQuest
{

	tagQuestProto			m_pProto = null;
	tagQuestTrack			m_pQuestTrack = null;
	public CQuest( )
	{
		m_pQuestTrack 	= new tagQuestTrack ();
	}

	//-----------------------------------------------------------
	// init quest
	public void Init( tagQuestProto pProto )
	{
		m_pProto = pProto;

		m_pQuestTrack.dwFlag = EQuestState.QS_DOING;
		m_pQuestTrack.fTime  = pProto.limit_time / 1000.0f;
		for (int i = 0; i < 3; i++)
		{
			m_pQuestTrack.completeInfo [i] = pProto.complete_req_data [i];
			m_pQuestTrack.nTargetnum[i] = 0;
		}
	}


	//-----------------------------------------------------------
	public void Update( )
	{

	}


	//---------------------------------------------------------------
	public void Complete( )
	{
		// cale quest 's jiang li
		if (m_pProto != null) 
		{
			if( m_pProto.rew_type == 0 )
			{
                CFightTeamMgr.Instance.ChangeHeroExp(m_pProto.rew_value);
			}

			if( m_pProto.rew_type == 1 )
			{
                CFightTeamMgr.Instance.ChangeTeamExp(m_pProto.rew_value);
			}

			if( m_pProto.rew_type2 == 0 )
			{
                CFightTeamMgr.Instance.ChangeTeamMoney(m_pProto.rew_value2);
			}
			
			if( m_pProto.rew_type2 == 1 )
			{
                CFightTeamMgr.Instance.ChangeTeamJewel(m_pProto.rew_value2);
			}

			// reward item
			for( int i = 0; i < 3; i++ )
			{
				if( m_pProto.rew_item[i] > 0 )
					CItemMgr.Inst.AddItem2Bag( m_pProto.rew_item[i], (Int16)m_pProto.rew_item_num[i], EItemQuality.EIQ_Null, 0 );
			}
		}
	}

	public tagQuestProto GetProto( )
	{
		return m_pProto;
	}

	//----------------------------------------------------------------
	public bool IsCanComplete( )
	{
		bool bComplete = true;
		for( int i = 0; i < 3; i++ )
		{

			if( m_pProto.complete_req_data[i] <= m_pQuestTrack.nTargetnum[i] )
			{
				bComplete = false;
			}
		}

		return bComplete;
	}

	//---------------------------------------------------------------
	// trigger event of quest
	public void OnEvent( EQuestEvent eQuestType, uint dwEventMisc1, uint dwEventMisc2, uint dwEventMisc3 )
	{

		switch( eQuestType )
		{
		case EQuestEvent.EQE_Kill:
			OnEventKillMonster( dwEventMisc1 );
			break;

		case EQuestEvent.EQE_Item:
			OnEventItem( dwEventMisc1, dwEventMisc2, dwEventMisc3 );
			break;

		case EQuestEvent.EQE_Talk:
			OnEventNPCTalk( dwEventMisc1 );
			break;

		case EQuestEvent.EQE_UseItem:
			OnEventUseItem( dwEventMisc1, 0 );
			break;
		}
	}


	//---------------------------------------------------------------
	// kill monster event
	private void OnEventKillMonster( uint dwTypeID )
	{

		if (m_pProto == null)
			return;

		if( m_pProto.type == EQuestEvent.EQE_Kill )
		{
			for( int i = 0; i < 3; i++ )
			{
				if( m_pProto.complete_req_data[i] <= 0 )
					continue;

				if( m_pProto.complete_req_data[i] == dwTypeID )
				{
					m_pQuestTrack.nTargetnum[i]++;

				}
			}
		}
	}


	private void OnEventItem( uint dwItemTypeID, uint nNum, uint bAdd )
	{
		if (m_pProto == null)
			return;
		
		if( m_pProto.type == EQuestEvent.EQE_Item )
		{
			for( int i = 0; i < 3; i++ )
			{
				if( m_pProto.complete_req_data[i] <= 0 )
					continue;
				
				if( m_pProto.complete_req_data[i] == dwItemTypeID )
				{
					m_pQuestTrack.nTargetnum[i]++;

				}
			}
		}
	}

	private void OnEventUseItem( uint dwItemTypeID, uint dwTargetID )
	{
		if (m_pProto == null)
			return;
		
		if( m_pProto.type == EQuestEvent.EQE_UseItem )
		{
			for( int i = 0; i < 3; i++ )
			{
				if( m_pProto.complete_req_data[i] <= 0 )
					continue;
				
				if( m_pProto.complete_req_data[i] == dwItemTypeID )
				{
					m_pQuestTrack.nTargetnum[i]++;
				}
			}
		}
	}

	private void OnEventNPCTalk( uint dwNPCTypeID )
	{
		if (m_pProto == null)
			return;
		
		if( m_pProto.type == EQuestEvent.EQE_Talk )
		{
			for( int i = 0; i < 3; i++ )
			{
				if( m_pProto.complete_req_data[i] <= 0 )
					continue;
				
				if( m_pProto.complete_req_data[i] == dwNPCTypeID )
				{
					m_pQuestTrack.nTargetnum[i] = 1;

				}
			}
		}
	}
}

