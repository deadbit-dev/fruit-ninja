using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "Spawn Settings", order = 0)]
    public class SpawnSettings: ScriptableObject
    {
        [SerializeField] private float duration;
        [Space]
        [SerializeField] private AnimationCurve spawnCurve;
        [Space]
        [SerializeField] private SpawnZoneInfo[] spawnZones;
        [Space] 
        [SerializeField] private SpawnPack spawnPack;

        public float Duration => duration;
        public AnimationCurve SpawnCurve => spawnCurve;
        public SpawnZone[] SpawnZones => spawnZones.Select(spawnZone => spawnZone.SpawnZone).ToArray();
        public SpawnPack SpawnPack => spawnPack;
        
        public float[] Priorities => spawnZones.Select(spawnZone => spawnZone.PriorityPercent/100).ToArray();
    }
}