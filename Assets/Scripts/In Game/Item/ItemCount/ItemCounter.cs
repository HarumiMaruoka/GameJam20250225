using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ItemCounter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "In Game")
        {
            _itemCounts[CountItemType.None] = 0;
            _itemCounts[CountItemType.Cow] = 0;
            _itemCounts[CountItemType.Human] = 0;
            _itemCounts[CountItemType.Pig] = 0;
            _itemCounts[CountItemType.Chicken] = 0;
        }
    }

    public static void AddItem(ItemController item)
    {
        if (_itemCounts.ContainsKey(item.ItemType))
        {
            _itemCounts[item.ItemType]++;
        }
    }

    private static Dictionary<CountItemType, int> _itemCounts = new Dictionary<CountItemType, int>();
    public static IReadOnlyDictionary<CountItemType, int> ItemCounts => _itemCounts;
}
