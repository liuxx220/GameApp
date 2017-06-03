/*
 * ----------------------------------------------------------------------------
 *          file name : CircleRecognizer.cs.cs
 *          desc      : 画圆手势识别器对象
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
/// 点击手势对象
/// </summary>
///---------------------------------------------------------------------------
[System.Serializable]
public class CirclePressGesture : Gesture
{

}


/// -------------------------------------------------------------------------------
/// <summary>
/// 画圆手势识别器对象
/// </summary>
/// -------------------------------------------------------------------------------
public class CircleRecongnizer : GestureRecognizerTemplate<CirclePressGesture>
{

    /// <summary>
    /// 手势开始
    /// </summary>
    protected override void OnBegin(CirclePressGesture gesture, IFingerList touches)
    {
         
    }

    /// <summary>
    /// 手势识别
    /// </summary>
    protected override GestureState OnRecognize(CirclePressGesture gesture, IFingerList touches)
    {

        return GestureState.InProgress;
    }

    public override string GetDefaultEventMessageName()
    {
        return "OnClrclePress";
    }
}



