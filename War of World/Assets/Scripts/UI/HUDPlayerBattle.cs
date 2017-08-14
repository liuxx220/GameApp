using UnityEngine;
using System.Collections;
using UnityEngine.UI;








namespace Tanks.UI
{
	//This class is responsible for displaying and updating the score display at the top of the HUD during multiplayer matches.
    public class HUDPlayerBattle : MonoBehaviour
	{
		//Reference to the target score text object.
		[SerializeField]
        protected EasyJoystick      m_EasyJoystick;

        [SerializeField]
        protected GameObject[]      m_mapBattle;


		protected virtual void Start()
		{

            //m_EasyJoystick.enable = false;
		}

        //--------------------------------------------------------------------------------
        // 技能按钮单击事件
        //--------------------------------------------------------------------------------
        public void OnClickedEvent( GameObject obj )
        {

            Vector2 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            m_EasyJoystick.enable = true;
            m_EasyJoystick.JoystickPositionOffset = screenPos;
        }
	}
}