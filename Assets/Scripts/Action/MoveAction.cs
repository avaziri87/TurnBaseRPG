using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveAction : BaseAction
{
    [SerializeField] Animator _animator;

    [SerializeField] float _moveSpeed = 4.0f;
    [SerializeField] float _stopDistance = 0.1f;
    [SerializeField] float _turnSpeed = 1;
    [SerializeField] int _maxMoveDistance = 4;

    Vector3 _targetPosition;

    protected override void Awake()
    {
        base.Awake();
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (!_isActive) return;
        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, _targetPosition) > _stopDistance)
        {
            transform.position += moveDirection * Time.deltaTime * _moveSpeed;
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _isActive = false;
            _onActionComplete();
            _animator.SetBool("IsWalking", false);
        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, _turnSpeed * Time.deltaTime);
    }
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        _isActive = true;
        _onActionComplete = onActionComplete;
        _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public void SetTargePosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public override List<GridPosition> GetValidActionGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for(int x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for(int z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
    public override string GetActionName()
    {
        return "Move";
    }
}
