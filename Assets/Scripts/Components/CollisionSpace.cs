using Interfaces;
using UnityEngine;

namespace Components
{
    public class CollisionSpace : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 extent;
        
        private Vector3 _position;
        
        public Vector2 Min => _position - (Vector3) extent;

        public Vector2 Max
        {
            get => _position + (Vector3) extent;
            set => extent = _position + (Vector3) value;
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
            UnityEditor.Handles.DrawWireCube(_position, Max - Min);
        }
#endif
        
    }
}