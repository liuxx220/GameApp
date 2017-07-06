using UnityEngine;
using Tanks.Explosions;







namespace Tanks.Shells
{
	[RequireComponent(typeof(Rigidbody))]
	public class Shell : MonoBehaviour
	{
		//References to ExplosionSettings ScriptableObject that defines the explosion parameters for this shell.
		[SerializeField]
		protected ExplosionSettings m_ExplosionSettings;

		//Variables for controlling shell bounciness - how many times to bounce, force decay per bounce, and bounce direction
		[SerializeField]
		protected int m_Bounces = 0;

		[SerializeField]
        protected float m_BounceForceDecay = 1.05f;

		[SerializeField]
		protected Vector3 m_BounceAdditionalForce = Vector3.up;

		//Minimum height that a shell can be in world coordinates before self-destructing.
		[SerializeField]
		protected float m_MinY = -1;

		//Modifier for shell velocity
		[SerializeField]
		protected float m_SpeedModifier = 1;

		//Modifier for the shell's spin rate.
		[SerializeField]
		protected float m_Spin = 720;

        [SerializeField]
        private float m_UpwardsModifier;

		//internal reference to ignore collider and ignore ticks.
		private Collider m_TempIgnoreCollider;
		private int m_TempIgnoreColliderTime = 2;

		//Random seed for spawning debris.
		private int m_RandSeed;

		//The current rotation of the shell.
		private float m_CurrentSpinRot;

		//Internal reference to this shell's rigidbody
		private Rigidbody m_CachedRigidBody;

		//Internal list of trail renderers attached to this shell
		private TrailRenderer[] m_ShellTrails;

		//Accessor for speed modifier
		public float speedModifier
		{
			get { return m_SpeedModifier; }
		}

		private void Awake()
		{
			m_CachedRigidBody   = GetComponent<Rigidbody>();
			m_ShellTrails       = GetComponentsInChildren<TrailRenderer>();
		}

        private void Start()
        {

        }


		public void Setup(int owningPlayerId, Collider ignoreCollider, int seed)
		{

			if (ignoreCollider != null)
			{
				// Ignore collisions temporarily
				Physics.IgnoreCollision(GetComponent<Collider>(), ignoreCollider, true);
				m_TempIgnoreCollider = ignoreCollider;
				m_TempIgnoreColliderTime = 2;
			}

			m_RandSeed = seed;
			if (m_SpeedModifier != 1)
			{
				ConstantForce force = gameObject.AddComponent<ConstantForce>();
				force.force = Physics.gravity * m_CachedRigidBody.mass * (m_SpeedModifier - 1);
			}

			transform.forward = m_CachedRigidBody.velocity;
		}

		private void FixedUpdate()
		{
			if (m_TempIgnoreCollider != null)
			{
				m_TempIgnoreColliderTime--;
				if (m_TempIgnoreColliderTime <= 0)
				{
					Physics.IgnoreCollision(GetComponent<Collider>(), m_TempIgnoreCollider, false);
					m_TempIgnoreCollider = null;
				}
			}
		}
			
		private void Update()
		{
			transform.forward = m_CachedRigidBody.velocity;

			// Spin the projectile
			m_CurrentSpinRot += m_Spin * Time.deltaTime * m_CachedRigidBody.velocity.magnitude;
			transform.Rotate(Vector3.forward, m_CurrentSpinRot, Space.Self);

			if (transform.position.y <= m_MinY)
			{
				Destroy(gameObject);
			}
		}
			
		// Create explosions on collision
		private void OnCollisionEnter(Collision c)
		{
            Vector3 explosionNormal = c.contacts.Length > 0 ? c.contacts[0].normal : Vector3.up;
            Vector3 explosionPosition = c.contacts.Length > 0 ? c.contacts[0].point : transform.position;
            if (ExplosionManager.s_InstanceExists)
            {
              
            }

			if (m_Bounces > 0)
			{
				m_Bounces--;

				Vector3 refl = Vector3.Reflect(-c.relativeVelocity, explosionNormal);
				refl *= m_BounceForceDecay;
				refl += m_BounceAdditionalForce;
				m_CachedRigidBody.velocity = refl;
				if (m_TempIgnoreCollider != null)
				{
					Physics.IgnoreCollision(GetComponent<Collider>(), m_TempIgnoreCollider, false);
				}
				m_TempIgnoreCollider = c.collider;
				m_TempIgnoreColliderTime = 2;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnDestroy()
		{
			for(int i = 0; i < m_ShellTrails.Length; i++)
			{
				TrailRenderer trail = m_ShellTrails[i];
				if (trail != null)
				{
					trail.transform.SetParent(null);
					trail.autodestruct = true;
				}
			}
		}

        public void ApplyForce(float force, Vector3 position, float radius)
        {
            m_CachedRigidBody.AddExplosionForce(force, position, radius, m_UpwardsModifier);
        }
	}
}