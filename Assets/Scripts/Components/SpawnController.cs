using UnityEngine;
using Components.Physics;

namespace Components 
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.SpawnController settings;
        [SerializeField] private GameField gameField;
        [SerializeField] private PhysicsUnit unitPrefab;
        
        private float _spawnInterval;
        
        private void Start()
        {
            _spawnInterval = settings.SpawnInterval;
        }

        private void Update()
        { 
            if (_spawnInterval <= 0)
            {
                Spawn();
                _spawnInterval = settings.SpawnInterval;
            }

            _spawnInterval -= Time.deltaTime;
        }

        private void Spawn()
        {
            var spawnZone = settings.SpawnZones[Utils.Random.RandomRangeWeight(settings.Priorities)];

            var weight = Random.value;

            var position = Vector3.Lerp(
                gameField.ViewportField.ViewportToWorld(spawnZone.MinPoint), 
                gameField.ViewportField.ViewportToWorld(spawnZone.MaxPoint), 
                weight);

            var unit = Instantiate(unitPrefab, position, Quaternion.identity, gameField.transform);
            
            unit.AddForce2D(
                Mathf.Lerp(spawnZone.MinPointAngle, spawnZone.MaxPointAngle, weight),
                Mathf.Lerp(spawnZone.MinPointForce, spawnZone.MaxPointForce, weight));
        }
    }   
}