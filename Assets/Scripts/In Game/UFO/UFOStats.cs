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

    public static UFOStats operator +(UFOStats a, UFOStats b)
    {
        var stats = CreateInstance<UFOStats>();

        stats.MaxSpeed = a.MaxSpeed + b.MaxSpeed;
        stats.Acceleration = a.Acceleration + b.Acceleration;
        stats.RotationSpeed = a.RotationSpeed + b.RotationSpeed;
        stats.Deceleration = a.Deceleration + b.Deceleration;
        return stats;
    }

    public override string ToString()
    {
        return
        $"Stats:\n" +
        $"  MaxSpeed: {MaxSpeed}\n" +
        $"  Acceleration: {Acceleration}\n" +
        $"  RotationSpeed: {RotationSpeed}\n" +
        $"  Deceleration: {Deceleration}";
    }
}
