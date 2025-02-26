using System;
using UnityEngine;

public class ItemCountView : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _countText;

    private void Update()
    {
        _countText.text = $"Count";

        foreach (var itemCount in ItemCounter.ItemCounts)
        {
            _countText.text += $"\n  {itemCount.Key.name}: {itemCount.Value}";
        }
    }
}
