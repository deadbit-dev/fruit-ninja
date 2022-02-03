using Interfaces;
using Settings;
using UnityEngine;

namespace Components.Controllers 
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private SpawnControllerSettings settings;
        [SerializeField] private BaseSpawner spawnerPrefab;

        private float _spawnInterval;
        
        private void Start()
        {
            Instantiate();
            _spawnInterval = settings.SpawnInterval;
        }

        private void Update()
        { 
            if (_spawnInterval <= 0)
            {
                Job();
                _spawnInterval = settings.SpawnInterval;
            }

            _spawnInterval -= Time.deltaTime;
        }

        private void Instantiate()
        {
            if (Camera.main == null)
            {
                return;
            }

            foreach (var spawnerSettings in settings.SpawnersSettings)
            {
                var worldSpaceMinPoint = Camera.main.ScreenToWorldPoint(
                    spawnerSettings.MinPoint * Utils.CurrentDisplay()); 
                
                var worldSpaceMaxPoint = Camera.main.ScreenToWorldPoint(
                    spawnerSettings.MaxPoint * Utils.CurrentDisplay()); 
                 
                var spawner = Instantiate(
                        spawnerPrefab,
                        Utils.MiddleBetweenVector3(worldSpaceMinPoint, worldSpaceMaxPoint),
                        Quaternion.identity, 
                        transform).GetComponent<BaseSpawner>();
                spawner.SetMinPoint(worldSpaceMinPoint);
                spawner.SetMaxPoint(worldSpaceMaxPoint);
                spawner.SetAngleMinPoint(spawnerSettings.AngleMinPoint);
                spawner.SetForceMinPoint(spawnerSettings.ForceMinPoint);
                spawner.SetAngleMaxPoint(spawnerSettings.AngleMaxPoint);
                spawner.SetForceMaxPoint(spawnerSettings.ForceMaxPoint);
            }
        }

        private void Job()
        {
            transform.GetChild(Utils.RandomRangeWeight(settings.Probes)).GetComponent<BaseSpawner>().Spawn();
        }
    }   
}