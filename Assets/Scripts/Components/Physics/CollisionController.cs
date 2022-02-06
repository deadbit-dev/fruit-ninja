using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Components.Physics
{
    public struct Collision
    {
        public BaseCollider Collider;
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
                    if (CircleColliderOutsideCollisionSpace(baseCollider as CircleCollider, trigger as CollisionSpace))
                    {
                        trigger.CollisionExitEvent(new Collision() {Collider = baseCollider});
                    }
                }
            }
        }
        
        private static bool CircleColliderOutsideCollisionSpace(CircleCollider circleCollider, CollisionSpace collisionSpace)
        {
            return circleCollider.Center.x - circleCollider.Radius > collisionSpace.Max.x ||
                   circleCollider.Center.x + circleCollider.Radius < collisionSpace.Min.x ||
                   circleCollider.Center.y - circleCollider.Radius > collisionSpace.Max.y ||
                   circleCollider.Center.y + circleCollider.Radius < collisionSpace.Min.y;
        }
        
        public void AddCollider(BaseCollider baseCollider)
        {
            switch (baseCollider.IsTrigger)
            {
                case true when !_triggers.Contains(baseCollider) && baseCollider is CollisionSpace:
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
                case true when _triggers.Contains(baseCollider) && baseCollider is CollisionSpace:
                    _triggers.Remove(baseCollider);
                    break;
                case false when _colliders.Contains(baseCollider) && baseCollider is CircleCollider:
                    _colliders.Remove(baseCollider);
                    break;
            }
        }
    }
}