using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public virtual void Init()
    {

    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
