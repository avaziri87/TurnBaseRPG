using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;
    public event EventHandler OnSelectedActionChange;
    public event EventHandler<bool> OnBusyChange;
    public event EventHandler OnActionStarted;

    [SerializeField] Unit _selectedUnit;
    [SerializeField] LayerMask _unitLayerMask;

    BaseAction _selectedAction;
    bool _isBusy;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Unit Action System" + transform + " " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(_selectedUnit);
    }

    private void Update()
    {
        if (_isBusy) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (TryHandleUnitSelection()) return;
        HandleSelectedAction();
    }
    void HandleSelectedAction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
            if(_selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                if(_selectedUnit.TrySpendActionPointsToTakeAction(_selectedAction))
                {
                    SetBusy();
                    _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                    OnActionStarted?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _unitLayerMask))
            {
                if(hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == _selectedUnit) return false;

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }

        return false;
    }
    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }
    public void SetSelectedAction(BaseAction baseAction)
    {
        _selectedAction = baseAction;
        OnSelectedActionChange?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return _selectedUnit;
    }
    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }
    void SetBusy()
    {
        _isBusy = true;
        OnBusyChange?.Invoke(this, _isBusy);
    }
    public void ClearBusy()
    {
        _isBusy = false;
        OnBusyChange?.Invoke(this, _isBusy);
    }
}
