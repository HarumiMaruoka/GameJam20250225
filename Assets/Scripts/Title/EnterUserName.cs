using UnityEngine;
using UnityEngine.UI;

public class EnterUserName : MonoBehaviour
{
    [SerializeField, Header("文字数制限")] private int _nameLimit;
    [SerializeField] private InputField _inputField;
    [SerializeField] private Text _inputText;
    public string userName = "";

    public void CheckPlaceHolder()
    {
        if (_inputField.text.Length >= _nameLimit)
        {
            _inputField.text = _inputField.text[..3];
        }
        else
        {
            userName = _inputField.text;
        }
        Debug.Log(userName);
    }
}
