using UnityEngine;




/** \class CPursueAI
	\brief NPC追击行为AI状态
*/
class CPursueAI : CAIStats
{

	public static CPursueAI		m_instance;
	public CPursueAI( )
	{

	}
	
	~CPursueAI()
	{
	
	}

	//-----------------------------------------------------------------------------------------
	// 状态的静态调用
	//-----------------------------------------------------------------------------------------
	public static CPursueAI Instance()
	{
		if (m_instance == null) 
		{
			m_instance = new CPursueAI();
		}
		
		return m_instance;
	}

	//-----------------------------------------------------------------------------------------
	// 进入本行为
	//-----------------------------------------------------------------------------------------
	public override void Enter( CAIController pAI )
	{
		if (pAI.m_pOwner == null)
			return;

		if (pAI.m_pTarget != null) 
		{
			pAI.StartPursue();
		}
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
		if (pAI.m_pOwner == null)
			return;

		if (pAI.m_pTarget != null) 
		{
			pAI.UpdatePursue ();
		}
	}
}




