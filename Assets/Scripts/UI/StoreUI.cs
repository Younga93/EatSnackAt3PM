using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : BaseUI
{
    private int currentPoint;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] Button exitButton;

    private void Awake()
    {
        UpdatePointInfo();
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    protected override UIState GetUIState()
    {
        return UIState.Store;
    }
    public void UpdatePointInfo()
    {
        currentPoint = PlayerPrefs.GetInt("Point", 0);
        pointText.text = currentPoint.ToString();
    }
    public void OnClickExitButton()
    {
        SoundManager.instance.ButtonSound();
        GameManager.Instance.LoadSceneWithCallback("TitleScene");
    }
}
