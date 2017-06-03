using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;





class CombatHandler
{

    private CSceneEntity    m_pOwner;
	private CtrolEnenyAI    m_pAI;

	public CombatHandler ()
	{
		m_pOwner = null;

	}

	~CombatHandler( )
	{

	}


    public void InitCombat(CSceneEntity pOwer)
	{
		m_pOwner 	= pOwer;
	}


    private void CalActionEffect(CSceneEntity pBeAtcker, string strActEffect)
	{
		if (pBeAtcker != null && m_pOwner != null ) 
		{

			int iCmd 	= int.Parse( strActEffect.Split( '|' )[0]);
			float fParam= float.Parse( strActEffect.Split( '|' )[1]);
			               

			if( iCmd == 0 )
			{
				if( pBeAtcker.gameObject.transform.tag == "Monster" )
				{
					CNPCEntity pNPC = (CNPCEntity)pBeAtcker;
					if( pNPC.m_FSM == null )
						return;

                    if (!pNPC.m_FSM.IsInBehavior(BehaviorType.EState_Suspension))
					{
                        pNPC.m_FSM.Change2BeAtkBeHavior();
					}
				}

				if( pBeAtcker.gameObject.transform.tag == "Player" )
				{
					CHeroEntity pNPC = (CHeroEntity)pBeAtcker;
					if( pNPC != null && pNPC.m_FSM != null )
					{
						pNPC.m_FSM.Change2BeAtkBeHavior();
					}
				}

				iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject, fParam);
			}

			if( iCmd == 1 )
			{
				if( pBeAtcker.gameObject.transform.tag != "Monster" )
					return;
				
				CNPCEntity pNPC = (CNPCEntity)pBeAtcker;
                if (pNPC.m_FSM == null)
					return;

                if (pNPC.m_FSM.IsInBehavior(BehaviorType.EState_Suspension))
				{
					iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject, fParam, 0.1f );

                    pNPC.m_FSM.ChangeBeHavior(BehaviorType.EState_Suspension, 0);
				}
				else 
				{
                    pNPC.m_FSM.Change2BeAtkBeHavior();

					iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject, fParam);
				}
			}

			if( iCmd == 2 )
			{
				if( pBeAtcker.gameObject.transform.tag != "Monster" )
					return;
				
				CNPCEntity pNPC = (CNPCEntity)pBeAtcker;
                if (pNPC.m_FSM == null)
					return;

                if (pNPC.m_FSM.IsInBehavior(BehaviorType.EState_Suspension))
				{
					iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject, fParam, 0.1f);
				}
				else
				{
					iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject, fParam, float.Parse(strActEffect.Split('|')[2]) );
				}

                pNPC.m_FSM.ChangeBeHavior(BehaviorType.EState_Suspension, 0);

			}

			if( iCmd == 3 )
			{
				if( pBeAtcker.gameObject.transform.tag != "Monster" )
					return;

				CNPCEntity pNPC = (CNPCEntity)pBeAtcker;
                if (pNPC.m_FSM == null)
					return;

                if (pNPC.m_FSM.IsInBehavior(BehaviorType.EState_Suspension))
				{

                    pNPC.m_FSM.ChangeBeHavior(BehaviorType.EState_Suspension, 0);
					iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject, fParam, 0.1f);

					CtrolAnimation pCtrlAnim = pNPC.gameObject.GetComponent<CtrolAnimation>();
					pCtrlAnim.SetPlaySpeed(0.01f);
				}


				else 
				{
					iTween.MoveToMy( pBeAtcker.gameObject, m_pOwner.gameObject,
					                 (fParam + 3) / Vector3.Distance(m_pOwner.gameObject.transform.position, 
					                 pBeAtcker.gameObject.transform.position) );
				}
			}

			if( iCmd == 4 )
			{
				if( pBeAtcker.gameObject.transform.tag != "Monster" )
					return;
				
				CNPCEntity pNPC = (CNPCEntity)pBeAtcker;
                if (pNPC.m_FSM == null)
					return;

				CtrolAnimation pCtrlAnim = pNPC.gameObject.GetComponent<CtrolAnimation>();
                if (pNPC.m_FSM.IsInBehavior(BehaviorType.EState_Suspension))
				{
					pCtrlAnim.SetPlaySpeed(0.001f);
				}
			}

			PlayBeAttackEffect( pBeAtcker, "effect/spark 04" );
		}
	}



    private void PlayBeAttackEffect(CSceneEntity pBeAtcker, string strEffect)
	{
		Transform effectpos 		= null;
		effectpos 					= (UnityEngine.GameObject.Instantiate(Resources.Load(strEffect)) as GameObject).transform;
		effectpos.parent 			= pBeAtcker.gameObject.transform;
		effectpos.localPosition 	= Vector3.zero;
		effectpos.gameObject.name 	= strEffect + "BeHit";
		effectpos.localRotation 	= Quaternion.Euler(Vector3.zero);
		UnityEngine.GameObject.Destroy(effectpos.gameObject, 1);
		effectpos.parent 			= null;
	}


    private void CalSkillEffect(CSceneEntity pBeAtcker, CSkill pSkill)
	{
		if ( pBeAtcker != null ) 
		{
			pBeAtcker.OnBeAttacked( m_pOwner, pSkill, false, false, false );
		}
	}


	private void CaleSkillCost( CSkill pSkill )
	{
		if (pSkill == null)
			return;

		ESkillCostType eType = pSkill.GetCostType ();
		if (eType == ESkillCostType.ESCT_HP) 
		{
			int nHP = pSkill.GetSkillCost();
			m_pOwner.ChangeHP( m_pOwner, pSkill, -nHP, false );
		}
		
		if (eType == ESkillCostType.ESCT_MP) 
		{
			int nMP = pSkill.GetSkillCost();
			m_pOwner.ChangeMP( -nMP );
		}
	}

    public void CalculateDmg(CSceneEntity pBeAtcker, CSkill pSkill, string strActEff)
	{

		bool bCrit = false;

		if (!CalculateBlock ( pBeAtcker, pSkill) ) 
		{
			pBeAtcker.OnBeAttacked( m_pOwner, pSkill, false, true, false );
			return ;
		}

		/*
		if (!CalculateHit ( pBeAtcker, pSkill) ) 
		{
			// miss 了
			pBeAtcker.OnBeAttacked( m_pOwner, pSkill, false, false, false );
			return ;
		}
		*/

		if( strActEff != "" )
			CalActionEffect ( pBeAtcker, strActEff );

		CalSkillEffect ( pBeAtcker, pSkill );


		float fDmg 	= CalculateBaseDmg ( pBeAtcker, pSkill );
		pBeAtcker.ChangeHP( m_pOwner, pSkill, -(int)fDmg, bCrit );
	}

    private float CalculateBaseDmg(CSceneEntity bBaker, CSkill pSkill)
	{
		float fBaseDmg = 1.0f;

		float fMinWeaponDmg = m_pOwner.GetAttValue(ERoleAttribute.ERA_WeaponDmgMin );
		float fMaxWeaponDmg = m_pOwner.GetAttValue(ERoleAttribute.ERA_WeaponDmgMax );
		float fWeaponDmg 	= UnityEngine.Random.Range ( fMinWeaponDmg, fMaxWeaponDmg );

		int nSkillDmg = 0;
		nSkillDmg = pSkill.GetDmg ();

		if (pSkill.GetDmgType () == ESkillDmgType.ESDGT_Physical) 
		{
			if( nSkillDmg > 100000 )
			{
				fBaseDmg = fWeaponDmg * ((float)(nSkillDmg-100000)/100000.0f );
			}
			else
			{
				fBaseDmg = fWeaponDmg + nSkillDmg;
			}

			fBaseDmg = fBaseDmg * (1.0f + (float)m_pOwner.GetAttValue(ERoleAttribute.ERA_ExAttack) / 100.0f);
		}

		if( pSkill.GetDmgType() == ESkillDmgType.ESDGT_Energy )
		{
			if( nSkillDmg > 100000 )
			{
				fBaseDmg = fWeaponDmg * ((float)(nSkillDmg-100000)/100000.0f );
			}
			else
			{
				fBaseDmg = fWeaponDmg + nSkillDmg;
			}
			
			fBaseDmg = fBaseDmg * (1.0f + (float)m_pOwner.GetAttValue(ERoleAttribute.ERA_InAttack) / 100.0f);
		}

		return fBaseDmg;
	}


    private bool CalculateCritRate(CSceneEntity bBaker, CSkill pSkill)
	{
		float fCrit = 2.0f * ( m_pOwner.GetAttValue(ERoleAttribute.ERA_Crit_Rate) * m_pOwner.m_nLevel - m_pOwner.GetAttValue(ERoleAttribute.ERA_Technique)* bBaker.m_nLevel )/( m_pOwner.m_nLevel + bBaker.m_nLevel ) * 1000.0f;
		if (fCrit < 0.01f)
		{
			fCrit = 0.01f;
		}
		else if (fCrit > 0.3f)
		{
			fCrit = 0.3f;
		}

		float fradom 	= UnityEngine.Random.Range ( 0, 100 );
		if( fradom < fCrit * 100 )
		{
			return true;
		}
		return false;
	}

    private bool CalculateHit(CSceneEntity bBaker, CSkill pSkill)
	{

		float fHit = 0.0f;
		fHit = 0.9f * ( 1.0f - (float)( bBaker.GetAttValue(ERoleAttribute.ERA_Dodge) - m_pOwner.GetAttValue(ERoleAttribute.ERA_HitRate) ) ) / 10000.0f;
		if (fHit < 0.0f)
			fHit = 0.0f;
		if (fHit > 1.0f)
			fHit = 1.0f;

		float fradom 	= UnityEngine.Random.Range ( 0, 100 );
		if( fradom < fHit * 100 )
		{
			return true;
		}
		return false;
	}

    private bool CalculateBlock(CSceneEntity bBaker, CSkill pSkill)
	{
		float fBlock = 20.0f;
	
		if( fBlock < 0.0f ) fBlock = 0.0f;
		if( fBlock > 1.0f ) fBlock = 1.0f;

		float fradom 	= UnityEngine.Random.Range ( 0, 100 );
		if( fradom < fBlock * 100 )
		{
			return true;
		}
		return false;
	}
}
