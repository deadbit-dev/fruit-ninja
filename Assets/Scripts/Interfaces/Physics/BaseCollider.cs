using System;
using UnityEngine;

namespace Interfaces.Physics
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        public bool IsTrigger => isTrigger;
        public event Action<ICollision> CollisionEnter;
        public event Action<ICollision> CollisionExit;


        public void CollisionEnterEvent(ICollision info)
        {
            CollisionEnter?.Invoke(info);
        }

        public void CollisionExitEvent(ICollision info)
        {
            CollisionExit?.Invoke(info);
        }
    }
}