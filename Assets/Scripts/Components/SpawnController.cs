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
            StartCoroutine(SpawnPackWithInterval(settings.SpawnInterval));
        }

        public void StopSpawn()
        {
            StopCoroutine(SpawnPackWithInterval(settings.SpawnInterval));
        }

        private IEnumerator SpawnPackWithInterval(float interval)
        {
            while (true)
            { 
                yield return new WaitForSeconds(interval);
                
                var spawnZone = settings.SpawnZones[Utils.Random.RandomRangeWeight(settings.Priorities)];
                
                StartCoroutine(SpawnUnitWithDelay(
                    spawnZone,
                    gameField.ViewportPointToWorldPoint(spawnZone.FirstPoint),
                    gameField.ViewportPointToWorldPoint(spawnZone.SecondPoint),
                    settings.SpawnPack.Delay));               
            }
        }

        private IEnumerator SpawnUnitWithDelay(SpawnZone spawnZone, Vector3 pointA, Vector3 pointB, float delay)
        {
            for (var i = 0; i < settings.SpawnPack.Count; i++)
            {
                var weight = Random.value;
                
                var unit = Instantiate(
                    settings.SpawnPack.UnitTypes[Utils.Random.RandomRangeWeight(settings.SpawnPack.Priorities)],
                    Vector3.Lerp(pointA, pointB, weight), Quaternion.identity, gameField.transform);
                
                unit.AddForce2D(
                    Mathf.Lerp(spawnZone.MinPointAngle, spawnZone.MaxPointAngle, weight),
                    Mathf.Lerp(spawnZone.MinPointForce, spawnZone.MaxPointForce, weight));

                yield return new WaitForSeconds(delay);
            }
        }
    }   
}