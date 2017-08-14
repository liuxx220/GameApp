using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;





class CItem
{

	public tagItemProto     m_pProto = null;
	public tagItem			m_data = null;

	public CItem()	
	{

	}

	public CItem( tagItem data )
	{
		m_data = data;
        CProtoManager.inst.m_mapItem.TryGetValue(m_data.dwTypeID, out m_pProto);
		if (m_pProto == null ) 
		{
			Common.DEBUG_MSG("tagIteProto's proto not find " + m_data.dwTypeID );
		}
	}

	public virtual bool IsItem()
	{
		return true;
	}

	public tagItem GetData()
	{
		return m_data;
	}

	public tagItemProto GetProto()
	{
		return m_pProto;
	}

	public EItemConType GetConType()
	{
		return m_data.eConType;
	}

	public Int16	GetPos()
	{
		return m_data.n16Index;
	}

	public Int64	GetItemID()
	{
		return m_data.n64Serial;
	}

	public uint	GetItemTypeID()
	{
		return m_data.dwTypeID;
	}

	public EItemType GetItemType()
	{
		return m_pProto.eType;
	}

	public Int16	GetItemNum()
	{
		return m_data.n16Num;
	}

	public EItemSpecFunc GetItemSpecFunc()
	{
		return m_pProto.eSpecFunc;
	}

	public int GetItemPrice()
	{
		return m_pProto.nBasePrice;
	}

	public int GetItemLevel()
	{
		return m_pProto.byLevel;
	}

	public void SetItemID( Int64 n64ID )
	{
		m_data.n64Serial = n64ID;
	}

	public void SetConType( EItemConType eType )
	{
		m_data.eConType = eType;
	}

	public void SetPos( Int16 n16Pos )
	{
		m_data.n16Index = n16Pos;
	}

	public void SetItemNum( Int16 nNum )
	{
		m_data.n16Num = nNum;
	}
}

class CEquipment : CItem 
{
	public tagEquipProto    m_pEquipProto = null;
	public tagEquipSpec		m_equipex = null;

	public CEquipment()
	{
		m_equipex = new tagEquipSpec ();
	}

	public CEquipment( tagEquip data )
	{

		m_equipex = new tagEquipSpec ();

		m_data = data;
        CProtoManager.inst.m_mapEquip.TryGetValue(m_data.dwTypeID, out m_pEquipProto);
		if (m_pProto == null ) 
		{
			Common.DEBUG_MSG("tagEquipProto's proto not find " + m_data.dwTypeID );
		}

		for (int i = 0; i < 3; i++) 
		{
			m_equipex.EquipBaseAtt[i].eRoleAtt = m_pEquipProto.BaseEffect[i].eRoleAtt;
			m_equipex.EquipBaseAtt[i].nValue   = m_pEquipProto.BaseEffect[i].nValue;
		}

		m_pProto = m_pEquipProto;
	}


	public void UpdateEquipMent( tagEquipSpec data )
	{
		m_equipex = data;
	}


	public override bool IsItem()
	{
		return false;
	}

	public EEquipPos GetEquipPos()
	{
		return m_pEquipProto.eEquipPos;
	}

	public byte GetEquipHoleNum()
	{
		return m_equipex.byHoleNum;
	}

	public void AddEquipHoleNum()
	{
		m_equipex.byHoleNum += 1;
	}

	public byte GetItemQuality()
	{
		return m_equipex.byQuality;
	}

	public uint GetEquipHoldGemID( int indx )
	{
		return m_equipex.dwHoleGemID[indx];
	}

	public Int16 GetEquipEnhanceLevel()
	{
		return m_equipex.nEnhanceLevel;
	}

	public void SetEquipEnhanceLevel( Int16 nL )
	{
		m_equipex.nEnhanceLevel = nL;
	}

	public tagRoleAttEffect GetEquipBaseAtt( int i )
	{
		return m_equipex.EquipBaseAtt [i];
	}
}
