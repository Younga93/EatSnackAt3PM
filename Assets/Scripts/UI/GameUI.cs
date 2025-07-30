using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI nowScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField]private Image energyBarMeter;

    //테스트용 변수
    public int currentHP;
    public int maxHP;

    public int currentScore;
    public int bestScore;


    // 에너지바 업데이트
    public void UpdateEnergyBarMeter()
    {
        //테스트용
        energyBarMeter.fillAmount = (float)currentHP / (float)maxHP;
    }

    // 현재점수 업데이트
    public void UpdateNowScoreText()
    {
        nowScoreText.text = currentScore.ToString();
    }

    // 최고점수 업데이트
    public void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
