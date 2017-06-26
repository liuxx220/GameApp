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

        protected bool          m_bJoystickInput = false;
		protected int           m_GroundLayerMask;
		protected Plane         m_FloorPlane;

		/// <summary>
		/// Occurs when input method changed.
		/// </summary>
		public static event Action<bool> s_InputMethodChanged;

		public static TankInputModule s_CurrentInputModule
		{
			get;
			private set;
		}

		public bool isActiveModule
		{
			get { return s_CurrentInputModule == this; }
		}

		protected virtual void Awake()
		{
			OnBecomesInactive();
			m_Shooting          = GetComponent<TankShooting>();
			m_Movement          = GetComponent<TankMovement>();
			m_FloorPlane        = new Plane(Vector3.up, 0);
			m_GroundLayerMask   = LayerMask.GetMask("Ground");
		}


        //On enable, restore our rigidbody's range of movement.
        void OnEnable()
        {

            EasyJoystick.On_JoystickMove    += OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
        }


        protected virtual void OnDisable()
        {
            SetFireIsHeld(false);
            EasyJoystick.On_JoystickMove    -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }

		protected virtual void Update()
		{
			bool isActive = DoMovementInput();
			isActive |= DoFiringInput();

			if (isActive && !isActiveModule)
			{
				if (s_CurrentInputModule != null)
				{
					s_CurrentInputModule.OnBecomesInactive();
				}
				s_CurrentInputModule = this;
				OnBecomesActive();
			}
		}

		protected virtual void OnBecomesActive()
		{
		}

		protected virtual void OnBecomesInactive()
		{
		}

		protected abstract bool DoMovementInput();

		protected abstract bool DoFiringInput();

		protected void SetDesiredMovementDirection(Vector2 moveDir)
		{
			m_Movement.SetDesiredMovementDirection(moveDir);
		}


        protected void DisableMovement()
        {
            m_Movement.SetDefaults();
        }


		protected void SetDesiredFirePosition(Vector3 target)
		{
			//m_Shooting.SetDesiredFirePosition(target);
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

        //移动摇杆结束
        protected void OnJoystickMoveEnd(MovingJoystick move)
        {
            m_bJoystickInput = false;
            DisableMovement();
            SetDesiredMovementDirection(Vector2.zero);
        }

        //移动摇杆中
        Vector2 moveDir = Vector2.zero;
        protected void OnJoystickMove(MovingJoystick move)
        {
            //获取摇杆中心偏移的坐标
            m_bJoystickInput = true;
            moveDir.x = move.joystickAxis.x;
            moveDir.y = move.joystickAxis.y;

            //设置角色的朝向（朝向当前坐标+摇杆偏移量）  
            transform.LookAt(new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z + moveDir.y));
            SetDesiredMovementDirection(moveDir);
        }
	}
}