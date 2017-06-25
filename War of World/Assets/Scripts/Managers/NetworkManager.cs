using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tanks.Map;
using Tanks.UI;

namespace Tanks.Networking
{
	public enum SceneChangeMode
	{
		None,
		Game,
		Menu
	}

	public enum NetworkState
	{
		Inactive,
		Pregame,
		Connecting,
		InLobby,
		InGame
	}

	public enum NetworkGameType
	{
		Matchmaking,
		Direct,
		Singleplayer
	}

    public class NetworkManager : MonoBehaviour
	{
		#region Constants

		private static readonly string s_LobbySceneName = "LobbyScene";

		#endregion


		#region Events
		/// <summary>
		/// Called on all clients when a player joins
		/// </summary>
		public event Action<NetworkPlayer> playerJoined;
		/// <summary>
		/// Called on all clients when a player leaves
		/// </summary>
		public event Action<NetworkPlayer> playerLeft;
        /// <summary>
        /// 开始单机版的回调函数
        /// </summary>
		private Action                      m_NextHostStartedCallback;
		#endregion


		#region Fields
		/// <summary>
		/// Prefab that is spawned for every connected player
		/// </summary>
		[SerializeField]
		protected NetworkPlayer             m_NetworkPlayerPrefab;

		protected GameSettings              m_Settings;

		private SceneChangeMode             m_SceneChangeMode;

		#endregion

		
		#region Properties

		/// <summary>
		/// Gets whether we're in a lobby or a game
		/// </summary>
		public NetworkState state
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets whether we're a multiplayer or single player game
		/// </summary>
		public NetworkGameType gameType
		{
			get;
			protected set;
		}

		/// <summary>
		/// Collection of all connected players
		/// </summary>
		public List<NetworkPlayer> connectedPlayers
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets current number of connected player
		/// </summary>
		public int playerCount
		{
			get
			{
				return connectedPlayers.Count;
			}
		}

		/// <summary>
		/// Gets whether we're playing in single player
		/// </summary>
		public bool isSinglePlayer
		{
			get
			{
				return gameType == NetworkGameType.Singleplayer;
			}
		}

		/// <summary>
		/// Gets whether we've currently got enough players to start a game
		/// </summary>
		public bool hasSufficientPlayers
		{
			get
			{
				return isSinglePlayer ? playerCount >= 1 : playerCount >= 2;
			}
		}

		#endregion

		#region Singleton

		/// <summary>
		/// Gets the NetworkManager instance if it exists
		/// </summary>
		public static NetworkManager s_Instance
		{
			get;
			protected set;
		}

		public static bool s_InstanceExists
		{
			get { return s_Instance != null; }
		}

		#endregion


		#region Unity Methods

		/// <summary>
		/// Initialize our singleton
		/// </summary>
		protected virtual void Awake()
		{
			{
				s_Instance = this;
				connectedPlayers = new List<NetworkPlayer>();
			}

            SceneManager.activeSceneChanged += OnClientSceneChanged;
            DontDestroyOnLoad(this);
		}

		protected virtual void Start()
		{
			m_Settings = GameSettings.s_Instance;

            // add player to client
            NetworkPlayer newPlayer = Instantiate<NetworkPlayer>(m_NetworkPlayerPrefab);
            DontDestroyOnLoad(newPlayer);
            newPlayer.StartLocalPlayer();
		}

		/// <summary>
		/// Progress to game scene when in transitioning state
		/// </summary>
		protected virtual void Update()
		{
			if (m_SceneChangeMode != SceneChangeMode.None)
			{
				LoadingModal modal = LoadingModal.s_Instance;

				bool ready = true;
				if (modal != null)
				{
					ready = modal.readyToTransition;

					if (!ready && modal.fader.currentFade == Fade.None)
					{
						modal.FadeIn();
					}
				}

				if (ready)
				{
					if (m_SceneChangeMode == SceneChangeMode.Menu)
					{
						if (state != NetworkState.Inactive)
						{
							if (gameType == NetworkGameType.Singleplayer)
							{
								state = NetworkState.Pregame;
							}
							else
							{
								state = NetworkState.InLobby;
							}
						}
						else
						{
							SceneManager.LoadScene(s_LobbySceneName);
						}
					}
					else
					{
						MapDetails map = GameSettings.s_Instance.map;
                        SceneManager.LoadScene(map.sceneName);
						state = NetworkState.InGame;
					}

					m_SceneChangeMode = SceneChangeMode.None;
				}
			}
		}

