using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "In Game") ResetScore();
    }

    public static event Action<float> OnScoreChanged;
    private static float _score;

    public static float Score
    {
        get => _score;
        set
        {
            if (SceneManager.GetActiveScene().name != "In Game")
            {
                Debug.LogWarning("Score can only be changed in the In Game scene.");
                return;
            }

            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public static string UserName { get; set; }

    public static void AddScore(float score)
    {
        Score += score;
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}
