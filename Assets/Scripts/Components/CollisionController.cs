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
            CircleCollidersExitSquareColliders();
        }

        private void CircleCollidersExitSquareColliders()
        {
            foreach (var trigger in _triggers)
            {
                foreach (var collider in _colliders)
                {
                    if (CircleColliderExitSquareCollider(collider as CircleCollider, trigger as SquareCollider))
                    {
                        collider.CollisionExit(new Collision() {Collider = trigger});
                    }
                }
            }
        }
        
        private static bool CircleColliderExitSquareCollider(CircleCollider circleCollider, SquareCollider squareCollider)
        {
            return circleCollider.Center.x - circleCollider.Radius > squareCollider.Max.x ||
                   circleCollider.Center.x + circleCollider.Radius < squareCollider.Min.x ||
                   circleCollider.Center.y - circleCollider.Radius > squareCollider.Max.y ||
                   circleCollider.Center.y + circleCollider.Radius < squareCollider.Min.y;
        }

        public void AddCollider(BaseCollider collider)
        {
            if (collider.IsTrigger && !_triggers.Contains(collider))
            {
                _triggers.Add(collider);
            }
            else if(!collider.IsTrigger && !_colliders.Contains(collider))
            {
                _colliders.Add(collider);
            }
        }

        public void RemoveCollider(BaseCollider collider)
        {
            if (collider.IsTrigger && _triggers.Contains(collider))
            {
                _triggers.Remove(collider);
            }
            else if(!collider.IsTrigger && _colliders.Contains(collider))
            {
                _colliders.Remove(collider);
            }
        }
    }
}