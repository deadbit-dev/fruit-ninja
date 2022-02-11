using System;
using Components.Physics;
using UnityEngine;

namespace Interfaces.Physics
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        public bool IsTrigger => isTrigger;
        public event Action<ICollision> CollisionEnter;
        public event Action<ICollision> CollisionExit;
        
        private void Start()
        {
            CollisionController.Instance.AddCollider(this); 
        }

        private void OnEnable()
        {
            CollisionController.Instance.AddCollider(this); 
        }

        private void OnDisable() 
        {
            CollisionController.Instance.RemoveCollider(this); 
        }

        private void OnDestroy()
        {
            CollisionController.Instance.RemoveCollider(this); 
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