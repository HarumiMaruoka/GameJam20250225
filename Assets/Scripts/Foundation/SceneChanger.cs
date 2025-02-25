using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>シーン切り替え</summary>
public static class SceneChanger
{
    public static async UniTask ChangeScene(string sceneName, Func<UniTask> callback = null)
    {
        if (callback != null)
        {
            await callback.Invoke();
        }

        SceneManager.LoadScene(sceneName);
    }
}