		/// <summary>
		/// Clear the singleton
		/// </summary>
		protected virtual void OnDestroy()
		{
			if (s_Instance == this)
			{
				s_Instance = null;
			}
            SceneManager.activeSceneChanged -= OnClientSceneChanged;
		}
		#endregion


		#region Methods
		/// <summary>
		/// Disconnect and return the game to the main menu scene
		/// </summary>
		public void DisconnectAndReturnToMenu()
		{
			ReturnToMenu(MenuPage.Home);
		}

		/// <summary>
		/// Initiate single player mode
		/// </summary>
		public void StartSinglePlayerMode(Action callback)
		{
			m_NextHostStartedCallback = callback;
			state = NetworkState.Pregame;
			gameType = NetworkGameType.Singleplayer;
		}

	
		/// <summary>
		/// Makes the server change to the correct game scene for our map, and tells all clients to do the same
		/// </summary>
		public void ProgressToGameScene()
		{
            
			// Update will change scenes once loading screen is visible
			m_SceneChangeMode = SceneChangeMode.Game;

			// Tell NetworkPlayers to show their loading screens
			for (int i = 0; i < connectedPlayers.Count; ++i)
			{
				NetworkPlayer player = connectedPlayers[i];
				if (player != null)
				{
                    ;// player.RpcPrepareForLoad();
				}
			}
		}

		/// <summary>
		/// Makes the server change to the menu scene, and bring all clients with it
		/// </summary>
		public void ReturnToMenu(MenuPage returnPage)
		{
			MainMenuUI.s_ReturnPage = returnPage;

			// Update will change scenes once loading screen is visible
			m_SceneChangeMode = SceneChangeMode.Menu;
			{
				// Show loading screen
				LoadingModal loading = LoadingModal.s_Instance;
				if (loading != null)
				{
					loading.FadeIn();
				}
			}
		}

		/// <summary>
		/// Gets a newwork player by its index
		/// </summary>
		public NetworkPlayer GetPlayerById(int id)
		{
			return connectedPlayers[id];
		}

		/// <summary>
		/// Gets whether all players are ready
		/// </summary>
		public bool AllPlayersReady()
		{
			if (!hasSufficientPlayers)
			{
				return false;
			}
			
			// Check all players
			for (int i = 0; i < connectedPlayers.Count; ++i)
			{
				if (!connectedPlayers[i].ready)
				{
					return false;
				}
			}

			return true;
		}


		/// <summary>
		/// Reset the ready states for all players
		/// </summary>
		public void ClearAllReadyStates()
		{
			for (int i = 0; i < connectedPlayers.Count; ++i)
			{
				NetworkPlayer player = connectedPlayers[i];
				if (player != null)
				{
                    ;// player.ClearReady();
				}
			}
		}

		protected virtual void UpdatePlayerIDs()
		{
			for (int i = 0; i < connectedPlayers.Count; ++i)
			{
				connectedPlayers[i].SetPlayerId(i);
			}
		}


		/// <summary>
		/// Register network players so we have all of them
		/// </summary>
		public void RegisterNetworkPlayer(NetworkPlayer newPlayer)
		{
			MapDetails currentMap = m_Settings.map;
			connectedPlayers.Add(newPlayer);
		}


		/// <summary>
		/// Deregister network players
		/// </summary>
		public void DeregisterNetworkPlayer(NetworkPlayer removedPlayer)
		{
			int index = connectedPlayers.IndexOf(removedPlayer);
			if (index >= 0)
			{
				connectedPlayers.RemoveAt(index);
			}

			UpdatePlayerIDs();
			if (playerLeft != null)
			{
				playerLeft(removedPlayer);
			}
		}

		#endregion


        private void OnClientSceneChanged( Scene scene1, Scene newScene )
        {
            if (m_Settings == null)
                return;
            MapDetails currentMap = m_Settings.map;
            NetworkPlayer localPlayer = connectedPlayers[0];
            if (!localPlayer)
            {
                return;
            }

            LoadingModal modal = LoadingModal.s_Instance;
            if( modal != null )
            {
                modal.FadeOut();
            }

            string sceneName = SceneManager.GetActiveScene().name;
            if (currentMap != null && sceneName == currentMap.sceneName)
            {
                state = NetworkState.InGame;

                // Tell all network players that they're in the game scene
                for (int i = 0; i < connectedPlayers.Count; ++i)
                {
                    NetworkPlayer np = connectedPlayers[i];
                    if (np != null)
                    {
                        np.OnEnterGameScene();
                    }
                }
            }
        }
	}
}