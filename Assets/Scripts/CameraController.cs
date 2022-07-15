using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 5.0f;
    [SerializeField] float _rotationSpeed = 100.0f;
    [SerializeField] CinemachineVirtualCamera _CMVirtualCam;
    [SerializeField] float _zoomStep = 1.0f;
    [SerializeField] float _zoomSpeed = 5.0f;

    const float _minFollowYOffset = 2.0f;
    const float _maxFollowYOffset = 12.0f;

    CinemachineTransposer _cinemachineTransposer;
    Vector3 _targetFollowOffset;

    private void Awake()
    {
        _cinemachineTransposer = _CMVirtualCam.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }

    void Update()
    {
        HandelMovement();
        HandleRotation();
        HandleZoom();
    }
    private void HandelMovement()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = +1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = +1.0f;
        }

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * _movementSpeed * Time.deltaTime;
    }
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1.0f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1.0f;
        }

        transform.eulerAngles += rotationVector * _rotationSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffset.y -= _zoomStep;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffset.y += _zoomStep;
        }

        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, _minFollowYOffset, _maxFollowYOffset);
        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * _zoomSpeed);
    }
}
