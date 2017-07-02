using UnityEngine;
using Tanks.Shells;
using Tanks.CameraControl;
using Tanks.Data;
using System;
using Tanks.Effects;
using Tanks.TankControllers;









namespace Tanks.Explosions
{
    /// <summary>
    /// 对战斗过程中使用频率比较高的资源缓存
    /// </summary>
    [Serializable]
    public class ExplosionCache
    {
        public GameObject       m_prefab;
        public int              m_cacheSize;

        private GameObject[] m_caches;
        private int m_cacheIndex = 0;

        public void InitExplosionCache()
        {
            m_caches = new GameObject[m_cacheSize];
            for (int i = 0; i < m_cacheSize; i++)
            {
                m_caches[i] = MonoBehaviour.Instantiate(m_prefab) as GameObject;
                m_caches[i].SetActive(false);
                m_caches[i].name = m_caches[i].name + i.ToString();
            }
        }

        public GameObject NextGameObject()
        {
            GameObject obj = null;
            for (int i = 0; i < m_cacheSize; i++)
            {
                obj = m_caches[i];
                if (!obj.activeSelf)
                    break;

                m_cacheIndex = (m_cacheIndex + 1) % m_cacheSize;
            }

            if (obj.activeSelf)
            {
                // 归还到列表
            }
            m_cacheIndex = (m_cacheIndex + 1) % m_cacheSize;
            return obj;
        }
    }


    public class ExplosionManager : MonoBehaviour
	{
		/// <summary>
		/// Screen shake duration for explosions
		/// </summary>
		protected float m_ExplosionScreenShakeDuration = 0.3f;

		/// <summary>
		/// Mask for sphere test
		/// </summary>
		private int m_PhysicsMask;


		/// <summary> 
		/// Reimplemented singleton. Can't use generics on NetworkBehaviours
		/// </summary>
		public static ExplosionManager s_Instance
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets whether an instance of this singleton exists
		/// </summary>
		public static bool              s_InstanceExists { get { return s_Instance != null; } }

	
        /// <summary>
        /// 缓存列表
        /// </summary>
        public ExplosionCache[]        m_ExplosionCache;

		/// <summary>
		/// Get physics mask
		/// </summary>
		protected virtual void Awake()
		{
			if (s_Instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				s_Instance = this;
			}
			m_PhysicsMask       = LayerMask.GetMask("Players", "Projectiles", "Powerups", "DestructibleHazards", "Decorations");
		}

		protected virtual void Start()
		{
			for( int i = 0; i < m_ExplosionCache.Length; i++ )
            {
                m_ExplosionCache[i].InitExplosionCache();
            }
		}


		/// <summary>
		/// Clear instance
		/// </summary>
		protected virtual void OnDestroy()
		{
			if (s_Instance == this)
			{
				s_Instance = null;
			}
		}

		/// <summary>
		/// Create an explosion at a given location
		/// </summary>
		public void SpawnExplosion(Vector3 explosionPosition, Vector3 explosionNormal, GameObject ignoreObject, int damageOwnerId, ExplosionSettings explosionConfig, bool clientOnly)
		{
			if (clientOnly)
			{
				CreateVisualExplosion(explosionPosition, explosionNormal, explosionConfig.explosionClass);
			}
			DoLogicalExplosion(explosionPosition, explosionNormal, ignoreObject, damageOwnerId, explosionConfig);
		}

		/// <summary>
		/// Perform logical explosion
		/// On server, this deals damage to stuff. On clients, it just applies forces
		/// </summary>
		private void DoLogicalExplosion(Vector3 explosionPosition, Vector3 explosionNormal, GameObject ignoreObject, int damageOwnerId, ExplosionSettings explosionConfig)
		{
			Collider[] colliders = Physics.OverlapSphere(explosionPosition, Mathf.Max(explosionConfig.explosionRadius, explosionConfig.physicsRadius), m_PhysicsMask);
			for (int i = 0; i < colliders.Length; i++)
			{
				Collider struckCollider = colliders[i];
				if (struckCollider.gameObject == ignoreObject)
				{
					continue;
				}

				Vector3 explosionToTarget = struckCollider.transform.position - explosionPosition;
				float explosionDistance = explosionToTarget.magnitude;
			}

			DoShakeForExplosion(explosionPosition, explosionConfig);
		}


