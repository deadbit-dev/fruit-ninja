using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        
        [SerializeField] private GameSettings settings;
        
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            StartGame();
        }

        private static void StartGame()
        {
            ScoreController.Instance.LoadScore();
            HealthController.Instance.ZeroHeart += GameOver;
            SpawnController.Instance.StartSpawn();
        }

        public static void StopGame()
        {
            
        }

        private static void GameOver()
        {
            SpawnController.Instance.StopSpawn();
            BladeController.Instance.enabled = false;
            UIController.Instance.GameOver();
        }
    }
}