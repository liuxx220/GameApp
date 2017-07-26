using UnityEngine;
using System.Collections;
using Tanks.TankControllers;








namespace Tanks.Map
{
    /// <summary>
    /// 武器刷新点
    /// </summary>
    public class WeaponSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        protected Transform     m_SpawnPointTransform;
        [SerializeField]
        protected bool          m_IsRotator = true;
        [SerializeField]
        protected float         m_XAnimatingRotationSpeed = 20f;
        private float           m_YRotationDirection = 1;

        public GameObject       m_WeaponPerfab;

        private bool            m_IsDirty = false;


        /// <summary>
        /// Raises the trigger enter event - if the collider is a tank then increase the number of tanks in zone
        /// </summary>
        /// <param name="c">C.</param>
        private void OnTriggerEnter(Collider c)
        {
            if( c.gameObject != null )
            {
                TankShooting player = c.gameObject.GetComponent<TankShooting>();
                if( player != null )
                {
                    player.SetPlayerWeapon(0);
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Raises the trigger exit event - if the collider is a tank then decrease the number of tanks in zone
        /// </summary>
        /// <param name="c">C.</param>
        private void OnTriggerExit(Collider c)
        {

        }


        public void SetDirty()
        {
            m_IsDirty = true;
        }

        /// <summary>
        /// Resets/cleans up the spawn point
        /// </summary>
        public void Cleanup()
        {
            m_IsDirty = false;
        }

        private void SetYRotation(float y)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z));
        }

        private void Update()
        {
            if (!m_IsRotator)
                return;

            float newRotation = transform.rotation.eulerAngles.y - m_XAnimatingRotationSpeed * Time.deltaTime * m_YRotationDirection;
            SetYRotation(newRotation);
        }

    }
}