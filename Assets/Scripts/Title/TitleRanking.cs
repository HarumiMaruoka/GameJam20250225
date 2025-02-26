using TMPro;
using UnityEngine;

public class TitleRanking : MonoBehaviour
{
    [SerializeField] private int rankingNum = 10;　//仮
    [SerializeField] private TextMeshProUGUI[] rankingTexts;

    private void Start()
    {
        Invoke(nameof(RankingUpdate), 0.1f); // 0.1秒後に実行
    }

    void RankingUpdate()
    {
        for (int i = 1; i <= rankingNum; i++)
        {
            var val = RankingManager.Instance.RankingBoard.ranking[i - 1];
            string userName = val._name;
            float score = val._score;
            rankingTexts[i - 1].text = $"{i}位 : {userName} , score{score}pt";
        }
    }
}
