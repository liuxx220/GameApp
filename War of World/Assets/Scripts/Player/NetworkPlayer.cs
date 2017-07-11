using System;
using UnityEngine;
using Tanks.Data;
using Tanks.TankControllers;
using Tanks.UI;
using Tanks.Explosions;
using UnityEngine.Networking;
using TanksNetworkManager = Tanks.Networking.NetworkManager;





namespace Tanks.Networking
{
    public class NetworkPlayer : NetworkBehaviour
	{

        [SerializeField]
        protected GameObject                m_LobbyPrefab;

        [SyncVar]
        public int                          m_StartingHealth = 100;
        [SyncVar(hook = "OnCurrentHealthChanged")]
        public int                          m_CurrentHealth;

        private bool                        m_IsDead;

        /// <summary>
        /// 资源相关, 死亡音效
        /// </summary>
        public AudioClip                    m_DeathClip;

        /// <summary>
        /// 资源相关, 音效控制器
        /// </summary>
        AudioSource                         m_AudioSource;

        /// <summary>
        /// 资源相关, 被攻特效
        /// </summary>
        ParticleSystem                      m_BehitParticles;


        /// <summary>
        /// 资源相关, 动画控制器
        /// </summary>
        Animator                             m_CtrlAnimator;


        private TanksNetworkManager         m_NetManager;
		private GameSettings                m_Settings;

        #region actionEvent  
        public  event Action<NetworkPlayer>  syncVarsChanged;
        public  event Action<NetworkPlayer>  becameReady;
        public  event Action                 gameDetailsReady;
        public  event Action<int>            healthChanged;
        #endregion

        /// 需要同步的变量
        [SyncVar(hook = "OnMyName")]
        private string                      m_PlayerName = "";
        [SyncVar(hook = "OnMyTank")]
        private int                         m_PlayerTankType = 0;
        [SyncVar(hook = "OnReadyChanged")]
        private bool                        m_Ready = false;
        [SyncVar(hook = "OnHasInitialized")]
        private bool                        m_Initialized = false;

        [SyncVar]
        private int                         m_PlayerId;
        /// <summary>
		/// Gets this player's id
		/// </summary>
		public int playerId
		{
			get { return m_PlayerId; }
		}

		/// <summary>
		/// Gets this player's name
		/// </summary>
		public string playerName
		{
			get { return m_PlayerName; }
		}

		/// <summary>
		/// Gets this player's tank ID
		/// </summary>
		public int tankType
		{
			get { return m_PlayerTankType; }
		}

		/// <summary>
		/// Gets whether this player has marked themselves as ready in the lobby
		/// </summary>
		public bool ready
		{
			get { return m_Ready; }
		}

		/// <summary>
		/// Gets the tank manager associated with this player
		/// </summary>
		public TankManager tank
		{
			get;
			set;
		}

        /// <summary>
        /// Gets the lobby object associated with this player
        /// </summary>
        public LobbyPlayer lobbyObject
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the local NetworkPlayer object
        /// </summary>
        public static NetworkPlayer s_LocalPlayer
        {
            get;
            private set;
        }

        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// 设置游戏的模式
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        public void SetGameModel( PLAYGAMEMODEL model )
        {
            //if( model == PLAYGAMEMODEL.PLAYGAME_FPS )
            //{
            //    m_FPS.SetActive(true);
            //    m_TPS.SetActive(false);
            //    m_Flag.SetActive(false);
            //}
            //else
            //{
            //    m_FPS.SetActive(false);
            //    m_TPS.SetActive(true);
            //    m_Flag.SetActive(true);
            //}
        }

        void Awake()
        {
            m_IsDead            = false;
            m_CurrentHealth     = m_StartingHealth;
            m_CtrlAnimator      = GetComponent<Animator>();
            m_AudioSource       = GetComponent<AudioSource>();
            m_BehitParticles    = GetComponentInChildren<ParticleSystem>();
        }

		/// <summary>
		/// Clean up lobby object for us
		/// </summary>
		protected virtual void OnDestroy()
		{
            if (s_LocalPlayer != null)
			{
                Destroy(s_LocalPlayer.gameObject);
			}
		}

        public override void OnNetworkDestroy()
        {
            base.OnNetworkDestroy();
            Debug.Log("Client Network Player OnNetworkDestroy");
            
            if( lobbyObject != null )
            {
                Destroy(lobbyObject.gameObject);
            }

            if (m_NetManager != null)
            {
                m_NetManager.DeregisterNetworkPlayer(this);
            }
        }


        public void StartLocalPlayer()
        {
            if (m_Settings == null)
            {
                m_Settings = GameSettings.s_Instance;
            }

            if (m_NetManager == null)
            {
                m_NetManager = TanksNetworkManager.s_Instance;
            }
            m_NetManager.RegisterNetworkPlayer(this);
        }

        private void CreateLobbyObject()
        {
            lobbyObject = Instantiate(m_LobbyPrefab).GetComponent<LobbyPlayer>();
            lobbyObject.Init(this);
        }

        /// <summary>
        /// 处理角色被攻击
        /// </summary>
        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (m_IsDead)
                return;

            m_AudioSource.Play();
            m_CurrentHealth -= amount;
            m_BehitParticles.transform.position = hitPoint;
            m_BehitParticles.Play();
            if (m_CurrentHealth <= 0)
            {
                Death();
            }
        }

        /// <summary>
        /// 处理角色被攻击
        /// </summary>
        public void TakeDamage(int amount)
        {
            if (m_IsDead)
                return;

            m_AudioSource.Play();
            m_CurrentHealth -= amount;
            if (m_CurrentHealth <= 0)
            {
                Death();
            }
        }


