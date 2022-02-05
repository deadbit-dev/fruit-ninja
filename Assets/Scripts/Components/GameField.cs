using System;
using UnityEngine;

namespace Components
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Camera screenSpace;

        // TODO: configuration game field by viewport space and screen space
        
        public Vector3 ViewportToGameField(Vector2 viewportPoint)
        {
            var worldPoint = screenSpace.ViewportToWorldPoint(viewportPoint);
            worldPoint.z = transform.position.z;
            return worldPoint;
        }
    }
}