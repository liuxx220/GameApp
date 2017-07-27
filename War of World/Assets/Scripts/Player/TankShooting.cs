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


        //[SyncVar]
        private float           m_TurretHeading;

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

        void Awake()
        {
            shootableMask       = LayerMask.GetMask("Shootable");
            m_LineRender        = gameObject.GetComponent<LineRenderer>();
            m_curLookatDeg      = transform.rotation.eulerAngles.y;
            m_TurretHeading     = m_curLookatDeg;
            m_fOldEulerAngles   = m_curLookatDeg;
            RedPoint.SetActive(false);
            muzzleFlash.SetActive(false);
            m_LineRender.enabled= false;
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
                    GameObject.DestroyImmediate(m_EquipWeapon);
                }

                UnityEngine.Object obj          = AssetManager.Get().GetResources(def.perfab);
                GameObject weapon               = GameObject.Instantiate(obj) as GameObject;
                weapon.transform.parent         = m_WeaponHP.transform;
                weapon.transform.localPosition  = Vector3.zero;
                weapon.transform.localRotation  = Quaternion.identity;
                weapon.transform.localScale     = Vector3.one;
                m_WeaponProtol = def;
                m_EquipWeapon  = weapon;
                
            }
        }


        [ClientCallback]
        private void Update()
        {
            m_shootRay.origin       = gunHead.transform.position;
            m_shootRay.direction    = gunHead.transform.forward;
            Physics.Raycast(m_shootRay, out m_shootHit, 50, shootableMask);
            DrawPulseLine();

            Esplasetimer += Time.deltaTime;
            if (m_WeaponProtol.m_ShootMode == SHOOTINGMODE.Shoot_pulse)
            {
                if (m_FireInput && Esplasetimer >= 1.0f && Time.timeScale != 0)
                {
                    Shoot();
                }
            }
            else
            {
                if (m_FireInput && Esplasetimer >= 0.1f && Time.timeScale != 0)
                {
                    Shoot();
                }
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
        void Shoot()
        {
            Esplasetimer        = 0f;
            Vector3 position    = gunHead.transform.position;
            Vector3 shotVector  = gunHead.transform.forward;

            // 随机种子
            int randSeed        = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            CmdFire(shotVector, position, randSeed);
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
        public void SetDesiredFirePosition( Vector3 facedir )
        {
            float angle     = 90f - Mathf.Atan2(facedir.y, facedir.x) * Mathf.Rad2Deg;
            CmdSetLook(angle + m_fOldEulerAngles);
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
            shell.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, m_curLookatDeg, transform.rotation.eulerAngles.z));
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