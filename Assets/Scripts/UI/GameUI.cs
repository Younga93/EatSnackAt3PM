using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField]private Image energyBarMeter;

    //테스트용 변수
    public int currentHP;
    public int maxHP;

    public int currentScore;
    public int bestScore;


    // 에너지바 업데이트 (연동 필요)
    public void UpdateEnergyBarMeter()
    {
        energyBarMeter.fillAmount = (float)currentHP / (float)maxHP;        //테스트
    }

    // 현재점수 업데이트 (연동 필요)
    public void UpdateCurrentScoreText()
    {
        currentScoreText.text = currentScore.ToString();        //테스트
    }

    // 최고점수 업데이트 (연동 필요)
    public void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();      //테스트
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
