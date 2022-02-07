using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnController", menuName = "Spawn Controller", order = 0)]
    public class SpawnController: ScriptableObject
    {
        [SerializeField] private int spawnInterval;
        [Space]
        [SerializeField] private SpawnZone[] spawnZones;

        public int SpawnInterval => spawnInterval;
        public SpawnZone[] SpawnZones => spawnZones;
        
        public float[] Priorities => spawnZones.Select(spawnZone => spawnZone.PriorityPercent/100).ToArray();
    }
}