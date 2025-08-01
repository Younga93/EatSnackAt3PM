using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreUI : BaseUI
{
    private int currentPoint;
    [SerializeField] TextMeshProUGUI pointText;

    private void Awake()
    {
        GetPointInfo();
    }
    protected override UIState GetUIState()
    {
        return UIState.Store;
    }
    public void GetPointInfo()
    {
        currentPoint = PlayerPrefs.GetInt("Point", 0);
        pointText.text = currentPoint.ToString();
    }
}
