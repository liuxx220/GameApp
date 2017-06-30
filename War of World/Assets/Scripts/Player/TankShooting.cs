using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Tanks.Data;
using Tanks.CameraControl;
using Tanks.Shells;
using Tanks.Explosions;
using Tanks.SinglePlayer;






namespace Tanks.TankControllers
{
    /// <summary>
    /// 武器的射击模式
    /// </summary>
	enum SHOOTINGMODE
    {
        Shoot_continued,        // 连续射击
        Shoot_pressUp,          // 释放射击
    };


    public class TankShooting : MonoBehaviour
	{
        public GameObject       gunobject;
        public GameObject       fireDirection;

        Ray                     shootRay = new Ray();
        RaycastHit              shootHit;
        int                     shootableMask;
        ParticleSystem          gunParticles;
        LineRenderer            gunLine;
        AudioSource             gunAudio;
        float                   Esplasetimer;
        float                   effectsDisplayTime = 0.2f;

        private SHOOTINGMODE    m_ShootMode = SHOOTINGMODE.Shoot_continued;
        private bool            m_FireInput;
        private float           m_curLookatDeg;
        private float           m_TurretHeading;
        

        void Awake()
        {
            shootableMask   = LayerMask.GetMask("Shootable");
            gunParticles    = gunobject.GetComponent<ParticleSystem>();
            gunLine         = gunobject.GetComponent<LineRenderer>();
            gunAudio        = gunobject.GetComponent<AudioSource>();
            fireDirection.SetActive(false);
            m_curLookatDeg  = transform.rotation.eulerAngles.y;
        }


        void Update()
        {
            Esplasetimer += Time.deltaTime;
            if (m_FireInput && Esplasetimer >= 0.15f && Time.timeScale != 0)
            {
                Shoot();
            }

            SmoothFaceDirection();

            if (Esplasetimer >= 0.15f * effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断是否可以攻击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsCanShooting()
        {
            return true;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断是否是持续射击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootContinued()
        {
            return m_ShootMode == SHOOTINGMODE.Shoot_continued;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断是否是抬起射击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPressup()
        {
            return m_ShootMode == SHOOTINGMODE.Shoot_pressUp; ;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 平滑旋转面向
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void SmoothFaceDirection( )
        {
            if (Mathf.Abs(m_TurretHeading - m_curLookatDeg) < 0.001f)
                return;

            m_curLookatDeg     = Mathf.LerpAngle(m_curLookatDeg, m_TurretHeading, Time.deltaTime * 10);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }


        public void DisableEffects()
        {
            gunLine.enabled = false;
        }


        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 攻击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void Shoot()
        {
            Esplasetimer = 0f;
            gunAudio.Play();
            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, gunobject.transform.position);

            shootRay.origin     = gunobject.transform.position;
            shootRay.direction  = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, 50f, shootableMask))
            {
                Npc enemyHealth = shootHit.collider.GetComponent<Npc>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(20, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 50f);
            }
        }

        public void SetFireIsHeld(bool fireHeld)
        {
            m_FireInput = fireHeld;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 设置枪口的朝向，即角色的面相
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public void SetDesiredFirePosition( Vector3 target )
        {
            Vector3 toAimPos     = target - transform.position;
            m_TurretHeading      = 90 - Mathf.Atan2(toAimPos.z, toAimPos.x) * Mathf.Rad2Deg;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里暂时先这么实现各种子弹，子弹1射击的效果
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect1()
        {

        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里暂时先这么实现各种子弹，子弹2射击的效果
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect2()
        {

        }


        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 创建一个子弹实体
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private Shell FireVisualBullet( Vector3 shortDir, Vector3 pos, int randSeed )
        {
            if( ExplosionManager.s_InstanceExists )
            {
                ExplosionManager.s_Instance.SpawnExplosion( pos, shortDir, null, 0,  null, true );
            }

            Rigidbody bullet          = null;// Instantiate<Rigidbody>();
            bullet.transform.position = pos;
            bullet.velocity           = shortDir;


            // 当射击的时候不与自己碰撞
            Shell shell = bullet.GetComponent<Shell>();
            shell.Setup(1, null, randSeed);
            Physics.IgnoreCollision(shell.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            return shell;
        }
	}
}