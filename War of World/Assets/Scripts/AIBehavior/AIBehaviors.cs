﻿using TsiU;
using UnityEngine;

namespace AIToolkit
{
    class AIEntityWorkingData : TBTWorkingData
    {
        public AIEntity entity          { get; set; }
        public Transform entityTF       { get; set; }
        public Animator entityAnimator  { get; set; }
        public float gameTime           { get; set; }
        public float deltaTime          { get; set; }
    }

    public class AIEntityBehaviorTreeFactory
    {
        private static TBTAction _bevTreeAct;
        static public TBTAction GetBehaviorTreeAction()
        {
			if(_bevTreeAct != null)
            {
				return _bevTreeAct;
            }
			_bevTreeAct = new TBTActionPrioritizedSelector();
//			_bevTreeAct.AddChild(new TBTActionSequence()
//                    .SetPrecondition(new TBTPreconditionNOT(new CON_HasReachedTarget()))
//                    .AddChild(new NOD_TurnTo())
//                    .AddChild(new NOD_MoveTo()))
//                .AddChild(new NOD_Attack());

			TBTActionSequence actionSequence = new TBTActionSequence ();
			_bevTreeAct.AddChild (actionSequence);

			CON_HasReachedTarget hasReachedTarget = new CON_HasReachedTarget ();
			TBTPreconditionNOT preconditionNOT = new TBTPreconditionNOT (hasReachedTarget);
			actionSequence.SetPrecondition (preconditionNOT);


			NOD_TurnTo turnTo = new NOD_TurnTo ();
			NOD_MoveTo moveTo = new NOD_MoveTo ();
			NOD_Attack attackTo = new NOD_Attack ();

			actionSequence.AddChild (turnTo);
			actionSequence.AddChild (moveTo);
			actionSequence.AddChild (attackTo);



			return _bevTreeAct;
        }
    }

    class CON_HasReachedTarget : TBTPreconditionLeaf
    {
        public override bool IsTrue(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
            Vector3 targetPos = TMathUtils.Vector3ZeroY(thisData.entity.GetBBValue<Vector3>(AIEntity.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
            Vector3 currentPos = TMathUtils.Vector3ZeroY(thisData.entityTF.position);
            return TMathUtils.GetDistance2D(targetPos, currentPos) < 1f;
        }
    }
    class NOD_Attack : TBTActionLeaf
    {
        private const float DEFAULT_WAITING_TIME = 5f;
        private static readonly string[] ENDING_ANIM = new string[]{"back_fall", "right_fall", "left_fall"};
        class UserContextData
        {
            internal float attackingTime;
        }
        protected override void onEnter(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
            UserContextData userData = getUserContexData<UserContextData>(wData);
            userData.attackingTime = DEFAULT_WAITING_TIME;
			if(thisData.entityAnimator)
				thisData.entityAnimator.CrossFade("attack", 0.2f);
        }
        protected override int onExecute(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
            UserContextData userData = getUserContexData<UserContextData>(wData);
            if (userData.attackingTime > 0)
            {
                userData.attackingTime -= thisData.deltaTime;
                if (userData.attackingTime <= 0)
                {
					if(thisData.entityAnimator)
                    thisData.entityAnimator.CrossFade(ENDING_ANIM[Random.Range(0, ENDING_ANIM.Length)], 0.2f);
					return TBTRunningStatus.FINISHED;
                }
            }
            return TBTRunningStatus.EXECUTING;
        }
    }
    class NOD_MoveTo : TBTActionLeaf
    {
        protected override void onEnter(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
			if(thisData.entityAnimator)
            thisData.entityAnimator.CrossFade("walk", 0.2f);

			thisData.entity.EnabledAutoMove (true);
        }
        protected override int onExecute(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
            Vector3 targetPos = TMathUtils.Vector3ZeroY(thisData.entity.GetBBValue<Vector3>(AIEntity.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
            Vector3 currentPos = TMathUtils.Vector3ZeroY(thisData.entityTF.position);
            float distToTarget = TMathUtils.GetDistance2D(targetPos, currentPos);
            if (distToTarget < 1f)
            {
                //thisData.entityTF.position = targetPos;
				thisData.entity.EnabledAutoMove (false);
                return TBTRunningStatus.FINISHED;
            }
            else
            {
                int ret = TBTRunningStatus.EXECUTING;
//                Vector3 toTarget = TMathUtils.GetDirection2D(targetPos, currentPos);
//                float movingStep = 0.5f * thisData.deltaTime;
//                if(movingStep > distToTarget)
//                {
//                    movingStep = distToTarget;
//                    ret = TBTRunningStatus.FINISHED;
//                }
//                thisData.entityTF.position = thisData.entityTF.position + toTarget * movingStep;
				thisData.entity.AutoMove (targetPos);

                return ret;
            }
        }
    }
    class NOD_TurnTo : TBTActionLeaf
    {
        protected override void onEnter(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
			if(thisData.entityAnimator)
            thisData.entityAnimator.CrossFade("walk", 0.2f);
        }
        protected override int onExecute(TBTWorkingData wData)
        {
            AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
            Vector3 targetPos = TMathUtils.Vector3ZeroY(thisData.entity.GetBBValue<Vector3>(AIEntity.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
            Vector3 currentPos = TMathUtils.Vector3ZeroY(thisData.entityTF.position);
            if (TMathUtils.IsZero((targetPos - currentPos).sqrMagnitude))
            {
                return TBTRunningStatus.FINISHED;
            }
            else
            {
                Vector3 toTarget = TMathUtils.GetDirection2D(targetPos, currentPos);
                Vector3 curFacing = thisData.entityTF.forward;
                float dotV = Vector3.Dot(toTarget, curFacing);
                float deltaAngle = Mathf.Acos(Mathf.Clamp(dotV, -1f, 1f));
                if(deltaAngle < 0.1f)
                {
                    thisData.entityTF.forward = toTarget;
                    return TBTRunningStatus.FINISHED;
                }
                else
                {
                    Vector3 crossV = Vector3.Cross(curFacing, toTarget);
                    float angleToTurn = Mathf.Min(3f * thisData.deltaTime, deltaAngle);
                    if (crossV.y < 0)
                    {
                        angleToTurn = -angleToTurn;
                    }
                    thisData.entityTF.Rotate(Vector3.up, angleToTurn*Mathf.Rad2Deg, Space.World);
                }
            }
            return TBTRunningStatus.EXECUTING;
        }
    }
}
