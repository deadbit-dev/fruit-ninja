using Components;
using UnityEngine;

namespace Settings
{ 
    [CreateAssetMenu(fileName = "SpawnZone", menuName = "Spawn Zone", order = 0)]
    public class SpawnZone: ScriptableObject
    {
        [SerializeField] private float priorityPercent;
        [Space] 
        [SerializeField] private ViewportPoint minPoint;
        [SerializeField] private ViewportPoint maxPoint;
        [Space]
        [Header("Angles")]
        [SerializeField, Range(0.0f, 360.0f)] private float minPointAngle;
        [SerializeField, Range(0.0f, 360.0f)] private float maxPointAngle;
        [Space] 
        [Header("Forces")]
        [SerializeField] private float minPointForce;
        [SerializeField] private float maxPointForce;

        public float PriorityPercent => priorityPercent;
        public ViewportPoint MinPoint => minPoint;
        public ViewportPoint MaxPoint => maxPoint;
        public float MinPointAngle => minPointAngle * Mathf.Deg2Rad;
        public float MinPointForce => minPointForce;
        public float MaxPointAngle => maxPointAngle * Mathf.Deg2Rad;
        public float MaxPointForce => maxPointForce;
    }
}