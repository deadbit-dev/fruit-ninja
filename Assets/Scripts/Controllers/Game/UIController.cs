using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Controllers.Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject scoreContainer;
        [SerializeField] private GameObject heartsContainer;
        [SerializeField] private GameObject gameOverPopUp;
        [Space]
        [SerializeField] private List<Text> currentScoreCounters;
        [SerializeField] private List<Text> bestScoreCounters;
        [SerializeField] private Text comboCounter;
        [SerializeField] private float durationScoreCounter;
        [SerializeField] private float durationBestScoreCounter;
        [Space]
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private float durationHeartBorn;
        [SerializeField] private float durationHeartDie;
        [SerializeField] private Vector3 scaleHeartBorn;
        [SerializeField] private Vector3 scaleHeartDie;
        [Space] 
        [SerializeField] private GameController gameController;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private HealthController healthController;

        private int countHearts;
        
        private void OnEnable()
        {
            gameController.OnStart += StartGame;
            gameController.OnEnd += GameOver;
            scoreController.OnScoreChanged += Score; 
            scoreController.OnBestScoreChanged += BestScore;
            scoreController.OnCombo += Combo;
            scoreController.OnZeroCombo += ZeroCombo;
            healthController.OnLoadHealth += Hearts;
            healthController.OnHealing += Hearts;
            healthController.OnDamage += Damage;
        }

        private void OnDisable()
        {
            gameController.OnStart -= StartGame;
            gameController.OnEnd -= GameOver;
            scoreController.OnScoreChanged -= Score;
            scoreController.OnBestScoreChanged -= BestScore;
            scoreController.OnCombo -= Combo;
            scoreController.OnZeroCombo -= ZeroCombo;
            healthController.OnLoadHealth -= Hearts;
            healthController.OnHealing -= Hearts;
            healthController.OnDamage -= Damage;
        }

        private void Hearts(int count)
        {
            for (var _ = 0; _ < count; _++)
            {
                var heart = Instantiate(heartPrefab, heartsContainer.transform);
                heart.transform.DOScale(scaleHeartBorn, durationHeartBorn);
                countHearts++;
            }
        }

        private void Damage(int count)
        {
            for (var _ = 1; _ <= count; _++)
            {
                var heart = heartsContainer.transform.GetChild(countHearts - 1).gameObject;
                heart.transform.DOScale(scaleHeartDie, durationHeartDie);
                Destroy(heart, durationHeartDie);
                countHearts--;
            }
        }

        private void Score(int score)
        {
            currentScoreCounters.ForEach(counter =>
            {
                counter.DOCounter(int.Parse(counter.text), score, durationScoreCounter, false);
            });
        }

        private void BestScore(int score)
        {
            bestScoreCounters.ForEach(count =>
            {
                count.DOCounter(int.Parse(count.text), score, durationBestScoreCounter, false);
            });
        }

        private void Combo(int stage)
        {
            comboCounter.text = "x" + stage.ToString();
        }

        private void ZeroCombo()
        {
            comboCounter.text = "";
        }

        private void StartGame()
        {
            gameOverPopUp.SetActive(false);
            scoreContainer.SetActive(true);
            heartsContainer.SetActive(true);
        }

        private void GameOver()
        {
            scoreContainer.SetActive(false);
            heartsContainer.SetActive(false);
            gameOverPopUp.SetActive(true);
        }
    }
}