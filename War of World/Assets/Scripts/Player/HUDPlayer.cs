using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using Tanks.TankControllers;
using Tanks.Extensions;







namespace Tanks.Data
{
	/// <summary>
	/// ��ɫѪ��
	/// </summary>
    public class HUDPlayer : MonoBehaviour
    {
        public    float             m_TestNeatorFar = 1f;
       
        /// <summary>
        /// Ѫ�����ֵĿؼ�
        /// </summary>
        protected Transform         m_Pivot = null;
        protected UISprite          m_hideSprite = null; 
        protected UILabel           m_hpLable    = null;
        protected UISprite          m_hpSprite   = null;
        protected Camera            m_mainCamera = null;
        protected Camera            m_uiCamera   = null;
        protected Transform         m_hideSlider = null;

        /// <summary>
        /// ðѪ����
        /// </summary>
        protected class Entry
        {
            public float        time;			// Timestamp of when this entry was added
            public float        stay = 0f;		// How long the text will appear to stay stationary on the screen
            public float        offset = 0f;	// How far the object has moved based on time
            public float        val = 0f;		// Optional value (used for damage)
            public UILabel      label;		    // Label on the game object
            public float movementStart { get { return time + stay; } }
        }

        static int Comparison(Entry a, Entry b)
        {
            if (a.movementStart < b.movementStart) return -1;
            if (a.movementStart > b.movementStart) return 1;
            return 0;
        }

        int             counter     = 0;
        List<Entry>     mList       = new List<Entry>();
        List<Entry>     mUnused     = new List<Entry>();
        Keyframe[]      mOffsets;
        Keyframe[]      mAlphas;

        /// <summary>
        /// Font used by the labels.
        /// </summary>
        public UIFont       bitmapFont;

        /// <summary>
        /// Size of the font to use for the popup list's labels.
        /// </summary>
        public int          fontSize = 20;

        /// <summary>
        /// Font style used by the dynamic font.
        /// </summary>
        public FontStyle    fontStyle = FontStyle.Normal;

        /// <summary>
        /// Curve used to move entries with time.
        /// </summary>

