using UnityEngine;
using TsiU;

namespace AIToolkit
{
    class AIEntity : MonoBehaviour
    {
        //-----------------------------------------------
        public const string BBKEY_NEXTMOVINGPOSITION = "NextMovingPosition";
        //-----------------------------------------------
        private TBTAction _behaviorTree;
        private AIEntityWorkingData _behaviorWorkingData;
        private TBlackBoard _blackboard;

        private AIBehaviorRequest _currentRequest;
        private AIBehaviorRequest _nextRequest;

        private GameObject _targetDummyObject;

        private float _nextTimeToGenMovingTarget;


		private UnityEngine.AI.NavMeshAgent _navagent;

		public AIEntity Init(GameObject targetObj)
        {
			_behaviorTree = AIEntityBehaviorTreeFactory.GetBehaviorTreeAction();

            _behaviorWorkingData = new AIEntityWorkingData();
            _behaviorWorkingData.entity = this;
            _behaviorWorkingData.entityTF = this.transform;
            _behaviorWorkingData.entityAnimator = GetComponent<Animator>();

            _blackboard = new TBlackBoard();

            _nextTimeToGenMovingTarget = 0;

			_targetDummyObject = targetObj;

			_navagent = GetComponent<UnityEngine.AI.NavMeshAgent>();


            return this;
        }
        public T GetBBValue<T>(string key, T defaultValue)
        {
            return _blackboard.GetValue<T>(key, defaultValue);
        }
        public int UpdateAI(float gameTime, float deltaTime)
        {
            if (gameTime > _nextTimeToGenMovingTarget)
            {
                _nextRequest = new AIBehaviorRequest(gameTime, new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f)));
                _nextTimeToGenMovingTarget = gameTime + 20f + Random.Range(-5f, 5f);
            }
            return 0;
        }
        public int UpdateReqeust(float gameTime, float deltaTime)
        {
            if (_nextRequest != _currentRequest)
            {
                //reset bev tree
                _behaviorTree.Transition(_behaviorWorkingData);
                //assign to current
                _currentRequest = _nextRequest;

                //reposition and add a little offset
                //Vector3 targetPos = _currentRequest.nextMovingTarget + TMathUtils.GetDirection2D(_currentRequest.nextMovingTarget, transform.position) * 0.2f;
                //Vector3 startPos = new Vector3(targetPos.x, -1.4f, targetPos.z);
                //_targetDummyObject.transform.position = startPos;
                //LeanTween.move(_targetDummyObject, targetPos, 1f);
            }
            return 0;
        }
        public int UpdateBehavior(float gameTime, float deltaTime)
        {
            if (_currentRequest == null)
            {
                return 0;
            }
            //update working data
			if (null != _behaviorWorkingData.entityAnimator) 
			{
				_behaviorWorkingData.entityAnimator.speed = AITimer.instance.timeScale;
			}
            
            _behaviorWorkingData.gameTime  = gameTime;
            _behaviorWorkingData.deltaTime = deltaTime;

            //test bb usage
            //_blackboard.SetValue(BBKEY_NEXTMOVINGPOSITION, _currentRequest.nextMovingTarget);
			_blackboard.SetValue(BBKEY_NEXTMOVINGPOSITION, _targetDummyObject.transform.position);

            if (_behaviorTree.Evaluate(_behaviorWorkingData))
            {
                _behaviorTree.Update(_behaviorWorkingData);
            }
            else
            {
                _behaviorTree.Transition(_behaviorWorkingData);
            }
            return 0;
        }



		public void AutoMove(Vector3 v3)
		{
			if (_navagent.enabled)
				_navagent.SetDestination(v3);
		}

		public void EnabledAutoMove(bool enabled)
		{
			_navagent.enabled = enabled;
		}
    }
}
