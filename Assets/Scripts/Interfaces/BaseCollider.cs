using System;
using Components;
using UnityEngine;
using Collision = Components.Collision;

namespace Interfaces
{
    public abstract class BaseCollider: MonoBehaviour, ICollider
    {
        [SerializeField] private bool isTrigger;
        
        private CollisionController _collisionController;
        
        public bool IsTrigger => isTrigger;
        public event Action<Collision> CollisionExit;
        
        private void Start()
        {
            _collisionController = FindObjectOfType<CollisionController>();
            _collisionController.AddCollider(this);
        }
        
        private void OnDestroy()
        {
            _collisionController.RemoveCollider(this); 
        }

        public void CollisionExitEvent(Collision info)
        {
            CollisionExit?.Invoke(info);
        }
    }
}