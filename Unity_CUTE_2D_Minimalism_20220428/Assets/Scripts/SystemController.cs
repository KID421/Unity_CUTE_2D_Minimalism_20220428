using Unity.VisualScripting;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// ����t��
    /// ��V���b���ʡB���D
    /// </summary>
    public class SystemController : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("���ʳt��"), Range(0, 100)]
        private float speed = 3.5f;
        [SerializeField, Header("���D����"), Range(0, 1000)]
        private float jump = 500;
        [SerializeField, Header("�i���D����ϼh")]
        private LayerMask layerCanJump;
        [SerializeField, Header("�a�O�����ؤo�P�첾")]
        private Vector3 v3CheckGroundSize = Vector3.one;
        [SerializeField]
        private Vector3 v3CheckGroundOffset;

        private Rigidbody2D rig;
        private Animator ani;
        private float inputH;
        private string parameterRun = "�}���]�B";
        private bool isGround;
        public float inputHorizontal { get => Input.GetAxis("Horizontal"); }
        #endregion

        #region �ƥ�
        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawCube(
                transform.position + transform.TransformDirection(v3CheckGroundOffset),
                v3CheckGroundSize);
        }

        private void Update()
        {
            InputMove();
            Flip();
            UpdateAnimator();
            Jump();
            CheckGround();
        }

        private void FixedUpdate()
        {
            Move();
        }
        #endregion

        #region ��k
        /// <summary>
        /// ��J����
        /// </summary>
        private void InputMove()
        {
            inputH = inputHorizontal;
        }

        /// <summary>
        /// ���ʱ���
        /// </summary>
        private void Move()
        {
            rig.velocity = new Vector3(inputH * speed, rig.velocity.y, 0);
        }

        /// <summary>
        /// ½��
        /// </summary>
        private void Flip()
        {
            if (Mathf.Abs(inputHorizontal) < 0.1f) return;
            float y = inputHorizontal > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, y, 0);
        }

        /// <summary>
        /// ����ʵe
        /// </summary>
        private void UpdateAnimator()
        {
            ani.SetBool(parameterRun, inputHorizontal != 0);
        }

        /// <summary>
        /// �ˬd�a�O
        /// </summary>
        private void CheckGround()
        {
            Collider2D hit = Physics2D.OverlapBox(
                transform.position + transform.TransformDirection(v3CheckGroundOffset),
                v3CheckGroundSize, 0, layerCanJump);

            isGround = hit;
        }

        /// <summary>
        /// ���D
        /// </summary>
        private void Jump()
        {
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                rig.AddForce(new Vector2(0, jump));
            }
        }
        #endregion
    }
}

