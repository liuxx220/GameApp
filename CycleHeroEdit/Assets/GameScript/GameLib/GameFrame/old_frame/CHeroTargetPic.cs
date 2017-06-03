using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;






public class CHeroTargetPic : MonoBehaviour
{


	UISlider 			m_pProgressHP = null;
	UISlider			m_pProgressMP = null;
	UILabel 			m_pLevel = null;
	CHeroEntity			m_pHero = null;

	int					m_nLastHP = 0;
	int					m_nLastMP = 0;
	int					m_nChangeHP = 0;
	int					m_nChangeMP = 0;
	bool				m_bUpdateHP = false;
	bool				m_bUpdateMp = false;

	void Awake ()     
	{	
		GameObject pHP  = UnityEngine.GameObject.Find( "Progress HP" );
		GameObject pMP  = UnityEngine.GameObject.Find( "Progress MP" );
		GameObject pLev = UnityEngine.GameObject.Find( "Level" );

		m_pProgressHP	= pHP.GetComponent<UISlider>();
		m_pProgressMP	= pMP.GetComponent<UISlider>();
		m_pLevel		= pLev.GetComponent<UILabel>();
	}


	void Start( )
	{
		m_pHero = CFightTeamMgr.Instance.m_pBattleHero;
		if (m_pHero != null) 
		{
			m_nLastHP = m_pHero.GetAttValue(ERoleAttribute.ERA_HP );
			m_nLastMP = m_pHero.GetAttValue(ERoleAttribute.ERA_MP );

			m_pLevel.text	= "LV:     " + m_pHero.m_nLevel.ToString();

			int nMaxHP = m_pHero.GetAttValue (ERoleAttribute.ERA_MaxHP);
			int nCurHP = m_pHero.GetAttValue (ERoleAttribute.ERA_HP);
			float vale = nCurHP * 1.0f / nMaxHP;
			m_pProgressHP.value = vale;

			int nMaxMP = m_pHero.GetAttValue (ERoleAttribute.ERA_MaxMP);
			int nCurMP = m_pHero.GetAttValue (ERoleAttribute.ERA_MP);
			vale 	   = nCurMP * 1.0f / nMaxMP;
			m_pProgressMP.value = vale;
		}
	}

	//--------------------------------------------------------------------------------
	// 
	//--------------------------------------------------------------------------------
	void Update( )
	{
		if (m_pHero != null) 
		{
			int nCurHP = m_pHero.GetAttValue(ERoleAttribute.ERA_HP );
			int nCurMP = m_pHero.GetAttValue(ERoleAttribute.ERA_MP );

			if( nCurHP != m_nLastHP )
			{
				m_nChangeHP += m_nLastHP - nCurHP;
				m_bUpdateHP = true;
				m_nLastHP   = nCurHP;
			}

			if( nCurMP != m_nLastMP )
			{
				m_nChangeMP += m_nLastMP - nCurMP;
				m_bUpdateMp = true;
				m_nLastMP   = nCurMP;
			}
		}
	}


	//--------------------------------------------------------------------------------
	// 固定更新
	//--------------------------------------------------------------------------------
	void FixedUpdate()
	{

		if ( m_bUpdateHP ) 
		{
			UpdateProgHP( );
		}

		if ( m_bUpdateMp ) 
		{
			UpdateProgMP ( );
		}
	}

	//--------------------------------------------------------------------------------
	// 更新英雄的血条
	//--------------------------------------------------------------------------------
	void UpdateProgHP( )
	{

		int nMaxHP = m_pHero.GetAttValue (ERoleAttribute.ERA_MaxHP);
		int nCurHP = m_pHero.GetAttValue (ERoleAttribute.ERA_HP);
		m_nChangeHP--;
		float value = (nCurHP + m_nChangeHP) * 1.0f / nMaxHP;
		m_pProgressHP.value = value;

		if (m_nChangeHP == 0) 
		{
			m_bUpdateHP = false;
		}

	}

	//--------------------------------------------------------------------------------
	// 更新英雄的蓝条
	//--------------------------------------------------------------------------------
	void UpdateProgMP( )
	{

		int nMaxMP = m_pHero.GetAttValue (ERoleAttribute.ERA_MaxMP);
		int nCurMP = m_pHero.GetAttValue (ERoleAttribute.ERA_MP);
		m_nChangeMP--;
		float value = (nCurMP + m_nChangeMP) * 1.0f / nMaxMP;
		m_pProgressHP.value = value;

		if (m_nChangeMP <= 0) 
		{
			m_bUpdateMp = false;
		}
	}
}
