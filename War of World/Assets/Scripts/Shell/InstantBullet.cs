using UnityEngine;
using Tanks.Explosions;
using Tanks.SinglePlayer;







namespace Tanks.Shells
{
    public class InstantBullet : MonoBehaviour
	{

        /// <summary>
        /// 移动速度
        /// </summary>
        private float           m_speed = 250f;

        /// <summary>
        /// 寿命
        /// </summary>
        public float            m_lifeTime = 0.5f;

        /// <summary>
        /// 飞行最大距离
        /// </summary>
        private float           m_dist = 1000f;

        /// <summary>
        /// 子弹重生事件
        /// </summary>
        private float           m_spawnTime = 0f;

        /// <summary>
        /// 临时忽律碰撞
        /// </summary>
        private Collider        m_TempIgnoreCollider;
        
        /// <summary>
        /// 忽略碰撞两 fixedUpdate
        /// </summary>
        private int             m_IgnoreColliderFixedFrames = 2;
        private int             m_TempIgnoreColliderTime = 2;
		private void Awake()
		{
            
            m_spawnTime = Time.time;
		}

		public void Setup(int owningPlayerId, Collider ignoreCollider, int seed)
		{
            if( ignoreCollider != null )
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), ignoreCollider, true);
                m_TempIgnoreColliderTime    = m_IgnoreColliderFixedFrames;
                m_TempIgnoreCollider        = ignoreCollider;
            }
            m_speed     = seed;
            m_spawnTime = Time.time;
            m_dist      = 1000f;
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
			
		/// <summary>
		/// 更新运动轨迹
		/// </summary>
		private void Update()
		{
            transform.position += transform.forward * m_speed * Time.deltaTime;
            m_dist              -= m_speed * Time.deltaTime;
            if( Time.time > m_spawnTime + m_lifeTime || m_dist < 0 )
            {
                transform.gameObject.SetActive(false);
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
                Npc enemy = c.gameObject.GetComponent<Npc>();
                if( enemy != null )
                    enemy.TakeDamage(20, hitPos);
            }


            // 子弹碰撞后效果
            if (ExplosionManager.s_InstanceExists)
            {
                ExplosionManager.s_Instance.DestroyBullet(transform.gameObject, BulletClass.FiringExplosion);
            }
            else
            {
                MonoBehaviour.Destroy(transform.gameObject);
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
        private void DoTargetDamage(Vector3 pos, Vector3 normal, GameObject ignoreObject, int damage)
        {

        }
	}
}