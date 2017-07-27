using System;
using UnityEngine;
using Tanks.Map;
using Tanks.Rules;
using Tanks.Networking;
using Tanks.Utilities;
using Tanks.Explosions;






namespace Tanks
{
	/// <summary>
	/// Persistent singleton for handling the game settings
	/// </summary>
	public class GameSettings : PersistentSingleton<GameSettings>
	{
		public event Action<MapDetails>     mapChanged;
		public event Action<ModeDetails>    modeChanged;

        /// <summary>
        /// 联机关卡数据
        /// </summary>
		[SerializeField]
		protected MapList                   m_MapList;

        /// <summary>
        /// 单机关卡数据
        /// </summary>
		[SerializeField]
		protected SinglePlayerMapList       m_SinglePlayerMapList;

        /// <summary>
        /// 关卡战场赢规则数据
        /// </summary>
		[SerializeField]
		protected ModeList                  m_ModeList;

        /// <summary>
        /// 武器配置数据
        /// </summary>
        [SerializeField]
        protected WeaponList                m_WeaponList;

		public MapDetails map
		{
			get;
			private set;
		}

		public int mapIndex
		{
			get;
			private set;
		}

		public ModeDetails mode
		{
			get;
			private set;
		}

		public int modeIndex
		{
			get;
			private set;
		}

		public int scoreTarget
		{
			get;
			private set;
		}


        public PLAYGAMEMODEL        m_PlayerGameModel = PLAYGAMEMODEL.PLAYGAME_TPS;
		public bool isSinglePlayer
		{
			get { return NetworkManager.s_Instance.isSinglePlayer; }
		}

        private void Start()
        {
            AssetManager.Get().Init();
        }

		/// <summary>
		/// Sets the index of the map.
		/// </summary>
		/// <param name="index">Index.</param>
		public void SetMapIndex(int index)
		{
			map         = m_MapList[index];
			mapIndex    = index;

			if (mapChanged != null)
			{
				mapChanged(map);
			}
		}

		/// <summary>
		/// Sets the index of the mode.
		/// </summary>
		/// <param name="index">Index.</param>
		public void SetModeIndex(int index)
		{
			SetMode(m_ModeList[index], index);
		}


        /// ---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sets up single player
        /// </summary>
        /// ---------------------------------------------------------------------------------------------------------------
		public void SetupSinglePlayer(int mapIndex, ModeDetails modeDetails)
		{
			this.map = m_SinglePlayerMapList[mapIndex];
			this.mapIndex = mapIndex;
			if (mapChanged != null)
			{
				mapChanged(map);
			}

			SetMode(modeDetails, -1);
		}

        /// ---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sets up single player
        /// </summary>
        /// ---------------------------------------------------------------------------------------------------------------
        public void SetupSinglePlayer(MapDetails map, ModeDetails modeDetails)
		{
			this.map = map;
			this.mapIndex = -1;
			if (mapChanged != null)
			{
				mapChanged(map);
			}

			SetMode(modeDetails, -1);
		}
        
        /// ---------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Sets the mode.
		/// </summary>
        /// ---------------------------------------------------------------------------------------------------------------
		private void SetMode(ModeDetails mode, int modeIndex)
		{
			this.mode = mode;
			this.modeIndex = modeIndex;
			if (modeChanged != null)
			{
				modeChanged(mode);
			}

            if (mode.rulesProcessor != null )
			    scoreTarget = mode.rulesProcessor.scoreTarget;
		}


        /// ---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 得到某个武器的配置信息
        /// </summary>
        /// ---------------------------------------------------------------------------------------------------------------
        public TankWeaponDefinition GetWeaponbyID( int nWeaponID )
        {
            for (int i = 0; i < m_WeaponList.Count; i++ )
            {
                if (m_WeaponList[i].m_ID == nWeaponID)
                    return m_WeaponList[i];
            }
            return null;
        }
	}
}