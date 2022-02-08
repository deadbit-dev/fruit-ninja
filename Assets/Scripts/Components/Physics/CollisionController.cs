using System.Collections.Generic;
using UnityEngine;
using Interfaces.Physics;

namespace Components.Physics
{
    public struct CollisionInfo : ICollision
    {
        public BaseCollider Collider { get; set; }
    }
    
    public class CollisionController : MonoBehaviour
    {
        private List<BaseCollider> _triggers;
        private List<BaseCollider> _colliders;
    
        private void Start()
        {
            _triggers = new List<BaseCollider>();
            _colliders = new List<BaseCollider>();
        }

        private void FixedUpdate()
        {
            BaseColliderExitTrigger();
        }

        private void BaseColliderExitTrigger()
        {
            foreach (var trigger in _triggers)
            {
                foreach (var baseCollider in _colliders)
                {
                    if (CircleColliderOutsideCollisionSpace(baseCollider as CircleCollider, trigger as CollisionSpace2D))
                    {
                        trigger.CollisionExitEvent(new CollisionInfo {Collider = baseCollider});
                    }
                }
            }
        }
        
        private static bool CircleColliderOutsideCollisionSpace(CircleCollider circleCollider, CollisionSpace2D collisionSpace2D)
        {
            return circleCollider.Center.x - circleCollider.Radius > collisionSpace2D.Max.x ||
                   circleCollider.Center.x + circleCollider.Radius < collisionSpace2D.Min.x ||
                   circleCollider.Center.y - circleCollider.Radius > collisionSpace2D.Max.y ||
                   circleCollider.Center.y + circleCollider.Radius < collisionSpace2D.Min.y;
        }
        
        public void AddCollider(BaseCollider baseCollider)
        {
            switch (baseCollider.IsTrigger)
            {
                case true when !_triggers.Contains(baseCollider) && baseCollider is CollisionSpace2D:
                    _triggers.Add(baseCollider);
                    break;
                case false when !_colliders.Contains(baseCollider) && baseCollider is CircleCollider:
                    _colliders.Add(baseCollider);
                    break;
            }
        }

        public void RemoveCollider(BaseCollider baseCollider)
        {
            switch (baseCollider.IsTrigger)
            {
                case true when _triggers.Contains(baseCollider) && baseCollider is CollisionSpace2D:
                    _triggers.Remove(baseCollider);
                    break;
                case false when _colliders.Contains(baseCollider) && baseCollider is CircleCollider:
                    _colliders.Remove(baseCollider);
                    break;
            }
        }
    }
}