using UnityEngine;
using System;






namespace Tanks.TankControllers
{

    public class TankMovement : MonoBehaviour
	{
        public enum MovementMode
        {
            Forward = 1,
            Backward = -1
        }

        private float m_OriginalSpeed       = 12f;
        private float m_OriginalTurnRate    = 180f;
        private float m_Speed               = 12f;

        public float speed
        {
            get
            {
                return m_Speed;
            }
        }
  

        Animator anim;  
        private Rigidbody m_Rigidbody;

        public Rigidbody Rigidbody
        {
            get
            {
                return m_Rigidbody;
            }
        }

        private Vector3 m_DesiredDirection;
        private MovementMode m_CurrentMovementMode;

        public MovementMode currentMovementMode
        {
            get
            {
                return m_CurrentMovementMode;
            }
        }

        private bool m_bJoystickInput = false;

        UnityEngine.AI.NavMeshAgent navagent;

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
            enabled = false;
            m_OriginalSpeed = manager.playerTankType.speed;
            m_OriginalTurnRate = manager.playerTankType.turnRate;

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
            m_CurrentMovementMode = MovementMode.Forward;
        }

        private void LazyLoadRigidBody()
        {
            if (m_Rigidbody != null)
            {
                return;
            }
            anim        = GetComponent<Animator>();
            floorMask   = LayerMask.GetMask("Floor");
            m_Rigidbody = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            if (isMoving )
            {
                Move();
            }

            anim.SetBool("IsWalking", isMoving);
        }


        private void Move()
        {
            float moveDistance   = m_DesiredDirection.magnitude * m_Speed * Time.deltaTime;
            Vector3 movement     = m_DesiredDirection * moveDistance;
            movement.y           = 0f;
            m_Rigidbody.position = m_Rigidbody.position + movement;
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
            ResetMovementVariables();
            LazyLoadRigidBody();

            m_Rigidbody.velocity        = Vector3.zero;
            m_DesiredDirection          = Vector3.zero;
            m_CurrentMovementMode       = MovementMode.Forward;
        }

      
        public void DisableMovement()
        {
            m_Speed = 0;
        }

    
        public void EnableMovement()
        {
            m_Speed = m_OriginalSpeed;
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

        void ResetMovementVariables()
        {
            m_Speed = m_OriginalSpeed;
        }
	}
}