        /// <summary>
        /// 实例化子弹对象
        /// </summary>
        public GameObject CreateVisualBullet(Vector3 pos, Vector3 dir, float speed, BulletClass explosionClass)
        {
            GameObject spawnedEffect = null;
            int ClassIndex      = (int)explosionClass;
            ExplosionCache pool = m_ExplosionCache[ClassIndex];
            switch (explosionClass)
            {
                case BulletClass.FiringExplosion:
                    spawnedEffect = pool.NextGameObject();
                    break;
                case BulletClass.ClusterExplosion:
                    spawnedEffect = pool.NextGameObject();
                    break;
            }
            return spawnedEffect;
        }

        /// <summary>
        /// 子弹与物体碰撞后的爆炸效果
        /// </summary>
		private void CreateVisualExplosion(Vector3 explosionPosition, Vector3 explosionNormal, ExplosionClass explosionClass)
		{
            /*
			Effect spawnedEffect = null;
			switch (explosionClass)
			{
				case ExplosionClass.ExtraLarge:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.extraLargeExplosion);
					break;
				case ExplosionClass.Large:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.largeExplosion);
					break;
				case ExplosionClass.Small:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.smallExplosion);
					break;
				case ExplosionClass.TankExplosion:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.tankExplosion);
					break;
				case ExplosionClass.TurretExplosion:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.turretExplosion);
					break;
				case ExplosionClass.BounceExplosion:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.smallExplosion);
					break;
				case ExplosionClass.FiringExplosion:
					spawnedEffect = Instantiate<Effect>(m_EffectsGroup.firingExplosion);
					break;
			}

			if (spawnedEffect != null)
			{
				spawnedEffect.transform.position = explosionPosition;
				spawnedEffect.transform.up = explosionNormal;

				AudioSource sound = spawnedEffect.GetComponentInChildren<AudioSource>();
				if (sound != null)
				{
					sound.clip = ThemedEffectsLibrary.s_Instance.GetRandomExplosionSound(explosionClass);
					sound.Play();
				}
			}
            */
		}


		/// <summary>
		/// Make a pretty explosion on clients
		/// </summary>
        public void DestroyExplosion(GameObject destoryObj, ExplosionClass explosionClass)
		{
            int ClassIndex      = (int)explosionClass;
            ExplosionCache pool = m_ExplosionCache[ClassIndex];
            if (pool != null )
            {
                destoryObj.SetActive(false);
            }
            else
            {
                MonoBehaviour.Destroy(destoryObj);
            }
		}


        /// <summary>
        /// Make a pretty explosion on clients
        /// </summary>
        public void DestroyBullet(GameObject destoryObj, BulletClass explosionClass)
        {
            int ClassIndex = (int)explosionClass;
            ExplosionCache pool = m_ExplosionCache[ClassIndex];
            if (pool != null)
            {
                destoryObj.SetActive(false);
            }
            else
            {
                MonoBehaviour.Destroy(destoryObj);
            }
        }

		private void DoShakeForExplosion(Vector3 explosionPosition, ExplosionSettings explosionConfig)
		{
			// Do screen shake on main camera
			if (ScreenShakeController.s_InstanceExists)
			{
				ScreenShakeController shaker = ScreenShakeController.s_Instance;

				float shakeMagnitude = explosionConfig.shakeMagnitude;
				shaker.DoShake(explosionPosition, shakeMagnitude, m_ExplosionScreenShakeDuration, 0.0f, 1.0f);
			}
		}
	}
}