using UnityEngine;
using System;
using System.Collections;
using System.Linq;





class CSkillBehavior
{

	public bool					m_bClosed;
    CSceneEntity                m_pOwner;
	UnityEngine.GameObject		m_GameObject;
	private CtrolAnimation		m_CtrlSkelton;
	private CSkill				m_pSkill;

	public CSkillBehavior()
	{
		m_bClosed 		= false;
		m_pOwner 		= null;
		m_GameObject 	= null;
		m_CtrlSkelton 	= null;
	}
	
	~CSkillBehavior()
	{
		
	}

    public void InitFSM(CSceneEntity pNPC)
	{
		m_pOwner 	 	= pNPC;
		m_GameObject 	= m_pOwner.gameObject;
		if (m_GameObject != null) 
		{
			m_CtrlSkelton = m_GameObject.GetComponent<CtrolAnimation>();
		}
	}
	
	public void Enter( CSkill pSkill )
	{

		m_pSkill 		= pSkill;
		if ( pSkill.GetProto ().ActID > 0 && m_CtrlSkelton != null ) 
		{
			ACTID  eActID = (ACTID)pSkill.GetProto ().ActID;
			m_CtrlSkelton.PlayTrack( eActID );
		}
	}
	
	public void Exit()
	{
		
	}
	

	public void Update( )
	{
		
	}

	public void doAnimEvent( string strParam )
	{
	
		float fDistance = m_pSkill.GetProto ().fOPDist;
		float fRadious 	= m_pSkill.GetProto ().fOPRadius;
		if (m_pOwner != null) 
		{
			GameObject[] EnemyList = GetFronEnemy( fDistance, fRadious );
			foreach ( var Enemy in EnemyList )
			{
				if( Enemy.transform.tag == "Player" )
				{
					CPlayerCompent pAI = Enemy.GetComponent<CPlayerCompent>();
					if( pAI !=null )
						m_pOwner.m_combat.CalculateDmg( pAI.m_pOwner, m_pSkill, strParam );
				}

				if( Enemy.transform.tag == "Monster" )
				{
					CtrolEnenyAI pAI = Enemy.GetComponent<CtrolEnenyAI>();
					if( pAI !=null )
						m_pOwner.m_combat.CalculateDmg( pAI.m_pOwner, m_pSkill, strParam );
				}
			}
		}
	}


	public bool IsClosed()
	{
		return m_bClosed;
	}

	private GameObject[] GetFronEnemy(float distance, float radious )
	{

		string _Tag = "Monster";
		if (m_GameObject.tag == "Monster")
			_Tag = "Player";

		Transform transform = m_GameObject.transform;
		GameObject[] GameObjectArr = (	from r in GameObject.FindGameObjectsWithTag(_Tag)
		                              	where Helper.isINRect(r.transform.position,
		                      			transform.right * radious + transform.forward * -1.5f + transform.position,
		                      			transform.right * -radious + transform.forward * -1.5f + transform.position,
		                     			transform.forward * (distance + 1) + transform.right * -radious + transform.position,
		                      			transform.forward * (distance + 1) + transform.right * radious + transform.position)
		                              	select r.gameObject).ToArray();
		
		return GameObjectArr;
	}
}





