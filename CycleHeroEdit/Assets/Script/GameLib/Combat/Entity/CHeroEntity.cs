using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






class CHeroEntity : CSceneEntity
{

	public CLocalPlayerFSM		m_FSM;
    public tagHeroProto         m_pProto;
	public int					m_nCurExp;			
	public int					m_nNextLevleExp;	
	public ItemContainer		m_pEquipment;


	private Vector3				m_TargetPos = Vector3.zero;
	private bool				m_bAutoFindPath = false;

    public CSkill               m_pMeleeSkill;
    public CSkill               m_pRangedSkill;	


	public CHeroEntity()
	{
		m_FSM 		= null;
		ID          = GenID.MakeHeroID();
		m_pEquipment= new ItemContainer ( EItemConType.EICT_Equip, 8 ); 
	}

	~CHeroEntity()  // destructor
    {
    	
    }
	
	public override void update( )
	{

		foreach ( var item in m_mapSkill )	
		{
			CSkill pSkill =  item.Value;
			if( pSkill != null )
			{
				pSkill.UpdateCD();
			}
		}
	}


	public void Fixedupdate( )
	{
		
	}


	public static CHeroEntity  CreateHero( uint dwTypeID )
	{
		CHeroEntity pHero = new CHeroEntity ();
		if (pHero != null)
		{
			pHero.InitHero (dwTypeID);
		}

		return pHero;
	}


	private void InitHero( uint dwTypeID )
	{

        CProtoManager.inst.m_mapHero.TryGetValue(dwTypeID, out m_pProto);
        if (m_pProto == null) 
		{
			Common.DEBUG_MSG("CHeroEntity's proto not find " + dwTypeID );
		}

        /// ��ʼ�첽������Դ
        asset.source        = m_pProto.strModel;
        asset.LoadAsset();

       
		m_nLevel 			= 1;
		m_nCurExp 			= 0;
		m_nNextLevleExp 	= 100;
		
		InitHeroAttr();
		InitHeroSkill();
		CalInitAtt ();
	}

    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// ģ����Դ�첽�������
    /// </summary>
    //-----------------------------------------------------------------------------------------
    public override void OnAsyncLoaded()
    {

        if (gameObject != null)
        {
            CPlayerCompent pCompent = gameObject.AddComponent<CPlayerCompent>() as CPlayerCompent;
            if (pCompent != null)
            {
                pCompent.m_pOwner = this;
            }

            m_AnimCtrl      = gameObject.GetComponentInChildren<Animator>();
            m_CtrlSkelton   = gameObject.GetComponent<CtrolAnimation>();
            m_FSM           = new CLocalPlayerFSM(this);
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

            gameObject.SetActive(false);
            UnityEngine.GameObject.DontDestroyOnLoad(gameObject);
        }
    }

	public void InitHeroAttr( )
	{
		SetBaseAttValue (ERoleAttribute.ERA_Physique, 10);
		SetBaseAttValue (ERoleAttribute.ERA_Strength, 15);
		SetBaseAttValue (ERoleAttribute.ERA_Pneuma,   20);
		SetBaseAttValue (ERoleAttribute.ERA_InnerForce, 12);
		SetBaseAttValue (ERoleAttribute.ERA_Technique, 21);
		SetBaseAttValue (ERoleAttribute.ERA_Agility, 18);
	}


	public void InitHeroSkill( )
	{
        if (m_pProto.dwSkill1 > 0) 
		{
            AddSkill(m_pProto.dwSkill1);
		}

        if (m_pProto.dwSkill2 > 0) 
		{
            AddSkill(m_pProto.dwSkill2);
		}

        if (m_pProto.dwSkill3 > 0)
        {
            AddSkill(m_pProto.dwSkill3);
        }

        if (m_pProto.dwSkill4 > 0)
        {
            AddSkill(m_pProto.dwSkill4);
        }
	}


