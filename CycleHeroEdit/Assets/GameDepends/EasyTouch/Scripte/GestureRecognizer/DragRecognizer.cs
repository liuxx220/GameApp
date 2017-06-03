/*
 * ----------------------------------------------------------------------------
 *          file name : DragRecognizer.cs.cs
 *          desc      : 拖动手势识别器对象
 *          author    : LJP
 *          
 *          log       : [ 2015-10-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;






///---------------------------------------------------------------------------
/// <summary>
/// 拖动手势对象
/// </summary>
///---------------------------------------------------------------------------
[System.Serializable]
public class DragPressGesture : Gesture
{
    private Vector2 deltaMove   = Vector2.zero;

    internal Vector2 LastPos    = Vector2.zero;
    internal Vector2 LastDelta  = Vector2.zero;


    public Vector2 DeltaMove
    {
        get { return deltaMove;  }
        set { deltaMove = value;  }
    }

    public Vector2 TotalMove
    {
		get { return position - startPosition;  }
    }
}


/// -------------------------------------------------------------------------------
/// <summary>
/// 拖动手势识别器对象
/// </summary>
/// -------------------------------------------------------------------------------
public class DragRecongnizer : GestureRecognizerTemplate<DragPressGesture>
{

    /// <summary>
    /// 移动误差
    /// </summary>
    public float MoveTolerance = 0.2f;

    
    /// <summary>
    /// 手势开始
    /// </summary>
    protected override void OnBegin( DragPressGesture gesture, IFingerList touches)
    {

        gesture.position        = touches.GetAveragePosition();
		gesture.startPosition    = touches.GetAverageStartPosition();
		gesture.DeltaMove       = gesture.position - gesture.startPosition;
        gesture.LastDelta       = Vector2.zero;
		gesture.LastPos         = gesture.position;
    }


    protected override bool CanBegin(DragPressGesture gesture, IFingerList touches)
    {
		/*
        if (!base.CanBegin(gesture, touches))
            return false;

        if (touches.GetAverageDistanceFromStart() < ToPixels(MoveTolerance))
            return false;

        if (!touches.AllMoving())
            return false;

        if (RequiredFingerCount >= 2 && !touches.MovingInSameDirection(0.35f))
            return false;
		*/
        return true;
    }

    /// <summary>
    /// 手势识别
    /// </summary>
    protected override GestureState OnRecognize( DragPressGesture gesture, IFingerList touches)
    {

        if (touches.Count != RequiredFingerCount)
        {
            // fingers were lifted off
            if (touches.Count < RequiredFingerCount)
                return GestureState.Ended;

            return GestureState.Failed;
        }

        if (RequiredFingerCount >= 2 && 
                 touches.AllMoving() && !touches.MovingInSameDirection(0.35f))
            return GestureState.Failed;

		gesture.position    = touches.GetAveragePosition();
        gesture.LastDelta   = gesture.DeltaMove;
		gesture.DeltaMove   = gesture.position - gesture.LastPos;

        if (gesture.DeltaMove.sqrMagnitude > 0 || gesture.LastDelta.sqrMagnitude > 0)
			gesture.LastPos = gesture.position;

        RaiseEvent(gesture);
        return GestureState.InProgress;
    }


    public override string GetDefaultEventMessageName()
    {
        return "OnDragPress";
    }
}



