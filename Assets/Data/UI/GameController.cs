using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthDenfender
{
    public enum GameState
    {
        Home,
        GamePlay,
        Pause,
        GameOver,
        Tutorial,
        Setting
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
        public Action<int, int> onExpChanged;
        public Action<int> onLevelChanged;
        [SerializeField] private HomePanel homePanel;
        [SerializeField] private GamePlayPanel gamePlayPanel;
        [SerializeField] private PausePanel pausePanel;
        [SerializeField] private GameOverPanel gameOverPanel;
        [SerializeField] private TutorialPanel tutorialPanel;
        [SerializeField] private SettingPanel settingPanel;

        [SerializeField] protected WaveData[] wave;
        [SerializeField] private int maxExp;

        private int currentWayIndex;
        private int level;
        private bool isWin;
        private bool isLevelUp;
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
            level = 1;
            isLevelUp = false;
            homePanel.gameObject.SetActive(false);
            gamePlayPanel.gameObject.SetActive(false);
            pausePanel.gameObject.SetActive(false);
            gameOverPanel.gameObject.SetActive(false);
            tutorialPanel.gameObject.SetActive(false);
            settingPanel.gameObject.SetActive(false);
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
            settingPanel.gameObject.SetActive(gameState == GameState.Setting);
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
            SpawnManager.Instance.StartBattle(w, true);
            SetState(GameState.GamePlay);
            currentExp = 0;
            if (onExpChanged != null)
                onExpChanged(currentExp, maxExp);
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

        public void Setting()
        {
            int key = PlayerPrefs.GetInt("SettingKey");
            SetState(GameState.Setting);
            if(gameState==GameState.Setting && key ==1)
            {
                Time.timeScale = 1;
            }
            else if(gameState == GameState.Setting && key == 2)
            {
                Time.timeScale = 0;
            }
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
            if (onExpChanged != null)
            {
                if (currentExp >= maxExp)
                {
                    currentExp = 0;
                    level++;
                    isLevelUp = true;
                    Vector3 curPos = PlayerController.Instance.PlayerPos();
                    curPos.y += 1f;
                    Vector3 textPos = new Vector3(curPos.x, curPos.y, curPos.z);
                    FloatingTextController dameText = SpawnManager.Instance.SpawnFloatingText(textPos);
                    dameText.transform.GetChild(0).GetComponent<TextMesh>().text = "level Up";
                    dameText.transform.GetChild(0).GetComponent<TextMesh>().color = Color.yellow;
                    onLevelChanged(level);
                }
                onExpChanged(currentExp, maxExp);
            }
            CheckWave();
            if (isLevelUp)
            {
                GetLevel();
            }
        }
        public void CheckWave()
        {
            if (SpawnManager.Instance.IsClear() == true)
            {
                currentWayIndex++;
                if (currentWayIndex >= wave.Length - 1)
                {
                    GameOver(true);
                }
                else
                {
                    WaveData w = wave[currentWayIndex];
                    SpawnManager.Instance.StartBattle(w, false);
                }
            }
        }
        public int GetLevel()
        {
            isLevelUp = false;
            return level;
        }
    }
}