	public void CalInitAtt( )
	{
		for (int i = 0; i < (int)ERoleAttribute.ERA_End; i++) 
		{
			m_nAttr[i] = CalAttMod( (ERoleAttribute)i, m_nbaseAttr[i] );
		}

		int nValue = GetAttValue (ERoleAttribute.ERA_Physique) * 10 + GetBaseAttValue (ERoleAttribute.ERA_MaxHP);
		SetAttValue (ERoleAttribute.ERA_MaxHP, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_Pneuma) * 10 + GetBaseAttValue (ERoleAttribute.ERA_MaxMP);
		SetAttValue (ERoleAttribute.ERA_MaxMP, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_Strength) * 5;
		SetAttValue (ERoleAttribute.ERA_ExAttack, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_Physique) * 5 + GetAttValue (ERoleAttribute.ERA_Strength);
		SetAttValue (ERoleAttribute.ERA_ExDefense, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_InnerForce) * 5;
		SetAttValue (ERoleAttribute.ERA_InAttack, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_Pneuma) * 5 + GetAttValue (ERoleAttribute.ERA_InnerForce);
		SetAttValue (ERoleAttribute.ERA_InDefense, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_Technique) * 10;
		SetAttValue (ERoleAttribute.ERA_HitRate, nValue, false );

		nValue = GetAttValue (ERoleAttribute.ERA_Agility) * 10;
		SetAttValue (ERoleAttribute.ERA_Dodge, nValue, false );

		int nMinWenpaneDmg = UnityEngine.Random.Range (10 * m_nLevel, 15 * m_nLevel);
		int nMaxWenpaneDmg = UnityEngine.Random.Range (15 * m_nLevel, 20 * m_nLevel);
		
		ModAttValue (ERoleAttribute.ERA_WeaponDmgMin, nMinWenpaneDmg);
		ModAttValue (ERoleAttribute.ERA_WeaponDmgMax, nMaxWenpaneDmg);

		int nMaxHP = GetAttValue (ERoleAttribute.ERA_MaxHP);
		int nMaxMP = GetAttValue (ERoleAttribute.ERA_MaxMP);

		SetAttValue (ERoleAttribute.ERA_HP, nMaxHP, false );
		SetAttValue (ERoleAttribute.ERA_MP, nMaxMP, false );
	}


	public tagHeroProto GetProto( )
	{
        return m_pProto;
	}

	public void EnterBattleMap( )
	{
		if (gameObject != null) 
		{
			GameObject reborn = UnityEngine.GameObject.Find("reborn");
			gameObject.SetActive( true );
			gameObject.transform.localScale = Vector3.one;
			if( reborn != null )
			{
				gameObject.transform.localPosition = reborn.transform.localPosition;
			}

            GameObject mainCamera = UnityEngine.GameObject.Find("Main Camera");
            if( mainCamera != null )
            {
                mainCamera.AddComponent<CameraFollow>();
            }
		}
	}

	public void LeaveBattleMap( )
	{
		if (gameObject != null) 
		{
			gameObject.SetActive( false );
		}
	}


	// handle hero equip or unequip
	public int HandleHeroEquip( CEquipment pEquip )
	{

		if (pEquip == null)
			return -1;

		int nRet = 0;
		EEquipPos ePos = pEquip.GetEquipPos ();
		if (ePos >= EEquipPos.EEP_Head && ePos < EEquipPos.EEP_Equip_End ) 
		{
			nRet = Equip( pEquip, ePos );
		}

		return nRet;
	}


	public int HandleHeroUnequip( CEquipment pEquip )
	{
		
		if (pEquip == null)
			return -1;

		int nRet = 0;
		EEquipPos ePos = pEquip.GetEquipPos ();
		if (ePos >= EEquipPos.EEP_Head && ePos < EEquipPos.EEP_Equip_End) 
		{
			nRet = UnEquip( pEquip );
		}
		return nRet;
	}

