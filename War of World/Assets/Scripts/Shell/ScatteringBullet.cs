using UnityEngine;
using Tanks.Explosions;
using Tanks.SinglePlayer;







namespace Tanks.Shells
{
    public class ScatteringBullet : MonoBehaviour
	{

        /// <summary>
        /// 移动速度
        /// </summary>
        private float           m_speed = 85f;

        /// <summary>
        /// 寿命
        /// </summary>
        public float            m_lifeTime = 0.2f;

        /// <summary>
        /// 子弹重生事件
        /// </summary>
        private float           m_spawnTime = 0f;

        /// <summary>
        /// Mask for sphere test
        /// </summary>
        private int             m_PhysicsMask;

        /// <summary>
        /// 子弹效果参数
        /// </summary>
        [SerializeField]
        protected ExplosionSettings m_ExplosionSettings;

        /// <summary>
        /// 临时忽律碰撞
        /// </summary>
        private Collider        m_TempIgnoreCollider;
        
        private BoxCollider     m_boxCollider;

        private Vector3         m_ModifySize = new Vector3( 1, 0.1f, 0.1f);
        /// <summary>
        /// 忽略碰撞两 fixedUpdate
        /// </summary>
        private int             m_IgnoreColliderFixedFrames = 2;
        private int             m_TempIgnoreColliderTime = 2;
		private void Awake()
		{
            m_boxCollider       = transform.GetComponent<BoxCollider>();
            m_PhysicsMask       = LayerMask.GetMask("Shootable");
            m_boxCollider.size  = m_ModifySize;
		}

        /// <summary>
        /// 更新运动轨迹
        /// </summary>
        private void Update()
        {
            transform.position += transform.forward * m_speed * Time.deltaTime;
            if (Time.time > m_spawnTime + m_lifeTime )
            {
                transform.gameObject.SetActive(false);
            } 
        }


		public void Setup(int owningPlayerId, Collider ignoreCollider, int seed)
		{
            if( ignoreCollider != null )
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), ignoreCollider, true);
                m_TempIgnoreColliderTime    = m_IgnoreColliderFixedFrames;
                m_TempIgnoreCollider        = ignoreCollider;
            }

            m_ModifySize.x  = 1.0f;
            m_spawnTime     = Time.time;
		}

		private void FixedUpdate()
		{
			if (m_TempIgnoreCollider != null)
			{
				m_TempIgnoreColliderTime--;
				if (m_TempIgnoreColliderTime <= 0)
				{
					Physics.IgnoreCollision(GetComponent<Collider>(), m_TempIgnoreCollider, false);
					m_TempIgnoreCollider = null;
				}
			}
		}
			
			
		// Create explosions on collision
		private void OnCollisionEnter(Collision c)
		{
            // 计算对对方的伤害
            Vector3 hitNormal = c.contacts.Length > 0 ? c.contacts[0].normal : Vector3.up;
            Vector3 hitPos    = c.contacts.Length > 0 ? c.contacts[0].point : transform.position;
            if( c.gameObject != null )
            {
                DoTargetDamage( hitPos, hitNormal, 20 );
            }
		}

		private void OnDestroy()
		{
            // 子弹碰撞后效果
            //if (ExplosionManager.s_InstanceExists)
            //{
            //    ExplosionManager.s_Instance.DestroyBullet(transform.gameObject, BulletClass.FiringExplosion);
            //}
            //else
            //{
            //    MonoBehaviour.Destroy(transform.gameObject);
            //}
		}

        /// -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 计算子弹的伤害
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------
        private void DoTargetDamage(Vector3 pos, Vector3 normal, int damage)
        {
            Collider[] colliders = Physics.OverlapSphere(pos, 2.0f, m_PhysicsMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == null)
                    continue;

                Vector3 hitPos =  colliders[i].transform.position;
                Npc enemy = colliders[i].gameObject.GetComponent<Npc>();
                if (enemy != null)
                    enemy.TakeDamage(100, hitPos);
            }
        }
	}
}