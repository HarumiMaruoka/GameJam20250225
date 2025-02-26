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
            $"  ç≈çÇë¨ìx: {_ufoController.MaxSpeed}\n" +
            $"  â¡ë¨óÕ: {_ufoController.Acceleration}\n" +
            $"  ê˘âÒóÕ: {_ufoController.RotationSpeed}\n" +
            $"  å∏ë¨óÕ: {_ufoController.Deceleration}";
    }
}
