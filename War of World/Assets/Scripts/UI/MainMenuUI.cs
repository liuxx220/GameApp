using System;
using UnityEngine;
using UnityEngine.Events;
using Tanks.Utilities;









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
		protected CanvasGroup m_DefaultPanel;
		[SerializeField]
		protected CanvasGroup m_SinglePlayerPanel;
		[SerializeField]
		protected LobbyInfoPanel m_InfoPanel;
		[SerializeField]
		protected LobbyPlayerList m_PlayerList;
		[SerializeField]
		protected SettingsModal m_SettingsModal;
		[SerializeField]
		protected GameObject m_QuitButton;
		private CanvasGroup m_CurrentPanel;

		private Action m_WaitTask;
		private bool m_ReadyToFireTask;

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

		public void ShowDefaultPanel()
		{
			ShowPanel(m_DefaultPanel);
		}

		/// <summary>
		/// Shows the info popup with a callback
		/// </summary>
		/// <param name="label">Label.</param>
		/// <param name="callback">Callback.</param>
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

		//Event listener
		private void OnClientStopped()
		{
			m_ReadyToFireTask = true;
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

        public void OnLoadGameClicked()
        {
            
        }

        public void OnSaveGameClicked()
        {

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
	}
}