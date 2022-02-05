using UnityEngine;
using Collision = Components.Collision;

namespace Interfaces
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;

        public bool IsTrigger => isTrigger;
        public abstract void CollisionExit(Collision info);
    }
}