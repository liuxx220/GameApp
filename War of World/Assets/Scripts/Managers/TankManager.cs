using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Tanks.Data;
using Tanks.Analytics;
using TanksNetworkPlayer = Tanks.Networking.NetworkPlayer;
using TanksNetworkManager = Tanks.Networking.NetworkManager;









namespace Tanks.TankControllers
{
	/// <summary>
	/// This class is to manage various settings on a tank.
	/// It works with the GameManager class to control how the tanks behave
	/// and whether or not players have control of their tank in the
	/// different phases of the game.
	/// </summary>
    public class TankManager : NetworkBehaviour
	{
		#region Fields

		//Current spawn point used
		private Transform m_AssignedSpawnPoint;


        [SyncVar(hook = "OnPlayerIdChanged")]
        protected int m_PlayerId = -1;
        [SyncVar]
        protected int m_Score = 0;
        [SyncVar(hook = "OnRankChanged")]
		protected int m_Rank = -1;

		#endregion


		#region Events

		//Fired when the pickup is collected
		public event Action<string> onPickupCollected;
		
		//Fired when the round currency changes
		public event Action<int> onCurrencyChanged;

		//Fired when the player's rank has changed
		public event Action rankChanged;

		//Fired when the player's award currency has changed
		public event Action awardCurrencyChanged;

		#endregion


		#region Properties

		public TanksNetworkPlayer player
		{
			get;
			protected set;
		}

		public TankMovement movement
		{
			get;
			protected set;
		}

		public TankShooting shooting
		{
			get;
			protected set;
		}

        public HUDPlayer hudPlayer
        {
            get;
            protected set;
        }

		public string playerName
		{
			get { return player.playerName; }
		}

		public int playerNumber
		{
			get { return m_PlayerId; }
		}

		public TankTypeDefinition playerTankType
		{
			get;
			protected set;
		}

		public bool removedTank
		{
			get;
			private set;
		}

		public bool ready
		{
			get { return player.ready; }
		}

		public bool initialized
		{
			get;
			private set;
		}

		#endregion


		#region Methods

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 初始化
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!initialized && m_PlayerId >= 0)
            {
                Initialize();
            }
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 初始化
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private void Initialize()
        {
            Initialize(TanksNetworkManager.s_Instance.GetPlayerById(m_PlayerId));
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Set up this tank with the correct properties
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private void Initialize(TanksNetworkPlayer player)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            this.player = player;
            playerTankType = TankLibrary.s_Instance.GetTankDataForIndex(player.tankType);


            // Analytics messages on server
            if (isServer)
            {
                AnalyticsHelper.PlayerUsedTankInGame(playerTankType.id);
            }

            // Create visual tank
            player.transform.position = transform.position;
            player.transform.SetParent(transform, true);
            if (isServer)
            {
                AnalyticsHelper.PlayerUsedTankInGame(playerTankType.id);
            }

            movement        = GetComponent<TankMovement>();
            shooting        = GetComponent<TankShooting>();
            hudPlayer       = GetComponent<HUDPlayer>();
            movement.Init(this);

            shooting.SetPlayerWeapon(0);
            GameManager.AddTank(this);

            if (player.hasAuthority)
            {
                DisableShooting();
                player.CmdSetReady();
            }
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 初始化
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        public override void OnNetworkDestroy()
        {
            base.OnNetworkDestroy();
            if (player != null)
            {
                player.tank = null;
            }

            if (GameManager.s_Instance != null )
                GameManager.s_Instance.RemoveTank(this);
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 禁止射击
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
		public void DisableShooting()
		{
			shooting.enabled = true;
		}


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 禁止射击，移动
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
		public void DisableControl()
		{
			movement.DisableMovement();
			shooting.enabled = false;
		}


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 禁止射击
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
		public void EnableControl()
		{
			movement.EnableMovement();
			shooting.enabled = true;
		}

		/// <summary>
		/// Moves tank to a spawn location transform
		/// </summary>
		/// <param name="spawnPoint">Spawn point.</param>
		public void MoveToSpawnLocation(Transform spawnPoint)
		{
			if (spawnPoint != null)
			{
				m_AssignedSpawnPoint = spawnPoint;
			}
			movement.transform.position = m_AssignedSpawnPoint.position;
		}

		/// <summary>
		/// Resets the tank at a specified spawn point
		/// </summary>
		/// <param name="spawnPoint">Spawn point.</param>
		public void Reset(Transform spawnPoint)
		{
			movement.SetDefaults();
			MoveToSpawnLocation(spawnPoint);
		}

		/// <summary>
		/// Prespawning, used by round based modes to ensure the tank is in the correct state prior to running spawn flow
		/// </summary>
		public void Prespawn()
		{
			
		}

		/// <summary>
		/// Respawns the tank at a position and ensures that it is invisible to prevent visible interpolation artifacts on clients
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="rotation">Rotation.</param>
		public void RespawnReposition(Vector3 position, Quaternion rotation)
		{
			if (removedTank)
			{
				return;
			}

			movement.transform.position = position;
		}

		/// <summary>
		/// Reactivates the tank as part of the spawn process. Turns on movement and shooting and enables visuals
		/// </summary>
		public void RespawnReactivate()
		{
			if (removedTank)
			{
				return;
			}
			movement.SetDefaults();
		}

		#region SYNCVAR HOOKS

		private void OnRankChanged(int rank)
		{
			this.m_Rank = rank;
			if (rankChanged != null)
			{
				rankChanged();
			}
		}

		private void OnPlayerIdChanged(int playerId)
		{
			this.m_PlayerId = playerId;
			Initialize();
		}

		#endregion

		#region Networking
        [Server]
		public void SetRank(int rank)
		{
			this.m_Rank = rank;
		}


        [Server]
        public void SetPlayerId(int id)
        {
            m_PlayerId = id;
        }

		#endregion

		#endregion
	}
}
