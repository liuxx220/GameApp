using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;






class CQuestMgr
{

	public static CQuestMgr Inst = null;

	private CQuestData						m_QuestData = null;
	private CQuestScript					m_Script = null;
	private uint							m_nTalkNPCTypeID = 0;
	private uint							m_nQuestNPCID = 0;

	public  Dictionary< uint, CQuest>		m_currentQuests;
	public  Dictionary< uint, int>			m_doneQuests;

	public CQuestMgr()
	{
		Inst 			= this;
		m_QuestData 	= new CQuestData ();
		m_Script 		= new CQuestScript ();

		m_currentQuests = new Dictionary<uint, CQuest> ();
		m_doneQuests 	= new Dictionary<uint, int> ();
	}

	public void InitQuest( )
	{
		if (m_QuestData != null) 
		{
			// load quest proto
			m_QuestData.LoadQuestProto();
			// load quest name
			m_QuestData.LoadQuestName();
		}

		if( m_Script != null && CScriptLuaMgr.Inst.m_luaState != null )
		{
			m_Script.InitLua( CScriptLuaMgr.Inst.m_luaState );
		}
	}


	public void Update( )
	{
		// ----------------------------------------------
		// chu li limit time quest
		foreach( var it in m_currentQuests )
		{
			it.Value.Update();
		}
	}


	public CQuestData GetData()
	{
		return m_QuestData;
	}

	public CQuestScript GetQuestScript( )
	{
		return m_Script;
	}

	public void ClearTalkNPC( )
	{
		m_nTalkNPCTypeID = 0;
	}


	public void OnNPCTalk( uint npcid, uint npctypeid )
	{
		m_nTalkNPCTypeID = npctypeid;
	}

	//-----------------------------------------------------------------
	// add quest by id
	public void	AcceptQuest( uint questid )
	{

		tagQuestProto pQuestProto = m_QuestData.GetQuestProto (questid);
		if( pQuestProto == null )
		{
			Common.ERROR_MSG( string.Format( "not find quest proto ", questid ) );
			return ;
		}

		CQuest pQuest = null;
		m_currentQuests.TryGetValue (questid, out pQuest);
		if( pQuest == null )
		{
			pQuest = new CQuest();
			pQuest.Init( pQuestProto );
			m_currentQuests.Add( questid, pQuest );
		}
	}


	public void CompleteQuest( uint questid )
	{
		CQuest pQuest = null;
		m_currentQuests.TryGetValue (questid, out pQuest);
		if( pQuest != null )
		{
			pQuest.Complete();
		}

		m_currentQuests.Remove (questid);
		m_doneQuests.Add (questid, 1);
	}

	//-------------------------------------------------------------------
	// delete quest by id
	public void DeleteQuest( uint questid )
	{
		CQuest pQuest = null;
		m_currentQuests.TryGetValue (questid, out pQuest);
		if( pQuest != null )
		{
			m_currentQuests.Remove( questid );
		}
	}


	public uint GetTalkNPCTypeID()
	{
		return m_nTalkNPCTypeID;
	}


	public uint GetQuestNPCID( )
	{
		return m_nQuestNPCID;
	}

	//------------------------------------------------------------------
	// quest is can accpet
	public bool IsCanAcceptQuest( uint questid )
	{

		// haved accepted quest
		bool bHave = IsHaveQuest (questid);
		if (bHave)
			return false;

		tagQuestProto pQuestProto = m_QuestData.GetQuestProto (questid);
		if( pQuestProto == null )
		{
			return false;
		}

		if( pQuestProto.limit_class != 0 )
		{

		}

		if( pQuestProto.limit_level != 0 )
		{
            if (CFightTeamMgr.Instance.m_pBattleHero.m_nLevel < pQuestProto.limit_level)
				return false;
		}

		if( pQuestProto.prev_quest_id != 0 )
		{
			if( !IsCopletedQuest(pQuestProto.prev_quest_id) )
				return false;
		}

		return true;
	}

	//-------------------------------------------------------------------
	// is have quest
	public bool IsHaveQuest( uint questid )
	{

		CQuest pQuest = null;
		m_currentQuests.TryGetValue (questid, out pQuest);
		if (pQuest != null)
			return true;

		return false;
	}


	//-----------------------------------------------------------------
	// quest is can complete
	public bool IsCanCompleteQuest( uint questid )
	{
		CQuest pQuest = null;
		m_currentQuests.TryGetValue (questid, out pQuest);
		if (pQuest != null)
			return pQuest.IsCanComplete();

		return true;
	}


	//--------------------------------------------------------------------
	// is completed quest
	public bool IsCopletedQuest( uint questid )
	{
		int nRet = 0;
		m_doneQuests.TryGetValue (questid, out nRet);
		if( nRet > 0 )
		{
			return true;
		}

		return false;
	}

	//-------------------------------------------------------------------
	// quest event update
	//-------------------------------------------------------------------
	public void OnQuestEvent( EQuestEvent eEvent, uint Param1, uint Param2, uint Param3 )
	{
		foreach( var it in m_currentQuests )
		{
			it.Value.OnEvent( eEvent, Param1, Param2, Param3 );
		}
	}
}
