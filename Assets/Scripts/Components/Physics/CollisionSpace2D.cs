using System;
using UnityEngine;
using Interfaces.Physics;

namespace Components.Physics
{
    public class CollisionSpace2D : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 extent;

        [SerializeField] private CollisionController collisionController;
        
        public Vector2 Center => (Vector2) transform.position + offset;
        public Vector2 Min => Center - extent;
        public Vector2 Max => Center + extent;

        public Vector2 Offset
        {
            get => offset;
            set => offset = value;
        }
        
        public Vector2 Size
        {
            get => Max - Min;
            set => extent = value / 2;
        }

        private void Start()
        {
            collisionController.AddCollider(this);
        }

        private void OnEnable()
        {
            collisionController.AddCollider(this);
        }

        private void OnDisable() 
        {
            collisionController.RemoveCollider(this);
        }

        private void OnDestroy()
        {
            collisionController.RemoveCollider(this); 
        }
 
#if UNITY_EDITOR        
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireCube(Center, Size);
        }
#endif
        
    }
}