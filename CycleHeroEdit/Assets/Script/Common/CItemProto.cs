using System;
using UnityEngine;






enum EEquipPos	
{
	EEP_Null			= -1, 
	
	EEP_Equip_Start		= 0,
	EEP_Head			= 0,	
	EEP_Face			= 1,	
	EEP_Body			= 2,	
	EEP_Legs			= 3,	
	EEP_Feet			= 4,	
	EEP_Neck			= 5,	
	EEP_Finger			= 6,	
	EEP_Weapon			= 7,
	EEP_Equip_End		= 8
}


// item's type
enum EItemType
{
	EIT_Null			= 0,
	EIT_ITEM			= 1,
	EIT_WEAPON			= 2
}


// item and equip's quality 5 level
enum EItemQuality
{

	EIQ_Null		= -1,
	EIQ_White		= 0,
	EIQ_Green		= 1,
	EIQ_Blue		= 2,
	EIQ_Purple		= 3,
	EIQ_Orange		= 4,
	EIQ_End			= 5
}

// use item limit by class
enum EClassLimit
{
	ECL_Null		= -1,
	ECL_All			= 0,  // all ' class meby use
}


// item's spec function 
enum  EItemSpecFunc
{

	EISF_Null		= 0, // no use type
}


// item is container type
enum EItemConType
{
	EICT_Null		= 0,
	EICT_Bag		= 1,  // bag
	EICT_Equip		= 2,
	EICT_Shop		= 3,
	EICT_RoleWare   = 4,
	EICT_GuildWare  = 5
}


class tagRoleAttEffect
{
	public ERoleAttribute	eRoleAtt;
	public int				nValue;
}


class tagItemProto
{
	public uint			dwTypeID;
	public EItemType	eType;

	public int			byLevel;
	public byte			byQuality;

	public int			nBasePrice;
	public int			nMaxUseTimes;
	public int 			nMaxLapNum;

	public EItemSpecFunc eSpecFunc;
	public int			nSpecFuncVal1;
	public int			nSpecFuncVal2;

	public string 		strName;
	public string		strIcon;
	public string 		strdesc;
}


class tagEquipProto : tagItemProto
{

	public uint			dwSuitID;
	public EEquipPos	eEquipPos;

	public int			nMinDmg;
	public int 			nMaxDmg;

	public int			nRepairPrice;
	public int			nHolePrice;

	public uint			dwFormulaID;
	public uint			dwDeFoemulaID;


	public tagRoleAttEffect[]	BaseEffect;

	public tagEquipProto()
	{
		BaseEffect = new tagRoleAttEffect[3];
		for( int i = 0; i < 3; i++ )
		{
			BaseEffect[i] = new tagRoleAttEffect();
		}
	}
}


class tagItem
{
	
	// accont data member of class
	public Int64		n64Serial;
	public uint			dwTypeID;
	
	public EItemConType	eConType;
	public Int16		n16Index;
	public Int16		n16Num;
	

	public tagItem( )
	{
		
	}
	
	public Int64 GetKey()
	{
		return n64Serial;
	}
	
	public void SetPos( EItemConType e, Int16 n16NewIndex )
	{
		eConType = e;
		n16Index = n16NewIndex;
	}
	
	public Int16 GetNum()
	{
		return n16Num;
	}
}


class tagEquipSpec
{
	public Int16		nEnhanceLevel;
	public Int16		nRepairMax;

	public byte			byHoleNum;
	public byte			byQuality;

	public uint[]		dwHoleGemID;
	public tagRoleAttEffect[]  EquipBaseAtt;
	
	public tagEquipSpec()
	{
		EquipBaseAtt = new tagRoleAttEffect[3];
		for( int i = 0; i < 3; i++ )
		{
			EquipBaseAtt[i] = new tagRoleAttEffect();
		}
		dwHoleGemID	 = new uint[5];
	}
}



class tagEquip : tagItem 
{
	
	public tagEquipSpec		equipSpec = null;
	
	public tagEquip()
	{
		equipSpec = new tagEquipSpec ();
	}
}
