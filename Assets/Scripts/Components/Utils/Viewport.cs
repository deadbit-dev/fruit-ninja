using System;
using UnityEngine;

namespace Components.Utils
{
    [Serializable]
    public struct ViewportPoint
    {
        // TODO: custom range attribute for float without slider
        [SerializeField, Range(0.0f, 1.0f)] private float x;
        [SerializeField, Range(0.0f, 1.0f)] private float y;
    
        public ViewportPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float X => x;
        public float Y => y;
    
        public static implicit operator Vector2(ViewportPoint point)
        {
            return new Vector2(point.x, point.y);
        }
    }
        
    [Serializable]
    public class ViewportField
    {
        [SerializeField] private ViewportPoint minPoint;
        [SerializeField] private ViewportPoint maxPoint;
        [Space] 
        [SerializeField] private Transform transform;
        [SerializeField] private Camera screenSpace;
    
        public ViewportPoint MinPoint => minPoint;
        public ViewportPoint MaxPoint => maxPoint;
    
        public ViewportPoint CenterPoint => new ViewportPoint(
            (maxPoint.X - minPoint.X) * 0.5f + minPoint.X, 
            (maxPoint.Y - minPoint.Y) * 0.5f + minPoint.Y);
        
        public Vector3 MinPointFieldToWorld => ViewportToWorld(minPoint);
        public Vector3 MaxPointFieldToWorld => ViewportToWorld(maxPoint);
        public Vector3 CenterPointFieldToWorld => ViewportToWorld(CenterPoint);
        public Vector3 ViewportToWorld(ViewportPoint viewportPoint)
        {
            if (transform == null || screenSpace == null)
            {
                return (Vector2) viewportPoint;
            }
        
            var worldPoint = screenSpace.ViewportToWorldPoint((Vector2) viewportPoint);
            worldPoint.z = transform.position.z;
            return worldPoint;
        }
    }
}