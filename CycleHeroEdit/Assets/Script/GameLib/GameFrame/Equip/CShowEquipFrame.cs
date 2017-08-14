using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






public class CShowEquipFrame : GUIFrame
{
	
	// attr's of hero show
	UnityEngine.GameObject			pShowEquip;
	UnityEngine.GameObject			pShengji;
	UnityEngine.GameObject			pZhuangbei;
	UnityEngine.GameObject			pEquipName;
	UnityEngine.GameObject			pEquipZhanli;
	UnityEngine.GameObject			pClose;

	UnityEngine.GameObject			pLevel;
	UnityEngine.GameObject			pVSattr;
	UnityEngine.GameObject			pNameVS;


	CEquipment						mEquip   = null;
	bool							mIsEquip = true;

	public Transform				mCharacterEquip;


	void Awake ()     
	{

		pShowEquip 			= UnityEngine.GameObject.Find("show");
		pShengji  			= UnityEngine.GameObject.Find("shengji");
		pZhuangbei 			= UnityEngine.GameObject.Find("zhuangbei");
		pEquipName  		= UnityEngine.GameObject.Find("equipname");
		pEquipZhanli 		= UnityEngine.GameObject.Find("equipzhanli");
		pClose 				= UnityEngine.GameObject.Find("showclose");

		pLevel 				= UnityEngine.GameObject.Find("equiplevel");
		pVSattr 			= UnityEngine.GameObject.Find("VSATT");
		pNameVS 			= UnityEngine.GameObject.Find("biaojiao");

		UIEventListener.Get( pZhuangbei ).onClick   = onEquipClick;
		UIEventListener.Get( pShengji ).onClick     = onShengJIClick;
		UIEventListener.Get( pClose ).onClick       = onCloseClick;
	}

	void Start( )
	{

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


	public IEnumerator OnOpenEquipShow( object _param  )
	{
        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero != null)
		{	
			// para [0]  item ' pos
			int idxPos 	    = (int)(_param as object[])[0];
			int bagorequip 	= (int)(_param as object[] )[1];
			mIsEquip 		= (bool)(_param as object[] )[2];

			mEquip = null;
			if (bagorequip == 1)
				mEquip = (CEquipment)CItemMgr.Inst.m_pPocket.GetPos ((Int16)idxPos);
			if (bagorequip == 2)
				mEquip = (CEquipment)pHero.m_pEquipment.GetPos ((Int16)idxPos);

			if( mEquip != null )
				RefushEquipItem ( mIsEquip );
		}
		yield return new WaitForSeconds(0.1f);
	}


	void onEquipClick( UnityEngine.GameObject item )
	{

        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero == null)
			return;

		if (mIsEquip && mEquip != null )
			pHero.HandleHeroEquip (mEquip);

		if (!mIsEquip && mEquip != null)
			pHero.HandleHeroUnequip (mEquip);

		if (mCharacterEquip != null)
			mCharacterEquip.gameObject.SetActive (true);
		
		transform.gameObject.SetActive( false );
	}


	void onShengJIClick( UnityEngine.GameObject item )
	{
        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero == null)
			return;
	}

	void onCloseClick( UnityEngine.GameObject item )
	{
		if (mCharacterEquip != null)
			mCharacterEquip.gameObject.SetActive (true);

		transform.gameObject.SetActive( false );
	}

	void RefushEquipItem( bool bEquip )
	{


        CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (pHero == null)
			return;

		FillEquipIcon ( pShowEquip );
		pEquipName.GetComponent<UILabel> ().text 	= "名称：  " + mEquip.m_pEquipProto.strName;
		pEquipZhanli.GetComponent<UILabel> ().text 	= "战力：  " + UnityEngine.Random.Range( 10, 100 ).ToString();
		pLevel.GetComponent<UILabel> ().text 		= "等级：  " + mEquip.m_pEquipProto.byLevel.ToString ();

		// attr compare

		EEquipPos ePos 		 = mEquip.m_pEquipProto.eEquipPos;
		CEquipment pCurEquip = (CEquipment)pHero.m_pEquipment.GetPos ( (Int16)ePos );

		string strName 	= mEquip.m_pEquipProto.strName;
		if( pCurEquip != null && bEquip )
		{
			strName += "VS" + pCurEquip.m_pEquipProto.strName;
		}
		pNameVS.GetComponent<UILabel>().text 	= strName;


		int nValue = 0;
		string strInfo = "";
		for (int i = 0; i < 3; i++) 
		{
			tagRoleAttEffect pEffect = mEquip.m_equipex.EquipBaseAtt[i];
			if( pEffect == null )
				return ;

			nValue  = pEffect.nValue;
			strInfo += GetAttName( pEffect.eRoleAtt ) + nValue.ToString();

			int nCurValue = 0;
			if( pCurEquip != null && bEquip )
			{
				for (int j = 0; j < 3; j++) 
				{
					tagRoleAttEffect pCur = pCurEquip.m_equipex.EquipBaseAtt[i];
					if( pCur != null && pEffect.eRoleAtt == pCur.eRoleAtt && pCur.nValue > 0)
					{
						nCurValue = pCur.nValue;
						break;
					}
				}

				strInfo += " ( " + ( nValue - nCurValue ).ToString() + " )";
			}

			strInfo += '\n';
		}

		pVSattr.GetComponent<UILabel> ().text = strInfo;
	}

	void FillEquipIcon( UnityEngine.GameObject obj )
	{
		
		if (obj != null) 
		{
			Transform pIcon = obj.transform.Find("Icon");
			if( pIcon != null )
			{
				UIAtlas tu = Resources.Load("GameIcon", typeof(UIAtlas)) as UIAtlas;
				UnityEngine.GameObject ctrl = pIcon.gameObject;
				ctrl.GetComponent<UISprite>().atlas = tu;
				ctrl.GetComponent<UISprite>().spriteName = mEquip.m_pEquipProto.strIcon;
			}
		}
	}
}
