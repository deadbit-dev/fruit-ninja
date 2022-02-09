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
        
        private Queue<BaseCollider> _addedColliderQueue;
        private Queue<BaseCollider> _removedColliderQueue;

        private void Awake()
        {
            _triggers = new List<BaseCollider>();
            _colliders = new List<BaseCollider>();
            
            _addedColliderQueue = new Queue<BaseCollider>();
            _removedColliderQueue = new Queue<BaseCollider>();
        }

        private void FixedUpdate()
        {
            AddQueue();
                    
            BaseColliderEnterTrigger();
            BaseColliderExitTrigger();
            
            RemoveQueue();
        }


        private void BaseColliderEnterTrigger()
        {
            foreach (var trigger in _triggers)
            {
                foreach (var baseCollider in _colliders)
                {
                    if (trigger as CircleCollider && baseCollider as CircleCollider &&
                        CircleColliderEnterCircleCollider(baseCollider as CircleCollider, trigger as CircleCollider))
                    {
                        trigger.CollisionEnterEvent(new CollisionInfo {Collider = baseCollider});
                    }
                }
            }
        }

        private void BaseColliderExitTrigger()
        {
            foreach (var trigger in _triggers)
            {
                foreach (var baseCollider in _colliders)
                {
                    if (trigger as CollisionSpace2D && baseCollider as CircleCollider &&
                        CircleColliderExitCollisionSpace(baseCollider as CircleCollider, trigger as CollisionSpace2D))
                    { 
                            trigger.CollisionExitEvent(new CollisionInfo {Collider = baseCollider});
                    }
                }
            }
        }

        private static bool CircleColliderExitCollisionSpace(CircleCollider circleCollider,
            CollisionSpace2D collisionSpace2D)
        {
            return circleCollider.Center.x - circleCollider.Radius > collisionSpace2D.Max.x ||
                   circleCollider.Center.x + circleCollider.Radius < collisionSpace2D.Min.x ||
                   circleCollider.Center.y - circleCollider.Radius > collisionSpace2D.Max.y ||
                   circleCollider.Center.y + circleCollider.Radius < collisionSpace2D.Min.y;
        }

        private static bool CircleColliderEnterCircleCollider(CircleCollider circleColliderA,
            CircleCollider circleColliderB)
        {
            return Vector2.Distance(circleColliderA.Center, circleColliderB.Center) < (circleColliderA.Radius + circleColliderB.Radius);
        }

        private void AddQueue()
        {
            for(var _ = 0; _ < _addedColliderQueue.Count; _++)
            {
                var addedCollider = _addedColliderQueue.Dequeue();
                
                if (addedCollider.IsTrigger)
                {
                    _triggers.Add(addedCollider);
                }
                else
                {
                    _colliders.Add(addedCollider);
                }
            }
        }

        private void RemoveQueue()
        {
            for (var _ = 0; _ < _removedColliderQueue.Count; _++)
            {
                var removedCollider = _removedColliderQueue.Dequeue();
                
                if (removedCollider.IsTrigger)
                {
                    _triggers.Remove(removedCollider);
                }
                else
                {
                    _colliders.Remove(removedCollider);
                }
            }
        }
        
        public void AddCollider(BaseCollider baseCollider)
        {
            if (_addedColliderQueue.Contains(baseCollider))
            {
                return;
            }
            
            _addedColliderQueue.Enqueue(baseCollider);
        }

        public void RemoveCollider(BaseCollider baseCollider)
        {
            if (_removedColliderQueue.Contains(baseCollider))
            {
                return;
            }
            
            _removedColliderQueue.Enqueue(baseCollider);
        }
    }
}