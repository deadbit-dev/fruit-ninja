using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnController", menuName = "Spawn Controller", order = 0)]
    public class SpawnController: ScriptableObject
    {
        [SerializeField] private float duration;
        [Space]
        [SerializeField] private AnimationCurve spawnCurve;
        [Space]
        [SerializeField] private SpawnZone[] spawnZones;
        [Space] 
        [SerializeField] private SpawnPack spawnPack;

        public float Duration => duration;
        public AnimationCurve SpawnCurve => spawnCurve;
        public SpawnZone[] SpawnZones => spawnZones;
        public SpawnPack SpawnPack => spawnPack;
        
        public float[] Priorities => spawnZones.Select(spawnZone => spawnZone.PriorityPercent/100).ToArray();
    }
}