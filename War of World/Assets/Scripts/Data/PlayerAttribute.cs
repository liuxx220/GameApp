using System;
using Tanks.Utilities;







namespace Tanks.Data
{
	public enum ATTPLAYER
    {
        ATT_MAXHP       = 0,
        ATT_HP          = 1,
        ATT_MAXMP       = 2,
        ATT_MP          = 3,
        ATT_SPEED       = 4,
        ATT_DAMGE       = 5,

        ATT_NUM
    }


    /// <summary>
    /// Player 属性对象
    /// </summary>
    public class PlayerAttribute
	{
        /// <summary>
        /// 基础属性值
        /// </summary>
        private int[]       m_AttBase;
 
        /// <summary>
        /// 百分比属性值
        /// </summary>
        private int[]       m_AttPct;

        /// <summary>
        /// 数值影响属性值
        /// </summary>
        private int[]       m_AttValue;

        /// <summary>
        /// 当前属性值
        /// </summary>
        private int[]       m_Att;


		private bool m_Initialized = false;
        public void Initialized()
		{
            int nArraySize = (int)ATTPLAYER.ATT_NUM;

            m_AttBase      = new int[nArraySize];
            m_AttPct       = new int[nArraySize];
            m_AttValue     = new int[nArraySize];
            m_Att          = new int[nArraySize];
			m_Initialized  = true;
		}


		//Returns whether init is complete (ie, if we've loaded last unlock info from player data).
		public bool IsInitialized()
		{
			return m_Initialized;
		}

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变基础属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void ChangeAttBase(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变数值影响属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void ChangeAttValue(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变百分比影响属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void ChangeAttPct(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变当前属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void ChangeAtt( ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变基础属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void GetAttBase(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变数值影响属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void GetAttValue(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变百分比影响属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void GetAttPct(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变当前属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void GetAtt(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变基础属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void SetAttBase(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变数值影响属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void SetAttValue(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变百分比影响属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void SetAttPct(ATTPLAYER eType, int nChangeValue)
        {

        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变当前属性值
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void SetAtt(ATTPLAYER eType, int nChangeValue)
        {

        }
	}
}
