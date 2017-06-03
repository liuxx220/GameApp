using UnityEngine;
using System.Collections;
using System.Linq;
using System;



public class CFightCenter : MonoBehaviour
{


	public static CFightCenter 	FightCenter;
    public Transform[] 			EnemyList;

    public bool IsVictoryFlag   = true;


    IEnumerator Start()
    {

		FightCenter = this;
		EnemyList   = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            if( transform.GetChild(i).tag == "Monster" )
            {
				EnemyList[i] = transform.GetChild(i);
				EnemyList[i].gameObject.name = "Enmey@" + (i + 1);
            }
        }

        yield return new WaitForSeconds(0.5f);

    }

	int nFrame = 0;
    void Update()
    {
		nFrame++;

		if (nFrame % 80 == 0)
        {
			var result1 = from   s in CFightCenter.FightCenter.EnemyList
                          where  s != null && s.gameObject.activeSelf
                          select s;

            if (result1.Count() == 0 )
            {
				CFightCenter.FightCenter.Victory();
            }
        }
    }


	//--------------------------------------------------------------------------------
	// 战斗胜利处理
	//--------------------------------------------------------------------------------
    public void Victory()
    {
        if (IsVictoryFlag)
        {
            StartCoroutine(Victory1());
        }

        IsVictoryFlag = false;
    }

	//--------------------------------------------------------------------------------
	// 战斗胜利处理
	//--------------------------------------------------------------------------------
    IEnumerator Victory1()
    {

		yield return new WaitForSeconds (4f);

        //if (CNGUISys.Inst != null && CNGUISys.Inst.m_CombatUI != null) 
        //{
        //    CNGUISys.Inst.m_CombatUI.SendMessage( "OpenFightOverUI" );
        //}
	}
}
