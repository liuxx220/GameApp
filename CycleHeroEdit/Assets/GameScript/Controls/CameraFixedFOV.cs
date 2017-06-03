using UnityEngine;
using System;
using System.Collections;






/// <summary>
/// 固定视角的摄像机
/// </summary>
public class CameraFixedFOV : MonoBehaviour
{

    private Vector3         cameraOffset = new Vector3(0, 5, -5);
    private Transform 		LocalPlayer;
    private Transform       myTransform;


	void Start( )
    {
        myTransform     = this.transform;
        CFightTeamMgr.Instance.OnEnterBattleScene();
		if (LocalPlayer == null)
			LocalPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        if ( LocalPlayer )
        {
            myTransform.position = LocalPlayer.position + cameraOffset;
            myTransform.LookAt(LocalPlayer.position, Vector3.up);
        }
    }
   
}
