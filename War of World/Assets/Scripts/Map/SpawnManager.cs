using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tanks.Utilities;
using System.Linq;
using Tanks.Rules;
using Tanks.TankControllers;







namespace Tanks.Map
{


	/// <summary>
	/// Spawn manager - used to get an unoccupied spawn point
	/// </summary>
	public class SpawnManager : Singleton<SpawnManager>
	{

        public GameObject               enemy;
        public float                    spawnTime = 3f;

        /// <summary>
        /// NPC刷新点列表
        /// </summary>
		private List<SpawnPoint>        spawnPoints     = new List<SpawnPoint>();
        private List<GameObject>        mapObjectList   = new List<GameObject>();

        /// <summary>
        /// 武器刷新点列表
        /// </summary>
        private List<WeaponSpawnPoint>  weaponPoints    = new List<WeaponSpawnPoint>();
        private List<GameObject>        weaponlist      = new List<GameObject>();


		protected override void Awake()
		{
			base.Awake();
			LazyLoadSpawnPoints();
		}

		private void Start()
		{
			LazyLoadSpawnPoints();
            InvokeRepeating("Spawn", spawnTime, spawnTime);
            InvokeRepeating("SpawnWeapon", 20, 20);
		}

		/// <summary>
		/// 初始各个刷新点和武器刷新点
        /// </summary>
		private void LazyLoadSpawnPoints()
		{
			if (spawnPoints != null && spawnPoints.Count > 0)
			{
				return;
			}

			SpawnPoint[] foundSpawnPoints = GetComponentsInChildren<SpawnPoint>();
			spawnPoints.AddRange(foundSpawnPoints);

            if (weaponPoints != null && weaponPoints.Count > 0)
                return;
            WeaponSpawnPoint[] foundPoints = GetComponentsInChildren<WeaponSpawnPoint>();
            weaponPoints.AddRange(foundPoints);

            for (int i = 0; i < weaponPoints.Count; i++)
            {
                TankWeaponDefinition def = GameSettings.s_Instance.GetWeaponbyID(0);
                if (def == null)
                    continue;

                UnityEngine.Object obj = AssetManager.Get().GetResources(def.perfab);
                GameObject weapon = GameObject.Instantiate(obj) as GameObject;
                weapon.transform.parent = weaponPoints[i].gameObject.transform;
                weaponPoints[i].m_PerviewObj    = weapon;
                weapon.transform.localPosition  = Vector3.zero;
                weapon.transform.localRotation  = Quaternion.identity;
                weapon.transform.localScale     = new Vector3(1.5f, 1.5f, 1.5f);
                weaponlist.Add(weapon);
            }
		}

		/// <summary>
		/// Gets index of a random empty spawn point
		/// </summary>
		public int GetRandomEmptySpawnPointIndex()
		{
			LazyLoadSpawnPoints();
			List<SpawnPoint> emptySpawnPoints = spawnPoints.Where(sp => sp.isEmptyZone).ToList();
            if (emptySpawnPoints.Count == 0)
			{
				return 0;
			}
			
			//Get random empty spawn point
			SpawnPoint emptySpawnPoint = emptySpawnPoints[Random.Range(0, emptySpawnPoints.Count)];
			emptySpawnPoint.SetDirty();
			return spawnPoints.IndexOf(emptySpawnPoint);
		}

		public SpawnPoint GetSpawnPointByIndex(int i)
		{
			LazyLoadSpawnPoints();
			return spawnPoints[i];
		}

		public Transform GetSpawnPointTransformByIndex(int i)
		{
			return GetSpawnPointByIndex(i).spawnPointTransform;
		}

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cleans up the spawn points.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
		public void CleanupSpawnPoints()
		{
			for (int i = 0; i < spawnPoints.Count(); i++)
			{
				spawnPoints[i].Cleanup();
			}

            for( int i = 0; i < weaponPoints.Count; i++ )
            {
                weaponPoints[i].Cleanup();
            }
		}


        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 找到附近最近的敌人
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        public Transform GetLastestEnemy( Vector3 player )
        {
            Vector3 dir     = Vector3.zero;
            Transform tform = null;
            float fMaxDist  = float.MaxValue;
            for (int i = 0; i < mapObjectList.Count; i++ )
            {
                dir = player - mapObjectList[i].transform.position;
                if( dir.sqrMagnitude < fMaxDist )
                {
                    fMaxDist = dir.sqrMagnitude;
                    tform = mapObjectList[i].transform;
                }
            }
            return tform;
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 刷新怪物
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            int i                       = Random.Range(0, spawnPoints.Count);
            GameObject pEnemy           = Instantiate(enemy, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            mapObjectList.Add(pEnemy);
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 刷新武器
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        void SpawnWeapon()
        {
            for (int i = 0; i < weaponPoints.Count; i++)
            {
                if (!weaponPoints[i].IsDirty() )
                    continue;

                TankWeaponDefinition def= GameSettings.s_Instance.GetWeaponbyID(0);
                if (def == null)
                    continue;

                UnityEngine.Object obj  = AssetManager.Get().GetResources(def.perfab);
                GameObject weapon       = GameObject.Instantiate(obj) as GameObject;
                weapon.transform.parent = weaponPoints[i].gameObject.transform;
                weaponPoints[i].m_PerviewObj    = weapon;
                weapon.transform.localPosition  = Vector3.zero;
                weapon.transform.localRotation  = Quaternion.identity;
                weapon.transform.localScale     = new Vector3( 1.5f, 1.5f, 1.5f );
                weaponlist.Add(weapon);
            }
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 销毁所有管理的对象
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------   
        public bool DestoryEnemy( GameObject pEnemy )
        {
            bool ret = mapObjectList.Remove(pEnemy);
            if( ret )
            {
                GameObject.Destroy(pEnemy, 2f);
            }

            return ret;
        }

        /// ------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 销毁所有管理的武器
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------
        public bool DestoryWeapon(GameObject pWeapon )
        {
            bool ret = weaponlist.Remove(pWeapon);
            if (ret)
            {
                GameObject.DestroyImmediate(pWeapon);
            }
            return ret;
        }
	}
}