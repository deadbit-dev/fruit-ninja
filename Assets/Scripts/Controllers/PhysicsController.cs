using System.Collections.Generic;
using UnityEngine;
using Interfaces.Physics;
using Components.Physics;

namespace Controllers
{
    public struct CollisionInfo : ICollision
    {
        public BaseCollider Collider { get; set; }
        public Vector3 ContactPosition { get; set; }
    }

    public class PhysicsController : MonoBehaviour
    {
        public static PhysicsController Instance;
        
        private Dictionary<BaseCollider, List<BaseCollider>> _triggers;
        private List<BaseCollider> _colliders;
        
        private Queue<BaseCollider> _addedColliderQueue;
        private Queue<BaseCollider> _removedColliderQueue;

        private void Awake()
        {
            Instance = this;
            
            _triggers = new Dictionary<BaseCollider, List<BaseCollider>>();
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
                        trigger.Key.CollisionEnterEvent(new CollisionInfo
                        {
                            Collider = baseCollider,
                            ContactPosition = ContactPositionWithCircleCollider(((CircleCollider) trigger.Key).Center, baseCollider as CircleCollider)
                        });
                        
                        trigger.Value.Add(baseCollider);
                    }
                }
            }
        }

        private void BaseColliderExitTrigger()
        {
            foreach (var trigger in _triggers)
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

        private static Vector3 ContactPositionWithCircleCollider(Vector2 centerOtherCollider, CircleCollider circleCollider)
        {
            return (centerOtherCollider - circleCollider.Center).normalized * circleCollider.Radius + circleCollider.Center;
        }
        
        private void AddQueue()
        {
            for(var _ = 0; _ < _addedColliderQueue.Count; _++)
            {
                var addedCollider = _addedColliderQueue.Dequeue();
                
                if (addedCollider.IsTrigger)
                {
                    _triggers[addedCollider] = new List<BaseCollider>();
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

        public static void Explosion(Transform transform, float radiusExplosion)
        {
            var explosion = new GameObject("ExplosionPhysics", typeof(CircleCollider));
            var circleCollider = explosion.GetComponent<CircleCollider>();
            
            explosion.transform.position = transform.position;

            circleCollider.Radius = radiusExplosion;
            circleCollider.IsTrigger = true;
            
            circleCollider.CollisionEnter += (info)=>
            {
                Debug.Log(info.Collider.gameObject.tag);
                // add force for unit in trigger by direction center trigger to position unit
            };

            Destroy(explosion, 2f);
        }
    }
}