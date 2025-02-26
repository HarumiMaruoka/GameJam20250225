using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class ResultButton : MonoBehaviour
{
    [SerializeField] Button _resultButton;
    [SerializeField] string _sceneName;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] FadeSystem _fadeSystem;
    private CancellationTokenSource _cts;

    void Start()
    {
        _cts = new CancellationTokenSource();
        _resultButton.onClick.AddListener(SceneChange);
    }

    async void SceneChange()
    {
        _audioSource.Play();
        await SceneChanger.ChangeScene(_sceneName, _cts, async () => await _fadeSystem.FadeOut());
    }
    
}
