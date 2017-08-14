using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Tanks.Data;






namespace Tanks.Rules
{
    /// <summary>
    /// 武器的射击模式
    /// </summary>
    public enum SHOOTINGMODE
    {
        Shoot_continued,        // 连续射击
        Shoot_pressUp,          // 释放射击
        Shoot_pulse,            // 脉冲式发射
        Shoot_Rocket,           // 火箭弹
    };


    // 这个结构定义武器的基本属性，武器库
    [Serializable]
    public class TankWeaponDefinition
    {
        [SerializeField]
        public int m_ID;
        public int ID
        {
            get { return m_ID; }
        }

        [SerializeField]
        public string m_name;
        public string name
        {
            get { return m_name; }
        }

        [SerializeField]
        public string m_desc;
        public string desc
        {
            get { return m_desc; }
        }

        [SerializeField]
        public string m_icon;
        public string icon
        {
            get { return m_icon; }
        }
        [SerializeField]
        public string m_perfab;
        public string perfab
        {
            get { return m_perfab; }
        }
        [SerializeField]
        public SHOOTINGMODE m_ShootMode;
        public SHOOTINGMODE ShootMode
        {
            get { return m_ShootMode; }
        }
        [SerializeField]
        public int m_nMaxDamage;
        public int MaxDamage
        {
            get { return m_nMaxDamage; }
        }
        [SerializeField]
        public int m_nMinDamage;
        public int MinDamage
        {
            get { return m_nMinDamage; }
        }
        [SerializeField]
        public int m_ShootBulletNumPer; // 单词射击子弹数量
        public int ShootBulletNumPer
        {
            get { return ShootBulletNumPer; }
        }
		[SerializeField]
		public int m_nBulletClip;	//弹夹容量
		public int BulletClip
		{
			get { return m_nBulletClip; }
		}
		[SerializeField]
		public int m_nBullets;	//子弹数量
		public int Bullets
		{
			get { return m_nBullets; }
		}
    }

}