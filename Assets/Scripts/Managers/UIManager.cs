using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum UIState
{
    Home,
    Game,
    GameOver
}

public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
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

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init();
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init();
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init();

        //ChangeState(UIState.Home);
    }

    //public void SetPlayGame()
    //{
    //    ChangeState(UIState.Game);
    //}

    //public void SetGameOver()
    //{
    //    ChangeState(UIState.GameOver);
    //}

    public void Update()
    {
        gameUI.UpdateEnergyBarMeter();
        gameUI.UpdateNowScoreText();
        gameUI.UpdateBestScoreText();
    }

    //public void ChangeState(UIState state)
    //{
    //    currentState = state;
    //    homeUI.SetActive(currentState);
    //    gameUI.SetActive(currentState);
    //    gameOverUI.SetActive(currentState);
    //}
}

