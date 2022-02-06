using System;
using UnityEngine;
using Collision = Components.Collision;

namespace Interfaces
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;

        public bool IsTrigger => isTrigger;

        public event Action<Collision> CollisionExit;

        public void CollisionExitEvent(Collision info)
        {
            CollisionExit?.Invoke(info);
        }
    }
}