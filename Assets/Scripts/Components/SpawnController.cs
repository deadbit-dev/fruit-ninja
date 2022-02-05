using Settings;
using UnityEngine;

namespace Components 
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private SpawnControllerSettings settings;
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
            var spawnZone = settings.SpawnZones[Utils.RandomRangeWeight(settings.Priorities)];

            var weight = Random.value;

            var position = gameField.ViewportToGameField(
                Vector3.Lerp(spawnZone.MinPoint, spawnZone.MaxPoint, weight));

            var unit = Instantiate(unitPrefab, position, Quaternion.identity, gameField.transform);
            
            unit.AddForce2D(
                Mathf.Lerp(spawnZone.MinPointAngle, spawnZone.MaxPointAngle, weight),
                Mathf.Lerp(spawnZone.MinPointForce, spawnZone.MaxPointForce, weight));
        }
    }   
}