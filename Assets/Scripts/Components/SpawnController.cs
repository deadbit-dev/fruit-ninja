using Settings;
using UnityEngine;

namespace Components 
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private SpawnControllerSettings settings;
        [SerializeField] private PhysicsUnit unitPrefab;
        [SerializeField] private Camera screen;
        
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
            var spawner = settings.Spawners[Utils.RandomRangeWeight(settings.Probes)];
           
            var position = Utils.ScreenToWorldPoint(
                Utils.RandomRangeVector2(spawner.MinPoint, spawner.MaxPoint),
                screen);
                        
            // TODO: get weightVelocity by point between points
            const float weightVelocity = 0.5f;
                        
            var velocity = Vector3.Lerp(
                Physics.GetVelocity(spawner.AngleMinPoint, spawner.ForceMinPoint), 
                Physics.GetVelocity(spawner.AngleMaxPoint, spawner.ForceMaxPoint), 
                weightVelocity);

            Instantiate(unitPrefab, position, Quaternion.identity).Velocity = velocity;
        }
        
        
    }   
}