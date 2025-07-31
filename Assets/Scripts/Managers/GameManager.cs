using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    //public GameState gameState { get; private set; } = GameState.Ready; //게임 상태 초기화 //필요없을듯

    public int currentScore { get; private set; }   //현재 점수
    public int bestScore { get; private set; }     //최고 점수

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UIManager.Instance.ChangeState(UIState.Title);
    }
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

    private void Init()
    {
        currentScore = 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0); //BestScore 없을 경우 자동으로 0 반환
    }

    public void AddScore(int score)
    {
        currentScore += score;
        //To do: UI에 점수 업데이트
    }
    public void LoadGame()
    {
        UIManager.Instance.ChangeState(UIState.Loading);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("GameScene");
        //Time.timeScale = 0f;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //씬이 로드 된 다음에 변경된 UI 적용
    {
        if (scene.name == "GameScene")   //씬 종류 많아지면 switch로 변경
        {
            UIManager.Instance.ChangeState(UIState.Game);
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;  //이벤트 중복 방지로 제거
    }
    public void StartGame()
    {
        //To do: ReadyUI에서 게임 시작 눌리면 호출되어야함.
        //To do: 게임 시작 로직 (씬전환, 초기화, UI 업데이트 등)
        Time.timeScale = 1f;
        Debug.Log("Game Started");
    }
    public void GameOver()
    {
        //To do: 게임 오버 화면 표시
        Debug.Log("Game Over");
        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore); //최고 점수 저장
        }
    }
}
