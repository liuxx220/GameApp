using UnityEngine;
using System;






class CAttackBehavior : CBehavior
{
	enum EStep
	{
		EStep_Null,
		EStep_Attack,			
		EStep_NorAttack,		
		EStep_Spell,			
		EStep_Pilot				
	}


	EStep						m_step;				
	bool						m_bMoveable;		
	bool						m_bNormAttack;		

	private CNormBehavior	  	m_norAttack;		
	private CSkillBehavior	 	m_actAttack;		
	private CSkill				m_pSkill = null;

	public CAttackBehavior()
	{
		m_step 				= EStep.EStep_Null;
		m_norAttack 		= new CNormBehavior ();
		m_actAttack 		= new CSkillBehavior ();
		m_bNormAttack 		= false;
	}
	
	~CAttackBehavior()
	{
	
	}

    public override void InitFSM(CSceneEntity pNPC)
	{
		m_pOwner 	= pNPC;
		m_norAttack.InitFSM( pNPC );
		m_actAttack.InitFSM (pNPC);
	}


	public override void Enter( uint dwParam )
	{

		uint dwSkillID 	= dwParam;
		m_pSkill 		= m_pOwner.FindSkillByTypeID (dwSkillID);
		if (m_pSkill == null)
			return;

		if (m_pSkill.GetProto ().eType == ESkillType.ESUT_Norm) 
		{
			m_step = EStep.EStep_NorAttack;
			m_norAttack.Enter( m_pSkill );
		} 
		else 
		{
			m_step = EStep.EStep_Attack;
			m_actAttack.Enter( m_pSkill );
		}
	}
	
	public override void Exit()
	{
		if (m_step == EStep.EStep_Attack) 
		{
			m_actAttack.Exit();
		}
	}

	public override void Update( )
	{
		if (m_step == EStep.EStep_NorAttack) 
		{
			m_norAttack.Update();
		}

		else if (m_step == EStep.EStep_Attack) 
		{
			m_actAttack.Update();
		}
	}

	public override void doAnimEvent( string strParam )
	{
		if (m_step == EStep.EStep_NorAttack) 
		{
			m_norAttack.doAnimEvent( strParam );
		}
		
		else if (m_step == EStep.EStep_Attack) 
		{
			m_actAttack.doAnimEvent( strParam );
		}
	}

	public bool IsNormalizeAtk()
	{
		return m_bNormAttack;
	}
}




