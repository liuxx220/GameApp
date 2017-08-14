using UnityEngine;





/** \class CPursueAI
	\brief NPC返回行为AI状态
*/
class CReturnAI : CAIStats
{

	public static CReturnAI		m_instance;	
	public CReturnAI()
	{

	}
	
	~CReturnAI()
	{
	
	}

	//-----------------------------------------------------------------------------------------
	// 状态的静态调用
	//-----------------------------------------------------------------------------------------
	public static CReturnAI Instance()
	{
		if (m_instance == null) 
		{
			m_instance = new CReturnAI();
		}
		
		return m_instance;
	}

	//-----------------------------------------------------------------------------------------
	// 进入本行为
	//-----------------------------------------------------------------------------------------
	public override void Enter( CAIController pAI )
	{
		CNPCEntity pOwner = pAI.m_pOwner;
		if (pOwner == null)
			return;

		pAI.StartReturn();

		pAI.ClearAllEnmity();
		pAI.m_pTarget = null;
		pOwner.SetState (EState.ES_Invincible);
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Exit( CAIController pAI )
	{
		CNPCEntity pOwner = pAI.m_pOwner;
		if (pOwner == null)
			return;

		// 回满蓝和血
		pOwner.SetAttValue (ERoleAttribute.ERA_HP, pOwner.GetAttValue (ERoleAttribute.ERA_HP), false );
		pOwner.SetAttValue (ERoleAttribute.ERA_MP, pOwner.GetAttValue (ERoleAttribute.ERA_MP), false );

		pOwner.UnSetState (EState.ES_Invincible);
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Update( CAIController pAI )
	{
		pAI.UpdateReturn();
	}
}




