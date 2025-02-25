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

    // Stats
    public StatsType DefaultStatsType;
    [Expandable] public UFOStats UFOStatsA;
    [Expandable] public UFOStats UFOStatsB;
    [Expandable] public UFOStats UFOStatsX;
    [Expandable] public UFOStats UFOStatsY;

    [NonSerialized] public UFOStats AdditionalStats;

    public float MaxSpeed => UFOStats.MaxSpeed + AdditionalStats.MaxSpeed;
    public float Acceleration => UFOStats.Acceleration + AdditionalStats.Acceleration;
    public float RotationSpeed => UFOStats.RotationSpeed + AdditionalStats.RotationSpeed;
    public float Deceleration => UFOStats.Deceleration + AdditionalStats.Deceleration;

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

        if (isMoveInputZero)
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, 0, Deceleration * Time.deltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, MaxSpeed, Acceleration * Time.deltaTime);
            TargetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, TargetAngle, RotationSpeed * Time.deltaTime));

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
