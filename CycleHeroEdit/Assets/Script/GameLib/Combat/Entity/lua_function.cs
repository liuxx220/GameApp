using System;
using System.Text;
using UniLua;



// C# function to lua 's 




public static class ToolLib
{
	
	
	public const string LIBNAME = "Quest";
	
	public static int OpenLib( ILuaState lua )
	{
		var toollib = new NameFuncPair[]
		{
			
			new NameFuncPair( "OpenNpcTalk", 		CL_OpenNPCTalk ),
			new NameFuncPair( "CloseNpcTalk", 		CL_CloseNPCTalk ),
			new NameFuncPair( "SetNpcTalkOption", 	CL_SetNPCTalkOption ), 
			new NameFuncPair( "IsCompletedQuest", 	CL_IsCompletedQuest ),
			new NameFuncPair( "IsCanCompleted", 	CL_IsCanCompleteQuest ), 
			new NameFuncPair( "IsCanAccepted", 		CL_IsCanAcceptQuest ),

			new NameFuncPair( "SetAcceptOption", 	CL_SetAcceptQuestOption ),
			new NameFuncPair( "SetCopleteOption", 	CL_SetCompleteQuestOption ),
		};
		
		lua.L_NewLib ( toollib );
		return 1;
	}
	
	static string EncUTF8( string strBuf )
	{
		Encoding encode   	= Encoding.GetEncoding ("ISO-8859-1"); 
		byte[] buffer1   	= encode.GetBytes (strBuf);
		string strBuffer 	= Encoding.UTF8.GetString( buffer1, 0, buffer1.Length);
		return strBuffer;
	}

	//--------------------------------------------------------------
	// npc talk function
	public static int CL_OpenNPCTalk( ILuaState lua )
	{
        uint npcid      = lua.L_CheckUnsigned(1);
        CQuestMgr.Inst.OnNPCTalk(npcid, npcid);


        GameUIManager.Instance().ShowFrame(GUIDefine.UIF_NPCTALKFRAME);
		return 0;
	}
	
	public static int CL_CloseNPCTalk( ILuaState lua )
	{

		CQuestMgr.Inst.ClearTalkNPC();
        GameUIManager.Instance().HideFrame(GUIDefine.UIF_NPCTALKFRAME);
		return 0;
	}
	
	public static int CL_SetNPCTalkOption( ILuaState lua )
	{
		uint questid  		= lua.L_CheckUnsigned(1);
		int  step 	 		= lua.L_CheckInteger (2);
		bool bNpcSay 		= lua.L_CheckInteger (3) == 1 ? true : false;
		string strCaption 	= ToolLib.EncUTF8( lua.ToString( 4 ) );


        CNPCTalkFrame pFrame = (CNPCTalkFrame)GameUIManager.Instance().GetFrame(GUIDefine.UIF_NPCTALKFRAME);
        if (pFrame != null)
		{
            pFrame.SetQuestTalkOption(questid, step, bNpcSay, strCaption);
		}
		return 0;
	}

	
	public static int CL_IsCompletedQuest( ILuaState lua )
	{
		if( CQuestMgr.Inst != null )
		{
			uint questid    = lua.L_CheckUnsigned(1);
			if( CQuestMgr.Inst.IsCopletedQuest( questid ) )
				lua.PushInteger (1);
			else
				lua.PushInteger (0);
		}
		return 1;
	}
	
	public static int CL_IsCanCompleteQuest( ILuaState lua )
	{
		if( CQuestMgr.Inst != null )
		{
			uint questid    = lua.L_CheckUnsigned(1);
			if( CQuestMgr.Inst.IsCanCompleteQuest( questid ) )
				lua.PushInteger (1);
			else
				lua.PushInteger (0);
		}
		return 1;
	}


	public static int CL_IsCanAcceptQuest( ILuaState lua )
	{

		if( CQuestMgr.Inst != null )
		{
			uint questid    = lua.L_CheckUnsigned(1);
			if( CQuestMgr.Inst.IsCanAcceptQuest( questid ) )
				lua.PushInteger (1);
			else
				lua.PushInteger (0);
		}
		return 1;
	}


	public static int CL_SetAcceptQuestOption( ILuaState lua )
	{
		uint questid  	= lua.L_CheckUnsigned(1);
		

        CNPCTalkFrame pFrame = (CNPCTalkFrame)GameUIManager.Instance().GetFrame(GUIDefine.UIF_NPCTALKFRAME);
        if (pFrame != null)
        {
            pFrame.SetAcceptTalkOption(questid);
        }

		return 0;
	}


	public static int CL_SetCompleteQuestOption( ILuaState lua )
	{
		uint questid  	= lua.L_CheckUnsigned(1);

        CNPCTalkFrame pFrame = (CNPCTalkFrame)GameUIManager.Instance().GetFrame(GUIDefine.UIF_NPCTALKFRAME);
        if (pFrame != null)
        {
            pFrame.SetCompleteTalkOption(questid);
        }
		return 0;
	}

	public static void LuaOpenCommonLib( ILuaState lua )
	{
		if( lua != null )
		{
			lua.L_RequireF( ToolLib.LIBNAME, 
			               ToolLib.OpenLib, true );
		}
	}
}


