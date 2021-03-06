using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;





class CFightTeamMgr
{


    private static CFightTeamMgr _instance = null;
    public static CFightTeamMgr Instance
    {
        get
        {
            if (_instance == null)
                _instance = new CFightTeamMgr();
            return _instance;
        }
    }


	public bool								  m_bActIsInCD = false;
	public Dictionary< uint, CHeroEntity>	  m_mapHero;
	public CHeroEntity						  m_pBattleHero;


	public int								  m_iEnteingMapID = 0;


	int 		m_nLevel;
	int			m_nCurExp;
	int			m_nCurMoney;
	int			m_nCurJewel;
	int			m_nCurPhysical;
	int			m_nMaxPhysical;

	public CFightTeamMgr()
	{
		
		m_pBattleHero 	= null;
		m_mapHero 		= new Dictionary< uint, CHeroEntity> ();

		m_nLevel 		= 1;
		m_nCurExp 		= 0;
		m_nCurMoney 	= 0;
		m_nCurJewel 	= 0;
		m_nCurPhysical 	= 0;
		m_nMaxPhysical 	= 0;
	}

	public void Initlize( )
	{
        //CreateHeroByID(10000);
	}


    /// --------------------------------------------------------------------------------
    /// <summary>
    /// 根据typeid 创建一个英雄
    /// </summary>
    /// --------------------------------------------------------------------------------
	public void CreateHeroByID( uint dwTypeID )
	{
        CHeroEntity pHero;
        m_mapHero.TryGetValue(dwTypeID, out pHero);
        if (pHero == null) 
		{
            pHero = CHeroEntity.CreateHero( dwTypeID );
            if (pHero != null)
                m_mapHero.Add(pHero.ID, pHero);
		}
	}


    /// --------------------------------------------------------------------------------
    /// <summary>
    /// 角色进入战场事件
    /// </summary>
    /// --------------------------------------------------------------------------------
	public void OnEnterBattleScene( )
	{
		if (m_pBattleHero != null) 
		{
			m_pBattleHero.EnterBattleMap();

            // 显示战斗属性界面
            // GameUIManager.Instance().HideFrame(GUIDefine.UIF_CITYMAINFRAME);
            // GameUIManager.Instance().ShowFrame(GUIDefine.UIF_BATTLEFRAME);
		}
	}


    /// --------------------------------------------------------------------------------
    /// <summary>
    /// 角色离开战场事件
    /// </summary>
    /// --------------------------------------------------------------------------------
    public void OnLeaveBattleMap()
    {
        if (m_pBattleHero != null)
        {
            m_pBattleHero.LeaveBattleMap();

            // 显示战斗属性界面
            GameUIManager.Instance().ShowFrame(GUIDefine.UIF_CITYMAINFRAME);
            GameUIManager.Instance().HideFrame(GUIDefine.UIF_BATTLEFRAME);
        }
    }

	public void		SelectHerobyID( uint dwID )
	{
		m_mapHero.TryGetValue( dwID, out m_pBattleHero);
	}


	public void SelectFristHero( )
	{
		foreach ( var item in m_mapHero )	
		{
			if( item.Value != null )
			{

				m_pBattleHero = item.Value;
				return;
			}
		}
	}

	public int ChangeHeroExp( int nValue )
	{
		if (m_pBattleHero == null)
			return -1;

		return m_pBattleHero.ChangeExp (nValue);
	}

	public int ChangeTeamExp( int nVal )
	{
		if( nVal < 0 )
		{
			m_nCurExp += nVal;
		}
		
		if( nVal > 0 )
		{
            tagTeamLevelEffect pEffect = CProtoManager.inst.GetTeamEffectLevel(m_nLevel);
			int nLevelUpExpRemain	 = pEffect.nExpLevelUp - m_nCurExp;
			if( nLevelUpExpRemain > nVal )
			{
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
                    pEffect = CProtoManager.inst.GetTeamEffectLevel(nNextLevel);
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

		OnChangeEvent ();
		return m_nCurExp;
	}


	private void  LevelChange( int nValue, bool bKill )
	{
		
		int MAX_LEVEL = 100;
		if (nValue < 0 || nValue > MAX_LEVEL)
			return;
		
		if (m_nLevel == nValue)
			return;


        tagTeamLevelEffect pEffect = CProtoManager.inst.GetTeamEffectLevel(m_nLevel);
		if( pEffect != null )
		{
			SetTeamMaxPhysical( pEffect.nPhysique );
		}

		int nMaxPhysical = m_nMaxPhysical;
		SetTeamMaxPhysical ( nMaxPhysical );

		OnChangeEvent ();
	}

	public int ChangeTeamMoney( int nValue )
	{
		m_nCurMoney += nValue;

		OnChangeEvent ();
		return m_nCurMoney;
	}


	public int ChangeTeamJewel( int nValue )
	{
		m_nCurJewel += nValue;

		OnChangeEvent ();
		return m_nCurJewel;
	}

	public int ChangeTeamCurPhysical( int nValue )
	{
		m_nCurPhysical += nValue;

		OnChangeEvent ();
		return m_nCurPhysical;
	}


	public int ChangeTeamMaxPhysical( int nValue )
	{
		m_nMaxPhysical += nValue;

		OnChangeEvent ();
		return m_nMaxPhysical;
	}

	public void SetTeamMaxPhysical( int nValue )
	{
		m_nMaxPhysical = nValue;
	}

	void OnChangeEvent( )
	{
		if( CNGUISys.Inst != null )
		{
			CNGUISys.Inst.FightAttChangeEvent( 2 );
		}
	}
}
