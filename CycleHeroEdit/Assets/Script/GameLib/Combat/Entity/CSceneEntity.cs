/*-----------------------------------------------------------------------------
 *      file name : CSceneEntity.cs
 *      desc      : 游戏对象实体
 *      author    : ljp
 *      
 *      log       : 
------------------------------------------------------------------------------*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;







class CSceneEntity : AssetObject
{


    public Animator     m_AnimCtrl      = null;
    public Vector3      m_position      = Vector3.zero;
    public Vector3      m_eulerAngles   = Vector3.zero;
    public Vector3      m_scale         = Vector3.zero;
    public int          m_nLevel        = 1;
    public bool         isPlayer        = false;

    public CtrolAnimation m_CtrlSkelton = null;

    public Asset asset
    {
        get;
        set;
    }

    public uint ID
    {
        get;
        set;
    }


    //---------------------------------------------------------------------------------
    // 英雄属性 begin
    //---------------------------------------------------------------------------------
    public int[]                    m_nAttr;
    public int[]                    m_nbaseAttr;
    public int[]                    m_nAttMod;
    public int[]                    m_nAttModPct;
    public bool[]                   m_bAttRecalFlag;
    //---------------------------------------------------------------------------------
    // 英雄属性 end
    //---------------------------------------------------------------------------------
    public CombatHandler	        m_combat;
    private CEntityState	        m_StateMgr;
    public Dictionary<uint, CSkill> m_mapSkill;
	public CSceneEntity()
	{
		m_nAttr					= new int[(int)ERoleAttribute.ERA_End];
		m_nbaseAttr				= new int[(int)ERoleAttribute.ERA_End];
		m_nAttMod				= new int[(int)ERoleAttribute.ERA_End];
		m_nAttModPct			= new int[(int)ERoleAttribute.ERA_End];
		m_bAttRecalFlag			= new bool[(int)ERoleAttribute.ERA_End];
		m_AnimCtrl 				= null;
		m_combat 				= new CombatHandler ();
		m_combat.InitCombat (this);

        asset                   = new CEntiyScene();
        asset.LoaderObj         = this;
		m_StateMgr 				= new CEntityState ();
		m_mapSkill 				= new Dictionary< uint, CSkill> ();
	}
	

	public virtual void update( )
	{

	}


    //-----------------------------------------------------------------------------------
	// 增加技能
	//-----------------------------------------------------------------------------------
	public CSkill	AddSkill( uint dwTypeID )
	{
		
		CSkill newSkill = null;
		m_mapSkill.TryGetValue( dwTypeID, out newSkill );
		if ( newSkill == null ) 
		{
			newSkill = new CSkill ();
			newSkill.Init( dwTypeID );
			m_mapSkill.Add ( dwTypeID, newSkill);
		}

		return newSkill;
	}


	//-----------------------------------------------------------------------------------
	// 查找技能
	//-----------------------------------------------------------------------------------
	public CSkill FindSkillByTypeID( uint dwTypeID )
	{
		CSkill pSkill = null;
		m_mapSkill.TryGetValue( dwTypeID, out pSkill );
		return pSkill;
	}

	//-----------------------------------------------------------------------------------
	// 读取当前属性
	//-----------------------------------------------------------------------------------
	public int	GetAttValue( ERoleAttribute nIndex )
	{
		return m_nAttr [(int)nIndex];
	}

	//-----------------------------------------------------------------------------------
	// 读取基础属性
	//-----------------------------------------------------------------------------------
	public int GetBaseAttValue( ERoleAttribute nIndex )
	{
		return m_nbaseAttr [(int)nIndex];
	}

	//-----------------------------------------------------------------------------------
	// 读取属性平直加成
	//-----------------------------------------------------------------------------------
	public int GetAttModValue( ERoleAttribute nIndex )
	{
		return m_nAttMod [(int)nIndex];
	}

	//-----------------------------------------------------------------------------------
	// 读取属性百分比加成
	//-----------------------------------------------------------------------------------
	public int GetAttModValuePct( ERoleAttribute nIndex )
	{
		return m_nAttModPct [(int)nIndex];
	}

	//------------------------------------------------------------------------------------
	// 设置当前属性
	//------------------------------------------------------------------------------------
	public void SetAttValue( ERoleAttribute nIndex, int nValue, bool bSend )
	{
		m_nAttr [(int)nIndex] = nValue;
	}

	//-----------------------------------------------------------------------------------
	// 设置基本属性
	//-----------------------------------------------------------------------------------
	public void SetBaseAttValue( ERoleAttribute nIndex, int nValue )
	{
		m_nbaseAttr [(int)nIndex] = nValue;
		SetAttRecalFlag (nIndex);
	}

	//-----------------------------------------------------------------------------------
	// 设置属性平直加成
	//-----------------------------------------------------------------------------------
	public void SetAttModValue( ERoleAttribute nIndex, int nValue )
	{
		m_nAttMod [(int)nIndex] = nValue;
		SetAttRecalFlag (nIndex);
	}

	//-----------------------------------------------------------------------------------
	// 设置属性百分比加成
	//-----------------------------------------------------------------------------------
	public void SetAttModValuePct( ERoleAttribute nIndex, int nValuePct )
	{
		m_nAttModPct [(int)nIndex] = nValuePct;
		SetAttRecalFlag (nIndex);
	}

	//------------------------------------------------------------------------------------
	// 修改当前属性
	//------------------------------------------------------------------------------------
	public void ModAttValue( ERoleAttribute nIndex, int nValueMod )
	{
		if (nValueMod == 0)
			return;

		m_nAttr [(int)nIndex] += nValueMod;
	}

	//-----------------------------------------------------------------------------------
	// 修改基础属性
	//-----------------------------------------------------------------------------------
	public void ModBaseAttValue( ERoleAttribute nIndex, int nValeMod )
	{
		m_nbaseAttr [(int)nIndex] += nValeMod;
		SetAttRecalFlag (nIndex);
	}

	//-----------------------------------------------------------------------------------
	// 修改属性平直加成
	//-----------------------------------------------------------------------------------
	public void	ModAttModValue( ERoleAttribute nIndex, int nValueMod )
	{
		m_nAttMod [(int)nIndex] += nValueMod;
		SetAttRecalFlag (nIndex);
	}

	//-----------------------------------------------------------------------------------
	// 修改属性百分比加成
	//-----------------------------------------------------------------------------------
	public void ModAttModValuePct( ERoleAttribute nIndex, int nValuePctMod )
	{
		m_nAttModPct [(int)nIndex] += nValuePctMod;
		SetAttRecalFlag (nIndex);
	}

	
	//----------------------------------------------------------------------------------
	// 属性重新计算标识位设置
	//----------------------------------------------------------------------------------
	public void SetAttRecalFlag( ERoleAttribute nIndex )
	{
		m_bAttRecalFlag [(int)nIndex] = true;
	}

	//----------------------------------------------------------------------------------
	// 得到某个属性是否需要重算
	//----------------------------------------------------------------------------------
	public bool GetAttRecalFlag( ERoleAttribute nIndex )
	{
		return m_bAttRecalFlag [(int)nIndex];
	}

	//----------------------------------------------------------------------------------
	// 清除属性重算标志位
	//----------------------------------------------------------------------------------
	public void ClearAttRecalFlag()
	{
		for (int i = 0; i < (int)ERoleAttribute.ERA_End; i++) 
		{
			m_bAttRecalFlag[i] = false;
		}
	}

	
	//--------------------------------------------------------------------------------
	// 根据重算标志位进行当前属性重算
	//--------------------------------------------------------------------------------
	public void RecalAtt( )
	{
		
		int nHP = m_nAttr [(int)ERoleAttribute.ERA_HP];
		int nMP = m_nAttr [(int)ERoleAttribute.ERA_MP];

		// 开始计算
		for (int i = 0; i < (int)ERoleAttribute.ERA_End; i++) 
		{
			if( false == GetAttRecalFlag( (ERoleAttribute)i ) )
				continue;

			m_nAttr[i] = CalAttMod( (ERoleAttribute)i, m_nbaseAttr[i] );
		}

		
		for (int j = (int)ERoleAttribute.ERA_AttA_Start; j <= (int)ERoleAttribute.ERA_AttA_End; j++) 
		{
			if( GetAttRecalFlag((ERoleAttribute)j) == false )
				continue;

			ERoleAttribute etype = (ERoleAttribute)j;
			switch(etype)
			{
				// 筋骨，则二级属性的最大体力和外功防御要重新计算
				case ERoleAttribute.ERA_Physique:
				{
					// 计算最大体力
					m_nAttr[(int)ERoleAttribute.ERA_MaxHP] = m_nAttr[(int)ERoleAttribute.ERA_Physique] * 10 + m_nbaseAttr[(int)ERoleAttribute.ERA_MaxHP];
					m_nAttr[(int)ERoleAttribute.ERA_MaxHP] = CalAttMod(ERoleAttribute.ERA_MaxHP, m_nAttr[(int)ERoleAttribute.ERA_MaxHP] );

					// 计算外功防御
					m_nAttr[(int)ERoleAttribute.ERA_ExDefense] = m_nAttr[(int)ERoleAttribute.ERA_Physique] * 5 + m_nAttr[(int)ERoleAttribute.ERA_ExDefense];
				}
				break;

				// 劲力，则二级属性的外功攻击，外功防御和持久力需要重新计算
				case ERoleAttribute.ERA_Strength:
				{
					// 计算外功攻击
					m_nAttr[(int)ERoleAttribute.ERA_ExAttack] = m_nAttr[ (int)ERoleAttribute.ERA_Strength] * 5;
					m_nAttr[(int)ERoleAttribute.ERA_ExAttack] = CalAttMod( ERoleAttribute.ERA_ExAttack, m_nAttr[(int)ERoleAttribute.ERA_ExAttack]);

				    // 计算外功防御
					m_nAttr[(int)ERoleAttribute.ERA_ExDefense]= m_nAttr[(int)ERoleAttribute.ERA_Physique] * 5 + m_nAttr[(int)ERoleAttribute.ERA_Strength];
					m_nAttr[(int)ERoleAttribute.ERA_ExDefense]= CalAttMod( ERoleAttribute.ERA_ExDefense, m_nAttr[(int)ERoleAttribute.ERA_ExDefense]);

				}
				break;

				// 元气，则二级属性的最大真气和内功防御需要重新计算
				case ERoleAttribute.ERA_Pneuma:
				{
					// 计算最大体力
					m_nAttr[(int)ERoleAttribute.ERA_MaxMP] = m_nAttr[(int)ERoleAttribute.ERA_Pneuma] * 10 + m_nbaseAttr[(int)ERoleAttribute.ERA_MaxMP];
					m_nAttr[(int)ERoleAttribute.ERA_MaxMP] = CalAttMod( ERoleAttribute.ERA_MP, m_nAttr[(int)ERoleAttribute.ERA_MaxMP]);

					
					// 计算内功防御
					m_nAttr[(int)ERoleAttribute.ERA_InDefense] = m_nAttr[(int)ERoleAttribute.ERA_Pneuma] * 5 + m_nAttr[(int)ERoleAttribute.ERA_InnerForce];
					m_nAttr[(int)ERoleAttribute.ERA_InDefense] =CalAttMod( ERoleAttribute.ERA_InDefense, m_nAttr[(int)ERoleAttribute.ERA_InDefense]);
				}
				break;

				// 内力，则二级属性的内功攻击，内功防御和持久力要重新计算
				case ERoleAttribute.ERA_InnerForce:
				{
					// 计算内功攻击
					m_nAttr[(int)ERoleAttribute.ERA_InAttack]	= m_nAttr[(int)ERoleAttribute.ERA_InnerForce] * 5;
					m_nAttr[(int)ERoleAttribute.ERA_InAttack]	= CalAttMod( ERoleAttribute.ERA_InAttack, m_nAttr[(int)ERoleAttribute.ERA_InAttack]);

					// 计算内功防御
					m_nAttr[(int)ERoleAttribute.ERA_InDefense]	= m_nAttr[(int)ERoleAttribute.ERA_Pneuma] * 5 + m_nAttr[(int)ERoleAttribute.ERA_InnerForce];
					m_nAttr[(int)ERoleAttribute.ERA_InDefense]	= CalAttMod( ERoleAttribute.ERA_InDefense, m_nAttr[(int)ERoleAttribute.ERA_InDefense ]);
				}
				break;
			}
		}

		// 再判断计算过后是否还有二级属性需要计算
		for (int n = (int)ERoleAttribute.ERA_AttB_Start; n < (int)ERoleAttribute.ERA_AttB_End; n++) 
		{

			if( GetAttRecalFlag((ERoleAttribute)n) == false )
				continue;

			// 最大体力
			if( n == (int)ERoleAttribute.ERA_MaxHP )
			{
				m_nAttr[(int)ERoleAttribute.ERA_MaxHP]	= m_nAttr[(int)ERoleAttribute.ERA_Physique] * 10 + m_nbaseAttr[(int)ERoleAttribute.ERA_MaxHP];
				m_nAttr[(int)ERoleAttribute.ERA_MaxHP]	= CalAttMod( ERoleAttribute.ERA_MaxHP, m_nAttr[(int)ERoleAttribute.ERA_MaxHP] );
			}

			// 最大真气
			else if( n == (int)ERoleAttribute.ERA_MaxMP )
			{
				m_nAttr[(int)ERoleAttribute.ERA_MaxMP]	= m_nAttr[(int)ERoleAttribute.ERA_Pneuma] * 10 + m_nbaseAttr[(int)ERoleAttribute.ERA_MaxMP];
				m_nAttr[(int)ERoleAttribute.ERA_MaxMP]	= CalAttMod( ERoleAttribute.ERA_MaxMP, m_nAttr[(int)ERoleAttribute.ERA_MaxMP] );
			}

			// 外功攻击
			else if( n == (int)ERoleAttribute.ERA_ExAttack )
			{
				m_nAttr[(int)ERoleAttribute.ERA_ExAttack]	= m_nAttr[(int)ERoleAttribute.ERA_Strength] * 5;
				m_nAttr[(int)ERoleAttribute.ERA_ExAttack]	= CalAttMod( ERoleAttribute.ERA_ExAttack, m_nAttr[(int)ERoleAttribute.ERA_ExAttack] );
			}

			// 外功防御
			else if( n == (int)ERoleAttribute.ERA_ExDefense )
			{
				m_nAttr[(int)ERoleAttribute.ERA_ExDefense]	= m_nAttr[(int)ERoleAttribute.ERA_Physique] * 5 + m_nAttr[(int)ERoleAttribute.ERA_Strength];
				m_nAttr[(int)ERoleAttribute.ERA_ExDefense]	= CalAttMod( ERoleAttribute.ERA_ExDefense, m_nAttr[(int)ERoleAttribute.ERA_ExDefense] );
			}

			// 内功攻击
			else if( n == (int)ERoleAttribute.ERA_InAttack )
			{
				m_nAttr[(int)ERoleAttribute.ERA_InAttack]	= m_nAttr[(int)ERoleAttribute.ERA_InnerForce] * 5;
				m_nAttr[(int)ERoleAttribute.ERA_InAttack]	= CalAttMod( ERoleAttribute.ERA_InAttack, m_nAttr[(int)ERoleAttribute.ERA_InAttack] );
			}

			//  内功防御
			else if( n == (int)ERoleAttribute.ERA_InDefense )
			{
				m_nAttr[(int)ERoleAttribute.ERA_InDefense]	= m_nAttr[(int)ERoleAttribute.ERA_Pneuma] * 5 + m_nAttr[(int)ERoleAttribute.ERA_InnerForce];
				m_nAttr[(int)ERoleAttribute.ERA_InDefense]	= CalAttMod( ERoleAttribute.ERA_InDefense, m_nAttr[(int)ERoleAttribute.ERA_InDefense] );
			}

			// 命中
			else if( n == (int)ERoleAttribute.ERA_HitRate )
			{
				m_nAttr[(int)ERoleAttribute.ERA_HitRate]	= m_nAttr[(int)ERoleAttribute.ERA_Technique] * 10;
				m_nAttr[(int)ERoleAttribute.ERA_HitRate]	= CalAttMod( ERoleAttribute.ERA_HitRate, m_nAttr[(int)ERoleAttribute.ERA_HitRate] );
			}

			//  闪避
			else if( n == (int)ERoleAttribute.ERA_Dodge )
			{
				m_nAttr[(int)ERoleAttribute.ERA_Dodge]	= m_nAttr[(int)ERoleAttribute.ERA_Agility] * 10;
				m_nAttr[(int)ERoleAttribute.ERA_Dodge]	= CalAttMod( ERoleAttribute.ERA_Dodge, m_nAttr[(int)ERoleAttribute.ERA_Dodge] );
			}

			// 持久力
			else if( n == (int)ERoleAttribute.ERA_MaxEndurance )
			{
				m_nAttr[(int)ERoleAttribute.ERA_MaxEndurance]	= (m_nAttr[(int)ERoleAttribute.ERA_Technique] + m_nAttr[(int)ERoleAttribute.ERA_Agility]) / 10;
				m_nAttr[(int)ERoleAttribute.ERA_MaxEndurance]	= CalAttMod( ERoleAttribute.ERA_MaxEndurance, m_nAttr[(int)ERoleAttribute.ERA_MaxEndurance] );
			}
		}

		// 附上原先保存的属性
		m_nAttr [(int)ERoleAttribute.ERA_HP] = nHP;
		m_nAttr [(int)ERoleAttribute.ERA_MP] = nMP;

		// 清空重算标志位
		ClearAttRecalFlag();

		OnAttChangeEvent ();
	}

	//--------------------------------------------------------------------------------
	// 计算属性加成最终值，并取上下限
	//--------------------------------------------------------------------------------
	public int CalAttMod( ERoleAttribute nIndex, int nBase )
	{
		int nValue = nBase + m_nAttMod [(int)nIndex] + (int)(nBase * (m_nAttModPct[(int)nIndex] / 10000.0f));
		return nValue;
	}

	//---------------------------------------------------------------------------------
	// 怪物死亡
	//---------------------------------------------------------------------------------
    public virtual void OnDead(CSceneEntity pKiller, CSkill pSkill)
	{
		
	}
	
	//---------------------------------------------------------------------------------
	// 怪物消失
	//---------------------------------------------------------------------------------
	public virtual void OnDisappear( )
	{
		
	}
	
	//----------------------------------------------------------------------------------
	// 生物击杀
	//----------------------------------------------------------------------------------
    public virtual void OnKill(CSceneEntity pTarget)
	{
		
	}
	
	//---------------------------------------------------------------------------------
	// 生物复活
	//---------------------------------------------------------------------------------
	public virtual void OnRevive( )
	{
		
		
	}
	

	//---------------------------------------------------------------------------------
	// 被攻击
	//---------------------------------------------------------------------------------
    public virtual void OnBeAttacked(CSceneEntity pSrc, CSkill pSkill, bool bHited, bool bBlock, bool bCrited)
	{
		if (CNGUISys.Inst != null && gameObject.transform.tag == "Player") 
		{
			//if( CNGUISys.Inst.m_CombatUI != null )
			//	CNGUISys.Inst.m_CombatUI.SendMessage( "ShowBeAttackEffect" );
		}
	}

	//---------------------------------------------------------------------------------
	// 游戏物体 HP 改变
	//---------------------------------------------------------------------------------
    public void ChangeHP(CSceneEntity pSrc, CSkill pSkill, int nValue, bool bCrit)
	{
		if (0 == nValue)
			return;

		ModAttValue (ERoleAttribute.ERA_HP, nValue);

		// 如果是见血事件
		if (nValue < 0) 
		{
			if( GetAttValue( ERoleAttribute.ERA_HP ) <= 0 )
			{
				SetAttValue(ERoleAttribute.ERA_HP, 0, false );
				OnDead( pSrc, pSkill );
			}

			ShowHPEffect( -nValue );
		}

		OnAttChangeEvent ();
	}


	private void ShowHPEffect( int iValue )
	{
		if (gameObject != null) 
		{
			Transform form =  gameObject.transform.Find("HeadMsg");
			if( form != null )
			{
				form.SendMessage("ShowHP", iValue);
			}
		}
	}

	//---------------------------------------------------------------------------------
	// 游戏物体 MP 改变
	//---------------------------------------------------------------------------------
	public void ChangeMP( int nValue )
	{
		if (0 == nValue)
			return;
		
		ModAttValue (ERoleAttribute.ERA_MP, nValue);

		OnAttChangeEvent ();
	}

	//-----------------------------------------------------------------------------------------
	// 设置状态
	//-----------------------------------------------------------------------------------------
	public void SetState( EState eState )
	{
		m_StateMgr.SetState (eState);
		OnSetState (eState);
	}

	//-----------------------------------------------------------------------------------------
	// 设置状态事件
	//-----------------------------------------------------------------------------------------
	private void OnSetState( EState eState )
	{

	}

	//-----------------------------------------------------------------------------------------
	// 取消状态
	//-----------------------------------------------------------------------------------------
	public void UnSetState( EState eState )
	{
		m_StateMgr.UnSetState (eState);
		OnUnSetState (eState);
	}

	//-----------------------------------------------------------------------------------------
	// 取消状态事件
	//-----------------------------------------------------------------------------------------
	private void OnUnSetState( EState eState )
	{
		
	}

	//-----------------------------------------------------------------------------------------
	// 状态查询事件
	//-----------------------------------------------------------------------------------------
	public bool IsInState( EState eState )
	{
		return m_StateMgr.IsInState( eState );
	}

	//-----------------------------------------------------------------------------------------
	// 是否死亡
	//-----------------------------------------------------------------------------------------
	public bool IsDead()
	{
		return m_StateMgr.IsInState (EState.ES_Dead );
	}


	public void OnAttChangeEvent()
	{
		if( CNGUISys.Inst != null )
		{
			CNGUISys.Inst.FightAttChangeEvent( 0 );
		}
	}
}

