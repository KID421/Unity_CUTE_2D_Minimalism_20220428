using UnityEngine;

namespace KID
{
    /// <summary>
    /// ���˨t��
    /// ��q�B�����ˮ`�P�ʧ@��s
    /// </summary>
    public class SystemHurt : MonoBehaviour
    {
        [SerializeField, Header("��q"), Range(0, 1000)]
        private float hp = 100;
        [SerializeField, Header("�n����欰")]
        private Behaviour behaviourToStop;
        [SerializeField, Header("���˶��j"), Range(0, 3)]
        private float intervalHurt = 0.5f;

        private float maxHp;

        private Animator ani;
        private Rigidbody2D rig;
        private string parameterHit = "Ĳ�o����";
        private string parameterDead = "�}�����`";

        private GameManager gm;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            maxHp = hp;
            gm = GameObject.Find("�޲z���C��").GetComponent<GameManager>();
        }

        /// <summary>
        /// ����ˮ`
        /// </summary>
        /// <param name="damage">�ˮ`</param>
        public void GetHurt(float damage)
        {
            hp -= damage;
            ani.SetTrigger(parameterHit);
            rig.velocity = Vector3.zero;
            behaviourToStop.enabled = false;
            CancelInvoke();
            Invoke("DelayEnableBehaviour", intervalHurt);

            if (hp <= 0) Dead();
        }

        /// <summary>
        /// ���`
        /// </summary>
        private void Dead()
        {
            behaviourToStop.enabled = false;
            ani.SetBool(parameterDead, true);
            GetComponent<Collider2D>().enabled = false;
            rig.velocity = Vector3.zero;
            rig.constraints = RigidbodyConstraints2D.FreezeAll;
            CancelInvoke();
            gm.UpdateEnemyCount();
        }

        /// <summary>
        /// ����Ұʦ欰
        /// </summary>
        private void DelayEnableBehaviour()
        {
            behaviourToStop.enabled = true;
        }
    }
}

