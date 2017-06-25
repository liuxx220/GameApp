using UnityEngine;
using Tanks.Rules.SinglePlayer;
using Tanks;
using Tanks.Effects;
using Tanks.Data;
using UnityEngine.UI;
using Tanks.Explosions;
using Tanks.TankControllers;

namespace Tanks.SinglePlayer
{
	/// <summary>
	/// Npc base class that registers death with single player rules processor
	/// </summary>
	public class Npc : MonoBehaviour
	{
		protected float m_MaximumHealth = 50;
		private float   m_CurrentHealth;
		private bool    m_IsDead = false;

        public GameObject hitObject;
        public GameObject deathObject;
        public AudioClip deathClip;                 
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 2;

        Transform       player;
        TankHealth      playerHealth;
        Animator        anim;
        AudioSource     enemyAudio;                     
        ParticleSystem  hitParticles;
        ParticleSystem  deathParticles;   
        CapsuleCollider capsuleCollider;           
        UnityEngine.AI.NavMeshAgent navagent;


        bool playerInRange;
        float timer;
		public bool isAlive { get { return !m_IsDead; } }
		
		void Awake()
		{
            m_CurrentHealth = m_MaximumHealth;
            player          = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth    = player.GetComponent<TankHealth>();
            navagent        = GetComponent<UnityEngine.AI.NavMeshAgent>();
            anim            = GetComponent<Animator>();
            enemyAudio      = GetComponent<AudioSource>();
            hitParticles    = hitObject.GetComponentInChildren<ParticleSystem>();
            hitParticles    = deathObject.GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
		}

        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }


        void OnTriggerExit(Collider other)
        {
            // If the exiting collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }


		void Update()
		{
            timer += Time.deltaTime;
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && m_CurrentHealth > 0)
            {
                // ... attack.
                Attack();
            }


            if (m_CurrentHealth > 0 && playerHealth.currentHealth > 0)
            {
                navagent.SetDestination(player.position);
            }
            else
            {
                navagent.enabled = false;
            }
		}

		protected virtual void OnDied()
		{
            // The enemy is dead.
            m_IsDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            anim.SetTrigger("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
			Destroy(gameObject, 2f);
		}

		public Vector3 GetPosition()
		{
			return transform.position;
		}

        void Attack()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage(attackDamage);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (m_IsDead)
                return;

            enemyAudio.Play();
            m_CurrentHealth -= amount;
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();

            if (m_CurrentHealth <= 0)
            {
                OnDied();
            }
        }


        public void StartSinking()
        {
           // 爲東中有事件保留
        }
	}
}