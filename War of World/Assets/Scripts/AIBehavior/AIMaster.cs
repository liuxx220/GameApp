using UnityEngine;

namespace AIToolkit
{
	class AIMaster : MonoBehaviour
    {
        private AIEnityManager.AIEntityUpdater _aiUpdater;
        private AIEnityManager.AIEntityUpdater _requestUpdater;
        private AIEnityManager.AIEntityUpdater _behaviorUpdater;
        void Awake()
        {
            _aiUpdater =
                delegate(AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateAI(gameTime, deltaTime);
                };
            _requestUpdater =
                delegate(AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateReqeust(gameTime, deltaTime);
                };
            _behaviorUpdater =
                delegate(AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateBehavior(gameTime, deltaTime);
                };
        }

		void Start()
		{
			//init timer
			AITimer.instance.Init();
		}

        void Update()
        {
            //update global timer
            AITimer.instance.UpdateTime();
            
			float deltaTime = AITimer.instance.deltaTime;
			float gameTime  = AITimer.instance.gameTime;

            AIEnityManager entityMgr = AIEnityManager.instance;
            //Update AI
            entityMgr.IteratorDo(_aiUpdater, gameTime, deltaTime);
            //Update Request
            entityMgr.IteratorDo(_requestUpdater, gameTime, deltaTime);
            //Update Behaivor
            entityMgr.IteratorDo(_behaviorUpdater, gameTime, deltaTime);
        }
        
        
    }
}
