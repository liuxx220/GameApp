using System;
using UniLua;
using System.Collections;
using System.Collections.Generic;




// C# function to lua 's 



class CScriptLuaMgr
{

	public static CScriptLuaMgr Inst = null;

	public ILuaState				m_luaState = null;	
	private	Dictionary< string, CGameScript >		m_mapScript;


	public CScriptLuaMgr( )
	{
		Inst = this;
		m_mapScript = new Dictionary<string, CGameScript>();
	}


	public void InitLuaMgr( )
	{
		m_luaState 		= LuaAPI.NewState ();
		m_luaState.L_OpenLibs ();

		ToolLib.LuaOpenCommonLib( m_luaState );
	}

	public CGameScript	CreateScript( string szfile, bool bcreate )
	{
		CGameScript pScript = null;
		m_mapScript.TryGetValue (szfile, out pScript);
		if( pScript != null )
		{
			if( bcreate )
			{
				m_mapScript.Remove( szfile );
			}
			else
			{
				return pScript;
			}
		}

		pScript	= new CGameScript();
		if( pScript.LoadFile( szfile ) )
		{
			m_mapScript.Add( szfile, pScript );
		}
		return pScript;
	}

	public void DestroyScript( string szfile )
	{
		CGameScript pScript = null;
		m_mapScript.TryGetValue (szfile, out pScript);
		if( pScript != null )
		{
			m_mapScript.Remove( szfile );
		}
	}

	public CGameScript	GetScript( string szfile )
	{
		CGameScript pScript = null;
		m_mapScript.TryGetValue (szfile, out pScript);
		if( pScript != null )
		{
			return pScript;
		}

		return null;
	}
}