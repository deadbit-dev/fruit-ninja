using Components;
using UnityEngine;

namespace Controllers
{
    public class ScoreController : MonoBehaviour
    {
        public static ScoreController Instance;

        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadScore()
        {
            CurrentScore = 0;
            BestScore = 0;
            
            if (PlayerPrefs.HasKey("bestScore"))
            {
                BestScore = PlayerPrefs.GetInt("bestScore");
            }
            
            UIController.Instance.SetScore(CurrentScore, BestScore);  
        }

        public void SetScore(Score score)
        {
            CurrentScore += score.Count;
            
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
                PlayerPrefs.SetInt("bestScore", BestScore);
                PlayerPrefs.Save();
            }
            
            UIController.Instance.SetScore(CurrentScore, BestScore);
        }

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("bestScore");
        }
    }
}