using Confront.Audio;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Object pool
    [NonSerialized] public ItemSpawner Pool;
    [NonSerialized] public ItemController Original;
    public int OriginalInstanceID => Original.GetInstanceID();

    [Header("Common")]
    public float Score = 10f;
    public float Probability = 3f;
    public float Lifetime = 10f;
    public AudioClip PickupSound;
    public CountItemType ItemType;

    [Header("Animation")]
    public float SpawnAnimationDuration = 0.5f;
    public float PickupAnimationDuration = 0.5f;

    private float _elapsedTime;
    private Vector2 _defaultSize;

    public static Vector2 DeathPointLeftTopOffset = new Vector2(-15.0f, 10.0f);
    public static Vector2 DeathPointRightBottomOffset = new Vector2(15.0f, -10.0f);

    public static Vector2 DeathPointLeftTop => ItemSpawner.ScreenLeftTop + DeathPointLeftTopOffset;
    public static Vector2 DeathPointRightBottom => ItemSpawner.ScreenRightBottom + DeathPointRightBottomOffset;

    public virtual void OnPickup() { }

    private bool _isPickedUp = false;

    protected virtual void Awake()
    {
        _defaultSize = transform.localScale;
    }

    protected virtual void OnEnable()
    {
        SpawnAnimate();
        _elapsedTime = 0;
        _isPickedUp = false;
    }

    protected virtual void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= Lifetime && CheckOutOfBounds())
        {
            Pool.ReturnToPool(this);
        }
    }

    private bool CheckOutOfBounds()
    {
        return transform.position.x < DeathPointLeftTop.x || transform.position.x > DeathPointRightBottom.x ||
               transform.position.y > DeathPointLeftTop.y || transform.position.y < DeathPointRightBottom.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isPickedUp) return;
        _isPickedUp = true;

        if (other.gameObject == UFOController.Instance.Searchlight.gameObject)
        {
            AudioManager.PlaySE(PickupSound);
            PickupAnimate();
        }
    }

    private CancellationTokenSource _cancellationTokenSource;

    private async void SpawnAnimate()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        transform.localScale = Vector3.zero;

        for (var timer = 0.0f; timer < SpawnAnimationDuration; timer += Time.deltaTime)
        {
            if (!this) return;
            if (!enabled) break;
            if (token.IsCancellationRequested) break;
            transform.localScale = Vector3.Lerp(Vector3.zero, _defaultSize, timer / SpawnAnimationDuration);
            await UniTask.Yield();
        }

        transform.localScale = _defaultSize;
    }

    private async void PickupAnimate()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        var target = UFOController.Instance.transform.position;
        var startPosition = transform.position;
        var startScale = transform.localScale;
        var timer = 0.0f;
        var ufo = UFOController.Instance.gameObject;

        while (timer < PickupAnimationDuration)
        {
            if (!this) return;
            if (!enabled) break;
            if (token.IsCancellationRequested) break;

            transform.position = Vector3.Lerp(startPosition, ufo.transform.position, timer / PickupAnimationDuration);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer / PickupAnimationDuration);
            timer += Time.deltaTime;
            await UniTask.Yield();
        }

        transform.localScale = startScale;
        ScorePopupSystem.Instance.ShowScorePopup(Score, ufo.transform.position);
        ScoreManager.AddScore(Score);
        ItemCounter.AddItem(Original);
        OnPickup();
        Pool.ReturnToPool(this);
    }
}

public enum CountItemType
{
    None, Cow, Human, Pig, Chicken
}