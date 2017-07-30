using UnityEngine;
using Tanks.TankControllers;
using Tanks.Extensions;







namespace Tanks.Data
{
	/// <summary>
	/// ��ɫѪ��
	/// </summary>
    public class HUDPlayer : MonoBehaviour
    {
        /// <summary>
        /// Ѫ������
        /// </summary>
        [SerializeField]
        protected GameObject        m_HPBar;

        /// <summary>
        /// Ѫ���ĸ߶�
        /// </summary>
        [SerializeField]
        protected float             m_HPHeight;

        /// <summary>
        /// Ѫ���ĸ߶�
        /// </summary>
        [SerializeField]
        protected Transform         m_Transform;

        /// <summary>
        /// ðѪ���ֵĿؼ�
        /// </summary>
        [SerializeField]
        protected UILabel           m_HPLable;

        public static int           m_BeAttackShowTimes = 0;
        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Ѫ�������߼�
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        void Update()
        {
            m_HPBar.transform.position  = new Vector3(m_Transform.position.x, m_Transform.position.y + m_HPHeight, m_Transform.position.z);
            Vector3 cameradir           = m_Transform.position - Camera.main.transform.position;
            Quaternion rotation         = Quaternion.LookRotation(cameradir);
            m_HPBar.transform.rotation  = rotation;
        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ����Ѫ���Ľ���
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        public void SetProcess( float fPress )
        {

        }

        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ͷ��ðѪ
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private void ShowHPMP( int nNum )
        {
            m_BeAttackShowTimes++;
            TweenPosition tw  = m_HPLable.GetComponent<TweenPosition>();
            TweenScale    ts  = m_HPLable.GetComponent<TweenScale>();
            if( tw == null )
            {
                tw = m_HPLable.gameObject.AddComponent<TweenPosition>();
            }

            if( ts == null )
            {
                ts = m_HPLable.gameObject.AddComponent<TweenScale>();
            }

            m_HPLable.gameObject.transform.localPosition = transform.localPosition;

            ts.ResetToBeginning();
            ts.from = transform.localPosition;
            ts.to   = new Vector3( transform.localPosition.x,
                                   transform.localPosition.y + (m_BeAttackShowTimes % 4 * 60),
                                   transform.localPosition.z);
            ts.PlayForward();

            tw.ResetToBeginning();
            tw.from = Vector3.one;
            tw.to   = new Vector3(0.01f, 0.01f, 0.0f);
            ts.PlayForward();
        }
    }
}