using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Tanks.Data;
using Tanks.CameraControl;
using Tanks.Shells;
using Tanks.Explosions;
using Tanks.SinglePlayer;
using Tanks.Rules;
using Random = UnityEngine.Random;






namespace Tanks.TankControllers
{

    public class TankShooting : NetworkBehaviour
    {

        #region ActionEvent
        //public  event Action<int>       ammoQtyChanged;
        public  event Action<int>       overrideShellChanged;
        public  event Action            fired;
        #endregion

        [SerializeField]
        protected GameObject    m_WeaponHP;             // �����ҵ�
        protected GameObject    m_EquipWeapon = null;  // ��ǰװ��������
        public GameObject       gunHead;
        public GameObject       muzzleFlash;
        public GameObject       RedPoint;


        float                   Esplasetimer;
        int                     shootableMask;

 
        private bool            m_FireInput;
        public  float           m_curLookatDeg = 0;
        private float           m_fOldEulerAngles = 0;
        private float           m_fFireCommandCD = 0;

        [SyncVar]
        private float           m_TurretHeading;
        private float           m_LastLookUpdate;

        /// <summary>
        /// Ԥ��Ŀ���ı���
        /// </summary>
        private Ray             m_shootRay = new Ray();
        private RaycastHit      m_shootHit;

        /// <summary>
        /// ��Ļ������ص�����
        /// </summary>
        private LineRenderer    m_LineRender;

        /// <summary>
        /// ��ǰ������������Ϣ
        /// </summary>
        private TankWeaponDefinition    m_WeaponProtol;

        /// <summary>
        /// ��ǰ������������Ϣ
        /// </summary>
        private int             m_curShootBullets = 0;

		private int 			m_curBullets = 0;

		/// <summary>
		/// ��Դ���, ��Ч������
		/// </summary>
		AudioSource                         m_AudioSource;

		/// <summary>
		/// �滻����
		/// </summary>
		private float m_ReplaceClipTimer;


		//effect 
		public GameObject shot;
		public GameObject effectShot;
		private GameObject effectShot1;
		private GameObject effectShot2;
		private GameObject effectShot3;
		private ParticleSystem particleEffectShot1;		
		private ParticleSystem particleEffectShot2;		
		private ParticleSystem particleEffectShot3;		
		private Vector3 m_PlayerPos;



        void Awake()
        {
            shootableMask       = LayerMask.GetMask("Shootable");
            m_curLookatDeg      = transform.rotation.eulerAngles.y;
            m_TurretHeading     = m_curLookatDeg;
            m_fOldEulerAngles   = m_curLookatDeg;
            m_LastLookUpdate    = Time.realtimeSinceStartup;
			m_AudioSource       = transform.Find("GunAudio").GetComponent<AudioSource>();
            RedPoint.SetActive(false);
            muzzleFlash.SetActive(false);
			m_ReplaceClipTimer = 0f;

			//m_PlayerPos = transform.position;

			effectShot1 = Instantiate( effectShot, transform.position,  transform.rotation ) as GameObject;
			particleEffectShot1 = effectShot1.GetComponentInChildren<ParticleSystem>() as ParticleSystem;
			effectShot2 = Instantiate( effectShot, transform.position, transform.rotation ) as GameObject;
			particleEffectShot2 = effectShot2.GetComponentInChildren<ParticleSystem>() as ParticleSystem;
			effectShot3 = Instantiate( effectShot, transform.position, transform.rotation ) as GameObject;
			particleEffectShot3 = effectShot3.GetComponentInChildren<ParticleSystem>() as ParticleSystem;
        }


        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// ��ɫ��װ������Ӧ��֪ͨ�����ͻ���
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        public void SetPlayerWeapon(int nWeaponID)
        {
            if (nWeaponID < 0)
                return;

            TankWeaponDefinition def = GameSettings.s_Instance.GetWeaponbyID(nWeaponID);
            if (def != null)
            {
                if( m_EquipWeapon != null )
                {
                    m_EquipWeapon.transform.parent = null;
                    m_EquipWeapon.SetActive(false);
                    GameObject.Destroy(m_EquipWeapon, 2f);
                }

                UnityEngine.Object obj          = AssetManager.Get().GetResources(def.perfab);
                GameObject weapon               = GameObject.Instantiate(obj) as GameObject;
                weapon.transform.parent         = m_WeaponHP.transform;
                weapon.transform.localPosition  = Vector3.zero;
                weapon.transform.localRotation  = Quaternion.identity;
                weapon.transform.localScale     = Vector3.one;
                m_WeaponProtol = def;
                m_EquipWeapon  = weapon;


				// װ������
				m_curBullets = m_WeaponProtol.m_nBullets;
                
            }
        }

