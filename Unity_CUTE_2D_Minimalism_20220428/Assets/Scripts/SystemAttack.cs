using UnityEngine;

namespace KID
{
    /// <summary>
    /// §ðÀ»¨t²Î
    /// ¤G¶¥¬q§ðÀ»
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [SerializeField, Header("§ðÀ»¶¡¹j"), Range(0, 1)]
        private float intervalAttack = 0.3f;
        [SerializeField, Header("§ðÀ»¥Ø¼Ð¹Ï¼h")]
        private LayerMask layerAttack;
        [SerializeField, Header("§ðÀ» 1 ½d³ò¤Ø¤o»P¦ì²¾")]
        private Vector3 v3Atk1Size = Vector3.one;
        [SerializeField]
        private Vector3 v3Atk1Offset;
        [SerializeField, Header("§ðÀ» 2 ½d³ò¤Ø¤o»P¦ì²¾")]
        private Vector3 v3Atk2Size = Vector3.one;
        [SerializeField]
        private Vector3 v3Atk2Offset;
        [SerializeField, Header("§ðÀ»¤O 1"), Range(0, 100)]
        private float atkValue1 = 30;
        [SerializeField, Header("§ðÀ»¤O 2"), Range(0, 100)]
        private float atkValue2 = 50;

        private string parameterAtk1 = "Ä²µo§ðÀ» 1";
        private string parameterAtk2 = "Ä²µo§ðÀ» 2";

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
        /// §ðÀ»
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

            if (ani.GetCurrentAnimatorStateInfo(0).IsTag("§ðÀ»"))
            {
                systemController.StopMove();
            }
            else
            {
                systemController.enabled = true;
            }
        }

        /// <summary>
        /// ÀË¬d§ðÀ»¸I¼²
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

