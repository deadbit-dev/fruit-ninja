using UnityEngine;

namespace Settings
{ 
    [CreateAssetMenu(fileName = "SpawnZone", menuName = "Spawn Zone", order = 0)]
    public class SpawnZone: ScriptableObject
    {
        [SerializeField] private float spawnPercent;
        [Space]
        [SerializeField] private Vector2 minPoint;
        [SerializeField] private Vector2 maxPoint;
        [Space]
        [SerializeField, Range(0.0f, 360.0f)] private float angleMinPoint;
        [SerializeField, Range(0.0f, 360.0f)] private float angleMaxPoint;
        [Space] 
        [SerializeField] private float forceMinPoint;
        [SerializeField] private float forceMaxPoint;

        public float SpawnPercent => spawnPercent;
        public Vector2 MinPoint => minPoint;
        public Vector2 MaxPoint => maxPoint;
        public float AngleMinPoint => angleMinPoint;
        public float ForceMinPoint => forceMinPoint;
        public float AngleMaxPoint => angleMaxPoint;
        public float ForceMaxPoint => forceMaxPoint;
    }
}