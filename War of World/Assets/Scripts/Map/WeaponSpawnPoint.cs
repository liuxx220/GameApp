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

        public GameObject       m_PerviewObj;
        private int             m_WeaponID = 0;
        private bool            m_IsDirty = false;


        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 进入武器刷新点
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        private void OnTriggerEnter(Collider c)
        {
            if( c.gameObject != null && !IsDirty() )
            {
                TankShooting player = c.gameObject.GetComponent<TankShooting>();
                if( player != null )
                {
                    player.SetPlayerWeapon(m_WeaponID);
                    SetDirty();
                    SpawnManager.s_Instance.DestoryWeapon(m_PerviewObj);
                    m_PerviewObj = null;
                }
            }
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 离开武器刷新点
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        private void OnTriggerExit(Collider c)
        {

        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 设置脏标记
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        public void SetDirty()
        {
            m_IsDirty = true;
        }


        public bool IsDirty()
        {
            return m_IsDirty;
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Resets/cleans up the spawn point
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------   
        public void Cleanup()
        {
            m_IsDirty = false;
            m_WeaponID= 0;
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 绕Y轴旋转
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------   
        private void SetYRotation(float y)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 心跳
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------   
        private void Update()
        {
            if (!m_IsRotator)
                return;

            float newRotation = transform.rotation.eulerAngles.y - m_XAnimatingRotationSpeed * Time.deltaTime * m_YRotationDirection;
            SetYRotation(newRotation);
        }

    }
}