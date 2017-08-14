using UnityEngine;
using System;
using System.Linq;
using System.Collections;






class CAIController
{

	public CNPCEntity 				m_pOwner;
    public CSceneEntity             m_pTarget;
	public tagEntityProto			m_pProto;

	private GuardTransition			m_pTransition;
	
	private AIStateType				m_eCurAIState;				
	private CAIStats				m_pCurrentState;			

	private bool					m_bIsPatroling;				
	private float					m_fLockTargetTime;
	private float					m_fPatrolResetTime;			
	private float					m_fLookForTargetTime;		
	private float					m_fNextAttackWaitTime;		

	public bool						m_bArrivedReturnPos = true;
	private UnityEngine.GameObject	m_GameObject;
	private Vector3					m_vBornPos = Vector3.zero;
	private Vector3					m_vPursueTargetPos = Vector3.zero;
	
	public CAIController( CNPCEntity pNPC )
	{
		m_pOwner 			= pNPC;
		m_pProto 			= m_pOwner.m_pProto;
		m_pTarget 			= null;
		m_eCurAIState 		= AIStateType.AIST_Idle;
		m_pCurrentState 	= CIdleAI.Instance ();
		m_pTransition 		= new GuardTransition ();

		m_GameObject 		= m_pOwner.gameObject;
		m_vBornPos 			= m_GameObject.transform.position;

	}
	
    /// -----------------------------------------------------------------------------------
    /// <summary>
    /// 英雄单位的AI tick
    /// </summary>
    /// ----------------------------------------------------------------------------------
	public void Update()
	{
		UpdateTransition ();
		UpdateCurrentState ();
	}
	
     /// ----------------------------------------------------------------------------------
    /// <summary>
    /// AI 状态实体
    /// </summary>
	private CAIStats GetStateByType( AIStateType eStateType )
	{
		
		if (eStateType == AIStateType.AIST_Idle) 
		{
			return (CAIStats)CIdleAI.Instance();
		} 
		
		else if (eStateType == AIStateType.AIST_Pursue) 
		{
			
			return (CAIStats)CPursueAI.Instance();	
		}
		
		else if (eStateType == AIStateType.AIST_Attack) 
		{
			return (CAIStats)CAttackAI.Instance();	
		}
		
		else if (eStateType == AIStateType.AIST_Return) 
		{
			
			return (CAIStats)CReturnAI.Instance();	
		}

		return null;
	}
	
	private void UpdateTransition()
	{
		AIStateType eNewState = m_pTransition.Transition ( this, m_eCurAIState );
		if (m_eCurAIState != eNewState) 
		{
			ChangeState( eNewState );
		}
	}
	
	private void UpdateCurrentState()
	{
		if (m_pCurrentState != null)
			m_pCurrentState.Update ( this );
	}

	public void ChangeState( AIStateType eState )
	{
		
		if (m_pCurrentState != null)
			m_pCurrentState.Exit ( this );
		
		m_eCurAIState 	= eState;
		m_pCurrentState = GetStateByType (m_eCurAIState);
		
		if (m_pCurrentState != null)
			m_pCurrentState.Enter( this );
		
	}

	public void ReSetLookForTargetTime()
	{
		m_fLookForTargetTime = UnityEngine.Random.Range (2.0f, 4.0f);
	}
	

	public void ReSetPatrolRestTime()
	{
		m_fPatrolResetTime 	= UnityEngine.Random.Range (2.0f, 3.0f);
	}


	public bool	IsPatroling()
	{
		return m_bIsPatroling;
	}


	public bool IsInAttackDistance()
	{
		if (m_pTarget == null)
			return false;

		float fdis = ( m_GameObject.transform.position - m_pTarget.gameObject.transform.position ).magnitude;
		if (m_pOwner.m_pRangedSkill != null)
		{
			if( fdis <= m_pOwner.m_pRangedSkill.GetDist() )
				return true;
		}

		if (m_pOwner.m_pMeleeSkill != null) 
		{
			if( fdis <= m_pOwner.m_pMeleeSkill.GetDist() )
				return true;
		}

		return false;
	}

	public void AttackTarget()
	{

		if( m_fNextAttackWaitTime > 0 ) 
		{
			m_fNextAttackWaitTime -= Time.deltaTime;
			return;
		}

		if (m_pTarget == null)
			return ;

        CreatureFSM pFSM = m_pOwner.m_FSM;
		if ( pFSM == null) 
		{
			return;
		}

		if (m_pOwner.m_pMoveCtrl != null ) 
		{
			m_pOwner.m_pMoveCtrl.StopMove();
		}


		float fdis = (m_GameObject.transform.position - m_pTarget.gameObject.transform.position).magnitude;
		if (m_pOwner.m_pRangedSkill != null)
		{
			if( fdis <= m_pOwner.m_pRangedSkill.GetDist() )
				pFSM.ChangeBeHavior(BehaviorType.EState_Skill, m_pOwner.m_pRangedSkill.GetID() );
		}

		if (m_pOwner.m_pMeleeSkill != null) 
		{
			if( fdis <= m_pOwner.m_pMeleeSkill.GetDist() )
				pFSM.ChangeBeHavior(BehaviorType.EState_Skill, m_pOwner.m_pMeleeSkill.GetID() );
		}

		SetAttackWaitTime ();
	}  


