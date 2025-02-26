using UnityEngine;

public class ResultManager : MonoBehaviour
{
   [SerializeField] private ResultText _resultText;

   private void Start()
   {
      SaveData();
   }

   private void SaveData()
   {
      // スコアの取り出しと保存
      var score = ScoreManager.Score;
      var userName  = ScoreManager.UserName;
      Debug.Log($"Score: {score}, UserName: {userName}");
      
      RankingManager.Instance.Save(userName, score);
   }
   
   private void SetResultScore()
   {
      _resultText.GetScoreValue();
   }
}
