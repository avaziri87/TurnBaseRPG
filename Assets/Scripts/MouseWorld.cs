using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    static MouseWorld _instance;

    [SerializeField] LayerMask _mousePlaneLayerMask;

    private void Awake()
    {
        _instance = this;
    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _instance._mousePlaneLayerMask);

        return hit.point;
    }
}
