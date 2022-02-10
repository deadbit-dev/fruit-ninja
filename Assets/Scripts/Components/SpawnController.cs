using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Components 
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.SpawnController settings;
        [SerializeField] private GameField2D gameField2D;

        private void Start()
        {
            StartSpawn();
        }

        public void StartSpawn()
        {
            StartCoroutine(SpawnPackWithInterval(settings.SpawnCurve.Evaluate(Time.time / settings.Duration)));
        }

        public void StopSpawn()
        {
            StopCoroutine(nameof(SpawnPackWithInterval));
        }

        private IEnumerator SpawnPackWithInterval(float interval)
        {
            while (true)
            { 
                yield return new WaitForSeconds(interval);
                
                var spawnZone = settings.SpawnZones[Utils.Random.RandomRangeWeight(settings.Priorities)];
                
                StartCoroutine(SpawnUnitWithDelay(
                    spawnZone,
                    gameField2D.ViewportPointToGameField2D(spawnZone.FirstPoint),
                    gameField2D.ViewportPointToGameField2D(spawnZone.SecondPoint),
                    settings.SpawnPack.DelayCurve.Evaluate(Time.time / settings.Duration)));               
            }
        }

        private IEnumerator SpawnUnitWithDelay(SpawnZone spawnZone, Vector3 pointA, Vector3 pointB, float delay)
        {
            for (var i = 0; i < settings.SpawnPack.CountCurve.Evaluate(Time.time / settings.Duration); i++)
            {
                var weight = Random.value;

                var unit = Instantiate(
                    settings.SpawnPack.UnitTypes[Utils.Random.RandomRangeWeight(settings.SpawnPack.Priorities)],
                    Vector3.Lerp(pointA, pointB, weight), Quaternion.identity, gameField2D.transform);

                unit.AddForce2D(
                    Mathf.Lerp(spawnZone.MinPointAngle, spawnZone.MaxPointAngle, weight),
                    Mathf.Lerp(spawnZone.MinPointForce, spawnZone.MaxPointForce, weight));
                
                unit.AddTorque2D(spawnZone.Torque);

                yield return new WaitForSeconds(delay);
            }
        }
    }   
}