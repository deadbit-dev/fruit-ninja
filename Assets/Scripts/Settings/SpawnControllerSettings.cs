using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpawnControllerSettings", menuName = "Spawn Controller Settings", order = 0)]
    public class SpawnControllerSettings : ScriptableObject
    {
        [SerializeField] private int spawnInterval;
        [SerializeField] private SpawnerSettings[] spawners;

        public int SpawnInterval => spawnInterval;
        public SpawnerSettings[] Spawners => spawners;
    }
}