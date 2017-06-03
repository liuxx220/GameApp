/*
 * ----------------------------------------------------------------------------
 *          file name : InputProvider.cs
 *          desc      : 输入提供者
 *          author    : LJP
 *          
 *          log       : [ 2015-05-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;







public abstract class InputProvider : System.Object
{

    public virtual void GetInputState(int fingerIdex, out bool down, out Vector2 pos)
    {
        down = false;
        pos  = Vector2.zero;
    }

    public virtual void InitInputProvider() { }

    public virtual void Update() { }
}
