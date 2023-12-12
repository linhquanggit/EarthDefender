using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EarthDenfender
{
    public class GameOverPanel : MonoBehaviour
    {
        private static GameOverPanel instance;
        public static GameOverPanel Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<GameOverPanel>();
                return instance;
            }
        }
        [SerializeField] TextMeshProUGUI txtResult;
        //[SerializeField] GameObject imgResult;


        private void Start()
        {

        }
        public void BtnHome_Pressed()
        {
            GameController.Instance.Home();
        }


        public void DisplayResultText(bool isWin)
        {
            if (isWin)
                txtResult.text = "YOU WIN";
            else
                txtResult.text = "YOU LOSE";
            StartCoroutine(BlinkResultText());
        }
        IEnumerator BlinkResultText()
        {
            float elapsedTime = 0f;
            float blinkInterval = 0.1f; // Thời gian giữa mỗi lần nhấp nháy (s)

            Color originalColor = txtResult.color;
            Color whiteColor = Color.white;
            Color yellowColor = Color.yellow;

            while (elapsedTime < 3f) // Chạy trong 3 giây
            {
                txtResult.color = (txtResult.color == whiteColor) ? yellowColor : whiteColor; // Chuyển đổi màu giữa trắng và vàng
                yield return new WaitForSeconds(blinkInterval);
                elapsedTime += blinkInterval;
            }

            // Kết thúc Coroutine, đảm bảo chữ được hiển thị và tắt nhấp nháy
            txtResult.color = originalColor;
        }

        public void BtnExit_Pressed()
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.ExitPlaymode();
            }
            Application.Quit();
        }

    }
}