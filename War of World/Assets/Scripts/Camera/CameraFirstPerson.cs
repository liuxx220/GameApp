using UnityEngine;
using Tanks.TankControllers;
using Tanks.Utilities;

namespace Tanks.CameraControl
{
    public class CameraFirstPerson : Singleton<CameraFirstPerson>
	{

		private Transform               m_TankTransform = null;

        public float sensitivityX       = 0;
        public float sensitivityY       = 0;

        public float mMinimumX          = -5;                      // 向下望的最大角度  
        public float mMaximumX          = +8;                       // 向上望的最大角度  
        public float mMinimumY          = -10;                     // 向左望的最大角度  
        public float mMaximumY          = +10;                      // 向右望的最大角度  


		private void Start()
		{
            LazyLoadTankToFollow();
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().freezeRotation = true;
		}

		// Update is called once per frame
		private void Update()
		{
            if ( m_TankTransform == null )
            {
                return;
            }

            transform.position = m_TankTransform.transform.position;
            transform.rotation = m_TankTransform.transform.rotation;
		}

        private void LazyLoadTankToFollow()
        {
            if (m_TankTransform != null)
            {
                return;
            }

            var tanksList = GameManager.s_Tanks;
            for (int i = 0; i < tanksList.Count; i++)
            {
                TankManager tank = tanksList[i];
                if (tank != null)
                {
                    m_TankTransform = tank.transform;
                }
            }
        }
	}
}
