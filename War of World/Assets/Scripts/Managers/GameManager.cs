using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tanks.TankControllers;
using Tanks.Data;
using Tanks.Rules;
using Tanks.UI;
using Tanks.Map;
using Tanks.Analytics;
using Tanks.Rules.SinglePlayer;
using Tanks.Networking;
using Tanks.Audio;
using System;
using TanksNetworkManager = Tanks.Networking.NetworkManager;
using TanksNetworkPlayer = Tanks.Networking.NetworkPlayer;








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
    public class GameManager : MonoBehaviour
    {

        static public GameManager           s_Instance;
        static public List<TankManager>     s_Tanks = new List<TankManager>();

        //This is the game object that end game modal is instantiated under
        protected Transform                 m_EndGameUiParent;

        //Caching the persistent singleton of game settings
        protected GameSettings              m_GameSettings;

        //Current game state - starts inactive
        protected GameState                 m_State = GameState.Inactive;

        //Transition state variables
        private float                       m_TransitionTime = 0f;
        private GameState                   m_NextState;

        //Various UI references to hide the screen between rounds.
        private FadingGroup                 m_LoadingScreen;

        //if the tanks are active
        private bool                        m_HazardsActive;

        //The rules processor being used
        private RulesProcessor              m_RulesProcessor;

        public RulesProcessor               rulesProcessor
        {
            get { return m_RulesProcessor; }
        }

        //The end game modal that is actually used
        protected EndGameModal              m_EndGameModal;

        //Number of players in game
        private int                         m_NumberOfPlayers = 0;


        //The modal displayed at the beginning of the game
        protected StartGameModal            m_StartGameModal;

       
        /// <summary>
        /// Unity message: Awake
        /// </summary>
        private void Awake()
        {
            s_Instance = this;
            Start();
        }

        /// <summary>
        /// Unity message: OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            s_Tanks.Clear();
        }


        /// <summary>
        /// Unity message: Start
        /// Only called on server
        /// </summary>
        void Start()
        {
            //Set the state to startup
            m_State         = GameState.StartUp;
            m_GameSettings  = GameSettings.s_Instance;

            /*
             * 出示华战场规则数据
            m_RulesProcessor = Instantiate<RulesProcessor>(m_GameSettings.mode.rulesProcessor);
            m_RulesProcessor.SetGameManager(this);
			*/
            if (m_GameSettings.isSinglePlayer)
            {
                AnalyticsHelper.SinglePlayerLevelStarted(m_GameSettings.map.id);
                SetupSinglePlayerModals();
            }
        }

        /// <summary>
        /// Setups the single player modals.
        /// </summary>
        private void SetupSinglePlayerModals()
        {
            //Cache the offline rules processor
            OfflineRulesProcessor offlineRulesProcessor = m_RulesProcessor as OfflineRulesProcessor;
            if (m_EndGameModal != null)
            {
                m_EndGameModal.SetRulesProcessor(m_RulesProcessor);
            }

            //Handle start game modal	
            if (offlineRulesProcessor.startGameModal != null)
            {
                m_StartGameModal = Instantiate(offlineRulesProcessor.startGameModal);
                m_StartGameModal.transform.SetParent(m_EndGameUiParent, false);
                m_StartGameModal.gameObject.SetActive(false);
                m_StartGameModal.Setup(offlineRulesProcessor);
                m_StartGameModal.Show();
                m_LoadingScreen.transform.SetAsLastSibling();
            }
        }

        /// <summary>
        /// Instantiates the end game modal.
        /// </summary>
        /// <param name="endGame">End game.</param>
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
        /// <param name="tank">Tank.</param>
        public void RemoveTank(TankManager tank)
        {
            int tankIndex = s_Tanks.IndexOf(tank);
            if (tankIndex >= 0)
            {
                s_Tanks.RemoveAt(tankIndex);
                if (m_RulesProcessor != null)
                {
                    m_RulesProcessor.TankDisconnected(tank);
                }

                m_NumberOfPlayers--;
            }
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        /// <param name="returnPage">Return page.</param>
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

                    GameObject.Destroy(s_Tanks[i].gameObject);
                }
            }

            s_Tanks.Clear();
        }


        /// <summary>
        /// 更新战场的状态机
        /// </summary>
        protected void Update()
        {
            HandleStateMachine();
        }


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
                m_LoadingScreen.StartFade(Fade.Out, 0.5f, SinglePlayerLoadedEvent);
                m_State = GameState.Inactive;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
            if (!m_RulesProcessor.canStartGame)
            {
                return;
            }

            RoundStarting();
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

            if (m_RulesProcessor.IsEndOfRound())
            {
                m_State = GameState.RoundEnd;
            }
        }

        /// <summary>
        /// RoundEnd state function
        /// </summary>
        protected void RoundEnd()
        {
            m_RulesProcessor.HandleRoundEnd();
            if (m_RulesProcessor.matchOver)
            {
                SetTimedTransition(GameState.EndGame, 1f);
            }
            else
            {
                SetTimedTransition(GameState.Preplay, 2f);
            }
        }


        /// <summary>
        /// EndGame state function
        /// </summary>
        protected void EndGame()
        {
            if (m_GameSettings.isSinglePlayer)
            {
                if (m_RulesProcessor.hasWinner)
                {
                    AnalyticsHelper.SinglePlayerLevelCompleted(m_GameSettings.map.id, 3);
                }
                else
                {
                    AnalyticsHelper.SinglePlayerLevelFailed(m_GameSettings.map.id);
                }
            }
           
            m_State = GameState.PostGame;
        }

        /// <summary>
        /// EveryoneBailed state function
        /// </summary>
        protected void EveryoneBailed()
        {
            //ReturnToMenu();
            m_State = GameState.Inactive;
        }

        /// <summary>
        /// s设置下一个要切换的状态和过度时间
        /// </summary>
        protected void SetTimedTransition(GameState nextState, float transitionTime)
        {
            this.m_NextState = nextState;
            this.m_TransitionTime = transitionTime;
            m_State = GameState.TimedTransition;
        }

        /// <summary>
        /// 开始新一轮回合战斗
        /// </summary>
        private void RoundStarting()
        {
            //we notify all clients that the round is starting
            m_RulesProcessor.StartRound();
            m_HazardsActive = false;
            SetTimedTransition(GameState.Playing, 2f);
        }
    }
}