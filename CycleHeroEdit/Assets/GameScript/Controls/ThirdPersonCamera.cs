using UnityEngine;
using System;
using System.Collections;









public class ThirdPersonCamera : MonoBehaviour
{
	public float 		smooth = 3f;		
	Transform 			standardPos;		


	bool bQuickSwitch = false;	//Change Camera Position Quickly
	
	
	void Start()
	{
        GameObject pObj = UnityEngine.GameObject.Find ("CamPos");
		if (pObj != null) 
		{
			standardPos         = pObj.transform;
			transform.position  = standardPos.position;	
			transform.forward   = standardPos.forward;
		}
	}

	
	void FixedUpdate ()	
	{
		if (standardPos == null)
        {
            CHeroEntity pHero = CFightTeamMgr.Instance.m_pBattleHero;
            if( pHero != null && pHero.gameObject != null )
            {
                Transform pObj = pHero.gameObject.transform.FindChild("CamPos");
                if (pObj != null)
                {
                    standardPos         = pObj;
                    transform.position  = pObj.position;
                    transform.forward   = pObj.forward;
                }
            }
        }
			
		// return the camera to standard position and direction
		setCameraPositionNormalView();
	}

	void setCameraPositionNormalView()
	{
		if(bQuickSwitch == false)
		{
		// the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
		}
		else
		{
			// the camera to standard position and direction / Quick Change
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;
			bQuickSwitch = false;
		}
	}

	

}
