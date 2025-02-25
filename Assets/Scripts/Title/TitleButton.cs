using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _inputNamePanel;
    [SerializeField] private Button _titleButton;
    
    void Start()
    {
        _titleButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
         _titlePanel.SetActive(false);
         _inputNamePanel.SetActive(true);
    }
}
