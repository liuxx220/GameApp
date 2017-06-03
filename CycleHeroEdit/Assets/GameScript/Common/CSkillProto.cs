using UnityEngine;
using System;





public enum ESkillType
{
	ESUT_Norm			=	0,		
	ESUT_Role			=	1,		
	ESUT_Monster		=	2,		
}


public enum ESkillUseType
{
	ESUT_Null			=	0,
	ESUT_Active			=	1,		
	ESUT_Passive		=	2,		
}


/// ------------------------------------------------------------------
/// <summary>
/// 技能释放手势类型
/// </summary>
/// ------------------------------------------------------------------
public enum ESKILLFINGERTYPE
{
    FST_spot            = 0,	// 单点击
	FST_doublehit       = 1,    // 连点击[两次单击时间间隔小于1秒]
	FST_drawline		= 2,	// 画线	
    FST_circle          = 3,    // 画圆
	FST_scale           = 4,    // 两指收缩[最少两指]	
    FST_zoomin          = 5,    // 两指张开[最少两指]
}


public enum SkillPriority
{
	SKP_Low				= 0,	
	SKP_Normalize		= 1,	
	SKP_High			= 2,	
	SKP_Highest			= 3		
}


public enum ESkillDmgType
{
	ESDGT_Null			=	0,		
	ESDGT_Physical		=	1,		
	ESDGT_Energy		=	2,		
}


enum ESkillCostType
{
	ESCT_HP			=	0,		
	ESCT_MP			=	1,		
	ESCT_End
}

class tagSkillProto
{

	public uint				dwID;

	public ESkillType		eType;			
	public ESkillUseType	eUseType;		
	public SkillPriority	ePriority;		
	public ESkillDmgType	eDmgType;		
	public ESkillCostType	eCostType;
    public ESKILLFINGERTYPE eFingerType;		

	public float			fOPDist;		
	public float			fOPRadius;		
	public int				nDmgValues;		
	public int				nCostValue;		


	public int				nPrepareTime;	
	public int				nPilotTime;		
	public int				nPilotNum;		

	public string			strIcon;		
	public string       	strName;		
	public string 			strdesc;		


	public int				ActID;			
	public string			strPrefabFile;	
	public int				nCoolDown;		
	
}




