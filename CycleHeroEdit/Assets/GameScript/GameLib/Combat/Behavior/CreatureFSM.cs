using UnityEngine;
using System;





class CreatureFSM 
{


    private CSceneEntity                m_pOwner;			
	private BehaviorType				m_curBehavior;		
	private CBehavior[]					m_arrayBehavir;		


	public CreatureFSM( CNPCEntity pOwner )
	{
		m_pOwner = pOwner;

		m_arrayBehavir			= new CBehavior[14];
		CIdleBehavior Idle 		= new CIdleBehavior();
		Idle.InitFSM (pOwner);
        m_arrayBehavir[0]       = null;
		m_arrayBehavir[0] 		= Idle;

		CMoveBehavior walk		= new CMoveBehavior();
		walk.InitFSM( m_pOwner );
        m_arrayBehavir[1]       = null;
		m_arrayBehavir[1] 		=  walk;

		CAttackBehavior attack 	= new CAttackBehavior();
		attack.InitFSM (m_pOwner);
        m_arrayBehavir[2]       = null;
		m_arrayBehavir[2] 		= attack;

		CDeadBehavior Dead 		= new CDeadBehavior();
		Dead.InitFSM (m_pOwner);
        m_arrayBehavir[3]       = null;
		m_arrayBehavir[3] 		= Dead;

		CBeAtkBehavior BeAtk 	= new CBeAtkBehavior();
		BeAtk.InitFSM (m_pOwner);
		m_arrayBehavir [4] 		= BeAtk;

		CHitFlyBehavior HitFly 	= new CHitFlyBehavior();
		HitFly.InitFSM (m_pOwner);
        m_arrayBehavir[5]       = null;
		m_arrayBehavir[5] 		= HitFly;

		CRepelBehavior Repel 	= new CRepelBehavior();
		Repel.InitFSM (m_pOwner);
        m_arrayBehavir[6]       = null;
		m_arrayBehavir[6] 		= Repel;

		CAssaultBehavior Assault= new CAssaultBehavior();
		Assault.InitFSM (m_pOwner);
        m_arrayBehavir[7]       = null;
		m_arrayBehavir[7] 		= Assault;

		CPullBehavior Pull		= new CPullBehavior();
		Pull.InitFSM (m_pOwner);
        m_arrayBehavir[8]       = null;
		m_arrayBehavir[8] 		= Pull;

		CDazzyBehavior Dazzy	= new CDazzyBehavior();
		Dazzy.InitFSM (m_pOwner);
        m_arrayBehavir[9]       = null;
		m_arrayBehavir[9] 		= Dazzy;

		CTieBehavior Tie		= new CTieBehavior();
		Tie.InitFSM (m_pOwner);
        m_arrayBehavir[10]      = null;
		m_arrayBehavir[10] 	    = Tie;

		CSoporBehavior Sopor	= new CSoporBehavior();
		Sopor.InitFSM (m_pOwner);
        m_arrayBehavir[11]      = null;
		m_arrayBehavir[11] 	    = Sopor;

		CHitDownBehavior HitDown= new CHitDownBehavior();
		HitDown.InitFSM (m_pOwner);
        m_arrayBehavir[12]      = null;
		m_arrayBehavir[12] 	    = HitDown;
		
		CSuspensionBehavior Susp= new CSuspensionBehavior();
		Susp.InitFSM (m_pOwner);
        m_arrayBehavir[13]      = null;
		m_arrayBehavir[13] 	    = Susp;

	}

	~CreatureFSM( )
	{

	}

	//-----------------------------------------------------------------------------------------
	// 更新行为状态机
	//-----------------------------------------------------------------------------------------
	public void Update( )
	{

		if (m_curBehavior >= 0 && m_curBehavior < BehaviorType.EState_Num) 
		{
			int index  = (int)m_curBehavior;
			m_arrayBehavir[index].Update( );
		}
	}


	//-----------------------------------------------------------------------------------------
	// 接受动画底层发来的事件处理接口
	//-----------------------------------------------------------------------------------------
	public void doAnimEvent( ANIMEVENTTYPE eEvent, string strParam )
	{
		// 需要考虑那些事件需要往下分派，那些需要在这层处理了
		if ( eEvent == ANIMEVENTTYPE.AET_AttackEnemy && m_curBehavior == BehaviorType.EState_Skill ) 
		{
			CAttackBehavior attack 	= (CAttackBehavior)m_arrayBehavir [2];
			attack.doAnimEvent( strParam );
		}
	}


	//-----------------------------------------------------------------------------------------
	// 切换行为
	//-----------------------------------------------------------------------------------------
	public void ChangeBeHavior( BehaviorType eBeHavior, uint dwParam )
	{

		if (m_curBehavior != eBeHavior) 
		{
			int idx = (int)m_curBehavior;
			m_arrayBehavir[idx].Exit();
		}

		Common.DEBUG_MSG("on_itemMouseOver: " + eBeHavior);
		m_curBehavior = eBeHavior;
		int i = (int)m_curBehavior;
		m_arrayBehavir[i].Enter( dwParam );

	}


	//-----------------------------------------------------------------------------------------
	// 切换到死亡行为
	//-----------------------------------------------------------------------------------------
	public void Change2DeadBeHavior( )
	{
		if (m_curBehavior != BehaviorType.EState_Dead) 
		{
			ChangeBeHavior( BehaviorType.EState_Dead, 0 );
		}
	}

	//-----------------------------------------------------------------------------------------
	// 切换到空闲行为
	//-----------------------------------------------------------------------------------------
	public void Change2IdleBeHavior( )
	{
	    ChangeBeHavior( BehaviorType.EState_Idle, 0 );
	}

	//-----------------------------------------------------------------------------------------
	// 切换到被击行为
	//-----------------------------------------------------------------------------------------
	public void Change2BeAtkBeHavior( )
	{
		
		ChangeBeHavior( BehaviorType.EState_BeAttack, 0 );
	}

	//-----------------------------------------------------------------------------------------
	// 是否在状态
	//-----------------------------------------------------------------------------------------
	public bool IsInMoveing()
	{
		if ( m_curBehavior == BehaviorType.EState_Move )
			return true;
		
		return false;
	}

	//-----------------------------------------------------------------------------------------
	// 是否在空闲状态
	//-----------------------------------------------------------------------------------------
	public bool IsInBehavior( BehaviorType eType )
	{
		if ( m_curBehavior == eType )
			return true;
		
		return false;
	}


	//-----------------------------------------------------------------------------------------
	// 是否在状态
	//-----------------------------------------------------------------------------------------
	public bool IsCanMove()
	{
		if ( m_curBehavior == BehaviorType.EState_Skill ||
		     m_curBehavior == BehaviorType.EState_Dead  || 
		     m_curBehavior == BehaviorType.EState_Repel  ||
		     m_curBehavior == BehaviorType.EState_Pull  ||
		     m_curBehavior == BehaviorType.EState_Dazzy  ||
		     m_curBehavior == BehaviorType.EState_Tie  ||
		     m_curBehavior == BehaviorType.EState_Sopor  ||
		     m_curBehavior == BehaviorType.EState_HitDown  ||
		     m_curBehavior == BehaviorType.EState_Suspension )
			return false;
		
		return true;
	}
}




