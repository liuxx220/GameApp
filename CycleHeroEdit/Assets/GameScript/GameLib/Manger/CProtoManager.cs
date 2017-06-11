using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;






class CProtoManager : View
{


	public static CProtoManager inst = null;

	// 各种数据字典
	public Dictionary< uint, tagHeroProto > 	m_mapHero;
	public Dictionary< uint, tagSkillProto > 	m_mapSkill;
	public Dictionary< uint, tagItemProto > 	m_mapItem;
	public Dictionary< uint, tagEquipProto > 	m_mapEquip;
	public Dictionary< uint, tagEntityProto > 	m_mapEntity;
	public Dictionary< uint, tagNpcMapPos >		m_mapNPCPos;

	public Dictionary< int, tagChapterProto > 	m_mapChapter;
	public Dictionary< int, tagInstanceProto > 	m_mapInstance;
	public List<tagLevelUpEffect>  				m_mapLevelExp;
	public List<tagTeamLevelEffect>  			m_mapTeamUpExp;
	public Dictionary< int, List<tagInstanceProto> > 	m_mapChaID2map;
    public CProtoManager()
	{
		inst 			= this;
		m_mapHero  		= new Dictionary< uint, tagHeroProto >();
		m_mapSkill  	= new Dictionary< uint, tagSkillProto >();
		m_mapItem  		= new Dictionary< uint, tagItemProto >();
		m_mapEquip  	= new Dictionary< uint, tagEquipProto >();
		m_mapEntity 	= new Dictionary< uint, tagEntityProto >();
		m_mapNPCPos 	= new Dictionary<uint, tagNpcMapPos> ();

		m_mapChapter	= new Dictionary< int, tagChapterProto >();
		m_mapInstance	= new Dictionary< int, tagInstanceProto >();
		m_mapChaID2map 	= new Dictionary< int, List<tagInstanceProto> >();

		m_mapLevelExp   = new List<tagLevelUpEffect> ();
		m_mapTeamUpExp	= new List<tagTeamLevelEffect> ();
	}

	public void LoadXMLS( )
	{
		LoadHeroConfig ();
		LoadSkillConfig ();

		LoadItemConfig ();

		LoadEquipConfig ();

		LoadEntityConfig ();

		LoadChapterConfig ();

		LoadMapConfig ();

		LoadLevelExp ();

		LoadTeamLevelExp ();

		LoadNpcPos ();
	}
	
	public void LoadHeroConfig( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > herolist = new List<string > ();
		if (xml.LoadXML ("data/hero_proto", "id", herolist)) 
		{

			for( int i = 0; i < herolist.Count; i++ )
			{
				tagHeroProto hero 	= new tagHeroProto();
				hero.dwID 			= xml.GetDword( "id",   herolist[i] );
                hero.strName        = xml.GetString("name", herolist[i], "");
                hero.strDesc        = xml.GetString("desc", herolist[i], "");
				hero.strIcon      	= xml.GetString("icon", herolist[i], ""  );
				hero.strModel      	= xml.GetString("model", herolist[i], ""  );
				hero.dwSkill1		= xml.GetDword( "normalskill1", herolist[i] );
				hero.dwSkill2		= xml.GetDword( "normalskill2", herolist[i] );
                hero.dwSkill3       = xml.GetDword( "normalskill3", herolist[i]);
                hero.dwSkill4       = xml.GetDword( "normalskill4", herolist[i]); 
				m_mapHero.Add( hero.dwID, hero );
			}	
		}
	}


