using System;
using UnityEngine;

public class StatsChangingItem : ItemController
{
    public float MaxSpeed = 0;
    public float Acceleration = 0;
    public float RotationSpeed = 0;
    public float Deceleration = 0;

    public override void OnPickup()
    {
        var ufoController = UFOController.Instance;
        ufoController.AdditionalStats.MaxSpeed += MaxSpeed;
        ufoController.AdditionalStats.Acceleration += Acceleration;
        ufoController.AdditionalStats.RotationSpeed += RotationSpeed;
        ufoController.AdditionalStats.Deceleration += Deceleration;
    }
}
