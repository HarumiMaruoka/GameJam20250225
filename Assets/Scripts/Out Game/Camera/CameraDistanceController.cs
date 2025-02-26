using Cinemachine;
using System;
using UnityEngine;

public class CameraDistanceController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _camera;
    [SerializeField]
    private float _minDistance = 5f;
    [SerializeField]
    private float _maxDistance = 20f;
    [SerializeField]
    private float _changeSpeed = 10f;

    private void Update()
    {
        var inputValue = InputHandler.InGameActions.Zoom.ReadValue<float>();
        _camera.m_Lens.OrthographicSize = Mathf.Clamp(_camera.m_Lens.OrthographicSize + inputValue * _changeSpeed * Time.deltaTime, _minDistance, _maxDistance);
    }
}