	public void LoadSkillConfig( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > herolist = new List<string > ();
		if (xml.LoadXML ("data/skill_proto", "id", herolist)) 
		{
			
			for( int i = 0; i < herolist.Count; i++ )
			{
				tagSkillProto skill = new tagSkillProto();
				skill.dwID 			= xml.GetDword( "id",   herolist[i] );

				skill.eType			= (ESkillType)xml.GetInt( "type", herolist[i], 0 );
				skill.eUseType		= (ESkillUseType)xml.GetInt( "usetype", herolist[i], 0 );
				skill.ePriority		= (SkillPriority)xml.GetInt( "Priority", herolist[i], 0 );
				skill.eDmgType		= (ESkillDmgType)xml.GetInt( "Dmgtype", herolist[i], 0 );
				skill.eCostType		= (ESkillCostType)xml.GetInt( "CostType", herolist[i], 0 );
                skill.eFingerType   = (ESKILLFINGERTYPE)xml.GetInt("FingerType", herolist[i], 0);


				skill.strIcon      	= xml.GetString("icon", herolist[i], ""  );
				skill.strName      	= xml.GetString("name", herolist[i], ""  );
				skill.strPrefabFile = xml.GetString("prefab", herolist[i], ""  );
				skill.strdesc      	= xml.GetString("desc", herolist[i], ""  );


				skill.ActID			= xml.GetInt( "ActMode", herolist[i], 0 );
				skill.nDmgValues	= xml.GetInt( "DmgValue", herolist[i], 1);
				skill.nCostValue	= xml.GetInt( "CostValue", herolist[i], 0 );
				skill.fOPDist		= xml.GetFloat( "distance", herolist[i], 0 );
				skill.fOPRadius		= xml.GetFloat( "radius", herolist[i], 0 );
				skill.nCoolDown		= xml.GetInt( "CoolDown", herolist[i], 1000 );
				m_mapSkill.Add( skill.dwID, skill );
			}	
		}
	}

	public void LoadItemConfig( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > herolist = new List<string > ();
		if (xml.LoadXML ("data/item_proto", "id", herolist)) 
		{
			
			for( int i = 0; i < herolist.Count; i++ )
			{
				tagItemProto item = new tagItemProto();
				item.dwTypeID 		= xml.GetDword( "id",   herolist[i] );
				item.eType			= (EItemType)xml.GetDword( "type", herolist[i] );
				item.byLevel		= xml.GetInt( "level", 		herolist[i], 1 );
				item.byQuality		= (byte)xml.GetInt( "quality", 	herolist[i], 0 );
				item.nBasePrice     = xml.GetInt( "Price", 		herolist[i], 1 );
				item.nMaxUseTimes	= xml.GetInt( "MaxUseTime", herolist[i], 1 );
				item.nMaxLapNum		= xml.GetInt( "MaxLap", 	herolist[i], 1 );


				item.eSpecFunc		= (EItemSpecFunc)xml.GetInt( "SpecFunction", herolist[i], 0 );
				item.nSpecFuncVal1	= xml.GetInt( "SpecValue1", herolist[i], 0 );
				item.nSpecFuncVal2	= xml.GetInt( "SpecValue2", herolist[i], 0 );


				item.strIcon      	= xml.GetString("icon", herolist[i], ""  );
				item.strName      	= xml.GetString("name", herolist[i], ""  );
				item.strdesc      	= xml.GetString("desc", herolist[i], ""  );
				m_mapItem.Add( item.dwTypeID, item );
			}	
		}
	}

	public void LoadEquipConfig( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > herolist = new List<string > ();
		if (xml.LoadXML ("data/equip_proto", "id", herolist)) 
		{
			
			for( int i = 0; i < herolist.Count; i++ )
			{
				tagEquipProto item = new tagEquipProto();
				item.dwTypeID 		= xml.GetDword( "id",   herolist[i] );

				item.eType			= (EItemType)xml.GetDword( "type", herolist[i] );
				item.byLevel		= xml.GetInt( "level", 		herolist[i], 1 );
				item.byQuality		= (byte)xml.GetInt( "quality", 	herolist[i], 0 );
				item.nBasePrice     = xml.GetInt( "Price", 		herolist[i], 1 );

				item.dwSuitID		= xml.GetDword( "SuitID", herolist[i]);
				item.eEquipPos		= (EEquipPos)xml.GetInt( "EquipPos", herolist[i], 0 );
				item.nMinDmg		= xml.GetInt( "WeaponMinDmg", herolist[i], 1 );
				item.nMaxDmg		= xml.GetInt( "WeaponMaxDmg", herolist[i], 1 );
				item.nRepairPrice	= xml.GetInt( "RepairPrice", herolist[i], 1 );
				item.nHolePrice		= xml.GetInt( "HolePrice", herolist[i], 1 );

				item.dwFormulaID	= xml.GetDword( "FormulaID", herolist[i]);
				item.dwDeFoemulaID	= xml.GetDword( "DeFoemulaID", herolist[i] );

				item.BaseEffect[0].eRoleAtt 	= (ERoleAttribute)xml.GetDword( "AttType1", herolist[i] );
				item.BaseEffect[0].nValue 		= xml.GetInt( "AttValue1", herolist[i], 1 );
				item.BaseEffect[1].eRoleAtt 	= (ERoleAttribute)xml.GetDword( "AttType2", herolist[i] );
				item.BaseEffect[1].nValue 		= xml.GetInt( "AttValue2", herolist[i], 1);
				item.BaseEffect[2].eRoleAtt 	= (ERoleAttribute)xml.GetDword( "AttType3", herolist[i] );
				item.BaseEffect[2].nValue 		= xml.GetInt( "AttValue3", herolist[i], 1 );

				item.strIcon      	= xml.GetString("icon", herolist[i], ""  );
				item.strName      	= xml.GetString("name", herolist[i], ""  );
				item.strdesc      	= xml.GetString("desc", herolist[i], ""  );
				m_mapEquip.Add( item.dwTypeID, item );
			}	
		}
	}

