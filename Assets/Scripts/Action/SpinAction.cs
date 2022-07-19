using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
    float _totalSpinAmount;
    void Update()
    {
        if (!_isActive) return;

        float spinAmount = 360.0f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAmount, 0);

        _totalSpinAmount += spinAmount;
        if (_totalSpinAmount >= 360)
        {
            _isActive = false;
            _onActionComplete();
        }
    }
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        _onActionComplete = onActionComplete;
        _isActive = true;
        _totalSpinAmount = 0.0f;
    }
    public override string GetActionName()
    {
        return "Spin";
    }
    public override List<GridPosition> GetValidActionGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();
        validGridPositionList.Add(unitGridPosition);
        return validGridPositionList;
    }
    public override int GetActionCost()
    {
        return 2;
    }
}
