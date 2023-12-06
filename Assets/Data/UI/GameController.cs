using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Home,
    GamePlay,
    Pause,
    GameOver,
    Tutorial
}
public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameController>();
            return instance;
        }
    }
    public Action<int,int> onScoreChanged;
    [SerializeField] private HomePanel homePanel;
    [SerializeField] private GamePlayPanel gamePlayPanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private TutorialPanel tutorialPanel;

    [SerializeField] protected WaveData[] wave;
    [SerializeField] private int maxExp; 

    private int currentWayIndex;
    private int level;
    private bool isWin;
    private int currentExp;
    private GameState gameState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        homePanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        tutorialPanel.gameObject.SetActive(false);
        SetState(GameState.Home);
    }


    private void SetState(GameState state)
    {
        gameState = state;
        homePanel.gameObject.SetActive(gameState == GameState.Home);
        gamePlayPanel.gameObject.SetActive(gameState == GameState.GamePlay);
        pausePanel.gameObject.SetActive(gameState == GameState.Pause);
        gameOverPanel.gameObject.SetActive(gameState == GameState.GameOver);
        tutorialPanel.gameObject.SetActive(gameState == GameState.Tutorial);
        if (gameState == GameState.Pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        if (gameState == GameState.Home)
        {
            AudioManager.Instance.PlayHomeMusic();
        }
        else
            AudioManager.Instance.PlayBattleMusic();
    }

    public bool IsActive()
    {
        return gameState == GameState.GamePlay;
    }
    public void Play()
    {
        currentWayIndex = 0;
        WaveData w = wave[currentWayIndex];
        Debug.Log("Wave : " + currentWayIndex);
        SpawnManager.Instance.StartBattle(w,true);
        SetState(GameState.GamePlay);
        currentExp = 0;
        if (onScoreChanged != null)
            onScoreChanged(currentExp,maxExp);
    }

    public void Pause()
    {
        SetState(GameState.Pause);
    }

    public void Home()
    {
        SetState(GameState.Home);
        SpawnManager.Instance.Clear();
    }
    public void Continue()
    {
        SetState(GameState.GamePlay);
    }

    public void Tutorial()
    {
        SetState(GameState.Tutorial);
    }    

    public void GameOver(bool win)
    {
        isWin = win;
        SetState(GameState.GameOver);
        gameOverPanel.DisplayResultText(isWin);
    }
    public void AddExp(int value)
    {
        currentExp += value;
        if (onScoreChanged != null)
        {
            if (currentExp >= maxExp)
            {
                currentExp = 0;
                level++;
                Debug.Log("Level : " + level);
            }
                onScoreChanged(currentExp, maxExp);
            
        }
        if (SpawnManager.Instance.IsClear())
        {
            NextWay();
        }


    }
    public void NextWay()
    {
        currentWayIndex++;
        if (currentWayIndex >= wave.Length)
        {
            GameOver(true);
            Time.timeScale = 0;
        }
        else
        {
            WaveData w = wave[currentWayIndex];
            Debug.Log("Wave : " + currentWayIndex);
            SpawnManager.Instance.StartBattle(w, false);
        }
    }
}