	private void LoadEntityConfig()
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > herolist = new List<string > ();
		if (xml.LoadXML ("data/entity_proto", "id", herolist)) 
		{
			
			for( int i = 0; i < herolist.Count; i++ )
			{
				tagEntityProto item = new tagEntityProto();
				item.nBaseAtt		= new int[5];
				item.dwTypeID 		= xml.GetDword( "id",   	herolist[i] );
				item.eType      	= (ECreatureType)xml.GetInt("type", herolist[i], 0 );
				item.szName      	= xml.GetString("name", 	herolist[i], ""  );
				item.szIcon			= xml.GetString("Icon", 	herolist[i], "" );
				item.szModelName    = xml.GetString("model", 	herolist[i], ""  );
				item.nLevel			= xml.GetInt( "Level", 		herolist[i], 1 );

				item.bCanAttack		= xml.GetInt( "CanAtk", 	herolist[i], 1 ) == 1 ?  true : false;
				item.bCanBeAttack	= xml.GetInt( "CanBeAtk", 	herolist[i], 1 )  == 1 ?  true : false ;

				item.nRespawnTime	= xml.GetInt( "spawnTime", 	herolist[i], 1 );
				item.nLiveTime		= xml.GetInt( "LiveTime", 	herolist[i], 0 );
				item.nExpGive		= xml.GetInt( "exp", 		herolist[i], 1 );
				item.dwLootID		= xml.GetDword( "LootID", 	herolist[i] );

				item.dwNormalSkill	= xml.GetDword( "normalskill1", 	herolist[i] );
				item.dwLongDisSkill	= xml.GetDword( "normalskill2", 	herolist[i] );

				item.fLookdistance	= xml.GetFloat( "Lookdist", 		herolist[i], 1 );
				item.nPatrolRadius	= xml.GetFloat( "Patroldist", 		herolist[i], 1 );
				//-----------------------------------------------------------------------
				// 怪物的一级属性
				item.nBaseAtt[0]	= xml.GetInt( "Physique", 	herolist[i], 0 );
				item.nBaseAtt[1]	= xml.GetInt( "Strength", 	herolist[i], 0 );
				item.nBaseAtt[2]	= xml.GetInt( "Pneuma", 	herolist[i], 0 );
				item.nBaseAtt[3]	= xml.GetInt( "InnerForce", herolist[i], 0 );
				item.nBaseAtt[4]	= xml.GetInt( "Agility", 	herolist[i], 0 );

				m_mapEntity.Add( item.dwTypeID, item );
			}	
		}
	}

	private void LoadChapterConfig( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > list = new List<string > ();
		if (xml.LoadXML ("data/chapter_proto", "id", list)) 
		{
			
			for( int i = 0; i < list.Count; i++ )
			{
				tagChapterProto item = new tagChapterProto();
				item.chapterID 		= xml.GetInt( "id",   		list[i], 0 );
				item.iEnterLevel	= xml.GetInt( "level", 		list[i], 0 );

				item.strName     	= xml.GetString("name", list[i], ""  );
				item.strBG      	= xml.GetString("icon", list[i], ""  );

				m_mapChapter.Add( item.chapterID, item );
			}	
		}
	}

	private void LoadMapConfig( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > list = new List<string > ();
		if (xml.LoadXML ("data/map_proto", "id", list)) 
		{
			
			for( int i = 0; i < list.Count; i++ )
			{
				tagInstanceProto item = new tagInstanceProto();
				item.dwID 			= xml.GetInt( "id",   		list[i], 0 );
				item.chapterID		= xml.GetInt( "chapterID", 	list[i], 0 );
				item.iEnterLevel	= xml.GetInt( "level", 		list[i], 0 );
				item.iPosX			= xml.GetInt( "PosX", 		list[i], 0 );
				item.iPosY			= xml.GetInt( "PosY", 		list[i], 0 );
				
				item.strMapName     = xml.GetString("name", list[i], ""  );
				item.strIcon      	= xml.GetString("icon", list[i], ""  );
				
				m_mapInstance.Add( item.dwID, item );
			}	
		}

		// 整合数据
		CaleChapter2Instance ();
	}

	private void CaleChapter2Instance( )
	{

		foreach ( var item in m_mapChapter)
		{
			tagChapterProto pChapter = item.Value;
			if( pChapter == null )
				continue;

			foreach ( var i in m_mapInstance)
			{
				tagInstanceProto pInstance = i.Value;
				if( pInstance != null )
				{
					List<tagInstanceProto> maplist = null;
					m_mapChaID2map.TryGetValue( pChapter.chapterID, out maplist );
					if( maplist != null )
					{
						if( pChapter.chapterID == pInstance.chapterID )
						{
							maplist.Add( pInstance );
						}
					}
					else 
					{
						maplist = new List<tagInstanceProto>();
						if( pChapter.chapterID == pInstance.chapterID )
						{
							maplist.Add( pInstance );
						}
						m_mapChaID2map.Add( pChapter.chapterID, maplist );
					}

				}
			}
		}
	}


	private void LoadLevelExp( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > list = new List<string > ();
		if (xml.LoadXML ("data/level_up", "id", list)) 
		{
			
			for( int i = 0; i < list.Count; i++ )
			{
				tagLevelUpEffect item = new tagLevelUpEffect();
				item.nLevel 		= xml.GetInt( "id",   			list[i], 0 );
				item.nExpLevelUp	= xml.GetInt( "ExpLevelUp", 	list[i], 0 );
				item.nRoleAtt[0]	= xml.GetInt( "Physique", 		list[i], 0 );
				item.nRoleAtt[1]	= xml.GetInt( "Strength", 		list[i], 0 );
				item.nRoleAtt[2]	= xml.GetInt( "Pneuma", 		list[i], 0 );
				item.nRoleAtt[3]	= xml.GetInt( "InnerForce", 	list[i], 0 );
				item.nRoleAtt[4]	= xml.GetInt( "Technique", 		list[i], 0 );
				item.nMaxHP     	= xml.GetInt( "HP", 			list[i], 0  );
				item.nMaxMP      	= xml.GetInt( "MP", 			list[i], 0  );
				
				m_mapLevelExp.Add( item );
			}	
		}
	}


	public tagLevelUpEffect GetEffectLevel( int nLevel )
	{

		if (nLevel >= m_mapLevelExp.Count)
			return null;

		return m_mapLevelExp [nLevel];
	}


	private void LoadTeamLevelExp( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > list = new List<string > ();
		if (xml.LoadXML ("data/team_uplevel", "id", list)) 
		{
			
			for( int i = 0; i < list.Count; i++ )
			{
				tagTeamLevelEffect item = new tagTeamLevelEffect();
				item.nLevel 		= xml.GetInt( "id",   			list[i], 0 );
				item.nExpLevelUp	= xml.GetInt( "ExpLevelUp", 	list[i], 0 );
				item.nPhysique     	= xml.GetInt( "Physique", 		list[i], 0  );

				m_mapTeamUpExp.Add( item );
			}	
		}
	}

	public tagTeamLevelEffect GetTeamEffectLevel( int nLevel )
	{
		
		if (nLevel >= m_mapTeamUpExp.Count)
			return null;
		
		return m_mapTeamUpExp [nLevel];
	}

	private void LoadNpcPos( )
	{
		CXmlContainer xml = new CXmlContainer ();
		List< string > list = new List<string > ();
		if (xml.LoadXML ("data/npc_pos", "id", list)) 
		{
			
			for( int i = 0; i < list.Count; i++ )
			{
				tagNpcMapPos item = new tagNpcMapPos();
				item.dwNpcID 		= xml.GetDword( "id",   		list[i] );
				item.fPosX			= xml.GetFloat( "PosX", 		list[i], 0.0f );
				item.fPosY	     	= xml.GetFloat( "PosY", 		list[i], 0.0f  );
				item.fPosZ	     	= xml.GetFloat( "PosZ", 		list[i], 0.0f  );

				m_mapNPCPos.Add( item.dwNpcID, item );
			}	
		}
	}
}
