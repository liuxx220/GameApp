using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
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

    public class NetworkManager : UnityEngine.Networking.NetworkManager
	{
		#region Constants
		private static readonly string          s_LobbySceneName = "LobbyScene";
		#endregion

        public GameObject                       m_NetworkPlayerPrefab;

		public GameObject m_newPlayer;

		#region Events
		public event Action<NetworkPlayer>      playerJoined;
		public event Action<NetworkPlayer>      playerLeft;
		private Action                          m_NextHostStartedCallback;
		#endregion


        /// <summary>
        /// 网络层触发事件的东西
        /// </summary>
        #region NetworkEvent
        public  event Action                    hostStarted;
        public  event Action                    serverStopped;
        public  event Action                    clientStopped;
        public  event Action<NetworkConnection> clientConnected;    // 客户端链接事件
        public  event Action<NetworkConnection> clientDisconnected;
        public  event Action<NetworkConnection, int> clientError;
        public  event Action<NetworkConnection, int> serverError;
        public  event Action<bool,string>       sceneChanged;
        public  event Action                    serverPlayersReadied;
        public  event Action<bool, MatchInfo>   matchCreated;
        public  event Action<bool, MatchInfo>   matchJoined;
        public  event Action                    serverClientDisconnected;
        /// <summary>
        /// Called when game mode changes
        /// </summary>
        public  event Action                    gameModeUpdated;
        /// <summary>
        /// Called when we've been dropped from a matchMade game
        /// </summary>
        public  event Action                    matchDropped;

        private Action<bool, MatchInfo>         NextMatchJoinedCallback;
        private Action<bool, MatchInfo>         NextMatchCreateCallback;
        #endregion

        #region Fields
        [SerializeField]
        protected int                           m_MultiplayerMaxPlayers = 4;

		protected GameSettings                  m_Settings;

		private SceneChangeMode                 m_SceneChangeMode;
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

        public static bool s_IsServer
        {
            get
            {
                return NetworkServer.active;
            }
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
            s_Instance          = this;
            connectedPlayers    = new List<NetworkPlayer>();
            DontDestroyOnLoad(this);
		}


		protected virtual void Start()
		{
			m_Settings = GameSettings.s_Instance;
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
                        //SceneManager.LoadScene(map.sceneName);
                        ServerChangeScene(map.sceneName);
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
        
		}
		#endregion


		#region Methods
        /// --------------------------------------------------------------------------------------------------
        /// <summary>
        /// 断开链接
        /// </summary>
        /// --------------------------------------------------------------------------------------------------
        public void Disconnect( )
        {
            switch( gameType )
            {
                case NetworkGameType.Direct:
                    StopDirectMultiplayerGame();
                    break;

                case NetworkGameType.Matchmaking:
                    StopMatchmakingGame();
                    break;

                case NetworkGameType.Singleplayer:
                    StopSingleplayerGame();
                    break;
            }
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 断开链接并返回主界面
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void DisconnectAndReturnToMenu()
        {
            Disconnect();
            ReturnToMenu(MenuPage.Home);
        }


        /// --------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 开启一个单人游戏模式
		/// </summary>
        /// --------------------------------------------------------------------------------------------------------
		public void StartSinglePlayerMode(Action callback)
		{
            if (state != NetworkState.Inactive)
            {
                throw new InvalidOperationException("Network currently active. Disconnect first.");
            }

			m_NextHostStartedCallback = callback;
			state       = NetworkState.Pregame;
			gameType    = NetworkGameType.Singleplayer;
            StartHost();
		}

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 开启一个多人游戏模式
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
		public void StartMultiplayerServer( Action callback )
        {
            if( state != NetworkState.Inactive )
            {
                throw new InvalidOperationException("Network currently active. Disconnect first.");
            }

            m_NextHostStartedCallback = callback;
            state                     = NetworkState.InLobby;
            gameType                  = NetworkGameType.Direct;

            StartHost();
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 开启一个多人匹配游戏模式
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
		public void StartMatchmakingGame( string gameName, Action< bool, MatchInfo> onCreate )
        {
            if (state != NetworkState.Inactive)
            {
                throw new InvalidOperationException("Network currently active. Disconnect first.");
            }

            state       = NetworkState.Connecting;
            gameType    = NetworkGameType.Matchmaking;

            StartMatchMaker();
            NextMatchCreateCallback = onCreate;
            matchMaker.CreateMatch(gameName, (uint)4, true, string.Empty, string.Empty, string.Empty, 0, 0, OnMatchCreate);
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Initialize the matchmaking client to receive match lists
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void StartMatchingmakingClient()
        {
            if (state != NetworkState.Inactive)
            {
                throw new InvalidOperationException("Network currently active. Disconnect first.");
            }

            state       = NetworkState.Pregame;
            gameType    = NetworkGameType.Matchmaking;
            StartMatchMaker();
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 加入游戏 匹配模式
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void JoinMatchmakingGame( NetworkID netID, Action< bool, MatchInfo> onJoin )
        {
            if( gameType != NetworkGameType.Matchmaking || state != NetworkState.Pregame )
            {
                throw new InvalidOperationException("Game not in matching state. Make sure you call StartMatchmakingClient first.");
            }

            state       = NetworkState.Connecting;
            NextMatchJoinedCallback = onJoin;
            matchMaker.JoinMatch(netID, string.Empty, string.Empty, string.Empty, 0, 0, OnMatchJoined);
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 停止单人游戏模式
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void StopSingleplayerGame()
        {
            switch( state )
            {
                case NetworkState.InLobby:
                    break;

                case NetworkState.Connecting:
                case NetworkState.Pregame:
                case NetworkState.InGame:
                    StopHost();
                    break;
            }
            state = NetworkState.Inactive;
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 停止直接多人游戏模式
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void StopDirectMultiplayerGame()
        {
            switch (state)
            {
                case NetworkState.Connecting:
                case NetworkState.Pregame:
                case NetworkState.InGame:
                    {
                        if (s_IsServer)
                            StopHost();
                        else
                            StopClient();
                    }
                    break;
            }
            state = NetworkState.Inactive;
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 停止多人匹配游戏模式
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void StopMatchmakingGame()
        {
            switch (state)
            {
                case NetworkState.Connecting:
                    {
                        if (s_IsServer)
                        {
                            StopMatchMaker();
                            StopHost();
                            matchInfo = null;
                        }
                        else
                        {
                            StopMatchMaker();
                            StopClient();
                            matchInfo = null;
                        }
                    }
                    break;
                case NetworkState.Pregame:
                    {
                        if( !s_IsServer )
                            StopMatchMaker();
                    }
                    break;
                case NetworkState.InLobby:
                case NetworkState.InGame:
                    {
                        if( s_IsServer )
                        {
                            if( matchMaker != null && matchInfo != null )
                            {
                                matchMaker.DestroyMatch(matchInfo.networkId, 0, (success, info) =>
                                {
                                    if (!success)
                                    {
                                        Debug.LogErrorFormat("Failed to terminate matchmaking game. {0}", info);
                                    }
                                    StopMatchMaker();
                                    StopHost();
                                    matchInfo = null;
                                });
                            }
                            else
                            {
                                Debug.LogWarning("No matchmaker or matchInfo despite being a server in matchmaking state.");
                                StopMatchMaker();
                                StopHost();
                                matchInfo = null;
                            }
                        }
                        else
                        {
                            if (matchMaker != null && matchInfo != null)
                            {
                                matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, (success, info) =>
                                {
                                    if (!success)
                                    {
                                        Debug.LogErrorFormat("Failed to disconnect from matchmaking game. {0}", info);
                                    }
                                    StopMatchMaker();
                                    StopClient();
                                    matchInfo = null;
                                });
                            }
                            else
                            {
                                Debug.LogWarning("No matchmaker or matchInfo despite being a client in matchmaking state.");
                                StopMatchMaker();
                                StopClient();
                                matchInfo = null;
                            }
                        }
                    }
                    break;
            }

            state   = NetworkState.Inactive;
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the current matchmaking game as unlisted
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void UnlistMatch()
        {
            if (gameType == NetworkGameType.Matchmaking &&
                matchMaker != null)
            {
                matchMaker.SetMatchAttributes(matchInfo.networkId, false, 0, (success, info) => Debug.Log("Match hidden"));
            }
        }

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Causes the current matchmaking game to become listed again
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void ListMatch()
        {
            if (gameType == NetworkGameType.Matchmaking &&
                matchMaker != null)
            {
                matchMaker.SetMatchAttributes(matchInfo.networkId, true, 0, (success, info) => Debug.Log("Match shown"));
            }
        }


		/// <summary>
		/// Makes the server change to the correct game scene for our map, and tells all clients to do the same
		/// </summary>
		public void ProgressToGameScene()
		{
            ClearAllReadyStates();

            UnlistMatch();
			m_SceneChangeMode = SceneChangeMode.Game;
			for (int i = 0; i < connectedPlayers.Count; ++i)
			{
				NetworkPlayer player = connectedPlayers[i];
				if (player != null)
				{
                    player.RpcPrepareForLoad();
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

        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 游戏状态更新
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        protected void FireGameModeUpdated()
        {
            if (gameModeUpdated != null)
            {
                gameModeUpdated();
            }
        }


        /// --------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 增加一个玩家到本地游戏中
		/// </summary>
        /// --------------------------------------------------------------------------------------------------------
		public void RegisterNetworkPlayer(NetworkPlayer newPlayer)
		{

			MapDetails currentMap = m_Settings.map;
            Debug.Log("Player joined");
			connectedPlayers.Add(newPlayer);
            newPlayer.becameReady += OnPlayerSetReady;

            if (s_IsServer)
            {
                UpdatePlayerIDs();
            }

            string sceneName = SceneManager.GetActiveScene().name;
            if (currentMap != null && sceneName == currentMap.sceneName)
            {
                newPlayer.OnEnterGameScene();
            }
            else if (sceneName == s_LobbySceneName)
            {
                newPlayer.OnEnterLobbyScene();
            }

            if( playerJoined != null )
            {
                playerJoined(newPlayer);
            }

            newPlayer.gameDetailsReady += FireGameModeUpdated;
		}

        /// --------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 从本地游戏里删除一个玩家
		/// </summary>
        /// --------------------------------------------------------------------------------------------------------
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

            removedPlayer.gameDetailsReady -= FireGameModeUpdated;
            if (removedPlayer != null)
            {
                removedPlayer.becameReady -= OnPlayerSetReady;
            }
		}
		#endregion


       
        /// ------------------------------------------------------------------------------------------------------
        #region Networking event


        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("OnClientError");
            base.OnClientError(conn, errorCode);
            if( clientError != null )
            {
                clientError(conn, errorCode);
            }
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            Debug.Log("OnClientConnect");
            ClientScene.Ready(conn);
            ClientScene.AddPlayer(0);
            
            if( clientConnected != null )
            {
                clientConnected(conn);
            }

        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("OnClientDisconnect");
            base.OnClientDisconnect(conn);
            if( clientDisconnected != null )
            {
                clientDisconnected(conn);
            }
        }

        public override void OnServerError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("OnClientDisconnect");
            base.OnServerError(conn, errorCode);
            if( serverError != null )
            {
                serverError(conn, errorCode);
            }
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            Debug.Log("OnServerSceneChanged");
            base.OnServerSceneChanged(sceneName);
            if( sceneChanged != null )
            {
                sceneChanged(true, sceneName);
            }

            if( sceneName == s_LobbySceneName )
            {
                ListMatch();
                networkSceneName = string.Empty;
            }
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            MapDetails currentMap = m_Settings.map;
            Debug.Log("OnClientSceneChanged");
            base.OnClientSceneChanged(conn);

            PlayerController pc = conn.playerControllers[0];
            if( !pc.unetView.isLocalPlayer )
            {
                return;
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
            else if (sceneName == s_LobbySceneName)
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

                // Tell all network players that they're in the lobby scene
                for (int i = 0; i < connectedPlayers.Count; ++i)
                {
                    NetworkPlayer np = connectedPlayers[i];
                    if (np != null)
                    {
                        np.OnEnterLobbyScene();
                    }
                }
            }

            if (sceneChanged != null)
            {
                sceneChanged(false, sceneName);
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            Debug.Log("OnServerAddPlayer");

            GameObject newPlayer = Instantiate(m_NetworkPlayerPrefab);
            DontDestroyOnLoad(newPlayer);
			m_newPlayer = newPlayer;
            NetworkServer.AddPlayerForConnection(conn, newPlayer, playerControllerId);
        }

        public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
        {
            Debug.Log("OnServerRemovePlayer");
            base.OnServerRemovePlayer(conn, player);

            NetworkPlayer connectedPlayer = GetPlayerForConnection(conn);
            if (connectedPlayer != null)
            {
                Destroy(connectedPlayer);
                connectedPlayers.Remove(connectedPlayer);
            }
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            Debug.Log("OnServerReady");
            base.OnServerReady(conn);
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            Debug.LogFormat("OnServerConnect\nID {0}\nAddress {1}\nHostID {2}", conn.connectionId, conn.address, conn.hostId);

            if (numPlayers >= 4 ||
                state != NetworkState.InLobby)
            {
                conn.Disconnect();
            }
            else
            {
                // Reset ready flags for everyone because the game state changed
                if (state == NetworkState.InLobby)
                {
                    ClearAllReadyStates();
                }
            }

            base.OnServerConnect(conn);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            Debug.Log("OnServerDisconnect");
            base.OnServerDisconnect(conn);

            // Reset ready flags for everyone because the game state changed
            if (state == NetworkState.InLobby)
            {
                ClearAllReadyStates();
            }

            if (serverClientDisconnected != null)
            {
                serverClientDisconnected();
            }
        }

        public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
        {
            base.OnMatchCreate(success, extendedInfo, matchInfo);
            Debug.Log("OnMatchCreate");

            if (success)
            {
                state = NetworkState.InLobby;
            }
            else
            {
                state = NetworkState.Inactive;
            }

            if (NextMatchCreateCallback != null)
            {
                NextMatchCreateCallback(success, matchInfo);
                NextMatchCreateCallback = null;
            }

            // Fire event
            if (matchCreated != null)
            {
                matchCreated(success, matchInfo);
            }
        }

        public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
        {
            base.OnMatchJoined(success, extendedInfo, matchInfo);
            Debug.Log("OnMatchJoined");

            if (success)
            {
                state = NetworkState.InLobby;
            }
            else
            {
                state = NetworkState.Pregame;
            }

            // Fire callback
            if (NextMatchJoinedCallback != null)
            {
                NextMatchJoinedCallback(success, matchInfo);
                NextMatchJoinedCallback = null;
            }

            // Fire event
            if (matchJoined != null)
            {
                matchJoined(success, matchInfo);
            }
        }

        public override void OnDropConnection(bool success, string extendedInfo)
        {
            base.OnDropConnection(success, extendedInfo);
            Debug.Log("OnDropConnection");

            if (matchDropped != null)
            {
                matchDropped();
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            networkSceneName = string.Empty;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            Debug.Log("OnStopServer");

            for (int i = 0; i < connectedPlayers.Count; ++i)
            {
                NetworkPlayer player = connectedPlayers[i];
                if (player != null)
                {
                    NetworkServer.Destroy(player.gameObject);
                }
            }

            connectedPlayers.Clear();
            networkSceneName = string.Empty;

            if (serverStopped != null)
            {
                serverStopped();
            }
        }

        public override void OnStopClient()
        {
            Debug.Log("OnStopClient");
            base.OnStopClient();

            for (int i = 0; i < connectedPlayers.Count; ++i)
            {
                NetworkPlayer player = connectedPlayers[i];
                if (player != null)
                {
                    Destroy(player.gameObject);
                }
            }

            connectedPlayers.Clear();
            if (clientStopped != null)
            {
                clientStopped();
            }
        }

        public override void OnStartHost()
        {
            Debug.Log("OnStartHost");
            base.OnStartHost();

            if (m_NextHostStartedCallback != null)
            {
                m_NextHostStartedCallback();
                m_NextHostStartedCallback = null;
            }
            if (hostStarted != null)
            {
                hostStarted();
            }
        }

        public virtual void OnPlayerSetReady(NetworkPlayer player)
        {
            if (AllPlayersReady() && serverPlayersReadied != null)
            {
                serverPlayersReadied();
            }
        }
        #endregion

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断所有的玩家是否准备好了
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public bool AllPlayersReady()
        {
            if (!hasSufficientPlayers)
            {
                return false;
            }

            for (int i = 0; i < connectedPlayers.Count; ++i)
            {
                if (!connectedPlayers[i].ready)
                {
                    return false;
                }
            }
            return true;
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 清除所有的玩家的状态
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public void ClearAllReadyStates()
        {
            for (int i = 0; i < connectedPlayers.Count; ++i)
            {
                NetworkPlayer player = connectedPlayers[i];
                if (player != null)
                {
                    player.ClearReady();
                }
            }
        }

        /// -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据 NetworkConnection 得到 NetworkPlayer
        /// </summary>
        /// -----------------------------------------------------------------------------------------------------
        public static NetworkPlayer GetPlayerForConnection(NetworkConnection conn)
        {
            return conn.playerControllers[0].gameObject.GetComponent<NetworkPlayer>();
        }
    }
}