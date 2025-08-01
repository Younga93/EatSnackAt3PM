using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isGodMode = false;
    public static GameManager Instance { get; private set; }
    //public GameState gameState { get; private set; } = GameState.Ready; //게임 상태 초기화 //필요없을듯

    public PlayerController Player { get; private set; }
    public int currentScore { get; private set; }   //현재 점수
    //public int bestScore { get; private set; }     //최고 점수

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        OutfitItemData.InitializeData();
        DontDestroyOnLoad(gameObject);
        Player = FindObjectOfType<PlayerController>();
    }

    //private void Start()
    //{
    //    UIManager.Instance.ChangeState(UIState.Title);
    //}

    //public void SetGameState(GameState newState)
    //{
    //    gameState = newState;
    //    Debug.Log("Game State Changed to: " + gameState);
    //    switch(gameState)
    //    {
    //        case GameState.Playing:
    //            Init(); //게임 시작 전에 초기화
    //            StartGame();
    //            break;
    //        case GameState.GameOver:
    //            GameOver();
    //            break;
    //    }
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))    //테스트용  //Prefs.Rest
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs 삭제");
        }
    }

    private void InitScore()
    {
        currentScore = 0;
        UIManager.Instance.UpdateGameScoresUI(currentScore);
        //bestScore = PlayerPrefs.GetInt("BestScore", 0); //BestScore 없을 경우 자동으로 0 반환
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UIManager.Instance.UpdateGameScoresUI(currentScore);
    }
    public void UpdateHealth(int currentHealth)  //0이 되면 게임오버, 업데이트 될때마다 UI 업데이트
    {
        UIManager.Instance.UpdateHealthUI(currentHealth);
        if (currentHealth <= 0)
        {
            if(isGodMode)
            {
                return;
            }
            GameOver();
            return;
        }
    }
    public void InitGame()
    {
        LoadSceneWithCallback("GameScene");
        InitScore();
        //Time.timeScale = 0f;
    }
    public void LoadSceneWithCallback(string sceneName)
    {
        UIManager.Instance.ChangeState(UIState.Loading);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //씬이 로드 된 다음에 변경된 UI 적용
    {
        switch (scene.name)
        {
            case "GameScene":
                UIManager.Instance.ChangeState(UIState.Game);
                PresetSpawnManager.Instance.MakePreset(10);
                Time.timeScale = 1f;
                break;
            case "TitleScene":
                UIManager.Instance.ChangeState(UIState.Title);
                break;
            case "StoreScene":
                UIManager.Instance.ChangeState(UIState.Store);
                break;
        }
        //if(scene.name == "GameScene")   //씬 종류 많아지면 switch로 변경
        //{
        //    UIManager.Instance.ChangeState(UIState.Game);
        //    PresetSpawnManager.Instance.MakePreset(10);
        //    Time.timeScale = 1f;
        //}
        SceneManager.sceneLoaded -= OnSceneLoaded;  //이벤트 중복 방지로 제거
    }
    public void GameOver()
    {
        //To do: 게임 오버 화면 표시
        Debug.Log("Game Over");

        Time.timeScale = 0f;
        if (currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }

        //재화 Score 쌓기
        int points = PlayerPrefs.GetInt("Point", 0);
        points += currentScore;
        PlayerPrefs.SetInt("Point", points);
        PlayerPrefs.Save();

        //테스트
        Debug.Log("Point: " + PlayerPrefs.GetInt("Point", 0));
        //테스트코드끝

        UIManager.Instance.ChangeState(UIState.GameOver);
        UIManager.Instance.UpdateGameOverUI(currentScore);
        UIManager.Instance.UpdateHealthUI(currentScore);
    }
}
