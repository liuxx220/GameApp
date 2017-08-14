using UnityEngine;
using System;
using UnityEngine.Networking;





namespace Tanks.TankControllers
{
    [Serializable]
    public class MoveAnimation
    {
        [SerializeField]
        public AnimationClip    clip;
        [SerializeField]
        public Vector3          velocity;
        [HideInInspector]
        public float            weight;
        [HideInInspector]
        public bool             currentBest;
        [HideInInspector]
        public float            speed;
        [HideInInspector]
        public float            angle;

        public void Init()
        {
            velocity.y = 0;
            speed = velocity.magnitude;
            angle = TankMovement.HorizontalAngle( velocity );
        }
    }

    public class TankMovement : NetworkBehaviour
	{
        public GameObject           AnimationObject;
        public float                minWalkSpeed = 2.0f;
        public float                maxIdleSpeed = 0.5f;

        [SyncVar]
        private float               walkingSpeed = 5f;

        private float               Speed = 0;
        private float               Angle = 0f;
        private float               lowerBodyDeltAngle = 0;
        private float               idleWeight = 0;
        private float               lastAnimTime = 0;
       

        public AnimationClip        idle;
        public AnimationClip        turn;
        public AnimationClip        shoot;
        
        public MoveAnimation[]      moveAnimations;

        /// <summary>
        /// 角色动画和行为控制器对象
        /// </summary>
        private CharacterController m_Controller;
        private Animation           m_animation;


        private Vector3             m_DesiredDirection;
        private Vector3             m_lastPosition      = Vector3.zero;
        private Vector3             m_Velocity          = Vector3.zero;
        private Vector3             m_localVelocity     = Vector3.zero;
        private Vector3             lowerBodyForward    = Vector3.forward;
        private Vector3             lowerBodyForwardTarget = Vector3.forward;
        private MoveAnimation       bestAnimation = null;

        public  Transform           rootBone;
        public  Transform           upperBodyBone;
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
        public void Init(TankManager manager)
        {
            enabled             = false;
            SetDefaults();
        }


        /// -------------------------------------------------------------------------------------------
        /// <summary>
        /// 设置移动方向
        /// </summary>
        /// -------------------------------------------------------------------------------------------
        public void SetMovementDirection( Vector3 moveDir )
        {
            m_DesiredDirection = moveDir;
            if (m_DesiredDirection.sqrMagnitude > 1)
            {
                m_DesiredDirection.Normalize();
            }
        }

        /// -------------------------------------------------------------------------------------------
        /// <summary>
        /// 得到移动方向
        /// </summary>
        /// -------------------------------------------------------------------------------------------
        public Vector3 GetMovementDirection( )
        {
            return m_DesiredDirection;
        }

        private void Awake()
        {
            m_lastPosition  = transform.position;
            LazyLoadRigidBody();
        }

