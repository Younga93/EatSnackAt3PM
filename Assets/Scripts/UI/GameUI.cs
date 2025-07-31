using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Slider energyBar;

    //테스트용 변수
    public int currentHP;
    public int maxHP = 500;

    //public int currentScore;
    //public int bestScore;
    private void Start()
    {
        UpdateBestScoreText(PlayerPrefs.GetInt("BestScore", 0));
    }

    // 에너지바 업데이트 (연동 필요)
    public void UpdateEnergyBar(int currentHealth)
    {
        energyBar.value = (float)currentHealth / (float)maxHP;        //테스트
    }

    // 현재점수 업데이트 (연동 필요)
    public void UpdateCurrentScoreText(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();        //테스트
    }

    // 최고점수 업데이트 (연동 필요)
    public void UpdateBestScoreText(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();      //테스트
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
