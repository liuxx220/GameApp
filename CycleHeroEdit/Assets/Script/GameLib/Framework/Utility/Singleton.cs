using UnityEngine;
using System.Collections;


public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    static object _lock = new object();
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = new T();
                }
            }
            return _instance;
        }
    }
}
