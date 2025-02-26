using TMPro;
using UnityEngine;

public class GetItemText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _texts;
    void Start()
    {
            _texts[0].text = ItemCounter.ItemCounts[CountItemType.Human].ToString();
            _texts[1].text = ItemCounter.ItemCounts[CountItemType.Chicken].ToString();
            _texts[2].text = ItemCounter.ItemCounts[CountItemType.Cow].ToString();
            _texts[3].text = ItemCounter.ItemCounts[CountItemType.Pig].ToString();
    }
}
