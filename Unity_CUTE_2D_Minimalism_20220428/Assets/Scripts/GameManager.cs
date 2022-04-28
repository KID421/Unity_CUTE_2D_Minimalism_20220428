using UnityEngine;
using System.Collections;
using TMPro;

namespace KID
{
    public class GameManager : MonoBehaviour
    {
        private TextMeshProUGUI textEnemyCount;
        private int countTotal;
        private int countKill;
        private CanvasGroup groupFinal;

        private void Awake()
        {
            groupFinal = GameObject.Find("�����e��").GetComponent<CanvasGroup>();
            textEnemyCount = GameObject.Find("�ĤH�ƶq").GetComponent<TextMeshProUGUI>();
            print(textEnemyCount);
            countTotal = GameObject.FindGameObjectsWithTag("�ĤH").Length;
            UpdateUI();
        }

        /// <summary>
        /// ��s�ĤH�����ƶq
        /// </summary>
        public void UpdateEnemyCount()
        {
            countKill++;
            UpdateUI();

            if (countKill == countTotal) StartCoroutine(FadeInFinal());
        }

        /// <summary>
        /// ��s����
        /// </summary>
        private void UpdateUI()
        {
            textEnemyCount.text = "Enemy Count - " +countKill + " / " + countTotal;
        }

        /// <summary>
        /// �H�J�����e��
        /// </summary>
        private IEnumerator FadeInFinal()
        {
            yield return new WaitForSeconds(1);

            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

