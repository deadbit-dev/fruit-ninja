using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Menu
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private Text bestScoreCount;
        [Space] 
        [SerializeField] private ScoreController scoreController;

        private void OnEnable()
        {
            scoreController.BestScoreChanged += SetBestScore;
        }

        private void OnDisable()
        { 
            scoreController.BestScoreChanged -= SetBestScore;
        }

        private void SetBestScore(int score)
        {
            bestScoreCount.text = score.ToString();
        }
    }
}