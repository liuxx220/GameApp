﻿using UnityEngine;
using UnityEngine.UI;
using Tanks.Data;

namespace Tanks.UI
{
	//Base class for Tank selection
	public class TankSelector : MonoBehaviour
	{
		[SerializeField]
		protected bool m_FilterLockedItems = false;

		[SerializeField]
		protected DiscretePointSlider m_SpeedSlider;

		[SerializeField]
		protected DiscretePointSlider m_RefireRateSlider;

		[SerializeField]
		protected DiscretePointSlider m_ArmorSlider;

		[SerializeField]
		protected Text m_TankName;

		[SerializeField]
		protected TankDragPreview m_TankDragPreview;

		[SerializeField]
		protected GameObject m_NextButton, m_PreviousButton;

		[SerializeField]
		protected bool m_Capitalize = false;

		protected int m_CurrentIndex = 0;

		protected int m_CurrentDecoration = -1;

		protected int m_CurrentDecorationMaterial = 0;

		protected virtual void OnEnable()
		{
			ResetSelections();

			UpdateTankStats(m_CurrentIndex);

			if (m_FilterLockedItems)
			{
				int length = TankLibrary.s_Instance.GetNumberOfUnlockedTanks();
				bool isActive = length > 1;
				SetActivationOfButton(m_NextButton, isActive);
				SetActivationOfButton(m_PreviousButton, isActive);
			}
			else
			{
				SetActivationOfButton(m_NextButton, true);
				SetActivationOfButton(m_PreviousButton, true);
			}
		}
		
		//Handles tank selection
		protected virtual void ResetSelections()
		{
			PlayerDataManager dataManager = PlayerDataManager.s_Instance;
			if (dataManager != null)
			{
				m_CurrentIndex = dataManager.selectedPlayer;
				m_CurrentDecoration = 0;
				m_CurrentDecorationMaterial = 0;
			}
		}

		//Changes attached decoration
		public void ChangeDecoration(int decorationIndex)
		{
			//Flip out the current decoration index
			m_CurrentDecoration = decorationIndex;
		}

		//Changes attached decoration colour
		public void ChangeCurrentDecorationColour(int decorationColour)
		{

		}


		//Updates the UI
		protected virtual void UpdateTankStats(int index)
		{
			TankTypeDefinition tankData = TankLibrary.s_Instance.GetTankDataForIndex(index);

			if (m_Capitalize)
			{
				m_TankName.text = tankData.name.ToUpperInvariant();
			}
			else
			{
				m_TankName.text = tankData.name;
			}

			if (m_SpeedSlider != null)
			{
				m_SpeedSlider.UpdateValue(tankData.speedRating);
			}

			if (m_RefireRateSlider != null)
			{
				m_RefireRateSlider.UpdateValue(tankData.refireRating);
			}
			if (m_ArmorSlider != null)
			{
				m_ArmorSlider.UpdateValue(tankData.armourRating);
			}
		}

		//Convenience helper for enabling previous/next buttons
		protected void SetActivationOfButton(GameObject button, bool isActive)
		{
			if (button == null)
			{
				return;
			}

			button.SetActive(isActive);
		}

		//Allows the list to wrap
		private int Wrap(int indexToWrap, int arraySize)
		{
			if (indexToWrap < 0)
			{
				indexToWrap = arraySize - 1;
			}
			else if (indexToWrap >= arraySize)
			{
				indexToWrap = 0;
			}

			return indexToWrap;
		}
	}
}
