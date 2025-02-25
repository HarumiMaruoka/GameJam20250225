using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>シーン切り替え</summary>
public static class SceneChanger
{
    public static async UniTask ChangeScene(string sceneName,CancellationTokenSource cts, Func<UniTask> callback = null)
    {

        if (cts.IsCancellationRequested)
        {
            Debug.LogWarning("シーン切り替えがキャンセルされました");
            return;
        }
        
        if (callback != null)
        {
            await callback.Invoke();
        }

        SceneManager.LoadScene(sceneName);
    }
}