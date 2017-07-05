using UnityEngine;
using System;






namespace Tanks.TankControllers
{

    public class TankMovement : MonoBehaviour
	{

        public Vector3          m_DesiredDirection;
        private float           walkingSpeed          = 8.5f;
        public float            walkingSnappyness     = 50f;
        
        private Animator        m_Animator;  
 
      
        public bool isMoving
        {
            get
            {
                return m_DesiredDirection.sqrMagnitude > 0.01f;
            }
        }


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
        }

        private void Awake()
        {
            LazyLoadRigidBody();
        }

        private void LazyLoadRigidBody()
        {
            m_Animator  = GetComponent<Animator>();
            //floorMask   = LayerMask.GetMask("Floor");
        }


        private void FixedUpdate()
        {
            if (isMoving )
            {
                Vector3 targetVelocity = m_DesiredDirection * walkingSpeed * Time.deltaTime;
                transform.position = transform.position + targetVelocity;
            }

            m_Animator.SetBool("IsWalking", isMoving);
        }


        private void Move()
        {
            Vector3 targetVelocity = m_DesiredDirection * walkingSpeed * Time.deltaTime;
            transform.position = transform.position + targetVelocity;
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