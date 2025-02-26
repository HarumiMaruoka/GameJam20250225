using Confront.Audio;
using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float Score = 10;
    public AudioClip PickupSound;
    public ItemSpawner Pool;

    [NonSerialized]
    public int OriginalInstanceID;

    public static Vector2 DeathPointLeftTopOffset = new Vector2(-8.0f, 5.0f);
    public static Vector2 DeathPointRightBottomOffset = new Vector2(8.0f, -5.0f);
    internal ItemController original;

    public static Vector2 DeathPointLeftTop => ItemSpawner.ScreenLeftTop + DeathPointLeftTopOffset;
    public static Vector2 DeathPointRightBottom => ItemSpawner.ScreenRightBottom + DeathPointRightBottomOffset;

    public virtual void OnPickup() { }

    protected virtual void Update()
    {
        if (transform.position.x < DeathPointLeftTop.x || transform.position.x > DeathPointRightBottom.x ||
            transform.position.y > DeathPointLeftTop.y || transform.position.y < DeathPointRightBottom.y)
        {
            Pool.ReturnToPool(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == UFOController.Instance.gameObject)
        {
            AudioManager.PlaySE(PickupSound);
            ScoreManager.AddScore(Score);
            OnPickup();
            Pool.ReturnToPool(this);
        }
    }
}