	int Equip( CEquipment pEquip, EEquipPos ePos )
	{

		if (!ItemCreator.MIsEquipment (pEquip.GetItemTypeID ()))
			return 1;

		if (pEquip.m_pEquipProto.byLevel > m_nLevel)
			return 2;

		CEquipment pSrc = (CEquipment)m_pEquipment.GetPos( (Int16)ePos );
		if (pSrc != null) 
		{
			// switch equip 's pos
			CItemMgr.Inst.m_pPocket.Remove( pEquip.GetItemID() );
			m_pEquipment.Equip( pEquip, (Int16)ePos );
			ProcEquipEffect(  pEquip, pSrc );
		}
		else
		{

			CItemMgr.Inst.m_pPocket.Remove( pEquip.GetItemID() );
			m_pEquipment.Equip( pEquip, (Int16)ePos );
			ProcEquipEffect(  pEquip, null );
		}

        //if( CNGUISys.Inst != null )
        //{
        //    CNGUISys.Inst.OnUpdateCharacterALL();
        //}
		return 0;
	}

	int UnEquip( CEquipment pEquip )
	{
		int nRet = 0;

		Int16 n16Index = CItemMgr.Inst.m_pPocket.GetFreeGrid ();
		if (n16Index < 0)
		{
			// pocket is full
			return 1;
		}

		m_pEquipment.UnEquip (pEquip.GetPos ());

		ProcEquipEffect (null, pEquip);

		CItemMgr.Inst.m_pPocket.Add (pEquip);

        //if( CNGUISys.Inst != null )
        //{
        //    CNGUISys.Inst.OnUpdateCharacterALL();
        //}
		return nRet;
	}

	void ProcEquipEffect( CEquipment pNewEquip, CEquipment pOldEquip )
	{

		if (pOldEquip != null) 
		{
			ProcEquipEffectAtt( pOldEquip, false );
		}

		if (pNewEquip != null) 
		{
			ProcEquipEffectAtt( pNewEquip, true );
		}

		RecalAtt ();
	}

	void ProcEquipEffectAtt( CEquipment pEquip, bool bEquip )
	{

		if( pEquip != null )
		{

			tagEquipProto pProto = pEquip.m_pEquipProto;
			if( pProto == null )
				return ;

			// weapon
			int nFactor = 1;
			if( !bEquip )
				nFactor = -1;

			if( pProto.eEquipPos == EEquipPos.EEP_Weapon )
			{
				if( bEquip )
				{
					ModBaseAttValue( ERoleAttribute.ERA_WeaponDmgMin, pProto.nMinDmg );
					ModBaseAttValue( ERoleAttribute.ERA_WeaponDmgMax, pProto.nMaxDmg );
				}
				else
				{
					ModBaseAttValue( ERoleAttribute.ERA_WeaponDmgMin, -pProto.nMinDmg );
					ModBaseAttValue( ERoleAttribute.ERA_WeaponDmgMax, -pProto.nMaxDmg );
				}
			}

			// cale weapon base att
			ChangeHeroAtt( pEquip.m_equipex.EquipBaseAtt, 3, nFactor );
		}
	}


	void ChangeHeroAtt( tagRoleAttEffect[] Effect, int iSize, int nFactor )
	{

		for( int i = 0; i < iSize; i++ )
		{
			if( Effect[i].eRoleAtt <= ERoleAttribute.ERA_Null && Effect[i].eRoleAtt > ERoleAttribute.ERA_End )
				continue;

			if( Helper.MIsValuePct(Effect[i].nValue) )
			{
				ModAttModValuePct( Effect[i].eRoleAtt, Helper.MValuePctTrans(Effect[i].nValue ) * nFactor );
			}
			else
			{
				ModAttModValue( Effect[i].eRoleAtt, Effect[i].nValue * nFactor );
			}
		}
	}


