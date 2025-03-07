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
            $"  最高速度: {_ufoController.MaxSpeed.ToString("F0")}\n" +
            $"  加速力: {_ufoController.Acceleration.ToString("F0")}\n" +
            $"  旋回力: {_ufoController.RotationSpeed.ToString("F0")}\n" +
            $"  減速力: {_ufoController.Deceleration.ToString("F0")}";
    }
}
