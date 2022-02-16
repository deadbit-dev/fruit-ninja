using Components;
using UnityEngine;

namespace Controllers
{
    public class ScoreController : MonoBehaviour
    {
        public static ScoreController Instance;

        private int currentScore;
        private int bestScore;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadScore()
        {
            currentScore = 0;
            bestScore = 0;
            
            if (PlayerPrefs.HasKey("bestScore"))
            {
                bestScore = PlayerPrefs.GetInt("bestScore");
            }
            
            UIController.Instance.SetScore(currentScore);
            UIController.Instance.SetBestScore(bestScore);
        }

        public void SetScore(Score score)
        {
            currentScore += score.Count;
            
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt("bestScore", bestScore);
                PlayerPrefs.Save();
            }
            
            UIController.Instance.SetScore(currentScore);
            UIController.Instance.SetBestScore(bestScore);
        }

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("bestScore");
        }
    }
}