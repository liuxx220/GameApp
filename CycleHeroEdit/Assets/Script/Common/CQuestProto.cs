using System;








enum EQuestEvent
{

	EQE_Null		= 0,
	EQE_Talk		= 1,
	EQE_Kill		= 2,
	EQE_Item		= 3,
	EQE_Trigger		= 4,
	EQE_Invest		= 5,
	EQE_UseItem		= 6,
	EQE_UseSkill	= 7,
	EQE_Level		= 8,
	EQE_End
}


enum EQuestState
{
	QS_DOING		= 1,
	QS_FAILED		= 2,
	QS_SUCCESS		= 3
}

enum EWeek
{
	EW_SUN			= 0,
	EW_MON			= 1,
	EW_TUES			= 2,
	EW_WEDNES		= 3,
	EW_THURS		= 4,
	EW_FRI			= 5,
	EW_SAT			= 6
}


enum QuestAcceptFlag
{

	QAF_Player		= 0x00000001,
	QAF_System		= 0x00000002,
	QAF_Item		= 0x00000004
}



class tagQuestProto
{
	public uint		id;
	public EQuestEvent	type;

	public uint 	prev_quest_id;
	public uint		next_quest_id;

	public uint		limit_time;
	public int		limit_level;
	public int		limit_class;

	public EWeek	week;
	public bool		repeatable;

	public uint		accept_quest_npc;
	public uint		complete_quest_npc;


	public uint[]	accept_req_item;
	public int[]	accept_req_item_num;

	public uint[]   complete_req_data;
	public int[]    complete_req_data_num;

	public int     	rew_type;					// 0 role 's exp  1 team ' exp
	public int		rew_value;

	public int     	rew_type2;					// 0 jin bi 1 yuan bao
	public int		rew_value2;


	public int		rew_sel_type;			// 0 all select, 1 select one
	public uint[]	rew_item;
	public int[]	rew_item_num;

	public tagQuestProto( )
	{

		accept_req_item			= new uint[3];
		accept_req_item_num 	= new int[3];
		complete_req_data		= new uint[3];
		complete_req_data_num	= new int[3];
		rew_item 				= new uint[3];
		rew_item_num 			= new int[3];
	}
}

class tagQuestText
{
	public uint		id;
	public string   name;
	public string   AcceptTalk;
	public string   CompleteTalk;
	public string   over_view;

}

public struct tagNpcMapPos
{

	public uint  dwNpcID;
	public float fPosX;
	public float fPosY;
	public float fPosZ;
}