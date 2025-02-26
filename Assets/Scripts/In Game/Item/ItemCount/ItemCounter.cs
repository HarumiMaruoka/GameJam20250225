using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ItemCounter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        _prefabs = Resources.LoadAll<ItemController>("Items");
        for (int i = 0; i < _prefabs.Length; i++)
        {
            _itemCounts.Add(_prefabs[i], 0);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "In Game")
        {
            for (int i = 0; i < _prefabs.Length; i++)
            {
                _itemCounts[_prefabs[i]] = 0;
            }
        }
    }

    public static void AddItem(ItemController item)
    {
        if (_itemCounts.ContainsKey(item))
        {
            _itemCounts[item]++;
        }
    }

    private static ItemController[] _prefabs;
    private static Dictionary<ItemController, int> _itemCounts = new Dictionary<ItemController, int>();
    public static IReadOnlyDictionary<ItemController, int> ItemCounts => _itemCounts;
}
