using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpawnController", menuName = "Spawn Controller Settings", order = 0)]
    public class SpawnControllerSettings : ScriptableObject
    {
        [SerializeField] private int spawnInterval;
        [Space]
        [SerializeField] private SpawnZone[] spawners;

        public int SpawnInterval => spawnInterval;
        public SpawnZone[] Spawners => spawners;
        public float[] Probes => spawners.Select(spawnerSettings => spawnerSettings.SpawnPercent/100).ToArray();
    }
}