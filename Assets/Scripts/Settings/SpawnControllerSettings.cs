using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpawnController", menuName = "Spawn Controller Settings", order = 0)]
    public class SpawnControllerSettings : ScriptableObject
    {
        [SerializeField] private int spawnInterval;
        [SerializeField] private SpawnerSettings[] spawnersSettings;

        public int SpawnInterval => spawnInterval;
        public IEnumerable<SpawnerSettings> SpawnersSettings => spawnersSettings;
        public float[] Probes => spawnersSettings.Select(spawnerSettings => spawnerSettings.SpawnPercent/100).ToArray();
    }
}