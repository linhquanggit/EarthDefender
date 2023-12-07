using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EarthDenfender
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtTutorial;
        [SerializeField] private float delayTypeWriter = 0.1f;
        private string fullText = "You need to defeat all the enemies to win by using\n" + "\n" +
            " 'W-A-S-D' keys or the 'Arrow keys' to move .\n" + "\n" +
            "'Spacebar' or 'Left mouse button' to attack.\n" + "\n" +
            "Note: that as the waves progress, enemies will become faster, attack more often, and their attack power will increase.";
        private string currentText = "";
        public void BtnBack_Pressed()
        {
            GameController.Instance.Home();
        }
        private void OnEnable()
        {
            StartCoroutine(TypeWriter());
        }
        IEnumerator TypeWriter()
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                currentText += fullText[i];
                txtTutorial.text = currentText;
                yield return new WaitForSeconds(delayTypeWriter);
            }
        }
    }
}