//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1022
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System;





class CEffectEntity : MonoBehaviour
{

	
	public CSceneEntity     m_Launcher;
    public CSkill m_pSkill;

	void Start()
	{
		
	}


	void Update()
	{

		if (m_Launcher == null || m_pSkill == null)
			return;
	}

	//---------------------------------------------------------------------------------
	// 进入碰撞
	//---------------------------------------------------------------------------------
	void OnCollisionEnter( Collision collisionInfo )
	{
		if (collisionInfo.gameObject != null) 
		{
			CtrolEnenyAI health = collisionInfo.gameObject.GetComponent<CtrolEnenyAI>();
			if( health != null && m_Launcher != null )
			{
				m_Launcher.m_combat.CalculateDmg( health.m_pOwner, m_pSkill, "" );
			}
		}
	}
	

	//---------------------------------------------------------------------------------
	// 当逗留碰撞器
	//---------------------------------------------------------------------------------
	void OnCollisionStay( Collision collisionInfo )
	{
		if (collisionInfo.gameObject != null) 
		{
			CtrolEnenyAI health = collisionInfo.gameObject.GetComponent<CtrolEnenyAI>();
			if( health != null && m_Launcher != null )
			{
				m_Launcher.m_combat.CalculateDmg( health.m_pOwner, m_pSkill, "" );
			}
		}
	}
	

	//---------------------------------------------------------------------------------
	// 当退出碰撞器
	//---------------------------------------------------------------------------------
	void OnCollisionExit( Collision collisionInfo ) 
	{
		if (collisionInfo.gameObject != null) 
		{
			CtrolEnenyAI health = collisionInfo.gameObject.GetComponent<CtrolEnenyAI>();
			if( health != null && m_Launcher != null )
			{
				m_Launcher.m_combat.CalculateDmg( health.m_pOwner, m_pSkill, "" );
			}
		}
	}
}

