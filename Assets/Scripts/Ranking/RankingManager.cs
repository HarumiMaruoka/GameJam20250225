﻿using System.IO;
using UnityEngine;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    private static RankingManager _instance;

    public static RankingManager Instance => _instance;

    [SerializeField, Header("ランキングの総表示数")]
    private int _rankingNum;

    [SerializeField, Header("ランキングの保存先")]
    private RankingBoard _rankingBoard = new RankingBoard();

    private string _filePath;

    public RankingBoard RankingBoard => _rankingBoard;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //ファイルにアクセスするためのPathを作る
        _filePath = Path.Combine(Application.persistentDataPath, "ranking.json");
    }

    private void Update()
    {
        //"P"Keyが押されたらData消去
        //Buildするときに消してね
        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            DataDelete();
        }
    }

    /// <summary>
    /// 保存してあるデータを参照（一番最初に1回呼び出せばヨシッ！）
    /// </summary>
    public void Load()
    {
        //JsonDataがあればそれを読み込む
        if (File.Exists(_filePath))
        {
            _rankingBoard = JsonUtility.FromJson<RankingBoard>(File.ReadAllText(_filePath));

            foreach(var data  in _rankingBoard.ranking)
            {
                Debug.Log(data.Name + ":" + data.Score);
            }
        }
    }

    /// <summary>
    /// UserNameとScoreを"ゲーム終了時"に保存
    /// </summary>
    public void Save(string userName, float score)
    {
        //ランキングにスコアを追加
        _rankingBoard.ranking.Add(new ScoreData(userName, score));

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

    /// <summary>
    /// データ消去関数
    /// </summary>
    public void DataDelete()
    {
        File.Delete(_filePath);
        _rankingBoard.ranking.Clear();
        _rankingBoard = new();
    }
}
