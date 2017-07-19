using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Tanks.Data;
using Tanks.CameraControl;
using Tanks.Shells;
using Tanks.Explosions;
using Tanks.SinglePlayer;
using Random = UnityEngine.Random;






namespace Tanks.TankControllers
{
    /// <summary>
    /// ���������ģʽ
    /// </summary>
	enum SHOOTINGMODE
    {
        Shoot_continued,        // �������
        Shoot_pressUp,          // �ͷ����
        Shoot_pulse,            // ����ʽ����
        Shoot_Rocket,           // �����
    };


    public class TankShooting : NetworkBehaviour
    {

        #region ActionEvent
        public  event Action<int>       ammoQtyChanged;
        public  event Action<int>       overrideShellChanged;
        public  event Action            fired;
        #endregion


        public GameObject       fireDirection;
        public GameObject       gunHead;
        public GameObject       muzzleFlash;
        public GameObject       RedPoint;

        public float            coneAngle = 1.5f;
        int                     shootableMask;
        protected int           m_PlayerNumber = 1;
        float                   Esplasetimer;
        float                   effectsDisplayTime = 0;

        private SHOOTINGMODE    m_ShootMode = SHOOTINGMODE.Shoot_continued;
        private bool            m_FireInput;
        public  float           m_curLookatDeg;
        private float           m_fOldEulerAngles = 0;

        [SyncVar]
        private float           m_TurretHeading;

        /// <summary>
        /// Ԥ��Ŀ���ı���
        /// </summary>
        private Ray             m_shootRay = new Ray();
        private RaycastHit      m_shootHit;

        /// <summary>
        /// ��Ļ������ص�����
        /// </summary>
        private LineRenderer    m_LineRender;
        public  float           m_pulseSpeed = 1.5f;
        public  float           m_maxWidth = 0.5f;
        public  float           m_minWidth = 0.2f;

        private static TankShooting s_localTank;
        public static TankShooting s_LocalTank
        {
            get { return s_localTank; }
        }


        void Awake()
        {
            shootableMask       = LayerMask.GetMask("Shootable");
            m_LineRender        = gameObject.GetComponent<LineRenderer>();
            fireDirection.SetActive(false);
            muzzleFlash.SetActive(false);
            m_curLookatDeg      = transform.rotation.eulerAngles.y;
            m_fOldEulerAngles   = m_curLookatDeg;

        }

