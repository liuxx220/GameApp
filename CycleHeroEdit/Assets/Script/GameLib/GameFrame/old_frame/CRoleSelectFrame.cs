using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;



public class CRoleSelectFrame : GUIFrame 
{

    /// <summary>
    /// 手势移动方向
    /// </summary>
    public enum FIMOVEDIR
    {
        FMD_Left = 0,
        FMD_Stop,
        FMD_Right
    }


    private List<GameObject>    m_Players = new List<GameObject>();
    private RenderTexture       m_RenderTarget = null;
	UnityEngine.GameObject		m_StartGame;
    private UILabel             m_ClassName;
    private UILabel             m_ClassInfo;
    private UITexture           m_bg = null;

	string[] 	m_strNameAry	= { "战士", "法师", "弓箭手", "召唤师", "猎人", "原力者", "异能者" };
	string[]    m_strInfoAry    = { "使用原始力量为主的武者，其拥有的强大的作战能力和顽强的生命力。",
								    "精通操作各类机械武器，中远距离攻击型职业。",
								    "以使用脑波力量（精神力）的能量操控者。",
								    "以继承暗杀术，迅捷灵巧的身手是其他职业无法拥有的优势。",
								    "以继承古武术流，使用强化肌体的原始力量为主的武者。",
		                            "以继承古武术流，使用强化肌体的原始力量为主的武者。",
								    "以继承古武术流，使用强化肌体的原始力量为主的武者。" };
    
    /// <summary>
    /// 滑动英雄的操作参数
    /// </summary>
    private int m_PlayerCount       = 7;
    private int m_CurrentIndex      = 0;
    private float MARGIN_X          = 3.0f;
    private float ITEM_WITH         = 8.0f;
    private FIMOVEDIR _touchMoveDir = FIMOVEDIR.FMD_Stop;
    private float _touchdistance    = 0f;
    private int sliderValue         = 4;


    public override bool ReloadUI()
    {
        base.ReloadUI();

        m_RenderTarget  = new RenderTexture(1024, 512, 24 );
        m_StartGame     = transform.Find("Anchor/heromiaoshu/startgame").gameObject;
        m_ClassName     = transform.Find("Anchor/heromiaoshu/bg4/classname").GetComponent<UILabel>();
        m_ClassInfo     = transform.Find("Anchor/heromiaoshu/bg3/classinfo").GetComponent<UILabel>();
        m_bg            = transform.Find ("Anchor/heromiaoshu/bg2" ).GetComponent<UITexture>();
       
      
		UIEventListener.Get( m_StartGame ).onClick = OnStartGameEvent;

        /// 初始化相机的数据
        Camera myCamera = UnityEngine.GameObject.Find("MainCamera").GetComponent<Camera>();
        if (myCamera != null)
        {
            myCamera.targetTexture = m_RenderTarget;
            m_bg.mainTexture = m_RenderTarget;
            InitModel();
        }
        return true;
	}

    // Update is called once per frame
    public override void Update()
    {
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (Input.GetTouch(0).deltaPosition.x < 0 - Mathf.Epsilon)
                {
                    _touchMoveDir = FIMOVEDIR.FMD_Left;
                }
                else
                {
                    _touchMoveDir = FIMOVEDIR.FMD_Right;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                _touchMoveDir = FIMOVEDIR.FMD_Stop;
            }
        }

