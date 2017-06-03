// EasyTouch v2.0 (September 2012)
// EasyTouch library is copyright (c) of Hedgehog Team
// Please send feedback or bug reports to the.hedgehog.team@gmail.com
using UnityEngine;
using System.Collections;


///---------------------------------------------------------------------------
/// <summary>
/// 手势状态
/// </summary>
///---------------------------------------------------------------------------
public enum GestureState
{
	/// <summary>
	/// The gesture recognizer is ready and waiting for the correct initial input conditions to begin
	/// </summary>
	Ready,

	/// <summary>
	/// The gesture recognition has just begun
	/// </summary>
	Started,

	/// <summary>
	/// The gesture is still ongoing and recognizer state has changed since last frame
	/// </summary>
	InProgress,

	/// <summary>
	/// The gesture detected a user input that invalidated it
	/// </summary>
	Failed,

	/// <summary>
	/// The gesture was succesfully recognized (used by continuous gestures)
	/// </summary>
	Ended,

	/// <summary>
	/// The gesture was succesfully recognized (used by discreet gestures)
	/// </summary>
	Recognized = Ended,

	/* ----------- INTERNAL -------------- */

	/// <summary>
	/// FOR INTERNAL USE ONLY (not an actual state)
	/// Used to tell the gesture to fail and immeditaly retry recognition (used by multi-tap)
	/// </summary>
	FailAndRetry,
}


/// <summary>
/// The reset mode determines when to reset a GestureRecognizer after it fails or succeeds (GestureState.Failed or GestureState.Recognized)
/// </summary>
public enum GestureResetMode
{
	/// <summary>
	/// Use the recommended value for this gesture recognizer
	/// </summary>
	Default,

	/// <summary>
	/// The gesture recognizer will reset on the next Update()
	/// </summary>
	NextFrame,

	/// <summary>
	/// The gesture recognizer will reset at the end of the current multitouch sequence
	/// </summary>
	EndOfTouchSequence,
}


/// <summary>
/// This is the class passed as parameter by EasyTouch events, that containing all informations about the touch that raise the event,
/// or by the tow fingers gesture that raise the event.
/// </summary>
public class Gesture{
	
	/// <summary>
	/// The index of the finger that raise the event (Starts at 0), or -1 for a two fingers gesture.
	/// </summary>
	public int fingerIndex;				
	/// <summary>
	/// The touches count.
	/// </summary>
	public int touchCount;				
	/// <summary>
	/// The start position of the current gesture, or the center position between the two touches for a two fingers gesture.
	/// </summary>
	public Vector2 startPosition;		
	/// <summary>
	/// The current position of the touch that raise the event,  or the center position between the two touches for a two fingers gesture.
	/// </summary>
	public Vector2 position;
	/// <summary>
	/// The position delta since last change.
	/// </summary>
	public Vector2 deltaPosition;		
	/// <summary>
	/// Time since the beginning of the gesture.
	/// </summary>
	public float actionTime;			
	/// <summary>
	/// Amount of time passed since last change.
	/// </summary>
	public float deltaTime;				
	/// <summary>
	/// The siwpe or drag  type ( None, Left, Right, Up, Down, Other => look at EayTouch.SwipeType enumeration).
	/// </summary>
	public EasyTouch.SwipeType swipe;	
	/// <summary>
	/// The length of the swipe.
	/// </summary>
	public float swipeLength;				
	/// <summary>
	/// The swipe vector direction.
	/// </summary>
	public Vector2 swipeVector;			
	/// <summary>
	/// The pinch length delta since last change.
	/// </summary>
	public float deltaPinch;	
	/// <summary>
	/// The angle of the twist.
	/// </summary>
	public float twistAngle;			
	/// <summary>
	/// The start time.
	/// </summary>
	public float startTime;
	/// <summary>
	/// 手指的当前状态
	/// </summary>
	public GestureState state = GestureState.Ready;

	/// <summary>
	/// 手指的前一个状态
	/// </summary>
	public GestureState prevstate = GestureState.Ready;


	/// <summary>
	/// Gets the swipe or drag angle. (calculate from swipe Vector)
	/// </summary>
	/// <returns>
	/// Float : The swipe or drag angle.
	/// </returns>
	public float GetSwipeOrDragAngle(){
		return Mathf.Atan2( swipeVector.normalized.y,swipeVector.normalized.x) * Mathf.Rad2Deg;	
	}
	
	/// <summary>
	/// Determines whether the touch is in a specified rect.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is in rect the specified rect; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='rect'>
	/// If set to <c>true</c> rect.
	/// </param>
	public bool IsInRect( Rect rect){
		return rect.Contains( position);	
	}

    private CFingerList fingers = new CFingerList();
    /// <summary>
    /// The fingers that began the gesture
    /// </summary>
    public CFingerList Fingers
    {
        get { return fingers; }
        internal set { fingers = value; }
    }
}

