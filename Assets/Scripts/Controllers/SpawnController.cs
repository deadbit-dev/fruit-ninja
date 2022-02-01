using System;
using Interfaces;
using Settings;
using UnityEngine;

namespace Controllers 
{
    public class SpawnController : MonoBehaviour, IController
    {
        [SerializeField] private SpawnControllerSettings settings;
        [SerializeField] private BaseSpawner spawnerPrefab;

        private void Start()
        {
            Instantiate();
        }

        public void Instantiate()
        {
            foreach (var spawnerSettings in settings.Spawners)
            {
                BaseSpawner spawner = Instantiate(spawnerPrefab, spawnerSettings.Position, spawnerSettings.Rotation) as BaseSpawner;
                spawner.transform.localScale = spawnerSettings.Scale;
            }
        }

        public void Job()
        {
            // TODO: call random by priority spawner from spawners
        }

        public void Delete()
        {
            // TODO: delete spawner
        }
    }   
}