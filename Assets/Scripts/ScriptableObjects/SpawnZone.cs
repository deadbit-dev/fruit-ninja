using System;
using UnityEngine;
using Components.Utils;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{
    [Serializable]
    public struct SpawnZoneInfo
    {
        [SerializeField] private float priorityPercent;
        [SerializeField] private SpawnZone spawnZone;

        public float PriorityPercent => priorityPercent;
        public SpawnZone SpawnZone => spawnZone;
    }
    
    [CreateAssetMenu(fileName = "SpawnZone", menuName = "Spawn Zone", order = 0)]
    public class SpawnZone : ScriptableObject
    {
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
        [Space] 
        [Header("Rotate Angle Animation")] 
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;

        public ViewportPoint FirstPoint => firstPoint;
        public ViewportPoint SecondPoint => secondPoint;
        public float MinPointAngle => firstPointAngle * Mathf.Deg2Rad;
        public float MinPointForce => Random.Range(firstPointForceMin, firstPointForceMax);
        public float MaxPointAngle => secondPointAngle * Mathf.Deg2Rad;
        public float MaxPointForce => Random.Range(secondPointForceMin, secondPointForceMax);
        public float RotateAngleForUnit => Random.Range(minAngle, maxAngle);
    }
}