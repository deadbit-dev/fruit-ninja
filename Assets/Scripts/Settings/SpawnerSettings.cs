using System;
using UnityEngine;

namespace Settings
{ 
    [Serializable]
    public class SpawnerSettings
    {
        [SerializeField] private int priority;
        [SerializeField] private Vector3 position;
        [SerializeField] private Quaternion rotation;
        [SerializeField] private Vector3 scale;


        public int Priority => priority;
        public Vector3 Position => position;
        public Quaternion Rotation => rotation;
        public Vector3 Scale => scale;
    }
}