        [ClientCallback]
        private void Update()
        {
            if (s_localTank == null)
            {
                s_localTank = this;
            }

            m_shootRay.origin       = gunHead.transform.position;
            m_shootRay.direction    = gunHead.transform.forward;
            Physics.Raycast(m_shootRay, out m_shootHit, 100, shootableMask);
            DrawPulseLine();

            Esplasetimer += Time.deltaTime;
            if (m_ShootMode == SHOOTINGMODE.Shoot_pulse)
            {
                if (m_FireInput && Esplasetimer >= 1.0f && Time.timeScale != 0)
                {
                    Shoot();
                }
            }
            else
            {
                if (m_FireInput && Esplasetimer >= 0.15f && Time.timeScale != 0)
                {
                    Shoot();
                }
            }
            SmoothFaceDirection();

            effectsDisplayTime += Time.deltaTime;
            if (effectsDisplayTime >= 0.2f)
            {
                muzzleFlash.SetActive(false);
            }
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// ������������
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void DrawPulseLine()
        {
           
            float aniFactor = Mathf.PingPong(Time.time * m_pulseSpeed, 1.0f);
            aniFactor       = Mathf.Max( m_minWidth, aniFactor ) * m_maxWidth;
            m_LineRender.SetWidth(aniFactor, aniFactor );

            if( m_shootHit.transform )
            {
                m_LineRender.SetPosition(1, (m_shootHit.distance * Vector3.forward));
               if( RedPoint != null )
               {
                   RedPoint.GetComponent<Renderer>().enabled = true;
                   RedPoint.transform.position = m_shootHit.point + ( transform.position - m_shootHit.point ) * 0.01f;
                   RedPoint.transform.rotation = Quaternion.LookRotation( m_shootHit.normal, transform.up );
               }
            }
            else
            {
                float maxDist = 100.0f;
                m_LineRender.SetPosition( 1, maxDist * Vector3.forward );
                if( RedPoint != null )
               {
                   RedPoint.GetComponent<Renderer>().enabled = false;
               }
            }
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// �ж��Ƿ���Թ���
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public void BackShootingAngles( float fAngles )
        {
            m_fOldEulerAngles = fAngles;
            m_curLookatDeg    = fAngles;
            m_TurretHeading   = fAngles;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// �ж��Ƿ��ǳ������
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootContinued()
        {
            return m_ShootMode == SHOOTINGMODE.Shoot_continued;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// �ж��Ƿ���̧�����
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPressup()
        {
            return m_ShootMode == SHOOTINGMODE.Shoot_pressUp || m_ShootMode == SHOOTINGMODE.Shoot_pulse; 
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// �ж��Ƿ���̧�����
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPulse()
        {
            return m_ShootMode == SHOOTINGMODE.Shoot_pulse;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// ƽ����ת����
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void SmoothFaceDirection( )
        {
            if (Mathf.Abs(m_TurretHeading - m_curLookatDeg) < 0.001f)
                return;

            m_curLookatDeg     = Mathf.LerpAngle(m_curLookatDeg, m_TurretHeading, Time.deltaTime * 10);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }


        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// ����
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void Shoot()
        {

            Esplasetimer = 0f;
            muzzleFlash.SetActive(true);
            effectsDisplayTime = 0;

            Vector3 position    = gunHead.transform.position;
            Vector3 shotVector  = gunHead.transform.forward;

            // �������
            int randSeed        = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            CmdFire(shotVector, position, randSeed);
            if (m_ShootMode == SHOOTINGMODE.Shoot_continued)
            {
                FireEffect1( shotVector, position, randSeed );
            }
            if (m_ShootMode == SHOOTINGMODE.Shoot_pressUp)
            {
                FireEffect2(shotVector, position, randSeed );
                m_FireInput = false;
            }
            if(m_ShootMode == SHOOTINGMODE.Shoot_pulse)
            {
                FireEffect3(shotVector, position, randSeed );
                m_FireInput = false;
            }
            if( m_ShootMode == SHOOTINGMODE.Shoot_Rocket )
            {
                FireEffect4(shotVector, position, randSeed );
                m_FireInput = false;
            }
        }

        public void SetFireIsHeld(bool fireHeld)
        {
            m_FireInput = fireHeld;
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ����ǹ�ڵĳ��򣬼���ɫ������
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        public void SetDesiredFirePosition( Vector3 facedir )
        {
            float angle     = 90f - Mathf.Atan2(facedir.y, facedir.x) * Mathf.Rad2Deg;
            CmdSetLook(angle + m_fOldEulerAngles);
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ������ʱ����ôʵ�ָ����ӵ����ӵ�1�����Ч��
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect1( Vector3 shootVector, Vector3 position, int randSeed )
        {
            GameObject shellInstance = null;
            if (ExplosionManager.s_InstanceExists)
            {
                shellInstance = ExplosionManager.s_Instance.CreateVisualBullet(position, shootVector, 0, BulletClass.FiringExplosion);
            }

            shellInstance.SetActive(true);
            shellInstance.transform.localScale = Vector3.one;
            shellInstance.transform.position   = position;
            shellInstance.transform.forward    = shootVector;

            // �������������ײ
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            InstantBullet shell = shellInstance.GetComponent<InstantBullet>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ������ʱ����ôʵ�ָ����ӵ����ӵ�2�����Ч��
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect2( Vector3 shootVector, Vector3 position, int randSeed )
        {
            GameObject shellInstance = null;
            if (ExplosionManager.s_InstanceExists)
            {
                shellInstance = ExplosionManager.s_Instance.CreateVisualBullet(position, shootVector, 0, BulletClass.ClusterExplosion );
            }

            shellInstance.SetActive(true);
            shellInstance.transform.localScale = Vector3.one;
            shellInstance.transform.position = position;
            shellInstance.transform.forward = shootVector;

            // �������������ײ
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            ScatteringBullet shell = shellInstance.GetComponent<ScatteringBullet>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ������ʱ����ôʵ�ָ����ӵ����ӵ�2�����Ч��
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect3(Vector3 shootVector, Vector3 position, int randSeed )
        {
            GameObject shellInstance = null;
            if (ExplosionManager.s_InstanceExists)
            {
                shellInstance = ExplosionManager.s_Instance.CreateVisualBullet(position, shootVector, 0, BulletClass.PulseExplosion);
            }

            shellInstance.SetActive(true);
            shellInstance.transform.localScale = Vector3.one;
            shellInstance.transform.position = position;
            shellInstance.transform.forward = shootVector;

            // �������������ײ
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            PulseBullet shell = shellInstance.GetComponent<PulseBullet>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ������ʱ����ôʵ�ָ����ӵ����ӵ�2�����Ч��
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect4(Vector3 shootVector, Vector3 position, int randSeed)
        {
            GameObject shellInstance = null;
            if (ExplosionManager.s_InstanceExists)
            {
                shellInstance = ExplosionManager.s_Instance.CreateVisualBullet(position, shootVector, 0, BulletClass.RocketExplosion);
            }

            shellInstance.SetActive(true);
            shellInstance.transform.localScale = Vector3.one;
            shellInstance.transform.position = position;
            shellInstance.transform.forward = shootVector;

            // �������������ײ
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            Shell shell = shellInstance.GetComponent<Shell>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }


        /// ------------------------------------------------------------------------------------------------
        /// �����
        
        [Command]
        private void CmdSetLook( float turretHeading )
        {
            m_TurretHeading = turretHeading;
        }

        [Command]
        private void CmdFire( Vector3 shotVector, Vector3 position, int randSedd )
        {
            RpcFire(0, shotVector, position, randSedd);
        }

        [ClientRpc]
        private void RpcFire(int playerId, Vector3 shotVector, Vector3 position, int randSeed)
        {
            if (fired != null)
            {
                fired();
            }

            // If this fire message is for our own local player id, we skip. We already spawned a projectile
            if (playerId != TankShooting.s_LocalTank.m_PlayerNumber)
            {
                if (m_ShootMode == SHOOTINGMODE.Shoot_continued)
                {
                    FireEffect1(shotVector, position, randSeed);
                }
                if (m_ShootMode == SHOOTINGMODE.Shoot_pressUp)
                {
                    FireEffect2(shotVector, position, randSeed);
                    m_FireInput = false;
                }
                if (m_ShootMode == SHOOTINGMODE.Shoot_pulse)
                {
                    FireEffect3(shotVector, position, randSeed);
                    m_FireInput = false;
                }
                if (m_ShootMode == SHOOTINGMODE.Shoot_Rocket)
                {
                    FireEffect4(shotVector, position, randSeed);
                    m_FireInput = false;
                }
            }
        }
	}
}