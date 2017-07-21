using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Tanks.TankControllers;
using Tanks.Data;
using Tanks.Rules;
using Tanks.UI;
using Tanks.Map;
using Tanks.Hazards;
using Tanks.Explosions;
using Tanks.Analytics;
using Tanks.Rules.SinglePlayer;
using Tanks.Networking;
using TanksNetworkManager = Tanks.Networking.NetworkManager;
using TanksNetworkPlayer = Tanks.Networking.NetworkPlayer;
using Tanks.Audio;
using System;






namespace Tanks
{
    /// <summary>
    /// Game state.
    /// </summary>
    public enum GameState
    {
        Inactive,
        TimedTransition,
        StartUp,
        Preplay,
        Preround,
        Playing,
        RoundEnd,
        EndGame,
        PostGame,
        EveryoneBailed
    }

    /// <summary>
    /// Game manager - handles game state and passes state to rules processor
    /// </summary>
    public class GameManager : NetworkBehaviour
    {
        //Singleton reference
        static public GameManager           s_Instance;

        static public List<TankManager>     s_Tanks = new List<TankManager>();

        //The explosion manager prefab
        [SerializeField]
        protected ExplosionManager          m_ExplosionManagerPrefab;
        [SerializeField]
        protected EndGameModal              m_MultiplayerGameModal;
        [SerializeField]
        protected StartGameModal            m_SinglePlayerModal;
        [SerializeField]
        protected Transform                 m_EndGameUiParent;
        [SerializeField]
        protected FadingGroup               m_EndScreen;


        protected GameSettings              m_GameSettings;
        protected GameState                 m_State = GameState.Inactive;
        private   GameState                 m_NextState;
        //Transition state variables
        private float                       m_TransitionTime = 0f;
       

        //synced variable for the game being finished
        [HideInInspector]
        [SyncVar]
        protected bool                      m_GameIsFinished = false;

        //Various UI references to hide the screen between rounds.
        private FadingGroup                 m_LoadingScreen;

        //The local player
        private TankManager                 m_LocalPlayer;
        private int                         m_LocalPlayerNumber = 0;
        private bool                        m_HazardsActive;
        protected EndGameModal              m_EndGameModal;


        //Number of players in game
        private int                         m_NumberOfPlayers = 0;
        private bool                        m_AllBailHandled = false;
        protected bool                      m_CanStartGame = false;
        protected StartGameModal            m_StartGameModal;
        private TanksNetworkManager         m_NetManager;
        private int                         m_Round = 0;
        protected InGameLeaderboardModal    m_Leaderboard;
        protected AnnouncerModal            m_Announcer;

       
        
        #region Initialisation
        /// <summary>
        /// Unity message: Awake
        /// </summary>
        private void Awake()
        {
            s_Instance      = this;
            InstantiateEndGameModal(m_MultiplayerGameModal);
            m_NetManager    = TanksNetworkManager.s_Instance;

            //Subscribe to events on the Network Manager
            if (m_NetManager != null)
            {
                m_NetManager.clientDisconnected += OnDisconnect;
                m_NetManager.clientError        += OnError;
                m_NetManager.serverError        += OnError;
                m_NetManager.matchDropped       += OnDrop;
            }
        }

        /// <summary>
        /// Unity message: OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            if (m_NetManager != null)
            {
                m_NetManager.clientDisconnected -= OnDisconnect;
                m_NetManager.clientError        -= OnError;
                m_NetManager.serverError        -= OnError;
                m_NetManager.matchDropped       -= OnDrop;
            }
            s_Tanks.Clear();
        }

        /// <summary>
        /// Cache the game setting
        /// </summary>
        private void SetGameSettings()
        {
            m_GameSettings = GameSettings.s_Instance;
        }

