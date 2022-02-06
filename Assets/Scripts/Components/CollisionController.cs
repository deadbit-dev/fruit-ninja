using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Components
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
                    if (CircleColliderOutsideSquareCollider(baseCollider as CircleCollider, trigger as SquareCollider))
                    {
                        trigger.CollisionExitEvent(new Collision() {Collider = baseCollider});
                    }
                }
            }
        }
        
        private static bool CircleColliderOutsideSquareCollider(CircleCollider circleCollider, SquareCollider squareCollider)
        {
            return circleCollider.Center.x - circleCollider.Radius > squareCollider.Max.x ||
                   circleCollider.Center.x + circleCollider.Radius < squareCollider.Min.x ||
                   circleCollider.Center.y - circleCollider.Radius > squareCollider.Max.y ||
                   circleCollider.Center.y + circleCollider.Radius < squareCollider.Min.y;
        }

        public void AddCollider(BaseCollider baseCollider)
        {
            switch (baseCollider.IsTrigger)
            {
                case true when !_triggers.Contains(baseCollider):
                    _triggers.Add(baseCollider);
                    break;
                case false when !_colliders.Contains(baseCollider):
                    _colliders.Add(baseCollider);
                    break;
            }
        }

        public void RemoveCollider(BaseCollider baseCollider)
        {
            switch (baseCollider.IsTrigger)
            {
                case true when _triggers.Contains(baseCollider):
                    _triggers.Remove(baseCollider);
                    break;
                case false when _colliders.Contains(baseCollider):
                    _colliders.Remove(baseCollider);
                    break;
            }
        }
    }
}