using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstSelectedObject : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        gameObject.GetComponent<Button>().OnSelect(null);
    }
}
//https://note.com/yamasho55/n/nbfc128e13082
//https://moon-bear.com/2020/05/25/ugui-gamepad/
