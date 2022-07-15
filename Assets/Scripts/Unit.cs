using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Vector3 _targetPosition;
    GridPosition _gridPosition;

    [SerializeField] Animator _animator;

    [SerializeField] float _moveSpeed = 4.0f;
    [SerializeField] float _stopDistance = 0.1f;
    [SerializeField] float _turnSpeed = 1;

    private void Awake()
    {
        _targetPosition = transform.position;
    }
    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
    }
    private void Update()
    {
        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        Vector3.Distance(transform.position, _targetPosition);
        if(Vector3.Distance(transform.position, _targetPosition) > _stopDistance)
        {
            _animator.SetBool("IsWalking", true);
            transform.position += moveDirection * Time.deltaTime * _moveSpeed;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, _turnSpeed);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        Debug.Log(newGridPosition.ToString());
        if(newGridPosition != _gridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, _gridPosition, newGridPosition);
            _gridPosition = newGridPosition;
        }
    }

    public void Move()
    {
        _targetPosition = MouseWorld.GetMousePosition();
    }
}
