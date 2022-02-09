using System;
using UnityEngine;
using Components.Physics;

namespace Interfaces.Physics
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        private CollisionController _collisionController;
        
        public bool IsTrigger => isTrigger;
        public event Action<ICollision> CollisionExit;
        
        private void Start()
        {
            _collisionController = FindObjectOfType<CollisionController>();
            _collisionController.AddCollider(this);
        }

        private void OnDestroy()
        {
            _collisionController.RemoveCollider(this); 
        }

        public void CollisionExitEvent(ICollision info)
        {
            CollisionExit?.Invoke(info);
        }
    }
}