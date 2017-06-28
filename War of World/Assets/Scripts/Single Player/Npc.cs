using UnityEngine;
using Tanks.Rules.SinglePlayer;
using Tanks;
using Tanks.Map;
using Tanks.Effects;
using Tanks.Data;
using Tanks.Networking;
using UnityEngine.UI;
using Tanks.Explosions;
using Tanks.TankControllers;
using TanksNetworkPlayer = Tanks.Networking.NetworkPlayer;






namespace Tanks.SinglePlayer
{
	/// <summary>
	/// Npc base class that registers death with single player rules processor
	/// </summary>
	public class Npc : MonoBehaviour
	{
		protected float     m_MaximumHealth = 50;
		private float       m_CurrentHealth;
		private bool        m_IsDead = false;

        public GameObject   hitObject;
        public GameObject   deathObject;
        public AudioClip    deathClip;                 
        public float        timeBetweenAttacks = 0.5f;
        public int          attackDamage = 2;

        Transform           player;
        TanksNetworkPlayer  playerHealth;
        Animator            anim;
        AudioSource         enemyAudio;                     
        ParticleSystem      hitParticles;  
        CapsuleCollider     capsuleCollider;           
        UnityEngine.AI.NavMeshAgent navagent;


        bool playerInRange;
        float timer;
		public bool isAlive { get { return !m_IsDead; } }
		
		void Awake()
		{
            m_CurrentHealth = m_MaximumHealth;
            player          = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth    = player.GetComponent<TanksNetworkPlayer>();
            navagent        = GetComponent<UnityEngine.AI.NavMeshAgent>();
            anim            = GetComponent<Animator>();
            enemyAudio      = GetComponent<AudioSource>();
            hitParticles    = hitObject.GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
		}

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }


        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }


		void Update()
		{
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && playerInRange && m_CurrentHealth > 0)
            {
                // ... attack.
                Attack();
            }

            Vector3 dir = transform.position - player.position;
            if( dir.magnitude <= 2.5f )
            {
                navagent.enabled = false;
                return;
            }

            else if (m_CurrentHealth > 0 && playerHealth.m_CurrentHealth > 0)
            {
                if (navagent.enabled )
                    navagent.SetDestination(player.position);
            }
            else
            {
                navagent.enabled = false;
            }
		}

		protected virtual void OnDied()
		{
            m_IsDead = true;
            capsuleCollider.isTrigger = true;

            anim.SetTrigger("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play();
            SpawnManager.s_Instance.DestoryEnemy(gameObject);
		}

        void Attack()
        {
            timer = 0f;
            if (playerHealth.m_CurrentHealth > 0)
            {
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