        /// <summary>
        /// 处理角色死亡
        /// </summary>
        void Death()
        {
            m_IsDead = true;
            m_CtrlAnimator.SetTrigger("Dead");
            m_AudioSource.clip = m_DeathClip;
            m_AudioSource.Play();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 网络层，客户端逻辑

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 开始本地客户端对象
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        [Client]
        public override void OnStartLocalPlayer()
        {
            if (m_Settings == null)
			{
				m_Settings = GameSettings.s_Instance;
			}
            base.OnStartLocalPlayer();
            Debug.Log("Local Network Player start");
            UpdatePlayerSelections();

            s_LocalPlayer = this;
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 开始本地客户端对象
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        [Client]
        public override void OnStartClient()
        {
            DontDestroyOnLoad(this);
            if (m_NetManager == null)
            {
                m_NetManager = TanksNetworkManager.s_Instance;
            }

            base.OnStartClient();
            Debug.Log("Client Network Player start");
            m_NetManager.RegisterNetworkPlayer(this);
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 进入场景，逻辑层
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        [Client]
        public void OnEnterGameScene()
        {
            tank = gameObject.GetComponent<TankManager>();
            tank.SetPlayerId(playerId);
            tank.OnStartClient();

            if( hasAuthority )
            {
                CmdClientReadyInScene();
            }
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 进入Lobby场景，逻辑层
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        [Client]
        public void OnEnterLobbyScene()
        {
            Debug.Log("OnEnterLobbyScene");
            if (m_Initialized && lobbyObject == null )
            {
                CreateLobbyObject();
            }
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Set up our player choices, changing local values too
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        [Client]
        private void UpdatePlayerSelections()
        {
            Debug.Log("UpdatePlayerSelections");
            PlayerDataManager dataManager = PlayerDataManager.s_Instance;
            if (dataManager != null)
            {
                m_PlayerTankType    = dataManager.selectedPlayer;
                m_PlayerName        = dataManager.playerName;
                CmdSetInitialValues(m_PlayerTankType, 0, 0, m_PlayerName);
            }
        }


        [ClientRpc]
        public void RpcSetGameSettings(int mapIndex, int modeIndex)
        {
            GameSettings settings = GameSettings.s_Instance;
            if (!isServer)
            {
                settings.SetMapIndex(mapIndex);
                settings.SetModeIndex(modeIndex);
            }
            if (gameDetailsReady != null && isLocalPlayer)
            {
                gameDetailsReady();
            }
        }

        [ClientRpc]
        public void RpcPrepareForLoad()
        {
            if (isLocalPlayer)
            {
                // Show loading screen
                LoadingModal loading = LoadingModal.s_Instance;

                if (loading != null)
                {
                    loading.FadeIn();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 网络层，服务器端逻辑
        [Server]
        public void ClearReady()
        {
            m_Ready = false;
        }

        [Server]
        public void SetPlayerId(int playerId)
        {
            this.m_PlayerId = playerId;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 命令层

        /// -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 创建我们自己的英雄
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------
        [Command]
        private void CmdClientReadyInScene()
        {
            Debug.Log("CmdClientReadyInScene");
            NetworkServer.SpawnWithClientAuthority(gameObject, connectionToClient);
            tank = gameObject.GetComponent<TankManager>();
            tank.SetPlayerId(playerId);
        }

        /// -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------
        [Command]
        private void CmdSetInitialValues(int tankType, int decorationIndex, int decorationMaterial, string newName)
        {
            Debug.Log("CmdChangeTank");
            m_PlayerTankType    = tankType;
            m_PlayerName        = newName;
            m_Initialized       = true;
        }

        /// -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 英雄类型
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------
        [Command]
        public void CmdChangeTank(int tankType)
        {
            Debug.Log("CmdChangeTank");
            m_PlayerTankType = tankType;
        }

        /// -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 设置英雄的外貌
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------
        [Command]
        public void CmdChangeDecorationProperties(int decorationIndex, int decorationMaterial)
        {
            Debug.Log("CmdChangeDecorationProperties");
            //m_PlayerTankDecoration = decorationIndex;
            //m_PlayerTankDecorationMaterial = decorationMaterial;
        }

        /// -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 设置英雄的游戏状态
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------
        [Command]
        public void CmdSetReady()
        {
            Debug.Log("CmdSetReady");
            if (m_NetManager.hasSufficientPlayers)
            {
                m_Ready = true;

                if (becameReady != null)
                {
                    becameReady(this);
                }
            }
        }

        ///----------------------------------------------------------------------------------------------------------------------
        /// Syncvar call backs
        private void OnMyName( string newName )
        {
            m_PlayerName = newName;
            if (syncVarsChanged != null)
            {
                syncVarsChanged(this);
            }
        }

        private void OnMyTank(int tankIndex)
        {
            if (tankIndex != -1)
            {
                m_PlayerTankType = tankIndex;

                if (syncVarsChanged != null)
                {
                    syncVarsChanged(this);
                }
            }
        }

        private void OnReadyChanged(bool value)
        {
            m_Ready = value;

            if (syncVarsChanged != null)
            {
                syncVarsChanged(this);
            }
        }

        private void OnHasInitialized(bool value)
        {
            if (!m_Initialized && value)
            {
                m_Initialized = value;
                CreateLobbyObject();

                if (isServer && !m_Settings.isSinglePlayer)
                {
                    RpcSetGameSettings(m_Settings.mapIndex, m_Settings.modeIndex);
                }
            }
        }

        void OnCurrentHealthChanged(int value)
        {
            m_CurrentHealth = value;

            if (healthChanged != null)
            {
                healthChanged(m_CurrentHealth / m_StartingHealth);
            }
        }
	}
}