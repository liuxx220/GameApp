using System;
using UnityEngine;
using UnityEngine.Events;
using Tanks.Utilities;
using Tanks.Networking;
using TanksNetworkPlayer = Tanks.Networking.NetworkPlayer;







namespace Tanks.UI
{
	//Page in menu to return to
	public enum MenuPage
	{
		Home,
		SinglePlayer,
		Lobby,
		CustomizationPage
	}

	//Class that handles main menu UI and transitions
	public class MainMenuUI : Singleton<MainMenuUI>
	{
		#region Static config

		public static MenuPage s_ReturnPage = MenuPage.Home;

		#endregion

		#region Fields

		[SerializeField]
		protected CanvasGroup       m_DefaultPanel;
		[SerializeField]
		protected CanvasGroup       m_SinglePlayerPanel;
        [SerializeField]
        protected CanvasGroup       m_CreateGamePanel;
        [SerializeField]
        protected CanvasGroup       m_ServerListPanel;
        [SerializeField]
        protected CanvasGroup       m_LobbyPanel;
		[SerializeField]
		protected LobbyInfoPanel    m_InfoPanel;
		[SerializeField]
		protected LobbyPlayerList   m_PlayerList;
		[SerializeField]
		protected SettingsModal     m_SettingsModal;
		[SerializeField]
		protected GameObject        m_QuitButton;
		private CanvasGroup         m_CurrentPanel;

		private Action              m_WaitTask;
		private bool                m_ReadyToFireTask;

		#endregion

		public LobbyPlayerList playerList
		{
			get
			{
				return m_PlayerList;
			}
		}


		#region Methods

		protected virtual void Update()
		{
			if (m_ReadyToFireTask)
			{
				bool canFire = false;

				LoadingModal modal = LoadingModal.s_Instance;
				if (modal != null && modal.readyToTransition)
				{
					modal.FadeOut();
					canFire = true;
				}
				else if (modal == null)
				{
					canFire = true;
				}

				if (canFire)
				{
					if (m_WaitTask != null)
					{
						m_WaitTask();
						m_WaitTask = null;
					}

					m_ReadyToFireTask = false;
				}
			}
		}

		protected virtual void Start()
		{
			LoadingModal modal = LoadingModal.s_Instance;
			if (modal != null)
			{
				modal.FadeOut();
			}

            switch( s_ReturnPage )
            {
                case MenuPage.Home:
                    ShowDefaultPanel();
                    break;

                case MenuPage.SinglePlayer:
                    ShowSingleplayerPanel();
                    break;

            }
		}
		
		//Convenience function for showing panels
		public void ShowPanel(CanvasGroup newPanel)
		{
			if (m_CurrentPanel != null)
			{
				m_CurrentPanel.gameObject.SetActive(false);
			}

			m_CurrentPanel = newPanel;
			if (m_CurrentPanel != null)
			{
				m_CurrentPanel.gameObject.SetActive(true);
			}
		}

        public void ShowConnectingModal(bool reconnectMatchmakingClient)
        {
            ShowInfoPopup("Connecting...", () =>
            {
                if (NetworkManager.s_Instance != null )
                {
                    if (reconnectMatchmakingClient)
                    {
                        NetworkManager.s_Instance.Disconnect();
                        NetworkManager.s_Instance.StartMatchingmakingClient();
                    }
                    else
                    {
                        NetworkManager.s_Instance.Disconnect();
                    }
                }
            });
        }

		public void ShowDefaultPanel()
		{
			ShowPanel(m_DefaultPanel);
		}

        public void ShowLobbyPanel()
        {
            ShowPanel(m_LobbyPanel);
        }

		/// <summary>
		/// Shows the info popup with a callback
		/// </summary>
		public void ShowInfoPopup(string label, UnityAction callback)
		{
			if (m_InfoPanel != null)
			{
				m_InfoPanel.Display(label, callback, true);
			}
		}

		public void ShowInfoPopup(string label)
		{
			if (m_InfoPanel != null)
			{
				m_InfoPanel.Display(label, null, false);
			}
		}

		public void HideInfoPopup()
		{
			if (m_InfoPanel != null)
			{
				m_InfoPanel.gameObject.SetActive(false);
			}
		}

        public void ShowServerListPanel()
        {
            ShowPanel(m_ServerListPanel);
        }

		//Event listener
		private void OnClientStopped()
		{
            NetworkManager netManager = NetworkManager.s_Instance;
            netManager.clientStopped  -= OnClientStopped;
            m_ReadyToFireTask         = true;
		}

		private void ShowSingleplayerPanel()
		{
			ShowPanel(m_SinglePlayerPanel);
		}

		#endregion


		#region Button events

        public void OnSingleplayerClicked()
        {
            ShowSingleplayerPanel();
        }


        private void GoToCreateGamePanel()
        {
            ShowPanel(m_CreateGamePanel);
        }


        public void OnLoadGameClicked()
        {
            DoIfNetworkReady(GoToCreateGamePanel);
        }

        public void OnSaveGameClicked()
        {
            ShowServerListPanel();
            NetworkManager.s_Instance.StartMatchingmakingClient();
        }

        public void OnShowSettingsModal()
        {
            m_SettingsModal.Show();
        }


		public void OnQuitGameClicked()
		{
			Application.Quit();
		}

		#endregion

        /// -------------------------------------------------------------------------------------------
        /// <summary>
        /// Wait for network to disconnect before performing an action
        /// </summary>
        /// -------------------------------------------------------------------------------------------
        public void DoIfNetworkReady( Action task )
        {
            if( task == null )
            {
                throw new ArgumentNullException("task");
            }

            NetworkManager netManager = NetworkManager.s_Instance;
            if (netManager.isNetworkActive)
            {
                m_WaitTask = task;

                LoadingModal modal = LoadingModal.s_Instance;
                if (modal != null)
                {
                    modal.FadeIn();
                }

                m_ReadyToFireTask = false;
                netManager.clientStopped += OnClientStopped;
            }
            else
            {
                task();
            }
        }
	}
}