using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;






/*
	brif class CtrolAnimation
	动画播放器
*/
public class CtrolAnimation : MonoBehaviour
{

   
	public float 						m_RunSpeed = 1;
	public ACTID						m_nCurPlayActID;

	private string[]					m_mapActName;
	private ACTID[]						m_mapActID;

	private Dictionary<ACTID, string> 	m_mapName2ID;
	//--------------------------------------------------------------------------------
	// 初始化
	//--------------------------------------------------------------------------------
	IEnumerator Start()
    {

		m_mapActName 	= new string[]{ "Run", "Idel", "ClimbUp", 
										"Attack01", "Attack02", "Attack03", 
										"Skill01", "Spurt","Show",
										"Damage01", "Damage02", "Damage03", "Damage04", "Damage06", "Damage07","Damage99" };
		
		m_mapActID 		= new ACTID[] { ACTID.ACT_Run, ACTID.ACT_Idel, ACTID.ACT_ClimbUp,
										ACTID.ACT_DoubleHit01, ACTID.ACT_DoubleHit02, ACTID.ACT_DoubleHit03, 
										ACTID.ACT_Skill01,ACTID.ACT_Skill02,ACTID.ACT_Skill03,
										ACTID.ACT_Damage01, ACTID.ACT_Damage02, ACTID.ACT_Damage03, ACTID.ACT_Damage04, ACTID.ACT_Damage05, ACTID.ACT_Damage06,ACTID.ACT_Dead };
   		


		m_mapName2ID 	= new Dictionary<ACTID, string>();
		for (int i = 0; i < m_mapActName.Length; i++)
		{
			m_mapName2ID.Add(m_mapActID[i], m_mapActName[i]);
		}

		yield return new WaitForSeconds(0.1f);

		m_nCurPlayActID = ACTID.ACT_Idel;
	}

	//--------------------------------------------------------------------------------
	// 根据序号来播放动作
	//--------------------------------------------------------------------------------
	public void PlayTrack( ACTID _AnimNum )
	{
	
		m_nCurPlayActID = _AnimNum;
		string szTrack	= m_mapName2ID [m_nCurPlayActID];

        GetComponent<Animation>()[szTrack].speed = 1;
		GetComponent<Animation>().Play (szTrack);

		if (m_RunSpeed != 1 && m_nCurPlayActID == ACTID.ACT_Run)
			GetComponent<Animation>()[szTrack].speed = m_RunSpeed;
	}
	
	//--------------------------------------------------------------------------------
	// 根据序号来播放动作
	//--------------------------------------------------------------------------------
	public void CrossFade( ACTID _AnimNum )
	{
	
		m_nCurPlayActID = _AnimNum;
		string szTrack	= m_mapName2ID [m_nCurPlayActID];
		GetComponent<Animation>() [szTrack].speed = 1;

		
		if (m_RunSpeed != 1 && m_nCurPlayActID == ACTID.ACT_Run)
			GetComponent<Animation>()[szTrack].speed = m_RunSpeed;

		GetComponent<Animation>().CrossFade (szTrack);
	}

	//--------------------------------------------------------------------------------
	// 设置当前动画的播放速度
	//--------------------------------------------------------------------------------
	public void SetPlaySpeed( float fspeed )
	{
		if (m_mapName2ID.ContainsKey (m_nCurPlayActID)) 
		{
			GetComponent<Animation>()[m_mapName2ID[m_nCurPlayActID]].speed = fspeed;
		}
	}
}