    public void SetAttackWaitTime()
	{
		m_fNextAttackWaitTime 	= UnityEngine.Random.Range (2.0f, 3.0f);
	}


	public void StartPatrol()
	{

		if (m_GameObject == null || m_pOwner == null)
			return;

        if (m_pProto.eType == ECreatureType.ECT_NPC)
            return;

		float nRadious 	= m_pProto.nPatrolRadius;
		Vector3 vDstPos = m_GameObject.transform.position;

		vDstPos.x 		= UnityEngine.Random.Range( 0, nRadious ) + m_vBornPos.x;
		vDstPos.z 		= UnityEngine.Random.Range( 0, nRadious ) + m_vBornPos.z;
		if (m_pOwner.m_pMoveCtrl != null ) 
		{
            m_pOwner.m_pMoveCtrl.StartWalk(vDstPos, EMoveState.EMS_Walk);
			m_bIsPatroling = true;
		}
	}
	

	public void UpdatePatrol()
	{

		if ( m_bIsPatroling ) 
		{
			if( EMoveState.EMS_Stand ==  m_pOwner.m_pMoveCtrl.m_eCurMove )
			{
				m_bIsPatroling = false;
				ReSetPatrolRestTime();
			}

			return;
		}

		if( m_fPatrolResetTime > 0 ) 
		{
			m_fPatrolResetTime -= Time.deltaTime;
			return;
		}
		
		StartPatrol( );
	}


	public void StartLookForEnemy( )
	{
        if (m_pProto.eType == ECreatureType.ECT_NPC)
            return;

		float fMaxDist = m_pProto.fLookdistance;
		GameObject[] EnemyList = GetFronEnemy( fMaxDist );


		CHeroEntity phero = null;
		foreach ( var Enemy in EnemyList )
		{
			CPlayerCompent pAI = Enemy.GetComponent<CPlayerCompent>();
			if( pAI.m_pOwner == null )
				continue;

			if( pAI.m_pOwner.IsDead() )
				continue;

			if( pAI.m_pOwner.IsInState(EState.ES_Lurk ) )
				continue;

			float fDis = (m_GameObject.transform.position - Enemy.transform.position).magnitude;
			if( fDis < fMaxDist )
			{
				phero    = pAI.m_pOwner;
				fMaxDist = fDis;
			}
		}

		if (phero != null) 
		{
			m_pTarget = phero;
		}
	
		ReSetLookForTargetTime();
	}


	public void UpdateLookForEnemy( )
	{
		if (m_pProto.eType != ECreatureType.ECT_Monster)
			return;

		if( m_fLookForTargetTime > 0 ) 
		{
			m_fLookForTargetTime -= Time.deltaTime;
			return;
		}

		ReSetLookForTargetTime();

		StartLookForEnemy();
	}


	private GameObject[] GetFronEnemy(float distance )
	{
		
		string _Tag = "Monster";
		if (m_GameObject.tag == "Monster")
			_Tag = "Player";
		
		Transform transform = m_GameObject.transform;
		GameObject[] GameObjectArr = (	from r in GameObject.FindGameObjectsWithTag(_Tag)
		                                where Helper.isInRadious(r.transform.position, transform.position, distance )
		                                select r.gameObject).ToArray();
		
		return GameObjectArr;
	}


	public void ClearAllEnmity( )
	{
		
	}

	public void ClearEnmity( uint dwRoleID )
	{
		
	}
	
	public void AddEnmity( int dwID, int nvalue )
	{
		
	}
	
	public void CalMaxEnmity()
	{
		
	}

	public void StartPursue( )
	{

		if (m_pTarget == null)
			return;

		ReSetLockTargetTime();

		if (m_pOwner.m_pMoveCtrl != null ) 
		{
            m_pOwner.m_pMoveCtrl.StartWalk(m_pTarget.gameObject.transform.position, EMoveState.EMS_Run);
		}
	}

	public void UpdatePursue( )
	{

		if (m_fLockTargetTime > 0) 
		{
			m_fLockTargetTime -= Time.deltaTime;
			return;
		}

		if( EMoveState.EMS_Stand ==  m_pOwner.m_pMoveCtrl.m_eCurMove )
		{
			StartPursue();
			return;
		}
	}


	public void SetPursueTargetPos( )
	{
		if( m_pTarget != null )
			m_vPursueTargetPos = m_pTarget.gameObject.transform.position;
	}


	public void ReSetLockTargetTime( )
	{
		m_fLockTargetTime 	= UnityEngine.Random.Range (0, 1);
	}

	public void StartReturn()
	{

		m_bArrivedReturnPos = false;
		if (m_pOwner.m_pMoveCtrl != null ) 
		{
            m_pOwner.m_pMoveCtrl.StartWalk(m_vPursueTargetPos, EMoveState.EMS_Run);
		}
	}


	public void UpdateReturn()
	{
		if( EMoveState.EMS_Stand ==  m_pOwner.m_pMoveCtrl.m_eCurMove )
		{
			m_bArrivedReturnPos = true;
		}
	}
}

	

