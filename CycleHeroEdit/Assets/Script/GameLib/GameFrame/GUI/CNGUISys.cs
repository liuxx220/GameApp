using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;







class CNGUISys
{


	public static CNGUISys Inst = null;
	public Transform		m_CharacterUI = null;


	public CNGUISys( )
	{
		Inst = this;
	}

	

	public void OnUpdateCharacterALL( )
	{
		if (m_CharacterUI != null) 
		{
			m_CharacterUI.SendMessage( "OnUpdateCharacterALL" );
		}
	}


	// param iType 0  hero'att  1 fightteam att
	public void FightAttChangeEvent( int iType )
	{

		
	}

	
}

