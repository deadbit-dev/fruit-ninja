using System;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSettings settings;
        [Space] 
        [SerializeField] private HealthController healthController;

        public event Action OnStart;
        public event Action OnEnd;

        private void Start()
        {
            StartGame();
        }

        private void OnEnable()
        {
            healthController.ZeroHealth += GameOver;
        }

        private void OnDisable()
        {
            healthController.ZeroHealth -= GameOver;
        }

        private void StartGame()
        { 
            OnStart?.Invoke();
        }
        
        private void GameOver()
        { 
            OnEnd?.Invoke();
        }

        public void ReloadGame()
        {
            StartGame();
        }
    }
}