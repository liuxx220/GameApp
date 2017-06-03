/*
 * ----------------------------------------------------------------------------
 *          file name : LongPressRecongnizer.cs
 *          desc      : 长按手势识别对象
 *          author    : LJP
 *          
 *          log       : [ 2015-10-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections.Generic;






///---------------------------------------------------------------------------
/// <summary>
/// 长按手势
/// </summary>
///---------------------------------------------------------------------------
[System.Serializable]
public class LongPressGesture : Gesture
{

}



///---------------------------------------------------------------------------
/// <summary>
/// 长按手势识别对象
/// </summary>
///---------------------------------------------------------------------------
public class LongPressRecognizer : GestureRecognizerTemplate<LongPressGesture>
{

    /// <summary>
    /// How long the finger must stay down without moving in order to validate the gesture
    /// </summary>
    public float Duration = 1.0f;

    /// <summary>
    /// How far the finger is allowed to move around its starting position without breaking the gesture
    /// </summary>
    public float MoveTolerance = 0.5f;


    protected override void OnBegin(LongPressGesture gesture, IFingerList touches)
    {
		gesture.position        = touches.GetAveragePosition();
		gesture.startPosition    = touches.GetAverageStartPosition();
    }


    protected override GestureState OnRecognize(LongPressGesture gesture, IFingerList touches)
    {
		/*
        if (touches.Count != 1)
            return GestureState.Failed;

		if (gesture.deltaTime >= Duration)
            return GestureState.Recognized;

        if (touches.GetAverageDistanceFromStart() > ToPixels(MoveTolerance))
            return GestureState.Failed;
		*/
        return GestureState.InProgress;
    }


    public override string GetDefaultEventMessageName()
    {
        return "OnLongPress";
    }
}
