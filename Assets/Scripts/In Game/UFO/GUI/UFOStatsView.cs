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
            $"  Max Speed: {_ufoController.MaxSpeed}\n" +
            $"  Acceleration: {_ufoController.Acceleration}\n" +
            $"  Rotation Speed: {_ufoController.RotationSpeed}\n" +
            $"  Deceleration: {_ufoController.Deceleration}";
    }
}
