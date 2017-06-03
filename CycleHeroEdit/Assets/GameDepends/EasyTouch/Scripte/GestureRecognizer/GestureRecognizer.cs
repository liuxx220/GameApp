/*
 * ----------------------------------------------------------------------------
 *          file name : GestureRecognizer.cs
 *          desc      : 手势识别器对象
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
/// 手势识别器对象
/// </summary>
/// -------------------------------------------------------------------------------
public abstract class GestureRecognizer
{

    
    public enum SelectionType
    {
        Default             = 0,
        StartSelection      = 1,
        CurrentSelection    = 2,
        None                = 3,
    }

    /// <summary>
    /// 需要手指数量
    /// </summary>
    [SerializeField]
    public int              requiredFingerCount = 1;

   
    /// <summary>
    /// Get or set the reset mode for this gesture recognizer
    /// </summary>
    public GestureResetMode ResetMode = GestureResetMode.Default;


    /// <summary>
    /// Use Unity's SendMessage() to broadcast the gesture event to MessageTarget
    /// </summary>
    public string           EventMessageName;         
    public GameObject       EventMessageTarget;    
   
    /// <summary>
    /// When the exclusive flag is set, this gesture recognizer will only detect the gesture when the total number
    ///  of active touches on the device is equal to RequiredFingerCount (FingerGestures.Touches.Count == RequiredFingerCount)
    /// </summary>
    public bool             IsExclusive = false;

    /// <summary>
    /// Exact number of touches required for the gesture to be recognized
    /// </summary>
    public virtual int RequiredFingerCount
    {
        get { return requiredFingerCount; }
        set { requiredFingerCount = value; }
    }

    /// <summary>
    /// Get the default reset mode for this gesture recognizer. 
    /// Derived classes can override this to specify a different default value
    /// </summary>
    public virtual GestureResetMode GetDefaultResetMode()
    {
        return GestureResetMode.EndOfTouchSequence;
    }

    /// <summary>
    /// Return the default name of the method to invoke on the message target 
    /// </summary>
    public abstract string GetDefaultEventMessageName();

   
    /// <summary>
    /// Convert a distance specified in the unit currently set by DistanceUnit property, and
    /// returns a distance in pixels
    /// </summary>
    //public float ToPixels(float distance)
   // {
    //    return distance.Convert(DistanceUnit, DistanceUnit.Pixels);
    //}

    /// <summary>
    /// Convert distance to pixels and returns the square of pixel distance
    ///  This is NOT the same as converting the square of the distance and converting it to pixels
    /// </summary>
    //public float ToSqrPixels(float distance)
    //{
    //    float pixelDist = ToPixels(distance);
    //    return pixelDist * pixelDist;
    //}

     /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 手势识别器心跳逻辑
    /// </summary>
    /// ---------------------------------------------------------------------------------
    public virtual void Update( )
    {

    }
}


