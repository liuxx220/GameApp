using UnityEngine;
using UnityEngine.EventSystems;
using System;






namespace Tanks.TankControllers
{
	/// <summary>
	/// Input module that handles keyboard controls
	/// </summary>
	public class TankKeyboardInput : TankInputModule
	{

		protected override bool DoFiringInput()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return false;
			}

			// Mouse pos
            if (Input.mousePresent && !IsJoystickMoving() )
			{
				bool mousePressed = Input.GetMouseButton(0);
                //if ( mousePressed)
                //{
                //    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                //    float hitDist;
                //    if (m_FloorPlane.Raycast(mouseRay, out hitDist))
                //    {
                //        //SetMovementTarget(mouseRay.GetPoint(hitDist));
                //    }
                    
                //}
				return mousePressed;
			}

			return false;
		}


		protected override bool DoMovementInput()
		{
            //return true;
			float y = Input.GetAxisRaw("Vertical");
			float x = Input.GetAxisRaw("Horizontal");

			Vector3 cameraDirection = new Vector3(x, y, 0);
			if (cameraDirection.sqrMagnitude < 0.01f)
			{
                if (!IsJoystickMoving() )
                {
                    DisableMovement();
                }
                return false;
            }

            Vector3 worldDirection = x * screenMovementRight + y * screenMovementForward;
            if (worldDirection.magnitude > 1)
			{
                worldDirection.Normalize();
			}
            SetMovementDirection(worldDirection);
			return true;
		}
	}
}