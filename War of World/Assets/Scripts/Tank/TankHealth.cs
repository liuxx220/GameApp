using System;
using UnityEngine;
using UnityEngine.Networking;
using Tanks.Map;
using Tanks.Explosions;
using Tanks.Data;






namespace Tanks.TankControllers
{
	//This class is responsible for handling tank health and damage
    /*
    public class TankHealth : MonoBehaviour
	{
        public int          startingHealth = 100;
        public int          currentHealth;
        public float        sinkSpeed = 2.5f;
        public int          scoreValue = 10;
        public AudioClip    deathClip;


        Animator            anim;
        AudioSource         enemyAudio;
        ParticleSystem      hitParticles;
        CapsuleCollider     capsuleCollider;
        bool                isDead;
        bool                isSinking;


        void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            currentHealth = startingHealth;
        }

        void Update()
        {
            if (isSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (isDead)
                return;

            enemyAudio.Play();

            currentHealth -= amount;

            hitParticles.transform.position = hitPoint;
            hitParticles.Play();

            if (currentHealth <= 0)
            {
                Death();
            }
        }

        public void TakeDamage(int amount )
        {
            if (isDead)
                return;

            enemyAudio.Play();

            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            isDead = true;

            capsuleCollider.isTrigger = true;

            anim.SetTrigger("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }
	}
    */
}
