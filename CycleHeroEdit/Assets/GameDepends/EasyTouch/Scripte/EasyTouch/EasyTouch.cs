using UnityEngine;
using System.Collections;

/// <summary>
/// This is the main class, you need to add it to your main camera or on a empty gameobject in your scene (or use Hedgehog Team menu).
/// </summary>
public class EasyTouch : MonoBehaviour {
	
	#region Enumerations
	public enum GestureType{ Tap, Drag, Swipe, None, LongTap, Pinch, Twist, Cancel, Acquisition };
	/// <summary>
	/// Represents the different directions for a swipe or drag gesture.
	/// </summary>
	public enum SwipeType{ None, Left, Right, Up, Down, Other};
	#endregion

	#region Joystick and Finger Gestues
	private EasyTouchInput 		input;					// 输入对象 
	private EasyJoystick		mEasyJoystick;			// 摇杆对象
	private EasyFingerGestues 	mEasyFingerGestues;		// 手势识别对象 
	#endregion


	public static EasyTouch instance;										// Fake singleton
	public bool 			enable = true;
	private Finger[] 		fingers=new Finger[10];						// The informations of the touch for finger 1.	
	

	void Start()
	{
		instance 			= this;
		input 				= new EasyTouchInput();

        gameObject.AddComponent<EasyJoystick>();
        gameObject.AddComponent<EasyFingerGestues>();
	}
		
	// Non comments.
	void Update(){
	
		if (EasyTouch.instance==this){

			// How many finger do we have ?
			int count = input.TouchCount();
			// Get touches		
			#if (((UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR))
				UpdateTouches(true, count);
			#else
				UpdateTouches(false, count);
			#endif
		}
	}
	
	void UpdateTouches(bool realTouch, int touchCount){


		if (realTouch ){
			foreach (Touch touch in Input.touches)
            {
				
				if (fingers[touch.fingerId]==null){
					fingers[touch.fingerId]= new Finger();
					fingers[touch.fingerId].fingerIndex = touch.fingerId;
					fingers[touch.fingerId].gesture = GestureType.None;
				}
				fingers[touch.fingerId].position        = touch.position;
				fingers[touch.fingerId].deltaPosition   = touch.deltaPosition;
				fingers[touch.fingerId].tapCount        = touch.tapCount;
				fingers[touch.fingerId].deltaTime       = touch.deltaTime;
				fingers[touch.fingerId].phase           = touch.phase;	
				fingers[touch.fingerId].touchCount      = touchCount;


                if (EasyJoystick.instance != null && EasyJoystick.instance.IsRectUnderTouch(fingers[touch.fingerId]))
                {
                    EasyJoystick.instance.UpdateJoystick(fingers[touch.fingerId]);
                }

                if (EasyFingerGestues.instance != null && EasyFingerGestues.instance.IsRectUnderTouch(fingers[touch.fingerId]))
                {

                }
				
			}			
		}
		else{
			int i=0;
			while (i<touchCount){
				fingers[i] = input.GetMouseTouch(i,fingers[i]) as Finger;
				fingers[i].touchCount = touchCount;

                if (EasyJoystick.instance != null && EasyJoystick.instance.IsRectUnderTouch(fingers[i]))
                {
					EasyJoystick.instance.UpdateJoystick (fingers[i]);
				}

                if (EasyFingerGestues.instance != null && EasyFingerGestues.instance.IsRectUnderTouch(fingers[i]))
                {

                }
				i++;
			}			
		}
	}

	
	private int GetTwoFinger( int index){
	
		int i=index+1;
		bool find=false;
		
		while (i<100 && !find){
			if (fingers[i]!=null ){
				if( i>=index){
					find=true;
				}
			}
			i++;
		}
		i--;
		
		return i;
	}
		
	/// <summary>
	/// Determines whether a touch is under a specified rect.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this a touch is rect under the specified rect; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='rect'>
	/// If set to <c>true</c> rect.
	/// </param>
	public static bool IsRectUnderTouch( Rect rect, int nID ){
		
		bool find=false;
		if ( EasyTouch.instance.fingers[nID]!=null){
			find = rect.Contains(  EasyTouch.instance.fingers[nID].position);
		}

		return find;
	}

}
