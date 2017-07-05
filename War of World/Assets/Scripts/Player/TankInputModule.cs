using UnityEngine;
using UnityEngine.Networking;
using System;








namespace Tanks.TankControllers
{
	/// <summary>
	/// Tank input module - base class for all input systems
	/// </summary>
	[RequireComponent(typeof(TankShooting))]
	[RequireComponent(typeof(TankMovement))]
    public abstract class TankInputModule : MonoBehaviour
	{
		protected TankShooting  m_Shooting;
		protected TankMovement  m_Movement;


        protected bool          m_bJoystickInputR= false;
        protected bool          m_bJoystickInputL= false;
		protected int           m_GroundLayerMask;
		protected Plane         m_FloorPlane;

        public    Camera        mainCamera;
       
		/// <summary>
		/// Occurs when input method changed.
		/// </summary>
		public static event Action<bool> s_InputMethodChanged;

		protected virtual void Awake()
		{
			m_Shooting          = GetComponent<TankShooting>();
			m_Movement          = GetComponent<TankMovement>();
			m_FloorPlane        = new Plane(Vector3.up, 0);
			m_GroundLayerMask   = LayerMask.GetMask("Ground");
		}

       void Start () {

           
        }


        //On enable, restore our rigidbody's range of movement.
        void OnEnable()
        {
            EasyJoystick.On_JoystickMove      += OnJoystickMove;
            EasyJoystick.On_JoystickMoveStart += OnJoystickMoveStart;
            EasyJoystick.On_JoystickMoveEnd   += OnJoystickMoveEnd;
            EasyJoystick.On_JoystickTouchStart+= OnJoystickTouchStart;
            EasyJoystick.On_JoystickTouchUp   += OnJoystickTouchEnd;
        }


        protected virtual void OnDisable()
        {
            EasyJoystick.On_JoystickMove      -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveStart -= OnJoystickMoveStart;
            EasyJoystick.On_JoystickMoveEnd   -= OnJoystickMoveEnd;
            EasyJoystick.On_JoystickTouchStart-= OnJoystickTouchStart;
            EasyJoystick.On_JoystickTouchUp   -= OnJoystickTouchEnd;
        }

		protected virtual void Update()
		{
			DoMovementInput();
			DoFiringInput();
		}

        /// --------------------------------------------------------------------------------------------------
        /// <summary>
        /// 接口函数，检测鼠标的状态做一些处理
        /// </summary>
        /// --------------------------------------------------------------------------------------------------
		protected abstract bool DoMovementInput();

        /// --------------------------------------------------------------------------------------------------
        /// <summary>
        /// 接口函数，检测键盘的状态做一些处理
        /// </summary>
        /// --------------------------------------------------------------------------------------------------
		protected abstract bool DoFiringInput();

		protected void SetMovementDirection(Vector3 moveDir)
		{
            m_Movement.SetMovementDirection(moveDir);
		}


        protected void DisableMovement()
        {
            m_Movement.SetDefaults();
        }


		protected void SetFirePosition( float fAngle )
		{
            m_Shooting.SetDesiredFirePosition(fAngle);
		}


		public void SetFireIsHeld(bool fireHeld)
		{
			m_Shooting.SetFireIsHeld(fireHeld);
		}

		protected void OnInputMethodChanged(bool isTouch)
		{
			if (s_InputMethodChanged != null)
			{
				s_InputMethodChanged(isTouch);
			}
		}

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断是否属于遥感控制
        /// </summary>
        /// <returns></returns>、
        /// --------------------------------------------------------------------------------------------------------
        protected bool IsJoystickMoving()
        {
            return (m_bJoystickInputL || m_bJoystickInputR);
        }


