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
    [NonSerialized] public CircleCollider2D CircleCollider2D;

    // Stats
    public StatsType DefaultStatsType;
    [Expandable] public UFOStats UFOStatsA;
    [Expandable] public UFOStats UFOStatsB;
    [Expandable] public UFOStats UFOStatsX;
    [Expandable] public UFOStats UFOStatsY;

    [Expandable] public UFOStats MinUFOStats;
    [Expandable] public UFOStats MaxUFOStats;

    [NonSerialized] public UFOStats AdditionalStats;
    [NonSerialized] public UFOStats MultipleStats;

    public float MaxSpeed => Mathf.Clamp((UFOStats.MaxSpeed + AdditionalStats.MaxSpeed) * MultipleStats.MaxSpeed, MinUFOStats.MaxSpeed, MaxUFOStats.MaxSpeed);
    public float Acceleration => Mathf.Clamp((UFOStats.Acceleration + AdditionalStats.Acceleration) * MultipleStats.Acceleration, MinUFOStats.Acceleration, MaxUFOStats.Acceleration);
    public float RotationSpeed => Mathf.Clamp((UFOStats.RotationSpeed + AdditionalStats.RotationSpeed) * MultipleStats.RotationSpeed, MinUFOStats.RotationSpeed, MaxUFOStats.RotationSpeed);
    public float Deceleration => Mathf.Clamp((UFOStats.Deceleration + AdditionalStats.Deceleration) * MultipleStats.Deceleration, MinUFOStats.Deceleration, MaxUFOStats.Deceleration);

    // Properties
    [NonSerialized] public float CurrentSpeed;
    [NonSerialized] public float TargetAngle;

    private void Start()
    {
        UFOStats = GetStats(DefaultStatsType);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CircleCollider2D = GetComponent<CircleCollider2D>();

        AdditionalStats = ScriptableObject.CreateInstance<UFOStats>();
        MultipleStats = ScriptableObject.CreateInstance<UFOStats>();
        AdditionalStats.ResetStats();
        MultipleStats.ResetStats(1);
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
