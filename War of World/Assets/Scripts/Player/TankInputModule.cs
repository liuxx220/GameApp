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


		protected void SetFirePosition(Vector3 target)
		{
			m_Shooting.SetDesiredFirePosition(target);
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
                if( Time.realtimeSinceStartup - fTouchAndUpTime > 0.5f && m_Shooting.IsShootPressup() )
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
                SetFireIsHeld(false);
            }

            if (move.joystickName == "Left_Joystick")
            {
                m_bJoystickInputL = false;
                DisableMovement();
                SetMovementDirection(Vector3.zero);
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
                m_bJoystickInputL = true;
                if( !m_bJoystickInputR )
                    SetFirePosition(new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y));
                
                Vector3 movedir = new Vector3(x, y, 0);
                if (movedir.sqrMagnitude > 0.01f)
                {
                    Vector3 worldUp = Camera.main.transform.TransformDirection(Vector3.up);
                    worldUp.y       = 0;
                    worldUp.Normalize();
                    Vector3 worldRight = Camera.main.transform.TransformDirection(Vector3.right);
                    worldRight.y    = 0;
                    worldRight.Normalize();

                    Vector3 worldDirection = worldUp * y + worldRight * x;
                    if (worldDirection.magnitude > 1)
                    {
                        worldDirection.Normalize();
                    }
                    SetMovementDirection(worldDirection);
                }
            }

            if (move.joystickName == "Right_Joystick" )
            {
                SetFirePosition(new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y));
                if( m_Shooting.IsShootContinued() )
                    SetFireIsHeld(true);
            }
        }
	}
}