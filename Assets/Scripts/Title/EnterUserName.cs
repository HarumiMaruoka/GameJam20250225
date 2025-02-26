using TMPro;
using UnityEngine;

public class EnterUserName : MonoBehaviour
{
    [SerializeField, Header("文字数制限")] private int _nameLimit;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _inputText;
    public string userName = "";

    public void CheckUserName()
    {
        if (_inputField.text.Length >= _nameLimit)
        {
            _inputField.text = _inputField.text[.._nameLimit];
        }
        else
        {
            userName = _inputField.text;
        }
    }
}
