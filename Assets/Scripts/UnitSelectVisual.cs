using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitSelectVisual : MonoBehaviour
{
    [SerializeField] Unit _unit;

    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnityActionSystem_OnSelectedUnitChange;

        UpdateVisual();
    }

    void UnityActionSystem_OnSelectedUnitChange(object sender, EventArgs emtpy)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == _unit)
        {
            _meshRenderer.enabled = true;
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }
}
