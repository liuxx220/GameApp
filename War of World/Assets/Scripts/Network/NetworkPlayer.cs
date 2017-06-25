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

		
		[SerializeField]
		protected GameObject                m_TankPrefab;
		[SerializeField]
		protected GameObject                m_LobbyPrefab;

		// Set by commands
		private string                      m_PlayerName = "";
		private int                         m_PlayerTankType = 0;
		private bool                        m_Ready = false;

		// Set on the server only

		private int                         m_PlayerId;

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

        private void Start()
        {
            if (m_NetManager == null)
            {
                m_NetManager = TanksNetworkManager.s_Instance;
            }
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

			Debug.Log("Client Network Player start");
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
			if (lobbyObject != null)
			{
				Destroy(lobbyObject.gameObject);
			}
		}


		/// <summary>
		/// Create our lobby object
		/// </summary>
		private void CreateLobbyObject()
		{
			lobbyObject = Instantiate(m_LobbyPrefab).GetComponent<LobbyPlayer>();
			lobbyObject.Init(this);
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
            Debug.Log("OnEnterGameScene");
            GameObject player = Instantiate(m_TankPrefab);
            tank = player.GetComponent<TankManager>();
            tank.SetPlayerId(playerId);
            tank.OnStartClient();
        }
	}
}