using TMPro;
using UnityEngine;

public class GetItemText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _texts;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            _texts[i].text = ItemCounter.ItemCounts[CountItemType.Human].ToString();
            _texts[i].text = ItemCounter.ItemCounts[CountItemType.Chicken].ToString();
            _texts[i].text = ItemCounter.ItemCounts[CountItemType.Cow].ToString();
            _texts[i].text = ItemCounter.ItemCounts[CountItemType.Pig].ToString();
        }
    }
}
