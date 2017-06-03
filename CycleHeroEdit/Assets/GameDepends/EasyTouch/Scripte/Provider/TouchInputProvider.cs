/*
 * ----------------------------------------------------------------------------
 *          file name : TouchInputProvider.cs
 *          desc      : 手指输入提供者 
 *          author    : LJP
 *          
 *          log       : [ 2015-05-04 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;






public class TouchInputProvider : InputProvider
{


    public int  maxTouches      = 2;
    private int touchIdOffset   = 0;

    UnityEngine.Touch nullTouch = new UnityEngine.Touch();
    int[] finger2touchMap;

    public override void InitInputProvider()
    {
        finger2touchMap = new int[maxTouches];
    }


    /// --------------------------------------------------------------------------
    /// <summary>
    /// 触摸屏输入更新逻辑
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Update()
    {
        UpdateFingerTouchMap();
    }

    void UpdateFingerTouchMap()
    {
        for (int i = 0; i < finger2touchMap.Length; ++i)
            finger2touchMap[i] = -1;

#if UNITY_ANDROID
        if( fixAndroidTouchIdBug )
        {
            if( Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began )
                touchIdOffset = Input.touches[0].fingerId;
        }
#endif

        for (int i = 0; i < Input.touchCount; ++i)
        {
            int fingerIndex = Input.touches[i].fingerId - touchIdOffset;

            if (fingerIndex < finger2touchMap.Length)
                finger2touchMap[fingerIndex] = i;
        }
    }


    bool HasValidTouch(int fingerIndex)
    {
        return finger2touchMap[fingerIndex] != -1;
    }

    UnityEngine.Touch GetTouch(int fingerIndex)
    {
        int touchIndex = finger2touchMap[fingerIndex];

        if (touchIndex == -1)
            return nullTouch;

        return Input.touches[touchIndex];
    }


    public override void GetInputState(int fingerIndex, out bool down, out Vector2 position)
    {
        down = false;
        position = Vector2.zero;

        if (HasValidTouch(fingerIndex))
        {
            UnityEngine.Touch touch = GetTouch(fingerIndex);

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                down = false;
            else
            {
                down = true;
                position = touch.position;
            }
        }
    }
   
}
