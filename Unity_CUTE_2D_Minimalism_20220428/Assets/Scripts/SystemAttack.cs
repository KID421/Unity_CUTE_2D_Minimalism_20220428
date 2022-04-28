using UnityEngine;

namespace KID
{
    /// <summary>
    /// �����t��
    /// �G���q����
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [SerializeField, Header("�������j"), Range(0, 1)]
        private float intervalAttack = 0.3f;
        [SerializeField, Header("�����ؼйϼh")]
        private LayerMask layerAttack;
        [SerializeField, Header("���� 1 �d��ؤo�P�첾")]
        private Vector3 v3Atk1Size = Vector3.one;
        [SerializeField]
        private Vector3 v3Atk1Offset;
        [SerializeField, Header("���� 2 �d��ؤo�P�첾")]
        private Vector3 v3Atk2Size = Vector3.one;
        [SerializeField]
        private Vector3 v3Atk2Offset;
        [SerializeField, Header("�����O 1"), Range(0, 100)]
        private float atkValue1 = 30;
        [SerializeField, Header("�����O 2"), Range(0, 100)]
        private float atkValue2 = 50;

        private string parameterAtk1 = "Ĳ�o���� 1";
        private string parameterAtk2 = "Ĳ�o���� 2";

        private Animator ani;
        private float timerAttack;
        private bool attackFirst;
        private SystemController systemController;

        public bool inputAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }

        private void Awake()
        {
            ani = GetComponent<Animator>();
            systemController = GetComponent<SystemController>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.3f, 0, 0.3f);
            Gizmos.DrawCube(
                transform.position + transform.TransformDirection(v3Atk1Offset),
                v3Atk1Size);

            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawCube(
                transform.position + transform.TransformDirection(v3Atk2Offset),
                v3Atk2Size);
        }

        private void Update()
        {
            Attack();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Attack()
        {
            if (!attackFirst && inputAttack)
            {
                attackFirst = true;
                ani.SetTrigger(parameterAtk1);
                CheckAttackHit(v3Atk1Offset, v3Atk1Size, atkValue1);
            }
            else if (attackFirst && inputAttack)
            {
                ani.SetTrigger(parameterAtk2);
                CheckAttackHit(v3Atk2Offset, v3Atk2Size, atkValue2);
            }

            if (attackFirst)
            {
                if (timerAttack < intervalAttack)
                {
                    timerAttack += Time.deltaTime;
                }
                else
                {
                    attackFirst = false;
                    timerAttack = 0;
                }
            }

            if (ani.GetCurrentAnimatorStateInfo(0).IsTag("����"))
            {
                systemController.StopMove();
            }
            else
            {
                systemController.enabled = true;
            }
        }

        /// <summary>
        /// �ˬd�����I��
        /// </summary>
        private void CheckAttackHit(Vector3 offset, Vector3 size, float atk)
        {
            Collider2D hit =Physics2D.OverlapBox(
                transform.position + transform.TransformDirection(offset),
                size, 0, layerAttack);

            if (hit) hit.GetComponent<SystemHurt>().GetHurt(atk);
        }
    }
}

