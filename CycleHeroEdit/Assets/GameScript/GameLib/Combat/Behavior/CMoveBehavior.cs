using UnityEngine;
using System;





class CMoveBehavior : CBehavior
{


	public CMoveBehavior()
	{
		m_CtrlSkelton = null;
	}
	
	~CMoveBehavior()
	{
		
	}
	
	
	//-----------------------------------------------------------------------------------------
	// 进入本行为
	//-----------------------------------------------------------------------------------------
	public override void Enter( uint dwParam )
	{
		if (m_CtrlSkelton != null)
		{
			if( dwParam == 1 )
				m_CtrlSkelton.m_RunSpeed = 1.5f;
			else
				m_CtrlSkelton.m_RunSpeed = 1.0f;
			m_CtrlSkelton.CrossFade ( ACTID.ACT_Run );
		}
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Exit()
	{
		
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Update( )
	{

	}
	
}


