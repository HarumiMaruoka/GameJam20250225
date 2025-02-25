using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public CanvasGroup CanvasGroup;
    public float FadeDuration;
    public string NextSceneName = "Result";

    private void Awake()
    {
        if (Instance)
        {
            Debug.Log("Multiple TimeManagers detected. Deleting the newest one.");
            Destroy(this);
            return;
        }
        Instance = this;
        TimeLimit = Time;
        OnTimeOver += TimeOver;
    }

    public float TimeLimit;
    public float Time;
    public event Action OnTimeOver;

    private void Update()
    {
        if (TimeLimit > 0)
        {
            TimeLimit -= UnityEngine.Time.deltaTime;
            if (TimeLimit <= 0)
            {
                TimeLimit = 0;
                OnTimeOver?.Invoke();
            }
        }
    }

    private bool _isFading = false;

    private async void TimeOver()
    {
        if (_isFading) return;
        _isFading = true;

        OnTimeOver -= TimeOver;

        for (float t = 0; t < FadeDuration; t += UnityEngine.Time.deltaTime)
        {
            if (!this) return;
            CanvasGroup.alpha = t / FadeDuration;
            await UniTask.Yield();
        }

        SceneManager.LoadScene(NextSceneName);
    }
}
