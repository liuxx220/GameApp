using UnityEngine;
using LuaInterface;
using LuaFramework;
using System.Collections;
using System.Collections.Generic;






public class CLuaBehaviour : View 
{

    protected static bool initialize        = false;
    protected List<LuaFunction> buttons     = new List<LuaFunction>();


	void Start()
	{
		
	}

   
    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 调用lua接口
    /// </summary>
    /// ------------------------------------------------------------------------------------
    public void AddClick( GameObject go, LuaFunction func )
    {
        if (go == null)
            return;
        UIEventListener.Get(go).onClick = delegate(GameObject o)
        {
            func.Call(go);
            buttons.Add(func);
        };
    }


    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 调用lua接口
    /// </summary>
    /// ------------------------------------------------------------------------------------
    public void ClearClick()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null)
            {
                buttons[i].Dispose();
                buttons[i] = null;
            }
        }
    }


    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 调用lua接口
    /// </summary>
    /// ------------------------------------------------------------------------------------
    protected object[] CallMethod( string func, params object[] args )
    {
        if (!initialize)
            return null;

        return Util.CallMethod(name, func, args);

    }

    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// 调用lua接口
    /// </summary>
    /// ------------------------------------------------------------------------------------
    protected void OnDestroy( )
    {
        LuaManager = null;
        ClearClick();
        Util.ClearMemory();
    }
}
