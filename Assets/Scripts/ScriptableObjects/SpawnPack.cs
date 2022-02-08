using System;
using System.Linq;
using Components.Physics;
using UnityEditor.UIElements;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public struct SpawnUnitInfo
    {
        [SerializeField] private float priorityPercent;
        [SerializeField] private PhysicsUnit unitType;

        public float PriorityPercent => priorityPercent;
        public PhysicsUnit UnitType => unitType;
    }

    [CreateAssetMenu(fileName = "SpawnPack", menuName = "Spawn Pack", order = 0)]
    public class SpawnPack : ScriptableObject
    {
        [SerializeField] private AnimationCurve delayCurve;
        [Space]
        [SerializeField] private AnimationCurve countCurve;
        [Space]
        [SerializeField] private SpawnUnitInfo[] spawnUnits;
        
        public AnimationCurve DelayCurve => delayCurve; 
        public AnimationCurve CountCurve => countCurve; 
        public PhysicsUnit[] UnitTypes => spawnUnits.Select(spawnUnit => spawnUnit.UnitType).ToArray();
        
        public float[] Priorities => spawnUnits.Select(spawnUnit => spawnUnit.PriorityPercent/100).ToArray();
    }
}