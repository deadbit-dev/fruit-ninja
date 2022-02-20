using System;
using Controllers;
using UnityEngine;

namespace Interfaces.Physics
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        public event Action<ICollision> CollisionEnter;
        public event Action<ICollision> CollisionExit;
        
        public bool IsTrigger
        {
            get => isTrigger;
            set => isTrigger = value;
        }

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