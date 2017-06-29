using System;
using UnityEngine;
using Tanks.Data;
using Tanks.TankControllers;
using Tanks.UI;
using TanksNetworkManager = Tanks.Networking.NetworkManager;





namespace Tanks.Networking
{
    public class NetworkPlayer : MonoBehaviour
	{

		private string                      m_PlayerName = "";
		private int                         m_PlayerTankType = 0;
		private bool                        m_Ready = false;
		private int                         m_PlayerId;

        public int                          m_StartingHealth = 100;
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
        /// 胶囊体
        /// </summary>
        CapsuleCollider                     m_CapsuleCollider;

        /// <summary>
        /// 资源相关, 动画控制器
        /// </summary>
        Animator                             m_CtrlAnimator;


        private TanksNetworkManager         m_NetManager;
		private GameSettings                m_Settings;

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
		/// Gets the local NetworkPlayer object
		/// </summary>
		public static NetworkPlayer s_LocalPlayer
		{
			get;
			private set;
		}


        void Awake()
        {
            m_IsDead            = false;
            m_CurrentHealth     = m_StartingHealth;
            m_CtrlAnimator      = GetComponent<Animator>();
            m_AudioSource       = GetComponent<AudioSource>();
            m_BehitParticles    = GetComponentInChildren<ParticleSystem>();
            m_CapsuleCollider   = GetComponent<CapsuleCollider>();
        }



		/// <summary>
		/// Register us with the NetworkManager
		/// </summary>
        public void StartLocalPlayer()
		{
			if (m_Settings == null)
			{
				m_Settings = GameSettings.s_Instance;
			}

            if( m_NetManager == null )
            {
                m_NetManager = TanksNetworkManager.s_Instance;
            }

            m_NetManager.RegisterNetworkPlayer(this);
		}

		
		public void SetPlayerId(int playerId)
		{
			this.m_PlayerId = playerId;
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


		/// <summary>
		/// Set up our player choices, changing local values too
		/// </summary>
		private void UpdatePlayerSelections()
		{
			Debug.Log("UpdatePlayerSelections");
			PlayerDataManager dataManager = PlayerDataManager.s_Instance;
			if (dataManager != null)
			{
				m_PlayerTankType = dataManager.selectedTank;
				m_PlayerName = dataManager.playerName;
			}
		}

        public void OnEnterGameScene()
        {
            tank = gameObject.GetComponent<TankManager>();
            tank.SetPlayerId(playerId);
            tank.OnStartClient();
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
            m_CapsuleCollider.isTrigger = true;
            m_CtrlAnimator.SetTrigger("Dead");
            m_AudioSource.clip = m_DeathClip;
            m_AudioSource.Play();
        }
	}
}