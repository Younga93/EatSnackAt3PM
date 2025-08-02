using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessageUI : BaseUI
{
    [SerializeField] TextMeshProUGUI systemMessageText;
    [SerializeField] Button exitButton;


    public void UpdateMessagePanel(string message, bool isWaitingCloseButton)
    {
        systemMessageText.text = message;
        exitButton.gameObject.SetActive(isWaitingCloseButton);
    }
    public void OnCloseButtonClicked()
    {
        SoundManager.instance.ButtonSound();
        this.gameObject.SetActive(false);
        Debug.Log("끔?");
    }

    protected override UIState GetUIState()
    {
        return UIState.SystemMessage;
    }

    private void OnDisable()    //혹시나해서 꺼질때 메시지 초기화 되도록
    {
        systemMessageText.text = "";
    }
}
