using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button titleButton;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    //// 점수 테스트용 변수
    //public int currentScore;
    //public int bestScore;
    public override void Init()
    {
        restartButton.onClick.AddListener(OnClickRestartButton);
        titleButton.onClick.AddListener(OnClickTitleButton);
        Debug.Log("GameOverUI Init됨");
    }

    // 현재점수 업데이트
    public void UpdateCurrentScoreText(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }

    // 최고점수 업데이트
    public void UpdateBestScoreText(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }

    // 재시작 버튼 누를시
    public void OnClickRestartButton()
    {
        SoundManager.instance.ButtonSound();
        Debug.Log("Restart 버튼 눌림");
        UIManager.Instance.ChangeState(UIState.Game);
        GameManager.Instance.InitGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 타이틀 버튼 누를시
    public void OnClickTitleButton()
    {
        SoundManager.instance.ButtonSound();
        Debug.Log("TitleScene 버튼 눌림");
        GameManager.Instance.LoadSceneWithCallback("TitleScene");
        //Debug.Log("타이틀로");  //테스트용
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
