using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KetteiButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _sceneName;
    [SerializeField] private EnterUserName _enterUserName;

    void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (_enterUserName.userName.Length > 0)
        {
            SceneManager.LoadScene(_sceneName);
        }
        else
        {
            _enterUserName.userName = "Player";
            SceneManager.LoadScene(_sceneName);
        }
    }
}