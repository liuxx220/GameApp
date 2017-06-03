using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;





/// <summary>
/// 战斗的技能界面
/// </summary>
public class CBattleFrame : GUIFrame
{
	
	public TweenColor 			    TweenColorBeHit;
	
    // 普通攻击按钮和技能按钮
    private UnityEngine.GameObject  m_NoralizeAttack;
    private UnityEngine.GameObject  m_btnSkill01;
    private UnityEngine.GameObject  m_btnSkill02;
    private UnityEngine.GameObject  m_btnSkill03;

    public  uint				    m_dwCurSkillID = 0;
	private uint				    m_dwNormSkillID= 0;
	private bool				    m_IsAttckNow = false;	// 是否处于连击状态

    private List<UnityEngine.GameObject> m_listSkill;
    private CHeroEntity             m_Hero = null;
	private CLocalPlayerFSM		    m_LocalFSM = null;
    private Dictionary<UnityEngine.GameObject, uint> m_map2ID;

    //--------------------------------------------------------------------------------
	// 初始化
	//--------------------------------------------------------------------------------
    public override bool ReloadUI()
    {
        base.ReloadUI();
        m_Hero = CFightTeamMgr.Instance.m_pBattleHero;
        m_LocalFSM = m_Hero.m_FSM;

        m_listSkill      = new List<UnityEngine.GameObject>();
        m_map2ID         = new Dictionary<UnityEngine.GameObject, uint>();
        m_NoralizeAttack = transform.FindChild("combat/Button_Attack").gameObject;
        m_btnSkill01     = transform.FindChild("combat/right ski 0").gameObject;
        m_btnSkill02     = transform.FindChild("combat/right ski 1").gameObject;
        m_btnSkill03     = transform.FindChild("combat/right ski 2").gameObject;


        UIEventListener.Get(m_NoralizeAttack).onClick = OnBtnNoralizeClick;
        UIEventListener.Get(m_btnSkill01).onClick = OnBtnSkill01Click;
        UIEventListener.Get(m_btnSkill02).onClick = OnBtnSkill01Click;
        UIEventListener.Get(m_btnSkill03).onClick = OnBtnSkill01Click;

        m_listSkill.Add(m_btnSkill01);
        m_listSkill.Add(m_btnSkill02);
        m_listSkill.Add(m_btnSkill03);
        m_dwCurSkillID = 0;
        InitSkillShortcut();
        return true;
    }

   
    //--------------------------------------------------------------------------------
    // 角色连击协程
    //--------------------------------------------------------------------------------
    private void SkillDoubleHitsAttack()
    {

        if (!IsInActCD())
        {
            m_LocalFSM.ChangeBeHavior(BehaviorType.EState_Skill, m_dwCurSkillID);

            // 播放攻击动作
            SetActCD(true);
        }
    }

    //--------------------------------------------------------------------------------
    // 普通技能按钮单击事件
    //--------------------------------------------------------------------------------
    private void OnBtnNoralizeClick(UnityEngine.GameObject go)
    {
        if (m_LocalFSM == null)
            return;

        m_LocalFSM.ChangeBeHavior(BehaviorType.EState_Skill, m_dwNormSkillID);
    }


    //--------------------------------------------------------------------------------
    // 技能1按钮单击事件
    //--------------------------------------------------------------------------------
    private void OnBtnSkill01Click(UnityEngine.GameObject go)
    {
        uint dwID = 0;
        m_map2ID.TryGetValue( go, out dwID);
        if (dwID <= 0)
            return;

        // 如果按钮在CD状态，返回，否则开始CD
        if (go.transform.FindChild("cd").GetComponent<UISprite>().fillAmount != 0)
            return;

        go.transform.FindChild("cd").GetComponent<UISprite>().fillAmount = 1;
        if (!IsInActCD() || m_LocalFSM.IsInIdle() || m_LocalFSM.IsInMoveing())
        {
            // 直接播放技能
            m_dwCurSkillID = 0;
            m_LocalFSM.ChangeBeHavior(BehaviorType.EState_Skill, dwID);
            SetActCD(true);
        }
        else
        {
            // 连其他技能， 不过需要知道要连接的技能ID, 一般是自己连自己
            m_dwCurSkillID = dwID;
            SkillDoubleHitsAttack();
        }
    }

    //--------------------------------------------------------------------------------
    // 根据英雄身上的技能，初始化技能快捷栏
    //--------------------------------------------------------------------------------
    private void InitSkillShortcut()
    {
        if (m_Hero == null)
            return;

        int i = 0;
        foreach (var item in m_Hero.m_mapSkill)
        {
            CSkill pEntity = item.Value;
            if (pEntity == null)
                continue;

            tagSkillProto pProto = pEntity.GetProto();
            if (pProto == null)
                continue;

            if (pProto.eType == ESkillType.ESUT_Norm)
            {
                Transform bg = m_NoralizeAttack.transform.FindChild("Background");
                bg.GetComponent<UISprite>().spriteName = pProto.strIcon;
                m_dwNormSkillID = pEntity.GetID();
                m_map2ID.Add(m_NoralizeAttack, pEntity.GetID());
                continue;
            }

            else
            {
                UnityEngine.GameObject ctrl = m_listSkill[i];
                Transform bg = ctrl.transform.FindChild("Background");
                bg.GetComponent<UISprite>().spriteName = pProto.strIcon;
                m_map2ID.Add(ctrl, pEntity.GetID());
            }
            i++;
        }
    }

    //--------------------------------------------------------------------------------
    // 技能1按钮单击事件
    //--------------------------------------------------------------------------------
    private bool IsInActCD()
    {
        return CFightTeamMgr.Instance.m_bActIsInCD;
    }

    //--------------------------------------------------------------------------------
    // 技能1按钮单击事件
    //--------------------------------------------------------------------------------
    private void SetActCD(bool bCD)
    {
        CFightTeamMgr.Instance.m_bActIsInCD = bCD;
    }

    //--------------------------------------------------------------------------------
    // 开始普通攻击或普通连击
    //--------------------------------------------------------------------------------
    public void doAttack()
    {
        if (IsInActCD())
            return;

        SetActCD(true);

        if (m_LocalFSM.IsInIdle())
        {
            m_LocalFSM.ChangeBeHavior(BehaviorType.EState_Skill, m_dwNormSkillID);
        }
        else
        {
            m_LocalFSM.ChangeBeHavior(BehaviorType.EState_Skill, m_dwNormSkillID);
        }
    }
}
