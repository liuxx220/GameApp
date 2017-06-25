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

        private float m_OriginalSpeed = 0f;
        private float m_OriginalTurnRate = 0f;
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

        [SerializeField]
        protected float m_NitroShakeMagnitude;

        private Vector2 m_DesiredDirection;

        //The remaining distance for which Nitro will remain active. Server-driven.
        private float m_PowerupDistance;

        [SerializeField]
        protected float m_MaxPowerupDistance = 50f;

        private int m_BoostShakeId;

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

        //Whether the tank was undergoing movement input last tick.
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
            m_BoostShakeId = -1;
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

        private void Update()
        {
            {
                if (!m_HadMovementInput || !isMoving)
                {
                    m_DesiredDirection = Vector2.zero;
                }

                m_HadMovementInput = false;
            }
        }

        private void FixedUpdate()
        {
           
            velocity = transform.position - m_LastPosition;
            m_LastPosition = transform.position;

            // Adjust the rigidbody's position and orientation in FixedUpdate.
            Turn();
            if (isMoving)
            {
                Move();
            }

            Animating();
        }


        private void Move()
        {
            float moveDistance = m_DesiredDirection.magnitude * m_Speed * Time.deltaTime;

            // Create a movement vector based on the input, speed and the time between frames, in the direction the tank is facing.
            Vector3 movement = m_CurrentMovementMode == MovementMode.Backward ? -transform.forward : transform.forward;
            movement *= moveDistance;

            // Apply this movement to the rigidbody's position.
            // Also immediately move our transform so that attached joints update this frame
            m_Rigidbody.position = m_Rigidbody.position + movement;
            transform.position = m_Rigidbody.position;
        }


         
        private void Turn()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
                m_Rigidbody.MoveRotation(newRotatation);
                transform.rotation = m_Rigidbody.rotation;

            }
        }


        // This function is called at the start of each round to make sure each tank is set up correctly.
        public void SetDefaults()
        {
            enabled = true;
            m_PowerupDistance = 0f;
            ResetMovementVariables();
            LazyLoadRigidBody();

            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;

            m_DesiredDirection = Vector2.zero;
            m_CurrentMovementMode = MovementMode.Forward;
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

        //NOTE: This method will only be called from server-based instances of the Nitro pickup.
        public void SetMovementPowerupVariables(float effectiveDistance, float speedBoostRatio, float turnBoostRatio)
        {
            //We don't want the boost powerup to stack its effects. So if we have no boost left, we set all variables. Otherwise, we just top up the effective distance again.
            if (m_PowerupDistance == 0)
            {
                m_Speed = m_OriginalSpeed * speedBoostRatio;
                m_TurnSpeed = m_OriginalTurnRate * turnBoostRatio;
            }

            m_PowerupDistance = Mathf.Clamp(m_PowerupDistance + effectiveDistance, 0f, m_MaxPowerupDistance);
        }

        //We freeze the rigibody when the control is disabled to avoid the tank drifting!
        protected RigidbodyConstraints m_OriginalConstrains;


        //On disable, lock our rigidbody in position.
        void OnDisable()
        {

            EasyJoystick.On_JoystickMove    -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
            m_OriginalConstrains = m_Rigidbody.constraints;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
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
            
        }

        //移动摇杆中
        Vector2 moveDir = Vector2.zero;
        void OnJoystickMove(MovingJoystick move)
        {
  
            //获取摇杆中心偏移的坐标
            moveDir.x = move.joystickAxis.x;
            moveDir.y = move.joystickAxis.y;

            //设置角色的朝向（朝向当前坐标+摇杆偏移量）  
            transform.LookAt(new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z + moveDir.y));
            //移动玩家的位置（按朝向位置移动）
            SetDesiredMovementDirection( moveDir );
        }
	}
}