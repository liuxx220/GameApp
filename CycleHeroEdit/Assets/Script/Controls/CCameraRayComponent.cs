/************************************************************************************
 *          
 *      file name :  CCameraRayComponent.cs
 *      author    :  ljp
 *      desc      :  屏幕射线检测组件，选择具体的物体
 *      log       :  creat by ljp [2016-4-10 ]
 * 
************************************************************************************/
using UnityEngine;
using System.Collections;




[RequireComponent(typeof(Camera))]
public class CCameraRayComponent : MonoBehaviour
{

    /// <summary>
    /// 射线选择的游戏对象所属的Layer, -1 表示所有层
    /// </summary>
    public LayerMask    targetLayerMark = -1;

    /// <summary>
    /// 射线的长度 
    /// </summary>
    private float      targetRayLength  = Mathf.Infinity;


    private Camera     cam;


    ///  得到摄像机组件
    private void Awake()
    {
        cam             = GetComponent<Camera>();
    }

    private void Update( )
    {
        TargetRaycast();
    }


    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 在屏幕空间内选择物体对象，
    /// </summary>
    /// -----------------------------------------------------------------------------
    public void TargetRaycast()
    {

        Vector3 mp          = Input.mousePosition;
        Transform target    = null;
        if( cam )
        {
            RaycastHit hitInfo;
            Ray  ray        = cam.ScreenPointToRay( new Vector3 ( mp.x, mp.y, 0f ));
            if( Physics.Raycast( ray.origin, ray.direction, out hitInfo, targetRayLength, targetLayerMark ))
            {
                target      = hitInfo.collider.transform;
            }
        }

        if( target != null )
        {
            CHLGObjectComponent Com = target.root.GetComponentInChildren<CHLGObjectComponent>();
            if( Com != null )
            {

            }
        }
    }
}