        if (_touchMoveDir != FIMOVEDIR.FMD_Stop)
        {
            if (_touchMoveDir == FIMOVEDIR.FMD_Left)
            {
                _touchdistance += Time.deltaTime;
                if (_touchdistance > 0.5f)
                {
                    sliderValue++;
                    if (sliderValue >= m_PlayerCount)
                    {
                        sliderValue = m_PlayerCount - 1;
                        return;
                    }
                    moveSlider(sliderValue);
                }
            }
            else
            {
                _touchdistance += Time.deltaTime;
                if (_touchdistance > 0.5f)
                {
                    sliderValue--;
                    if (sliderValue < 0)
                    {
                        sliderValue = 0;
                        return;
                    }
                    moveSlider((int)sliderValue);
                }
            }
        }
    }


    private void InitModel()
    {
        UnityEngine.Object obj       = Resources.Load("UI/NullObject");
        UnityEngine.GameObject Clone = UnityEngine.GameObject.Instantiate( obj ) as GameObject;
        if (Clone != null)
        {
            for (int i = 0; i < m_PlayerCount; i++)
            {
                Texture2D tex = Resources.Load("PlayerCard/player" + i.ToString(), typeof(Texture2D)) as Texture2D;
                m_Players.Add(GeneratePlayer(tex, i, Clone.transform));
            }
        }

        obj = null;
        moveSlider( m_Players.Count / 2 );
    }

    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {
        for (int i = 0; i < m_Players.Count; i++ )
        {
            GameObject.DestroyImmediate(m_Players[i]);
        }
        m_Players.Clear();
        m_RenderTarget  = null;
        m_StartGame     = null;
        m_ClassName     = null;
        m_ClassInfo     = null;
        base.Destroy();
    }
	
	
	void OnStartGameEvent( UnityEngine.GameObject item )
	{
        if (CFightTeamMgr.Instance.m_pBattleHero != null)
        {
            GameUIManager.Instance().DestoryFrame(GUIDefine.UIF_SELECTHEROFRAME);
            //LoadLevelMgr.Instance.ShowLoadLevel("City");
            GameUIManager.Instance().ShowFrame(GUIDefine.UIF_CITYMAINFRAME);
        }
	}


	void UpdateHeroProtoInfo( int index )
	{
		if( index >= 0 && index < 7 )
		{
			if( m_ClassName != null )
				m_ClassName.text = m_strNameAry[index];

			if( m_ClassInfo != null )
				m_ClassInfo.text = m_strInfoAry[index];
		}
	}


    /// --------------------------------------------------------------------------
    /// <summary>
    /// 滑动英雄
    /// </summary>
    /// --------------------------------------------------------------------------
    private void moveSlider( int id )
    {
        if (m_CurrentIndex == id)
            return;

        m_CurrentIndex = id;
        for (int i = 0; i < m_PlayerCount; i++)
        {
            float targetX = 0f;
            float targetZ = 0f;
            float targetRot = 0f;

            targetX = MARGIN_X * (i - id);
            //left slides
            if (i < id)
            {
                targetX -= ITEM_WITH * 0.6f;
                targetZ = ITEM_WITH * 3f / 4;
                targetRot = -60f;

            }
            //right slides
            else if (i > id)
            {
                targetX += ITEM_WITH * 0.6f;
                targetZ = ITEM_WITH * 3f / 4;
                targetRot = 60f;
            }
            else
            {
                targetX += 0f;
                targetZ = 0f;
                targetRot = 0f;
            }

            GameObject pPlayer = m_Players[i];
            float ys    = pPlayer.transform.position.y;
            Vector3 ea  = pPlayer.transform.eulerAngles;

            iTween.MoveTo(pPlayer, new Vector3(targetX, ys, targetZ), 1f);
            iTween.RotateTo(pPlayer, new Vector3(ea.x, targetRot, targetZ), 1f);
            //iTween.ScaleTo( pPlayer, new Vector3(), 1f ); 
        }

        CFightTeamMgr.Instance.SelectFristHero();
    }

    /// --------------------------------------------------------------------------
    /// <summary>
    /// 自动创建可以滑动选择的英雄
    /// </summary>
    /// --------------------------------------------------------------------------
    private GameObject GeneratePlayer( Texture2D tex, int id, Transform parent )
    {
        GameObject photoObj = new GameObject();
        photoObj.name       = "PlayerObj" + id.ToString();
        GameObject pPlayer  = GameObject.CreatePrimitive(PrimitiveType.Plane);
        pPlayer.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");

        pPlayer.name = "Player";
        pPlayer.GetComponent<Renderer>().material.mainTexture = tex;

        pPlayer.transform.localScale    = new Vector3(0.5f, 2.0f, -1f);
        pPlayer.transform.parent        = photoObj.transform;
        photoObj.transform.eulerAngles  = new Vector3(-90f, 0f, 0f);
        photoObj.transform.parent       = parent;

        return photoObj;
    }
}
 