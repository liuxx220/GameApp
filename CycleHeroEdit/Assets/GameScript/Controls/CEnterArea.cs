using UnityEngine;
using System;
using System.Collections;
using System.Linq;






public class CEnterArea : MonoBehaviour
{

	public int			npctypeid = 0;

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 开始任务对话框
    /// </summary>
    /// ---------------------------------------------------------------------------------
	
	void OnTriggerEnter ( Collider other ) 
	{
		if (other.tag == "Player")
		{
            if (CQuestMgr.Inst != null)
            {
                CQuestScript pQS = CQuestMgr.Inst.GetQuestScript();
                if (pQS != null)
                {
                    pQS.OnTalk((uint)npctypeid);
                }
            }
		}
	}

    /// ---------------------------------------------------------------------------------
    /// <summary>
    /// 退出任务对话框
    /// </summary>
    /// ---------------------------------------------------------------------------------
	void OnTriggerExit ( Collider other ) 
	{
        if (CQuestMgr.Inst != null)
        {
            CQuestScript pQS = CQuestMgr.Inst.GetQuestScript();
            if (pQS != null)
            {
                pQS.OnExitQuestTalk( );
            }
        }
	}

    
}