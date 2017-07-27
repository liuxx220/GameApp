using UnityEngine;
using System.Collections;
using System.Collections.Generic;








namespace Tanks.Rules
{
    /// <summary>
    /// Map list base - provides indexer implementation. It is a scriptable object
    /// </summary>
    public abstract class WeaponListBase<T> : ScriptableObject where T : TankWeaponDefinition
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