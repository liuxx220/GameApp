/*
 * ----------------------------------------------------------------------------
 *          file name : ClickPressRecongnizer.cs
 *          desc      : 单击手势识别对象
 *          author    : LJP
 *          
 *          log       : [ 2015-10-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections.Generic;




///---------------------------------------------------------------------------
/// <summary>
/// 点击手势对象
/// </summary>
///---------------------------------------------------------------------------
[System.Serializable]
public class TapGesture : Gesture
{
    int taps    = 0;

    /// <summary>
    /// 
    /// </summary>
    public int Taps
    {
        get { return taps; }
        set { taps = value; }
    }

    internal bool   Down            = false;
    internal bool   WasDown         = false;
    internal float  LastDownTime    = 0;
    internal float  LastTapTime     = 0;
}

///---------------------------------------------------------------------------
/// <summary>
/// 单击手势识别对象
/// </summary>
///---------------------------------------------------------------------------
public class TapRecognizer : GestureRecognizerTemplate<TapGesture>
{

    /// <summary>
    /// How long the finger must stay down without moving in order to validate the gesture
    /// </summary>
    public float Duration           = 1.0f;
    public float StartTime          = 0f;


    protected override void OnBegin(TapGesture gesture, IFingerList touches)
    {
      
    }


    protected override GestureState OnRecognize(TapGesture gesture, IFingerList touches)
    {
        if (touches.Count != 1)
            return GestureState.Failed;

		if (gesture.deltaTime <= Duration)
            return GestureState.Recognized;

        return GestureState.InProgress;
    }


    public override string GetDefaultEventMessageName()
    {
        return "OnLongPress";
    }
}