        /// <summary>
        /// Unity message: Start ,   Only called on server
        /// </summary>
        [ServerCallback]
        private void Start()
        {
            //Set the state to startup
            m_State = GameState.StartUp;
            SetGameSettings();

            if (m_ExplosionManagerPrefab != null)
            {
                ExplosionManager explosionManager = Instantiate<ExplosionManager>(m_ExplosionManagerPrefab);
                NetworkServer.Spawn(explosionManager.gameObject);
            }

            if (m_GameSettings.isSinglePlayer)
            {
                AnalyticsHelper.SinglePlayerLevelStarted(m_GameSettings.map.id);
                SetupSinglePlayerModals();
            }
            else
            {
                AnalyticsHelper.MultiplayerGameStarted(m_GameSettings.map.id, m_GameSettings.mode.id, m_NetManager.playerCount);
            }
        }


        public void StartGame()
        {
            m_CanStartGame = true;
        }

        /// <summary>
        /// Setups the single player modals.
        /// </summary>
        private void SetupSinglePlayerModals()
        {
            m_StartGameModal = Instantiate(m_SinglePlayerModal);
            m_StartGameModal.transform.SetParent(m_EndGameUiParent, false);
            m_StartGameModal.gameObject.SetActive(false);
            m_StartGameModal.Setup(null);
            m_StartGameModal.Show();
            //The loading screen must always be the last sibling
            LazyLoadLoadingPanel();
            m_LoadingScreen.transform.SetAsLastSibling();
        }

        /// <summary>
        /// Instantiates the end game modal.
        /// </summary>
        private void InstantiateEndGameModal(EndGameModal endGame)
        {
            if (endGame == null)
            {
                return;
            }

            if (m_EndGameModal != null)
            {
                Destroy(m_EndGameModal);
                m_EndGameModal = null;
            }

            m_EndGameModal = Instantiate<EndGameModal>(endGame);
            m_EndGameModal.transform.SetParent(m_EndGameUiParent, false);
            m_EndGameModal.gameObject.SetActive(false);
        }

        /// <summary>
        /// Add a tank from the lobby hook
        /// </summary>
        static public void AddTank(TankManager tank)
        {
            if (s_Tanks.IndexOf(tank) == -1)
            {
                s_Tanks.Add(tank);
                tank.MoveToSpawnLocation(SpawnManager.s_Instance.GetSpawnPointTransformByIndex(tank.playerNumber));
            }
        }

        /// <summary>
        /// Removes the tank.
        /// </summary>
        public void RemoveTank(TankManager tank)
        {
            Debug.Log("Removing tank");

            int tankIndex = s_Tanks.IndexOf(tank);

            if (tankIndex >= 0)
            {
                s_Tanks.RemoveAt(tankIndex);
                m_NumberOfPlayers--;
            }

            if (s_Tanks.Count == 1 && !m_GameIsFinished && !m_AllBailHandled)
            {
                HandleEveryoneBailed();
            }
        }
        #endregion

        /// <summary>
        /// Handles everyone bailed.
        /// </summary>
        public void HandleEveryoneBailed()
        {
            if (!TanksNetworkManager.s_IsServer)
            {
                return;
            }

            if (TanksNetworkManager.s_Instance.state != NetworkState.Inactive)
            {
                m_AllBailHandled = true;
                RpcDisplayEveryoneBailed();
                SetTimedTransition(GameState.EveryoneBailed, 3f);
            }
        }

        /// <summary>
        /// Rpcs the display everyone bailed.
        /// </summary>
        [ClientRpc]
        private void RpcDisplayEveryoneBailed()
        {
            SetMessageText("GAME OVER", "Everyone left the game");
        }


        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame(MenuPage returnPage)
        {
            for (int i = 0; i < s_Tanks.Count; i++)
            {
                TankManager tank = s_Tanks[i];
                if (tank != null)
                {
                    TanksNetworkPlayer player = tank.player;
                    if (player != null)
                    {
                        player.tank = null;
                    }

                    NetworkServer.Destroy(s_Tanks[i].gameObject);
                }
            }

            s_Tanks.Clear();
            m_NetManager.ReturnToMenu(returnPage);
        }

