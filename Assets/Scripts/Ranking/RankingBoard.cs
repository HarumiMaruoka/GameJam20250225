using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ScoreをJsonに保存するためのList</summary>
[Serializable]
public class RankingBoard
{
    [Header("ランキングのリスト")]
    public List<ScoreData> ranking = new List<ScoreData>(11);
}