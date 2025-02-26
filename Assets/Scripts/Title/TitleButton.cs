using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _nextPanel;
    [SerializeField] private AudioSource _audioSource;
    private void Start()
    {
        _button.onClick.AddListener(OnClickBack);
    }

    void OnClickBack()
    {
        _currentPanel.SetActive(false);
        _nextPanel.SetActive(true);
        _audioSource.Play();
    }
}
