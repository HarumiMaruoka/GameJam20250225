using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        _itemSpawnData = Resources.LoadAll<ItemController>("Items");
    }

    private static ItemController[] _itemSpawnData;

    [Header("Range")]
    [SerializeField]
    private float _minSpawnRange = 5f;
    [SerializeField]
    private float _maxSpawnRange = 10f;

    [Header("Time")]
    [SerializeField]
    private float _minSpawnTime = 5f;
    [SerializeField]
    private float _maxSpawnTime = 10f;

    [Header("Items")]
    [SerializeField]
    private int _initialItems = 10;
    [SerializeField]
    private int _maxActiveItems = 100;

    private HashSet<ItemController> _activeItems = new HashSet<ItemController>();
    private Dictionary<int, Stack<ItemController>> _pool = new Dictionary<int, Stack<ItemController>>();

    public static Vector2 ScreenLeftTop => Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
    public static Vector2 ScreenRightBottom => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

    private float _spawnTime;

    private void Start()
    {
        _spawnTime = UnityEngine.Random.Range(_minSpawnTime, _maxSpawnTime);

        for (int i = 0; i < _itemSpawnData.Length; i++)
        {
            _pool.Add(_itemSpawnData[i].GetInstanceID(), new Stack<ItemController>());
        }

        for (int i = 0; i < _initialItems; i++)
        {
            SpawnItem();
        }
    }

    private void Update()
    {
        _spawnTime -= Time.deltaTime;
        if (_spawnTime <= 0)
        {
            if (_activeItems.Count < _maxActiveItems)
            {
                SpawnItem();
            }
            _spawnTime = UnityEngine.Random.Range(_minSpawnTime, _maxSpawnTime);
        }
    }

    private void SpawnItem()
    {
        var random = UnityEngine.Random.Range(_minSpawnRange, _maxSpawnRange);
        var spawnPosition = UFOController.Instance.transform.position +
            (Vector3)UnityEngine.Random.insideUnitCircle.normalized * random;

        var prefab = GetRandomItem();

        if (TryGetItemFromPool(prefab, out ItemController instance))
        {
            instance.transform.position = spawnPosition;
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
            instance.Pool = this;
            instance.Original = prefab;
        }
        _activeItems.Add(instance);
    }

    private bool TryGetItemFromPool(ItemController prefab, out ItemController instance)
    {
        if (_pool[prefab.GetInstanceID()].Count > 0)
        {
            instance = _pool[prefab.GetInstanceID()].Pop();
            return true;
        }
        else
        {
            instance = null;
            return false;
        }
    }

    public void ReturnToPool(ItemController item)
    {
        item.gameObject.SetActive(false);
        _pool[item.OriginalInstanceID].Push(item);
        _activeItems.Remove(item);
    }

    private ItemController GetRandomItem()
    {
        float totalProbability = 0;
        foreach (var itemSpawnData in _itemSpawnData)
        {
            totalProbability += itemSpawnData.Probability;
        }
        float randomValue = UnityEngine.Random.Range(0, totalProbability);
        float currentProbability = 0;
        foreach (var itemSpawnData in _itemSpawnData)
        {
            currentProbability += itemSpawnData.Probability;
            if (randomValue <= currentProbability)
            {
                return itemSpawnData;
            }
        }
        return _itemSpawnData[0];
    }
}