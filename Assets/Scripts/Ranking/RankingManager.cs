using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.Search;

public class RankingManager : MonoBehaviour
{
    [SerializeField, Header("ランキングの表示数")]
    private int _rankingNum;

    [SerializeField, Header("データ保存先のKeyの名前")]
    private string _rankingDataName;

    //ランキングのList
    [SerializeField]
    private Dictionary<string,float> _ranking = new();

    public Dictionary<string, float> Ranking => _ranking;

    private EnterUserName _enterUserName;

    private void Start()
    {
        _enterUserName = FindAnyObjectByType<EnterUserName>();
    }

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
                string[] nameAndScore = saveData.Split(" ");
                _ranking.Add(nameAndScore[0], float.Parse(nameAndScore[1]));
                Debug.Log(saveData.ToString());
            }
            _ranking.OrderByDescending(x => x.Value);
            if (_ranking.Count > _rankingNum)
            {
                for (int count = _ranking.Count; count > _rankingNum; count--)
                {
                    float minValue = _ranking.Values.Min();
                    _ranking.Remove(_ranking.FirstOrDefault(x => x.Value == minValue).Key);
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
            PlayerPrefs.SetString(_rankingDataName, _enterUserName.userName + " " + ScoreManager.Score.ToString());
        }
        else
        {
            PlayerPrefs.SetString(_rankingDataName, PlayerPrefs.GetString(_rankingDataName) + "," + _enterUserName.userName + " " + ScoreManager.Score.ToString());
        }

        if(_ranking.Count < _rankingNum)
        {
            _ranking.Add(_enterUserName.userName, ScoreManager.Score);
        }
        Debug.Log(_rankingDataName + "に" + PlayerPrefs.GetString(_rankingDataName) + "をセーブしました");
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        _ranking.Clear();
    }
}
