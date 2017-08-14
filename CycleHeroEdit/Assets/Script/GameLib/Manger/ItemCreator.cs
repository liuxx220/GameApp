using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;




class ItemCreator
{

	private static Int64 m_n64MaxSerial = 0;

	public static bool  MIsEquipment( uint dwTypeID )
	{
		if (dwTypeID >= 1000000 && dwTypeID <= 2999999)
			return true;

		return false;
	}

	public static tagItem	Create( uint dwTypeID, Int16 nNum, EItemQuality eQuality, int nCmd )
	{

		if (nNum <= 0)
			return null;

		tagItem pRet = null;
		if (MIsEquipment (dwTypeID)) 
		{
			pRet = new tagEquip();
			pRet.dwTypeID = dwTypeID;
		}
		else
		{
			pRet = new tagItem();
			pRet.dwTypeID = dwTypeID;
		}

		// 64 bit id
		Int64 nSerial = CreateItemSerial ();

		InitItem( pRet, nSerial, nNum );

		return pRet;
	}


	public static tagItem	Create( tagItem item, Int16 nNum )
	{
		if (nNum <= 0)
			return null;

		tagItem pNewItem = null;
		if (MIsEquipment ( item.dwTypeID )) 
		{
			pNewItem = new tagEquip();
			pNewItem = item;
		}
		else
		{
			pNewItem = new tagItem();
			pNewItem = item;
		}

		pNewItem.n64Serial = CreateItemSerial ();

		return pNewItem;
	}


	public static Int64 CreateItemSerial( )
	{
		return m_n64MaxSerial++;
	}


	public static void InitItem( tagItem item, Int64 n64Serial, Int16 nNum )
	{
		item.n64Serial 	= n64Serial;
		item.n16Num 	= nNum;
	}
}
