using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tanks.Utilities;
using System.Linq;
using Tanks.TankControllers;







namespace Tanks.Map
{


	/// <summary>
	/// Spawn manager - used to get an unoccupied spawn point
	/// </summary>
	public class SpawnManager : Singleton<SpawnManager>
	{

        public GameObject           enemy;
        public float                spawnTime = 3f;
		private List<SpawnPoint>    spawnPoints     = new List<SpawnPoint>();
        private List<GameObject>    mapObjectList   = new List<GameObject>();

        /// <summary>
        /// 武器刷新点列表
        /// </summary>
        private List<SpawnPoint>    weaponPoints    = new List<SpawnPoint>();

		protected override void Awake()
		{
			base.Awake();
			LazyLoadSpawnPoints();
		}

		private void Start()
		{
			LazyLoadSpawnPoints();
            InvokeRepeating("Spawn", spawnTime, spawnTime);
		}

		/// <summary>
		/// Lazy load the spawn points - this assumes that all spawn points are children of the SpawnManager
		/// </summary>
		private void LazyLoadSpawnPoints()
		{
			if (spawnPoints != null && spawnPoints.Count > 0)
			{
				return;
			}

			SpawnPoint[] foundSpawnPoints = GetComponentsInChildren<SpawnPoint>();
			spawnPoints.AddRange(foundSpawnPoints);
		}

		/// <summary>
		/// Gets index of a random empty spawn point
		/// </summary>
		/// <returns>The random empty spawn point index.</returns>
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

		/// <summary>
		/// Cleans up the spawn points.
		/// </summary>
		public void CleanupSpawnPoints()
		{
			for (int i = 0; i < spawnPoints.Count(); i++)
			{
				spawnPoints[i].Cleanup();
			}
		}


        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        /// 找到附近最近的敌人
        /// </summary>
        /// -------------------------------------------------------------------------------------------------
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

        void Spawn()
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            GameObject pEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
            mapObjectList.Add(pEnemy);
        }


        public bool DestoryEnemy( GameObject pEnemy )
        {
            bool ret = mapObjectList.Remove(pEnemy);
            if( ret )
            {
                GameObject.Destroy(pEnemy, 2f);
            }
            return ret;
        }
	}
}