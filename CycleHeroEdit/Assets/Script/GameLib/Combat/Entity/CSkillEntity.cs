using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






class CSkill 
{

	public tagSkillProto 				m_pSkillProto;
	private uint						m_dwID;
	public  UnityEngine.GameObject		m_effectprefab;		
															

	public  bool						m_bCanUse;
	private float						m_cooldown;
    private CSceneEntity                m_pOwer;			

	public CSkill()
	{
		m_bCanUse 		= true;
		m_dwID 			= GenID.MakeSkillID ();
		m_effectprefab 	= null;
	}

	~CSkill()  // destructor
    {
    	
    }

	public void 	Init( uint dwTypeID )
	{

        CProtoManager.inst.m_mapSkill.TryGetValue(dwTypeID, out m_pSkillProto);
		if (m_pSkillProto == null) 
		{
			Common.DEBUG_MSG("CCycleEntity's proto not find " + dwTypeID );
			return;
		}

		m_dwID = dwTypeID;


		m_effectprefab = null;
		if (m_pSkillProto.strPrefabFile != "") 
		{
			try
			{
				string streffect    = "effect/"+m_pSkillProto.strPrefabFile;
				m_effectprefab 		= (UnityEngine.GameObject)Resources.Load( streffect );
				m_effectprefab.SetActive( false );
			}
			catch (Exception e)
			{
				Common.ERROR_MSG("instantiate failed: " + e.ToString());
			}
		} 
	}

	public tagSkillProto GetProto( )
	{
		return m_pSkillProto;
	}

	public uint 	GetID( )
	{
		return m_dwID;
	}

	private float	GetCoolDown()
	{
		return (float)(m_pSkillProto.nCoolDown / 1000);
	}

	private int	GetDmgValue()
	{
		return m_pSkillProto.nDmgValues;
	}

	public ESkillUseType GetUseType()
	{
		return m_pSkillProto.eUseType;
	}

	public SkillPriority GetPriority()
	{
		return m_pSkillProto.ePriority;
	}

	public ESkillDmgType GetDmgType()
	{
		return m_pSkillProto.eDmgType;
	}

	public int GetDmg()
	{
		return m_pSkillProto.nDmgValues;
	}


	public ESkillCostType GetCostType()
	{
		return m_pSkillProto.eCostType;
	}

	public void		StartCoolDown( )
	{
		m_bCanUse 	= false;
		m_cooldown 	= GetCoolDown ();
	}


	public int	GetSkillCost()
	{
		return m_pSkillProto.nCostValue;
	}


	public void	 UpdateCD( )
	{
		if( !m_bCanUse ) 
		{
			m_cooldown -= Time.deltaTime;
			if( m_cooldown < 0 )
			{
				m_cooldown 	= 0;
				m_bCanUse	= true;
			}
		}
	}

	public float GetPrepareTime()
	{
		return m_pSkillProto.nPrepareTime / 1000.0f;
	}

	public float GetPilotTime()
	{
		return m_pSkillProto.nPilotTime / 1000.0f;
	}


	public int GetPilotNum()
	{
		return m_pSkillProto.nPilotNum;
	}

    public ESKILLFINGERTYPE GetFingerType()
	{
		return m_pSkillProto.eFingerType;
	}

	public float GetDist()
	{
		return m_pSkillProto.fOPDist;
	}

	public float GetRadius()
	{
		return m_pSkillProto.fOPRadius;
	}

	public int GetActID()
	{
		return m_pSkillProto.ActID;
	}
}

