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
				if ( mousePressed)
				{
					Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
					float hitDist;
				    if (m_FloorPlane.Raycast(mouseRay, out hitDist))
					{
                        //SetMovementTarget(mouseRay.GetPoint(hitDist));
					}
                    
				}
				return mousePressed;
			}

			return false;
		}


		protected override bool DoMovementInput()
		{
			float y = Input.GetAxisRaw("Vertical");
			float x = Input.GetAxisRaw("Horizontal");

			Vector3 cameraDirection = new Vector3(x, y, 0);

			if (cameraDirection.sqrMagnitude > 0.01f)
			{
				// Get camera relative vectors
				Vector3 worldUp = Camera.main.transform.TransformDirection(Vector3.up);
				worldUp.y = 0;
				worldUp.Normalize();
				Vector3 worldRight = Camera.main.transform.TransformDirection(Vector3.right);
				worldRight.y = 0;
				worldRight.Normalize();

				Vector3 worldDirection = worldUp * y + worldRight * x;
				//Vector2 desiredDir = new Vector2(worldDirection.x, worldDirection.z);
                if (worldDirection.magnitude > 1)
				{
                    worldDirection.Normalize();
				}
                SetDesiredMovementDirection(worldDirection);

				return true;
			}


            if (!IsJoystickMoving() )
            {
                DisableMovement();
            }

			return false;
		}
	}
}