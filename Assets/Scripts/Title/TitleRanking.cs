using TMPro;
using UnityEngine;

public class TitleRanking : MonoBehaviour
{
    [SerializeField] private int rankingNum = 10;　//仮
    [SerializeField] private TextMeshProUGUI[] _userNameTexts;
    [SerializeField] private TextMeshProUGUI[] _scoreTexts;

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
            _userNameTexts[i - 1].text = $"{userName}";
            _scoreTexts[i - 1].text = $"{score}";
        }
    }
}
