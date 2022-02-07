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
        
        public static ViewportPoint operator +(ViewportPoint pointA, ViewportPoint pointB)
        { 
            return new ViewportPoint(pointA.x + pointB.x, pointA.y + pointB.y);
        }
        
        public static ViewportPoint operator -(ViewportPoint pointA, ViewportPoint pointB)
        {
            return new ViewportPoint(pointA.x - pointB.x, pointA.y - pointB.y);
        }
    
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
    
        public ViewportPoint MinPoint => minPoint;
        
        public ViewportPoint MaxPoint => maxPoint;
        
        public ViewportPoint Size => MaxPoint - MinPoint;
        
        public ViewportPoint CenterPoint => new ViewportPoint(
            (maxPoint.X - minPoint.X) * 0.5f + minPoint.X, 
            (maxPoint.Y - minPoint.Y) * 0.5f + minPoint.Y);
    }
}