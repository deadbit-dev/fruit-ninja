using System;
using System.Collections;
using UnityEngine;
using Components;
using Controllers.Game;
using ScriptableObjects;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private SpawnSettings settings;
        [Space]
        [SerializeField] private GameController gameController;
        [SerializeField] private GameField2D gameField2D;
        
        private float _spawnStartTime;

        private void OnEnable()
        {
            gameController.OnStart += StartSpawn;
            gameController.OnEnd += StopSpawn;
        }

        private void OnDisable()
        {
            gameController.OnStart -= StartSpawn;
            gameController.OnEnd -= StopSpawn;
        }

        private IEnumerator SpawnPackWithInterval(float interval)
        {
            while (true)
            { 
                yield return new WaitForSeconds(interval);
                
                var spawnZone = settings.SpawnZones[Utils.Math.RandomRangeWeight(settings.Priorities)];
                
                StartCoroutine(SpawnUnitWithDelay(
                    spawnZone,
                    gameField2D.ViewportPointToGameField2D(spawnZone.FirstPoint),
                    gameField2D.ViewportPointToGameField2D(spawnZone.SecondPoint),
                    settings.SpawnPack.DelayCurve.Evaluate((Time.time / _spawnStartTime) / settings.Duration)));               
            }
        }

        private IEnumerator SpawnUnitWithDelay(SpawnZone spawnZone, Vector3 pointA, Vector3 pointB, float delay)
        {
            for (var i = 0; i < settings.SpawnPack.CountCurve.Evaluate((Time.time / _spawnStartTime) / settings.Duration); i++)
            {
                var weight = Random.value;

                var unit = Instantiate(
                    settings.SpawnPack.UnitTypes[Utils.Math.RandomRangeWeight(settings.SpawnPack.Priorities)],
                    Vector3.Lerp(pointA, pointB, weight), Quaternion.identity, gameField2D.transform);

                unit.AddForce2D(
                    Mathf.Lerp(spawnZone.MinPointAngle, spawnZone.MaxPointAngle, weight),
                    Mathf.Lerp(spawnZone.MinPointForce, spawnZone.MaxPointForce, weight));
                
                unit.AddTorque2D(spawnZone.Torque);

                yield return new WaitForSeconds(delay);
            }
        }
        
        public void StartSpawn()
        {
            _spawnStartTime = Time.realtimeSinceStartup;
            StartCoroutine(SpawnPackWithInterval(settings.SpawnCurve.Evaluate((Time.time / _spawnStartTime) / settings.Duration)));
        }
         
        public void StopSpawn()
        {
            StopAllCoroutines();
        }
    }   
}