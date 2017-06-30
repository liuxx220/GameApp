using UnityEngine;








namespace Tanks.Shells
{
	[RequireComponent(typeof(Rigidbody))]
	public class PhysicsAffected : MonoBehaviour
	{
		[SerializeField]
		private float m_UpwardsModifier;
		private Rigidbody m_Rigidbody;

		private void Awake()
		{
			m_Rigidbody = GetComponent<Rigidbody>();
		}
		

		public void ApplyForce(float force, Vector3 position, float radius)
		{
			m_Rigidbody.AddExplosionForce(force, position, radius, m_UpwardsModifier);
		}
	}
}