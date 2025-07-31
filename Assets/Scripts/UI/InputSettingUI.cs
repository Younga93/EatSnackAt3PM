using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InputSettingUI : BaseUI
{

    private string inputBindingKey; //PlayerPrefs 키
    private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();    //사용..? 할일... 있나?

    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        inputBindingKey = InputManager.Instance.inputBindingKey;
        if (!PlayerPrefs.HasKey(inputBindingKey))
        {
            Debug.Log($"PlayerPrefs에 {inputBindingKey} 값이 없습니다, 기본 셋팅으로 돌아갑니다.");
            InputManager.Instance.ResetBinding();   //리셋 후 playerprefs에 저장함
        }
        keyValuePairs = InputManager.Instance.GetCurrentBindingDictionary();
        UpdateButtons();

        //this.gameObject.SetActive(false);
    }
    private void UpdateButtons()
    {
        keyValuePairs = InputManager.Instance.GetCurrentBindingDictionary();
        ChangeButtonText(jumpButton, keyValuePairs["Jump"]);
        ChangeButtonText(slideButton, keyValuePairs["Slide"]);
        ChangeButtonText(attackButton, keyValuePairs["Attack"]);
    }
    public void OnChangeJumpKeyButtonClicked()
    {
        InputManager.Instance.StartNewKeyBinding("Jump", 0, newKey =>
        {
            if(!string.IsNullOrEmpty(newKey) || newKey != "")
            {
                ChangeButtonText(jumpButton, newKey);
            }
            else
            {
                Debug.Log("키 바인딩에 실패하였습니다.");
            }
        });
    }
    public void OnChangeSlideKeyButtonClicked()
    {
        InputManager.Instance.StartNewKeyBinding("Slide", 0, newKey =>
        {
            if (!string.IsNullOrEmpty(newKey) || newKey != "")
            {
                ChangeButtonText(slideButton, newKey);
            }
            else
            {
                Debug.Log("키 바인딩에 실패하였습니다.");
            }
        });
    }
    public void OnChangeAttackKeyButtonClicked()
    {
        InputManager.Instance.StartNewKeyBinding("Attack", 0, newKey =>
        {
            if (!string.IsNullOrEmpty(newKey) || newKey != "")
            {
                ChangeButtonText(attackButton, newKey);
            }
            else
            {
                Debug.Log("키 바인딩에 실패하였습니다.");
            }
        });
    }
    public void OnResetButtonClicked()
    {
        InputManager.Instance.ResetBinding();
        UpdateButtons();
    }
    public void OnCloseButtonClicked()
    {
        UIManager.Instance.ChangeState(UIState.Title);
    }

    public void ChangeButtonText(Button button, string keyText)
    {
        if(keyText.StartsWith('/'))
        {
            keyText = keyText.Substring(1);
        }
        button.GetComponentInChildren<TextMeshProUGUI>().text = keyText;
    }

    protected override UIState GetUIState()
    {
        return UIState.InputSetting;
    }
}
