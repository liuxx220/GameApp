using UnityEngine;





class CAttackAI : CAIStats
{

	public static CAttackAI		m_instance;	
	public CAttackAI( )
	{
		
	}
	
	~CAttackAI()
	{
		
	}
	
	public static CAttackAI Instance()
	{
		if (m_instance == null) 
		{
			m_instance = new CAttackAI();
		}
		
		return m_instance;
	}


	public override void Enter( CAIController pAI )
	{


	}
	
	public override void Exit( CAIController pAI )
	{
		
	}
	
	public override void Update( CAIController pAI )
	{
		if (pAI.m_pTarget == null)
			return;

        CreatureFSM pFSM = pAI.m_pOwner.m_FSM;
		if ( pFSM!= null) 
		{
			pAI.AttackTarget();
		}
	}
}






