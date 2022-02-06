using System;
using UnityEngine;

namespace Components
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Camera screenSpace;
        [SerializeField] private SquareCollider squareCollider;

        // TODO: configuration game field by viewport space and screen space
        
        private void Start()
        {
            squareCollider.CollisionExit += CollisionExit;
        }
        
        private static void CollisionExit(Collision info)
        {
            Destroy(info.Collider.gameObject);
        }
        
        public Vector3 ViewportToGameField(Vector2 viewportPoint)
        {
            var worldPoint = screenSpace.ViewportToWorldPoint(viewportPoint);
            worldPoint.z = transform.position.z;
            return worldPoint;
        }
    }
}