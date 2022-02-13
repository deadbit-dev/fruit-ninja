using System;
using UnityEngine;
using Controllers;

namespace Interfaces.Physics
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        public bool IsTrigger => isTrigger;
        public event Action<ICollision> CollisionEnter;
        public event Action<ICollision> CollisionExit;
        
        private void Awake()
        {
            PhysicsController.Instance.AddCollider(this); 
        }

        private void OnEnable()
        {
            PhysicsController.Instance.AddCollider(this); 
        }

        private void OnDisable() 
        {
            PhysicsController.Instance.RemoveCollider(this); 
        }
        
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