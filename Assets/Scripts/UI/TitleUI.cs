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

    // Debug.Log는 전부 테스트용 입니다.
    public void OnClickStartButton()
    {
        Debug.Log("게임 시작");
    }
    public void OnClickSettingButton()
    {
        Debug.Log("설정 창");
    }
    public void OnClickExitButton()
    {
        Debug.Log("나가기");
    }

    protected override UIState GetUIState()
    {
        return UIState.Title;
    }
}
