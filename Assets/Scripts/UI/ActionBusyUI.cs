using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChange += UnitActionSystem_OnBusyChange;
        Hide();
    }

    private void UnitActionSystem_OnBusyChange(object sender, bool isBusy)
    {
        if (isBusy) Show();
        else Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
