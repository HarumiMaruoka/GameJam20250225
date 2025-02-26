using System.IO;
using UnityEngine;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    [SerializeField, Header("ランキングの総表示数")]
    private int _rankingNum;

    [SerializeField, Header("ランキングの保存先")]
    private RankingBoard _rankingBoard = new RankingBoard();

    private string _filePath;

    public RankingBoard RankingBoard => _rankingBoard;

    private void Start()
    {
        //ファイルにアクセスするためのPathを作る
        _filePath = Path.Combine(Application.persistentDataPath, "ranking.json");
    }

    /// <summary>
    /// 保存してあるデータを参照（最初に1回呼び出せばヨシッ！）
    /// </summary>
    public void Load()
    {
        //JsonDataがあればそれを読み込む
        if (File.Exists(_filePath))
        {
            _rankingBoard = JsonUtility.FromJson<RankingBoard>(File.ReadAllText(_filePath));
            Debug.Log(_filePath + "にデータがあるよ");

            foreach(var data  in _rankingBoard.ranking)
            {
                Debug.Log(data.Name + ":" + data.Score);
            }
        }
        else
        {
            Debug.Log("何もないよ");
        }
    }

    /// <summary>
    /// UserNameとScoreを"ゲーム終了時"に保存
    /// </summary>
    public void Save(string userName, float score)
    {
        //ランキングにスコアを追加
        _rankingBoard.ranking.Add(new ScoreData(userName, score));
        Debug.Log("Scoreを追加");

        //ランキングを降順ソート
        _rankingBoard.ranking = _rankingBoard.ranking.OrderByDescending(x => x.Score).ToList();

        //必要な要素数のみ残す
        if (_rankingBoard.ranking.Count > _rankingNum)
        {
            _rankingBoard.ranking.RemoveAt(_rankingNum);
        }

        //JsonDataを保存
        File.WriteAllText(_filePath, JsonUtility.ToJson(_rankingBoard, true));
    }
}
