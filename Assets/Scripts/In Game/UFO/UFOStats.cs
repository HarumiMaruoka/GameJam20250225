using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UFOStats", menuName = "UFOStats", order = 0)]
public class UFOStats : ScriptableObject
{
    public float MaxSpeed = 8f;
    public float Acceleration = 2f;
    public float RotationSpeed = 600f;
    public float Deceleration = 8f;

    public void Reset()
    {
        MaxSpeed = 0f;
        Acceleration = 0f;
        RotationSpeed = 0f;
        Deceleration = 0f;
    }

    public override string ToString() =>
        $"Stats:\n" +
        $"  MaxSpeed: {MaxSpeed}\n" +
        $"  Acceleration: {Acceleration}\n" +
        $"  RotationSpeed: {RotationSpeed}\n" +
        $"  Deceleration: {Deceleration}";
}
