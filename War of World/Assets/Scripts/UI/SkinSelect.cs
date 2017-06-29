using UnityEngine;
using Tanks.Data;
using UnityEngine.UI;

namespace Tanks.UI
{
	//Handles selecting a decoration
	public class SkinSelect : MonoBehaviour
	{
		[SerializeField]
		protected Skin m_SkinPrefab;

		[SerializeField]
		protected RouletteModal m_RouletteModal;

		[SerializeField]
		protected Modal m_SelectionModal;

		[SerializeField]
		protected Button m_RouletteButton;

		[SerializeField]
		protected LobbyCustomization m_Customization;

		public RouletteModal rouletteModal
		{
			get
			{
				return m_RouletteModal;
			}
		}

		public Modal selectionModal
		{
			get
			{
				return m_SelectionModal;
			}
		}

		public LobbyCustomization customization
		{
			get
			{
				return m_Customization;
			}
		}

		public void Clear()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}

		protected virtual void OnEnable()
		{
			RegenerateItems();
		}

		protected virtual void OnDisable()
		{
			Clear();
		}
		
		//Clears current UI items and adds new UI items
        public void RegenerateItems()
        {

        }
	}
}
