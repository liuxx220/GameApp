/*
 * ----------------------------------------------------------------------------
 *          file name : MouseInputProvider.cs
 *          desc      : 鼠标输入提供者
 *          author    : LJP
 *          
 *          log       : [ 2015-05-10 ]
 * ----------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;






public class MouseInputProvider : InputProvider
{

  
    public KeyCode pivotKey             = KeyCode.LeftAlt;
    bool pivoting = false;

  
    Vector2 pivot                       = Vector2.zero;
    Vector2[] pos                       = { Vector2.zero, Vector2.zero };

   
    public override void InitInputProvider()
    {
        
    }

    /// --------------------------------------------------------------------------
    /// <summary>
    /// 鼠标输入更新逻辑
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Update()
    {
       
        if (Input.GetKey(pivotKey))
        {
            if (Input.GetKeyDown(pivotKey))
            {
                pivot = Input.mousePosition;
            }

            if (!pivoting)
            {
                if (Vector2.Distance(Input.mousePosition, pivot) > 50.0f)
                    pivoting = true;
            }

            if (pivoting)
            {
                pos[0] = pivot;
                pos[1] = Input.mousePosition;
            }
        }
        else
        {
            pivoting = false;
        }
    }


    public override void GetInputState(int fingerIndex, out bool down, out Vector2 position)
    {
        down         = Input.GetMouseButton(fingerIndex);
        position     = Input.mousePosition;

    }
}
