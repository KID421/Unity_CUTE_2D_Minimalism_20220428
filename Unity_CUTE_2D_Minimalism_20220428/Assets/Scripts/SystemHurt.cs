using UnityEngine;

namespace KID
{
    /// <summary>
    /// 受傷系統
    /// 血量、接收傷害與動作更新
    /// </summary>
    public class SystemHurt : MonoBehaviour
    {
        [SerializeField, Header("血量"), Range(0, 1000)]
        private float hp = 100;
        [SerializeField, Header("要停止的行為")]
        private Behaviour behaviourToStop;
        [SerializeField, Header("受傷間隔"), Range(0, 3)]
        private float intervalHurt = 0.5f;

        private float maxHp;

        private Animator ani;
        private Rigidbody2D rig;
        private string parameterHit = "觸發受傷";
        private string parameterDead = "開關死亡";

        private GameManager gm;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            maxHp = hp;
            gm = GameObject.Find("管理器遊戲").GetComponent<GameManager>();
        }

        /// <summary>
        /// 受到傷害
        /// </summary>
        /// <param name="damage">傷害</param>
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
        /// 死亡
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
        /// 延遲啟動行為
        /// </summary>
        private void DelayEnableBehaviour()
        {
            behaviourToStop.enabled = true;
        }
    }
}

