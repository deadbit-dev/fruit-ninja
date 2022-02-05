using Components;
using UnityEngine;

namespace Interfaces
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        public abstract void CollisionEnter(CollisionController.Collision info);
        public abstract void CollisionExit(CollisionController.Collision info);
    }
}