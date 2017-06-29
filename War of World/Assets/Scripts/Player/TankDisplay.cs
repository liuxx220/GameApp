using UnityEngine;
using System.Collections.Generic;
using Tanks.Data;
using Tanks.Effects;

namespace Tanks.TankControllers
{
	//This class acts as a general visual control class for all tank variants.
	//It allows tank chassis to be easily swapped out from the common controller scripts in-game, and doubles as an easy interface for menu-related functionality.
	public class TankDisplay : MonoBehaviour
	{
		//Root object for all tank mesh renderer/mesh objects. Used to mass enable/disable them.
		[SerializeField]
		protected GameObject m_TankRendererParent;

		// Stored reference to TankManager
		private TankManager m_TankManager;


		// Collection of all attached decorations
		private List<Decoration> m_AttachedDecorations;

		private void Awake()
		{
            m_AttachedDecorations = new List<Decoration>();
		}

		private void Start()
		{

		}

		private void Update()
		{
			
		}

		public void Init(TankManager tankManager)
		{
			this.m_TankManager = tankManager;
			SetTankColor(Color.white);
		}


		//Stops and clears the particles for tank movement.
		public void StopTrackParticles()
		{
			
		}

		//Hides the shadow renderer object.
		public void HideShadow()
		{
			
		}
			
		public void SetTankColor(Color newColor)
		{

		}

		public void SetTankDecoration(int newDecorationId, int newMaterialIndex, bool destroyDecorations = true)
		{
			//Iterate through all decoration points and clear them
			if (destroyDecorations)
			{
				for (int i = 0; i < m_AttachedDecorations.Count; i++)
				{
					Decoration decoration = m_AttachedDecorations[i];
					if (decoration != null)
					{
						Destroy(decoration.gameObject);
					}
				}
			}
		}
			
		// Detach all tank decoration objects
		public void DetachDecorations()
		{
			for (int i = 0; i < m_AttachedDecorations.Count; i++)
			{
				Decoration decoration = m_AttachedDecorations[i];
				if (decoration != null)
				{
					decoration.Detach();
				}
			}
		}


		//Gets the total bounds of this tank model and its equipped decorations for viewport fitting in the main menu.
		public Bounds GetTankBounds()
		{
			Bounds? objectBounds = null;


            foreach (Renderer rend in GetComponentsInChildren<SkinnedMeshRenderer>())
			{
				if (rend.enabled && rend.gameObject.activeInHierarchy)
				{
					Bounds rendBounds = rend.bounds;
					// Only on bounds with volume
					if (rendBounds.size.x > 0 &&
					    rendBounds.size.y > 0 &&
					    rendBounds.size.z > 0)
					{
						if (objectBounds.HasValue)
						{
							Bounds boundVal = objectBounds.Value;
							boundVal.Encapsulate(rendBounds);
							objectBounds = boundVal;
						}
						else
						{
							objectBounds = rend.bounds;
						}
					}
				}
			}

			// Encapsulate decorations
			foreach (Decoration dec in m_AttachedDecorations)
			{
				if (dec != null)
				{
					Bounds? decBounds = dec.GetDecorationBounds();
					if (decBounds.HasValue)
					{
						Bounds boundVal = objectBounds.Value;
						boundVal.Encapsulate(decBounds.Value);
						objectBounds = boundVal;
					}
				}
			}

			return objectBounds.Value;
		}
	}
}
