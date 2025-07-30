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

    // 점수 테스트용 변수
    public int currentScore;
    public int bestScore;

    public override void Init()
    {
        restartButton.onClick.AddListener(OnClickRestartButton);
        titleButton.onClick.AddListener(OnClickTitleButton);
    }

    // 현재점수 업데이트
    public void UpdateCurrentScoreText()
    {
        currentScoreText.text = currentScore.ToString();    //테스트
    }

    // 최고점수 업데이트
    public void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();      //테스트
    }

    // 재시작 버튼 누를시
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 타이틀 버튼 누를시
    public void OnClickTitleButton()
    {
        Debug.Log("타이틀로");  //테스트용
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
