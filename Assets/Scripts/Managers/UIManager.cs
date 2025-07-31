using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum UIState
{
    Title,
    Game,
    GameOver,
    InputSetting,
    SystemMessage
}

public class UIManager : MonoBehaviour
{
    //씬 UI  //서로 스위치 되는 UI들
    TitleUI titleUI;
    GameUI gameUI;
    
    GameOverUI gameOverUI;
    InputSettingUI inputSettingUI;

    //패널 UI //다른 UI들과 겹쳐서 켜졌다가 꺼질 수 있는 UI들
    SystemMessageUI systemMessageUI;

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
        titleUI = GetComponentInChildren<TitleUI>(true);
        if(titleUI != null)
            titleUI.Init();
        gameUI = GetComponentInChildren<GameUI>(true);
        if (gameUI != null)
        {
            //SetPlayGame();
            gameUI.Init();
        }
            
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        if(gameOverUI != null)
            gameOverUI.Init();
        inputSettingUI = GetComponentInChildren<InputSettingUI>(true);
        if (inputSettingUI != null)
            inputSettingUI.Init();
        systemMessageUI = GetComponentInChildren<SystemMessageUI>(true);
        if(systemMessageUI != null)
            systemMessageUI.Init();

    }

    public void Update()
    {
        // 작동 테스트용 코드
        if (gameUI != null)
        {
            gameUI.UpdateEnergyBar();
            gameUI.UpdateCurrentScoreText();
            gameUI.UpdateBestScoreText();

            if (gameUI.currentHP == 0)
            {
                SetGameOver();
            }
        }

        if(gameOverUI != null)
        {
            gameOverUI.UpdateCurrentScoreText();
            gameOverUI.UpdateBestScoreText();
        }

        
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    //일부러 패널들은 다른 UI들이랑 겹쳐서 보일 수 있도록 메소드를 따로 뻈습니다.
    //(메인 게임 씬에 설정이 있다거나 했을 때, 되돌아갈 State 구분 안하고 그냥 패널만 꺼도 되도록
    public void ShowInputSettingPanel()
    {
        if (inputSettingUI != null)
            inputSettingUI.SetActive(UIState.InputSetting);
    }
    public void ShowSystemMessagePanel(string message, bool isWaitingCloseButton)
    {
        if (systemMessageUI != null)
        {
            systemMessageUI.SetActive(UIState.SystemMessage);
            systemMessageUI.UpdateMessagePanel(message, isWaitingCloseButton);
        }
        else
        {
            Debug.Log("SystemMessageUI가 존재하지 않습니다.");
        }
    }
    public void CloseSystemMessagePanel()
    {
        if (systemMessageUI != null)
        {
            systemMessageUI.OnCloseButtonClicked();
        }
        else
        {
            Debug.Log("SystemMessageUI가 존재하지 않습니다.");
        }
    }

    public void ChangeState(UIState state)
    {
        currentState = state;

        if(titleUI != null)
            titleUI.SetActive(currentState);
        if(gameUI != null) 
            gameUI.SetActive(currentState);
        if(gameOverUI!= null)
            gameOverUI.SetActive(currentState);
        if(inputSettingUI!= null)
            inputSettingUI.SetActive(currentState);
    }
}

