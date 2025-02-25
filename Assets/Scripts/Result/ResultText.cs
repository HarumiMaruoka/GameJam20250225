using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    [SerializeField] Text _scoreText;

    void Start()
    {
        GetScoreValue();
    }

    /// <summary>
    /// スコアの値を受け取るメソッド
    /// </summary>
    void GetScoreValue()
    {
        _scoreText.text = ScoreManager.Score.ToString();
    }
}