        [ClientCallback]
        private void Update()
        {
            if (m_WeaponProtol == null)
                return;

            Esplasetimer += Time.deltaTime;
			if (m_FireInput && Esplasetimer >= 0.2f && m_curBullets >= m_WeaponProtol.m_ShootBulletNumPer && m_curShootBullets < m_WeaponProtol.m_ShootBulletNumPer)
            {
                ShootFire();
            }

			if (m_curBullets < m_WeaponProtol.m_ShootBulletNumPer)
            {
				m_ReplaceClipTimer += Time.deltaTime;
                m_FireInput = false;
            }

			if (m_curShootBullets >= m_WeaponProtol.m_ShootBulletNumPer)
			{
				m_FireInput = false;
				m_curShootBullets = 0;
			}

			if (m_ReplaceClipTimer >= 2f) 
			{
				m_curBullets = m_WeaponProtol.m_nBullets;
				m_ReplaceClipTimer = 0f;
			}
            
            SmoothFaceDirection();
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// ������������
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void DrawPulseLine()
        {
           
            float aniFactor = Mathf.PingPong(Time.time * 1.5f, 1.0f);
            aniFactor       = Mathf.Max( 0.2f, aniFactor ) * 0.5f;
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
        /// �ж��Ƿ��ǳ������
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootContinued()
        {
            return m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_continued;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// �ж��Ƿ���̧�����
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPressup()
        {
            return m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pressUp || m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pulse; 
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// �ж��Ƿ���̧�����
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPulse()
        {
            return m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pulse;
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
        void Fire()
        {
			m_curShootBullets = 0;
            CmdFire();
        }

       
        public void SetFireIsHeld(bool fireHeld)
        {
            // ����CD
            m_fFireCommandCD += Time.deltaTime;
            if (m_fFireCommandCD < 1.0f)
                return;

            m_fFireCommandCD = 0f;
            if (m_FireInput != fireHeld && fireHeld )
            {
                Fire();
            }
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

            if (Time.realtimeSinceStartup - m_LastLookUpdate >= 0.2f)
            {
                CmdSetLook(angle + m_fOldEulerAngles);
                m_LastLookUpdate = Time.realtimeSinceStartup;
            }
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
            //shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
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
        private void CmdFire()
        {
            RpcFire();
        }

        [ClientRpc]
        private void RpcFire()
        {
            if (fired != null)
            {
                fired();
            }

            // Զ����ҵĻ�������߼�
            if( !hasAuthority )
                m_FireInput = true;
        }

        private void ShootFire( )
        {
            Esplasetimer        = 0f;
            Vector3 position    = gunHead.transform.position;
            Vector3 shotVector  = gunHead.transform.forward;
            int randSeed        = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

			//û�ӵ���
			if (m_curBullets <= 0) {
				// ������ǹ����Ч

				return;
			}
				
			// һ��������ĵ��ӵ�����
			if (m_curBullets >= m_WeaponProtol.m_ShootBulletNumPer) 
			{
				m_curBullets -= m_WeaponProtol.m_ShootBulletNumPer;
			} 
			else 
			{
				m_ReplaceClipTimer = 0f;
			}

			m_AudioSource.Play ();
				
			// ���������Ч
            if (m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_continued)
            {
                FireEffect1(shotVector, position, randSeed);
            }
            if (m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pressUp)
            {
                FireEffect2(shotVector, position, randSeed);
            }
            if (m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pulse)
            {
                FireEffect3(shotVector, position, randSeed);
            }
            if (m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_Rocket)
            {
                FireEffect4(shotVector, position, randSeed);
            }

			m_curShootBullets++;

			Firing ();
        }

		void  Firing()
		{
			// ショットオブジェクトが存在するか?.
			if (shot) {
				Vector3 v3 = transform.position;
				v3.y += 3f;

				// 弾1作成.
				//Instantiate( shot, transform.position, transform.rotation );
				effectShot1.transform.rotation = transform.rotation;
				effectShot1.transform.position = effectShot1.transform.forward * 1.5f;
				effectShot1.transform.position = v3;
				effectShot1.transform.localScale = new Vector3 (10f, 10f, 10f);
				// エフェクトの角度を補正.
				//  - 素材の方向補正.90fを足しているのは単に素材の方向を補正するため.
				//  - 57.29578fで割ると丁度プレイヤーの前方位置になる.(ParticleSystemのstartRotationはこれをしないとずれる).
				//particleEffectShot1.startRotation = ( transform.rotation.eulerAngles.y + 90f ) / 57.29578f;
				// エフェクトの再生.
				particleEffectShot1.Play ();

				// 一定時間待つ.
				//yield return new WaitForSeconds( fireInterval );

				// 弾2作成.
				//Instantiate( shot, transform.position, transform.rotation );
				effectShot2.transform.rotation = transform.rotation;
				effectShot2.transform.position = effectShot2.transform.forward * 1.5f;
				effectShot2.transform.position = v3;
				// エフェクトの角度を補正.
				//  - 素材の方向補正.90fを足しているのは単に素材の方向を補正するため.
				//  - 57.29578fで割ると丁度プレイヤーの前方位置になる.(ParticleSystemのstartRotationはこれをしないとずれる).
				//particleEffectShot2.startRotation = ( transform.rotation.eulerAngles.y + 90f ) / 57.29578f;
				// エフェクトの再生.
				//particleEffectShot2.Play();

				// 一定時間待つ.
				//yield return new WaitForSeconds( fireInterval );

				// 弾3作成.
				//Instantiate( shot, transform.position, transform.rotation );
				effectShot3.transform.rotation = transform.rotation;
				effectShot3.transform.position = effectShot3.transform.forward * 1.5f;
				effectShot3.transform.position = v3;
				// エフェクトの角度を補正.
				//  - 素材の方向補正.90fを足しているのは単に素材の方向を補正するため.
				//  - 57.29578fで割ると丁度プレイヤーの前方位置になる.(ParticleSystemのstartRotationはこれをしないとずれる).
				//particleEffectShot3.startRotation = ( transform.rotation.eulerAngles.y + 90f ) / 57.29578f;
				// エフェクトの再生.
				//particleEffectShot3.Play();

				// 次の発射まで一定時間待つ.
				//yield return new WaitForSeconds( fireSetInterval );

				// 発射終了.
				//isFiring = false;
			}
		}
	}
}