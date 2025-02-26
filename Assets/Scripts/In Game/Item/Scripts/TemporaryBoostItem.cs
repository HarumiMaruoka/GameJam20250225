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

        // �����Ńv���C���[�̃X�e�[�^�X���ꎞ�I�ɏ㏸������
        player.MultipleStats.MaxSpeed += MaxSpeedBoost;
        player.MultipleStats.Acceleration += AccelerationBoost;
        player.MultipleStats.RotationSpeed += RotationSpeedBoost;
        player.MultipleStats.Deceleration += DecelerationBoost;

        // �ҋ@
        for (float t = 0f; t < boostDuration; t += Time.deltaTime)
        {
            if (token.IsCancellationRequested) break;
            if (!player) return;
            await UniTask.Yield();
        }

        // �����Ńv���C���[�̃X�e�[�^�X�����ɖ߂�
        if (!player) return;
        player.MultipleStats.MaxSpeed -= MaxSpeedBoost;
        player.MultipleStats.Acceleration -= AccelerationBoost;
        player.MultipleStats.RotationSpeed -= RotationSpeedBoost;
        player.MultipleStats.Deceleration -= DecelerationBoost;
    }
}