        private float fTouchAndUpTime = 0f;
        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里用来处理，操纵杆拖动抬起攻击技能
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void OnJoystickTouchStart(MovingJoystick move)
        {
            if (move.joystickName == "Right_Joystick")
            {
                fTouchAndUpTime = Time.realtimeSinceStartup;
            }
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里用来处理，操纵杆拖动抬起攻击技能
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void OnJoystickTouchEnd(MovingJoystick move)
        {
            if (move.joystickName == "Right_Joystick")
            {
                if (m_Shooting.IsShootPressup() && (Time.realtimeSinceStartup - fTouchAndUpTime < 0.25f ) )
                {
                    SetFireIsHeld(true);
                }
            }
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///  移动摇杆开始,这里用来处理，操纵杆连续攻击攻击技能和移动方向的控制
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void OnJoystickMoveStart( MovingJoystick move )
        {
            if( m_Shooting == null || m_Shooting.fireDirection == null )
                return;

            if (move.joystickName == "Right_Joystick")
            {
                m_bJoystickInputR = true;
                m_Shooting.fireDirection.SetActive(true);
            }

            else if (move.joystickName == "Left_Joystick")
            {
                m_bJoystickInputL = true;
                m_Shooting.fireDirection.SetActive(false);
            }
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 移动摇杆结束,和移动方向的控制
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void OnJoystickMoveEnd(MovingJoystick move)
        {
            if (move.joystickName == "Right_Joystick")
            {
                m_bJoystickInputR = false;
                m_Shooting.fireDirection.SetActive(false);
                if (m_Shooting.IsShootContinued())
                {
                    SetFireIsHeld(false);
                }
            }

            if (move.joystickName == "Left_Joystick")
            {
                m_bJoystickInputL = false;
                DisableMovement();
            }
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 移动摇杆中，操纵杆连续攻击攻击技能和移动方向的控制
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void OnJoystickMove(MovingJoystick move)
        {
            float x = move.joystickAxis.x;
            float y = move.joystickAxis.y;
            if (move.joystickName == "Left_Joystick" )
            {

                if (GameSettings.s_Instance.m_PlayerGameModel == Explosions.PLAYGAMEMODEL.PLAYGAME_FPS)
                {
                    Vector3 worldUp = mainCamera.transform.TransformDirection(Vector3.up);
                    worldUp.y = 0;
                    worldUp.Normalize();
                    Vector3 worldRight = mainCamera.transform.TransformDirection(Vector3.right);
                    worldRight.y = 0;
                    worldRight.Normalize();

                    Vector3 worldDirection = worldUp * y + worldRight * x;
                    Vector2 desiredDir = new Vector2(worldDirection.x, worldDirection.z);
                    if (desiredDir.magnitude > 1)
                    {
                        desiredDir.Normalize();
                    }

                    SetMovementDirection(worldDirection);
                }

                if (GameSettings.s_Instance.m_PlayerGameModel == Explosions.PLAYGAMEMODEL.PLAYGAME_TPS)
                {
                    if (!m_bJoystickInputR)
                    {
                        float angle = 90 - Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                        SetFirePosition(angle);
                    }

                    Vector3 worldUp = Camera.main.transform.TransformDirection(Vector3.up);
                    worldUp.y = 0;
                    worldUp.Normalize();
                    Vector3 worldRight = Camera.main.transform.TransformDirection(Vector3.right);
                    worldRight.y = 0;
                    worldRight.Normalize();

                    Vector3 worldDirection = worldUp * y + worldRight * x;
                    Vector2 desiredDir = new Vector2(worldDirection.x, worldDirection.z);
                    if (desiredDir.magnitude > 1)
                    {
                        desiredDir.Normalize();
                    }
                    SetMovementDirection(worldDirection);
                }
            }

            if (move.joystickName == "Right_Joystick" )
            {
                if (GameSettings.s_Instance.m_PlayerGameModel == Explosions.PLAYGAMEMODEL.PLAYGAME_FPS)
                {

                    if (Mathf.Abs(x) > 0)
                    {
                        float speed = x * 30;
                        speed = Mathf.Min(20.0f, speed);
                        transform.Rotate(Vector3.up * Time.deltaTime * speed);
                    }
                }

                if (GameSettings.s_Instance.m_PlayerGameModel == Explosions.PLAYGAMEMODEL.PLAYGAME_TPS)
                {
                    float angle = move.Axis2Angle(true);
                    SetFirePosition(angle);
                    if (m_Shooting.IsShootContinued())
                        SetFireIsHeld(true);
                }
            }
        }
	}
}