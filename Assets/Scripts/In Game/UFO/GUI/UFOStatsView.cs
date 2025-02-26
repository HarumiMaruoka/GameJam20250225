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
            $"  �ō����x: {_ufoController.MaxSpeed}\n" +
            $"  ������: {_ufoController.Acceleration}\n" +
            $"  �����: {_ufoController.RotationSpeed}\n" +
            $"  ������: {_ufoController.Deceleration}";
    }
}
