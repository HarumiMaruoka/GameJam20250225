using UnityEngine;
using UnityEngine.UI;

public class TItleRanking : MonoBehaviour
{
    [SerializeField] private int rankingNum = 10;　//仮
    [SerializeField] private Text[] rankingTexts;

    private void Start()
    {
        RankingInstance();
    }

    void RankingInstance()
    {
        string userName = "aaa";
        float score = 0;
        rankingTexts[0].text = $"{1}位 : {userName} , score{score}pt";
        for (int i = 2; i <= rankingNum; i++)
        {
            rankingTexts[i - 1].text = $"{i}位 : {userName} , score{score}pt";
        }
    }
}
