using System;
using Components;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private ScoreSettings settings;

        public event Action<int> ScoreChanged;
        public event Action<int> BestScoreChanged;

        private int _currentScore;
        private int _bestScore;

        public void LoadScore()
        {
            _currentScore = 0;
            _bestScore = 0;
            
            if (PlayerPrefs.HasKey("bestScore"))
            {
                _bestScore = PlayerPrefs.GetInt("bestScore");
            }
           
            ScoreChanged?.Invoke(_currentScore);
            BestScoreChanged?.Invoke(_bestScore);
        }

        public void AddScore()
        {
            _currentScore += settings.ScoreForSlicing;
            
            if (_currentScore > _bestScore)
            {
                _bestScore = _currentScore;
                PlayerPrefs.SetInt("bestScore", _bestScore);
                PlayerPrefs.Save();
            }
            
            ScoreChanged?.Invoke(_currentScore);
            BestScoreChanged?.Invoke(_bestScore);           
        }

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("bestScore");
        }
    }
}