        /// <summary>
        /// Convenience function wrapping the announcer modal
        /// </summary>
        private void SetMessageText(string heading, string body)
        {
            
        }

       
        /// <summary>
        /// Gets the local player ID.
        /// </summary>
        public int GetLocalPlayerId()
        {
            return m_LocalPlayerNumber;
        }

        /// <summary>
        /// Unity message: Update , Runs only on server
        /// </summary>
        [ServerCallback]
        protected void Update()
        {
            HandleStateMachine();
        }

        /// <summary>
        /// Unity message: OnApplicationPause
        /// </summary>
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		protected void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				Time.timeScale = 1f;
				m_NetManager.Disconnect();
			}
		}
#endif

        #region STATE HANDLING

        /// <summary>
        /// Handles the state machine.
        /// </summary>
        protected void HandleStateMachine()
        {
            switch (m_State)
            {
                case GameState.StartUp:
                    StartUp();
                    break;
                case GameState.TimedTransition:
                    TimedTransition();
                    break;
                case GameState.Preplay:
                    Preplay();
                    break;
                case GameState.Playing:
                    Playing();
                    break;
                case GameState.RoundEnd:
                    RoundEnd();
                    break;
                case GameState.EndGame:
                    EndGame();
                    break;
                case GameState.EveryoneBailed:
                    EveryoneBailed();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// State up state function
        /// </summary>
        protected void StartUp()
        {
            if (m_GameSettings.isSinglePlayer)
            {
                LazyLoadLoadingPanel();
                m_LoadingScreen.StartFade(Fade.Out, 0.5f, SinglePlayerLoadedEvent);
                m_State = GameState.Inactive;
            }
            else
            {
                if (m_NetManager.AllPlayersReady())
                {
                    m_State = GameState.Preplay;
                    RpcInstantiateHudScore();
                    RpcGameStarted();

                    // Reset all ready states for players again
                    m_NetManager.ClearAllReadyStates();
                }
            }
        }

        protected void SinglePlayerLoadedEvent()
        {
            m_State = GameState.Preplay;
        }

        /// <summary>
        /// Time transition state function
        /// </summary>
        protected void TimedTransition()
        {
            m_TransitionTime -= Time.deltaTime;
            if (m_TransitionTime <= 0f)
            {
                m_State = m_NextState;
            }
        }

        /// <summary>
        /// Preplay state function
        /// </summary>
        protected void Preplay()
        {
            if (!m_CanStartGame)
                return;

            RoundStarting();
            
            //notify clients that the round is now started, they should allow player to move.
            RpcRoundPlaying();
        }

        /// <summary>
        /// Playing state function
        /// </summary>
        protected void Playing()
        {
            if (!m_HazardsActive)
            {
                m_HazardsActive = true;
            }
        }

        /// <summary>
        /// RoundEnd state function
        /// </summary>
        protected void RoundEnd()
        {
            RpcRoundEnding("Round End!!!");
            SetTimedTransition(GameState.Preplay, 2f);
        }

        /// <summary>
        /// EndGame state function
        /// </summary>
        protected void EndGame()
        {
            m_GameIsFinished = true;
            RpcGameEnd();
            if (m_GameSettings.isSinglePlayer)
            {
                AnalyticsHelper.SinglePlayerLevelCompleted(m_GameSettings.map.id, 3);
            }
            m_State = GameState.PostGame;
        }

        /// <summary>
        /// EveryoneBailed state function
        /// </summary>
        protected void EveryoneBailed()
        {
            //m_NetManager.DisconnectAndReturnToMenu();
            m_State = GameState.Inactive;
        }

        /// <summary>
        /// Sets the timed transition
        /// </summary>
        protected void SetTimedTransition(GameState nextState, float transitionTime)
        {
            this.m_NextState = nextState;
            this.m_TransitionTime = transitionTime;
            m_State = GameState.TimedTransition;
        }

        #endregion

        /// <summary>
        /// Starts the round
        /// </summary>
        private void RoundStarting()
        {
            m_HazardsActive = false;
            RpcRoundStarting(m_GameSettings.isSinglePlayer);
            SetTimedTransition(GameState.Playing, 2f);
        }

        /// <summary>
        /// Rpc for game started
        /// </summary>
        [ClientRpc]
        void RpcGameStarted()
        {
            if (PlayerDataManager.s_InstanceExists && PlayerDataManager.s_Instance.everyplayEnabled)
            {
                // Start recording!
                if (Everyplay.IsRecordingSupported())
                {
                    SetGameSettings();
                    Everyplay.StartRecording();
                    if (m_GameSettings.mode != null)
                    {
                        Everyplay.SetMetadata("game_mode", m_GameSettings.mode.modeName);
                    }
                    if (m_GameSettings.map != null)
                    {
                        Everyplay.SetMetadata("level", m_GameSettings.map.name);
                    }
                }
            }
        }

        /// <summary>
        /// Rpcs for round started
        /// </summary>
        /// <param name="isSinglePlayer">If set to <c>true</c> is single player</param>
        [ClientRpc]
        void RpcRoundStarting(bool isSinglePlayer)
        {
            // As soon as the round starts reset the tanks and make sure they can't move
            if (m_Round == 0)
            {
                ResetAllTanks();
            }

            DisableTankControl();

            InitHudAndLocalPlayer();
            m_Round++;

            if (isSinglePlayer)
            {
                EnableHUD();
            }
            else
            {
                UIAudioManager.s_Instance.PlayRoundStartSound();
                LazyLoadLoadingPanel();
                m_LoadingScreen.StartFadeOrFireEvent(Fade.Out, 0.5f, EnableHUD);
            }
        }

        /// <summary>
        /// Enables the HUD
        /// </summary>
        void EnableHUD()
        {
            HUDController.s_Instance.SetHudEnabled(true);
        }

        /// <summary>
        /// Rpc for Round Playing
        /// </summary>
        [ClientRpc]
        void RpcRoundPlaying()
        {
            // As soon as the round begins playing let the players control the tanks
            EnableTankControl();
            m_Announcer.Hide();
        }

        /// <summary>
        /// Rpc for Round Ending
        /// </summary>
        /// <param name="winnerText">Winner text</param>
        [ClientRpc]
        private void RpcRoundEnding(string winnerText)
        {
            HUDController.s_Instance.SetHudEnabled(false);
            DisableTankControl();
            m_EndScreen.StartFade(Fade.In, 2f, FadeOutEndRoundScreen);
            SetMessageText("ROUND END", winnerText);
        }

        /// <summary>
        /// Fades the out end round screen
        /// </summary>
        private void FadeOutEndRoundScreen()
        {
            m_EndScreen.StartFade(Fade.Out, 2f);
        }

        /// <summary>
        /// Rpc for Game End
        /// </summary>
        [ClientRpc]
        private void RpcGameEnd()
        {
            HUDController.s_Instance.SetHudEnabled(false);
            DisableTankControl();
            m_GameIsFinished = true;
            if (m_EndGameModal != null)
            {
                m_EndGameModal.Show();
            }

            if (Everyplay.IsRecording())
            {
                int tankIndex = s_Tanks.IndexOf(m_LocalPlayer);
                if (tankIndex >= 0)
                {
                    Everyplay.SetMetadata("final_position", tankIndex + 1);
                }
                Everyplay.StopRecording();
            }

            MainMenuUI.s_ReturnPage = MenuPage.Lobby;
            LazyLoadLoadingPanel();
            m_LoadingScreen.transform.SetAsLastSibling();
        }


        /// <summary>
        /// Handles the kill
        /// </summary>
        public void HandleKill(TankManager killed)
        {
            /*
            TankManager killer = GetTankByPlayerNumber(killed.health.lastDamagedByPlayerNumber);
            string explosionId = killed.health.lastDamagedByExplosionId;
            if (killer != null)
            {
                if (killer.playerNumber == killed.playerNumber)
                {
                    m_RulesProcessor.HandleSuicide(killer);
                    if (m_GameSettings.isSinglePlayer)
                    {
                        AnalyticsHelper.SinglePlayerSuicide(m_GameSettings.map.id, explosionId);
                    }
                    else
                    {
                        RpcAnnounceKill(m_KillLogPhrases.GetRandomSuicidePhrase(killer.playerName, killer.playerColor));
                        AnalyticsHelper.MultiplayerSuicide(m_GameSettings.map.id, m_GameSettings.mode.id, killer.playerTankType.id, explosionId);
                        HeatmapsHelper.MultiplayerSuicide(m_GameSettings.map.id, m_GameSettings.mode.id, killer.playerTankType.id, explosionId, killer.transform.position);
                    }

                }
                else
                {
                    m_RulesProcessor.HandleKillerScore(killer, killed);
                    if (!m_GameSettings.isSinglePlayer)
                    {
                        RpcAnnounceKill(m_KillLogPhrases.GetRandomKillPhrase(killer.playerName, killer.playerColor, killed.playerName, killed.playerColor));
                        AnalyticsHelper.MultiplayerTankKilled(m_GameSettings.map.id, m_GameSettings.mode.id, killed.playerTankType.id, killer.playerTankType.id, explosionId);
                        HeatmapsHelper.MultiplayerDeath(m_GameSettings.map.id, m_GameSettings.mode.id, killed.playerTankType.id, killer.playerTankType.id, explosionId, killed.transform.position);
                        HeatmapsHelper.MultiplayerKill(m_GameSettings.map.id, m_GameSettings.mode.id, killed.playerTankType.id, killer.playerTankType.id, explosionId, killer.transform.position);
                    }
                }
            }
            */
            s_Tanks.Sort(TankSort);
        }

        /// <summary>
        /// Sort for tanks list
        /// </summary>
        private int TankSort(TankManager tank1, TankManager tank2)
        {
            return tank2.score - tank1.score;
        }

        /// <summary>
        /// Rpc wrapper for InGameNotificationManager
        /// </summary>
        [ClientRpc]
        private void RpcAnnounceKill(string msg)
        {
            InGameNotificationManager.s_Instance.Notify(msg);
        }

        /// <summary>
        /// Gets the local player position
        /// </summary>
        public int GetLocalPlayerPosition()
        {
            return GetPlayerPosition(m_LocalPlayer);
        }

        /// <summary>
        /// Gets the player position
        /// </summary>
        public int GetPlayerPosition(TankManager tank)
        {
            if (!isServer)
            {
                s_Tanks.Sort(TankSort);
            }

            int index = s_Tanks.IndexOf(tank);
            return index + 1;
        }

        /// <summary>
        /// Resets all the tanks on the server
        /// </summary>
        [Server]
        public void ServerResetAllTanks()
        {
            Debug.Log("ServerResetAllTanks");
            SpawnManager.s_Instance.CleanupSpawnPoints();
            for (int i = 0; i < s_Tanks.Count; i++)
            {
                RespawnTank(s_Tanks[i].playerNumber, false);
            }
        }

        // This function is used to turn all the tanks back on and reset their positions and properties
        private void ResetAllTanks()
        {
            for (int i = 0; i < s_Tanks.Count; i++)
            {
                s_Tanks[i].Reset(SpawnManager.s_Instance.GetSpawnPointTransformByIndex(s_Tanks[i].playerNumber));
            }
        }

        #region Respawn

        /// <summary>
        /// Respawns the tank
        /// </summary>
        public void RespawnTank(int playerNumber, bool showLeaderboard = true)
        {
            RpcRespawnTank(playerNumber, showLeaderboard, SpawnManager.s_Instance.GetRandomEmptySpawnPointIndex());
        }

        /// <summary>
        /// Rpc for respawning the tank
        /// </summary>
        [ClientRpc]
        public void RpcRespawnTank(int playerNumber, bool showLeaderboard, int spawnPointIndex)
        {
            TankManager tank = GetTankByPlayerNumber(playerNumber);

            if (tank == null)
            {
                return;
            }

            LocalRespawn(tank, showLeaderboard, SpawnManager.s_Instance.GetSpawnPointTransformByIndex(spawnPointIndex));
        }

        /// <summary>
        /// Locals the respawn
        /// </summary>
        protected void LocalRespawn(TankManager tank, bool showLeaderboard, Transform respawnPoint)
        {
            
        }

        #endregion

        /// <summary>
        /// Clients the ready
        /// </summary>
        public void ClientReady()
        {
            m_NumberOfPlayers++;
        }

        /// <summary>
        /// Enables the tank control
        /// </summary>
        public void EnableTankControl()
        {
            for (int i = 0; i < s_Tanks.Count; i++)
            {
                s_Tanks[i].EnableControl();
            }
        }

        /// <summary>
        /// Disables the tank control
        /// </summary>
        public void DisableTankControl()
        {
            for (int i = 0; i < s_Tanks.Count; i++)
            {
                s_Tanks[i].DisableControl();
            }
        }

        //Iterates through all tankmanagers in the player list to determine which one represents the local player
        //Once the correct tank is found, pass its tankmanager reference to the HUD for init, and store its player number for reference by other scripts
        private void InitHudAndLocalPlayer()
        {
            for (int i = 0; i < s_Tanks.Count; i++)
            {
                if (s_Tanks[i].hasAuthority)
                {
                    m_LocalPlayer = s_Tanks[i];
                    HUDController.s_Instance.InitHudPlayer(s_Tanks[i]);
                    m_LocalPlayerNumber = s_Tanks[i].playerNumber;
                }
            }
        }

        //Instantiates the multiplayer score tracker on this client's HUD, and stores a reference to its script for later update
        [ClientRpc]
        void RpcInstantiateHudScore()
        {
            
        }

        //Called by the current Rules Manager to update the multiplayer score on all clients via RPC
        //These colours and scores are preformatted by the rules manager to suit the game type
        public void UpdateHudScore(Color[] teamColours, int[] scores)
        {
            if (!m_GameSettings.isSinglePlayer)
            {
                RpcUpdateHudScore(teamColours, scores);
            }
        }

        //Fired to update multiplayer score display on this client, using the reference cached during client HUD instantiation
        [ClientRpc]
        void RpcUpdateHudScore(Color[] teamColours, int[] scores)
        {
            if (scores.Length != teamColours.Length)
            {
                Debug.LogWarning("Score arrays different size");
                return;
            }

            UpdateScoreDictionary(teamColours, scores);
        }

        /// <summary>
        /// Updates the score dictionary
        /// </summary>
        void UpdateScoreDictionary(Color[] teamColours, int[] scores)
        {
            
        }


        /// <summary>
        /// Gets the tank by player number
        /// </summary>
        private TankManager GetTankByPlayerNumber(int playerNumber)
        {
            int length = s_Tanks.Count;
            for (int i = 0; i < length; i++)
            {
                TankManager tank = s_Tanks[i];
                if (tank.playerNumber == playerNumber)
                {
                    return tank;
                }
            }

            Debug.LogWarning("Could NOT find tank!");
            return null;
        }

        #region Networking Issues Listeners

        /// <summary>
        /// Convenience function for showing error panel
        /// </summary>
        private void ShowErrorPanel()
        {
            TimedModal.s_Instance.Show();
        }

        /// <summary>
        /// Raised by disconnect event
        /// </summary>
        private void OnDisconnect(NetworkConnection connection)
        {
            ShowErrorPanel();
        }

        /// <summary>
        /// Raised by error event
        /// </summary>
        private void OnError(NetworkConnection connection, int errorCode)
        {
            ShowErrorPanel();
        }

        /// <summary>
        /// Raised by drop event
        /// </summary>
        private void OnDrop()
        {
            ShowErrorPanel();
        }

        #endregion


        /// <summary>
        /// Lazy loads the loading panel
        /// </summary>
        public void LazyLoadLoadingPanel()
        {
            if (m_LoadingScreen != null)
            {
                return;
            }

            m_LoadingScreen = LoadingModal.s_Instance.fader;
        }
    }
}