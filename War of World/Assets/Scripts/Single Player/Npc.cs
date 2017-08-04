using UnityEngine;
using Tanks.Rules.SinglePlayer;
using Tanks;
using Tanks.Shells;
using Tanks.Map;
using Tanks.Rules;
using Tanks.Data;
using Tanks.Networking;
using Tanks.Explosions;
using Tanks.TankControllers;
using TanksNetworkPlayer = Tanks.Networking.NetworkPlayer;






namespace Tanks.SinglePlayer
{
	public class Npc : MonoBehaviour
	{
		protected float         m_MaximumHealth = 50;
		private float           m_CurrentHealth;
		private bool            m_IsDead = false;

        public AudioClip        deathClip;   
        public HUDPlayer        hudPlayer = null;      
        public int              attackDamage = 2;

        TanksNetworkPlayer      TargetPlayer;
        Animator                entityAnimator;
        AudioSource             enemyAudio;
        public GameObject       gunHead;

        /// <summary>
        /// 当前武器的配置信息
        /// </summary>
        private TankWeaponDefinition m_Weapon = null;

		void Awake()
		{
            m_CurrentHealth     = m_MaximumHealth;
            TargetPlayer        = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<TanksNetworkPlayer>();
            entityAnimator      = GetComponent<Animator>();
            enemyAudio          = GetComponent<AudioSource>();
		}

        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// 死亡
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
		protected virtual void OnDied()
		{
            m_IsDead    = true;
            entityAnimator.SetTrigger("Dead");
            enemyAudio.clip = deathClip;
            enemyAudio.Play();

            hudPlayer.gameObject.SetActive(false);
            hudPlayer   = null;
            SpawnManager.s_Instance.DestoryEnemy(gameObject);
		}

        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// 攻击
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        void Shoot()
        {
            if (TargetPlayer.m_CurrentHealth > 0 && gunHead != null )
            {
                Vector3 position    = gunHead.transform.position;
                Vector3 shotVector  = gunHead.transform.forward;
                TargetPlayer.TakeDamage(attackDamage);
                FireEffect1(shotVector, position);
            }
        }


        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// 伤害
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (m_IsDead)
                return;

            enemyAudio.Play();
            m_CurrentHealth -= amount;
            UpdateHpChange(0, amount);
            if (m_CurrentHealth <= 0)
            {
                OnDied();
            }
        }

        public void StartSinking()
        {
           // 爲東中有事件保留
        }

        /// -----------------------------------------------------------------------------------------------
        /// <summary>
        /// 血量改变
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        public void UpdateHpChange(byte reason, int curHp)
        {
            hudPlayer.AddHUD( curHp, Color.red, 0f );
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 这里暂时先这么实现各种子弹，子弹1射击的效果
        /// </summary>
        /// ------------------------------------------------------------------------------------------
        private void FireEffect1(Vector3 shootVector, Vector3 position)
        {
            GameObject shellInstance = null;
            if (ExplosionManager.s_InstanceExists)
            {
                shellInstance = ExplosionManager.s_Instance.CreateVisualBullet(position, shootVector, 0, BulletClass.FiringExplosion);
            }

            shellInstance.SetActive(true);
            shellInstance.transform.localScale  = Vector3.one;
            shellInstance.transform.position    = position;
            shellInstance.transform.forward     = shootVector;

            // 忽略与自身的碰撞
            Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponentInChildren<Collider>(), true);
            InstantBullet shell = shellInstance.GetComponent<InstantBullet>();
            shell.Setup(0, null, 100);
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
                m_Weapon = def;
            }
        }
	}
}