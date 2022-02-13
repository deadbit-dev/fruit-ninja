using System.Collections.Generic;
using UnityEngine;
using Interfaces.Physics;
using Components.Physics;

namespace Controllers
{
    public struct CollisionInfo : ICollision
    {
        public BaseCollider Collider { get; set; }
    }

    public class PhysicsController : MonoBehaviour
    {
        public static PhysicsController Instance;
        
        private Dictionary<BaseCollider, List<BaseCollider>> triggers;
        private List<BaseCollider> colliders;
        
        private Queue<BaseCollider> addedColliderQueue;
        private Queue<BaseCollider> removedColliderQueue;

        private void Awake()
        {
            Instance = this;
            
            triggers = new Dictionary<BaseCollider, List<BaseCollider>>();
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
                    if (baseCollider == null || trigger.Value.Contains(baseCollider))
                    {
                        continue;
                    }

                    if (trigger.Key is SpaceCollider2D && baseCollider is CircleCollider && 
                        CircleColliderInsideSpaceCollider2D(baseCollider as CircleCollider, trigger.Key as SpaceCollider2D))
                    {
                        trigger.Key.CollisionEnterEvent(new CollisionInfo {Collider = baseCollider});
                        
                        trigger.Value.Add(baseCollider);
                    }

                    if (trigger.Key is CircleCollider && baseCollider is CircleCollider &&
                        CircleColliderInsideCircleCollider(baseCollider as CircleCollider, trigger.Key as CircleCollider))
                    {
                        trigger.Key.CollisionEnterEvent(new CollisionInfo {Collider = baseCollider});
                        
                        trigger.Value.Add(baseCollider);
                    }
                }
            }
        }

        private void BaseColliderExitTrigger()
        {
            foreach (var trigger in triggers)
            {
                for (var i= trigger.Value.Count - 1; i >= 0; i--)
                {
                    if (trigger.Value[i] == null)
                    {
                        trigger.Value.RemoveAt(i);
                        continue;
                    }
                    
                    if (trigger.Key is SpaceCollider2D && trigger.Value[i] is CircleCollider && 
                        !CircleColliderInsideSpaceCollider2D(trigger.Value[i] as CircleCollider, trigger.Key as SpaceCollider2D))
                    {
                        trigger.Key.CollisionExitEvent(new CollisionInfo {Collider = trigger.Value[i]});
                        
                        trigger.Value.RemoveAt(i);
                    }

                    if (trigger.Key is CircleCollider && trigger.Value[i] is CircleCollider &&
                        !CircleColliderInsideCircleCollider(trigger.Value[i] as CircleCollider,
                            trigger.Key as CircleCollider))
                    {
                        trigger.Key.CollisionExitEvent(new CollisionInfo {Collider = trigger.Value[i]});
                        
                        trigger.Value.RemoveAt(i);
                    }
                }
            }
        }

        private static bool CircleColliderInsideSpaceCollider2D(CircleCollider circleCollider,
            SpaceCollider2D spaceCollider2D)
        {
            return circleCollider.Center.x - circleCollider.Radius < spaceCollider2D.Max.x &&
                   circleCollider.Center.x + circleCollider.Radius > spaceCollider2D.Min.x &&
                   circleCollider.Center.y - circleCollider.Radius < spaceCollider2D.Max.y &&
                   circleCollider.Center.y + circleCollider.Radius > spaceCollider2D.Min.y;
        }

        private static bool CircleColliderInsideCircleCollider(CircleCollider circleColliderA,
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
                    triggers[addedCollider] = new List<BaseCollider>();
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