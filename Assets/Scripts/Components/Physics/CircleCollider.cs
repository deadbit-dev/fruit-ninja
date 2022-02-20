using UnityEngine;
using Interfaces.Physics;

namespace Components.Physics
{
    public class CircleCollider : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float radius;

        public Vector2 Center => transform.position + (Vector3) offset;

        public float Radius
        {
            get => radius;
            set => radius = value;
        }
        
#if UNITY_EDITOR 
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(Center, Vector3.forward, radius);
        }
#endif
    }
}