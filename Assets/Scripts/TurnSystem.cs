using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    public EventHandler OnTurnChange;
    public static TurnSystem Instance { get; private set; }

    int _turnNumber = 1;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Unit Action System" + transform + " " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }
    public void NextTurn()
    {
        _turnNumber++;
        OnTurnChange?.Invoke(this, EventArgs.Empty);
    }
    public int GetTurnNumber()
    {
        return _turnNumber;
    }
}
