using UnityEngine;
using Tanks.TankControllers;
using Tanks.Explosions;






namespace Tanks.Pickups
{
    [RequireComponent(typeof(Rigidbody))]

	//This class acts as a base for all powerup pickups, defining all common behaviours.
    public abstract class PickupBase : MonoBehaviour, IDamageObject
    {
        //The name of this pickup.
		protected string m_PickupName;

		//The explosion definition for when this pickup is destroyed by player fire.
        protected ExplosionSettings m_DeathExplosion;

		//The minimum damage that must be dealt to the pickup in one shot before it considers itself hit.
        protected float m_MinDamage = 45;

		//The effect prefab to spawn when the pickup is collected.
		protected GameObject m_CollectionEffect;

		//Delay before the attached TankSeeker is set to active.
		protected float m_AttractorActivationDelay = 1.2f;

		//Internal cache for the layer of objects that are able to trigger pickup.
		private int m_PickupLayer;

		//Index of the player that destroyed this pickup, for damage attribution.
		private int m_DestroyingPlayer;

		//Number of ticks before the pickup collider is enabled.
		private int m_ColliderEnableCount = 2;

		private float m_AttractorActivationTime = 0f;

		//Internal reference to the TankSeeker that attracts this pickup to player tanks.
		private TankSeeker m_Attractor;

		//Whether this pickup is still alive. Required to implement IDamageObject.
        public bool isAlive { get; protected set; }

        protected virtual void Awake()
        {
            m_PickupLayer = LayerMask.NameToLayer("Players");
            GetComponent<Collider>().enabled = false;
            isAlive = true;
        }

        protected virtual void Start()
        {
            m_Attractor = GetComponent<TankSeeker>();

            if (m_Attractor != null)
            {
                m_AttractorActivationTime = Time.time + m_AttractorActivationDelay;
            }

			//Add this powerup to the GameManager so that it can be destroyed between rounds if needed.
            GameManager.s_Instance.AddPowerup(this);
        }

        protected virtual void Update()
        {
            //Tick down the collider enable count, and enable the collection collider when depleted.
			if (m_ColliderEnableCount >= 0)
            {
                m_ColliderEnableCount--;

                if (m_ColliderEnableCount == 0)
                {
                    GetComponent<Collider>().enabled = true;
                }
            }

			//Tick down the attractor enable count, and enable when depleted.
            if ((m_AttractorActivationTime > 0f) && (m_AttractorActivationTime <= Time.time))
            {
                m_Attractor.SetAttracted(true);
                m_AttractorActivationTime = 0f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //We only want to register triggers fired by objects in the player layer.
			if (other.gameObject.layer == m_PickupLayer)
            {
                //Create the collection effect. Immediate collection feedback on clients looks better.
				if (m_CollectionEffect != null)
                {
                    Instantiate(m_CollectionEffect, transform.position + Vector3.up, Quaternion.LookRotation(Vector3.up));
                }
            }
        }

		//Damage is called by any player fire, and implements IDamageObject. It allows drop pods to be destroyed by players as a denial tactic.
		//It also spawns a big nasty explosion that can take out any nearby tanks.
        public void Damage(float damage)
        {
            if (damage < m_MinDamage || !isAlive)
            {
                return;
            }

            isAlive = false;
            if (m_DeathExplosion != null && ExplosionManager.s_InstanceExists)
            {
                ExplosionManager.s_Instance.SpawnExplosion(transform.position, transform.up, gameObject, m_DestroyingPlayer, m_DeathExplosion, false);
            }

            GameObject.Destroy(gameObject);
        }

        public void SetDamagedBy(int playerNumber, string explosionId)
        {
            m_DestroyingPlayer = playerNumber;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

		//This method is overridden in child classes to implement specific powerup logic
		//As a standard, though, it updates the triggering tank with info to update its player's HUD.
        protected virtual void OnPickupCollected(GameObject targetTank)
        {
            targetTank.GetComponentInParent<TankManager>().AddPickupName(m_PickupName);
        }
    }
}
