using UnityEngine;

namespace Settings
{ 
    [CreateAssetMenu(fileName = "Spawner", menuName = "Spawner Settings", order = 0)]
    public class SpawnerSettings: ScriptableObject
    {
        [SerializeField] private float spawnPercent;
        [SerializeField] private Vector2 minPoint;
        [SerializeField] private Vector2 maxPoint;
        [SerializeField] private float angleMinPoint;
        [SerializeField] private float forceMinPoint;
        [SerializeField] private float angleMaxPoint;
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