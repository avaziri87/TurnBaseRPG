using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    GridSystem _gridSystem;
    void Start()
    {
        _gridSystem = new GridSystem(10, 10, 2.0f);
    }

    private void Update()
    {
        Debug.Log(_gridSystem.GetGridPosition(MouseWorld.GetMousePosition()));
    }
}
