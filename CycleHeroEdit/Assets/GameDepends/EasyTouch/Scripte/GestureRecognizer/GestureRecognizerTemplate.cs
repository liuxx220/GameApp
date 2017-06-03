/*
 * ----------------------------------------------------------------------------
 *          file name : GestureRecognizerTemplate.cs
 *          desc      : 不连续手势识别器对象
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
/// 手势识别器对象【模板类】
/// </summary>
/// -------------------------------------------------------------------------------
public abstract class GestureRecognizerTemplate<T> : GestureRecognizer where T : Gesture, new()
{

    /// <summary>
    ///  手势列表
    /// </summary>
    private List<T> _gestures = new List<T>();
    public delegate void GestureEventHandler(T gesture);
    public event GestureEventHandler OnGesture;


    public void Initalize( GameObject gameobject, Camera camera )
    {

        EventMessageTarget = gameobject;

        T gesture = new T();
        _gestures.Add(gesture);

        if (string.IsNullOrEmpty(EventMessageName))
            EventMessageName = GetDefaultEventMessageName();
    }



    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 判断手势是否可以开始
    /// </summary>
    /// ---------------------------------------------------------------------------------
    protected virtual bool CanBegin( T gesture, IFingerList touches)
    {
        if (touches.Count != RequiredFingerCount)
            return false;

        return true;
    }

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 手势可以开始识别, 子类中必须实现
    /// </summary>
    /// ---------------------------------------------------------------------------------
    protected abstract void OnBegin( T gesture, IFingerList touches );



    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 手势可以开识别处理, 子类中必须实现
    /// </summary>
    /// ---------------------------------------------------------------------------------
    protected abstract GestureState OnRecognize( T gesture, IFingerList touches);
    

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 手势状态变化处理
    /// </summary>
    /// ---------------------------------------------------------------------------------
	protected virtual void OnStateChanged(Gesture sender )
    {
        T gesture = (T)sender;
		if (gesture.state == GestureState.Recognized)
            RaiseEvent( gesture );
    }

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 手势识别器心跳逻辑
    /// </summary>
    /// ---------------------------------------------------------------------------------
    public override void Update()
    {
        if (IsExclusive)
        {
            UpdateExclusive();
        }

        else if (RequiredFingerCount == 1)
        {
            UpdatePerFinger();
        }

        else
        {
            UpdateExclusive();
        }
    }

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 计算当前所有的手指的情况
    /// </summary>
    /// ---------------------------------------------------------------------------------
    public virtual void UpdateExclusive()
    {
		
        T pGest = _gestures[0];
        IFingerList touches = EasyFingerGestues.instance.mTouches;
        if (pGest.state == GestureState.Ready)
        {
            if (CanBegin(pGest, touches))
                Begin(pGest, touches);
        }

        UpdateGesture(pGest, touches);
		
    }


    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 根据射线检测，手势与GameObject 是否有碰撞，来更新手势的状态
    /// </summary>
    /// ---------------------------------------------------------------------------------
    void Begin(T gesture, IFingerList touches)
    {
		
		gesture.startTime = Time.time;
#if UNITY_EDITOR
        if (gesture.Fingers.Count > 0)
            Debug.Log("egin gesture with fingers list not properly released");
#endif

        for (int i = 0; i < touches.Count; ++i)
        {
            Finger finger = touches[i];
            gesture.Fingers.Add(finger);
        }

        OnBegin(gesture, touches);
        gesture.state = GestureState.Started;
		
    }


    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ------------------------------------------------------------------------------------
	private CFingerList tempTouches = new CFingerList();
    void UpdatePerFinger()
    {

        for (int i = 0; i < EasyFingerGestues.instance.mFingers.Length; i++)
        {
            Finger finger       = EasyFingerGestues.instance.mFingers[i];
            T gesture           = _gestures[0];
            CFingerList touches = tempTouches;
            touches.Clear();


            if (finger.IsDown() )
                touches.Add( finger );

            if (gesture.state == GestureState.Ready)
            {
                if (CanBegin(gesture, touches))
                    Begin(gesture, touches);
            }

            UpdateGesture(gesture, touches);
        }
		
    }


    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 判断手势的状态
    /// </summary>
    /// ------------------------------------------------------------------------------------
    protected virtual void UpdateGesture(T gesture, IFingerList touches)
    {
		/*
        if (gesture.State == GestureState.Ready)
            return;

        if (gesture.State == GestureState.Started)
            gesture.State = GestureState.InProgress;

        switch (gesture.State)
        {
            case GestureState.InProgress:
                {
                    GestureState newState = OnRecognize(gesture, touches);
                    if (newState == GestureState.FailAndRetry)
                    {
                        gesture.State = GestureState.Failed;
                        Reset(gesture);

                        if (CanBegin(gesture, touches))
                        {
                            Begin(gesture, touches);
                        }
                    }
                    else
                    {
                        if (newState == GestureState.InProgress)
                        {
                            //gesture.PickStartSelection(Raycaster);
                        }

                        gesture.State = newState;
                    }
                }
                break;

            case GestureState.Recognized:
            case GestureState.Failed:
                {
                    RaiseEvent(gesture);
                    Reset(gesture);
                }
                break;

            default:
                Debug.LogError("Unhandled state" + gesture.State);
                gesture.State = GestureState.Failed;
                break;

        }
        */
    }

    ///----------------------------------------------------------------------------------------------
    /// 重置手势的状态
    //-----------------------------------------------------------------------------------------------
	protected virtual void Reset(Gesture gesture)
    {
        gesture.state = GestureState.Ready;
    }

    protected void RaiseEvent( T gesture)
    {
        if ( !string.IsNullOrEmpty(EventMessageName))
        {
            if (EventMessageTarget)
                EventMessageTarget.SendMessage(EventMessageName, gesture, SendMessageOptions.DontRequireReceiver);
        }
    }
}



