using UnityEngine;
using System;





public enum  AIStateType
{
	AIST_Idle			= 0,		
	AIST_Pursue			= 1,		
	AIST_Attack			= 2,		
	AIST_Return			= 3,		
	AIST_Flee			= 4,		
	AIST_SOS			= 5
}



class AITransition
{

	public virtual AIStateType Transition( CAIController pAI, AIStateType eCurState )
	{
		return AIStateType.AIST_Idle;
	}
}


class AggressiveTransition : AITransition
{

	public AggressiveTransition()
	{
		
	}
	
	~AggressiveTransition( )
	{
		
	}
	
	public override AIStateType Transition( CAIController pAI, AIStateType eCurState )
	{
		if (eCurState == AIStateType.AIST_Idle) 
		{
			IdleTransition( pAI );
		}

		else if (eCurState == AIStateType.AIST_Pursue) 
		{
			PursueTransition(pAI);
		}

		else if (eCurState == AIStateType.AIST_Attack) 
		{
			AttackTransition( pAI );
		}

		else if (eCurState == AIStateType.AIST_Return) 
		{
			ReturnTransition( pAI );
		}

		return AIStateType.AIST_Idle;
	}

	private AIStateType IdleTransition( CAIController pAI )
	{
		if (pAI.m_pTarget == null) 
		{
			return AIStateType.AIST_Idle;
		}

		if (pAI.IsInAttackDistance ()) 
		{
			return AIStateType.AIST_Attack;
		} 
		else 
		{
			return AIStateType.AIST_Pursue;
		}
	}

	private AIStateType PursueTransition( CAIController pAI )
	{
		if (pAI.m_pTarget == null) 
		{
			return AIStateType.AIST_Return;
		}

		if (pAI.IsInAttackDistance ()) 
		{
			return AIStateType.AIST_Attack;
		} 

		return AIStateType.AIST_Pursue;
	}

	private AIStateType AttackTransition( CAIController pAI )
	{
		if (pAI.m_pTarget == null) 
		{
			return AIStateType.AIST_Return;
		}

		if ( !pAI.IsInAttackDistance () ) 
		{
			return AIStateType.AIST_Pursue;
		} 

		return AIStateType.AIST_Attack;
	}

	private AIStateType ReturnTransition( CAIController pAI )
	{
		if (pAI.m_bArrivedReturnPos) 
		{
			return AIStateType.AIST_Idle;
		}

		return AIStateType.AIST_Return;
	}
}


class GuardTransition : AITransition
{

	public GuardTransition()
	{

	}

	~GuardTransition( )
	{

	}

	public override AIStateType Transition( CAIController pAI, AIStateType eCurState )
	{
		if (eCurState == AIStateType.AIST_Idle) 
		{
			return IdleTransition( pAI );
		}
		
		else if (eCurState == AIStateType.AIST_Pursue) 
		{
			return PursueTransition(pAI);
		}
		
		else if (eCurState == AIStateType.AIST_Attack) 
		{
			return AttackTransition( pAI );
		}
		
		else if (eCurState == AIStateType.AIST_Return) 
		{
			return ReturnTransition( pAI );
		}
		
		return AIStateType.AIST_Idle;
	}
	
	private AIStateType IdleTransition( CAIController pAI )
	{
		if (pAI.m_pTarget == null) 
		{
			return AIStateType.AIST_Idle;
		}
		
		if (pAI.IsInAttackDistance ()) 
		{
			return AIStateType.AIST_Attack;
		} 
		else 
		{
			return AIStateType.AIST_Pursue;
		}
	}
	
	private AIStateType PursueTransition( CAIController pAI )
	{
		if (pAI.m_pTarget == null) 
		{
			return AIStateType.AIST_Return;
		}
		
		if (pAI.IsInAttackDistance ()) 
		{
			return AIStateType.AIST_Attack;
		} 
		
		return AIStateType.AIST_Pursue;
	}
	
	private AIStateType AttackTransition( CAIController pAI )
	{
		if (pAI.m_pTarget == null) 
		{
			return AIStateType.AIST_Return;
		}
		
		if ( !pAI.IsInAttackDistance () ) 
		{
			return AIStateType.AIST_Pursue;
		} 
		
		return AIStateType.AIST_Attack;
	}
	
	private AIStateType ReturnTransition( CAIController pAI )
	{
		if (pAI.m_bArrivedReturnPos) 
		{
			return AIStateType.AIST_Idle;
		}
		
		return AIStateType.AIST_Return;
	}
}