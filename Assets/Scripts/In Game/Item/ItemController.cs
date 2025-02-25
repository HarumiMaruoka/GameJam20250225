using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float Score = 10;

    public virtual void OnPickup() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == UFOController.Instance.gameObject)
        {
            ScoreManager.AddScore(Score);
            OnPickup();
            Destroy(gameObject);
        }
    }
}