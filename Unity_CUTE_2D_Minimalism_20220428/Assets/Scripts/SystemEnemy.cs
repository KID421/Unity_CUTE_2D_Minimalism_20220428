using UnityEngine;

namespace KID
{
    /// <summary>
    /// �ĤH�t��
    /// �l�ܪ��a
    /// </summary>
    public class SystemEnemy : MonoBehaviour
    {
        [SerializeField, Header("���ʳt��"), Range(0, 10)]
        private float speed = 2;
        [SerializeField, Header("�l�ܽd��ؤo�P�첾")]
        private Vector3 v3TrackRangeSize = Vector3.one;
        [SerializeField]
        private Vector3 v3TrackRangeOffset;
        [SerializeField, Header("�����d��ؤo�P�첾")]
        private Vector3 v3AttackRangeSize = Vector3.one;
        [SerializeField]
        private Vector3 v3AttackRangeOffset;
        [SerializeField, Header("�l�ܹϼh")]
        private LayerMask layerTrack;

        private Transform traPlayer;
        private string namePlayer = "�M�h";
        private bool targetInTrackRange;
        private Rigidbody2D rig;
        private Animator ani;
        private string parameterRun = "�}���]�B";

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
        /// �l��
        /// </summary>
        private void Track()
        {
            Collider2D hit = Physics2D.OverlapBox(
                transform.position + transform.TransformDirection(v3TrackRangeOffset),
                v3TrackRangeSize, 0, layerTrack);

            targetInTrackRange = hit;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Move()
        {
            ani.SetBool(parameterRun, targetInTrackRange);

            if (!targetInTrackRange) return;
            rig.velocity = transform.right * speed;
        }

        /// <summary>
        /// ½��
        /// </summary>
        private void Flip()
        {
            if (!targetInTrackRange) return;
            float y = transform.position.x > traPlayer.position.x ? 180 : 0;
            transform.eulerAngles = new Vector3(0, y, 0);
        }
    }
}

