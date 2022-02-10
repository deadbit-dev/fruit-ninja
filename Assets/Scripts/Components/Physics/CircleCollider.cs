using System;
using UnityEngine;
using Interfaces.Physics;

namespace Components.Physics
{
    public class CircleCollider : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float radius;

        private Vector3 _position;

        public Vector2 Center => _position;
        public float Radius => radius;

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