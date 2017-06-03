using UnityEngine;
using System;
using System.Collections;
using System.Linq;






class CNormBehavior
{

	public bool					m_bClose;
	public NormalizeSeg			m_StepSeg;
	private int					m_SegIndex;

	CSkill						m_pSkill;
    CSceneEntity                m_pOwner;
	UnityEngine.GameObject		m_GameObject;
	private CtrolAnimation		m_CtrlSkelton;

	public CNormBehavior( )
	{
		m_pSkill 	= null;
		m_pOwner 	= null;
		m_bClose 	= true;
		m_SegIndex 	= 0;
		m_StepSeg 	= NormalizeSeg.NATK_Null;
		m_CtrlSkelton = null;

	}
	
	~CNormBehavior()
	{
		
	}

    public void InitFSM(CSceneEntity pNPC)
	{
		m_pOwner 	 = pNPC;
		m_GameObject = m_pOwner.gameObject;
		if (m_GameObject != null) 
		{
			m_CtrlSkelton = m_GameObject.GetComponent<CtrolAnimation>();
		}
	}
	
	public void Enter( CSkill pSkill )
	{

		m_pSkill 	= pSkill;
		m_bClose 	= false;

		m_SegIndex++;
		if (m_SegIndex > 3)
			m_SegIndex = 1;

		m_StepSeg 	= (NormalizeSeg)m_SegIndex;
		if (m_StepSeg == NormalizeSeg.NATK_Seg0) 
		{
			m_CtrlSkelton.PlayTrack( ACTID.ACT_DoubleHit01 );
		}

		if (m_StepSeg == NormalizeSeg.NATK_Seg1) 
		{
			m_CtrlSkelton.PlayTrack( ACTID.ACT_DoubleHit02 );
		}

		if (m_StepSeg == NormalizeSeg.NATK_Seg2) 
		{
			m_CtrlSkelton.PlayTrack( ACTID.ACT_DoubleHit03 );
		}
	}
	
	public void Exit()
	{
		m_SegIndex 	= 0;
		m_StepSeg	= NormalizeSeg.NATK_Null;
	}
	
	public void Update( )
	{
		if (m_bClose)
			return;

	}

	public void doAnimEvent( string strParam )
	{
		if (m_pSkill == null)
			return;


		float fDistance = m_pSkill.GetProto ().fOPDist;
		float fRadious 	= m_pSkill.GetProto ().fOPRadius;
		if (m_pOwner != null) 
		{
			GameObject[] EnemyList = GetFronEnemy( fDistance, fRadious );
			foreach ( var Enemy in EnemyList )
			{
				CtrolEnenyAI pAI = Enemy.GetComponent<CtrolEnenyAI>();
				m_pOwner.m_combat.CalculateDmg( pAI.m_pOwner, m_pSkill, strParam );
			}
		}
	}

	public bool IsClosed( )
	{
		return m_bClose;
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





