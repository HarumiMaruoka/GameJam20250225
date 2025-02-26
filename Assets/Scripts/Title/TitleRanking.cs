using System;
using TMPro;
using UnityEngine;

public class TitleRanking : MonoBehaviour
{
    [SerializeField] private int rankingNum = 10;　//仮
    [SerializeField] private TextMeshProUGUI[] rankingTexts;
    //private RankingManager _rankingManager;

    private void Start()
    {
        //_rankingManager = RankingManager.Instance;
    }

    private void OnEnable()
    {
        RankingUpdate();
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
