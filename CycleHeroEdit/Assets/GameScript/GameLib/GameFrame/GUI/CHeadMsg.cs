using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;






public class CHeadMsg : MonoBehaviour
{


	public  Transform		_HeadMsg;
	public  Transform		_HeadNum;
	public Transform 		_HeadPos;

	public static int 		BeAttackShowTimes = 0;
	void Start( )
	{
		_HeadPos = Instantiate( _HeadMsg ) as Transform;
		_HeadPos.GetComponent<UILabel>().SetAnchor( transform );
	}


	public IEnumerator ShowHP( int nNum )
	{

		_HeadPos.GetComponent<UILabel>().SetAnchor( transform );
		BeAttackShowTimes++;
		if( _HeadNum == null )
			_HeadNum = Instantiate (_HeadMsg, _HeadPos.localPosition, Quaternion.Euler (0, 0, 1)) as Transform;

		if (_HeadPos.localPosition.y > 300)
			_HeadNum.localPosition = new Vector3(_HeadPos.localPosition.x, 280, _HeadPos.localPosition.z);
		else
			_HeadNum.localPosition = _HeadPos.localPosition;
		_HeadNum.localScale 	   = Vector3.one;

		TweenPosition _TweenPosition= _HeadNum.GetComponent<TweenPosition>();
		TweenScale _TweenScale 	   = _HeadNum.GetComponent<TweenScale>();

		yield return new WaitForSeconds( 0.05f );

		_HeadNum.GetComponent<UILabel>().text = nNum.ToString();
		
		_HeadNum.localPosition 	= _HeadPos.localPosition;
		_TweenPosition.from 	= _HeadPos.localPosition;
		
		_TweenPosition.to 		= new Vector3(_HeadPos.localPosition.x, 
		                                  	  _HeadPos.localPosition.y + (BeAttackShowTimes % 4 * 60), 
		                                  	  _HeadPos.localPosition.z);
		
		_TweenPosition.enabled 	= true;
		_TweenScale.enabled 	= true;
		
		_TweenPosition.duration = 0.05f;
		_TweenScale.duration 	= 0.05f;
		
		_TweenPosition.PlayReverse();
		_TweenScale.PlayReverse();
		
		yield return new WaitForSeconds(0.08f);
		
		_TweenPosition.PlayForward();
		_TweenScale.PlayForward();
	}
}
