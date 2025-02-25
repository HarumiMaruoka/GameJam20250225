using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class AreaExpansionItem : ItemController
{
    public float Duration;
    public float ExpansionAmount;

    public async override void OnPickup()
    {
        var player = UFOController.Instance;
        player.CircleCollider2D.radius += ExpansionAmount;

        for (float t = 0; t < Duration; t += Time.deltaTime)
        {
            if (!player) return;
            await UniTask.Yield();
        }

        if (!player) return;
        player.CircleCollider2D.radius -= ExpansionAmount;
    }
}
