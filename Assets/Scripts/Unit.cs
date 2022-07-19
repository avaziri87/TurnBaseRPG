using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const int _ActionPointsMax = 2;

    public static event EventHandler OnAnyActionPointsChange;
    GridPosition _gridPosition;
    MoveAction _moveAction;
    SpinAction _spinAction;
    BaseAction[] _baseActionArray;
    int _actionPoints = _ActionPointsMax;

    private void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }
    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
    }
    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != _gridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, _gridPosition, newGridPosition);
            _gridPosition = newGridPosition;
        }
    }
    public MoveAction GetMoveAction()
    {
        return _moveAction;
    }
    public SpinAction GetSpinAction()
    {
        return _spinAction;
    }
    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }
    public BaseAction[] GetBaseActionArray()
    {
        return _baseActionArray;
    }
    public bool CanSpendActionPointsForAction(BaseAction baseAction)
    {
        return _actionPoints >= baseAction.GetActionCost();
    }
    void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
        OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
    }
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(CanSpendActionPointsForAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionCost());
            return true;
        }
        else
        {
            return false;
        }
    }
    public int GetActionPoints()
    {
        return _actionPoints;
    }
    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        _actionPoints = _ActionPointsMax;
        OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
    }
}
