using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace EarthDenfender
{
    public class HomePanel : MonoBehaviour
    {
        private static HomePanel instance;
        public static HomePanel Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<HomePanel>();
                return instance;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        public void BtnPlay_Pressed()
        {
            GameController.Instance.Play();
        }

        public void BtnTutorial_Pressed()
        {
            GameController.Instance.Tutorial();
        }
        public void BtnExit_Pressed()
        {
            /*if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.ExitPlaymode();
            }*/
            Application.Quit();
        }
        public void BtnSettingPressed()
        {
            PlayerPrefs.SetInt("SettingKey", 1);
            GameController.Instance.Setting();
        }

    }
}