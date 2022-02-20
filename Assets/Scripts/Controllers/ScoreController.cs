using System;
using System.Collections.Generic;
using Components;
using Controllers.Game;
using ScriptableObjects;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private ScoreSettings settings;
        [Space] 
        [SerializeField] private GameController gameController;
        [SerializeField] private SliceController sliceController;
        [SerializeField] private List<string> tagsObjectsForScoring;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;
        public event Action<int> OnCombo;
        public event Action OnZeroCombo;

        private int _currentScore;
        private int _bestScore;
        private int _comboStage;
        
        private int _score;
        private float _previousScoreTime;

        private void OnEnable()
        {
            gameController.OnStart += LoadScore;
            sliceController.OnSlice += Score;
        }

        private void OnDisable()
        {
            gameController.OnStart -= LoadScore;
            sliceController.OnSlice -= Score;
        }

        private void LoadScore()
        {
            _currentScore = 0;
            _bestScore = 0;
            _comboStage = 0;

            _score = settings.ScoreForSlicing;

            if (PlayerPrefs.HasKey("bestScore"))
            {
                _bestScore = PlayerPrefs.GetInt("bestScore");
            }
           
            OnScoreChanged?.Invoke(_currentScore);
            OnBestScoreChanged?.Invoke(_bestScore);
        }

        private void Score(GameObject unit)
        {
            if(!tagsObjectsForScoring.Contains(unit.tag.ToString())) return;
            
            _currentScore += _score;

            if (_currentScore > _bestScore)
            {
                _bestScore = _currentScore;
                PlayerPrefs.SetInt("bestScore", _bestScore);
                PlayerPrefs.Save();
            }
            
            OnScoreChanged?.Invoke(_currentScore);
            OnBestScoreChanged?.Invoke(_bestScore);
            
            Combo();
            
            _previousScoreTime = Time.time;
        }
        
        private void Combo()
        {
            if(Time.time - _previousScoreTime > settings.IntervalForCombo)
            {
                _score = settings.ScoreForSlicing;
                _comboStage = 0;
                
                OnZeroCombo?.Invoke();
            }
            else if(_comboStage < settings.MaxComboStage)
            { 
                _score += settings.ScoreForSlicing;
                _comboStage++;
                
                OnCombo?.Invoke(_comboStage);
            }
        }
        
        public static int GetBestScore()
        {
            return PlayerPrefs.HasKey("bestScore") ? PlayerPrefs.GetInt("bestScore") : 0;
        }

        public static void ResetScore()
        {
            PlayerPrefs.DeleteKey("bestScore");
        }
    }
}