using System;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    [Header("Speed")]
    public float MaxSpeed = 2f;
    public float Acceleration = 12f;
    public float Deceleration = 8f;
    [Header("Stop")]
    public float MinStopDuration = 2.5f;
    public float MaxStopDuration = 5f;
    [Header("Move")]
    public float MinMoveDuration = 3f;
    public float MaxMoveDuration = 6f;
    public float NoiseScale = 0.1f;

    private Vector2 _velocity;

    private float _elapsedTime;
    private float _stopDuration;
    private float _moveDuration;

    private float _noiseOffsetX;
    private float _noiseOffsetY;

    private bool _isMoving = false;

    private void Start()
    {
        _stopDuration = UnityEngine.Random.Range(MinStopDuration, MaxStopDuration);
        _noiseOffsetX = UnityEngine.Random.Range(0f, 100f);
        _noiseOffsetY = UnityEngine.Random.Range(0f, 100f);
        _isMoving = UnityEngine.Random.value > 0.5f;
    }

    private void Update()
    {
        if (PauseManager.IsPaused) return;

        _elapsedTime += Time.deltaTime;
        if (_isMoving)
        {
            Move();
            if (_elapsedTime >= _moveDuration)
            {
                _isMoving = false;
                _stopDuration = UnityEngine.Random.Range(MinStopDuration, MaxStopDuration);
                _elapsedTime = 0;
            }
        }
        else
        {
            Stop();
            if (_elapsedTime >= _stopDuration)
            {
                _isMoving = true;
                _moveDuration = UnityEngine.Random.Range(MinMoveDuration, MaxMoveDuration);
                _elapsedTime = 0;
            }
        }

        transform.position += (Vector3)_velocity * Time.deltaTime;
    }

    private void Move()
    {
        // Perlinノイズを使用してランダムなオフセットを生成
        float noiseX = Mathf.PerlinNoise(Time.time * NoiseScale + _noiseOffsetX, 0f) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0f, Time.time * NoiseScale + _noiseOffsetY) - 0.5f;
        float angle = Mathf.Atan2(noiseY, noiseX);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        var random = new Vector2(x, y);

        _velocity = Vector2.Lerp(_velocity, random * MaxSpeed, Acceleration * Time.deltaTime);
    }

    private void Stop()
    {
        _velocity = Vector2.Lerp(_velocity, Vector2.zero, Deceleration * Time.deltaTime);
    }
}
