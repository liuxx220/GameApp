using UnityEngine;
using System.Collections;






class CtrolEnenyAI : MonoBehaviour
{

	public  int				dwTypeID = 0;
	public  CNPCEntity		m_pOwner;

	void Start( )
	{

		m_pOwner    = CNPCEntity.CreateNPCNoEntity( (uint)dwTypeID );
		if (m_pOwner != null) 
		{
			m_pOwner.gameObject = transform.gameObject;
            m_pOwner.InitEntity((uint)dwTypeID );
		}
	}


	void Update()
	{
		if (m_pOwner != null ) 
		{
			m_pOwner.Update ();
		}
	}
	
	void FixedUpdate ()
	{
		if (m_pOwner != null && m_pOwner.m_FSM != null ) 
		{
			m_pOwner.m_FSM.Update ();
		}
	}
}
