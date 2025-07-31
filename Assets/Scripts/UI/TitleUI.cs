using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;

    public override void Init()
    {
        startButton.onClick.AddListener(OnClickStartButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // 시작버튼 누를시(연동필요)
    public void OnClickStartButton()
    {
        GameManager.Instance.LoadGame();
    }

    // 설정버튼 누를시(연동필요)
    public void OnClickSettingButton()
    {
        UIManager.Instance.ChangeState(UIState.InputSetting);
    }

    // 나가기버튼 누를시(연동필요)
    public void OnClickExitButton()
    {
        Debug.Log("나가기");
    }

    protected override UIState GetUIState()
    {
        return UIState.Title;
    }
}
