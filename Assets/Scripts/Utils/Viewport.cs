using System;
using UnityEngine;

namespace Utils
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

        public static ViewportPoint operator *(ViewportPoint pointA, ViewportPoint pointB)
        {
            return new ViewportPoint(pointA.x * pointB.x, pointA.y * pointB.y);
        }

        public static ViewportPoint operator *(ViewportPoint point, float scalar)
        {
            return new ViewportPoint(point.x * scalar, point.y * scalar);
        }

        public static implicit operator Vector2(ViewportPoint point)
        {
            return new Vector2(point.x, point.y);
        }
    }
        
    [Serializable]
    public class ViewportField
    {
        [SerializeField] private ViewportPoint min;
        [SerializeField] private ViewportPoint max;
    
        public ViewportPoint Min => min;
        
        public ViewportPoint Max => max;
        
        public ViewportPoint Size => Max - Min;

        public ViewportPoint Center => Size * 0.5f + Min; 
    }
}