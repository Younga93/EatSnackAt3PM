using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;

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
        return UIState.Home;
    }
}
