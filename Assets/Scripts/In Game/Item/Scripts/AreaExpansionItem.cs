using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class AreaExpansionItem : ItemController
{
    public float ExpansionAmount;

    public static float MinExpansionAmount = 0.4f;
    public static float MaxExpansionAmount = 1.5f;

    public override void OnPickup()
    {
        var player = UFOController.Instance;
        var searchlight = player.Searchlight;
        var scale = searchlight.localScale;
        searchlight.localScale = new Vector3(scale.x + ExpansionAmount, scale.y, scale.z);
    }
}
