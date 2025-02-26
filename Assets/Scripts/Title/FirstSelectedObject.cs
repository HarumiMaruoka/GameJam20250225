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
