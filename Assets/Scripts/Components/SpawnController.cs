using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Components 
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.SpawnController settings;
        [SerializeField] private GameField gameField;

        private void Start()
        {
            StartSpawn();
        }

        public void StartSpawn()
        {
            StartCoroutine(SpawnWithInterval(settings.SpawnInterval));
        }

        public void StopSpawn()
        {
            StopCoroutine(SpawnWithInterval(settings.SpawnInterval));
        }

        private IEnumerator SpawnWithInterval(float interval)
        {
            while (true)
            { 
                yield return new WaitForSeconds(interval);
                
                var spawnZone = settings.SpawnZones[Utils.Random.RandomRangeWeight(settings.Priorities)];
                
                StartCoroutine(SpawnWithDelay(
                    spawnZone,
                    gameField.ViewportField.ViewportToWorld(spawnZone.FirstPoint),
                    gameField.ViewportField.ViewportToWorld(spawnZone.SecondPoint),
                    settings.SpawnPack.Delay));               
            }
        }

        private IEnumerator SpawnWithDelay(SpawnZone spawnZone, Vector3 pointA, Vector3 pointB, float delay)
        {
            for (var i = 0; i < settings.SpawnPack.Count; i++)
            {
                var weight = Random.value;
                
                var position = Vector3.Lerp(pointA, pointB, weight);

                var unitPrefab = settings.SpawnPack.UnitTypes[Utils.Random.RandomRangeWeight(settings.SpawnPack.Priorities)];
                
                var unit = Instantiate(unitPrefab, position, Quaternion.identity, gameField.transform);
                
                unit.AddForce2D(
                    Mathf.Lerp(spawnZone.MinPointAngle, spawnZone.MaxPointAngle, weight),
                    Mathf.Lerp(spawnZone.MinPointForce, spawnZone.MaxPointForce, weight));

                yield return new WaitForSeconds(delay);
            }
        }
    }   
}