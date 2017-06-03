/*
 * ----------------------------------------------------------------------------
 *          file name : Finger.cs
 *          desc      : 手指列表对象
 *          author    : LJP
 *          
 *          log       : [ 2015-05-04 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public interface IFingerList : IEnumerable<Finger>
{
    /// <summary>
    /// Get finger in array by index
    /// </summary>
    /// <param name="index">The array index</param>
	Finger this[int index]
    {
        get;
    }

    /// <summary>
    /// Number of fingers in the list
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Return true if all the touches are currently moving
    /// </summary>
    bool AllMoving();

    /// <summary>
    /// 返回所有手指的移动方向是否相同
    /// </summary>
    bool MovingInSameDirection(float tolerance);

    /// --------------------------------------------------------------------------
    /// <summary>
    /// 得到列表中所有手指的平均位置
    /// </summary>
    /// --------------------------------------------------------------------------
    Vector2 GetAveragePosition();

     /// --------------------------------------------------------------------------
    /// <summary>
    /// 得到列表中所有手指的平均位置
    /// </summary>
    /// --------------------------------------------------------------------------
    Vector2 GetAverageStartPosition();

    /// <summary>
    /// Get the average distance from each finger's starting position in the list
    /// </summary>
    float GetAverageDistanceFromStart();
}

[System.Serializable]
public class CFingerList : IFingerList
{
	private List<Finger> fingerList = null;
	public CFingerList()
    {
		fingerList = new List<Finger>();
    }

	/// <summary>
	/// Get finger in array by index
	/// </summary>
	/// <param name="index">The array index</param>
	public Finger this[int index]
    {
        get { return fingerList[index]; }
    }

	/// <summary>
	/// Number of fingers in the list
	/// </summary>
    public int Count
    {
        get { return fingerList.Count; }
    }

	/// <summary>
	/// Add the specified touch.
	/// </summary>
	public void Add(Finger touch)
    {
        fingerList.Add(touch);
    }

	/// <summary>
	/// Remove the specified touch.
	/// </summary>
	public bool Remove(Finger touch)
    {
        return fingerList.Remove(touch);
    }

	/// <summary>
	/// Determines whether this instance is contains the specified touch.
	/// </summary>
	public bool IsContains(Finger touch)
    {
        return fingerList.Contains(touch);
    }

    public void Clear()
    {
        fingerList.Clear();
    }

	/// <summary>
	/// Returns an enumerator that iterates through a collection.
	/// </summary>
	/// <returns>The enumerator.</returns>
	public IEnumerator<Finger> GetEnumerator()
	{
		return fingerList.GetEnumerator();
	}

	/// <summary>
	/// Gets the enumerator.
	/// </summary>
	/// <returns>The enumerator.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>
	/// Alls the moving.
	/// </summary>
	/// <returns><c>true</c>, if moving was alled, <c>false</c> otherwise.</returns>
	public bool AllMoving()
	{
		return false;
	}

	/// <summary>
	/// Movings the in same direction.
	/// </summary>
	/// <returns><c>true</c>, if in same direction was movinged, <c>false</c> otherwise.</returns>
	/// <param name="tolerance">Tolerance.</param>
	public bool MovingInSameDirection(float tolerance)
	{
		return false;
	}

	/// <summary>
	/// Gets the average position.
	/// </summary>
	/// <returns>The average position.</returns>
	public Vector2 GetAveragePosition()
	{
		return Vector2.zero;
	}

	/// <summary>
	/// Gets the average start position.
	/// </summary>
	/// <returns>The average start position.</returns>
	public Vector2 GetAverageStartPosition()
	{
		return Vector2.zero;
	}

	/// <summary>
	/// Gets the average distance from start.
	/// </summary>
	/// <returns>The average distance from start.</returns>
	public float GetAverageDistanceFromStart()
	{
		return 0.0f;
	}
}