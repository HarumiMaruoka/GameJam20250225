using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class TemporaryBoostItem : ItemController
{
    [Header("Boost Settings")]
    public float boostDuration = 5f;

    public float MaxSpeedBoost = 1.0f;
    public float AccelerationBoost = 1.0f;
    public float RotationSpeedBoost = 1.0f;
    public float DecelerationBoost = 1.0f;

    private static CancellationTokenSource _cancellationTokenSource;

    public override async void OnPickup()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        var player = UFOController.Instance;
        if (!player) return;

        // ここでプレイヤーのステータスを一時的に上昇させる
        player.MultipleStats.MaxSpeed += MaxSpeedBoost;
        player.MultipleStats.Acceleration += AccelerationBoost;
        player.MultipleStats.RotationSpeed += RotationSpeedBoost;
        player.MultipleStats.Deceleration += DecelerationBoost;

        // 待機
        for (float t = 0f; t < boostDuration; t += Time.deltaTime)
        {
            if (token.IsCancellationRequested) break;
            if (!player) return;
            await UniTask.Yield();
        }

        // ここでプレイヤーのステータスを元に戻す
        if (!player) return;
        player.MultipleStats.MaxSpeed -= MaxSpeedBoost;
        player.MultipleStats.Acceleration -= AccelerationBoost;
        player.MultipleStats.RotationSpeed -= RotationSpeedBoost;
        player.MultipleStats.Deceleration -= DecelerationBoost;
    }
}