using UnityEngine;
using System;
using System.Collections;







public class CameraFollow : MonoBehaviour
{

	private Vector3			cameraOffset = Vector3.zero;
	private Vector3			cameraVelocity = Vector3.zero;
    private Vector3 		cameraPosNow = Vector3.zero;
	private Vector3			initOffsetToPlayer;
    private Transform 		LocalPlayer;

	public  float 			cameraSmoothing = 0.01f;
	void Start( )
    {

        CFightTeamMgr.Instance.OnEnterBattleScene();
		if (LocalPlayer == null)
			LocalPlayer = GameObject.FindGameObjectWithTag("Player").transform;

		if (LocalPlayer != null) 
		{
			initOffsetToPlayer = transform.position - LocalPlayer.position;
			cameraOffset	   = transform.position - LocalPlayer.position;
		}
    }


    void Update()
    {
     
		if ( LocalPlayer )
        {
			/*
			cameraPosNow.x = LocalPlayer.position.x - 5;
			cameraPosNow.y = LocalPlayer.position.y + 1.5f;
			cameraPosNow.z = LocalPlayer.position.z;

			transform.position = cameraPosNow;
			*/
			Vector3 cameraTargetPos = LocalPlayer.position + initOffsetToPlayer;
			transform.position      = Vector3.SmoothDamp( transform.position, cameraTargetPos, ref cameraVelocity, cameraSmoothing );
        }
    }

   
}
