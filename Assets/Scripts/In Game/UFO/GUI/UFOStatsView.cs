using System;
using UnityEngine;

public class UFOStatsView : MonoBehaviour
{
    [SerializeField]
    private UFOController _ufoController;
    [SerializeField]
    private TMPro.TextMeshProUGUI _view;

    private void Update()
    {
        _view.text =
            $"Stats\n" +
            $"  �ō����x: {_ufoController.MaxSpeed.ToString("F0")}\n" +
            $"  ������: {_ufoController.Acceleration.ToString("F0")}\n" +
            $"  �����: {_ufoController.RotationSpeed.ToString("F0")}\n" +
            $"  ������: {_ufoController.Deceleration.ToString("F0")}";
    }
}
