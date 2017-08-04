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
		/// <summary>
		/// Ñ¡ÔñµÄÓ¢ÐÛ
		/// </summary>
		public int selectedPlayer
		{
            get { return m_Data.selectedPlayer; }
            set { m_Data.selectedPlayer = value; }
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

		//Returns whether a tank for a given index is unlocked.
		public bool IsTankUnlocked(int index)
		{
			if (m_Data.unlockedTanks.Length > index && index >= 0)
			{
				return m_Data.unlockedTanks[index];
			}

			return false;
		}

		//Returns data for the latest level entered.
		public LevelData GetLevelData(string id)
		{
			return m_Data.GetLevelData(id);
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
