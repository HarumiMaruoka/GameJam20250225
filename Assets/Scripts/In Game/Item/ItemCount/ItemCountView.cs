using System;
using UnityEngine;

public class ItemCountView : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _countText;

    private void Update()
    {
        _countText.text = $"Count";

        _countText.text += $"\n  None   : {ItemCounter.ItemCounts[CountItemType.None]}";
        _countText.text += $"\n  Cow    : {ItemCounter.ItemCounts[CountItemType.Cow]}";
        _countText.text += $"\n  Human  : {ItemCounter.ItemCounts[CountItemType.Human]}";
        _countText.text += $"\n  Pig    : {ItemCounter.ItemCounts[CountItemType.Pig]}";
        _countText.text += $"\n  Chicken: {ItemCounter.ItemCounts[CountItemType.Chicken]}";
    }
}
