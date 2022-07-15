using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebug : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMesh;
    GridObject _gridObject;
    public void SetyGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;
    }

    private void Update()
    {
        _textMesh.text = _gridObject.ToString();
    }
}