    /// --------------------------------------------------------------------------------------
    /// <summary>
    /// �ı�Ӣ�۵ľ���
    /// </summary>
    /// --------------------------------------------------------------------------------------
	public int ChangeExp( int nVal )
	{

		if( nVal < 0 )
		{
			m_nCurExp += nVal;
		}

		if( nVal > 0 )
		{
            tagLevelUpEffect pEffect = CProtoManager.inst.GetEffectLevel(m_nLevel);
			int nLevelUpExpRemain	 = pEffect.nExpLevelUp - m_nCurExp;
			if( nLevelUpExpRemain > nVal )
			{
				// not up level
				// 20 % 
				int nPhaseExp  = pEffect.nExpLevelUp / 5;
				int nOldPhase  = m_nCurExp / nPhaseExp;
				int nNewPhase  = (m_nCurExp + nVal ) / nPhaseExp;
				if( nOldPhase != nNewPhase )
				{
					SetAttValue( ERoleAttribute.ERA_HP, GetAttValue(ERoleAttribute.ERA_MaxHP ), false );
					SetAttValue( ERoleAttribute.ERA_MP, GetAttValue(ERoleAttribute.ERA_MaxMP ), false );
				}
				m_nCurExp  += nVal;
			}
			else
			{
				int  MAX_LEVEL   = 100;
				m_nCurExp		+= nLevelUpExpRemain;
				if( m_nLevel == MAX_LEVEL )
				{
					return 0;
				}
	
				nVal		   -= nLevelUpExpRemain;
				int nNextLevel  = m_nLevel + 1;

				for( ; nNextLevel <= MAX_LEVEL; nNextLevel++ )
				{
                    pEffect = CProtoManager.inst.GetEffectLevel(nNextLevel);
					if( pEffect.nExpLevelUp <= nVal )
					{
						nVal 	   -= pEffect.nExpLevelUp;
					}
					else
					{
						break;
					}
				}
			
				if( nNextLevel > MAX_LEVEL )
				{
					nNextLevel = MAX_LEVEL;
					nVal  	   = pEffect.nExpLevelUp;
				}

				LevelChange( nNextLevel, false );
				m_nCurExp = nVal;
			}
		}

		OnAttChangeEvent ();
		return m_nCurExp;
	}


    /// --------------------------------------------------------------------------------------
    /// <summary>
    /// �ı�Ӣ�۵ĵȼ�
    /// </summary>
    /// --------------------------------------------------------------------------------------
	private void  LevelChange( int nValue, bool bKill )
	{

		int MAX_LEVEL = 100;
		if (nValue < 0 || nValue > MAX_LEVEL)
			return;

		if (m_nLevel == nValue)
			return;

		m_nLevel = nValue;
        tagLevelUpEffect pEffect = CProtoManager.inst.GetEffectLevel(m_nLevel);
		if( pEffect != null )
		{
			SetBaseAttValue(ERoleAttribute.ERA_Physique, 	pEffect.nRoleAtt[0] );
			SetBaseAttValue(ERoleAttribute.ERA_Strength, 	pEffect.nRoleAtt[1] );
			SetBaseAttValue(ERoleAttribute.ERA_Pneuma, 	 	pEffect.nRoleAtt[2] );
			SetBaseAttValue(ERoleAttribute.ERA_InnerForce,	pEffect.nRoleAtt[3] );
			SetBaseAttValue(ERoleAttribute.ERA_Technique, 	pEffect.nRoleAtt[4] );

			SetBaseAttValue(ERoleAttribute.ERA_MaxHP,		pEffect.nMaxHP );
			SetBaseAttValue(ERoleAttribute.ERA_MaxMP, 		pEffect.nMaxMP );
		}

		RecalAtt();

		SetAttValue( ERoleAttribute.ERA_HP, GetAttValue(ERoleAttribute.ERA_MaxHP ), false );
		SetAttValue( ERoleAttribute.ERA_MP, GetAttValue(ERoleAttribute.ERA_MaxMP ), false );

		OnAttChangeEvent ();
	}


	public void SetTargetPos( Vector3 vTargetPos, bool bAuto )
	{
		m_TargetPos 	= vTargetPos;
		m_bAutoFindPath = bAuto;
	}
}

