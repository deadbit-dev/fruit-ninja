using UnityEngine;
using Interfaces.Physics;

namespace Components.Physics
{
    public class CircleCollider : BaseCollider
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float radius;

        private Vector3 position;

        public Vector2 Center => position;
        public float Radius => radius;
     
        private void FixedUpdate()
        {
            position = transform.position + (Vector3) offset;
        }

#if UNITY_EDITOR 
        private void OnDrawGizmosSelected()
        {
            position = transform.position + (Vector3) offset;
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.forward, radius);
        }
#endif

    }
}