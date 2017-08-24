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
        protected GameObject    m_WeaponHP;             // 武器挂点
        protected GameObject    m_EquipWeapon = null;  // 当前装备的武器
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
        /// 预测目标点的变量
        /// </summary>
        private Ray             m_shootRay = new Ray();
        private RaycastHit      m_shootHit;

        /// <summary>
        /// 屏幕射线相关的数据
        /// </summary>
        private LineRenderer    m_LineRender;

        /// <summary>
        /// 当前武器的配置信息
        /// </summary>
        private TankWeaponDefinition    m_WeaponProtol;

        /// <summary>
        /// 当前武器的配置信息
        /// </summary>
        private int             m_curShootBullets = 0;

		/// <summary>
		/// 资源相关, 音效控制器
		/// </summary>
		AudioSource                         m_AudioSource;

		/// <summary>
		/// 替换弹夹
		/// </summary>
		private float m_ReplaceClipTimer;


        void Awake()
        {
            shootableMask       = LayerMask.GetMask("Shootable");
            m_curLookatDeg      = transform.rotation.eulerAngles.y;
            m_TurretHeading     = m_curLookatDeg;
            m_fOldEulerAngles   = m_curLookatDeg;
            m_LastLookUpdate    = Time.realtimeSinceStartup;
			m_AudioSource       = transform.FindChild("GunAudio").GetComponent<AudioSource>();
            RedPoint.SetActive(false);
            muzzleFlash.SetActive(false);
			m_ReplaceClipTimer = 0f;
        }


        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// 角色安装武器，应该通知其他客户端
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


				// 装弹操作
				m_curShootBullets = m_WeaponProtol.m_nBullets;
                
            }
        }

        [ClientCallback]
        private void Update()
        {
            if (m_WeaponProtol == null)
                return;

            Esplasetimer += Time.deltaTime;
            if (m_FireInput && Esplasetimer >= 0.2f && m_curShootBullets >= m_WeaponProtol.m_ShootBulletNumPer)
            {
                ShootFire();
            }

            if (m_curShootBullets < m_WeaponProtol.m_ShootBulletNumPer)
            {
				m_ReplaceClipTimer += Time.deltaTime;
                m_FireInput = false;
            }

			if (m_ReplaceClipTimer >= 2f) 
			{
				m_curShootBullets = m_WeaponProtol.m_nBullets;
				m_ReplaceClipTimer = 0f;
			}
            
            SmoothFaceDirection();
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新脉冲摄像
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
        /// 判断是否是持续射击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootContinued()
        {
            return m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_continued;
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断是否是抬起射击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPressup()
        {
            return m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pressUp || m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pulse; 
        }

        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断是否是抬起射击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        public bool IsShootPulse()
        {
            return m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pulse;
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


        /// ----------------------------------------------------------------------------------------------
        /// <summary>
        /// 攻击
        /// </summary>
        /// ----------------------------------------------------------------------------------------------
        void Fire()
        {
            CmdFire();
        }

       
        public void SetFireIsHeld(bool fireHeld)
        {
            // 公共CD
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
        /// 设置枪口的朝向，即角色的面相
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
        /// 这里暂时先这么实现各种子弹，子弹1射击的效果
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

            // 忽略与自身的碰撞
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            InstantBullet shell = shellInstance.GetComponent<InstantBullet>();
            shell.Setup(0, null, 100);
            //shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里暂时先这么实现各种子弹，子弹2射击的效果
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

            // 忽略与自身的碰撞
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            ScatteringBullet shell = shellInstance.GetComponent<ScatteringBullet>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里暂时先这么实现各种子弹，子弹2射击的效果
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

            // 忽略与自身的碰撞
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            PulseBullet shell = shellInstance.GetComponent<PulseBullet>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里暂时先这么实现各种子弹，子弹2射击的效果
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

            // 忽略与自身的碰撞
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            Shell shell = shellInstance.GetComponent<Shell>();
            shell.Setup(0, null, 100);
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
        }


        /// ------------------------------------------------------------------------------------------------
        /// 网络层
        
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

            // 远端玩家的话走这个逻辑
            if( !hasAuthority )
                m_FireInput = true;
        }

        private void ShootFire( )
        {
            Esplasetimer        = 0f;
            Vector3 position    = gunHead.transform.position;
            Vector3 shotVector  = gunHead.transform.forward;
            int randSeed        = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

			//没子弹了
			if (m_curShootBullets <= 0) {
				// 播发空枪的声效

				return;
			}
				
			// 一次射击消耗的子弹数量
			if (m_curShootBullets >= m_WeaponProtol.m_ShootBulletNumPer) 
			{
				m_curShootBullets -= m_WeaponProtol.m_ShootBulletNumPer;

				string str = m_curShootBullets.ToString ();
				Debug.Log (str);
			} 
			else 
			{
				m_ReplaceClipTimer = 0f;
			}

			m_AudioSource.Play ();
				
			// 播放射击特效
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
        }
	}
}