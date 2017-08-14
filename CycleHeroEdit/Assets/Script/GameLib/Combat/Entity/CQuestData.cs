using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;





class tagQuestTrack
{

	public bool		IsTracked;		// is can track quest
	public bool		IsLimit;		// quest is have time
	public float	fTime;

	public uint[]	completeInfo;
	public int[]	nTargetnum;
	public EQuestState	dwFlag;

	public tagQuestTrack( )
	{
		IsTracked 	= false;
		IsLimit 	= false;

		completeInfo 	= new uint[3];
		nTargetnum		= new int[3];
	}
}


class CQuestData
{

	private Dictionary< uint, tagQuestProto >	m_QuestData;
	private Dictionary< uint, tagQuestText >	m_QuestInfo;


	public CQuestData()
	{
		m_QuestData = new Dictionary<uint, tagQuestProto> ();
		m_QuestInfo = new Dictionary<uint, tagQuestText> ();
	}

	// 对应任务的 quest_name.xml
	public void LoadQuestName( )
	{

		CXmlContainer xml 		 = new CXmlContainer ();
		List< string > questlist = new List<string > ();
		if (xml.LoadXML ("data/quest_name", "id", questlist)) 
		{
			for( int i = 0; i < questlist.Count; i++ )
			{
				tagQuestText quest 	= new tagQuestText();
				quest.id 			= xml.GetDword( "id",   		questlist[i] );
				quest.name			= xml.GetString( "name",		questlist[i] );
				quest.AcceptTalk	= xml.GetString( "AcceptTalk",	questlist[i] );  
				quest.CompleteTalk	= xml.GetString( "CompleteTalk",questlist[i] );
				m_QuestInfo.Add( quest.id, quest );
			}	
		}
	}

	public void LoadQuestProto( )
	{
		CXmlContainer xml 		 = new CXmlContainer ();
		List< string > questlist = new List<string > ();
		if (xml.LoadXML ("data/quest_proto", "id", questlist)) 
		{

			for( int i = 0; i < questlist.Count; i++ )
			{
				tagQuestProto quest 			= new tagQuestProto();
				quest.id 						= xml.GetDword( "id",   		questlist[i] );
				quest.type						= (EQuestEvent)xml.GetInt( "type",			questlist[i], 0 );
				quest.prev_quest_id 			= xml.GetDword( "prev_quest", 	questlist[i] );
				quest.next_quest_id				= xml.GetDword( "next_quest",	questlist[i] );
				quest.limit_time				= xml.GetDword( "limit_time",	questlist[i] );
				quest.limit_level				= xml.GetInt( "limit_level",	questlist[i], -1 );
				quest.limit_class				= xml.GetInt( "limit_class",    questlist[i], -1 );
				quest.week						= (EWeek)xml.GetInt( "week",	questlist[i], 0 ); 
				int repeata						= xml.GetInt( "canrepeata", questlist[i], 0 );
				quest.repeatable				= Convert.ToBoolean(repeata);

				quest.accept_quest_npc 			= xml.GetDword( "accept_npc",	questlist[i] );
				quest.complete_quest_npc		= xml.GetDword( "complete_npc", questlist[i] );

				quest.accept_req_item[0]		= xml.GetDword( "accept_req_item1", 	questlist[i] );
				quest.accept_req_item_num[0]	= xml.GetInt( 	"accept_req_item_num1", questlist[i], 1 );
				quest.accept_req_item[1]		= xml.GetDword( "accept_req_item2", 	questlist[i] );
				quest.accept_req_item_num[1]	= xml.GetInt( 	"accept_req_item_num2", questlist[i], 1 );
				quest.accept_req_item[2]		= xml.GetDword( "accept_req_item3", 	questlist[i] );
				quest.accept_req_item_num[2]	= xml.GetInt( 	"accept_req_item_num3", questlist[i], 1 );

				quest.complete_req_data[0]		= xml.GetDword( "complete_req_item1", 		questlist[i] );
				quest.complete_req_data_num[0]	= xml.GetInt( 	"complete_req_item_num1", 	questlist[i], 1 );
				quest.complete_req_data[1]		= xml.GetDword( "complete_req_item2", 		questlist[i] );
				quest.complete_req_data_num[1]	= xml.GetInt( 	"complete_req_item_num2", 	questlist[i], 1 );
				quest.complete_req_data[2]		= xml.GetDword( "complete_req_item3", 		questlist[i] );
				quest.complete_req_data_num[2]	= xml.GetInt( 	"complete_req_item_num3", 	questlist[i], 1 );

				quest.rew_type					= xml.GetInt( "rew_type", 	questlist[i], 0 );
				quest.rew_value					= xml.GetInt( "rew_value", 	questlist[i], 0 );
				quest.rew_type2					= xml.GetInt( "rew_type1", 	questlist[i], 0 );
				quest.rew_value2				= xml.GetInt( "rew_value1", questlist[i], 0 );

				quest.rew_sel_type				= xml.GetInt( 	"rew_item_seltype", questlist[i], 0 );
				quest.rew_item[0]				= xml.GetDword( "rew_item1", 		questlist[i] );
				quest.rew_item_num[0]			= xml.GetInt( 	"rew_item_num1", 	questlist[i], 1 );
				quest.rew_item[1]				= xml.GetDword( "rew_item2", 		questlist[i] );
				quest.rew_item_num[1]			= xml.GetInt( 	"rew_item_num2", 	questlist[i], 1 );
				quest.rew_item[2]				= xml.GetDword( "rew_item3", 		questlist[i] );
				quest.rew_item_num[2]			= xml.GetInt( 	"rew_item_num3", 	questlist[i], 1 );


				m_QuestData.Add( quest.id, quest );
			}	
		}
	}

	public tagQuestProto GetQuestProto( uint questid )
	{
		tagQuestProto pProto = null;
		m_QuestData.TryGetValue (questid, out pProto);
		if( pProto != null )
		{
			return pProto;
		}
		return null;
	}

	public tagQuestText	GetQuestText( uint questid )
	{
		tagQuestText pQuestText = null;
		m_QuestInfo.TryGetValue (questid, out pQuestText);
		if( pQuestText != null )
		{
			return pQuestText;
		}

		return null;
	}
}
