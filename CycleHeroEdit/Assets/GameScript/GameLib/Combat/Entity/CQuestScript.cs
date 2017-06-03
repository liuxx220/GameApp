using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





class CQuestScript : CGameScript
{


	public CQuestScript( )
	{

	}

	private string GetNpcScriptFile( uint  npcid )
	{
		string szfile = "quest/script/npc_";
		szfile += npcid.ToString ();
		szfile += ".lua";
		
		return szfile;
	}


	public void	OnTalk( uint npcID )
	{

		string scriptfile = GetNpcScriptFile( npcID );
		if( scriptfile != "" )
		{
			CGameScript pScript = CScriptLuaMgr.Inst.CreateScript( scriptfile, true );
			if( pScript != null )
			{
				pScript.GetStoreFunction( "OnTalk" + npcID.ToString() );
				pScript.RunFunction( 0, 0 );
			}
		}
	}

	public void OnAcceptQuestTalk( uint questid, int step )
	{

		uint npcid = CQuestMgr.Inst.GetTalkNPCTypeID();
		string scriptfile = GetNpcScriptFile( npcid );
		if( scriptfile != "" )
		{
			CGameScript pScript = CScriptLuaMgr.Inst.CreateScript( scriptfile, true );
			if( pScript != null )
			{
				pScript.GetStoreFunction( "OnAcceptQuestTalk" + npcid.ToString() );
				pScript.Pop();
				pScript.PushUint( questid );
				pScript.PushInt( step );
				pScript.RunFunction( 2, 0 );
			}
		}
	}

	public void OnCompleteQuestTalk( uint questid, int step )
	{

		uint npcid = CQuestMgr.Inst.GetTalkNPCTypeID();
		string scriptfile = GetNpcScriptFile( npcid );
		if( scriptfile != "" )
		{
			CGameScript pScript = CScriptLuaMgr.Inst.CreateScript( scriptfile, true );
			if( pScript != null )
			{
				pScript.GetStoreFunction( "OnCompleteQuestTalk" + npcid.ToString() );
				pScript.Pop();
				pScript.PushUint( questid );
				pScript.PushInt( step );
				pScript.RunFunction( 2, 0 );
			}
		}
	}

	public void OnScenarioTalk( uint id, int step )
	{

		uint npcid = CQuestMgr.Inst.GetTalkNPCTypeID();
		string scriptfile = GetNpcScriptFile( npcid );
		if( scriptfile != "" )
		{
			CGameScript pScript = CScriptLuaMgr.Inst.CreateScript( scriptfile, true );
			if( pScript != null )
			{
				pScript.GetStoreFunction( "OnScenarioTalk" + npcid.ToString() );
				pScript.PushUint( id );
				pScript.PushInt( step );
				pScript.RunFunction( 2, 0 );
			}
		}
	}

	public void OnQuestTalk( uint questid, int step )
	{

		uint npcid = CQuestMgr.Inst.GetTalkNPCTypeID();
		string scriptfile = GetNpcScriptFile( npcid );
		if( scriptfile != "" )
		{
			CGameScript pScript = CScriptLuaMgr.Inst.CreateScript( scriptfile, true );
			if( pScript != null )
			{
				pScript.GetStoreFunction( "OnQuestTalk" + npcid.ToString() );
				pScript.PushUint( questid );
				pScript.PushInt( step );
				pScript.RunFunction( 2, 0 );
			}
		}
	}

	public void OnExitQuestTalk( )
	{
        CQuestMgr.Inst.ClearTalkNPC();
        GameUIManager.Instance().HideFrame(GUIDefine.UIF_NPCTALKFRAME);
	}
}

