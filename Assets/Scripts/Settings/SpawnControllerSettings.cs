using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpawnController", menuName = "Spawn Controller Settings", order = 0)]
    public class SpawnControllerSettings : ScriptableObject
    {
        [SerializeField] private int spawnInterval;
        [Space]
        [SerializeField] private SpawnZone[] spawnZones;

        public int SpawnInterval => spawnInterval;
        public SpawnZone[] SpawnZones => spawnZones;
        
        public float[] Priorities => spawnZones.Select(spawnZone => spawnZone.PercentPriority/100).ToArray();
    }
}