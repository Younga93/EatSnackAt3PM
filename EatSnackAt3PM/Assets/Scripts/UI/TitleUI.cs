using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;

    public override void Init()
    {
        startButton.onClick.AddListener(OnClickStartButton);
        storeButton.onClick.AddListener(OnClickStoreButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // 시작버튼 누를시(연동필요)
    public void OnClickStartButton()
    {
        SoundManager.instance.ButtonSound();
        GameManager.Instance.InitGame();
    }

    public void OnClickStoreButton()
    {
        //To do: store 씬 진입 연결
        SoundManager.instance.ButtonSound();
        GameManager.Instance.LoadSceneWithCallback("StoreScene");
        Debug.Log("Store 진입하기");
    }
    // 설정버튼 누를시(연동필요)
    public void OnClickSettingButton()
    {
        SoundManager.instance.ButtonSound();
        UIManager.Instance.ChangeState(UIState.InputSetting);
    }

    // 나가기버튼 누를시(연동필요)
    public void OnClickExitButton()
    {
        SoundManager.instance.ButtonSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("나가기");
    }

    protected override UIState GetUIState()
    {
        return UIState.Title;
    }
}
