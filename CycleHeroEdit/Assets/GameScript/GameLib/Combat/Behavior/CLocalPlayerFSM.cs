using UnityEngine;
using System;





class CLocalPlayerFSM 
{
	

	private	CHeroEntity 			m_pOwner;		
	private BehaviorType			m_curBehavior;		
	private CBehavior[]				m_arrayBehavir;		


	public CLocalPlayerFSM( CHeroEntity pOwner )
	{
		m_pOwner = pOwner;

		m_arrayBehavir			= new CBehavior[15];

		CIdleBehavior Idle 		= new CIdleBehavior();
		Idle.InitFSM (pOwner);
		m_arrayBehavir[0] 		= Idle;

		CMoveBehavior walk		= new CMoveBehavior();
		walk.InitFSM( m_pOwner );
		m_arrayBehavir[1] 		=  walk;

		CAttackBehavior attack 	= new CAttackBehavior();
		attack.InitFSM (m_pOwner);
		m_arrayBehavir [2] 		= attack;

		CDeadBehavior Dead 		= new CDeadBehavior();
		Dead.InitFSM (m_pOwner);
		m_arrayBehavir [3] 		= Dead;

		CBeAtkBehavior BeAtk 	= new CBeAtkBehavior();
		BeAtk.InitFSM (m_pOwner);
		m_arrayBehavir [4] 		= BeAtk;

		CHitFlyBehavior HitFly 	= new CHitFlyBehavior();
		HitFly.InitFSM (m_pOwner);
		m_arrayBehavir [5] 		= HitFly;

		CRepelBehavior Repel 	= new CRepelBehavior();
		Repel.InitFSM (m_pOwner);
		m_arrayBehavir [6] 		= Repel;

		CAssaultBehavior Assault= new CAssaultBehavior();
		Assault.InitFSM (m_pOwner);
		m_arrayBehavir [7] 		= Assault;

		CPullBehavior Pull		= new CPullBehavior();
		Pull.InitFSM (m_pOwner);
		m_arrayBehavir [8] 		= Pull;

		CDazzyBehavior Dazzy	= new CDazzyBehavior();
		Dazzy.InitFSM (m_pOwner);
		m_arrayBehavir [9] 		= Dazzy;

		CTieBehavior Tie		= new CTieBehavior();
		Tie.InitFSM (m_pOwner);
		m_arrayBehavir [10] 	= Tie;

		CSoporBehavior Sopor	= new CSoporBehavior();
		Sopor.InitFSM (m_pOwner);
		m_arrayBehavir [11] 	= Sopor;

		CHitDownBehavior HitDown= new CHitDownBehavior();
		HitDown.InitFSM (m_pOwner);
		m_arrayBehavir [12] 	= HitDown;

		CSuspensionBehavior Susp= new CSuspensionBehavior();
		Susp.InitFSM (m_pOwner);
		m_arrayBehavir [13] 	= Susp;

	}

	~CLocalPlayerFSM( )
	{

	}

	public void Update( )
	{

		if (m_curBehavior > 0 && m_curBehavior < BehaviorType.EState_Num) 
		{
			int index  = (int)m_curBehavior;
			m_arrayBehavir[index].Update( );
		}
	}


	public void doAnimEvent( ANIMEVENTTYPE eEvent, string strParam )
	{
		if ( eEvent == ANIMEVENTTYPE.AET_AttackEnemy && m_curBehavior == BehaviorType.EState_Skill ) 
		{
			CAttackBehavior attack 	= (CAttackBehavior)m_arrayBehavir [2];
			attack.doAnimEvent( strParam );
		}
	}

	
	public void ChangeBeHavior( BehaviorType eBeHavior, uint dwParam )
	{

		if (m_curBehavior != eBeHavior) 
		{
			int idx = (int)m_curBehavior;
			m_arrayBehavir[idx].Exit();
		}

		m_curBehavior = eBeHavior;
		int i = (int)m_curBehavior;
		m_arrayBehavir[i].Enter( dwParam );
	}


	public void Change2DeadBeHavior( )
	{
		
		if (m_curBehavior != BehaviorType.EState_Dead) 
		{
			ChangeBeHavior( BehaviorType.EState_Dead, 0 );
		}
	}

	public void Change2IdleBeHavior( )
	{
		if (m_curBehavior != BehaviorType.EState_Idle) 
		{
			ChangeBeHavior( BehaviorType.EState_Idle, 0 );
		}
	}

	public void Change2BeAtkBeHavior( )
	{

		ChangeBeHavior( BehaviorType.EState_BeAttack, 0 );
	}


	public bool IsNoAttacked()
	{
		if (m_curBehavior == BehaviorType.EState_Idle || m_curBehavior == BehaviorType.EState_Move)
			return true;

		return false;
	}

	public bool IsInAttacked()
	{
		if (m_curBehavior == BehaviorType.EState_Skill )
			return true;
		
		return false;
	}

	public bool IsInIdle()
	{
		if ( m_curBehavior == BehaviorType.EState_Idle || m_curBehavior == BehaviorType.EState_Move )
			return true;
		
		return false;
	}


	public bool IsInBehavior( BehaviorType eType )
	{
		if ( m_curBehavior == eType )
			return true;
		
		return false;
	}

	public bool IsInMoveing()
	{
		if ( m_curBehavior == BehaviorType.EState_Move )
			return true;
		
		return false;
	}


	public bool IsNormalizeAtk()
	{
		if (m_curBehavior == BehaviorType.EState_Skill) 
		{
			return true;
		}
		
		return false;
	}

	public NormalizeSeg GetNormAtkSeg()
	{
		if ( m_curBehavior == BehaviorType.EState_Skill ) 
		{
			return NormalizeSeg.NATK_Seg0;
		}
		
		return NormalizeSeg.NATK_Null;
	}
}




