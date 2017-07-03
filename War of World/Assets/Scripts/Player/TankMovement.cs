using UnityEngine;
using System;






namespace Tanks.TankControllers
{

    public class TankMovement : MonoBehaviour
	{

        public Vector3          m_DesiredDirection;
        public float            walkingSpeed          = 5.0f;
        public float            walkingSnappyness     = 50f;
        
        private Animator        m_Animator;  
        private Rigidbody       m_Rigidbody;

        public Rigidbody Rigidbody
        {
            get
            {
                return m_Rigidbody;
            }
        }

      
        public bool isMoving
        {
            get
            {
                return m_DesiredDirection.sqrMagnitude > 0.01f;
            }
        }

        int     floorMask;                      
        float   camRayLength = 100f;   

        public void Init(TankManager manager)
        {
            enabled             = false;
            walkingSpeed        = manager.playerTankType.speed;
            walkingSnappyness   = manager.playerTankType.turnRate;
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
            m_OriginalConstrains  = m_Rigidbody.constraints;
        }

        private void LazyLoadRigidBody()
        {
            if (m_Rigidbody != null)
            {
                return;
            }
            m_Animator  = GetComponent<Animator>();
            floorMask   = LayerMask.GetMask("Floor");
            m_Rigidbody = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            if (isMoving )
            {
                Move();
            }

            m_Animator.SetBool("IsWalking", isMoving);
        }


        private void Move()
        {
            Vector3 targetVelocity = m_DesiredDirection * walkingSpeed * Time.deltaTime;
            Vector3 deltaVelocity  = targetVelocity - m_Rigidbody.velocity;
            if (m_Rigidbody != null && m_Rigidbody.useGravity )
            {
                deltaVelocity.y = 0;
            }

            //m_Rigidbody.AddForce(deltaVelocity * walkingSnappyness, ForceMode.Acceleration );
            m_Rigidbody.position = m_Rigidbody.position + targetVelocity;
            transform.position   = m_Rigidbody.position;
        }

        public void SetTargetPosition( Vector3 target )
        {
            
        }


        private void Turn()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
                m_Rigidbody.MoveRotation(newRotatation);
                transform.rotation = m_Rigidbody.rotation;
            }
        }


        public void SetDefaults()
        {
            enabled = true;
            LazyLoadRigidBody();

            m_Rigidbody.velocity        = Vector3.zero;
            m_DesiredDirection          = Vector3.zero;
        }

      
        public void DisableMovement()
        {
            
        }

    
        public void EnableMovement()
        {
            
        }

        protected RigidbodyConstraints m_OriginalConstrains;
        void OnDisable()
        {
            m_OriginalConstrains             = m_Rigidbody.constraints;
            m_Rigidbody.constraints          = RigidbodyConstraints.FreezeAll;
        }


        void OnEnable()
        {
            m_Rigidbody.constraints = m_OriginalConstrains;
        }
	}
}