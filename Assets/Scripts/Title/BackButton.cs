using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject _rankingPanel;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _rankingButton;
    private void Start()
    {
        _button.onClick.AddListener(OnClickBack);
    }

    void OnClickBack()
    {
        _rankingPanel.SetActive(false);
        _rankingButton.SetActive(true);
    }
}
