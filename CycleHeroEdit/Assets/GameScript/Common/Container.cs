using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;





class ItemContainer
{

	EItemConType	m_eType;
	List<CItem>		m_Container;
	List<CItem>  	m_samelist = new List<CItem>();
	int				m_nMaxSize;
	int				m_n16RemainSize;



	public ItemContainer( EItemConType eType, int nMaxSize )
	{
		m_eType 	= eType;
		m_nMaxSize 	= nMaxSize;
		m_Container = new List<CItem>();
		m_n16RemainSize = nMaxSize;

		while (m_Container.Count < nMaxSize) 
			m_Container.Add (null);
	}

	private void Add(CItem pItem, Int16 ipos )
	{

		if (!IsOnePlaceFree (ipos) )
			return;

		if (pItem.GetItemNum () <= 0)
			return;

		m_n16RemainSize--;
		m_Container [ipos] = pItem;
		pItem.SetPos (ipos);
	}

	private void Remove( Int16 iPos )
	{

		if (iPos < 0 || iPos >= m_Container.Count)
			return;

		m_Container [iPos] = null;
	}

	public bool Add( CItem pItem )
	{
		Int16 n16CanLap  = 0;
		Int16 n16HaveAdd = 0;
		if (m_eType == EItemConType.EICT_Bag || m_eType == EItemConType.EICT_RoleWare) 
		{
			if( pItem.m_pProto.nMaxLapNum > 1 )
			{

				m_samelist.Clear();
				GetSameItemList( pItem.GetItemTypeID() );


				for (int i = 0; i < m_samelist.Count; i++) 
				{
					CItem pConItem = m_samelist[i];
					n16CanLap  = (Int16)(pConItem.m_pProto.nMaxLapNum - pConItem.GetItemNum());
					if( n16CanLap > 0 )
					{
						n16HaveAdd	 = (Int16)(n16HaveAdd + n16CanLap);	
						if( n16HaveAdd > pItem.GetItemNum() )
						{
							pConItem.m_data.n16Num += pItem.m_data.n16Num;
							n16HaveAdd = pItem.GetItemNum();
							break;
						}
						else
						{
							pConItem.m_data.n16Num +=  n16CanLap;
						}
					}
				}	
			}

			int NotHaveAdd = pItem.GetItemNum() - n16HaveAdd;
			if( NotHaveAdd > 0 )
			{
				Int16 iPos = GetFreeGrid();
				Add( pItem, iPos );
			}
		}
		else
		{
			if( pItem.m_pProto.nMaxLapNum > 1 )
			{
				m_samelist.Clear();
				GetSameItemList( pItem.GetItemTypeID() );
				for (int i = 0; i < m_samelist.Count; i++) 
				{
					CItem pConItem = m_samelist[i];
					n16CanLap  = (Int16)(pConItem.m_pProto.nMaxLapNum - pConItem.GetItemNum());
					if( n16CanLap >= pItem.GetItemNum() )
					{
						pConItem.m_data.n16Num += pItem.m_data.n16Num;
						n16HaveAdd = pItem.GetItemNum();
					}
				}	
			}

			if( 0 == n16HaveAdd )
			{
				Int16 ipos = GetFreeGrid();
				Add( pItem, ipos );
			}
		}
		return true;
	}

	public CItem Remove( Int64 n64Serial )
	{

		CItem pItem = GetIteBySerialID(n64Serial);
		if (pItem != null) 
		{
			m_n16RemainSize++;
			Remove( pItem.GetPos() );
		}

		return pItem;
	}

	public void Removes( uint dwTypeID, int nNum )
	{

		m_samelist.Clear();
		GetSameItemList( dwTypeID );

		for (int i = 0; i < m_samelist.Count; i++) 
		{
			CItem pItem = m_samelist[i];
			if( pItem == null && pItem.GetItemTypeID() == dwTypeID )
			{
				if( pItem.GetItemNum() < nNum )
				{
					nNum  -= pItem.GetItemNum();
					Remove( pItem.GetPos() );
				}
				else if( pItem.GetItemNum() == nNum )
				{
					nNum = 0;
					Remove( pItem.GetPos() );
					break;
				}
			}
		}
	}

	public void Removes( uint dwTypeID )
	{
		
		for (int i = 0; i < m_Container.Count; i++) 
		{
			CItem pItem = m_Container[i];
			if( pItem != null && pItem.GetItemTypeID() == dwTypeID )
				Remove( pItem.GetPos() );
		}
	}


	public CItem GetPos( Int16 iPos )
	{
		if (iPos >= 0 && iPos < m_Container.Count) 
		{
			return m_Container[iPos];
		}
		return null;
	}


	public int	GetItemNumByTypeID ( uint dwTypeID )
	{
		int nRet = 0;
		for (int i = 0; i < m_Container.Count; i++) 
		{
			CItem pItem = m_Container[i];
			if( pItem != null && pItem.GetItemTypeID() == dwTypeID )
				nRet += pItem.GetItemNum();
		}
		return nRet;
	}

	public CItem GetItemByTypeID( uint dwTypeID )
	{

		for (int i = 0; i < m_Container.Count; i++) 
		{
			CItem pItem = m_Container[i];
			if( pItem != null && pItem.GetItemTypeID() == dwTypeID )
				return pItem;
		}

		return null;
	}


	public CItem GetIteBySerialID( Int64 nSerialID )
	{

		for (int i = 0; i < m_Container.Count; i++) 
		{
			CItem pItem = m_Container[i];
			if( pItem != null && pItem.GetItemID() == nSerialID )
				return pItem;
		}

		return null;
	}

	public bool IsFull( )
	{
		if (GetFreeGrid () == -1)
			return true;

		return false;
	}

	public Int16 GetFreeGrid( )
	{
		for (int i = 0; i < m_Container.Count; i++) 
		{
			CItem pItem = m_Container[i];
			if( pItem == null )
				return (Int16)i;
		}
		return -1;
	}

	public void GetSameItemList( uint dwTypeID )
	{
		for (int i = 0; i < m_Container.Count; i++) 
		{
			CItem pItem = m_Container[i];
			if( pItem != null && pItem.GetItemTypeID() == dwTypeID )
				m_samelist.Add( pItem );
		}
	}

	public bool IsOnePlaceFree( Int16 iPos )
	{
		if (iPos < 0 || iPos >= m_Container.Count) 
			return false;
			
		return m_Container[iPos] == null;
	}

	public int GetFreeSpaceSize()
	{
		return m_n16RemainSize;
	}

	public void Equip( CItem pItem, Int16 iPos )
	{

		if (iPos >= 0 && iPos < this.m_nMaxSize) 
		{
			if( m_Container[iPos] != null )
			{
				Remove( iPos );
				Add( pItem, iPos );
			}
			else
			{
				Add( pItem, iPos );
			}
		}
	}

	public void UnEquip( Int16 iPos )
	{
		if (iPos >= 0 && iPos < this.m_nMaxSize) 
		{
			Remove( iPos );
		}
	}
}
