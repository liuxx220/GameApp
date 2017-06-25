using System;
using UnityEngine;
using Tanks.Utilities;
using Tanks.TankControllers;








namespace Tanks.UI
{

	//Class that handles main menu UI and transitions
	public class BattleUI : MonoBehaviour
	{
        private Transform       player;
        private TankInputModule Input;
        protected void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Input = player.GetComponent<TankInputModule>();
        }

        float fUpdateShootTime = 0;
		protected void Update()
		{
            fUpdateShootTime += Time.deltaTime;
            if( fUpdateShootTime > 0.1f )
            {
                fUpdateShootTime = 0;
                if (Input != null)
                {
                    Input.SetFireIsHeld(false);
                }
            }
		}

        public void OnBattleClicked()
        {
            if( Input != null )
            {
                fUpdateShootTime = 0;
                Input.SetFireIsHeld(true);
            }
        }
	}
}