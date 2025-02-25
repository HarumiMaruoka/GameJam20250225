using System;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    public UFOStats UFOStats;

    public float TargetSpeed;
    public Vector2 TargetAngle;

    public async void Update()
    {

    }
}
