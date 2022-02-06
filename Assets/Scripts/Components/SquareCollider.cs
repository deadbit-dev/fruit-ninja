using Interfaces;
using UnityEngine;

namespace Components
{
    public class SquareCollider : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 size;
        
        private Bounds _bounds;
        private CollisionController _collisionController;

        public Vector2 Min => _bounds.min;

        public Vector2 Max => _bounds.max;

        private void Start()
        {
            _bounds.size = size;
            
            _collisionController = FindObjectOfType<CollisionController>();
            _collisionController.AddCollider(this);
        }

        private void FixedUpdate()
        {
            _bounds.center = transform.position + (Vector3) offset;
        }

        private void OnDestroy()
        {
           _collisionController.RemoveCollider(this); 
        }
        
#if UNITY_EDITOR        
        private void OnDrawGizmosSelected()
        {
            _bounds.size = size;
            _bounds.center = transform.position + (Vector3) offset;
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireCube(_bounds.center, _bounds.size);
        }
#endif
        
    }
}