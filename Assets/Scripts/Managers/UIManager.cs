using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum UIState
{
    Title,
    Game,
    GameOver
}

public class UIManager : MonoBehaviour
{
    TitleUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;

    private UIState currentState;

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // null 체크 후 초기화
        homeUI = GetComponentInChildren<TitleUI>(true);
        if(homeUI != null)
            homeUI.Init();
        gameUI = GetComponentInChildren<GameUI>(true);
        if(gameUI != null)
            gameUI.Init();
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        if(gameOverUI != null)
        gameOverUI.Init();

    }

    public void Update()
    {
        // 작동 테스트용 코드
        if (gameUI != null)
        {
            gameUI.UpdateEnergyBarMeter();
            gameUI.UpdateNowScoreText();
            gameUI.UpdateBestScoreText();
        }
    }


    //public void SetPlayGame()
    //{
    //    ChangeState(UIState.Game);
    //}

    //public void SetGameOver()
    //{
    //    ChangeState(UIState.GameOver);
    //}

    //public void ChangeState(UIState state)
    //{
    //    currentState = state;
    //    homeUI.SetActive(currentState);
    //    gameUI.SetActive(currentState);
    //    gameOverUI.SetActive(currentState);
    //}
}

