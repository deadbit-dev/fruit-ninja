using Components;
using Components.Physics;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Math = Utils.Math;

namespace Controllers.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSettings settings;
        [Space] 
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private EffectController effectController;
        [SerializeField] private GameUIController gameUIController;
        [SerializeField] private HealthController healthController;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private BladeController bladeController;
        [SerializeField] private SpawnController spawnController;

        private void Start()
        {
            StartGame();
        }

        private void OnEnable()
        {
            healthController.ZeroHealth += GameOver;
            gameField2D.UnitExit += UnitExitGameField;
            bladeController.OnBladeContact += BladeContact;
        }

        private void OnDisable()
        {
            healthController.ZeroHealth -= GameOver;
            gameField2D.UnitExit -= UnitExitGameField;
            bladeController.OnBladeContact -= BladeContact;
        }

        private void StartGame()
        { 
            scoreController.LoadScore();
            healthController.LoadHealth();
            gameUIController.StartGame();
            bladeController.enabled = true;
            spawnController.StartSpawn();
        }
        
        private void BladeContact(GameObject unit, Vector3 contactPosition)
        {
            switch (unit.tag)
            {
                case "Fruit":
                    Slicer.Slice(unit, contactPosition);
                    effectController.SplatterEffect2D(contactPosition, Color.white);
                    scoreController.AddScore();
                    break;
                
                case "Heart":
                    Slicer.Slice(unit, contactPosition);
                    effectController.SplatterEffect2D(contactPosition, Color.magenta);
                    healthController.Healing();
                    break;
                
                case "Bomb":
                    Destroy(unit);
                    effectController.ExplosionEffect2D(contactPosition);
                    healthController.Damage();
                    break;
            }
        }

        private void UnitExitGameField(GameObject unit)
        {
            switch (unit.tag)
            {
                case "Fruit":
                    healthController.Damage();
                    Destroy(unit);
                    break;
                
                default:
                    Destroy(unit);
                    break;
            }
        }

        private void GameOver()
        { 
            spawnController.StopSpawn();
            bladeController.enabled = false;
            gameUIController.GameOver();
        }

        public void ReloadGame()
        {
            StartGame();
        }
    }
}