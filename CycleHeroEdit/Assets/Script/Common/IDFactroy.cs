using UnityEngine;
using System;



public class GenID 
{
	public static uint static_heroid 	= 0;
	public static uint static_cycleid 	= 100000;
	public static uint static_npcid 	= 500000;
	public static uint static_skillid 	= 100000;


	public static uint MakeHeroID()
	{
		return static_heroid++;
	}
	
	public static uint MakeCycleID() 
	{
		return static_cycleid++;
	}
	
	public static uint  MakeNpcID( )
	{
		return static_npcid++;
	}

	public static uint  MakeSkillID( )
	{
		return static_npcid++;
	}
}
