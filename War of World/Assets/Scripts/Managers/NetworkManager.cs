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
		private static readonly string      s_LobbySceneName = "LobbyScene";
		#endregion


		#region Events
		public event Action<NetworkPlayer>  playerJoined;
		public event Action<NetworkPlayer>  playerLeft;
		private Action                      m_NextHostStartedCallback;
		#endregion


		#region Fields
		[SerializeField]
        protected GameObject                m_NetworkPlayerPrefab;
		protected GameSettings              m_Settings;
		private SceneChangeMode             m_SceneChangeMode;
		#endregion

		
		#region Properties
		public NetworkState state
		{
			get;
			protected set;
		}

		public NetworkGameType gameType
		{
			get;
			protected set;
		}

		public List<NetworkPlayer> connectedPlayers
		{
			get;
			private set;
		}

		public int playerCount
		{
			get{ return connectedPlayers.Count;}
		}

		public bool isSinglePlayer
		{
			get { return gameType == NetworkGameType.Singleplayer; }
		}

		public bool hasSufficientPlayers
		{
			get {return isSinglePlayer ? playerCount >= 1 : playerCount >= 2;}
		}
		#endregion


		public static NetworkManager s_Instance
		{
			get;
			protected set;
		}


		#region Unity Methods
		protected virtual void Awake()
		{
            s_Instance = this;
            connectedPlayers = new List<NetworkPlayer>();
            SceneManager.activeSceneChanged += OnClientSceneChanged;
            DontDestroyOnLoad(this);
		}


		protected virtual void Start()
		{
			m_Settings = GameSettings.s_Instance;
            GameObject LocalPlayer  = Instantiate(m_NetworkPlayerPrefab);
            NetworkPlayer newPlayer = LocalPlayer.GetComponent<NetworkPlayer>();
            DontDestroyOnLoad(LocalPlayer);
            newPlayer.StartLocalPlayer();
		}


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
		/// Initiate single player mode
		/// </summary>
		public void StartSinglePlayerMode(Action callback)
		{
			m_NextHostStartedCallback = callback;
			state       = NetworkState.Pregame;
			gameType    = NetworkGameType.Singleplayer;
		}

	
		/// <summary>
		/// Makes the server change to the correct game scene for our map, and tells all clients to do the same
		/// </summary>
		public void ProgressToGameScene()
		{
			m_SceneChangeMode = SceneChangeMode.Game;
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
                for (int i = 0; i < connectedPlayers.Count; ++i)
                {
                    NetworkPlayer np = connectedPlayers[i];
                    if (np != null)
                    {
                        np.OnEnterGameScene();
                    }
                }
            }

            if( sceneName == "snow2")
            {
                localPlayer.SetGameModel(Explosions.PLAYGAMEMODEL.PLAYGAME_TPS);
                GameSettings.s_Instance.m_PlayerGameModel = Explosions.PLAYGAMEMODEL.PLAYGAME_TPS;
            }
            if (sceneName == "snow3")
            {
                localPlayer.SetGameModel(Explosions.PLAYGAMEMODEL.PLAYGAME_FPS);
                GameSettings.s_Instance.m_PlayerGameModel = Explosions.PLAYGAMEMODEL.PLAYGAME_FPS;
            }
        }
	}
}