using NexEditor;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UFOController : MonoBehaviour
{
    public static UFOController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Components
    [NonSerialized] public Rigidbody2D Rigidbody2D;
    [NonSerialized] public UFOStats UFOStats;

    // Settings
    public StatsType DefaultStatsType;
    [Expandable] public UFOStats UFOStatsA;
    [Expandable] public UFOStats UFOStatsB;
    [Expandable] public UFOStats UFOStatsX;
    [Expandable] public UFOStats UFOStatsY;

    [NonSerialized] public UFOStats AdditionalStats;

    // Properties
    [NonSerialized] public float CurrentSpeed;
    [NonSerialized] public float TargetAngle;

    private void Start()
    {
        UFOStats = GetStats(DefaultStatsType);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        AdditionalStats = ScriptableObject.CreateInstance<UFOStats>();
        AdditionalStats.Reset();
    }

    public void Update()
    {
        HandleMovement();
        SwitchUFOStats();
    }

    private void HandleMovement()
    {
        var moveInput = InputHandler.InGameActions.Move.ReadValue<Vector2>();
        var isMoveInputZero = moveInput == Vector2.zero;

        var maxSpeed = UFOStats.MaxSpeed + AdditionalStats.MaxSpeed;
        var acceleration = UFOStats.Acceleration + AdditionalStats.Acceleration;
        var rotationSpeed = UFOStats.RotationSpeed + AdditionalStats.RotationSpeed;
        var deceleration = UFOStats.Deceleration + AdditionalStats.Deceleration;

        if (isMoveInputZero)
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, 0, deceleration * Time.deltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, maxSpeed, acceleration * Time.deltaTime);
            TargetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, TargetAngle, rotationSpeed * Time.deltaTime));

        Rigidbody2D.velocity = transform.right * CurrentSpeed;
    }

    private void SwitchUFOStats()
    {
        if (InputHandler.InGameActions.A.triggered && UFOStatsA)
        {
            UFOStats = UFOStatsA;
        }
        else if (InputHandler.InGameActions.B.triggered && UFOStatsB)
        {
            UFOStats = UFOStatsB;
        }
        else if (InputHandler.InGameActions.X.triggered && UFOStatsX)
        {
            UFOStats = UFOStatsX;
        }
        else if (InputHandler.InGameActions.Y.triggered && UFOStatsY)
        {
            UFOStats = UFOStatsY;
        }
    }

    public UFOStats GetStats(StatsType type)
    {
        if (type == StatsType.A && UFOStatsA) return UFOStatsA;
        if (type == StatsType.B && UFOStatsB) return UFOStatsB;
        if (type == StatsType.X && UFOStatsX) return UFOStatsX;
        if (type == StatsType.Y && UFOStatsY) return UFOStatsY;

        return ScriptableObject.CreateInstance<UFOStats>();
    }

    public enum StatsType
    {
        A, B, X, Y
    }
}
