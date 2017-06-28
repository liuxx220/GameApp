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
        public GameObject fireDirection;
        public GameObject fireGuide;

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
        
        private float   m_LastLookUpdate;
        private float   m_TurretHeading;
        private Vector3 m_TargetFirePosition;

        public bool canShoot
        {
            get;
            set;
        }


        void Awake()
        {
            shootableMask   = LayerMask.GetMask("Shootable");
            gunParticles    = gunobject.GetComponent<ParticleSystem>();
            gunLine         = gunobject.GetComponent<LineRenderer>();
            gunAudio        = gunobject.GetComponent<AudioSource>();
            gunLight        = gunobject.GetComponent<Light>();
            fireDirection.SetActive(false);
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


        /// <summary>
        /// 设置枪口的朝向，即角色的面相
        /// </summary>
        public void SetDesiredFirePosition( Vector3 target )
        {
            m_TargetFirePosition = target;
            Vector3 toAimPos     = m_TargetFirePosition - transform.position;
            m_TurretHeading      = 90 - Mathf.Atan2(toAimPos.z, toAimPos.x) * Mathf.Rad2Deg;

            if (Time.realtimeSinceStartup - m_LastLookUpdate >= 0.1f )
            {
                m_LastLookUpdate = Time.realtimeSinceStartup;
            }
            transform.rotation = Quaternion.AngleAxis(m_TurretHeading, Vector3.up);
        }


	}
}