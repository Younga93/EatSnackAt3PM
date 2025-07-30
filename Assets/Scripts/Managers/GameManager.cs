using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public GameState gameState { get; private set; } = GameState.Ready; //게임 상태 초기화

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
        Init();        
    }
    public void SetGameState(GameState newState)
    {
        gameState = newState;
        Debug.Log("Game State Changed to: " + gameState);
        switch(gameState)
        {
            case GameState.Playing:
                Init(); //게임 시작 전에 초기화
                StartGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

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

    private void StartGame()
    {
        //To do: 게임 시작 로직 (씬전환, 초기화, UI 업데이트 등)
        Debug.Log("Game Started");
    }
    private void GameOver()
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
