/*
 * ----------------------------------------------------------------------------
 *          file name : FGestuesInput.cs
 *          desc      : 手势输入管理对象
 *          author    : LJP
 *          
 *          log       : [ 2015-05-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





#pragma warning disable 618
public class EasyFingerGestues : MonoBehaviour
{

	#region private members
	// Joystick properties
	public Vector2 			InputAreaCenter;
	public Vector2 			InputAreaSize;
	#endregion


	#region Inspector
	public bool 			showProperties=true;
	public bool 			showInteraction=true;
	public bool 			showAppearance=true;

	/// <summary>
	/// Enable or disable the joystick.
	/// </summary>
	public bool 			enable = true;
	#endregion


    public delegate void    EventHandler();
    public static event     EventHandler OnInputProviderChanged;


	static public EasyFingerGestues instance;

    /// <summary>
    /// 输入提供者 
    /// </summary>
    private InputProvider       mInputProvider;


    // 手势识别器相关
    static List<GestureRecognizer> mRecognizer = new List<GestureRecognizer>();
    ///----------------------------------------------------------------------------------
    /// <summary>
    /// 判断平台，平台是否支持手势输入 
    /// </summary> 
    ///----------------------------------------------------------------------------------
   
    ///----------------------------------------------------------------------------------
    /// <summary>
    /// 初始化输入设备
    /// </summary> 
    ///----------------------------------------------------------------------------------
	public   Finger[]         mFingers = new Finger[10];
    public   CFingerList      mTouches = new CFingerList();


    ///----------------------------------------------------------------------------------
    /// <summary>
    /// 更新某个手指的位置信息，用来判断手势的输出
    /// </summary> 
    ///----------------------------------------------------------------------------------
    private void UpdateFingers()
    {

        if (mInputProvider != null)
        {
            mInputProvider.Update();
        }

        mTouches.Clear();
        for( int i = 0; i < mFingers.Length; ++i )
        {
			Finger finger   = mFingers[i];
            Vector2 pos     = Vector2.zero;
            bool IsDown     = false;

			mInputProvider.GetInputState(finger.fingerIndex, out IsDown, out pos);
            finger.Update(IsDown, pos);

			if( finger.IsDown() )
            {      
                mTouches.Add(finger);
            }
        }

        for (int i = 0; i < mRecognizer.Count; i++)
        {
            mRecognizer[i].Update();
        }
    }


    ///----------------------------------------------------------------------------------
    /// <summary>
    /// mono 行为 
    /// </summary> 
    ///----------------------------------------------------------------------------------
    void Start()
    {
        instance = this;
        InstallGestureRecognizer();
      
    }


    ///----------------------------------------------------------------------------------
    /// <summary>
    /// 得到某个手指的信息
    /// </summary> 
    ///----------------------------------------------------------------------------------
	public static Finger GetFinger( int idx )
    {
		return EasyFingerGestues.instance.mFingers[idx]; 
    }

   
    const float DESKTOP_SCREEN_STANDARD_DPI = 96; // default win7 dpi
    const float INCHES_TO_CENTIMETERS       = 2.54f; // 1 inch = 2.54 cm
    const float CENTIMETERS_TO_INCHES       = 1.0f / INCHES_TO_CENTIMETERS; // 1 cm = 0.3937... inches

    static float screenDPI                  = 0;
    /// <summary>
    /// Screen Dots-Per-Inch
    /// </summary>
    public static float ScreenDPI
    {
        get
        {
            // not intialized?
            if (screenDPI <= 0)
            {
                screenDPI = Screen.dpi;

                // on desktop, dpi can be 0 - default to a standard dpi for screens
                if (screenDPI <= 0)
                    screenDPI = DESKTOP_SCREEN_STANDARD_DPI;

#if UNITY_IPHONE
                // try to detect some devices that aren't supported by Unity (yet)
                if( iPhone.generation == iPhoneGeneration.Unknown ||
                    iPhone.generation == iPhoneGeneration.iPadUnknown ||
                    iPhone.generation == iPhoneGeneration.iPhoneUnknown )
                {
                    // ipad mini 2 ?
                    if( Screen.width == 2048 && Screen.height == 1536 && screenDPI == 260 )
                        screenDPI = 326;
                }
#endif
            }

			return EasyFingerGestues.screenDPI;
        }

		set { EasyFingerGestues.screenDPI = value; }
    }

	/*
    public static float Convert(float distance, DistanceUnit fromUnit, DistanceUnit toUnit)
    {
        float dpi = ScreenDPI;
        float pixelDistance;

        switch (fromUnit)
        {
            case DistanceUnit.Centimeters:
                pixelDistance = distance * CENTIMETERS_TO_INCHES * dpi; // cm -> in -> px
                break;

            case DistanceUnit.Inches:
                pixelDistance = distance * dpi; // in -> px
                break;

            case DistanceUnit.Pixels:
            default:
                pixelDistance = distance;
                break;
        }

        switch (toUnit)
        {
            case DistanceUnit.Inches:
                return pixelDistance / dpi; // px -> in

            case DistanceUnit.Centimeters:
                return (pixelDistance / dpi) * INCHES_TO_CENTIMETERS;  // px -> in -> cm

            case DistanceUnit.Pixels:
                return pixelDistance;
        }

        return pixelDistance;
    }
	*/

    /// ----------------------------------------------------------------------------------
    /// <summary>
    /// 注册手势识别器
    /// </summary>
    /// ----------------------------------------------------------------------------------
    public static void REGISTERRECOGNIZER(GestureRecognizer recognizer)
    {
        if (mRecognizer.Contains(recognizer))
            return;

        mRecognizer.Add(recognizer);
    }

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 注册手势识别器
    /// </summary>
    /// ---------------------------------------------------------------------------------
    public static void UNREGISTERRECOGNIZER(GestureRecognizer recognizer)
    {
        mRecognizer.Remove(recognizer);
    }

    ///----------------------------------------------------------------------------------
    /// <summary>
    /// 安装手势识别对象
    /// </summary> 
    ///----------------------------------------------------------------------------------
    public void InstallGestureRecognizer()
    {
        DragRecongnizer drag = new DragRecongnizer();
        drag.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(drag);

        CircleRecongnizer circle = new CircleRecongnizer();
        circle.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(circle);

        TapRecognizer click = new TapRecognizer();
        click.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(click);

        LongPressRecognizer longpress = new LongPressRecognizer();
        longpress.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(longpress);

        PinchRecongnizer pinch = new PinchRecongnizer();
        pinch.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(pinch);

        RelaxRecongnizer relax = new RelaxRecongnizer();
        relax.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(relax);

        SwipeRecongnizer swipe = new SwipeRecongnizer();
        swipe.Initalize(this.gameObject, null );
        REGISTERRECOGNIZER(swipe);
    }

    ///----------------------------------------------------------------------------------
    /// <summary>
    /// 判断跟本手指的信息是否在这个区域内
    /// </summary> 
    ///----------------------------------------------------------------------------------
    public bool IsRectUnderTouch(Finger gesture)
    {

        if (gesture.position.x < InputAreaCenter.x - InputAreaSize.x )
            return false;
        if (gesture.position.x > InputAreaCenter.x + InputAreaSize.x )
            return false;
        if (gesture.position.y < InputAreaCenter.y - InputAreaSize.y )
            return false;
        if (gesture.position.y > InputAreaCenter.y + InputAreaSize.y )
            return false;

        return true;
    }
}
	