        public AnimationCurve offsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 40f) });

        /// <summary>
        /// Curve used to fade out entries with time.
        /// </summary>

        public AnimationCurve alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });



        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Ѫ�������߼�
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        void Awake()
        {
            m_hpSprite    = transform.FindChild("Progress HP/Foreground").GetComponent<UISprite>();
            //m_hpLable     = transform.FindChild("Name").GetComponent<UILabel>();
            m_hideSlider  = transform.transform.FindChild("Progress HP/ProgressHide");
            m_hideSprite  = m_hideSlider.transform.FindChild("HideBg").GetComponent<UISprite>();
            m_hideSprite.fillAmount = 0;
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ��ʼ��Ѫ����Ҫ���������ӵ���߶���
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        public void Init( Transform form )
        {
            m_mainCamera = Camera.main;
            m_uiCamera   = HUDPlayerManager.Get().GetUICamera();
            Transform o  = form.FindChild("Pivot");
            m_Pivot      = o;
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Ѫ�������߼�
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        void Update()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) return;
            #endif

            if (m_Pivot != null && m_mainCamera != null && m_uiCamera != null)
            {
                Vector3 pos          = m_Pivot.position;
                // Ѫ����Ӧ����Ļ��λ��
                Vector2 pos_screen   = m_mainCamera.WorldToScreenPoint(pos);
                // Ѫ����Ӧ��ui�е�λ��
                Vector3 pos_ui       = m_uiCamera.ScreenToWorldPoint(pos_screen);
                transform.position   = Vector3.Slerp(transform.position, pos_ui, Time.time);

                transform.localScale = Vector3.one * m_TestNeatorFar;


                float time          = RealTime.time;
                if (mOffsets == null)
                {
                    mOffsets        = offsetCurve.keys;
                    mAlphas         = alphaCurve.keys;
                }

                float offsetEnd     = mOffsets[mOffsets.Length - 1].time;
                float alphaEnd      = mAlphas[mAlphas.Length - 1].time;
                float totalEnd      = Mathf.Max(offsetEnd, alphaEnd);
                for (int i = mList.Count; i > 0; )
                {
                    Entry ent       = mList[--i];
                    float currentTime = time - ent.movementStart;
                    ent.offset      = offsetCurve.Evaluate(currentTime);
                    ent.label.alpha = alphaCurve.Evaluate(currentTime);

                    if (currentTime > totalEnd)
                        Delete(ent);
                    else 
                       ent.label.enabled = true;
                }

                float offset = 0f;
                for (int i = mList.Count; i > 0; )
                {
                    Entry ent       = mList[--i];
                    offset          = Mathf.Max(offset, ent.offset);
                    ent.label.cachedTransform.localPosition = new Vector3(0f, offset, 0f);
                    offset          += Mathf.Round(ent.label.cachedTransform.localScale.y * ent.label.fontSize);
                }
            }
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ����Ѫ���Ľ���
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        public void SetProcess( float fPress )
        {
            m_hpSprite.fillAmount = fPress;
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Ѫ������
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        IEnumerator SlowDown()
        {
            if (m_hideSlider != null && gameObject.activeInHierarchy)
            {
                while (true)
                {
                    yield return new WaitForSeconds(0.05f);
                    m_hideSprite.fillAmount -= 0.05f;
                    if (m_hideSprite.fillAmount <= 0)
                    {
                        StopCoroutine("slowDown");
                        break;
                    }
                }
            }
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// �����˺�
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        public void XueTiaoDmgShow(int currentHp, int dmg, int maxHp)
        {
            if (dmg <= 0)
                return;

            float value = currentHp / maxHp;
            if (m_hideSlider != null && m_hideSprite != null && dmg > 0)
            {
                Vector3 currentPos = new Vector3(m_hpSprite.width * value, m_hideSlider.transform.localPosition.y, m_hideSlider.transform.localPosition.z);
                if ((m_hpSprite.transform.localScale.x * (dmg / maxHp)) != 0)
                {
                    m_hideSlider.transform.localPosition = currentPos;
                    m_hideSprite.SetDimensions((int)(m_hpSprite.width * (dmg / maxHp)), m_hideSprite.height);
                    m_hideSprite.fillAmount = 1;
                    if (gameObject.activeInHierarchy)
                    {
                        StartCoroutine("SlowDown");
                    }
                    else
                    {
                        m_hideSprite.fillAmount = 0f;
                        StopCoroutine("slowDown");
                    }
                }
            }
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ����Ѫ���ĸ߶�
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        protected virtual float GetXueTiaoHeight()
        {
            return 7.0f;
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// ����ðѪ����
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        private Entry CreateHUDPlayerPrefab( )
        {
            if (mUnused.Count > 0)
            {
                Entry ent       = mUnused[mUnused.Count - 1];
                mUnused.RemoveAt(mUnused.Count - 1);
                ent.time        = Time.realtimeSinceStartup;
                ent.label.depth = NGUITools.CalculateNextDepth(gameObject);
                NGUITools.SetActive(ent.label.gameObject, true);
                ent.offset      = 0f;
                mList.Add(ent);
                return ent;
            }

            // New entry
            Entry ne            = new Entry();
            ne.time             = Time.realtimeSinceStartup;
            ne.label            = NGUITools.AddWidget<UILabel>(gameObject);
            ne.label.name       = counter.ToString();
            ne.label.ambigiousFont = bitmapFont;
            ne.label.fontSize   = fontSize;
            ne.label.fontStyle  = fontStyle;
            ne.label.overflowMethod = UILabel.Overflow.ResizeFreely;

            // Make it small so that it's invisible to start with
            mList.Add(ne);
            ++counter;
            return ne;
        }


        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// �����˺�����
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void AddHUD(object obj, Color c, float stayDuration )
        {
            bool isNumeric   = false;
            int valuse       = 0;
            if (obj is int)
            {
                valuse       = (int)obj;
                isNumeric    = true;
            }

            if (valuse == 0)
                return;

            // Create a new entry
            Entry ne        = CreateHUDPlayerPrefab( );
            ne.stay         = stayDuration;
            ne.label.color  = c;
            ne.label.alpha  = 1f;
            ne.val          = valuse;
            if (isNumeric)
                ne.label.text = valuse < 0f ? valuse.ToString() : "+" + valuse.ToString();
            else
                ne.label.text = obj.ToString();

            mList.Sort(Comparison);
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// ��Դ����
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        void Delete(Entry ent)
        {
            mList.Remove(ent);
            mUnused.Add(ent);
            NGUITools.SetActive(ent.label.gameObject, false);
        }
    }
}