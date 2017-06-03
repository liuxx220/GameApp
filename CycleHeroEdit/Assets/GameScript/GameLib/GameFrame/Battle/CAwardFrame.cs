using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;





/// <summary>
/// 战斗奖励界面
/// </summary>
public class CAwardFrame : GUIFrame
{



    public override bool ReloadUI()
    {
        base.ReloadUI();


        return true;
	}


    /// ---------------------------------------------------------------------------
    /// <summary>
    /// 释放本UIFrame 所用到的资源
    /// </summary>
    /// --------------------------------------------------------------------------
    public override void Destroy()
    {

		
	}
}
