using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public enum UIState
{
    Title,
    Game,
    GameOver,
    InputSetting,
    SystemMessage,
    Loading
}

public class UIManager : MonoBehaviour
{
    //씬 UI  //서로 스위치 되는 UI들
    TitleUI titleUI;
    GameUI gameUI;
    
    GameOverUI gameOverUI;
    InputSettingUI inputSettingUI;
    LoadingUI loadingUI;

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
        loadingUI = GetComponentInChildren<LoadingUI>(true);
        if(loadingUI != null)
            loadingUI.Init();
        systemMessageUI = GetComponentInChildren<SystemMessageUI>(true);
        if(systemMessageUI != null)
            systemMessageUI.Init();
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "TitleScene":
                ChangeState(UIState.Title);
                break;
            case "GameScene":
                ChangeState(UIState.Game);
                break;
        }
    }

    private void Update()
    {
        // 작동 테스트용 코드
        if (UIManager.Instance.currentState == UIState.Game)
        {
            gameUI.UpdateEnergyBar();
            //gameUI.UpdateCurrentScoreText();  //게임매니저로부터 인자 받아서, UIManager의 UpdateGameUI를 호출하면, 현재 점수와 베스트 점수 확인하여 업데이트 하는 방법으로 UpdateGameScores 추가하여 변경하였음.
            //gameUI.UpdateBestScoreText();

            if (gameUI.currentHP == 0)
            {
                SetGameOver();
            }
        }

        if(UIManager.Instance.currentState == UIState.GameOver)
        {
            gameOverUI.UpdateCurrentScoreText();
            gameOverUI.UpdateBestScoreText();
        }
    }
    public void UpdateGameScores(int currentScore)
    {
        gameUI.UpdateCurrentScoreText(currentScore);
        if(currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            gameUI.UpdateBestScoreText(currentScore);
            //추후 게임종료 구현되면 그때 한번만 저장하는게 맞을 것 같음. 일단 임시
            PlayerPrefs.SetInt("BestScore", currentScore); //최고 점수 저장
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
    public void ShowInputSettingPanel()
    {
        if (inputSettingUI != null)
            inputSettingUI.SetActive(UIState.InputSetting);
    }

    //일부러 패널은 다른 UI들이랑 겹쳐서 보일 수 있도록 메소드를 따로 뻈습니다.
    //(메인 게임 씬에 설정이 있다거나 했을 때, 되돌아갈 State 구분 안하고 그냥 패널만 꺼도 되도록
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
        if (inputSettingUI != null)
            inputSettingUI.SetActive(currentState);
        if (loadingUI != null)
            loadingUI.SetActive(currentState);
    }
}

