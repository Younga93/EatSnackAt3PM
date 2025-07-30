using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI nowScoreText;
    [SerializeField] private Image energyBarMeter;

    private void Start()
    {
        energyBarMeter = GetComponent<Image>();
    }

    public void UpdateEnergyBarMeter(float percentage)
    {
        //테스트용
        energyBarMeter.fillAmount = percentage;
    }

    public void UpdateNowScoreText(int score)
    {
        nowScoreText.text = score.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
