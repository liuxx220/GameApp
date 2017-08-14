using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public enum FIMOVEDIR
{
	FMD_Left	= 0,
	FMD_Stop,
	FMD_Right
}


public class CoverFlow : MonoBehaviour
{

	private List<GameObject> Players = new List<GameObject> ();
	private int m_PlayerCount = 7;
	
	private int currentIndex  = 0;
	private float MARGIN_X	  = 3f;  	 
	private float ITEM_WITH	  = 8f;   

	private FIMOVEDIR _touchMoveDir = FIMOVEDIR.FMD_Stop;
	private float _touchdistance  = 0f;
	private int sliderValue= 4;



	void Start ()
	{
        LoadImages();
	}
	

	void LoadImages ()
	{

		for (int i=0; i<m_PlayerCount; i++) 
		{
            Texture2D tex = Resources.Load("PlayerCard/player" + i.ToString(), typeof(Texture2D)) as Texture2D;
			Players.Add( GeneratePlayerWithReflect( tex, i, this.transform) );
		}

		moveSlider( Players.Count / 2 );
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0) {

			if( Input.GetTouch(0).phase == TouchPhase.Moved )
			{
				if( Input.GetTouch(0).deltaPosition.x < 0 - Mathf.Epsilon )
				{
					_touchMoveDir = FIMOVEDIR.FMD_Left;
				}
				else
				{
					_touchMoveDir = FIMOVEDIR.FMD_Right;
				}
			}

			if( Input.GetTouch(0).phase == TouchPhase.Stationary )
			{
				_touchMoveDir = FIMOVEDIR.FMD_Stop;
			}
		}

		if( _touchMoveDir != FIMOVEDIR.FMD_Stop )
		{
			if( _touchMoveDir == FIMOVEDIR.FMD_Left )
			{
				_touchdistance += Time.deltaTime;
				if( _touchdistance > 0.5f )
				{
					sliderValue++;
					if( sliderValue >= m_PlayerCount )
					{
						sliderValue = m_PlayerCount - 1;
						return;
					}
					moveSlider( sliderValue );
				}
			}
			else
			{
				_touchdistance += Time.deltaTime;
				if( _touchdistance > 0.5f )
				{
					sliderValue--;
					if( sliderValue < 0 )
					{
						sliderValue = 0;
						return;
					}
					moveSlider( (int)sliderValue );
				}
			}

		}
	}
	
	void moveSlider(int id)
	{
		if( currentIndex==id )
			return;

		currentIndex = id;
		for(int i=0; i < m_PlayerCount; i++ )
		{
			float targetX	= 0f;
			float targetZ	= 0f;
			float targetRot	= 0f;
			
			targetX			= MARGIN_X*(i-id);
			//left slides
			if(i<id)
			{
				targetX	   -= ITEM_WITH * 0.6f;
				targetZ		= ITEM_WITH * 3f/4;
				targetRot	= -60f;
				
			}
			//right slides
			else if(i>id)
			{
				targetX	   += ITEM_WITH * 0.6f;
				targetZ		= ITEM_WITH * 3f/4;
				targetRot	= 60f;
			}
			else
			{
				targetX    += 0f;
				targetZ 	= 0f;
				targetRot 	= 0f;
			}
			
			GameObject pPlayer	= Players[i];
			float ys			= pPlayer.transform.position.y;
			Vector3 ea			= pPlayer.transform.eulerAngles;

			iTween.MoveTo( pPlayer,new Vector3(targetX,ys,targetZ),1f);
			iTween.RotateTo( pPlayer,new Vector3(ea.x,targetRot,targetZ),1f);
			//iTween.ScaleTo( pPlayer, new Vector3(), 1f ); 
		}

        //if( CFightTeamMgr.inst != null )
        //{
        //    CFightTeamMgr.inst.SelectFristHero();
        //}

        //if( CNGUISys.Inst != null )
        //{
        //    CNGUISys.Inst.OnUpdateSelectHero( currentIndex );
        //}
	}
	
	
	void OnGUI ()
	{

		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			return;


		sliderValue = (int)GUI.HorizontalSlider(new Rect((Screen.width-175f)/2,Screen.height-30f,175f,30f),
		                                   sliderValue, 0f, m_PlayerCount-1);
		moveSlider(sliderValue);

	}



	GameObject GeneratePlayerWithReflect( Texture2D tex,int id,Transform parent )
	{
		GameObject photoObj		= new GameObject();
		photoObj.name			= "PlayerObj"+id.ToString();
		
		
		GameObject pPlayer 		= GameObject.CreatePrimitive (PrimitiveType.Plane);
		pPlayer.GetComponent<Renderer>().material.shader=Shader.Find("Unlit/Texture");
	
		pPlayer.name="Player";
		pPlayer.GetComponent<Renderer>().material.mainTexture = tex;

		pPlayer.transform.localScale    = new Vector3(0.6f,2.0f, -1f);   
		pPlayer.transform.parent	    = photoObj.transform;
		photoObj.transform.eulerAngles  = new Vector3 (-90f,0f, 0f);
		photoObj.transform.parent		= parent;
		
		return photoObj;
	}
}
