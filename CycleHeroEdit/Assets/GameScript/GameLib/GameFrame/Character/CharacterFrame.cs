using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






public class CharacterFrame : GUIFrame
{
	
	// equip's of hero
	
	private int						m_curPage = 1;
	private int						m_nEquipments = 8;

	// attr's of hero show
    private UILabel                 pPhysique;
    private UILabel                 pPneuma;
    private UILabel                 pStrength;
    private UILabel                 pInnerForce;
    private UILabel                 pPrecise;

    private UILabel                 pLevel;
    private UILabel                 pPhysicalAttack;
    private UILabel                 pEnergyAttack;
    private UILabel                 pPhysicalDefense;
    private UILabel                 pEnergyDefense;
    private UILabel                 pHitRate;
    private UILabel                 pCritRate;

	
	UnityEngine.GameObject[]		m_Equips;

	public Transform 				mShowEquip;
	public Transform				mCharacterEquip;

    public override bool ReloadUI()
    {
        base.ReloadUI();

		
		m_Equips			= new GameObject[m_nEquipments];

        pPhysique           = transform.Find("Anchor/Physique").GetComponent<UILabel>();
        pPneuma             = transform.Find("Anchor/Pneuma").GetComponent<UILabel>();
        pStrength           = transform.Find("Anchor/Strength").GetComponent<UILabel>();
        pInnerForce         = transform.Find("Anchor/InnerForce").GetComponent<UILabel>();
        pPrecise            = transform.Find("Anchor/Precise").GetComponent<UILabel>();

        pLevel              = transform.Find("Anchor/bagLevel").GetComponent<UILabel>();
        pPhysicalAttack     = transform.Find("Anchor/PhysicalAttack").GetComponent<UILabel>();
        pEnergyAttack       = transform.Find("Anchor/EnergyAttack").GetComponent<UILabel>();
        pPhysicalDefense    = transform.Find("Anchor/PhysicalDefense").GetComponent<UILabel>();
        pEnergyDefense      = transform.Find("Anchor/EnergyDefense").GetComponent<UILabel>();
        pHitRate            = transform.Find("Anchor/HitRate").GetComponent<UILabel>();
        pCritRate           = transform.Find("Anchor/CritRate").GetComponent<UILabel>();


		// init role equipments 
		m_Equips[0]  		= UnityEngine.GameObject.Find("equip0");
		m_Equips[1]  		= UnityEngine.GameObject.Find("equip1");
		m_Equips[2]  		= UnityEngine.GameObject.Find("equip2");
		m_Equips[3]  		= UnityEngine.GameObject.Find("equip3");
		m_Equips[4]  		= UnityEngine.GameObject.Find("equip4");
		m_Equips[5]  		= UnityEngine.GameObject.Find("equip5");
		m_Equips[6]  		= UnityEngine.GameObject.Find("equip6");
		m_Equips[7]  		= UnityEngine.GameObject.Find("equip7");

		
		for (int j = 0; j < m_nEquipments; j++) 
		{
			UIEventListener.Get( m_Equips[j] ).onClick = onEquipClick; 
		}

        return true;
	}


	void RefushAttr( )
	{


        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero == null)
			return;

		if (mShowEquip != null)
			mShowEquip.gameObject.SetActive (false);
	
