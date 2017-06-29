using UnityEngine;
using Tanks.Data;

namespace Tanks.UI
{
	//Handles colour select for skin
	public class SkinColourSelector : MonoBehaviour
	{
		[SerializeField]
		protected GameObject m_ColourSelectButton;

		[SerializeField]
		protected LobbyCustomization m_CustomizationScreen;

		[SerializeField]
		protected RouletteModal m_RouletteModal;

		[SerializeField]
		protected Transform m_ContentChild;

		//Check available colours
		protected virtual void OnEnable()
		{
			RefreshAvailableColours();
		}

		protected virtual void OnDisable()
		{
			Clear();
		}

		public void Clear()
		{
			for (int i = 0; i < m_ContentChild.childCount; i++)
			{
				Destroy(m_ContentChild.GetChild(i).gameObject);
			}
		}

		//Creates the colour buttons for the available options - clears current UI elements
		public void RefreshAvailableColours()
		{
			
		}
		
		//Handles colour change
		public void ChangeColourIndex(int newIndex)
		{
			m_CustomizationScreen.ChangeCurrentDecorationColour(newIndex);
		}

		//Opens roulette modal
		public void OpenRoulette(int skinColourIndex)
		{
			m_RouletteModal.Show(m_CustomizationScreen.GetCurrentPreviewDecoration(), skinColourIndex);
		}
	}
}
