using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _nextPanel;
    private void Start()
    {
        _button.onClick.AddListener(OnClickBack);
    }

    void OnClickBack()
    {
        _currentPanel.SetActive(false);
        _nextPanel.SetActive(true);
    }
}
