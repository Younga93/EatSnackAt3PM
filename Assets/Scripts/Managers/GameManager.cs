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
        SceneManager.LoadScene("GameScene");
        UIManager.Instance.ChangeState(UIState.Game);
        Time.timeScale = 1f;
        //To do: ReadyUI 출력하기
    }
    public void StartGame()
    {
        //To do: ReadyUI에서 게임 시작 눌리면 호출되어야함.
        //To do: 게임 시작 로직 (씬전환, 초기화, UI 업데이트 등)
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
