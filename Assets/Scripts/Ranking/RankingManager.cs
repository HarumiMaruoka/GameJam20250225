using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    [SerializeField, Header("ランキングの表示数")]
    private int _rankingNum;

    [SerializeField, Header("データ保存先のKeyの名前")]
    private string _rankingDataName;

    //ランキングのList
    [SerializeField]
    private List<float> _ranking = new();

    public List<float> Ranking => _ranking;

    /// <summary>
    /// 保存してあるデータを参照してランキング作成
    /// </summary>
    public void Load()
    {
        if (PlayerPrefs.HasKey(_rankingDataName))
        {
            string[] saveDatas = PlayerPrefs.GetString(_rankingDataName).Split(',');
            foreach (string saveData in saveDatas)
            {
                _ranking.Add(float.Parse(saveData));
                Debug.Log(saveData.ToString());
            }
            _ranking.Sort();
            _ranking.Reverse();
            if (_ranking.Count > _rankingNum)
            {
                for (int count = _ranking.Count; count > _rankingNum; count--)
                {
                    _ranking.Remove(_ranking.Min());
                }
            }
        }
    }

    /// <summary>
    /// セーブ
    /// </summary>
    public void Save()
    {
        if (!PlayerPrefs.HasKey(_rankingDataName))
        {
            PlayerPrefs.SetString(_rankingDataName, ScoreManager.Score.ToString());
        }
        else
        {
            PlayerPrefs.SetString(_rankingDataName, PlayerPrefs.GetString(_rankingDataName) + "," + ScoreManager.Score.ToString());
        }

        if(_ranking.Count < _rankingNum)
        {
            _ranking.Add(ScoreManager.Score);
        }
        Debug.Log(_rankingDataName + "に" + PlayerPrefs.GetString(_rankingDataName) + "をセーブしました");
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        _ranking.Clear();
    }
}
