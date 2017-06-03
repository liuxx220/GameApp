/*
 * ----------------------------------------------------------------------------
 *          file name : RelaxRecognizer.cs.cs
 *          desc      : 两指拉开手势识别器对象
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
public class RelaxPressGesture : Gesture
{

}


/// -------------------------------------------------------------------------------
/// <summary>
/// 两指拉开手势识别器对象
/// </summary>
/// -------------------------------------------------------------------------------
public class RelaxRecongnizer : GestureRecognizerTemplate<RelaxPressGesture>
{

    /// <summary>
    /// 手势开始
    /// </summary>
    protected override void OnBegin(RelaxPressGesture gesture, IFingerList touches)
    {

    }

    /// <summary>
    /// 手势识别
    /// </summary>
    protected override GestureState OnRecognize(RelaxPressGesture gesture, IFingerList touches)
    {

        return GestureState.InProgress;
    }


    public override string GetDefaultEventMessageName()
    {
        return "OnRelaxPress";
    }
}



