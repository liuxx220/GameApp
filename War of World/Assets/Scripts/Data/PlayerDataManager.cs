using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Tanks.Utilities;







namespace Tanks.Data
{
	public class PlayerDataManager : Singleton<PlayerDataManager>
	{
		#region Fields

		//References to the datastore object, and the saver object responsible for serializing the datastore to file.
		[NonSerialized]
		private DataStore           m_Data;
		[SerializeField]
		protected AudioMixer        m_AudioMixer;
		#endregion


		#region Properties
		public int currency
		{
			get { return m_Data.currency;}
		}

		/// <summary>
		/// 选择的英雄
		/// </summary>
		public int selectedPlayer
		{
            get { return m_Data.selectedPlayer; }
            set { m_Data.selectedPlayer = value; }
		}

        /// <summary>
        /// 选择的英雄的装饰
        /// </summary>
		public int selectedDecoration
		{
			get { return m_Data.selectedDecoration; }
			set { m_Data.selectedDecoration = value; }
		}


		//The player's chosen name.
		public string playerName
		{
			get { return m_Data.playerName; }
			set { m_Data.playerName = value; }
		}

		//The master audio volume level for the game.
		public float masterVolume
		{
			get { return m_Data.settingsData.masterVolume; }
			set { m_Data.settingsData.masterVolume = value; }
		}

		//The music volume level for the game.
		public float musicVolume
		{
			get { return m_Data.settingsData.musicVolume; }
			set { m_Data.settingsData.musicVolume = value; }
		}

		//The sfx volume level for the game.
		public float sfxVolume
		{
			get { return m_Data.settingsData.sfxVolume; }
			set { m_Data.settingsData.sfxVolume = value; }
		}

		//The chosen on-screen thumbstick size for mobile platforms.
		public float thumbstickSize
		{
			get { return m_Data.settingsData.thumbstickSize; }
			set { m_Data.settingsData.thumbstickSize = value; }
		}

		//Whether the user has chosen to flip the thumbstick from bottom right to bottom left.
		public bool isLeftyMode
		{
			get { return m_Data.settingsData.isLeftyMode; }
			set { m_Data.settingsData.isLeftyMode = value; }
		}

		//Whether Everyplay will be active to record multiplayer games.
		public bool everyplayEnabled
		{
			get { return m_Data.settingsData.everyplayEnabled; }
			set { m_Data.settingsData.everyplayEnabled = value; }
		}

		//Volatile storage for tracking the last SP level the player selected over the app lifetime. NOT TO BE SAVED.
		private int m_LastLevelSelected = -1;

		public int lastLevelSelected
		{
			get { return m_LastLevelSelected; }
			set { m_LastLevelSelected = value; }
		}

		#endregion

		//Event fired whenever currency is altered.
		public event Action<int> onCurrencyChanged;

		protected override void Awake()
		{
			DontDestroyOnLoad(gameObject);
			base.Awake();

			// In the Unity editor, use a plain text saver so files can be inspected.
			m_Data = new DataStore();
		}

