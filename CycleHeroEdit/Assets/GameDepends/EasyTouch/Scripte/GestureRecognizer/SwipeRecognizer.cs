/*
 * ----------------------------------------------------------------------------
 *          file name : SwipeRecognizer.cs.cs
 *          desc      : 滑动手势识别器对象
 *          author    : LJP
 *          
 *          log       : [ 2015-10-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;






/// -------------------------------------------------------------------------------
/// <summary>
/// 滑动手势对象
/// </summary>
/// -------------------------------------------------------------------------------
[System.Serializable]
public class SwipePressGesture : Gesture
{

}


/// -------------------------------------------------------------------------------
/// <summary>
/// 滑动手势识别器对象
/// </summary>
/// -------------------------------------------------------------------------------
public class SwipeRecongnizer : GestureRecognizerTemplate<SwipePressGesture>
{

    /// <summary>
    /// 手势开始
    /// </summary>
    protected override void OnBegin(SwipePressGesture gesture, IFingerList touches)
    {

    }

    /// <summary>
    /// 手势识别
    /// </summary>
    protected override GestureState OnRecognize(SwipePressGesture gesture, IFingerList touches)
    {

        return GestureState.InProgress;
    }


    public override string GetDefaultEventMessageName()
    {
        return "OnSwipePress";
    }
}



