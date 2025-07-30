using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;

    //public override void Init()
    //{
    //    restartButton.onClick.AddListener(OnClickRestartButton);
    //    homeButton.onClick.AddListener(OnClickHomeButton);
    //}

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickHomeButton()
    {
        Debug.Log("홈으로");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
