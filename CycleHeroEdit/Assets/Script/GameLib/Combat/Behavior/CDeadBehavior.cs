using UnityEngine;
using System;





class CDeadBehavior : CBehavior
{

	float  m_alphevalue = 1.0f;
	float  m_ffadeout   = 1.5f;
	public CDeadBehavior()
	{
		
	}
	
	~CDeadBehavior()
	{

	}
	
	
	//-----------------------------------------------------------------------------------------
	// 进入本行为
	//-----------------------------------------------------------------------------------------
	public override void Enter( uint dwParam )
	{
		if (m_CtrlSkelton != null) 
		{
			m_ffadeout = 1.5f;
			m_CtrlSkelton.PlayTrack( ACTID.ACT_Dead );
		}
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Exit()
	{
		if (m_pOwner.gameObject != null) 
		{
            m_pOwner.gameObject.SetActive(false);
		}
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Update( )
	{
        if (m_pOwner.gameObject == null)
			return;

		float fAlpha = (m_ffadeout - Time.deltaTime) / 1.5f;
		if (fAlpha > 1.0f) 
		{
			fAlpha = 1.0f;
            m_pOwner.gameObject.SetActive(false);
		}
		//m_GameObject.renderer.material.color.a = 1.0f - fAlpha;
	}

	//-----------------------------------------------------------------------------------------
	// 处理动作底层发来的事件
	//-----------------------------------------------------------------------------------------
	public override void doAnimEvent(  string strParam )
	{
		
	}
}

