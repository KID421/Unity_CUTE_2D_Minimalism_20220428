using Unity.VisualScripting;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 控制系統
    /// 橫向卷軸移動、跳躍
    /// </summary>
    public class SystemController : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("移動速度"), Range(0, 100)]
        private float speed = 3.5f;
        [SerializeField, Header("跳躍高度"), Range(0, 1000)]
        private float jump = 500;
        [SerializeField, Header("可跳躍物件圖層")]
        private LayerMask layerCanJump;
        [SerializeField, Header("地板偵測尺寸與位移")]
        private Vector3 v3CheckGroundSize = Vector3.one;
        [SerializeField]
        private Vector3 v3CheckGroundOffset;

        private Rigidbody2D rig;
        private Animator ani;
        private float inputH;
        private string parameterRun = "開關跑步";
        private bool isGround;
        public float inputHorizontal { get => Input.GetAxis("Horizontal"); }
        #endregion

        #region 事件
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

        #region 方法
        /// <summary>
        /// 輸入移動
        /// </summary>
        private void InputMove()
        {
            inputH = inputHorizontal;
        }

        /// <summary>
        /// 移動控制
        /// </summary>
        private void Move()
        {
            rig.velocity = new Vector3(inputH * speed, rig.velocity.y, 0);
        }

        /// <summary>
        /// 翻面
        /// </summary>
        private void Flip()
        {
            if (Mathf.Abs(inputHorizontal) < 0.1f) return;
            float y = inputHorizontal > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, y, 0);
        }

        /// <summary>
        /// 控制動畫
        /// </summary>
        private void UpdateAnimator()
        {
            ani.SetBool(parameterRun, inputHorizontal != 0);
        }

        /// <summary>
        /// 檢查地板
        /// </summary>
        private void CheckGround()
        {
            Collider2D hit = Physics2D.OverlapBox(
                transform.position + transform.TransformDirection(v3CheckGroundOffset),
                v3CheckGroundSize, 0, layerCanJump);

            isGround = hit;
        }

        /// <summary>
        /// 跳躍
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

