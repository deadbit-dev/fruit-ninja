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
        public static CollisionController Instance;
    
        private List<BaseCollider> triggers;
        private List<BaseCollider> colliders;
        
        private Queue<BaseCollider> addedColliderQueue;
        private Queue<BaseCollider> removedColliderQueue;

        private void Awake()
        {
            Instance = this;
            
            triggers = new List<BaseCollider>();
            colliders = new List<BaseCollider>();
            
            addedColliderQueue = new Queue<BaseCollider>();
            removedColliderQueue = new Queue<BaseCollider>();
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
            foreach (var trigger in triggers)
            {
                foreach (var baseCollider in colliders)
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
            foreach (var trigger in triggers)
            {
                foreach (var baseCollider in colliders)
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
            for(var _ = 0; _ < addedColliderQueue.Count; _++)
            {
                var addedCollider = addedColliderQueue.Dequeue();
                
                if (addedCollider.IsTrigger)
                {
                    triggers.Add(addedCollider);
                }
                else
                {
                    colliders.Add(addedCollider);
                }
            }
        }

        private void RemoveQueue()
        {
            for (var _ = 0; _ < removedColliderQueue.Count; _++)
            {
                var removedCollider = removedColliderQueue.Dequeue();
                
                if (removedCollider.IsTrigger)
                {
                    triggers.Remove(removedCollider);
                }
                else
                {
                    colliders.Remove(removedCollider);
                }
            }
        }
        
        public void AddCollider(BaseCollider baseCollider)
        {
            if (addedColliderQueue.Contains(baseCollider))
            {
                return;
            }
            
            addedColliderQueue.Enqueue(baseCollider);
        }

        public void RemoveCollider(BaseCollider baseCollider)
        {
            if (removedColliderQueue.Contains(baseCollider))
            {
                return;
            }
            
            removedColliderQueue.Enqueue(baseCollider);
        }
    }
}