using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






class CNPCEntity : CSceneEntity
{

	public  tagEntityProto				m_pProto;
	private uint						m_dwTypeID;
	public CreatureFSM					m_FSM;
	public CAIController				m_AIController;
	public CMoveController				m_pMoveCtrl;

	public CSkill						m_pMeleeSkill;		
	public CSkill						m_pRangedSkill;		
	public CNPCEntity()
	{

	}


	public void Update( )
	{
		if (IsCanUpdateAI () && m_AIController != null ) 
		{
			m_AIController.Update();
		}

       
		if (IsCanMove () && m_pMoveCtrl != null)
		{
			m_pMoveCtrl.Update();
		}
	}


	private bool IsCanUpdateAI( )
	{
		if( IsInState( EState.ES_Suspension ) ||
		    IsInState( EState.ES_Dead ) ||
            m_FSM.IsInBehavior(BehaviorType.EState_BeAttack))
		{
			return false;
		}

		return true;
	}

	private bool IsCanMove( )
	{
		if( IsInState( EState.ES_Suspension ) ||
		   IsInState( EState.ES_Dead ) ||
           m_FSM.IsInBehavior(BehaviorType.EState_BeAttack))
		{
			return false;
		}

		return true;
	}


    public void InitEntity( uint dwTypeID )
	{
        InitEnemy(dwTypeID, false);
		m_FSM 			= new CreatureFSM ( this );
		m_AIController 	= new CAIController (this);
		m_pMoveCtrl 	= new CMoveController (this);

        /// 初始化完毕后进入空闲状态
        if (m_FSM != null)
        {
            m_FSM.Change2IdleBeHavior();
        }
	}

	public uint GetTypeID()
	{
		return m_dwTypeID;
	}

	public static CNPCEntity  CreateNPCEntity( uint dwTypeID )
	{
		CNPCEntity pNPC = new CNPCEntity ();
		pNPC.InitEnemy( dwTypeID, true );
		return pNPC;
	}


	public static CNPCEntity  CreateNPCNoEntity( uint dwTypeID )
	{
		CNPCEntity pNPC = new CNPCEntity ();
		return pNPC;
	}

	private void InitEnemy( uint dwTypeID, bool IsNeedLoad )
	{
        CProtoManager.inst.m_mapEntity.TryGetValue(dwTypeID, out m_pProto);
		if (m_pProto == null) 
		{
			Common.DEBUG_MSG("CCycleEntity's proto not find " + dwTypeID );
			return;
		}

        m_dwTypeID = dwTypeID;
        if (IsNeedLoad)
        {
            asset.source = m_pProto.szModelName;
            asset.LoadAsset();
        }
        else
        {
            m_CtrlSkelton = gameObject.GetComponent<CtrolAnimation>();
        }

		InitAtt();
        InitSkill();
	}


	protected void InitAtt( )
	{

		for (int i = 0; i < (int)ERoleAttribute.ERA_AttA_End; i++) 
		{
			SetBaseAttValue( (ERoleAttribute)i, m_pProto.nBaseAtt[i] );
		}

		m_nLevel 		= m_pProto.nLevel;
		RecalAtt ();
	}


    private void InitSkill( )
    {
        if (m_pProto.dwNormalSkill > 0)
        {
            m_pMeleeSkill = AddSkill(m_pProto.dwNormalSkill);
        }

        if (m_pProto.dwLongDisSkill > 0)
        {
            m_pRangedSkill = AddSkill(m_pProto.dwLongDisSkill);
        }
    }

    public override void OnDead(CSceneEntity pKiller, CSkill pSkill)
	{

		if (IsInState (EState.ES_Dead))
			return;

		//SetState (EState.ES_Dead);

		if (pKiller != null) 
		{
			pKiller.OnKill( this );
		}
	}

	public override void OnDisappear( )
	{

	}

    public override void OnKill(CSceneEntity pTarget)
	{

	}

	public override void OnRevive( )
	{


	}

    public void ExpReward(CSceneEntity pReward, bool bKill)
	{

	}

    public override void OnBeAttacked(CSceneEntity pSrc, CSkill pSkill, bool bHited, bool bBlock, bool bCrited)
	{

	}

    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// 模型资源异步加载完成
    /// </summary>
    //-----------------------------------------------------------------------------------------
    public override void OnAsyncLoaded()
    {
        if (gameObject != null)
        {
            m_AnimCtrl = gameObject.GetComponentInChildren<Animator>();
            if (isPlayer == false)
            {
                Common.calcPositionY(m_position, out m_position.y, false);
            }
            else
            {
                BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
                if (boxCollider != null)
                    UnityEngine.Object.Destroy(boxCollider);
            }
        }
    }
}



