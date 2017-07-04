using UnityEngine;
using Tanks.TankControllers;
using Tanks.Utilities;

namespace Tanks.CameraControl
{
    public class CameraFirstPerson : Singleton<CameraFirstPerson>
	{

		private Transform               m_TankToFollowTransform = null;
		private TankMovement            m_TankToFollowMovement = null;
        private CharacterController     m_CharacterController;

        public float sensitivityX       = 0;
        public float sensitivityY       = 0;

        public float mMinimumX          = -5;                      // 向下望的最大角度  
        public float mMaximumX          = +8;                       // 向上望的最大角度  
        public float mMinimumY          = -10;                     // 向左望的最大角度  
        public float mMaximumY          = +10;                      // 向右望的最大角度  


		private void Start()
		{
            LazyLoadTankToFollow();
            m_CharacterController = GetComponent<CharacterController>();
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().freezeRotation = true;
		}

		// Update is called once per frame
		private void Update()
		{
            if (m_TankToFollowTransform == null || m_TankToFollowMovement == null)
            {
                return;
            }

            sensitivityY = m_TankToFollowTransform.rotation.eulerAngles.y;
            sensitivityY = Mathf.Clamp(sensitivityY, mMinimumY, mMaximumY);
            sensitivityX = m_TankToFollowTransform.rotation.eulerAngles.x;
            sensitivityX = Mathf.Clamp(sensitivityX, mMinimumY, mMaximumY);
            transform.localEulerAngles = new Vector3(0, sensitivityY, 0);
		}

        private void LazyLoadTankToFollow()
        {
            if (m_TankToFollowTransform != null)
            {
                return;
            }

            var tanksList = GameManager.s_Tanks;
            for (int i = 0; i < tanksList.Count; i++)
            {
                TankManager tank = tanksList[i];
                if (tank != null)
                {
                    m_TankToFollowTransform = tank.transform;
                    m_TankToFollowMovement  = tank.movement;
                }
            }
        }
	}
}