		private void Start()
		{
			if (m_AudioMixer != null)
			{
				m_AudioMixer.SetFloat("MusicVolume",    musicVolume);
				m_AudioMixer.SetFloat("SFXVolume",      sfxVolume);
				m_AudioMixer.SetFloat("MasterVolume",   masterVolume);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}


		//Adds currency to the player's currency pool
		public void AddCurrency(int currencyToAdd)
		{
			if (currencyToAdd >= 0)
			{
				m_Data.currency += currencyToAdd;
				if (onCurrencyChanged != null)
				{
					onCurrencyChanged(m_Data.currency);
				}
			}
			else
			{
				Debug.Log("<color=red>Attempting to add negative currency. Please use RemoveCurrency for this.</color>");
			}
		}

		//Removes currency from the player's currency pool, clamping to zero.
		public void RemoveCurrency(int currencyToRemove)
		{
			if (currencyToRemove >= 0)
			{
				m_Data.currency -= currencyToRemove;
				if (m_Data.currency < 0)
				{
					m_Data.currency = 0;
				}
				if (onCurrencyChanged != null)
				{
					onCurrencyChanged(m_Data.currency);
				}
			}
			else
			{
				Debug.Log("<color=red>Attempting to remove negative currency. Please use AddCurrency for this.</color>");
			}
		}

		//Returns whether a tank for a given index is unlocked.
		public bool IsTankUnlocked(int index)
		{
			if (m_Data.unlockedTanks.Length > index && index >= 0)
			{
				return m_Data.unlockedTanks[index];
			}

			return false;
		}

		//Allows a tank index's unlocked status to be set. Defaults to unlocking the tank.
		public void SetTankUnlocked(int index, bool setUnlocked = true)
		{
			if (m_Data.unlockedTanks.Length > index && index >= 0)
			{
				m_Data.unlockedTanks[index] = setUnlocked;
			}
			else
			{
				throw new IndexOutOfRangeException("Tank index invalid");
			}
		}

		//Returns whether a decoration for a given index is unlocked.
		public bool IsDecorationUnlocked(int index)
		{
			if (m_Data.decorations.Length > index && index >= 0)
			{
				return m_Data.decorations[index].unlocked;
			}

			return false;
		}

		//Allows a decoration index's unlocked status to be set. Defaults to unlocking the decoration.
		public void SetDecorationUnlocked(int index, bool setUnlocked = true)
		{
			if (m_Data.decorations.Length > index && index >= 0)
			{
				m_Data.decorations[index].unlocked = setUnlocked;
			}
			else
			{
				throw new IndexOutOfRangeException("Tank index invalid");
			}
		}

		//Returns whether a given material of a specific decoration has been unlocked.
		public bool IsColourUnlockedForDecoration(int decorationIndex, int colourIndex)
		{
			if (m_Data.decorations.Length > decorationIndex && decorationIndex >= 0)
			{
				return m_Data.decorations[decorationIndex].availableColours.Contains(colourIndex);
			}

			return false;
		}

		//Sets whether a given material of a specific decoration is unlocked. Defaults to unlocking the colour.
		public void SetDecorationColourUnlocked(int decorationIndex, int colourIndex, bool setUnlocked = true)
		{
			if (m_Data.decorations.Length > decorationIndex && decorationIndex >= 0)
			{
				if (IsColourUnlockedForDecoration(decorationIndex, colourIndex) != setUnlocked)
				{
					if (setUnlocked)
					{
						m_Data.decorations[decorationIndex].availableColours.Add(colourIndex);
					}
					else
					{
						m_Data.decorations[decorationIndex].availableColours.Remove(colourIndex);
					}
				}
			}
		}


		//Returns the number of materials that have been unlocked for a given decoration.
		public int GetNumberOfUnlockedColours(int decorationIndex)
		{
			if (m_Data.decorations.Length > decorationIndex && decorationIndex >= 0)
			{
				return m_Data.decorations[decorationIndex].availableColours.Count;
			}

			return 0;
		}

		//Returns the number of materials that remain locked for a given decoration.
		public int GetNumberOfLockedColours(int decorationIndex)
		{
			return 0;
		}

		//Returns data for the latest level entered.
		public LevelData GetLevelData(string id)
		{
			return m_Data.GetLevelData(id);
		}

		//Returns the total number of medals that the player has earned across all SP missions for progression purposes.
		public int GetTotalMedalCount()
		{
			List<LevelData> allLevelData = m_Data.GetAllLevelData();

			int totalMedalCount = 0;

			for (int i = 0; i < allLevelData.Count; i++)
			{
				List<bool> objectives = allLevelData[i].objectivesAchieved;
				if (objectives != null)
				{
					for (int j = 0; j < objectives.Count; j++)
					{
						if (objectives[j] == true)
						{
							totalMedalCount++;
						}
					}
				}
			}
			return totalMedalCount;
		}

		//Returns whether a given decoration still has any locked colours.
		public bool DecorationHasLockedColours(int decorationIndex)
		{
			return (GetNumberOfLockedColours(decorationIndex) > 0);
		}

		//Sets whether a multiplayer map is unlocked or not. Defaults to unlocking.
		//NOTE: No longer visible in-game. This existed for the purchase of MP maps with in-game currency.
		public void SetMapUnlocked(string mapId, bool setUnlocked = true)
		{
			if (!m_Data.unlockedMultiplayerMaps.Contains(mapId) && setUnlocked)
			{
				m_Data.unlockedMultiplayerMaps.Add(mapId);
			}
			else if (!setUnlocked)
			{
				m_Data.unlockedMultiplayerMaps.Remove(mapId);
			}
		}

		//Returns whether the multiplayer map with the given ID string has been unlocked.
		//NOTE: No longer visible in-game. This existed for the purchase of MP maps with in-game currency.
		public bool IsMapUnlocked(string mapId)
		{
			return m_Data.unlockedMultiplayerMaps.Contains(mapId);
		}

		//Saves the details of an item temporarily unlocked using the Daily Advert Unlock feature.
		//NOTE: No longer visible in-game. Used in conjunction with disabled ad functionality.
		public void SaveTempUnlockData(string tempUnlockId, int tempUnlockColour, DateTime unlockDate)
		{
			m_Data.tempUnlockId     = tempUnlockId;
			m_Data.tempUnlockColour = tempUnlockColour;
			m_Data.tempUnlockDate   = unlockDate.ToFileTime();
		}

		//Returns the time that the last unlock was made.
		//NOTE: No longer visible in-game. Used in conjunction with disabled ad functionality.
		public DateTime LoadUnlockTime()
		{
			return DateTime.FromFileTime(m_Data.tempUnlockDate);
		}

		//Returns the ID of the last item unlocked via a Daily Ad.
		//NOTE: No longer visible in-game. Used in conjunction with disabled ad functionality.
		public string GetLastUnlockId()
		{
			return m_Data.tempUnlockId;
		}

		//Returns the last decoration colour unlocked via a daily ad.
		//NOTE: No longer visible in-game. Used in conjunction with disabled ad functionality.
		public int GetLastUnlockColour()
		{
			return m_Data.tempUnlockColour;
		}
	}
}
