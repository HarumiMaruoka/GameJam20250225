using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraDistanceSlider : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _camera;
    [SerializeField]
    private Slider _slider;

    public float MinDistance = 5f;
    public float MaxDistance = 20f;

    private void Start()
    {
        _slider.minValue = MinDistance;
        _slider.maxValue = MaxDistance;
        _slider.value = _camera.m_Lens.OrthographicSize;
        _slider.onValueChanged.AddListener(SetCameraDistance);
    }

    public void SetCameraDistance(float distance)
    {
        _camera.m_Lens.OrthographicSize = distance;
    }
}
