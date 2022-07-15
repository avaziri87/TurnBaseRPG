using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnityChange;

    [SerializeField] Unit _selectedUnit;
    [SerializeField] LayerMask _unitLayerMask;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Unit Action System" + transform + " " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;

            _selectedUnit.Move();
        }
    }

    bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _unitLayerMask))
        {
            if(hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnityChange?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return _selectedUnit;
    }
}
