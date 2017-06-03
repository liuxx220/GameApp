/*
 * ----------------------------------------------------------------------------------
 *         file name : GameEventMgr.cs
 *         desc      : 游戏中通过广播事件，来实现逻辑与UI之间的通信
 *         author    : LJP
 * 
 *         log       : [ 2015-05-01 ]
 * -----------------------------------------------------------------------------------
*/
using UnityEngine;
using System;
using LuaInterface;
using System.Collections.Generic;







public class CEventCenterMgr : View
{

    // disable the unused variable warning
    #pragma warning disable 0414
    #pragma warning restore 0414


    /// <summary>
    /// 监听事件的异常处理
    /// </summary>
    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg) { }
    }


    static public Dictionary<EGameEvent, Delegate> mEventMap = new Dictionary<EGameEvent, Delegate>();
    static public List< EGameEvent >    mPermanentMessage    = new List<EGameEvent>();
    static public Dictionary<EGameEvent, LuaFunction> mLuaFunctionMap = new Dictionary<EGameEvent, LuaFunction>();



    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册游戏内的事件触发调用
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void OnListenerAdding( EGameEvent eType, Delegate listener )
    {
        if( !mEventMap.ContainsKey(eType) )
        {
            mEventMap.Add(eType, null);
        }

        Delegate d = mEventMap[eType];
        if( d != null && d.GetType() != listener.GetType() )
        {
            throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eType, d.GetType().Name, listener.GetType().Name));
        }
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 删除游戏内注册的事件触发调用
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void OnListenerRemoving(EGameEvent eventType, Delegate listener )
    {
        if ( mEventMap.ContainsKey(eventType) )
        {
            Delegate d = mEventMap[eventType];
            if (d == null)
            {
                throw new ListenerException(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listener.GetType())
            {
                throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listener.GetType().Name));
            }

            mEventMap.Remove(eventType);
        }
        else
        {
            throw new ListenerException(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }


    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void AddListener(EGameEvent eventType, LuaFunction handler )
    {
        mLuaFunctionMap[eventType] = handler;
    }


    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void AddListener( EGameEvent eventType, Callback handler )
    {
        OnListenerAdding(eventType, handler);
        mEventMap[eventType] = (Callback)mEventMap[eventType] + handler;
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册事件, 带一个参数
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void AddListener<T>(EGameEvent eventType, Callback<T> handler)
    {
        OnListenerAdding(eventType, handler);
        mEventMap[eventType] = (Callback<T>)mEventMap[eventType] + handler;
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册事件, 带二个参数
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void AddListener<T, U>(EGameEvent eventType, Callback<T, U> handler)
    {
        OnListenerAdding(eventType, handler);
        mEventMap[eventType] = (Callback<T, U>)mEventMap[eventType] + handler;
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册事件, 带三个参数
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void AddListener<T, U, V>(EGameEvent eventType, Callback<T, U, V> handler)
    {
        OnListenerAdding(eventType, handler);
        mEventMap[eventType] = (Callback<T, U, V>)mEventMap[eventType] + handler;
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 注册事件, 带四个参数
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void AddListener<T, U, V, X>(EGameEvent eventType, Callback<T, U, V, X> handler)
    {
        OnListenerAdding(eventType, handler);
        mEventMap[eventType] = (Callback<T, U, V, X>)mEventMap[eventType] + handler;
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 取消注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void RemoveListener(EGameEvent eventType, Callback handler) 
    {

        mEventMap[eventType] = (Callback)mEventMap[eventType] - handler;
        OnListenerRemoving(eventType, handler );
    }

    public void RemoveListener( EGameEvent eventType )
    {
        mLuaFunctionMap.Remove(eventType);
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 取消注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
	public void RemoveListener<T>(EGameEvent eventType, Callback<T> handler) 
    {
        mEventMap[eventType] = (Callback<T>)mEventMap[eventType] - handler;
        OnListenerRemoving(eventType, handler);
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 取消注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
	public void RemoveListener<T, U>(EGameEvent eventType, Callback<T, U> handler)
    {
        mEventMap[eventType] = (Callback<T, U>)mEventMap[eventType] - handler;
        OnListenerRemoving(eventType, handler);
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 取消注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
	public void RemoveListener<T, U, V>(EGameEvent eventType, Callback<T, U, V> handler) 
    {
        mEventMap[eventType] = (Callback<T, U, V>)mEventMap[eventType] - handler;
        OnListenerRemoving(eventType, handler);
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 取消注册事件
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void RemoveListener<T, U, V, X>(EGameEvent eventType, Callback<T, U, V, X> handler)
    {
        mEventMap[eventType] = (Callback<T, U, V, X>)mEventMap[eventType] - handler;
        OnListenerRemoving(eventType, handler);
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 广播这个事件，到所有对本事件有兴趣的系统中去
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void FireEvent(EGameEvent eventType, CEvent pParam )
    {
        LuaFunction luafun;
        if (mLuaFunctionMap.TryGetValue(eventType, out luafun))
        {
            if (luafun != null)
            {
                luafun.Call( pParam );
            }
        }
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 广播这个事件，到所有对本事件有兴趣的系统中去
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void FireEvent<T>(EGameEvent eventType, T arg1)
    {
        Delegate d;
        if (mEventMap.TryGetValue(eventType, out d))
        {
            Callback<T> callback = d as Callback<T>;
            if (callback != null)
                callback(arg1);
        }
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 广播这个事件，到所有对本事件有兴趣的系统中去
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void FireEvent<T, U>(EGameEvent eventType, T arg1, U arg2)
    {
        Delegate d;
        if (mEventMap.TryGetValue(eventType, out d))
        {
            Callback<T, U> callback = d as Callback<T, U>;
            if (callback != null)
                callback(arg1, arg2);
        }
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 广播这个事件，到所有对本事件有兴趣的系统中去
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void FireEvent<T, U, V>(EGameEvent eventType, T arg1, U arg2, V arg3)
    {
        Delegate d;
        if (mEventMap.TryGetValue(eventType, out d))
        {
            Callback<T, U, V> callback = d as Callback<T, U, V>;
            if (callback != null)
                callback(arg1, arg2, arg3);
        }
    }

    /// -----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 广播这个事件，到所有对本事件有兴趣的系统中去
    /// </summary>
    /// -----------------------------------------------------------------------------------------------------
    public void FireEvent<T, U, V, X>(EGameEvent eventType, T arg1, U arg2, V arg3, X arg4)
    {
        Delegate d;
        if (mEventMap.TryGetValue(eventType, out d))
        {
            Callback<T, U, V, X> callback = d as Callback<T, U, V, X>;
            if (callback != null)
                callback(arg1, arg2, arg3, arg4);
        }
    }	
}

