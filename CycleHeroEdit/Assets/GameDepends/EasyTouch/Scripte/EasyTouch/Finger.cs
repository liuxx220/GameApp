// EasyTouch v2.0 (September 2012)
// EasyTouch library is copyright (c) of Hedgehog Team
// Please send feedback or bug reports to the.hedgehog.team@gmail.com
using UnityEngine;
using System.Collections;





// Represents informations on Finger for touch
// Internal use only, DO NOT USE IT
public class Finger{

	/// <summary>
	/// The index of the finger.
	/// </summary>
	public int 						fingerIndex;		
	/// <summary>
	/// The touch count.
	/// </summary>
	public int 						touchCount;	
	/// <summary>
	/// The start position.
	/// </summary>
	public Vector2 					startPosition;
	/// <summary>
	/// The complex start position.
	/// </summary>
	public Vector2 					complexStartPosition;
	/// <summary>
	/// The position.
	/// </summary>
	public Vector2 					position;			
	/// <summary>
	/// The delta position.
	/// </summary>
	public Vector2 					deltaPosition; 
	/// <summary>
	/// The old position.
	/// </summary>
	public Vector2 					oldPosition;
	/// <summary>
	/// The tap count.
	/// </summary>
	public int 						tapCount;
	/// <summary>
	/// The start time action.
	/// </summary>
	public float 					startTimeAction;  
	/// <summary>
	/// The delta time.
	/// </summary>
	public float 					deltaTime;	
	/// <summary>
	/// Describes the phase of the touch.
	/// </summary>
	public TouchPhase 				phase;		
	/// <summary>
	/// 前一个手指的状态
	/// </summary>
	public TouchPhase				prevPhase;
	/// <summary>
	/// The gesture.
	/// </summary>
	public  EasyTouch.GestureType 	gesture;	

	/// <summary>
	/// 判断手指是否已经按下
	/// </summary>
	public bool IsDown()
	{
		return true;
	}
	/// <summary>
	/// 返回当前手指的是否在移动中
	/// </summary>
	public bool IsMoving
	{
		get { return phase == TouchPhase.Moved; }
	}

	/// <summary>
	/// 返回上一帧的手势是否在移动中
	/// </summary>
	public bool WasMoving
	{
		get { return prevPhase == TouchPhase.Moved; }
	}

	/// <summary>
	/// 当前的手指的姿势是否是静止
	/// </summary>
	public bool IsStationary
	{
		get { return phase == TouchPhase.Stationary; }
	}

	/// <summary>
	/// 手指的姿势上一帧是否是静止
	/// </summary>
	public bool WasStationary
	{
		get { return prevPhase == TouchPhase.Stationary; }
	}
	/// <summary>
	/// 更新手指的状态
	/// </summary>
	public void Update(bool isDown, Vector2 pos)
	{
	}

	public void Clear()
	{

	}
}
