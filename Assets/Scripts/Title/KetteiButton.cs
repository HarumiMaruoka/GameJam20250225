using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class KetteiButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _sceneName;
    [SerializeField] private EnterUserName _enterUserName;
    [SerializeField] private FadeSystem _fadeSystem;
    [SerializeField] private AudioSource _audioSource;
    private CancellationTokenSource _cts;

    void Start()
    {
        _cts = new CancellationTokenSource();
        _button.onClick.AddListener(async () =>await OnClick());
    }

    public async UniTask OnClick()
    {
        if (_enterUserName.userName.Length < 0)
        {
            _enterUserName.userName = "Player";
        }
        _audioSource.Play();
        await SceneChanger.ChangeScene(_sceneName,_cts,async () => await  _fadeSystem.FadeOut());
    }
}