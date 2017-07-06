using UnityEngine;
using System;






namespace Tanks.TankControllers
{

    public class TankMovement : MonoBehaviour
	{

        public Vector3          m_DesiredDirection;
        private float           walkingSpeed          = 5f;
        public float            walkingSnappyness     = 50f;


        /// <summary>
        /// 角色动画和行为控制器对象
        /// </summary>
        private CharacterController m_Controller;
        private Animator            m_Animator;  
 
      
        public bool isMoving
        {
            get
            {
                return m_DesiredDirection.sqrMagnitude > 0.01f;
            }
        }

        /// -------------------------------------------------------------------------------------------
        /// <summary>
        /// 移动是否启用固定更新逻辑
        /// </summary>
        /// -------------------------------------------------------------------------------------------
        public bool m_bUseFixedUpdate = false;


        public void Init(TankManager manager)
        {
            enabled             = false;
            SetDefaults();
        }

 
        public void SetMovementDirection( Vector3 moveDir )
        {
            m_DesiredDirection = moveDir;
            if (m_DesiredDirection.sqrMagnitude > 1)
            {
                m_DesiredDirection.Normalize();
            }
            //m_DesiredDirection = transform.TransformDirection(m_DesiredDirection);
        }

        private void Awake()
        {
            LazyLoadRigidBody();
        }

        private void LazyLoadRigidBody()
        {
            m_Animator    = GetComponent<Animator>();
            m_Controller  = GetComponent<CharacterController>();
            //floorMask   = LayerMask.GetMask("Floor");
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 行为的心跳帧
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void Update()
        {
            if( !m_bUseFixedUpdate )
            {
                UpdateMove();
            }
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 行为的心跳帧
        /// </summary>
        private void FixedUpdate()
        {
            if (!m_bUseFixedUpdate)
            {
                UpdateMove();
            }
        }

        float gravity = 20.0f;
        private void UpdateMove( )
        {
            if (isMoving)
            {
                CollisionFlags flag;
                Vector3 targetVelocity = m_DesiredDirection * walkingSpeed * Time.deltaTime;
                if (m_Controller != null)
                {
                    targetVelocity.y -= gravity * Time.deltaTime;
                    flag = m_Controller.Move(targetVelocity);
                    //transform.position = new Vector3( transform.position.x, 0, transform.position.z );
                }
            }

            m_Animator.SetBool("IsWalking", isMoving);
        }

        public void SetDefaults()
        {
            enabled = true;
            LazyLoadRigidBody();

            m_DesiredDirection          = Vector3.zero;
        }

      
        public void DisableMovement()
        {
            
        }

    
        public void EnableMovement()
        {
            
        }
	}
}