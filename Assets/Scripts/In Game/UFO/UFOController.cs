using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UFOController : MonoBehaviour
{
    [NonSerialized]
    public Rigidbody2D Rigidbody2D;

    public UFOStats UFOStats;

    public float CurrentSpeed;
    public float TargetAngle;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!UFOStats) UFOStats = ScriptableObject.CreateInstance<UFOStats>();
    }

    public void Update()
    {
        var moveInput = InputHandler.InGameActions.Move.ReadValue<Vector2>();
        var isMoveInputZero = moveInput == Vector2.zero;

        if (isMoveInputZero)
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, 0, UFOStats.Deceleration * Time.deltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, UFOStats.MaxSpeed, UFOStats.Acceleration * Time.deltaTime);
            TargetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, TargetAngle, UFOStats.RotationSpeed * Time.deltaTime));

        Rigidbody2D.velocity = transform.right * CurrentSpeed;
    }
}
