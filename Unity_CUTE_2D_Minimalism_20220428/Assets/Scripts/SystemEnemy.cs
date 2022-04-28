using UnityEngine;

namespace KID
{
    /// <summary>
    /// 敵人系統
    /// 追蹤玩家
    /// </summary>
    public class SystemEnemy : MonoBehaviour
    {
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float speed = 2;
        [SerializeField, Header("追蹤範圍尺寸與位移")]
        private Vector3 v3TrackRangeSize = Vector3.one;
        [SerializeField]
        private Vector3 v3TrackRangeOffset;
        [SerializeField, Header("攻擊範圍尺寸與位移")]
        private Vector3 v3AttackRangeSize = Vector3.one;
        [SerializeField]
        private Vector3 v3AttackRangeOffset;
        [SerializeField, Header("追蹤圖層")]
        private LayerMask layerTrack;

        private Transform traPlayer;
        private string namePlayer = "騎士";
        private bool targetInTrackRange;
        private Rigidbody2D rig;
        private Animator ani;
        private string parameterRun = "開關跑步";

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
            traPlayer = GameObject.Find(namePlayer).transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawCube(
                transform.position + transform.TransformDirection(v3TrackRangeOffset),
                v3TrackRangeSize);

            Gizmos.color = new Color(1, 0.3f, 0, 0.3f);
            Gizmos.DrawCube(
                transform.position + transform.TransformDirection(v3AttackRangeOffset),
                v3AttackRangeSize);
        }

        private void Update()
        {
            Track();
            Flip();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 追蹤
        /// </summary>
        private void Track()
        {
            Collider2D hit = Physics2D.OverlapBox(
                transform.position + transform.TransformDirection(v3TrackRangeOffset),
                v3TrackRangeSize, 0, layerTrack);

            targetInTrackRange = hit;
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {
            ani.SetBool(parameterRun, targetInTrackRange);

            if (!targetInTrackRange) return;
            rig.velocity = transform.right * speed;
        }

        /// <summary>
        /// 翻面
        /// </summary>
        private void Flip()
        {
            if (!targetInTrackRange) return;
            float y = transform.position.x > traPlayer.position.x ? 180 : 0;
            transform.eulerAngles = new Vector3(0, y, 0);
        }
    }
}

