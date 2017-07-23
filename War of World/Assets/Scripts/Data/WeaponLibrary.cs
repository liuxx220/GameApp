using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tanks.Utilities;





///
/// weapon
namespace Tanks.Rules
{
    /// <summary>
    /// 武器的射击模式
    /// </summary>
    enum SHOOTINGMODE
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
        public string       weaponID;
        [SerializeField]
        public string       name;
        [SerializeField]
        public string       desc;
        [SerializeField]
        public string       icon;
        [SerializeField]
        public string       perfab;
        [SerializeField]
        public int          MaxDamage;
        [SerializeField]
        public int          MinDamage;
    }

    [CreateAssetMenu(fileName = "WeaponList", menuName = "Modes/Weapons/Create List", order = 1)]
    public class WeaponList : WeaponListBase<TankWeaponDefinition>
    {

    }
   
    /// <summary>
    /// Map list base - provides indexer implementation. It is a scriptable object
    /// </summary>
    public abstract class WeaponListBase<T> : ScriptableObject where T: TankWeaponDefinition
    {
        [SerializeField]
        private List<T> m_Weapons;

        /// <summary>
        /// Gets the <see cref="Tanks.Weapon.TankWeaponDefinition"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public T this[int index]
        {
            get { return m_Weapons[index]; }
        }

        /// <summary>
        /// Number of elements in the list
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return m_Weapons.Count; }
        }
    }
}
