/*
 * ----------------------------------------------------------------------------
 *          file name : PinchRecognizer.cs.cs
 *          desc      : 捏手势识别器对象
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
/// 捏手势识别器对象
/// </summary>
/// -------------------------------------------------------------------------------
[System.Serializable]
public class PinchPressGesture : Gesture
{

}


///---------------------------------------------------------------------------
/// <summary>
/// 拖动手势对象
/// </summary>
///---------------------------------------------------------------------------
public class PinchRecongnizer : GestureRecognizerTemplate<PinchPressGesture>
{

    /// <summary>
    /// 手势开始
    /// </summary>
    protected override void OnBegin(PinchPressGesture gesture, IFingerList touches)
    {

    }

    /// <summary>
    /// 手势识别
    /// </summary>
    protected override GestureState OnRecognize(PinchPressGesture gesture, IFingerList touches)
    {

        return GestureState.InProgress;
    }


    public override string GetDefaultEventMessageName()
    {
        return "OnPinchPress";
    }
}




