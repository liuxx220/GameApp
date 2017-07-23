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
    /// ���������ģʽ
    /// </summary>
    enum SHOOTINGMODE
    {
        Shoot_continued,        // �������
        Shoot_pressUp,          // �ͷ����
        Shoot_pulse,            // ����ʽ����
        Shoot_Rocket,           // �����
    };


	// ����ṹ���������Ļ������ԣ�������
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
