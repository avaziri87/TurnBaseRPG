using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] Button _endTurnButton;
    [SerializeField] TextMeshProUGUI _turnText;

    private void Start()
    {
        _endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        UpdateTurnText();
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        UpdateTurnText();
    }

    void UpdateTurnText()
    {
        _turnText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }
}
