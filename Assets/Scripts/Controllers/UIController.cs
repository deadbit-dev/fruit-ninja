using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        
        [SerializeField] private GameObject scoreContainer;
        [SerializeField] private List<Text> currentScoreCounts;
        [SerializeField] private List<Text> bestScoreCounts;
        [SerializeField] private GameObject heartsContainer;
        [SerializeField] private GameObject gameOver;

        private void Awake()
        {
            Instance = this;
        }

        public void SetScore(int currentScore, int bestScore)
        {
            currentScoreCounts.ForEach(count => count.text = currentScore.ToString());
            bestScoreCounts.ForEach(count => count.text = bestScore.ToString());
        }

        public void GameOver()
        {
            scoreContainer.SetActive(false);
            heartsContainer.SetActive(false);
            gameOver.SetActive(true);
        }
    }
}