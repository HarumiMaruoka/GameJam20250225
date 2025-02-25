using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UFOStats", menuName = "UFOStats", order = 0)]
public class UFOStats : ScriptableObject
{
    public float MaxSpeed = 8f;
    public float Acceleration = 2f;
    public float RotationSpeed = 600f;
    public float Deceleration = 8f;
}
