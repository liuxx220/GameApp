using UnityEngine;
using System.Collections.Generic;
using Tanks.Data;
using Tanks.Effects;






namespace Tanks.TankControllers
{
    /// <summary>
    /// Player 的外观显示，主要包含装饰层的东西
    /// </summary>
	public class TankDisplay : MonoBehaviour
	{

		/// <summary>
		/// 装饰
		/// </summary>
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
