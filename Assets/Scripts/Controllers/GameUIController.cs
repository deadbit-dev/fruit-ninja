using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject scoreContainer;
        [SerializeField] private GameObject heartsContainer;
        [SerializeField] private GameObject gameOverPopUp;
        [Space]
        [SerializeField] private List<Text> currentScoreCounts;
        [SerializeField] private List<Text> bestScoreCounts;
        [SerializeField] private GameObject heartPrefab;
        [Space] 
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private HealthController healthController;

        private void OnEnable()
        {
            scoreController.ScoreChanged += SetScore; 
            scoreController.BestScoreChanged += SetBestScore;
            
            healthController.OnLoadHealth += SetHearts;
            healthController.OnHealing += SetHearts;
            healthController.OnDamage += Damage;
        }

        private void OnDisable()
        {
            scoreController.ScoreChanged -= SetScore;
            scoreController.BestScoreChanged -= SetBestScore;
            
            healthController.OnLoadHealth += SetHearts;
            healthController.OnHealing += SetHearts;
            healthController.OnDamage += Damage;
        }

        private void SetHearts(int count)
        {
            for (var _ = 0; _ < count; _++)
            {
                Instantiate(heartPrefab, heartsContainer.transform);
            }
        }

        private void Damage(int count)
        {
            for (var _ = 0; _ < count; _++)
            {
                Destroy(heartsContainer.transform.GetChild(heartsContainer.transform.childCount - 1).gameObject);
            }
        }

        private void SetScore(int score)
        {
            currentScoreCounts.ForEach(count => count.text = score.ToString());
        }

        private void SetBestScore(int score)
        {
            bestScoreCounts.ForEach(count => count.text = score.ToString());
        }
        
        public void StartGame()
        {
            gameOverPopUp.SetActive(false);
            scoreContainer.SetActive(true);
            heartsContainer.SetActive(true);
        }

        public void GameOver()
        {
            scoreContainer.SetActive(false);
            heartsContainer.SetActive(false);
            gameOverPopUp.SetActive(true);
        }
    }
}