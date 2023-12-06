using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private Image imgHpBar;
    [SerializeField] private Image imgExpBar;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameController.Instance.onScoreChanged += OnScoreChanged;
        SpawnManager.Instance.Player.onHpChanged += OnHpChanged;
    }

    private void OnHpChanged(int curHp, int Hp)
    {
        imgHpBar.fillAmount = curHp * 1f / Hp;
    }

    private void OnDisable()
    {
        GameController.Instance.onScoreChanged -= OnScoreChanged;
        SpawnManager.Instance.Player.onHpChanged -= OnHpChanged;
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
