using System;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopupSystem : MonoBehaviour
{
    public static ScorePopupSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private ScorePopup _scorePopupPrefab;
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 1, 0);

    private Stack<ScorePopup> _pool = new Stack<ScorePopup>();

    public void ShowScorePopup(float score, Vector3 worldPosition)
    {
        ScorePopup scorePopup;
        if (_pool.Count > 0)
        {
            scorePopup = _pool.Pop();
        }
        else
        {
            scorePopup = Instantiate(_scorePopupPrefab, transform);
        }

        var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        scorePopup.transform.position = screenPosition + _offset;
        scorePopup.SetScore(score);
        scorePopup.gameObject.SetActive(true);
        scorePopup.OnFinished += ReturnToPool;
    }

    private void ReturnToPool(ScorePopup scorePopup)
    {
        scorePopup.OnFinished -= ReturnToPool;
        scorePopup.gameObject.SetActive(false);
        _pool.Push(scorePopup);
    }
}
