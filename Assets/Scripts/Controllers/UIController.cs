using System.Collections.Generic;
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
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private GameObject gameOver;

        private void Awake()
        {
            Instance = this;
        }

        public void SetHeart(int hearts)
        {
            for (var _ = 0; _ < hearts; _++)
            {
                Instantiate(heartPrefab, heartsContainer.transform);
            }
        }

        public void Damage(int hearts)
        {
            for (var _ = 0; _ < hearts; _++)
            {
                Destroy(heartsContainer.transform.GetChild(heartsContainer.transform.childCount - 1).gameObject);
            }
        }

        public void SetScore(int score)
        {
            currentScoreCounts.ForEach(count => count.text = score.ToString());
        }

        public void SetBestScore(int score)
        {
            bestScoreCounts.ForEach(count => count.text = score.ToString());
        }

        public void GameOver()
        {
            scoreContainer.SetActive(false);
            heartsContainer.SetActive(false);
            gameOver.SetActive(true);
        }
    }
}