		int nValue = 0;
		nValue = pHero.m_nLevel;
		pLevel.text = "LV:  " + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_Physique);
		pPhysique.text = GetAttName(ERoleAttribute.ERA_Physique).ToString() + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_Strength);
		pStrength.text = GetAttName(ERoleAttribute.ERA_Strength) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_Pneuma);
		pPneuma.text = GetAttName(ERoleAttribute.ERA_Pneuma) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_InnerForce);
		pInnerForce.text = GetAttName(ERoleAttribute.ERA_InnerForce) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_Technique);
		pPrecise.text = GetAttName(ERoleAttribute.ERA_Technique) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_ExAttack);
		pPhysicalAttack.text = GetAttName(ERoleAttribute.ERA_ExAttack) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_ExDefense);
		pPhysicalDefense.text = GetAttName(ERoleAttribute.ERA_ExDefense) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_InAttack);
		pEnergyAttack.text = GetAttName(ERoleAttribute.ERA_InAttack) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_InAttack);
		pEnergyAttack.text = GetAttName(ERoleAttribute.ERA_InAttack) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_InDefense);
		pEnergyDefense.text = GetAttName(ERoleAttribute.ERA_InDefense) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_HitRate);
		pHitRate.text = GetAttName(ERoleAttribute.ERA_HitRate) + nValue.ToString ();

		nValue = pHero.GetAttValue (ERoleAttribute.ERA_Crit_Rate );
		pCritRate.text = GetAttName(ERoleAttribute.ERA_Crit_Rate ) + nValue.ToString ();
	}

	string GetAttName( ERoleAttribute e )
	{
		if (e == ERoleAttribute.ERA_Physique) 
		{ 
			return "体质:  ";
		}
		if (e == ERoleAttribute.ERA_Strength) 
		{
			return "力量:  ";
		}
		if (e == ERoleAttribute.ERA_Pneuma) 
		{
			return "精神:  ";
		}
		if (e == ERoleAttribute.ERA_InnerForce) 
		{
			return "坚毅:  ";
		}
		if (e == ERoleAttribute.ERA_Technique) 
		{
			return "精准:  ";
		}

		if (e == ERoleAttribute.ERA_ExAttack) 
		{
			return "物理攻击:  ";
		}
		if (e == ERoleAttribute.ERA_ExDefense) 
		{
			return "物理防御:  ";
		}
		if (e == ERoleAttribute.ERA_InAttack) 
		{
			return "能量攻击:  ";
		}
		if (e == ERoleAttribute.ERA_InDefense) 
		{
			return "能量防御:  ";
		}

		if (e == ERoleAttribute.ERA_HitRate) 
		{
			return "命中率:  ";
		}

		if (e == ERoleAttribute.ERA_Crit_Rate) 
		{
			return "暴击几率:  ";
		}
		return "";
	}


	public IEnumerator OnOpenCharacterBag( )
	{
		RefushAttr ();
		RefushEquipItem ();

		yield return new WaitForSeconds(0.1f);
	}


	public IEnumerator OnUpdateCharacterALL( )
	{

		RefushAttr ();
		RefushEquipItem ();

		yield return new WaitForSeconds(0.1f);
	}

	
	public IEnumerator OnUpdateCharacterATT( )
	{
		RefushAttr ();

		yield return new WaitForSeconds(0.1f);
	}

	void onEquipClick( UnityEngine.GameObject item )
	{
		for (int j = 0; j < m_nEquipments; j++) 
		{
			if( item == m_Equips[j] )
			{

                CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
				if (pHero == null)
					return ;

				CItem pItem = pHero.m_pEquipment.GetPos( (Int16)j );
				if( pItem == null )
					return ;

				object[] _param = new object[3];
				_param[0] = j;
				_param[1] = 2;
				_param[2] = false;
				
				if( mShowEquip == null )
					return;

				mCharacterEquip.gameObject.SetActive( false );
				mShowEquip.gameObject.SetActive( true );

				mShowEquip.SendMessage( "OnOpenEquipShow", _param, SendMessageOptions.DontRequireReceiver );
			}
		}
	}


	void RefushEquipItem( )
	{
		
		if (CItemMgr.Inst == null)
			return;

        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero == null)
			return;

		for (int i = 0; i < m_nEquipments; i++) 
		{
			CEquipment pItem = (CEquipment)pHero.m_pEquipment.GetPos( ( Int16 )i );
			if( pItem != null )
			{
				FillEquipIcon( m_Equips[i], pItem );
			}
			else
			{
				FillFreeIcon( m_Equips[i] );
			}
		}
	}


	void FillItemIcon( UnityEngine.GameObject obj, CItem pItem )
	{

		if (obj != null) 
		{
			Transform pIcon = obj.transform.Find("Icon");
			if( pIcon != null )
			{
				UIAtlas tu = Resources.Load("GameIcon", typeof(UIAtlas)) as UIAtlas;
				UnityEngine.GameObject ctrl = pIcon.gameObject;
				ctrl.GetComponent<UISprite>().atlas = tu;
				ctrl.GetComponent<UISprite>().spriteName = pItem.m_pProto.strIcon;
			}

			Transform pNum  = obj.transform.Find("num");
			if( pNum != null )
			{
				UnityEngine.GameObject ctrl = pNum.gameObject;

				if( pItem.GetItemNum() > 1 )
					ctrl.GetComponent<UILabel>().text = pItem.GetItemNum().ToString();
				else 
					ctrl.GetComponent<UILabel>().text ="";
			}
		}
	}

	void FillEquipIcon( UnityEngine.GameObject obj, CEquipment pEquip )
	{
		
		if (obj != null) 
		{
			Transform pIcon = obj.transform.Find("Icon");
			if( pIcon != null )
			{
				UIAtlas tu = Resources.Load("GameIcon", typeof(UIAtlas)) as UIAtlas;
				UnityEngine.GameObject ctrl = pIcon.gameObject;
				ctrl.GetComponent<UISprite>().atlas = tu;
				ctrl.GetComponent<UISprite>().spriteName = pEquip.m_pEquipProto.strIcon;
			}
		}
	}

	void FillFreeIcon( UnityEngine.GameObject obj )
	{
		if (obj != null) 
		{
			Transform pIcon = obj.transform.Find("Icon");
			if( pIcon != null )
			{
				UIAtlas tu = Resources.Load("GameUI", typeof(UIAtlas)) as UIAtlas;
				UnityEngine.GameObject ctrl = pIcon.gameObject;
				ctrl.GetComponent<UISprite>().atlas = tu;
				ctrl.GetComponent<UISprite>().spriteName = "btn_kuang";
			}
		}
	}

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {
        pPhysique       = null;
        pPneuma         = null;
        pStrength       = null;
        pInnerForce     = null;
        pPrecise        = null;

        pLevel          = null;
        pPhysicalAttack = null;
        pEnergyAttack   = null;
        pPhysicalDefense= null;
        pEnergyDefense  = null;
        pHitRate        = null;
        pCritRate       = null;

        base.Destroy();
    }
}
