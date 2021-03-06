using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;




//----------------------------------------------------------------
// 游戏内生物种类
//----------------------------------------------------------------
enum ECreatureType
{
	ECT_NULL		=	0,
	ECT_Monster		=	1,		// 怪物
	ECT_GameObject	=	2,		// 可交互的地物
	ECT_NPC			=	3,		// 普通NPC
	ECT_MovieObject =	4		// 不可交互的地物 (不可点击 不可锁定)
}


//----------------------------------------------------------------
// 动画类型
//----------------------------------------------------------------
enum EAniType
{
	EAT_None		=	0,		// 静态
	EAT_KeyFrame	=	1,		// 关键帧
	EAT_Skeleton				// 骨骼动画
}

//------------------------------------------------------------------
// 怪物级别
//------------------------------------------------------------------
enum ERank
{
	ER_Null,			// 无
	ER_Normal,			// 普通
	ER_Elite,			// 精英
	ER_Rare,			// 稀有， BOSS
	ER_Quest,			// 任务
	ER_Leader,			// 头目
	ER_Special,			// 特殊
}


// 怪物实体配置定义
class tagEntityProto
{

	public uint				dwTypeID;
	public ECreatureType	eType;			// 怪物的类别
	public int				nLevel;			// 怪物等级

	public uint 			dwLootID;		// 掉落ID
	public uint				dwNormalSkill;	// 普通技能
	public uint 			dwLongDisSkill;

	public bool				bCanAttack;
	public bool				bCanBeAttack;

	public int				nRespawnTime;
	public int 				nLiveTime;
	public int				nExpGive;

	public string 			szName;			// 怪物名称
	public string			szIcon;
	public string 			szModelName;	// 怪物实体模型

	public int[]			nBaseAtt;		// 怪物的基础属性

	public float			fLookdistance;	// 锁敌范围
	public float 			nPatrolRadius;	// 的移动半径

}