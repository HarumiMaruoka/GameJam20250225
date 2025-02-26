using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterUserName : MonoBehaviour
{
    [SerializeField, Header("文字数制限")] private int _nameLimit;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _inputText;
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
            RankingManager.Instance.UserNameSet(userName);
        }
    }
}
