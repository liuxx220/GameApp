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
/// �����ͷ���������
/// </summary>
/// ------------------------------------------------------------------
public enum ESKILLFINGERTYPE
{
    FST_spot            = 0,	// �����
	FST_doublehit       = 1,    // �����[���ε���ʱ����С��1��]
	FST_drawline		= 2,	// ����	
    FST_circle          = 3,    // ��Բ
	FST_scale           = 4,    // ��ָ����[������ָ]	
    FST_zoomin          = 5,    // ��ָ�ſ�[������ָ]
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




