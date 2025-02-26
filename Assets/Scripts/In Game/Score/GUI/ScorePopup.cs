using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField]
    private float _upwardSpeed = 1.0f;
    [SerializeField]
    private float _lifeTime = 1.0f;

    [SerializeField]
    private TMPro.TextMeshProUGUI _scoreText;
    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private Gradient _shadowGradient;

    private float _score;
    private float _timer = 0.0f;
    public event Action<ScorePopup> OnFinished;

    public void SetScore(float score)
    {
        _score = score;
        _scoreText.text = score.ToString("F0");
    }

    private void OnEnable()
    {
        if (_scoreText.text == "0") return;
        AnimateScorePopup();
    }

    private CancellationTokenSource _cancellationTokenSource;

    private async void AnimateScorePopup()
    {
        // Initialize values
        _timer = 0.0f;
        _scoreText.color = _gradient.Evaluate(0.0f);

        // Cancel previous animation
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        // Animate
        while (_timer < _lifeTime)
        {
            if (!this || !enabled) return;
            if (token.IsCancellationRequested) break;

            transform.position += Vector3.up * _upwardSpeed * Time.deltaTime;
            if (_score > 0)
            {
                _scoreText.color = _gradient.Evaluate(_timer / _lifeTime);
            }
            else
            {
                _scoreText.color = _shadowGradient.Evaluate(_timer / _lifeTime);
            }
            _timer += Time.deltaTime;
            await UniTask.Yield();
        }

        OnFinished?.Invoke(this);
    }
}
