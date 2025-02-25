using System;
using UnityEngine;

public class AddTimeItem : ItemController
{
    public float TimeToAdd = 0;
    public override void OnPickup()
    {
        TimeManager.Instance.TimeLimit += TimeToAdd;
    }
}
