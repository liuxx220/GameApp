using System;
using System.Collections;
using System.Collections.Generic;








public class GUIDefine
{
    public static string UIF_LOGINFRAME         = "LoginFrame";
    public static string UIF_SELECTHEROFRAME    = "CRoleSelectFrame";
    public static string UIF_LOADLEVELFRAME     = "LoadLevelFrame";
    public static string UIF_CITYMAINFRAME      = "CityMainFrame";
    public static string UIF_CHARACTERFRAME     = "CharacterFrame";
    public static string UIF_BAGFRAME           = "BagFrame";
    public static string UIF_NPCTALKFRAME       = "QuestTalkFrame";
    public static string UIF_BATTLEFRAME        = "CombatleFrame";
}


/// --------------------------------------------------------------------------
/// <summary>
/// 游戏事件的枚举 
/// </summary>
/// --------------------------------------------------------------------------
public enum EGameEvent
{
    EGE_ErrorStr    = 1,
    EGE_ConnectedLoginApp = 2,
    EGE_ConnectFailed = 3,
}



public class CEvent
{
    private EGameEvent eventId;
    private Dictionary<string, object> paramList;

    public CEvent()
    {
        paramList = new Dictionary<string, object>();
    }

    public CEvent(EGameEvent id)
    {
        eventId = id;
        paramList = new Dictionary<string, object>();
    }

    public EGameEvent GetEventId()
    {
        return eventId;
    }

    public void AddParam(string name, object value)
    {
        paramList[name] = value;
    }

    public object GetParam(string name)
    {
        if (paramList.ContainsKey(name))
        {
            return paramList[name];
        }
        return null;
    }

    public bool HasParam(string name)
    {
        if (paramList.ContainsKey(name))
        {
            return true;
        }
        return false;
    }

    public int GetParamCount()
    {
        return paramList.Count;
    }

    public Dictionary<string, object> GetParamList()
    {
        return paramList;
    }
}