using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;




class CItemMgr
{

	public static CItemMgr		Inst = null;
	public ItemContainer		m_pPocket;
	public ItemContainer		m_pEquipment;
	public ItemContainer		m_pStorge;

	public CItemMgr( )
	{
		Inst 			= this;
		m_pPocket 		= new ItemContainer (EItemConType.EICT_Bag, 30);
		m_pEquipment 	= new ItemContainer (EItemConType.EICT_Equip, 8 );
		m_pStorge 		= new ItemContainer (EItemConType.EICT_RoleWare, 30 );
	}
	

	public void  InitIteMgr( )
	{
        /*
		AddItem2Bag ( 3000000, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 3000001, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 3000002, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 3000003, 5, EItemQuality.EIQ_Blue, 0 );

		AddItem2Bag ( 1000000, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000001, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000002, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000003, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000004, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000005, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000006, 1, EItemQuality.EIQ_Blue, 0 );
		AddItem2Bag ( 1000007, 1, EItemQuality.EIQ_Blue, 0 );
        */
	}


	public CItem	GetItemByID( Int64 nSerial )
	{

		return null;
	}

	public CItem  GetPocketItem( Int16 iPos )
	{
		return m_pPocket.GetPos(iPos);;
	}

	public CItem  GetPocketItem( uint dwTypeID )
	{
		return m_pPocket.GetItemByTypeID(dwTypeID);
	}


	public CItem  GetPocketItemByID( Int64 n64ID )
	{
		return m_pPocket.GetIteBySerialID(n64ID);
	}


	public int  GetPocketItemNum( uint dwTypeID )
	{
		return m_pPocket.GetItemNumByTypeID(dwTypeID);
	}


	public CEquipment GetCurEquip( EEquipPos ePos )
	{
		return (CEquipment)m_pEquipment.GetPos( (Int16)ePos );
	}


	public CEquipment GetCurEquipByID( Int64 n64ID )
	{
		return (CEquipment)m_pEquipment.GetIteBySerialID( n64ID );
	}

	//----------------------------------------------------------------------
	int CalSpaceUsed( uint dwTypeID, Int16 nNum )
	{

		tagItemProto pProto = null;
		if (ItemCreator.MIsEquipment (dwTypeID)) 
		{
			tagEquipProto pEquipProto = null;
            CProtoManager.inst.m_mapEquip.TryGetValue(dwTypeID, out pEquipProto);
			pProto = (tagItemProto)pEquipProto;
		}
		else
		{
            CProtoManager.inst.m_mapItem.TryGetValue(dwTypeID, out pProto);
		}

		if (pProto == null) 
		{
			return -1;
		}

		int nUseSpace = 0;
		if (nNum < pProto.nMaxLapNum)
		{
			nUseSpace = 1;
		}
		else
		{
			if( pProto.nMaxLapNum <= 0 )
				pProto.nMaxLapNum = 1;
			nUseSpace = ( 1 == pProto.nMaxLapNum ? nNum : (nNum - 1) / pProto.nMaxLapNum );
		}

		return nUseSpace;
	}

	public bool AddItem2Bag( uint dwTypeID, Int16 nNum, EItemQuality eQ, int nCmd )
	{

		if (m_pPocket.IsFull ())
			return false;

		CItem pNewItem  = null;
		Int16 nBagSpace = (Int16)CalSpaceUsed( dwTypeID, nNum );

		// check bag free grid is enongh
		int nFreeSpace = m_pPocket.GetFreeSpaceSize ();
		if (nFreeSpace < nBagSpace - 1)
			return false;

		for (int i = 0; i < nBagSpace; i++)
		{
			tagItem pNew = ItemCreator.Create( dwTypeID, nNum, EItemQuality.EIQ_White, 0 );
			if( pNew != null )
			{
				if( ItemCreator.MIsEquipment(dwTypeID) )
					pNewItem = new CEquipment( (tagEquip) pNew );
				else
					pNewItem = new CItem( pNew );
			}


			if( pNewItem != null )
			{
				AddItem( m_pPocket, pNewItem, nNum );
			}
		}
	
		return true;
	}

	public int  RemoveFromRole( uint dwTypeID, int nNum, int nCmd )
	{
		if (nNum == 0) 
		{
			return RemoveItems( m_pPocket, dwTypeID, nCmd );
		}
		else
		{
			return RemoveItems( m_pPocket, dwTypeID, nCmd );
		}

	}

	public bool MoveItemBag2Equip( Int64 nSerID, Int16 dstPos )
	{
		return true;
	}
	

	public bool MoveItemEquip2Bag( Int64 nSerID, Int16 dstPos )
	{
		return true;
	}
	

	private int AddItem( ItemContainer pCon, CItem item, int nCmd )
	{
		if (item == null)
			return -1;

		pCon.Add ( item );

		return 0;
	}


	private int RemoveItem( ItemContainer pCon, Int64 nID, int nCmd )
	{
		CItem pItem = pCon.GetIteBySerialID (nID);
		if (pItem == null) 
		{
			Common.ERROR_MSG( " item not find " );
			return -1;
		}
		
		pCon.Remove (nID);
		return 0;
	}


	private int RemoveItems( ItemContainer pCon, uint dwTypeID, int nCmd )
	{
		if( pCon != null )
			pCon.Removes (dwTypeID);

		return 0;
	}

	private int RemoveItems( ItemContainer pCon, uint dwTypeID, int nNum, int nCmd )
	{

		if (nNum <= 0)
			return -1;

		int nCurNum = pCon.GetItemNumByTypeID (dwTypeID);
		if (nCurNum < nNum) 
		{
			return -2;
		}
		
		pCon.Removes (dwTypeID, nNum );

		return 0;
	}

	//----------------------------------------------------------------
	// equip or unequip
	void Equip( Int64 n64Serial, EEquipPos ePosDst )
	{
		
		
	}
	
	void UnEquip( Int64 n64Serial, Int16 nIndexDst )
	{
		
	}
}
