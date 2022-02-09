using System;
using UnityEngine;
using Interfaces.Physics;

namespace Components.Physics
{
    public class CircleCollider : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float radius;
        [SerializeField] private CollisionController collisionController;

        private Vector3 _position;

        public CollisionController CollisionController
        {
            get => collisionController;
            set => collisionController = value;
        }
        public Vector2 Center => _position;
        public float Radius => radius;

        private void Start()
        {
            collisionController.AddCollider(this);
        }

        private void OnEnable()
        {
            if (collisionController == null)
            {
                return;
            }

            collisionController.AddCollider(this);
        }

        private void OnDisable() 
        {
            if (collisionController == null)
            {
                return;
            }
            
            collisionController.RemoveCollider(this);
        }

        private void OnDestroy()
        {
            collisionController.RemoveCollider(this); 
        }
        
        private void FixedUpdate()
        {
            _position = transform.position + (Vector3) offset;
        }

#if UNITY_EDITOR 
        private void OnDrawGizmosSelected()
        {
            _position = transform.position + (Vector3) offset;
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(_position, Vector3.forward, radius);
        }
#endif

    }
}