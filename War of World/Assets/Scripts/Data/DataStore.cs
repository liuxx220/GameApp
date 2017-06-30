using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;








namespace Tanks.Data
{
	/// <summary>
	/// Decoration data serializable class
	/// </summary>
	[Serializable]
	public class DecorationData
	{
		public bool      unlocked;
		public List<int> availableColours;
		public int       selectedColourIndex;

		public DecorationData()
		{
			availableColours = new List<int>();
		}
	}

	/// <summary>
	/// Level data serializable class
	/// </summary>
	[Serializable]
	public class LevelData
	{
		public string       id;

		public List<bool>   objectivesAchieved;

		public LevelData(string id)
		{
			this.id = id;
			objectivesAchieved = new List<bool>();
		}
	}

	/// <summary>
	/// Settings data serializable class
	/// </summary>
	[Serializable]
	public class SettingsData
	{
		public float musicVolume        = 0.0f;
		public float sfxVolume          = 0.0f;
		public float masterVolume       = 0.0f;
		public float thumbstickSize     = 0.6f;
		public bool isLeftyMode         = false;
		public bool everyplayEnabled    = false;
	}

	/// <summary>
	/// Data store implements serialization callback receiver to tie into the serialization events
	/// </summary>
	[Serializable]
	public class DataStore : ISerializationCallbackReceiver
	{

		public int          selectedPlayer = 0;
		public int          selectedDecoration;
		public int          currency;
		public bool[]       unlockedTanks;
		public string       playerName;
		public DecorationData[] decorations;
		public List<LevelData>  levels;
		public SettingsData settingsData;
		public List<string> unlockedMultiplayerMaps;
		public long         tempUnlockDate;
		public string       tempUnlockId;
		public int          tempUnlockColour;

		private Dictionary<string, LevelData> m_LevelsDictionary;

		public DataStore()
		{
            unlockedMultiplayerMaps = new List<string>();
            m_LevelsDictionary  = new Dictionary<string, LevelData>();
            levels              = new List<LevelData>();
            settingsData        = new SettingsData();
            unlockedTanks       = new bool[3];
		}

		/// <summary>
		/// Gets the level data.
		/// </summary>
		/// <returns>The level data.</returns>
		/// <param name="id">Identifier.</param>
		public LevelData GetLevelData(string id)
		{
			LevelData result;
			if (!m_LevelsDictionary.TryGetValue(id, out result))
			{
				LevelData newLevelData = new LevelData(id);
				m_LevelsDictionary.Add(id, newLevelData);
				return newLevelData;
			}
			
			return result;
		}

		/// <summary>
		/// Gets all level data.
		/// </summary>
		/// <returns>The all level data.</returns>
		public List<LevelData> GetAllLevelData()
		{
			return m_LevelsDictionary.Values.ToList();
		}

		/// <summary>
		/// Serialization implementation from ISerializationCallbackReceiver
		/// </summary>
		public void OnBeforeSerialize()
		{
			LevelDataSerialize();
		}

		/// <summary>
		/// Deserialization implementation from ISerializationCallbackReceiver
		/// </summary>
		public void OnAfterDeserialize()
		{
			LevelDataDeserialize();
		}

		/// <summary>
		/// Converts dictionary to list by getting the values for serialization
		/// </summary>
		private void LevelDataSerialize()
		{
			levels = m_LevelsDictionary.Values.ToList();
		}

		/// <summary>
		/// Converts list to dictionary on deserialization for optimal accessing
		/// </summary>
		private void LevelDataDeserialize()
		{
			m_LevelsDictionary = levels.ToDictionary(l => l.id);
			levels.Clear();
		}
	}
}