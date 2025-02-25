using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class FadeSystem : MonoBehaviour
{
    [SerializeField, Tooltip("ここにフェードインorフェードアウトするまでの時間を設定する")] float toFadeTime = 1;
    [SerializeField, Tooltip("チェックが入っていればフェードイン、入ってなければフェードアウト")] bool fadeIn = true;
    GameObject fadeCanvas;
    FadeImage fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        fadeCanvas = GameObject.FindWithTag("FadeCanvas");
        fadeImage = fadeCanvas.GetComponent<FadeImage>();
    }

    private void Update()
    {
        Debug.Log("読み込みは終了済み");
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            FadeOut(5);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.V))
        {
            FadeIn(1);
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

    public void FadeIn(float time)
    {
        toFadeTime = time;
        fadeIn = true;
        StartCoroutine("FadeIn_Out");
    }

    public void FadeOut(float time)
    {
        toFadeTime = time;
        fadeIn = false;
        StartCoroutine("FadeIn_Out");
    }

    IEnumerator FadeIn_Out()
    {
        for (int i = 0; i < 100; i++)
        {
            if (fadeIn)
            {
                fadeImage.Range -= 0.01f;
            }
            else
            {
                fadeImage.Range += 0.01f;
            }

            yield return new WaitForSeconds(toFadeTime / 100);
        }
    }
}