        private void LazyLoadRigidBody()
        {
            m_animation   = AnimationObject.GetComponent<Animation>();
            m_Controller  = GetComponent<CharacterController>();
            //floorMask   = LayerMask.GetMask("Floor");

            for (int i = 0; i < moveAnimations.Length; i++  )
            {
                moveAnimations[i].Init();
                m_animation[moveAnimations[i].clip.name].layer   = 1;
                m_animation[moveAnimations[i].clip.name].enabled = true;
            }
            m_animation.SyncLayer(1);

            m_animation[idle.name].layer = 2;
            m_animation[turn.name].layer = 3;
            m_animation[idle.name].enabled = true;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 行为的心跳帧
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void Update()
        {

            idleWeight = Mathf.Lerp(idleWeight, Mathf.InverseLerp(minWalkSpeed, maxIdleSpeed, Speed), Time.deltaTime * 10);
            m_animation[idle.name].weight = idleWeight;
            if( Speed > 0 )
            {
                float smallestDiff = Mathf.Infinity;
                for( int i = 0; i < moveAnimations.Length; i++ )
                {
                    var angleDiff = Mathf.Abs(Mathf.DeltaAngle(Angle, moveAnimations[i].angle));
                    var speedDiff = Mathf.Abs(Speed - moveAnimations[i].speed);

                    var diff  = angleDiff + speedDiff;
                    if (moveAnimations[i] == bestAnimation)
				        diff *= 0.9f;

                    if (diff < smallestDiff)
                    {
                        bestAnimation = moveAnimations[i];
                        smallestDiff = diff;
                    }
                }
                m_animation.CrossFade(bestAnimation.clip.name, 0.2f );
            }
            else
            {
                bestAnimation = null;
            }

            if (lowerBodyForward != lowerBodyForwardTarget && idleWeight >= 0.9)
               m_animation.CrossFade(turn.name, 0.2f);

            if ( bestAnimation != null && idleWeight < 0.9f )
            {
                var newAnimTime = Mathf.Repeat(m_animation[bestAnimation.clip.name].normalizedTime * 2 + 0.1f, 1);
                if (newAnimTime < lastAnimTime)
                {
                    
                }
                lastAnimTime = newAnimTime;
            }

            if (!hasAuthority)
            {
                return;
            }

            if (isMoving)
            {
                Vector3 targetVelocity = m_DesiredDirection * walkingSpeed * Time.deltaTime;
                if (m_Controller != null)
                {
                    m_Controller.Move(targetVelocity);
                }
            }
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 行为的心跳帧
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FixedUpdate()
        {
            m_Velocity      = (transform.position - m_lastPosition) / Time.deltaTime;
            m_localVelocity = transform.InverseTransformDirection(m_Velocity);
            m_localVelocity.y = 0;
            Speed           = m_localVelocity.magnitude;
            Angle           = HorizontalAngle(m_localVelocity);
            m_lastPosition  = transform.position;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 行为的心跳帧
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void LateUpdate()
        {
            if (!hasAuthority)
            {
                return;
            }

            float idle = Mathf.InverseLerp(minWalkSpeed, maxIdleSpeed, Speed);
            if ( idle < 1)
            {
                Vector3 animatedLocalVelocity = Vector3.zero;
                for( int i = 0; i < moveAnimations.Length; i++ )
                {
                    if (m_animation[moveAnimations[i].clip.name].weight == 0)
                        continue;
                    if (Vector3.Dot(moveAnimations[i].velocity, m_localVelocity) <= 0)
                        continue;

                    animatedLocalVelocity += moveAnimations[i].velocity * m_animation[moveAnimations[i].clip.name].weight;
                }
                float lowerBodyDeltaAngleTarget = Mathf.DeltaAngle(
                                                  HorizontalAngle(transform.rotation * animatedLocalVelocity),
                                                  HorizontalAngle(m_Velocity));

                lowerBodyDeltAngle              = Mathf.LerpAngle(lowerBodyDeltAngle, lowerBodyDeltaAngleTarget, Time.deltaTime);
                lowerBodyForwardTarget          = transform.forward;
                lowerBodyForward                = Quaternion.Euler(0, lowerBodyDeltAngle, 0) * lowerBodyForwardTarget;
            }
            else
            {
                lowerBodyForward                = Vector3.RotateTowards(lowerBodyForward, lowerBodyForwardTarget, Time.deltaTime * 520 * Mathf.Deg2Rad, 1);
                lowerBodyDeltAngle              = Mathf.DeltaAngle( HorizontalAngle(transform.forward),
                                                                    HorizontalAngle(lowerBodyForward));

                if (Mathf.Abs(lowerBodyDeltAngle) > 80)
                    lowerBodyForwardTarget = transform.forward;
            }

            Quaternion lowerBodyDeltaRotation   = Quaternion.Euler(0, lowerBodyDeltAngle, 0);
            rootBone.rotation                   = lowerBodyDeltaRotation * rootBone.rotation;
            upperBodyBone.rotation              = Quaternion.Inverse(lowerBodyDeltaRotation) * upperBodyBone.rotation;
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

        public static float HorizontalAngle( Vector3 dir )
        {
            return Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        }
	}
}