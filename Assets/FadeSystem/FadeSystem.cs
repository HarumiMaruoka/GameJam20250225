using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Cysharp.Threading;

public class FadeSystem : MonoBehaviour
{
    //[SerializeField, Tooltip("ここにフェードインorフェードアウトするまでの時間を設定する")] float toFadeTime = 1;
    [SerializeField, Tooltip("チェックが入っていればフェードイン、入ってなければフェードアウト")] bool fadeIn = true;
    [SerializeField, Tooltip("DelayTime")]private int _fadeDelayTime = 10;
    FadeImage fadeImage;

    private const int _fadeLoop = 100;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<FadeImage>();
    }

    private void Update()
    {
        Debug.Log("読み込みは終了済み");
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            FadeOut();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.V))
        {
            FadeIn();
        }

        if (fadeImage.Range < 0)
        {
            fadeImage.Range = 0;
        }
        else if (fadeImage.Range > 1)
        {
            fadeImage.Range = 1;
        }
    }

    public async UniTask FadeIn()
    {
        fadeIn = true;
        await FadeIn_Out();
    }

    public async UniTask FadeOut()
    {
        fadeIn = false;
        await FadeIn_Out();
    }

    public async UniTask FadeIn_Out()
    {
        for (int i = 0; i < _fadeLoop; i++)
        {
            if (fadeIn)
            {
                fadeImage.Range -= 0.01f;
                await UniTask.Delay(_fadeDelayTime);
            }
            else
            {
                fadeImage.Range += 0.01f;
                await UniTask.Delay(_fadeDelayTime);
            }
        }
    }
}
