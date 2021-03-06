using UnityEngine;
using System.Collections;





public enum EMoveState
{
	EMS_Stand,				
	EMS_Walk,				
	EMS_Run,				
	EMS_Drop				
}



class CMoveController
{


	float							m_fmovespeed = 3.0f;
    CNPCEntity                      m_pOwner;
	public EMoveState				m_eCurMove;

	private Vector3					m_vDstPosition;		
	private UnityEngine.GameObject	m_GameObject;

	private Vector3 				m_velocity = Vector3.zero;
	CharacterController				m_character;

    public CMoveController(CNPCEntity pOwner)
	{
		m_pOwner 		= pOwner;
		m_GameObject 	= pOwner.gameObject;
		m_eCurMove 		= EMoveState.EMS_Stand;
		m_character		= m_GameObject.GetComponent<CharacterController>();
	}


	public void Update( )
	{

		if (m_eCurMove == EMoveState.EMS_Stand)
			return;

		if (m_eCurMove == EMoveState.EMS_Walk) 
		{
			UpdateWalk();
		}

		else if (m_eCurMove == EMoveState.EMS_Run ) 
		{
			UpdateRun();
		}

		else if (m_eCurMove == EMoveState.EMS_Drop ) 
		{
			UpdateDrop();
		}
	}

	private void UpdateWalk( )
	{
		m_fmovespeed = 2.0f;
		Move();
	}


	private void UpdateRun()
	{
		m_fmovespeed = 3.0f;
		Move();
	}


	private void Move( )
	{
		if (m_GameObject != null && m_character != null ) 
		{
			if (Vector3.Distance(m_GameObject.transform.position, m_vDstPosition) < 0.6f)
			{
				StopMove();
			}


			m_GameObject.transform.rotation = Quaternion.Lerp( m_GameObject.transform.rotation, Quaternion.LookRotation(m_vDstPosition - m_GameObject.transform.position), 5 * Time.deltaTime);
			m_velocity 		= m_GameObject.transform.TransformDirection( Vector3.forward );
			m_velocity 		= m_velocity.normalized * m_fmovespeed * Time.deltaTime;
			m_character.Move( m_velocity );
		}
	}
	

	private void UpdateDrop()
	{

	}

	public void StartWalk( Vector3 vDstPos, EMoveState eState )
	{
        if (m_pOwner != null && m_pOwner.m_FSM != null)
        {
            m_vDstPosition = vDstPos;
            m_eCurMove = eState;

            if (Vector3.Distance(m_GameObject.transform.position, m_vDstPosition) > 0.6f)
            {
                m_pOwner.m_FSM.ChangeBeHavior(BehaviorType.EState_Move, 0);
            }
        }
	}


	public void StartNPCPursue( Vector3 vDstPos, EMoveState eState )
	{
        if (m_pOwner != null && m_pOwner.m_FSM != null)
        {
            m_vDstPosition = vDstPos;
            m_eCurMove = eState;
            if (Vector3.Distance(m_GameObject.transform.position, m_vDstPosition) > 0.6f)
            {
                m_pOwner.m_FSM.ChangeBeHavior(BehaviorType.EState_Move, 1);
            }
        }
	}


	public void StopMove(  )
	{
        if (m_pOwner != null && m_pOwner.m_FSM != null)
        {
            m_eCurMove = EMoveState.EMS_Stand;
            m_pOwner.m_FSM.ChangeBeHavior(BehaviorType.EState_Idle, 0);
        }
	}
}
	 
	
