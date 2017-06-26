using UnityEngine;
using System;






namespace Tanks.TankControllers
{
	//This class is responsible for the movement of the tank and related animation/audio.
    public class TankMovement : MonoBehaviour
	{
        //Enum to define how the tank is moving towards its desired direction.
        public enum MovementMode
        {
            Forward = 1,
            Backward = -1
        }

        private float m_OriginalSpeed = 12f;
        private float m_OriginalTurnRate = 180f;
        private float m_Speed = 12f;

        public float speed
        {
            get
            {
                return m_Speed;
            }
        }
        private float m_TurnSpeed = 180f;

        Animator anim;  
        private Rigidbody m_Rigidbody;

        public Rigidbody Rigidbody
        {
            get
            {
                return m_Rigidbody;
            }
        }

        private Vector2 m_DesiredDirection;
        //The tank's position last tick.
        private Vector3 m_LastPosition;

        private MovementMode m_CurrentMovementMode;

        public MovementMode currentMovementMode
        {
            get
            {
                return m_CurrentMovementMode;
            }
        }

        private bool m_bJoystickInput = false;
        private bool m_HadMovementInput;

        //The final velocity of the tank.
        public Vector3 velocity
        {
            get;
            protected set;
        }

        //Whether the tank is moving.
        public bool isMoving
        {
            get
            {
                return m_DesiredDirection.sqrMagnitude > 0.01f;
            }
        }

        int     floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float   camRayLength = 100f;   


        //Accepts a TankManager reference to pull in all necessary data and references.
        public void Init(TankManager manager)
        {
            enabled = false;
            m_OriginalSpeed = manager.playerTankType.speed;
            m_OriginalTurnRate = manager.playerTankType.turnRate;

            SetDefaults();
        }

        //Called by the active tank input manager to set the movement direction of the tank.
        public void SetDesiredMovementDirection(Vector2 moveDir)
        {
            m_DesiredDirection = moveDir;
            m_HadMovementInput = true;

            if (m_DesiredDirection.sqrMagnitude > 1)
            {
                m_DesiredDirection.Normalize();
            }
        }

        private void Awake()
        {
            //Get our rigidbody, and init originalconstraints for enable/disable code.
            LazyLoadRigidBody();

            m_OriginalConstrains = m_Rigidbody.constraints;

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


        private void Start()
        {
            m_LastPosition = transform.position;
        }


        private void FixedUpdate()
        {
           
            velocity        = transform.position - m_LastPosition;
            m_LastPosition  = transform.position;

            if ( !m_bJoystickInput )
                Turn();

            if (isMoving)
            {
                Move();
            }

            Animating();
        }


        private void Move()
        {
            float moveDistance   = m_DesiredDirection.magnitude * m_Speed * Time.deltaTime;
            Vector3 movement     = transform.forward;
            movement            *= moveDistance;

            m_Rigidbody.position = m_Rigidbody.position + movement;
            transform.position   = m_Rigidbody.position;
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
            m_Rigidbody.angularVelocity = Vector3.zero;
            m_DesiredDirection          = Vector2.zero;
            m_CurrentMovementMode       = MovementMode.Forward;
        }

        //Disable movement, and also disable our engine noise emitter.
        public void DisableMovement()
        {
            m_Speed = 0;
        }

        //Reenable movement, and also the engine noise emitter.
        public void EnableMovement()
        {
            m_Speed = m_OriginalSpeed;
        }

        //We freeze the rigibody when the control is disabled to avoid the tank drifting!
        protected RigidbodyConstraints m_OriginalConstrains;
        void OnDisable()
        {

            EasyJoystick.On_JoystickMove    -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
            m_OriginalConstrains             = m_Rigidbody.constraints;
            m_Rigidbody.constraints          = RigidbodyConstraints.FreezeAll;
        }

        //On enable, restore our rigidbody's range of movement.
        void OnEnable()
        {

            EasyJoystick.On_JoystickMove    += OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
            m_Rigidbody.constraints = m_OriginalConstrains;
        }

        //Reset our movement values to their original values for this tank. This is to reset Nitro effects.
        void ResetMovementVariables()
        {
            m_Speed = m_OriginalSpeed;
            m_TurnSpeed = m_OriginalTurnRate;
        }

 
        void Animating()
        {
            anim.SetBool("IsWalking", isMoving);
        }

        //移动摇杆结束
        void OnJoystickMoveEnd(MovingJoystick move)
        {
            m_bJoystickInput = false;
            DisableMovement();
            SetDesiredMovementDirection( Vector2.zero );
        }

        //移动摇杆中
        Vector2 moveDir = Vector2.zero;
        void OnJoystickMove(MovingJoystick move)
        {
            // 首先使能移动
            EnableMovement();

            //获取摇杆中心偏移的坐标
            m_bJoystickInput = true;
            moveDir.x = move.joystickAxis.x;
            moveDir.y = move.joystickAxis.y;

            //设置角色的朝向（朝向当前坐标+摇杆偏移量）  
            transform.LookAt(new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z + moveDir.y));
            SetDesiredMovementDirection( moveDir );
        }
	}
}