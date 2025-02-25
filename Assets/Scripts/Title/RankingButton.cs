using UnityEngine;
using UnityEngine.UI;

public class RankingButton : MonoBehaviour
{
    [SerializeField] private GameObject _rankingPanel;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _titlePanel;
    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        _rankingPanel.SetActive(true);
        _titlePanel.gameObject.SetActive(false);
    }
}
