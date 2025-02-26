using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ResultText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField,Header("スコアの表示にかかる時間")] float _scoreChangeDuration;
    private CancellationTokenSource _cancellationTokenSource;
    float _targetScore;
    float _currentScore;
    

    /// <summary>
    /// スコアの値を受け取るメソッド
    /// </summary>
    public async UniTask GetScoreValue()
    {
        await OnScoreChanged(ScoreManager.Score);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public async UniTask OnScoreChanged(float value)
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
            _scoreText.text = _currentScore.ToString("F0");
            await UniTask.Yield();
        }

        _currentScore = _targetScore;
        _scoreText.text = _currentScore.ToString("F0");
    }
}
