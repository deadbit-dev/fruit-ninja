using Components;
using UnityEngine;

namespace Settings
{ 
    [CreateAssetMenu(fileName = "SpawnZone", menuName = "Spawn Zone", order = 0)]
    public class SpawnZone: ScriptableObject
    {
        [SerializeField] private float percentPriority;
        [Space]
        [Header("Points")]
        [SerializeField] private Vector2 minPoint;
        [SerializeField] private Vector2 maxPoint;
        [Space]
        [Header("Angles")]
        [SerializeField, Range(0.0f, 360.0f)] private float minPointAngle;
        [SerializeField, Range(0.0f, 360.0f)] private float maxPointAngle;
        [Space] 
        [Header("Forces")]
        [SerializeField] private float minPointForce;
        [SerializeField] private float maxPointForce;

        public float PercentPriority => percentPriority;
        public Vector2 MinPoint => minPoint;
        public Vector2 MaxPoint => maxPoint;
        public float MinPointAngle => minPointAngle * Mathf.Deg2Rad;
        public float MinPointForce => minPointForce;
        public float MaxPointAngle => maxPointAngle * Mathf.Deg2Rad;
        public float MaxPointForce => maxPointForce;
    }
}