using Cysharp.Threading.Tasks;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
   [SerializeField] private ResultText _resultText;

   private async void Start()
   {
      SaveData();
      await SetResultScore();
   }

   private void SaveData()
   {
      // スコアの取り出しと保存
      var score = ScoreManager.Score;
      var userName  = ScoreManager.UserName;
      RankingManager.Instance.Save(userName, score);
   }
   
   private async UniTask SetResultScore()
   {
      await _resultText.GetScoreValue();
   }
}
