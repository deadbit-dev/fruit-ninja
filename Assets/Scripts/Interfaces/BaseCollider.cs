using System;
using UnityEngine;
using Components.Physics;

namespace Interfaces
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        private Components.Physics.CollisionController _collisionController;
        
        public bool IsTrigger => isTrigger;
        public event Action<CollisionInfo> CollisionExit;
        
        private void Start()
        {
            _collisionController = FindObjectOfType<CollisionController>();
            _collisionController.AddCollider(this);
        }
        
        private void OnDestroy()
        {
            _collisionController.RemoveCollider(this); 
        }

        public void CollisionExitEvent(CollisionInfo info)
        {
            CollisionExit?.Invoke(info);
        }
    }
}