using TMPro;
using UnityEngine;

public class GetItemText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _texts;
    [SerializeField] ItemController[] _prefabs;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _texts[0].text = ItemCounter.ItemCounts[_prefabs[i]].ToString();
        }
    }
}
