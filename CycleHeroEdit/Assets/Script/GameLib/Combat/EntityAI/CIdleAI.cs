using UnityEngine;




/** \class CPursueAI
	\brief NPC空闲AI状态
*/
class CIdleAI : CAIStats
{

	public static CIdleAI		m_instance;	
	public CIdleAI( )
	{
		
	}
	
	~CIdleAI()
	{
		
	}

	//-----------------------------------------------------------------------------------------
	// 状态的静态调用
	//-----------------------------------------------------------------------------------------
	public static CIdleAI Instance()
	{
		if (m_instance == null) 
		{
			m_instance = new CIdleAI();
		}

		return m_instance;
	}

	//-----------------------------------------------------------------------------------------
	// 进入本行为
	//-----------------------------------------------------------------------------------------
	public override void Enter( CAIController pAI )
	{

		// 清楚所有仇恨
		pAI.m_pTarget = null;

		pAI.ClearAllEnmity( );

		// 重置巡逻和锁敌的时间
		pAI.ReSetPatrolRestTime( );
		pAI.ReSetLookForTargetTime( );
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Exit( CAIController pAI )
	{
		
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Update( CAIController pAI )
	{
		if (pAI.m_pOwner != null) 
		{
			pAI.UpdatePatrol();
		}

		pAI.UpdateLookForEnemy ();
	}
}





