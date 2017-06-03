using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;




/** \class CSuspensionBehavior
	\brief 悬浮行为
*/
class CSuspensionBehavior : CBehavior
{

	Vector3  	m_vStartPos = Vector3.zero;
	public CSuspensionBehavior()
	{
		
	}
	
	~CSuspensionBehavior()
	{
		
	}
	
	//-----------------------------------------------------------------------------------------
	// 进入本行为
	//-----------------------------------------------------------------------------------------
	public override void Enter( uint dwParam )
	{
		if (m_pOwner != null) 
		{
			m_vStartPos = m_pOwner.gameObject.transform.position;
			m_pOwner.SetState( EState.ES_Suspension );
		}

		// 被攻击欣慰
		if (m_CtrlSkelton != null) 
		{
			
			if( m_CtrlSkelton.m_nCurPlayActID != ACTID.ACT_Damage03 )
			{
				m_CtrlSkelton.PlayTrack( ACTID.ACT_Damage03 );
			}
			else
			{
				m_CtrlSkelton.PlayTrack( ACTID.ACT_Damage04 );
			}
		}
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Exit()
	{
		if (m_pOwner != null) 
		{
			m_vStartPos = m_pOwner.gameObject.transform.position;
			m_pOwner.UnSetState( EState.ES_Suspension );
		}
	}
	
	//-----------------------------------------------------------------------------------------
	// 退出本行为
	//-----------------------------------------------------------------------------------------
	public override void Update( )
	{
		if (m_pOwner == null)
			return;

		if (m_pOwner.gameObject == null)
			return;


		Vector3 _Vector3 = Vector3.zero;
		_Vector3.x = m_pOwner.gameObject.transform.position.x;
		_Vector3.z = m_pOwner.gameObject.transform.position.z;
		_Vector3.y = m_pOwner.gameObject.transform.position.y - 10 * Time.deltaTime;
		
		if (_Vector3.y <= m_vStartPos.y )
			_Vector3.y = m_vStartPos.y;
		
		m_pOwner.gameObject.transform.position = _Vector3;

	}

	//-----------------------------------------------------------------------------------------
	// 处理动作底层发来的事件
	//-----------------------------------------------------------------------------------------
	public override void doAnimEvent(  string strParam )
	{
		
	}

}





