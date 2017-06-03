using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;






public class CBagFrame : GUIFrame
{
	
	// attr's of hero show
    private int m_row       = 6;
    private int m_col       = 5;

    // bag 's item
    UnityEngine.GameObject[] m_Items;
    public override bool ReloadUI()
    {
        base.ReloadUI();
        m_Items             = new GameObject[m_row * m_col];

        // init item ctrl
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_col; j++)
            {
                string strName = "bagitem" + i.ToString() + (j + 1).ToString();
                int idx = i * m_col + j;
                m_Items[idx] = UnityEngine.GameObject.Find(strName);
            }
        }

        // regsit oncheck event
        for (int i = 0; i < m_row * m_col; i++)
        {
            UIEventListener.Get(m_Items[i]).onClick = onItemClick;
        }
        return true;
	}

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_col; j++)
            {
                int idx      = i * m_col + j;
                m_Items[idx] = null;
            }
        }
        base.Destroy();
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 单击某个道具
    /// </summary>
    /// --------------------------------------------------------------------------
    void onItemClick(UnityEngine.GameObject item)
    {
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_col; j++)
            {

                int idx = i * m_col + j;
                if (item == m_Items[idx])
                {

                    CItem pItem = CItemMgr.Inst.m_pPocket.GetPos((Int16)idx);
                    if (pItem == null)
                        return;

                    //if (ItemCreator.MIsEquipment(pItem.GetItemTypeID()))
                    //{
                    //    object[] _param = new object[3];
                    //    _param[0] = idx;
                    //    _param[1] = 1;
                    //    _param[2] = true;

                    //    if (mShowEquip == null)
                    //        return;

                    //    mCharacterEquip.gameObject.SetActive(false);
                    //    mShowEquip.gameObject.SetActive(true);
                    //    mShowEquip.SendMessage("OnOpenEquipShow", _param, SendMessageOptions.DontRequireReceiver);
                    //}
                }
            }
        }
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 刷新背包内的数据
    /// </summary>
    /// --------------------------------------------------------------------------
    void RefushBagItem()
    {

        
    }
}
