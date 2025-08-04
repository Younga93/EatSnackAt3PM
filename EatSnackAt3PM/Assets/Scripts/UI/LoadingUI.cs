using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Loading;
    }

}
