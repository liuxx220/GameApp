using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Tanks.Data;
using Tanks.CameraControl;
using Tanks.Shells;
using Tanks.SinglePlayer;






namespace Tanks.TankControllers
{
	//This class is responsible for all firing behaviour for the tank.
    public class TankShooting : MonoBehaviour
	{
        public GameObject gunobject;
        public int      damagePerShot = 20;
        public float    timeBetweenBullets = 0.15f;
        public float    range = 100f;

        float           timer;
        Ray             shootRay = new Ray();
        RaycastHit      shootHit;
        int             shootableMask;
        ParticleSystem  gunParticles;
        LineRenderer    gunLine;
        AudioSource     gunAudio;
        Light           gunLight;
        float           effectsDisplayTime = 0.2f;


        private bool    m_FireInput;
        private bool    m_WasFireInput;
        private Vector3 m_TargetFirePosition;

        public bool canShoot
        {
            get;
            set;
        }

		//Local static reference to the local player's tank for input toggle purposes.
		private static TankShooting s_localTank;

		public static TankShooting s_LocalTank
		{
			get { return s_localTank; }
		}

        void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable");
            gunParticles = gunobject.GetComponent<ParticleSystem>();
            gunLine = gunobject.GetComponent<LineRenderer>();
            gunAudio = gunobject.GetComponent<AudioSource>();
            gunLight = gunobject.GetComponent<Light>();
        }


        void Update()
        {
            timer += Time.deltaTime;

            if (m_FireInput && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                Shoot();
            }

            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                DisableEffects();
            }
        }


        public void DisableEffects()
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot()
        {
            timer = 0f;

            gunAudio.Play();
            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, gunobject.transform.position);

            shootRay.origin     = gunobject.transform.position;
            shootRay.direction  = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                Npc enemyHealth = shootHit.collider.GetComponent<Npc>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }

        public void SetFireIsHeld(bool fireHeld)
        {
            m_FireInput = fireHeld;
        }

        public void SetDesiredFirePosition( Vector3 target )
        {
            m_TargetFirePosition = target;
            Vector3 toAimPos     = m_TargetFirePosition - transform.position;
            toAimPos.Normalize();
            transform.LookAt(toAimPos);  
        }
	}
}