using UnityEngine;
using System;
using System.Collections;
using System.Linq;







public class CNpcAnimEvents : MonoBehaviour
{
   
   
	private CreatureFSM				m_LocalFSM = null;
	private CtrolAnimation			m_CtrlSkelton = null;
	private CtrolEnenyAI 			m_AI = null;

	//--------------------------------------------------------------------------------
	// 初始化
	//--------------------------------------------------------------------------------
    void Start()
    {
		m_CtrlSkelton 		= GetComponent<CtrolAnimation>();
		m_AI   				= GetComponent<CtrolEnenyAI>();

    }

	//--------------------------------------------------------------------------------
	// 心跳
	//--------------------------------------------------------------------------------
	void Update()
	{
		if (m_AI != null && m_LocalFSM == null) 
		{
			if( m_AI.m_pOwner != null && m_AI.m_pOwner.m_FSM != null )
				m_LocalFSM = m_AI.m_pOwner.m_FSM;
		}
	}

	//--------------------------------------------------------------------------------
	// 切换到空闲状态
	//--------------------------------------------------------------------------------
	public IEnumerator ANIMEVENT_CHANGETOIDEL( float time )
	{

		ACTID tempACTID = m_CtrlSkelton.m_nCurPlayActID;
		yield return new WaitForSeconds(time);

		if( tempACTID == m_CtrlSkelton.m_nCurPlayActID )
			m_LocalFSM.Change2IdleBeHavior ( );

        CFightTeamMgr.Instance.m_bActIsInCD = false;
	}
	
	
	//--------------------------------------------------------------------------------
	// 播放某个动作  事件
	//--------------------------------------------------------------------------------
	public IEnumerator ANIMEVENT_PLAYANIMATION( string path )
	{

		ACTID tempACTID = m_CtrlSkelton.m_nCurPlayActID;
		yield return new WaitForSeconds( float.Parse(path.Split(',')[1]) );

		if (tempACTID == m_CtrlSkelton.m_nCurPlayActID) 
		{
			m_CtrlSkelton.PlayTrack( (ACTID)int.Parse(path.Split(',')[0]) );
		}
	
	}
	
	//--------------------------------------------------------------------------------
	// 攻击前方群体怪物  事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_ATTACKENEMYRANGE( string strValue )
	{

		if (m_LocalFSM != null ) 
		{
			m_LocalFSM.doAnimEvent( ANIMEVENTTYPE.AET_AttackEnemy, strValue );
		} 
	}
	
	//--------------------------------------------------------------------------------
	// 设置动画播放的速度  事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_SETSPEED( float fspeed )
	{

		if (m_CtrlSkelton != null) 
		{
			m_CtrlSkelton.SetPlaySpeed( fspeed );
		}
	}
	
	//--------------------------------------------------------------------------------
	// 向前移动 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_MOVEFORWARD( float fdistance )
	{
		iTween.MoveToMy(gameObject, fdistance);
	}
	
	
	//--------------------------------------------------------------------------------
	// 向前移动 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_MOVEFORWARDINTIME( string disandtime )
	{

		iTween.MoveToMyInTime(gameObject, float.Parse(disandtime.Split(',')[0]), float.Parse(disandtime.Split(',')[1]));
	}
	
	
	//--------------------------------------------------------------------------------
	// 摄像机震动 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_SHAKECAMERA( float fshakevalue )
	{
		iTween.ShakePosition(Camera.main.gameObject, new Vector3(fshakevalue, fshakevalue, fshakevalue), 0.1f);
	}
	
	
	//--------------------------------------------------------------------------------
	// 播放攻击特效在自身位置 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_PALYEFFECTONSELF( string path )
	{

		Transform transformEffect 		= null;
		transformEffect 				= (Instantiate(Resources.Load(path)) as GameObject).transform;
		transformEffect.parent 			= transform;
		transformEffect.localPosition 	= Vector3.zero;
		transformEffect.gameObject.name = gameObject.name + "effect";
		transformEffect.localRotation 	= Quaternion.Euler(Vector3.zero);
		
		Destroy(transformEffect.gameObject, 1);
	}
	
	
	//--------------------------------------------------------------------------------
	// 播放攻击特效在自身位置 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_PALYEFFECTONSELFPOS( string path )
	{

		Transform transformEffect 		= null;
		transformEffect 				= (Instantiate(Resources.Load(path)) as GameObject).transform;
		transformEffect.parent 			= transform;
		transformEffect.localPosition 	= Vector3.zero;
		transformEffect.gameObject.name = gameObject.name + "effect";
		transformEffect.localRotation 	= Quaternion.Euler(Vector3.zero);
		Destroy(transformEffect.gameObject, 1);
		transformEffect.parent 			= null;
	}
	
	
	//--------------------------------------------------------------------------------
	// 播放攻击特效在自身位置 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_PALYEFFECTONSELFPOSINTIME( string path )
	{

		Transform transformEffect 		= null;
		transformEffect 				= (Instantiate(Resources.Load(path.Split(',')[0])) as GameObject).transform;
		transformEffect.parent 			= transform;
		transformEffect.localPosition 	= Vector3.zero;
		transformEffect.gameObject.name = gameObject.name + "Effect";
		transformEffect.localRotation 	= Quaternion.Euler(Vector3.zero);
		Destroy(transformEffect.gameObject, float.Parse(path.Split(',')[1]));
		transformEffect.parent 			= null;
	}
	
	//--------------------------------------------------------------------------------
	// 打开某个技能的CD 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_SETSKILLCDTURE( )
	{
		CFightTeamMgr.Instance.m_bActIsInCD = true;
	}
	
	//--------------------------------------------------------------------------------
	// 关闭某个技能的CD 事件
	//--------------------------------------------------------------------------------
	public void ANIMEVENT_SETSKILLCDFALSE( )
	{
        CFightTeamMgr.Instance.m_bActIsInCD = false;
	}
}
