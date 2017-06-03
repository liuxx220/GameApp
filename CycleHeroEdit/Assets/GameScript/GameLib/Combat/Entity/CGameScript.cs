using System;
using UniLua;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





enum EGameScriptState
{
	EGSS_Invalid		= -1,
	EGSS_NotLoad		= 0,
	EGSS_Loaded,
	EGSS_Running,
	EGSS_WaitTime,
	EGSS_Break,
	EGSS_Done,
}


class CGameScript
{

	EGameScriptState		m_eState;
	ILuaState				m_lua;

	string 					strError;
	public CGameScript( )
	{
		if( m_lua == null )
		{
			m_lua = CScriptLuaMgr.Inst.m_luaState;
		}
	}

	public void InitLua( ILuaState lua )
	{
		m_lua = lua;
	}

	public bool LoadFile( string szFile )
	{
		if( m_lua != null )
		{
			var status = m_lua.L_DoFile( szFile );
			if( status != ThreadStatus.LUA_OK )
			{
				strError = m_lua.ToString( -1 );
				return false;
			}

			if( !m_lua.IsTable(-1) )
			{
				strError = "lua return value is not table";
				return false;
			}
		}

		return true;
	}

	public void GetStoreFunction( string name )
	{
		m_lua.GetGlobal (name);
		if( !m_lua.IsFunction( -1 ) )
		{
			Common.ERROR_MSG( string.Format("method not found!", name ) );
			return ;
		}
	}

	public void RunFunction( int iParamNum, int nRetNum )
	{
		m_lua.Call (iParamNum, nRetNum );
	}

	private static int Traceback( ILuaState lua )
	{
		var msg = lua.ToString (1);
		if( msg != null )
		{
			lua.L_Traceback( lua, msg, 1 );
		}

		else if( !lua.IsNoneOrNil(1) )
		{
			if(!lua.L_CallMeta(1, "__tostring")) {
				lua.PushString("(no error message)");
			}
		}

		return 1;
	}

	public void PushString( string strParam )
	{
		m_lua.PushString (strParam);
	}

	public void PushInt( int iParam )
	{
		m_lua.PushInteger (iParam);
	}

	public void PushUint( uint uParam )
	{
		m_lua.PushUnsigned (uParam);
	}

	public void Pop()
	{
		m_lua.Pop (1);
	}
}