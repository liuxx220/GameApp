﻿using TsiU;
using System.Collections.Generic;

namespace AIToolkit
{
    class AIEnityManager : TSingleton<AIEnityManager>
    {
        //--------------------------------------------------------------------------------------
        public delegate int AIEntityUpdater(AIEntity entity, float gameTime, float deltaTime);
        //--------------------------------------------------------------------------------------
        private List<AIEntity> _entites;

        public AIEnityManager()
        {
            _entites = new List<AIEntity>();
        }
        public void AddEntity(AIEntity e)
        {
            _entites.Add(e);
        }
		public void RemoveEntity(AIEntity e)
		{
			_entites.Remove (e);
		}
        public void IteratorDo(AIEntityUpdater updater, float gameTime, float deltaTime)
        {
            foreach (AIEntity e in _entites)
            {
                updater(e, gameTime, deltaTime);
            }
        }
    }
}
