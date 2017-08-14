using UnityEngine;
using System;




/** \
	\brief 行为状态枚举
*/

//-----------------------------------------------------------------------------
// 怪物行为状态类型
//-----------------------------------------------------------------------------
public enum BehaviorType
{
	EState_Idle			= 0,		
	EState_Move			= 1,		
	EState_Skill		= 2,		
	EState_Dead			= 3,		
	EState_BeAttack		= 4,		
	EState_HitFly		= 5,		
	EState_Repel		= 6,		
	EState_Assault		= 7,		
	EState_Pull			= 8,		
	EState_Dazzy		= 9,		
	EState_Tie			= 10,		
	EState_Sopor		= 11,		
	EState_HitDown		= 12,  		
	EState_Suspension	= 13,		
	EState_Num			= 14,		
}


//-----------------------------------------------------------------------------
// 普通攻击的三段
//-----------------------------------------------------------------------------
public enum NormalizeSeg
{
	NATK_Null			= 0,
	NATK_Seg0			= 1,		
	NATK_Seg1			= 2,		
	NATK_Seg2			= 3,		
}


//-----------------------------------------------------------------------------
// 动作事件类型枚举
//-----------------------------------------------------------------------------
public enum ANIMEVENTTYPE
{
	AET_Null			= 0,
	AET_AttackEnemy		= 1,
}


//-----------------------------------------------------------------------------
// 动作名称到动作ID的映射
//-----------------------------------------------------------------------------
public enum ACTID
{

	ACT_Run				= 101,
	ACT_Idel			= 100,
	ACT_ClimbUp			= 151,

	ACT_DoubleHit01		= 201,
	ACT_DoubleHit02		= 202,
	ACT_DoubleHit03		= 203,

	ACT_Skill01			= 301,
	ACT_Skill02			= 302,
	ACT_Skill03			= 303,


	ACT_Damage01		= 401,
	ACT_Damage02		= 402,
	ACT_Damage03		= 411,
	ACT_Damage04		= 412,
	ACT_Damage05		= 413,
	ACT_Damage06		= 414,
	ACT_Dead			= 444,
}
