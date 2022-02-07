using UnityEngine;
using Components.Utils;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{ 
    [CreateAssetMenu(fileName = "SpawnZone", menuName = "Spawn Zone", order = 0)]
    public class SpawnZone : ScriptableObject
    {
        [SerializeField] private float priorityPercent;
        [Space] 
        [SerializeField] private ViewportPoint firstPoint;
        [SerializeField] private ViewportPoint secondPoint;
        [Space]
        [Header("Angles")]
        [SerializeField, Range(0.0f, 360.0f)] private float firstPointAngle;
        [SerializeField, Range(0.0f, 360.0f)] private float secondPointAngle;
        [Space] 
        [Header("Forces")]
        [SerializeField] private float firstPointForceMin;
        [SerializeField] private float firstPointForceMax;
        [SerializeField] private float secondPointForceMin;
        [SerializeField] private float secondPointForceMax;

        public float PriorityPercent => priorityPercent;
        public ViewportPoint FirstPoint => firstPoint;
        public ViewportPoint SecondPoint => secondPoint;
        public float MinPointAngle => firstPointAngle * Mathf.Deg2Rad;
        public float MinPointForce => Random.Range(firstPointForceMin, firstPointForceMax);
        public float MaxPointAngle => secondPointAngle * Mathf.Deg2Rad;
        public float MaxPointForce => Random.Range(secondPointForceMin, secondPointForceMax);
    }
}