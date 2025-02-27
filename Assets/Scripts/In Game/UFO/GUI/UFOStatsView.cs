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
            $"  ç≈çÇë¨ìx: {_ufoController.MaxSpeed.ToString("F0")}\n" +
            $"  â¡ë¨óÕ: {_ufoController.Acceleration.ToString("F0")}\n" +
            $"  ê˘âÒóÕ: {_ufoController.RotationSpeed.ToString("F0")}\n" +
            $"  å∏ë¨óÕ: {_ufoController.Deceleration.ToString("F0")}";
    }
}
