using UnityEngine;
using Tanks.TankControllers;
using Tanks.Utilities;








namespace Tanks.CameraControl
{
    public class CameraFollow : Singleton<CameraFollow>
    {
        private Transform m_PlayerTransform = null;
        protected Vector3 m_MoveVelocity;
        protected Vector3 m_Offset2Camera = Vector3.zero;

        public float distanceAway = 24;    //相机到目标的水平距离
        public float distanceUp = 32;      //相机到目标的垂直距离
        public float fixedRotationYaw = 45;//相机的固定朝向 


        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            float radian        = fixedRotationYaw * Mathf.Deg2Rad;
            m_Offset2Camera.x   = -(distanceAway * Mathf.Sin(radian));
            m_Offset2Camera.z   = -(distanceAway * Mathf.Cos(radian));
            m_Offset2Camera.y   = distanceUp;
            LazyLoadTankToFollow();
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新摄像机的位置
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (m_PlayerTransform == null)
            {
                LazyLoadTankToFollow();
            }

            if (m_PlayerTransform == null)
            {
                return;
            }
            Vector3 targetPosition = m_PlayerTransform.position + m_Offset2Camera;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_MoveVelocity, 0.2f, float.PositiveInfinity, Time.unscaledDeltaTime);
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 初始化需要的数据
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        private void LazyLoadTankToFollow()
        {
            var tanksList = GameManager.s_Tanks;
            for (int i = 0; i < tanksList.Count; i++)
            {
                TankManager tank = tanksList[i];
                if (tank != null)
                {
                    m_PlayerTransform = tank.transform;
                }
            }
        }
    }
}
