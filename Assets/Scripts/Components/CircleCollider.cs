using System;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class CircleCollider : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float radius;

        private Vector3 _position;
        private CollisionController _collisionController;

        public Vector2 Center => _position;
        public float Radius => radius;
        
        private void Start()
        {
            _collisionController = FindObjectOfType<CollisionController>();
            _collisionController.AddCollider(this);
        }

        private void FixedUpdate()
        {
            _position = transform.position + (Vector3) offset;
        }

        private void OnDestroy()
        {
           _collisionController.RemoveCollider(this); 
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