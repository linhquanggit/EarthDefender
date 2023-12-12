using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace EarthDenfender
{
    public class GamePlayPanel : MonoBehaviour
    {
        [SerializeField] private Image imgHpBar;
        [SerializeField] private Image imgExpBar;
        [SerializeField] private TextMeshProUGUI txtLV;
        // Start is called before the first frame update
        private void OnEnable()
        {
            GameController.Instance.onExpChanged += OnScoreChanged;
            GameController.Instance.onLevelChanged += OnLevelChanged;
            SpawnManager.Instance.Player.onHpChanged += OnHpChanged;
        }
        private void OnDisable()
        {
            GameController.Instance.onExpChanged -= OnScoreChanged;
            SpawnManager.Instance.Player.onHpChanged -= OnHpChanged;
        }
        private void OnHpChanged(int curHp, int Hp)
        {
            imgHpBar.fillAmount = curHp * 1f / Hp;
        }
        private void OnLevelChanged(int level)
        {
            txtLV.text = "LV : " + level;
        }
        public void Pause()
        {
            GameController.Instance.Pause();
        }

        private void OnScoreChanged(int currentExp, int maxExp)
        {
            imgExpBar.fillAmount = currentExp * 1f / maxExp;
        }


    }
}