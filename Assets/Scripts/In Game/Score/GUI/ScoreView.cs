using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    private float _currentScore;
    private float _targetScore;
    [SerializeField] private float _scoreChangeDuration = 0.5f;

    private void Awake()
    {
        ScoreManager.OnScoreChanged += OnScoreChanged;
        _currentScore = ScoreManager.Score;
        text.text = $"Score: {_currentScore.ToString("F0")}";
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChanged -= OnScoreChanged;
    }

    private CancellationTokenSource _cancellationTokenSource;

    private async void OnScoreChanged(float value)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        _targetScore = value;
        if (_currentScore == _targetScore) return;

        var start = _currentScore;
        var end = _targetScore;
        for (float t = 0f; t < _scoreChangeDuration; t += Time.deltaTime)
        {
            if (token.IsCancellationRequested) return;
            if (!this) return;
            _currentScore = Mathf.Lerp(start, end, t / _scoreChangeDuration);
            text.text = $"Score: {_currentScore.ToString("F0")}";
            await UniTask.Yield();
        }

        _currentScore = _targetScore;
        text.text = $"Score: {_currentScore.ToString("F0")}";
    }
}
