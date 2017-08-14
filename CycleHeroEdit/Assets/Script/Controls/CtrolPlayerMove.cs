using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;







public class CtrolPlayerMove : MonoBehaviour
{
	
	public float		m_fmovespeed = 4.5f;
	private float		m_Gravity = 20.0f;
	public float		touchkey_x;
	public float		touchkey_y;

	Vector3				m_velocity;
	CharacterController m_character;
	CLocalPlayerFSM		m_LocalFSM = null;

	
	void Start( )
	{
		m_character	= GetComponent<CharacterController>();
        CHeroEntity m_Hero = CFightTeamMgr.Instance.m_pBattleHero;
        if (m_Hero != null)
            m_LocalFSM = m_Hero.m_FSM;
		
	}


	void Update ()
	{
		if (m_LocalFSM != null)
			m_LocalFSM.Update ();

		UpdateInput ();
	}


	void UpdateInput( )
	{

		touchkey_x 		= Input.GetAxis("Vertical");
		touchkey_y 		= Input.GetAxis("Horizontal");
		UpdateNoCombatJoystick (touchkey_x, touchkey_y);
	}

	void UpdateNoCombatJoystick( float touchX, float touchY )
	{
		if (m_LocalFSM == null)
			return;

		if (m_LocalFSM.IsInIdle () && (Math.Abs (touchX) + Math.Abs (touchY)) > 0f ) 
		{

			m_LocalFSM.ChangeBeHavior( BehaviorType.EState_Move, 0 );
		}

		if (m_LocalFSM.IsInMoveing () && (Math.Abs (touchX) + Math.Abs (touchY)) < 0.001f ) 
		{
			m_LocalFSM.ChangeBeHavior( BehaviorType.EState_Idle, 0 );
		}


		m_velocity 		= Vector3.zero;
		if ( m_character.isGrounded ) 
		{
			if( touchX > 0 )
				m_velocity = Vector3.forward * m_fmovespeed * 0.02f;

			if( touchX < 0 )
				m_velocity = Vector3.forward * -m_fmovespeed *  0.02f;

			if( touchY > 0 )
				transform.Rotate(0, 120 * Time.deltaTime, 0);

			if( touchY < 0 )
                transform.Rotate(0, -120 * Time.deltaTime, 0);
		}

        m_velocity      = transform.TransformDirection(m_velocity);
		m_velocity.y   -= m_Gravity * Time.deltaTime;
		m_character.Move( m_velocity  );

	}


	void OnCollisionEnter(Collision collisionInfo)
	{
		Debug.Log("Collision name is" + collisionInfo.gameObject.name);
	}
}
