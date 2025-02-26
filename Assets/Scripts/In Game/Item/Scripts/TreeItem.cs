using System;
using UnityEngine;

public class TreeItem : ItemController
{
    [SerializeField] private UFOStats[] _table;

    public override void OnPickup()
    {
        var randam = _table[UnityEngine.Random.Range(0, _table.Length)];
        if (randam.Type == UFOStatsType.MaxSpeed)
        {
            UFOController.Instance.AdditionalStats.MaxSpeed += randam.Amount;
        }
        else if (randam.Type == UFOStatsType.Acceleration)
        {
            UFOController.Instance.AdditionalStats.Acceleration += randam.Amount;
        }
        else if (randam.Type == UFOStatsType.RotationSpeed)
        {
            UFOController.Instance.AdditionalStats.RotationSpeed += randam.Amount;
        }
        else if (randam.Type == UFOStatsType.Deceleration)
        {
            UFOController.Instance.AdditionalStats.Deceleration += randam.Amount;
        }
    }

    public enum UFOStatsType
    {
        MaxSpeed,
        Acceleration,
        RotationSpeed,
        Deceleration,
    }

    [Serializable]
    public struct UFOStats
    {
        public UFOStatsType Type;
        public float Amount;
    }
}