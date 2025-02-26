using Confront.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
public class FadeSystem : MonoBehaviour
{
    [SerializeField, Tooltip("チェックが入っていればフェードイン、入ってなければフェードアウト")] bool fadeIn = true;
    [SerializeField, Tooltip("DelayTime")]private int _fadeDelayTime = 10;
    FadeImage fadeImage;
    GameObject bgmPlayer;
    BGMPlayer bGMPlayer;

    private const int _fadeLoop = 100;
    
    void Start()
    {
        fadeImage = GetComponent<FadeImage>();
        bgmPlayer = GameObject.Find("BGMPlayer");
    }

    public async UniTask FadeIn()
    {
        fadeIn = true;
        await FadeIn_Out();
        
    }

    public async UniTask FadeOut()
    {
        AudioManager.StopBGM();
        fadeIn = false;
        await FadeIn_Out();
    }

    private async UniTask FadeIn_Out()
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
