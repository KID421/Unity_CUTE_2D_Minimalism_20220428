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
            groupFinal = GameObject.Find("結束畫面").GetComponent<CanvasGroup>();
            textEnemyCount = GameObject.Find("敵人數量").GetComponent<TextMeshProUGUI>();
            print(textEnemyCount);
            countTotal = GameObject.FindGameObjectsWithTag("敵人").Length;
            UpdateUI();
        }

        /// <summary>
        /// 更新敵人擊殺數量
        /// </summary>
        public void UpdateEnemyCount()
        {
            countKill++;
            UpdateUI();

            if (countKill == countTotal) StartCoroutine(FadeInFinal());
        }

        /// <summary>
        /// 更新介面
        /// </summary>
        private void UpdateUI()
        {
            textEnemyCount.text = "Enemy Count - " +countKill + " / " + countTotal;
        }

        /// <summary>
        /// 淡入結束畫面
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

